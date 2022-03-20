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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9060);
            

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

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
                MessageBox.Show(mensaje.Replace("/", ", "), "Aqui tienes a tus jugadores");

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
                MessageBox.Show(mensaje);
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
            else 
            {
                string mensaje = "4/" + userName.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor en formato NOK/nºPartidas ganadas
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
            consultasGrupo.Enabled = false;



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
    }
}
