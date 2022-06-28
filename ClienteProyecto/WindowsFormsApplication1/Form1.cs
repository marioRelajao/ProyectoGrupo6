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
using System.Text.RegularExpressions;
//using JuegoLib;



namespace WindowsFormsApplication1
{
     

    public partial class Form1 : Form
    {   

        //------------------------------------VARIABLES PUBLICAS--------------------------------------------//

        List<int> listaIDDisponibles = new List<int>();
        List<CCarta> barajaJugador = new List<CCarta>();
        bool conectado = false;
        Socket server;
        Thread atender;
        Image[] imgArray = new Image[70];
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer player2 = new WMPLib.WindowsMediaPlayer();
        Random r = new Random();
        int cont;                                                               //Contador de jugadores que hemos invitado
        int cont2 = 0;                                                          //Contador para contar respuestas de los que he invitado
        bool seJuega = true;                                                    //Bool para mirar si al final se juega o no
        int idPartida;
        int numJugadores;
        bool host=false;                                                        //creador de la partida
        bool miturno = false;
        string JugadaAResponder;                                                //para que si nos lanzan un attack no podamos lanzar un skip o un defuse, etc
        bool Respondemos = false;                                               //lo mismo
        Regex testFecha = new Regex(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$");                  //Prueba de Expresion Regular para controlar la entrada de texto
        Regex testIdPartida = new Regex(@"^\d{2}");
        //string mensaje;



        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            girdIDBaraja.Visible = false;
            listaJugadores.ColumnCount = 1;
            this.BackgroundImage = Image.FromFile("bg.jpg");
            var img = Image.FromFile("cartas.jpg");
            for (int i=0;i < 10; i++) //Bucles para llenar la lista de imagenes 
            {
                for (int j = 0; j < 7; j++)
                {
                    var index = j *10 + i;
                    imgArray[index] = new Bitmap(680, 1023);
                    var graphics = Graphics.FromImage(imgArray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, 680, 1023), new Rectangle(i * 680, j * 1023, 680, 1023), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    if (index < 56) //Creamos una baraja principal con todas las cartas
                    {
                        listaIDDisponibles.Add(index);
                    }
                }
            }
        }

        //------------------------------------FUNCIONES--------------------------------------------//

        public void MoverCosas()
        {
            listaJugadores.Refresh();
            consultasGrupo.Enabled = true;
            groupLogin.Visible = false;
            groupRegistro.Visible = false;
            consultasGrupo.Visible = true;
            consultasGrupo.Location = new Point(12, 104);
        }
        private List<int> RetornaIDsBaraja(List<int> listaEnviar) //Nos da un vector de ID's que enviaremos al servidor (o nos quedamos si es nuestra baraja)
        {
            Random r = new Random();
            int ID = r.Next(57);                                 //generamos un ID random para sacarlo de la baraja
            while (listaEnviar.Count() < 5)                      //Mientras no hayamos robado las necesarias
            {
                if (listaIDDisponibles.Contains(ID) && ID >= 15) //Si la carta aun no se ha robado y no es bomba/defuse
                {
                    listaEnviar.Add(ID);                         //Añadimos ese ID en la lista
                    listaIDDisponibles.Remove(ID);               //Lo sacamos de las cartas disponibles, pues lo acabamos de "repartir" a una baraja
                }
                ID = r.Next(57);
            }
            ID = r.Next(4, 10);
            listaEnviar.Add(ID);                                //Añadimos ese ID en la lista
            listaIDDisponibles.Remove(ID);
            return listaEnviar;
        }

