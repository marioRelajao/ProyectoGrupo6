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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.button4 = new System.Windows.Forms.Button();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.Consulta1 = new System.Windows.Forms.RadioButton();
            this.Consulta3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.idpartida = new System.Windows.Forms.TextBox();
            this.Consult2 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupRegistro = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listaJugadores = new System.Windows.Forms.DataGridView();
            this.testCarta1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.idCarta = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupLogin = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.consultasGrupo = new System.Windows.Forms.GroupBox();
            this.InvitarJugador = new System.Windows.Forms.Button();
            this.groupRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaJugadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testCarta1)).BeginInit();
            this.groupLogin.SuspendLayout();
            this.consultasGrupo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(63, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(246, 175);
            this.nombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(244, 26);
            this.nombre.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(666, 571);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "Conectar👍";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // passLog
            // 
            this.passLog.Location = new System.Drawing.Point(198, 111);
            this.passLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passLog.Name = "passLog";
            this.passLog.PasswordChar = '*';
            this.passLog.Size = new System.Drawing.Size(244, 26);
            this.passLog.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(22, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 37);
            this.label6.TabIndex = 20;
            this.label6.Text = "Usuario";
            // 
            // userLog
            // 
            this.userLog.Location = new System.Drawing.Point(198, 29);
            this.userLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userLog.Name = "userLog";
            this.userLog.Size = new System.Drawing.Size(244, 26);
            this.userLog.TabIndex = 22;
            this.userLog.TextChanged += new System.EventHandler(this.userLog_TextChanged);
            this.userLog.MouseEnter += new System.EventHandler(this.userLog_MouseEnter);
            // 
            // LogIn
            // 
            this.LogIn.ForeColor = System.Drawing.Color.Black;
            this.LogIn.Location = new System.Drawing.Point(243, 155);
            this.LogIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(124, 51);
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
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 37);
            this.label1.TabIndex = 11;
            this.label1.Text = "Contraseña";
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(246, 111);
            this.usuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(244, 26);
            this.usuario.TabIndex = 21;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(51, 235);
            this.userName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(91, 26);
            this.userName.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(24, 209);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Usuario del jugador";
            // 
            // Consulta4
            // 
            this.Consulta4.AutoSize = true;
            this.Consulta4.ForeColor = System.Drawing.Color.White;
            this.Consulta4.Location = new System.Drawing.Point(194, 177);
            this.Consulta4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Consulta4.Name = "Consulta4";
            this.Consulta4.Size = new System.Drawing.Size(314, 24);
            this.Consulta4.TabIndex = 17;
            this.Consulta4.TabStop = true;
            this.Consulta4.Text = "Cuantas partidas ha ganado un jugador";
            this.Consulta4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(261, 235);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(124, 62);
            this.button5.TabIndex = 15;
            this.button5.Text = "Realizar consulta";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(51, 172);
            this.fecha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(91, 26);
            this.fecha.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(26, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Fecha(yyyy-mm-dd)";
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(210, 294);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 51);
            this.button4.TabIndex = 12;
            this.button4.Text = "Registrarse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // contrasena
            // 
            this.contrasena.Location = new System.Drawing.Point(246, 242);
            this.contrasena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.contrasena.Name = "contrasena";
            this.contrasena.PasswordChar = '*';
            this.contrasena.Size = new System.Drawing.Size(244, 26);
            this.contrasena.TabIndex = 10;
            // 
            // Consulta1
            // 
            this.Consulta1.AutoSize = true;
            this.Consulta1.ForeColor = System.Drawing.Color.White;
            this.Consulta1.Location = new System.Drawing.Point(194, 80);
            this.Consulta1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Consulta1.Name = "Consulta1";
            this.Consulta1.Size = new System.Drawing.Size(268, 24);
            this.Consulta1.TabIndex = 7;
            this.Consulta1.TabStop = true;
            this.Consulta1.Text = "Jugadores en determinada fecha";
            this.Consulta1.UseVisualStyleBackColor = true;
            // 
            // Consulta3
            // 
            this.Consulta3.AutoSize = true;
            this.Consulta3.ForeColor = System.Drawing.Color.White;
            this.Consulta3.Location = new System.Drawing.Point(194, 145);
            this.Consulta3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Consulta3.Name = "Consulta3";
            this.Consulta3.Size = new System.Drawing.Size(303, 24);
            this.Consulta3.TabIndex = 7;
            this.Consulta3.TabStop = true;
            this.Consulta3.Text = "Cuantos jugadores hay en una partida";
            this.Consulta3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(40, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Id de la partida";
            // 
            // idpartida
            // 
            this.idpartida.Location = new System.Drawing.Point(51, 109);
            this.idpartida.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.idpartida.Name = "idpartida";
            this.idpartida.Size = new System.Drawing.Size(91, 26);
            this.idpartida.TabIndex = 9;
            this.idpartida.TextChanged += new System.EventHandler(this.idpartida_TextChanged);
            // 
            // Consult2
            // 
            this.Consult2.AutoSize = true;
            this.Consult2.ForeColor = System.Drawing.Color.White;
            this.Consult2.Location = new System.Drawing.Point(194, 112);
            this.Consult2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Consult2.Name = "Consult2";
            this.Consult2.Size = new System.Drawing.Size(298, 24);
            this.Consult2.TabIndex = 8;
            this.Consult2.TabStop = true;
            this.Consult2.Text = "Jugador que ha ganado más partidas";
            this.Consult2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1107, 572);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(220, 46);
            this.button3.TabIndex = 10;
            this.button3.Text = "Desconectar👎";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupRegistro
            // 
            this.groupRegistro.BackColor = System.Drawing.Color.Transparent;
            this.groupRegistro.Controls.Add(this.usuario);
            this.groupRegistro.Controls.Add(this.label2);
            this.groupRegistro.Controls.Add(this.contrasena);
            this.groupRegistro.Controls.Add(this.label7);
            this.groupRegistro.Controls.Add(this.nombre);
            this.groupRegistro.Controls.Add(this.button4);
            this.groupRegistro.Controls.Add(this.label8);
            this.groupRegistro.Enabled = false;
            this.groupRegistro.ForeColor = System.Drawing.Color.White;
            this.groupRegistro.Location = new System.Drawing.Point(78, 86);
            this.groupRegistro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupRegistro.Name = "groupRegistro";
            this.groupRegistro.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupRegistro.Size = new System.Drawing.Size(519, 375);
            this.groupRegistro.TabIndex = 17;
            this.groupRegistro.TabStop = false;
            this.groupRegistro.Text = "Registro";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(63, 103);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 37);
            this.label7.TabIndex = 24;
            this.label7.Text = "Usuario";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(36, 232);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 37);
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
            this.listaJugadores.Location = new System.Drawing.Point(508, 58);
            this.listaJugadores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listaJugadores.Name = "listaJugadores";
            this.listaJugadores.ReadOnly = true;
            this.listaJugadores.RowHeadersVisible = false;
            this.listaJugadores.RowHeadersWidth = 62;
            this.listaJugadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.listaJugadores.Size = new System.Drawing.Size(276, 231);
            this.listaJugadores.TabIndex = 23;
            // 
            // testCarta1
            // 
            this.testCarta1.Location = new System.Drawing.Point(1521, 111);
            this.testCarta1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.testCarta1.Name = "testCarta1";
            this.testCarta1.Size = new System.Drawing.Size(309, 449);
            this.testCarta1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.testCarta1.TabIndex = 21;
            this.testCarta1.TabStop = false;
            this.testCarta1.Visible = false;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(1536, 569);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 35);
            this.button2.TabIndex = 22;
            this.button2.Text = "Cambia Carta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // idCarta
            // 
            this.idCarta.Location = new System.Drawing.Point(1666, 571);
            this.idCarta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.idCarta.Name = "idCarta";
            this.idCarta.Size = new System.Drawing.Size(148, 26);
            this.idCarta.TabIndex = 23;
            this.idCarta.Visible = false;
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
            this.groupLogin.Location = new System.Drawing.Point(1176, 143);
            this.groupLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupLogin.Name = "groupLogin";
            this.groupLogin.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupLogin.Size = new System.Drawing.Size(600, 238);
            this.groupLogin.TabIndex = 6;
            this.groupLogin.TabStop = false;
            this.groupLogin.Text = "LogIn";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(579, 5);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "Lista Jugadores";
            // 
            // consultasGrupo
            // 
            this.consultasGrupo.BackColor = System.Drawing.Color.Transparent;
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
            this.consultasGrupo.Location = new System.Drawing.Point(638, 111);
            this.consultasGrupo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consultasGrupo.Name = "consultasGrupo";
            this.consultasGrupo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consultasGrupo.Size = new System.Drawing.Size(806, 391);
            this.consultasGrupo.TabIndex = 20;
            this.consultasGrupo.TabStop = false;
            this.consultasGrupo.Text = "consultasGrupo";
            this.consultasGrupo.Visible = false;
            // 
            // InvitarJugador
            // 
            this.InvitarJugador.ForeColor = System.Drawing.Color.Black;
            this.InvitarJugador.Location = new System.Drawing.Point(596, 312);
            this.InvitarJugador.Name = "InvitarJugador";
            this.InvitarJugador.Size = new System.Drawing.Size(116, 50);
            this.InvitarJugador.TabIndex = 24;
            this.InvitarJugador.Text = "Invitar";
            this.InvitarJugador.UseVisualStyleBackColor = true;
            this.InvitarJugador.Click += new System.EventHandler(this.InvitarJugador_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1910, 665);
            this.Controls.Add(this.idCarta);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.testCarta1);
            this.Controls.Add(this.consultasGrupo);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupLogin);
            this.Controls.Add(this.groupRegistro);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Explodding Kittens";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupRegistro.ResumeLayout(false);
            this.groupRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaJugadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testCarta1)).EndInit();
            this.groupLogin.ResumeLayout(false);
            this.groupLogin.PerformLayout();
            this.consultasGrupo.ResumeLayout(false);
            this.consultasGrupo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton Consulta1;
        private System.Windows.Forms.RadioButton Consult2;
        private System.Windows.Forms.RadioButton Consulta3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox idpartida;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox fecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
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
        private System.Windows.Forms.PictureBox testCarta1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox idCarta;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView listaJugadores;
        private System.Windows.Forms.GroupBox groupLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox consultasGrupo;
        private System.Windows.Forms.Button InvitarJugador;
    }
}

