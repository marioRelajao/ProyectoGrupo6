using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Image[] imgArray = new Image[70];
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer player2 = new WMPLib.WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("bg.jpg");
            var img = Image.FromFile("cartas.jpg");
            for (int i=0;i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    var index = j *10 + i;
                    imgArray[index] = new Bitmap(680, 1023);
                    var graphics = Graphics.FromImage(imgArray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, 680, 1023), new Rectangle(i * 680, j * 1023, 680, 1023), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }
            testCarta1.Image = imgArray[10];

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9040);

            //Memillo de la musica

            player2.URL = "meow.mp3";
            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");
                player.URL = "rave.mp3";
                player.settings.volume = 0;
                player.settings.setMode("loop", true);
                groupLogin.Enabled = true;
                groupRegistro.Enabled = true;

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Jugadores en una fecha
            if (Consulta1.Checked)
            {
                string mensaje = "1/" + fecha.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor Juan/Maria/Jose
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2);

            }//Mas partidas ganadas
            else if (Consult2.Checked)
            {
                string mensaje = "2/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                mensaje = mensaje.Split('\0')[0];
                if (mensaje == "NOK")
                {
                    MessageBox.Show("No hemos obtenido resultados", "Error");
                }
                else
                {
                    MessageBox.Show(mensaje, "Aqui tienes tu consulta");
                }


            }
            //Cuantos jugadores hay en una partida (Lidi)
            else if (Consulta3.Checked)
            {
                // Enviamos numero de consulta y el id de la partida a consultar
                string mensaje = "3/" + idpartida.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                mensaje = mensaje.Split('\0')[0];
                if (mensaje == "NOK")
                {
                    MessageBox.Show($"La partida con ID: {idpartida.Text} no existe o no tiene jugadores", "Consulta");
                }
                else
                {
                    MessageBox.Show($"En la partida {idpartida.Text} hay {mensaje} jugadores", "Aqui tienes tu consulta");
                }
            }
            //Consulta 4, miramos el nombre de un jugador y nos dice cuantas partidas ha ganado
            else if (Consulta4.Checked)
            {
                string mensaje = "4/" + userName.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor en formato NOK si hay  error o bien el nº de partidas ganadas
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                mensaje = mensaje.Split('\0')[0];
                if (mensaje == "NOK")
                {
                    MessageBox.Show($"{userName.Text} no existe o bien no ha ganado ninguna partida", "Consulta");
                }
                else
                {
                    if (userName.Text == "Pingu") 
                    {
                        
                    }
                    MessageBox.Show($"{userName.Text} ha ganado {mensaje} partidas", "Aqui tienes tu consulta");
                }
            }
            //El usuario pide la lista de conectados
            else
            {
                string mensaje = "7/";
                // Enviamos el código 7
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor en formato NUMJUGADORES/USER/USER/USER
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                mensaje = mensaje.Split('\0')[0];
                int numJug = Convert.ToInt32(mensaje.Split('/')[0]);
                listaJugadores.RowCount = numJug;
                for (int i = 0; i < numJug; i++)
                {
                    listaJugadores.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    listaJugadores.Rows[i].Cells[0].Value = mensaje.Split('/')[i+1];
                }
                listaJugadores.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";  
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            groupLogin.Visible = true;
            groupRegistro.Visible = true;
            consultasGrupo.Visible = false;
            testCarta1.Visible = false;
            button2.Visible = false;
            idCarta.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + nombre.Text + "/" + usuario.Text + "/"+ contrasena.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('/')[1];
            MessageBox.Show(mensaje);




        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + userLog.Text + "/" + passLog.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('/')[1];
            mensaje = mensaje.Split('\0')[0];
            if (mensaje == "SI") 
            {
                MessageBox.Show("Has iniciado sesion correctamente", "Log In");
                consultasGrupo.Enabled = true;
                groupLogin.Visible = false;
                groupRegistro.Visible = false;
                consultasGrupo.Visible = true;
                testCarta1.Visible = true;
                button2.Visible = true;
                idCarta.Visible = true;
                consultasGrupo.Location = new Point(12, 154);
            }
            else if (mensaje == "NOK")
            {
                MessageBox.Show("Revisa los datos o registrate primero", "Error");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void idpartida_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int indice = Convert.ToInt32(idCarta.Text);
            testCarta1.Image = imgArray[indice];
        }

        private void consultarLista_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listaJugadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Test falllido de cosas en los texbox
        private void userLog_TextChanged(object sender, EventArgs e)
        {
            //if (userLog.Text == "")
            //{
            //    userLog.Text = "Username";
            //    userLog.Font = new Font(userLog.Font, FontStyle.Italic);
            //    userLog.BackColor = Color.Tomato;
            //}
        }

        private void userLog_MouseEnter(object sender, EventArgs e)
        {
            //if (userLog.Text == "Username")
            //{
            //    userLog.Text = "";
            //    userLog.Font = new Font(userLog.Font, FontStyle.Regular);
            //    userLog.BackColor = Color.OrangeRed;
            //}
        }

        //para evitar errores, nos desconectamos antes de cerrar el form
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
