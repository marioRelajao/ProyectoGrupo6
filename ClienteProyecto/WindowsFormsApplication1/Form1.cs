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
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        Image[] imgArray = new Image[70];
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer player2 = new WMPLib.WindowsMediaPlayer();
        Random r = new Random();
        int jugadoresPartida; //
        int cont; //Contador de jugadores que hemos invitado
        int cont2 = 0; //Contador para contar respuestas de los que he invitado
        bool seJuega = true; //Bool para mirar si al final se juega o no
        int idPartida; 
        //string mensaje;
        public Form1()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listaJugadores.ColumnCount = 1;
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

        private void AtenderServidor() 
        { 
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2); 
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //mensaje = trozos[0].Split('\0');
                string [] trozos= mensaje.Split('/'); 
                int codigo = Convert.ToInt32(trozos[0]);

                switch (codigo)
                {
                    case 1: //Jugadores en una fecha determinada  pepe/maria/jose
                        //MessageBox.Show(trozos[1].Replace("/", ", "), "Aqui tienes a tus jugadores");
                        if(trozos[1]=="NOK")
                        {
                            MessageBox.Show("No se ha encontrado ningún jugador en esta fecha");
                        }
                        else
                        {
                            string result = trozos[1];
                            for (int g = 2; g < trozos.Length; g++)
                            {
                                result = result + ", " + trozos[g];
                            }
                            MessageBox.Show(result, "Aqui tienes a tus jugadores");
                        }     
                        break;

                    case 2: //Jugador con mas partidas ganadas  
                            
                        if (trozos[1] == "NOK")
                            {
                                MessageBox.Show("No hemos obtenido resultados", "Error");
                            }
                            else
                            {
                                MessageBox.Show(trozos[1], "Aqui tienes tu consulta");
                            }
                            break;

                    case 3: //Cuantos jugadores hay en una partida, recibimos 3/NOK o 3/NumJugadores
                        
                        if (trozos[1] == "NOK")
                        {
                            MessageBox.Show($"La partida con ID: {idpartida.Text} no existe o no tiene jugadores", "Consulta");
                        }
                        else
                        {
                            MessageBox.Show($"En la partida {idpartida.Text} hay {trozos[1]} jugadores", "Aqui tienes tu consulta");
                        }
                        break;

                    case 4:  //Recibimos la respuesta del servidor en formato 4/NOK si hay  error o bien 4/el nº de partidas ganada
                        
                        if (trozos[1] == "NOK")
                        {
                            MessageBox.Show($"{userName.Text} no existe o bien no ha ganado ninguna partida", "Consulta");
                        }
                        else
                        {
                            MessageBox.Show($"{userName.Text} ha ganado {trozos[1]} partidas", "Aqui tienes tu consulta");
                        }
                        break;

                    case 5: //registro, devuelve 5/MAL O 5/BIEN
                        
                        if (trozos[1]=="MAL")
                        {
                            MessageBox.Show("No se ha podido registrar");
                        }
                        else
                        {
                            MessageBox.Show("Registrado correctamente");
                        }
                        //MessageBox.Show(mensaje);
                        break;

                    case 6: //Hacemos login, nos llega 6/SI/7/Jugador1/jugadorN
                        
                        if (trozos[1] == "SI")
                        {
                            MessageBox.Show("Has iniciado sesion correctamente", "Log In");
                            //Invocamos al thread original para poder realizar la accion y que c# no se queje
                            Invoke(new Action(() =>
                            {
                                this.Text = this.Text + " - " + userLog.Text;
                                int x = 0;
                                listaJugadores.Rows.Clear();
                                for (int i = 3; i < trozos.Length; i++)
                                {
                                    listaJugadores.Rows.Add(trozos[i]);
                                    listaJugadores.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                                    x++;
                                }
                                listaJugadores.Refresh();
                                consultasGrupo.Enabled = true;
                                groupLogin.Visible = false;
                                groupRegistro.Visible = false;
                                consultasGrupo.Visible = true;
                                testCarta1.Visible = true;
                                button2.Visible = true;
                                idCarta.Visible = true;
                                consultasGrupo.Location = new Point(12, 104);
                            }));
                            
                        }
                        else if (mensaje == "NOK")
                        {
                            MessageBox.Show("Revisa los datos o registrate primero", "Error");
                        }
                        break;

                    case 7:
                        //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                        //MessageBox.Show(trozos[1]);
                        //listaJugadores.RowCount = numJug;
                        Invoke(new Action(() =>
                        {
                            int x = 0;
                            listaJugadores.Rows.Clear();
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                listaJugadores.Rows.Add(trozos[i]);
                                listaJugadores.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                                //listaJugadores.Rows[i].Cells[0].Value = trozos[i];
                                x++;
                            }
                            listaJugadores.Refresh();
                        }));
                        break;

                    case 8://el jugador es invitado a una partida, recibe 8/jugador que le invita/id partida
                        DialogResult siono = MessageBox.Show($"Te invita a jugar {trozos[1]}, ¿Aceptas?","Invitación" + userLog.Text, MessageBoxButtons.YesNo);
                        if (siono==DialogResult.Yes)
                        {
                            string mensaje2 = "9/SI/" + trozos[2];
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        else if (siono==DialogResult.No)
                        {
                            string mensaje2 = "9/NO/" + trozos[2];
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        break;

                    case 9://el jugador invitado ha respondido a la invitacion, recibimos 9/SI/idpartida o 9/NO/idpartida
                        
                        cont2++;//Incrementamos pq recibimos una respuesta
                        if (trozos[1]=="SI")
                        {
                            MessageBox.Show("El jugador ha aceptado la petición", userLog.Text);
                        }
                        else
                        {
                            MessageBox.Show("El jugador ha rechazado la petición");
                            seJuega = false;
                        }
                        if (cont2 == cont) //Miramos que hayamos recibido la misma cantidad de respuestas que juagadores invitados
                        { 
                            if (seJuega == true)
                            {
                                //Hacemos cosas porque jugamos
                                string mensaje2 = $"10/SI/{trozos[2]}";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }
                            else
                            {
                                //Avisamos que no jugamos
                                string mensaje2 = $"10/NO/{trozos[2]}";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }
                        }
                        break;

                    case 10: //recibimos el resultado de si jugamos la partida al final o no

                        if (trozos[1] == "SI")
                        {
                            idPartida = Convert.ToInt32(trozos[2]);
                            MessageBox.Show("Se juega ", userLog.Text);
                            Invoke(new Action(() =>
                            {
                                chat.Visible = true;
                            }));
                        }
                        else 
                        {
                            MessageBox.Show("No se juega ", userLog.Text);
                        }
                        break;

                    case 11: //recibimos 11/mensaje/usuario/idPartida
                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add($"{trozos[2]}: {trozos[1]}");

                        }));
                        
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

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

            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

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

                

            }//Mas partidas ganadas
            else if (Consult2.Checked)
            {
                string mensaje = "2/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            //Cuantos jugadores hay en una partida (Lidi)
            else if (Consulta3.Checked)
            {
                // Enviamos numero de consulta y el id de la partida a consultar
                string mensaje = "3/" + idpartida.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            //Consulta 4, miramos el nombre de un jugador y nos dice cuantas partidas ha ganado
            else if (Consulta4.Checked)
            {
                string mensaje = "4/" + userName.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

               
            }
            //El usuario pide la lista de conectados
            //else
            //{
            //    string mensaje = "7/";
                // Enviamos el código 7
            //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //    server.Send(msg);

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";  
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
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

        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + userLog.Text + "/" + passLog.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

 
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
            atender.Abort();
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void InvitarJugador_Click(object sender, EventArgs e)
        {
            
            string mensaje = "8";
            for (cont = 0; cont < listaJugadores.SelectedCells.Count; cont++)
            {
                 string value;
                 value = listaJugadores.SelectedCells[cont].FormattedValue.ToString();
                 mensaje = mensaje + "/"+ value ; 
            }
            if(cont>=5)
            {
                MessageBox.Show("Solo puedes invitar a un máximo de 4 jugadores");
            }
            else
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void simButton_Click(object sender, EventArgs e)
        {
            int defuse = r.Next(4,10);
            pictureBox1.Image = imgArray[defuse];
        }

        private void button6_Click(object sender, EventArgs e) //Boton para enviar un mensaje, 11/Hola/Usuario/idPartida
        {
            string mensaje_clean = mensajeEnviar.Text.Replace("/", "\\"); //Saneamos por si acaso el cliente envia una barra
            string mensaje = $"11/{mensaje_clean}/{userLog.Text}/{idPartida}";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }
    }
}