        private void AñadirColumna(int idCarta)                                                 //Funcion que recibe un ID de carta y lo muestra al jugador
        {            
            DataGridViewImageColumn imageCol2 = new DataGridViewImageColumn();                  //Creamos otra columna de imagenes
            barajaJugador_dgv.Columns.Add(imageCol2);                                           //La añadimos al dgv
            barajaJugador_dgv.Rows[0].Height = 200;                                             //Le decimos lo que mide de alto la celda (si no se ve muy chiquito)
            barajaJugador_dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //Le decimos que se adapte al tamaño de la imagen
            int test = barajaJugador_dgv.Columns.Count;
            Bitmap testDgv2;                                                                    //Creamos otro lienzo (si no sale una cosa con una cruz)
            testDgv2 = new Bitmap(imgArray[idCarta]);                                           //llenamos el lienzo
            Bitmap imgRSZ2 = new Bitmap(testDgv2, new Size(150, 200));                          //Le damos un tamaño en concreto (supongo quue se podra optimizar)
            barajaJugador_dgv.Rows[0].Cells[test-1].Value = imgRSZ2;                            //Lo colocamos en el dgv
        }
        public static void Shuffle(List<int> listaIDDisponibles) //esta función se encarga de mezclar las cartas de la baraja
        {
            Random rand = new Random();
            int n = listaIDDisponibles.Count;
            while (n > 1)
            {
                n--;
                int r = rand.Next(n + 1);
                int value = listaIDDisponibles[r];
                listaIDDisponibles[r] = listaIDDisponibles[n];
                listaIDDisponibles[n] = value;
            }
        }
        private void ListIntACard(List<int> lista) // esta función pasa de lista de enteros a lista de cartas
        {
            CCarta cartaTemp = new CCarta();
            int k=0;
            while(k<lista.Count)
            {
                cartaTemp = new CCarta(lista[k]);
                barajaJugador.Add(cartaTemp);
                k++;
            }
        }

        private void RecargarBaraja() // esta función se encarga de recargar la baraja cada vez que hay un cambio
        {
            girdIDBaraja.RowCount = 1;
            girdIDBaraja.ColumnHeadersVisible = false;
            girdIDBaraja.RowHeadersVisible = false;
            girdIDBaraja.ColumnCount = barajaJugador.Count();
            for (int m = 0; m < barajaJugador.Count(); m++)
            {
                girdIDBaraja[m, 0].Value = barajaJugador[m].RetornaTipo();
                girdIDBaraja.Columns[m].Width = 150;
            }
        }

        //------------------------------------THREAD--------------------------------------------//

        private void AtenderServidor() 
        { 
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2); 
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string [] trozos= mensaje.Split('/'); 
                int codigo = Convert.ToInt32(trozos[0]);

