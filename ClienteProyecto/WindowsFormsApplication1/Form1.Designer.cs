namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.Conectar = new System.Windows.Forms.Button();
            this.passLog = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.userLog = new System.Windows.Forms.TextBox();
            this.LogIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Consulta4 = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.fecha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Registrar = new System.Windows.Forms.Button();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.Consulta1 = new System.Windows.Forms.RadioButton();
            this.Consulta3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.idpartida = new System.Windows.Forms.TextBox();
            this.Consult2 = new System.Windows.Forms.RadioButton();
            this.Desconectar = new System.Windows.Forms.Button();
            this.groupRegistro = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listaJugadores = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupLogin = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.consultasGrupo = new System.Windows.Forms.GroupBox();
            this.bajaBtn = new System.Windows.Forms.Button();
            this.InvitarJugador = new System.Windows.Forms.Button();
            this.girdIDBaraja = new System.Windows.Forms.DataGridView();
            this.chat = new System.Windows.Forms.GroupBox();
            this.Enviar_msj = new System.Windows.Forms.Button();
            this.mensajeEnviar = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.barajaJugador_dgv = new System.Windows.Forms.DataGridView();
            this.robarCarta = new System.Windows.Forms.Button();
            this.groupRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaJugadores)).BeginInit();
            this.groupLogin.SuspendLayout();
            this.consultasGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.girdIDBaraja)).BeginInit();
            this.chat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barajaJugador_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(84, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 48);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(328, 219);
            this.nombre.Margin = new System.Windows.Forms.Padding(6);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(324, 31);
            this.nombre.TabIndex = 3;
            // 
            // Conectar
            // 
            this.Conectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Conectar.Location = new System.Drawing.Point(888, 713);
            this.Conectar.Margin = new System.Windows.Forms.Padding(6);
            this.Conectar.Name = "Conectar";
            this.Conectar.Size = new System.Drawing.Size(298, 60);
            this.Conectar.TabIndex = 4;
            this.Conectar.Text = "Conectar👍";
            this.Conectar.UseVisualStyleBackColor = true;
            this.Conectar.Click += new System.EventHandler(this.Conectar_Click);
            // 
            // passLog
            // 
            this.passLog.Location = new System.Drawing.Point(264, 138);
            this.passLog.Margin = new System.Windows.Forms.Padding(6);
            this.passLog.Name = "passLog";
            this.passLog.PasswordChar = '*';
            this.passLog.Size = new System.Drawing.Size(324, 31);
            this.passLog.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(30, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 48);
            this.label6.TabIndex = 20;
            this.label6.Text = "Usuario";
            // 
            // userLog
            // 
            this.userLog.Location = new System.Drawing.Point(264, 37);
            this.userLog.Margin = new System.Windows.Forms.Padding(6);
            this.userLog.Name = "userLog";
            this.userLog.Size = new System.Drawing.Size(324, 31);
            this.userLog.TabIndex = 22;
            // 
            // LogIn
            // 
            this.LogIn.ForeColor = System.Drawing.Color.Black;
            this.LogIn.Location = new System.Drawing.Point(324, 194);
            this.LogIn.Margin = new System.Windows.Forms.Padding(6);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(166, 63);
            this.LogIn.TabIndex = 16;
            this.LogIn.Text = "Log In";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 125);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 48);
            this.label1.TabIndex = 11;
            this.label1.Text = "Contraseña";
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(328, 138);
            this.usuario.Margin = new System.Windows.Forms.Padding(6);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(324, 31);
            this.usuario.TabIndex = 21;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(68, 294);
            this.userName.Margin = new System.Windows.Forms.Padding(6);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(120, 31);
            this.userName.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(32, 262);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Usuario del jugador";
            // 
            // Consulta4
            // 
            this.Consulta4.AutoSize = true;
            this.Consulta4.ForeColor = System.Drawing.Color.White;
            this.Consulta4.Location = new System.Drawing.Point(258, 221);
            this.Consulta4.Margin = new System.Windows.Forms.Padding(6);
            this.Consulta4.Name = "Consulta4";
            this.Consulta4.Size = new System.Drawing.Size(422, 29);
            this.Consulta4.TabIndex = 17;
            this.Consulta4.TabStop = true;
            this.Consulta4.Text = "Cuantas partidas ha ganado un jugador";
            this.Consulta4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(348, 294);
            this.button5.Margin = new System.Windows.Forms.Padding(6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(166, 77);
            this.button5.TabIndex = 15;
            this.button5.Text = "Realizar consulta";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(68, 215);
            this.fecha.Margin = new System.Windows.Forms.Padding(6);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(120, 31);
            this.fecha.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(34, 183);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Fecha(yyyy-mm-dd)";
            // 
            // Registrar
            // 
            this.Registrar.ForeColor = System.Drawing.Color.Black;
            this.Registrar.Location = new System.Drawing.Point(280, 367);
            this.Registrar.Margin = new System.Windows.Forms.Padding(6);
            this.Registrar.Name = "Registrar";
            this.Registrar.Size = new System.Drawing.Size(150, 63);
            this.Registrar.TabIndex = 12;
            this.Registrar.Text = "Registrarse";
            this.Registrar.UseVisualStyleBackColor = true;
            this.Registrar.Click += new System.EventHandler(this.Registrar_Click);
            // 
            // contrasena
            // 
            this.contrasena.Location = new System.Drawing.Point(328, 302);
            this.contrasena.Margin = new System.Windows.Forms.Padding(6);
            this.contrasena.Name = "contrasena";
            this.contrasena.PasswordChar = '*';
            this.contrasena.Size = new System.Drawing.Size(324, 31);
            this.contrasena.TabIndex = 10;
            // 
            // Consulta1
            // 
            this.Consulta1.AutoSize = true;
            this.Consulta1.ForeColor = System.Drawing.Color.White;
            this.Consulta1.Location = new System.Drawing.Point(258, 100);
            this.Consulta1.Margin = new System.Windows.Forms.Padding(6);
            this.Consulta1.Name = "Consulta1";
            this.Consulta1.Size = new System.Drawing.Size(358, 29);
            this.Consulta1.TabIndex = 7;
            this.Consulta1.TabStop = true;
            this.Consulta1.Text = "Jugadores en determinada fecha";
            this.Consulta1.UseVisualStyleBackColor = true;
            // 
            // Consulta3
            // 
            this.Consulta3.AutoSize = true;
            this.Consulta3.ForeColor = System.Drawing.Color.White;
            this.Consulta3.Location = new System.Drawing.Point(258, 181);
            this.Consulta3.Margin = new System.Windows.Forms.Padding(6);
            this.Consulta3.Name = "Consulta3";
            this.Consulta3.Size = new System.Drawing.Size(409, 29);
            this.Consulta3.TabIndex = 7;
            this.Consulta3.TabStop = true;
            this.Consulta3.Text = "Cuantos jugadores hay en una partida";
            this.Consulta3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(54, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Id de la partida";
            // 
            // idpartida
            // 
            this.idpartida.Location = new System.Drawing.Point(68, 137);
            this.idpartida.Margin = new System.Windows.Forms.Padding(6);
            this.idpartida.Name = "idpartida";
            this.idpartida.Size = new System.Drawing.Size(120, 31);
            this.idpartida.TabIndex = 9;
            // 
            // Consult2
            // 
            this.Consult2.AutoSize = true;
            this.Consult2.ForeColor = System.Drawing.Color.White;
            this.Consult2.Location = new System.Drawing.Point(258, 140);
            this.Consult2.Margin = new System.Windows.Forms.Padding(6);
            this.Consult2.Name = "Consult2";
            this.Consult2.Size = new System.Drawing.Size(400, 29);
            this.Consult2.TabIndex = 8;
            this.Consult2.TabStop = true;
            this.Consult2.Text = "Jugador que ha ganado más partidas";
            this.Consult2.UseVisualStyleBackColor = true;
            // 
            // Desconectar
            // 
            this.Desconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desconectar.Location = new System.Drawing.Point(1476, 715);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(6);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(294, 58);
            this.Desconectar.TabIndex = 10;
            this.Desconectar.Text = "Desconectar👎";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Visible = false;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // groupRegistro
            // 
            this.groupRegistro.BackColor = System.Drawing.Color.Transparent;
            this.groupRegistro.Controls.Add(this.usuario);
            this.groupRegistro.Controls.Add(this.label2);
            this.groupRegistro.Controls.Add(this.contrasena);
            this.groupRegistro.Controls.Add(this.label7);
            this.groupRegistro.Controls.Add(this.nombre);
            this.groupRegistro.Controls.Add(this.Registrar);
            this.groupRegistro.Controls.Add(this.label8);
            this.groupRegistro.Enabled = false;
            this.groupRegistro.ForeColor = System.Drawing.Color.White;
            this.groupRegistro.Location = new System.Drawing.Point(104, 108);
            this.groupRegistro.Margin = new System.Windows.Forms.Padding(6);
            this.groupRegistro.Name = "groupRegistro";
            this.groupRegistro.Padding = new System.Windows.Forms.Padding(6);
            this.groupRegistro.Size = new System.Drawing.Size(692, 469);
            this.groupRegistro.TabIndex = 17;
            this.groupRegistro.TabStop = false;
            this.groupRegistro.Text = "Registro";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(84, 129);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 48);
            this.label7.TabIndex = 24;
            this.label7.Text = "Usuario";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(48, 290);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(235, 48);
            this.label8.TabIndex = 23;
            this.label8.Text = "Contraseña";
            // 
            // listaJugadores
            // 
            this.listaJugadores.AllowUserToAddRows = false;
            this.listaJugadores.AllowUserToDeleteRows = false;
            this.listaJugadores.AllowUserToResizeColumns = false;
            this.listaJugadores.AllowUserToResizeRows = false;
            this.listaJugadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.listaJugadores.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.listaJugadores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.listaJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listaJugadores.ColumnHeadersVisible = false;
            this.listaJugadores.Location = new System.Drawing.Point(678, 73);
            this.listaJugadores.Margin = new System.Windows.Forms.Padding(6);
            this.listaJugadores.Name = "listaJugadores";
            this.listaJugadores.ReadOnly = true;
            this.listaJugadores.RowHeadersVisible = false;
            this.listaJugadores.RowHeadersWidth = 62;
            this.listaJugadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.listaJugadores.Size = new System.Drawing.Size(368, 288);
            this.listaJugadores.TabIndex = 23;
            // 
            // groupLogin
            // 
            this.groupLogin.BackColor = System.Drawing.Color.Transparent;
            this.groupLogin.Controls.Add(this.passLog);
            this.groupLogin.Controls.Add(this.label6);
            this.groupLogin.Controls.Add(this.userLog);
            this.groupLogin.Controls.Add(this.LogIn);
            this.groupLogin.Controls.Add(this.label1);
            this.groupLogin.Enabled = false;
            this.groupLogin.ForeColor = System.Drawing.Color.White;
            this.groupLogin.Location = new System.Drawing.Point(1568, 179);
            this.groupLogin.Margin = new System.Windows.Forms.Padding(6);
            this.groupLogin.Name = "groupLogin";
            this.groupLogin.Padding = new System.Windows.Forms.Padding(6);
            this.groupLogin.Size = new System.Drawing.Size(800, 298);
            this.groupLogin.TabIndex = 6;
            this.groupLogin.TabStop = false;
            this.groupLogin.Text = "LogIn";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(772, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 25);
            this.label9.TabIndex = 22;
            this.label9.Text = "Lista Jugadores";
            // 
            // consultasGrupo
            // 
            this.consultasGrupo.BackColor = System.Drawing.Color.Transparent;
            this.consultasGrupo.Controls.Add(this.bajaBtn);
            this.consultasGrupo.Controls.Add(this.InvitarJugador);
            this.consultasGrupo.Controls.Add(this.listaJugadores);
            this.consultasGrupo.Controls.Add(this.label9);
            this.consultasGrupo.Controls.Add(this.button5);
            this.consultasGrupo.Controls.Add(this.label4);
            this.consultasGrupo.Controls.Add(this.Consulta3);
            this.consultasGrupo.Controls.Add(this.userName);
            this.consultasGrupo.Controls.Add(this.fecha);
            this.consultasGrupo.Controls.Add(this.label3);
            this.consultasGrupo.Controls.Add(this.label5);
            this.consultasGrupo.Controls.Add(this.idpartida);
            this.consultasGrupo.Controls.Add(this.Consult2);
            this.consultasGrupo.Controls.Add(this.Consulta4);
            this.consultasGrupo.Controls.Add(this.Consulta1);
            this.consultasGrupo.Enabled = false;
            this.consultasGrupo.ForeColor = System.Drawing.Color.White;
            this.consultasGrupo.Location = new System.Drawing.Point(15, 613);
            this.consultasGrupo.Margin = new System.Windows.Forms.Padding(6);
            this.consultasGrupo.Name = "consultasGrupo";
            this.consultasGrupo.Padding = new System.Windows.Forms.Padding(6);
            this.consultasGrupo.Size = new System.Drawing.Size(1084, 548);
            this.consultasGrupo.TabIndex = 20;
            this.consultasGrupo.TabStop = false;
            this.consultasGrupo.Text = "consultasGrupo";
            this.consultasGrupo.Visible = false;
            // 
            // bajaBtn
            // 
            this.bajaBtn.ForeColor = System.Drawing.Color.Black;
            this.bajaBtn.Location = new System.Drawing.Point(59, 366);
            this.bajaBtn.Name = "bajaBtn";
            this.bajaBtn.Size = new System.Drawing.Size(200, 66);
            this.bajaBtn.TabIndex = 37;
            this.bajaBtn.Text = "Darse de baja";
            this.bajaBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bajaBtn.UseVisualStyleBackColor = true;
            this.bajaBtn.Click += new System.EventHandler(this.bajaBtn_Click);
            // 
            // InvitarJugador
            // 
            this.InvitarJugador.ForeColor = System.Drawing.Color.Black;
            this.InvitarJugador.Location = new System.Drawing.Point(794, 390);
            this.InvitarJugador.Margin = new System.Windows.Forms.Padding(4);
            this.InvitarJugador.Name = "InvitarJugador";
            this.InvitarJugador.Size = new System.Drawing.Size(154, 62);
            this.InvitarJugador.TabIndex = 24;
            this.InvitarJugador.Text = "Invitar";
            this.InvitarJugador.UseVisualStyleBackColor = true;
            this.InvitarJugador.Click += new System.EventHandler(this.InvitarJugador_Click);
            // 
            // girdIDBaraja
            // 
            this.girdIDBaraja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.girdIDBaraja.ColumnHeadersVisible = false;
            this.girdIDBaraja.Location = new System.Drawing.Point(282, 1079);
            this.girdIDBaraja.Margin = new System.Windows.Forms.Padding(6);
            this.girdIDBaraja.Name = "girdIDBaraja";
            this.girdIDBaraja.RowHeadersVisible = false;
            this.girdIDBaraja.RowHeadersWidth = 82;
            this.girdIDBaraja.Size = new System.Drawing.Size(1628, 73);
            this.girdIDBaraja.TabIndex = 36;
            this.girdIDBaraja.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.girdIDBaraja_CellClick);
            // 
            // chat
            // 
            this.chat.BackColor = System.Drawing.Color.Transparent;
            this.chat.Controls.Add(this.Enviar_msj);
            this.chat.Controls.Add(this.mensajeEnviar);
            this.chat.Controls.Add(this.listBox1);
            this.chat.ForeColor = System.Drawing.Color.White;
            this.chat.Location = new System.Drawing.Point(1053, 327);
            this.chat.Margin = new System.Windows.Forms.Padding(6);
            this.chat.Name = "chat";
            this.chat.Padding = new System.Windows.Forms.Padding(6);
            this.chat.Size = new System.Drawing.Size(678, 438);
            this.chat.TabIndex = 33;
            this.chat.TabStop = false;
            this.chat.Text = "Chat de panas";
            this.chat.Visible = false;
            // 
            // Enviar_msj
            // 
            this.Enviar_msj.Location = new System.Drawing.Point(548, 358);
            this.Enviar_msj.Margin = new System.Windows.Forms.Padding(6);
            this.Enviar_msj.Name = "Enviar_msj";
            this.Enviar_msj.Size = new System.Drawing.Size(118, 44);
            this.Enviar_msj.TabIndex = 2;
            this.Enviar_msj.Text = "Enviar";
            this.Enviar_msj.UseVisualStyleBackColor = true;
            this.Enviar_msj.Click += new System.EventHandler(this.button6_Click);
            // 
            // mensajeEnviar
            // 
            this.mensajeEnviar.Location = new System.Drawing.Point(14, 360);
            this.mensajeEnviar.Margin = new System.Windows.Forms.Padding(6);
            this.mensajeEnviar.MaxLength = 500;
            this.mensajeEnviar.Name = "mensajeEnviar";
            this.mensajeEnviar.Size = new System.Drawing.Size(518, 31);
            this.mensajeEnviar.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(14, 38);
            this.listBox1.Margin = new System.Windows.Forms.Padding(6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(648, 279);
            this.listBox1.TabIndex = 0;
            // 
            // barajaJugador_dgv
            // 
            this.barajaJugador_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.barajaJugador_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.barajaJugador_dgv.ColumnHeadersVisible = false;
            this.barajaJugador_dgv.Location = new System.Drawing.Point(282, 690);
            this.barajaJugador_dgv.Margin = new System.Windows.Forms.Padding(4);
            this.barajaJugador_dgv.Name = "barajaJugador_dgv";
            this.barajaJugador_dgv.RowHeadersVisible = false;
            this.barajaJugador_dgv.RowHeadersWidth = 62;
            this.barajaJugador_dgv.RowTemplate.Height = 28;
            this.barajaJugador_dgv.Size = new System.Drawing.Size(1626, 385);
            this.barajaJugador_dgv.TabIndex = 34;
            this.barajaJugador_dgv.Visible = false;
            // 
            // robarCarta
            // 
            this.robarCarta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.robarCarta.Location = new System.Drawing.Point(1992, 785);
            this.robarCarta.Margin = new System.Windows.Forms.Padding(4);
            this.robarCarta.Name = "robarCarta";
            this.robarCarta.Size = new System.Drawing.Size(166, 33);
            this.robarCarta.TabIndex = 35;
            this.robarCarta.Text = "RobarCarta";
            this.robarCarta.UseVisualStyleBackColor = true;
            this.robarCarta.Visible = false;
            this.robarCarta.Click += new System.EventHandler(this.robarCarta_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2740, 1146);
            this.Controls.Add(this.girdIDBaraja);
            this.Controls.Add(this.robarCarta);
            this.Controls.Add(this.barajaJugador_dgv);
            this.Controls.Add(this.chat);
            this.Controls.Add(this.consultasGrupo);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.groupLogin);
            this.Controls.Add(this.groupRegistro);
            this.Controls.Add(this.Conectar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Explodding Kittens";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupRegistro.ResumeLayout(false);
            this.groupRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaJugadores)).EndInit();
            this.groupLogin.ResumeLayout(false);
            this.groupLogin.PerformLayout();
            this.consultasGrupo.ResumeLayout(false);
            this.consultasGrupo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.girdIDBaraja)).EndInit();
            this.chat.ResumeLayout(false);
            this.chat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barajaJugador_dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button Conectar;
        private System.Windows.Forms.RadioButton Consulta1;
        private System.Windows.Forms.RadioButton Consult2;
        private System.Windows.Forms.RadioButton Consulta3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox idpartida;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.TextBox fecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Registrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox contrasena;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.RadioButton Consulta4;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupRegistro;
        private System.Windows.Forms.TextBox passLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox userLog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView listaJugadores;
        private System.Windows.Forms.GroupBox groupLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox consultasGrupo;
        private System.Windows.Forms.Button InvitarJugador;
        private System.Windows.Forms.GroupBox chat;
        private System.Windows.Forms.Button Enviar_msj;
        private System.Windows.Forms.TextBox mensajeEnviar;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridView barajaJugador_dgv;
        private System.Windows.Forms.Button robarCarta;
        private System.Windows.Forms.DataGridView girdIDBaraja;
        private System.Windows.Forms.Button bajaBtn;
    }
}