                switch (codigo)
                {
                    case 1: //Jugadores en una fecha determinada  pepe/maria/jose
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
                                    if (trozos[i]!=userLog.Text)
                                    {
                                        listaJugadores.Rows.Add(trozos[i]);
                                        listaJugadores.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                                        x++;
                                    }
                                    MoverCosas();
                                }
                                
                            }));
                            
                        }
                        else 
                        {
                            MessageBox.Show("Revisa los datos o registrate primero", "Error");
                        }
                        break;

                    case 7:
                        
                        Invoke(new Action(() =>
                        {
                            int x = 0;
                            listaJugadores.Rows.Clear();
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                if (trozos[i] != userLog.Text)
                                {
                                    listaJugadores.Rows.Add(trozos[i]);
                                    listaJugadores.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                                    x++;
                                }
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

                    case 10: //recibimos el resultado de si jugamos la partida al final o no 10/SI/id/NUMJUG 10/NO

                        if (trozos[1] == "SI")
                        {
                            idPartida = Convert.ToInt32(trozos[2]);
                            MessageBox.Show("Se juega ", userLog.Text);
                            numJugadores = Convert.ToInt32(trozos[3]);
                            List<int> listaTemp = new List<int>();
                            int k = 1;
                            string mensaje2 = "12/"+idPartida;
                            // 12/13,24,43/3,9,12
                            
                            if (host==true) // solo si es la persona que ha creado la partida, entonces creará la baraja
                            {
                                listaTemp = RetornaIDsBaraja(listaTemp);
                                ListIntACard(listaTemp);
                                Invoke(new Action(() =>
                                { //limpiamos el form y preparamos datagridview con la baraja del jugador
                                    consultasGrupo.Enabled = false;
                                    groupLogin.Visible = false;
                                    groupRegistro.Visible = false;
                                    consultasGrupo.Visible = false;
                                    chat.Location = new Point(12, 12);
                                    robarCarta.Visible = true;
                                    barajaJugador_dgv.Visible = true;
                                    girdIDBaraja.Visible = true;
                                    RecargarBaraja();
                                    barajaJugador_dgv.Rows.Clear();
                                    barajaJugador_dgv.Columns.Clear();
                                    for (int i = 0; i < barajaJugador.Count; i++)
                                    {
                                        AñadirColumna(barajaJugador[i].RetornaId());
                                    }
                                }));
                                listaTemp.Clear();
                                while (k<numJugadores)
                                {
                                    mensaje2 = mensaje2 + "/";
                                    listaTemp = RetornaIDsBaraja(listaTemp);
                                    int x = 0;
                                    while (x< listaTemp.Count())
                                    {
                                        mensaje2 = mensaje2 + listaTemp[x]+",";
                                        x++;
                                    }
                                    
                                    listaTemp.Clear();
                                    k++;
                                }
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }
                            
                            Invoke(new Action(() =>
                            {
                                chat.Visible = true;
                            }));
                        }
                        else 
                        {
                            host = false;
                            MessageBox.Show("No se juega ", userLog.Text);
                        }
                        break;

                    case 11: //recibimos 11/mensaje/usuario/idPartida
                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add($"{trozos[2]}: {trozos[1]}");

                        }));
                        
                        break;

                    case 12: //recibimos 12/carta1,carta2,carta3,carta4... (los ids de las cartas)
                        List<int> listaTemp1 = new List<int>();
                        string[] cartas = trozos[1].Split(',');
                        int l = 0;
                        while(l<6)
                        {
                            listaTemp1.Add(Convert.ToInt32(cartas[l]));
                            l++;
                        }
                        ListIntACard(listaTemp1);
                        Invoke(new Action(() =>
                        { //limpiamos el form y preparamos datagridview con la baraja del jugador
                            consultasGrupo.Enabled = false;
                            groupLogin.Visible = false;
                            groupRegistro.Visible = false;
                            consultasGrupo.Visible = false;                            
                            chat.Location = new Point(12, 12);
                            robarCarta.Visible = true;
                            barajaJugador_dgv.Visible = true;
                            girdIDBaraja.Visible = true;
                            RecargarBaraja();
                            barajaJugador_dgv.Rows.Clear();
                            barajaJugador_dgv.Columns.Clear();
                            for (int i = 0; i < barajaJugador.Count; i++)
                            {
                                AñadirColumna(barajaJugador[i].RetornaId());
                            }
                        }));
                        break;

                    case 13: //el jugador recibirá 13/jugada del anterior jugador, será 13/tipocarta
                        miturno = true;
                        if (trozos.Length>1)
                        {
                            if(trozos[1]=="ataca" || trozos[1]=="favor")
                            {
                                Respondemos = true; // es necesario responder
                                JugadaAResponder = trozos[1];
                            }
                            
                        }
                            break;
                    case 14: //el jugador recibe la confirmacion sobre si se ha dado de baja bien o no
                        // 14/BIEN o 14/MAL
                        if(trozos[1]=="BIEN")
                        {
                            MessageBox.Show("Te has dado de baja correctamente");
                            //Mensaje de desconexión
                            string mensaje2 = "0/";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);

                            // Nos desconectamos
                            Invoke(new Action(() =>
                            {
                                server.Shutdown(SocketShutdown.Both);
                                server.Close();
                                groupLogin.Visible = true;
                                groupRegistro.Visible = true;
                                consultasGrupo.Visible = false;
                                conectado = false;
                                Conectar.Visible = true;
                                Desconectar.Visible = false;
                                LogIn.Enabled = false;
                                Registrar.Enabled = false;
                            }));
                            atender.Abort();

                        }
                        else
                        {
                            MessageBox.Show("No te has podido dar de baja");
                        }

                        break;
                }
            }
        }

        private void Conectar_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50018);

            //Memillo de la musica

            player2.URL = "meow.mp3";
            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                conectado = true;
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");
                player.URL = "rave.mp3";
                player.settings.volume = 5;
                player.settings.setMode("loop", true);
                groupLogin.Enabled = true;
                groupRegistro.Enabled = true;
                Desconectar.Visible = true;
                Conectar.Visible = false;
                LogIn.Enabled = true;
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
                if (testFecha.IsMatch(fecha.Text))//Match = Todo OK
                {
                    string mensaje = "1/" + fecha.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (!testFecha.IsMatch(fecha.Text))
                {
                    MessageBox.Show("Revisa los datos");
                }                           
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
                if (testIdPartida.IsMatch(idpartida.Text))
                {
                    // Enviamos numero de consulta y el id de la partida a consultar
                    string mensaje = "3/" + idpartida.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else
                {
                    MessageBox.Show("Revisa los datos");
                }
                
            }
            //Consulta 4, miramos el nombre de un jugador y nos dice cuantas partidas ha ganado
            else if (Consulta4.Checked)
            {
                string mensaje = "4/" + userName.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }            
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            if (conectado)
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
                conectado = false;
                Conectar.Visible = true;
                Desconectar.Visible = false;
                LogIn.Enabled = false;
                Registrar.Enabled = false;
            }            
        }

        private void Registrar_Click(object sender, EventArgs e)//boton registrarse
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
       
        //para evitar errores, nos desconectamos antes de cerrar el form
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conectado)
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
        }

        private void InvitarJugador_Click(object sender, EventArgs e)
        {
            host = true;
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

        private void button6_Click(object sender, EventArgs e) //Boton para enviar un mensaje, 11/Hola/Usuario/idPartida
        {
            string mensaje_clean = mensajeEnviar.Text.Replace("/", "\\"); //Saneamos por si acaso el cliente envia una barra
            string mensaje = $"11/{mensaje_clean}/{userLog.Text}/{idPartida}";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            mensajeEnviar.Text = "";
        }
        
        private void robarCarta_Click(object sender, EventArgs e)
        {
            if (miturno == true)
            {
                string mensaje2 = "14/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg);
                miturno = false;
            }
        }

        private void girdIDBaraja_CellClick(object sender, DataGridViewCellEventArgs e) //Funcion para ver que tipo de carta clicamos
        {
            if (miturno == true)
            {
                string tipo = girdIDBaraja.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (Respondemos == true)
                {
                    if (JugadaAResponder == "ataca")
                    {
                        //añadiremos más adelante un botón que sea no puedo responder para en el caso en el que no puedas hacer nada contra un ataque
                        //no se quedé colgado el servidor esperando un mensaje
                        if (tipo == "nope" || tipo == "ataca")
                        {
                            string mensaje2 = "13/" + tipo + "/1"; //uno indica que el turno se ha acabado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                            int m = 0;
                            bool encontrado = false;
                            while (encontrado == false)//eliminamos la carta de la baraja para que no la pueda volver a usar
                            {
                                if (barajaJugador[m].RetornaTipo() == tipo)
                                {
                                    barajaJugador.Remove(barajaJugador[m]);
                                    encontrado = true;
                                }
                                else
                                {
                                    m++;
                                }
                            }
                            miturno = false;
                        }
                    }
                    if (JugadaAResponder == "favor")
                    {
                        string mensaje2 = "13/" + tipo + "/1"; //uno indica que el turno se ha acabado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                        server.Send(msg);
                        int m = 0;
                        bool encontrado = false;
                        while (encontrado == false)//eliminamos la carta de la baraja para que no la pueda volver a usar
                        {
                            if (barajaJugador[m].RetornaTipo() == tipo)
                            {
                                barajaJugador.Remove(barajaJugador[m]);
                                encontrado = true;
                            }
                            else
                            {
                                m++;
                            }
                        }
                        miturno = false;
                    }
                }
                else
                {
                    //el defuse irá en otro tipo de mensaje para que el jugador no pueda usarlo asi como asi, solo cuando robe carta y reciba una bomba

                    if (tipo == "nope" || tipo == "skip" || tipo == "ataca")//comprueba que tipo de carta es
                    {
                        string mensaje2 = "13/" + tipo + "/1"; //uno indica que el turno se ha acabado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                        //server.Send(msg);
                        MessageBox.Show(tipo);
                        MessageBox.Show(mensaje2);
                        bool encontrado = false;
                        int m = 0;
                        while (encontrado == false)//eliminamos la carta de la baraja para que no la pueda volver a usar
                        {
                            if (barajaJugador[m].RetornaTipo() == tipo)
                            {
                                barajaJugador.Remove(barajaJugador[m]);
                                encontrado = true;
                            }
                            else
                            {
                                m++;
                            }
                        }
                        miturno = false;
                    }
                    else if (tipo == "futuro" || tipo == "shuffle" || tipo == "favor")
                    {
                        string mensaje2 = "13/" + tipo + "/0"; // 0 indica que sigue siendo su turno
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                        server.Send(msg);
                        bool encontrado = false;
                        int m = 0;
                        while (encontrado == false)//eliminamos la carta de la baraja para que no la pueda volver a usar
                        {
                            if (barajaJugador[m].RetornaTipo() == tipo)
                            {
                                barajaJugador.Remove(barajaJugador[m]);
                                encontrado = true;
                            }
                            else
                            {
                                m++;
                            }
                        }
                    }
                    else if (tipo == "potato" || tipo == "rainbow" || tipo == "melon" || tipo == "taco" || tipo == "barba")
                    {
                        string mensaje2 = "13/" + tipo + "/0"; // 0 indica que sigue siendo su turno
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                        server.Send(msg);
                        int m = 0, contador = 0; //necesitamos el contador ya que estas cartas se usan de 3 en 3
                        bool encontrado = false;
                        while (m < barajaJugador.Count() && contador < 3)
                        {
                            if (barajaJugador[m].RetornaTipo() == tipo)
                            {
                                contador++;
                            }
                            m++;
                        }
                        m = 0;
                        if (contador == 3)
                        {
                            while (contador > 0)
                            {
                                while (encontrado == false)//eliminamos las carta de la baraja para que no las pueda volver a usar
                                {
                                    if (barajaJugador[m].RetornaTipo() == tipo)
                                    {
                                        barajaJugador.Remove(barajaJugador[m]);
                                        encontrado = true;
                                        contador--;
                                    }
                                    else
                                    {
                                        m++;
                                    }
                                }
                                m = 0;
                            }

                        }
                        else
                        {
                            MessageBox.Show("No tienes sufiecientes cartas de este tipo, necesitas 3");
                        }                       
                        RecargarBaraja();
                    }
                    else
                    {
                        MessageBox.Show("No puedes jugar esta carta, vuelve a clicar");
                    }
                    RecargarBaraja();
                    barajaJugador_dgv.Rows.Clear();
                    barajaJugador_dgv.Columns.Clear();
                    for (int i = 0; i < barajaJugador.Count; i++)
                    {
                        AñadirColumna(barajaJugador[i].RetornaId());
                    }
                }

            }
        }

        private void bajaBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "14/" + userLog.Text;
            // Enviamos al servidor el usuario a dar de baja
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }

    //Aqui podemos declarar las clases con sus metodos
    public class CCarta //Clase carta con ID y tipo
    {
        int id;
        string tipo;

        //Constructores
        public CCarta()
        {

        }
        public CCarta(int id)//Constructor que se le pasa un ID y este añade el tipo que es
        {
            this.id = id;
            if (id <= 3)
            {
                this.tipo = "bomba";
            }
            else if (id >= 4 && id <= 9)
            {
                this.tipo = "defuse";
            }
            else if (id >= 10 && id <= 14)
            {
                this.tipo = "nope";
            }
            else if (id >= 15 && id <= 19)
            {
                this.tipo = "futuro";
            }
            else if (id >= 20 && id <= 23)
            {
                this.tipo = "skip";
            }
            else if (id >= 24 && id <= 27)
            {
                this.tipo = "ataca";
            }
            else if (id >= 28 && id <= 31)
            {
                this.tipo = "shuffle";
            }
            else if (id >= 32 && id <= 35)
            {
                this.tipo = "favor";
            }
            else if (id >= 36 && id <= 39)
            {
                this.tipo = "potato";
            }
            else if (id >= 40 && id <= 43)
            {
                this.tipo = "rainbow";
            }
            else if (id >= 44 && id <= 47)
            {
                this.tipo = "taco";
            }
            else if (id >= 48 && id <= 51)
            {
                this.tipo = "melon";
            }
            else if (id >= 52 && id <= 55)
            {
                this.tipo = "barba";
            }
        }

        //Metodos
        public int RetornaId()
        {
            return this.id;
        }
        public string RetornaTipo()
        {
            return this.tipo;
        }
    }
}
