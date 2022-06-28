
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>


//Aqui definimos los diferentes constructores que vamos a usar
typedef struct{
	char nombre [20];
	int socket;
}Conectado;

typedef struct {
	Conectado conectados [100];
	int num;
} ListaConectados;

typedef struct {
	Conectado jugadores [5];
	int id;
	int oc;//0 si est� libre, 1 si no
	int numJug;
}Partida;

typedef Partida TablaPartidas[100];

//Variables publicas que vamos a usar
ListaConectados miLista;
TablaPartidas miTabla;
MYSQL *conn;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
//Contador de jugadores que tenemos actualmente?
int i=0;
//Definimos el vector de sockets
int sockets[100];
char notificacion[500];

//Funciones del proyecto
void LimpiaPartida (int id)
{
	int c = 0;
	miTabla[id].numJug=0;
	while (c < 5)
	{
		strcpy(miTabla[id].jugadores[c].nombre, ""); //Limpiamos el valor de la partida que al final no se juega
		miTabla[id].jugadores[c].socket = -1; //por si acaso hay algun cliente fantasma, enviamos a nadie
		c++;
	}
}

int Pon (char nombre [20], int socket){
	//A�ade nuevo conectados. Retorna 0 si ok y -1 si la lista ya estaba llena y no lo ha podido a�adir.
	
	if(miLista.num == 100)
	{
		return -1;
	}

	else {
		strcpy (miLista.conectados[miLista.num].nombre, nombre);
		miLista.conectados[miLista.num].socket = socket;
		miLista.num++;
		return 0;
	}
	
}

int DamePosicion (char nombre[20]) {
	//Devuelve la posicion o -1 si no est� en la lista
	int i = 0;
	int encontrado = 0;
	while ((i < miLista.num) && !encontrado)
	{
		if (strcmp (miLista.conectados[i].nombre, nombre) == 0 )
			encontrado = 1;
		if (!encontrado)
			i++;
	}
	if (encontrado)
		return  i;
	else
		return -1;
}

int Elimina (char nombre[20])
	//Retorna 0 si elimina y -1 si ese usuario no est� en la lista	
{
	int pos = DamePosicion(nombre);
	if (pos == -1)
	{
		return -1;
	}
	else 
	{
		int i;
		for (i = pos; i < miLista.num-1; i++)
		{
			miLista.conectados[i] = miLista.conectados[i+1];
		}
		miLista.num--;
		return 0;
	}
	
}

void DameConectados (char conectados[512])
{
	//Pone en conectados los nombres de todos los conectados separados por /. Primero pone el n�mero de connectados. Ejemplo: "3/Juan/Maria/Pedro"
	sprintf(conectados, "7");
    int i;
	for(i = 0; i< miLista.num; i++)
		sprintf(conectados, "%s/%s", conectados, miLista.conectados[i].nombre);
}
void InicializarTabla ()
{
	int i;
	for (i=0; i<100; i++)
	{	
		miTabla[i].oc=0;
		miTabla[i].id=i;
	}	
}

int CreaPartida(char listausuarios[200], int socket)//devuelve -1 si est�n ocupadas todas, el id de partida si no
{
	int g=0;
	int encontrado=0; //0 no encuentra partida libre, 1 encuentra partida libre
	while(g<100 && encontrado==0)
	{
		if(miTabla[g].oc==0)
		{
			encontrado=1;
			miTabla[g].oc=1;
		}
		else
		   g++;
		
	}
	if (encontrado==0)
	{
		return -1;
	}
	else
	{
		int j = 0;
		encontrado=0;
		while(j<miLista.num && encontrado==0)
		{
			if (miLista.conectados[j].socket==socket)
			{
				encontrado=1;
			}
			else j++;
		}
		//int id=g+1;
		strcpy(miTabla[g].jugadores[0].nombre,miLista.conectados[j].nombre);
		miTabla[g].jugadores[0].socket=socket;
		int numero=1;
		printf("socket jugador que invita: %d\n", miTabla[g].jugadores[0].socket);
		char *p = strtok(listausuarios, "/");
		int cont=1;
		while(p!=NULL)
		{
			numero++;
			strcpy(miTabla[g].jugadores[cont].nombre,p);
			encontrado=0;
			j=0;
			while(j<miLista.num && encontrado==0)
			{
				if (strcmp(miLista.conectados[j].nombre,p)==0)
				{
					encontrado=1;
				}
				else j++;
			}
			miTabla[g].jugadores[cont].socket=miLista.conectados[j].socket;
			cont++;
			p = strtok( NULL, "/");
		}
		miTabla[g].numJug=numero;
		//printf("socket jugador invitado: %d\n", miTabla[g].jugadores[1].socket);
		return miTabla[g].id;
	}
}


void *AtenderCliente (void *socket)
{
	printf("Voy a atender al thread\n");
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	//Hasta aqui hemos creado el socket
	printf("Socket creado\n");
	printf("");
	//Iniciamos todo lo necesario para la BBDD
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi￳n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexin
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "M6_BBDDJuego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//int socket_conn = * (int *) socket;
	printf("Connexion con la BBDD\n");
	char peticion[512];
	char respuesta[512];
	int ret;
	char consulta[300];
	int err;

	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el c?digo de la petici?n
		char contrasena[20];
		char nombre[20];
		char usuario[20];
		if (codigo ==0) //petici?n de desconexi?n
		{
			pthread_mutex_lock(&mutex);
			int num= Elimina(usuario);
			pthread_mutex_unlock(&mutex);
			if (num==0)
			{
				printf("Usuario eliminado correctamente");
				DameConectados(notificacion); //llenamos la lista con la lista nueva sin el usuario que se acaba de desconectar
				for (int j = 0; j<miLista.num; j++)
				{
					if (miLista.conectados[j].socket!=sock_conn)
					{
						write (miLista.conectados[j].socket, notificacion, strlen(notificacion));
					}							
				}
			}
			else 
				printf("El usuario no habia iniciado sesion");
			terminar=1;
			//printf ("Usuario desc, envio: %s\n", respuesta);
		}
		else if (codigo ==5) //registrarse
		{
			
			p = strtok( NULL, "/");
			strcpy(nombre,p);
			p = strtok( NULL, "/");
			strcpy(usuario,p);
			p =strtok( NULL, "/");
			strcpy(contrasena,p);
			//Bloqueamos el thread al insertar en la BBDD
			pthread_mutex_lock(&mutex);
			sprintf(consulta,"INSERT INTO Jugadores (Nombre,Usuario,Contrasena) VALUES('%s', '%s', SHA1('%s'))", nombre,usuario,contrasena);
			err=mysql_query (conn, consulta);
			pthread_mutex_unlock(&mutex);
			//Desbloqueamos el thread
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				sprintf (respuesta, "5/MAL");
				exit (1);
			}
			else
			{
				printf("Insertado correctamente\n");
				sprintf (respuesta, "5/BIEN");
			}
			
		}
		//Me llega 1/2020-11-12 Mario
		//El usuario quiere saber cuantos jugadores han jugado en una determinada fecha
		else if (codigo ==1)
		{
			p = strtok(NULL,"/");
			char fecha[20];
			strcpy(fecha,p);
			sprintf(consulta,"SELECT j.Nombre FROM Jugadores j INNER JOIN Participacion r ON r.IdJ = j.Id INNER JOIN Partidas p ON (p.Id = r.IdP AND p.FechaInicio LIKE '%s%')", fecha);
			printf("%s",consulta);
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			//row[0] es el nombre, hay que iterar hasta encontrar NULL
			if (row == NULL)
			{
				printf("No se han obtenido resultados");
				sprintf (respuesta,"1/NOK");
			}
			
			else
			{
				sprintf(respuesta,"1/%s",row[0]);
				row = mysql_fetch_row(resultado);
				while (row != NULL) 
				{
					//Respuesta = 1/Maria/Juan/
					sprintf(respuesta,"%s/%s",respuesta,row[0]);
					row = mysql_fetch_row(resultado);
				}
				//Guardo contador/nombre1/nombre2
				
			}
			
		}	
		//Nos llega 4/usuario Codigo Ana
		//El usuario nos pide cuantas partidas ha ganado un usuario en concreto
		else if (codigo ==4)
		{
			p = strtok(NULL,"/");
			strcpy(usuario,p); 
			sprintf(consulta,"SELECT COUNT(*) partidas_ganadas FROM Jugadores j, Participacion p WHERE j.Usuario='%s' AND j.Id = p.IdJ AND p.Posicion = 1 GROUP BY j.Id ORDER BY partidas_ganadas DESC", usuario);
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				//O bien el jugador no ha ganado ninguna o bien no existe el jugador
				printf("No se han obtenido resultados");
				sprintf (respuesta,"4/NOK");
			}
			else//Guardamos en respuesta el n� de partidas que gana el usuario y lo enviamos 4/2
				sprintf (respuesta,"4/%s",row[0]);
		}
		//Codigo Ines: El usuario pide que jugador ha ganado m�s partidas
		else if (codigo ==2)
		{
			strcpy(consulta,"SELECT Jugadores.Usuario, COUNT(*) partidas_ganadas FROM Jugadores, Participacion WHERE Jugadores.Id = Participacion.IdJ AND Participacion.Posicion = 1 GROUP BY Jugadores.Id ORDER BY partidas_ganadas DESC LIMIT 1");
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf("No se han obtenido resultados");
				sprintf (respuesta,"2/NOK");
			}
			else
				sprintf (respuesta,"2/%s",row[0]); //Enviamos 2/Usuario
		}
		//EL usuario quiere saber cuantos jugadores hay en una partida
		else if (codigo ==3)
		{
			p = strtok(NULL, "/");
			char id[20];
			strcpy(id,p);
			sprintf(consulta,"SELECT COUNT(p.IdP) FROM Participacion p WHERE p.IdP = %s", id);
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (strcmp(row[0],"0")==0)
			{
				printf("No se ha jugado esa partida");
				sprintf (respuesta,"3/NOK");
			}
			else //Enviamos cuantos jugadores hay en esa partida
				sprintf (respuesta,"3/%s",row[0]);
		}
		else if (codigo==6) //Login, nos llega 6/Mario/mario123
		{
			
			p = strtok( NULL, "/");
			strcpy(usuario,p);
			p = strtok( NULL, "/");
			strcpy(contrasena,p);
			sprintf (consulta , "SELECT COUNT(*) FROM Jugadores WHERE Usuario='%s' AND Contrasena= SHA1('%s')",usuario, contrasena);
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al buscar los datos en la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			int num =atoi(row[0]);
			if (num==1)
			{	
				pthread_mutex_lock(&mutex);
				int res =Pon (usuario, sock_conn);
				pthread_mutex_unlock(&mutex);
				if (res == -1)
					printf("Lista llena. No se puede a�adir.\n");
				else
				{
					sprintf(respuesta, "6/SI");
					printf ("A�adir  bien.\n");
					DameConectados(notificacion);
					//printf(notificacion);
					for (int j = 0; j<miLista.num; j++)
					{
						if (miLista.conectados[j].socket!=sock_conn)
						{
							write (miLista.conectados[j].socket, notificacion, strlen(notificacion));
						}
					}
					printf("%d\n",sock_conn);
					
					sprintf(respuesta, "6/SI/%s", notificacion);
				}
				
			}
			
			else 
				sprintf(respuesta, "6/NOK");
			
			
		}
		else if (codigo == 8)
		{
			char juginvitados[200];
			p = strtok( NULL, "/");
			sprintf(juginvitados, "%s", p);
			p = strtok( NULL, "/");
			while(p!=NULL)
			{
				sprintf(juginvitados, "%s/%s", juginvitados,p);
				p = strtok( NULL, "/");	
			}
			pthread_mutex_lock(&mutex);
			int id = CreaPartida(juginvitados,sock_conn);
			pthread_mutex_unlock(&mutex);
			//ahora enviamos al usuario al que ha invitado el codigo 8/usuarioqueinvita/idpartida
			sprintf(respuesta,"8/%s/%d",miTabla[id].jugadores[0].nombre, id);
			printf("socket jugadores invitados en el codigo 8 %d\n", miTabla[id].jugadores[1].socket);
			int cont=1;

			//Mi tabla = tabla con los jugadores conectados
			//Buscamos el nombre de la persona en la tabla y le escribimos en el socket que tiene guardado
			while(strcmp(miTabla[id].jugadores[cont].nombre,"")!=0)
			{
				//printf ("Posicion %d de la tabla: %s\n", cont, miTabla[id].jugadores[cont].nombre);
				write (miTabla[id].jugadores[cont].socket,respuesta, strlen(respuesta));
				cont++;
			}
			
		}
		//Codigo que recibe la respuesta de los clientes 9/SI/idPartida
		else if (codigo == 9)
		{
			p = strtok( NULL, "/");
			char SIONO[2];
			strcpy(SIONO,p);
			p = strtok( NULL, "/");
			int id = atoi(p);
			int cont=0;
			if(strcmp(SIONO,"SI")==0)
			{
				sprintf(respuesta, "9/SI/%d", id);
				//Mi tabla = tabla con los jugadores conectados
				//Buscamos el nombre de la persona en la tabla y le escribimos en el socket que tiene guardado
				write (miTabla[id].jugadores[0].socket,respuesta, strlen(respuesta)); //Solo enviamos a la persona queinvita y gestiona la creacion de la partida
			}
			else
			{
				miTabla[id].oc=0;
				sprintf(respuesta, "9/NO/%d", id);
				
				//Mi tabla = tabla con los jugadores conectados
				//Buscamos el nombre de la persona en la tabla y le escribimos en el socket que tiene guardado
				write (miTabla[id].jugadores[0].socket,respuesta, strlen(respuesta));
			}
		}
		
		else if (codigo == 10) //Recibimos si al final jugamos o klk 10/SI/idPartida
		{
			p = strtok( NULL, "/");
			char SIONO[2];
			strcpy(SIONO,p);
			p = strtok( NULL, "/");
			int id = atoi(p);
			int cont=0;
			if(strcmp(SIONO,"SI")==0)
			{
				
				sprintf(respuesta, "10/SI/%d/%d", id,miTabla[id].numJug);
				cont=0;
				//Mi tabla = tabla con los jugadores conectados
				//Buscamos el nombre de la persona en la tabla y le escribimos en el socket que tiene guardado
				while(strcmp(miTabla[id].jugadores[cont].nombre,"")!=0)
				{
					write (miTabla[id].jugadores[cont].socket,respuesta, strlen(respuesta)); //Enviamos a todos que al final si jugamos la partida
					cont++;
				}
			}
			else
			{
				miTabla[id].oc=0;
				sprintf(respuesta, "10/NO/%d", id);
				
				//Mi tabla = tabla con los jugadores conectados
				//Buscamos el nombre de la persona en la tabla y le escribimos en el socket que tiene guardado
				while(strcmp(miTabla[id].jugadores[cont].nombre,"")!=0)
				{
					write (miTabla[id].jugadores[cont].socket,respuesta, strlen(respuesta));
					cont++;
				}
				LimpiaPartida(id);
			}
		}
		
		else if (codigo == 11) //Codigo para los mensajes del chat de panas 11/Mensaje/Usuario/idPartida
		{
			p = strtok( NULL, "/");
			char mensaje[500];
			strcpy(mensaje,p);
			char usuario[20];
			p = strtok( NULL, "/");
			strcpy(usuario,p);
			int id = atoi(p);
			int g = 0;
			sprintf (respuesta, "11/%s/%s/%d", mensaje, usuario, id);
			while(strcmp(miTabla[id].jugadores[g].nombre,"")!=0) //Enviamos a todos los panas los mensajes
			{
				write (miTabla[id].jugadores[g].socket,respuesta, strlen(respuesta));
				g++;
			}
		}
		else if(codigo==12) //recibimos un 12/idPartida/8,9,12.../45,55,35.../.
		//los numeros separados por comas son las ids de las cartas que forman parte de una baraja de un jugador, el servidor deber� encargarse
		//de enviarle las barajas a todos menos al primer jugador de la lista en la partida
		{
			p = strtok( NULL, "/");
			int id = atoi(p);
			int k=1;
			while(k<miTabla[id].numJug)
			{
				p = strtok( NULL, "/");
				sprintf (respuesta, "12/%s", p);
				write (miTabla[id].jugadores[k].socket,respuesta, strlen(respuesta));
				k++;
			}
			sprintf (respuesta, "13/");
			write (miTabla[id].jugadores[0].socket,respuesta, strlen(respuesta)); //indicamos al primer jugador que es su turno de realizar el primer movimiento
			
		}
		else if(codigo==14) //Recibimos 14/usuarioElminar (nosotros mismos)
		{
			p = strtok(NULL,"/");
			char usarioE[20];
			strcpy(usuarioE,p);
			sprintf(consulta,"DELETE FROM Jugadores WHERE Usuario ='%s'",usuarioE);
			pthread_mutex_lock(&mutex);
			err=mysql_query (conn, consulta);
			pthread_mutex_unlock(&mutex);
			//Desbloqueamos el thread
			if (err!=0) {
				printf ("Error al introducir los datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				sprintf (respuesta, "14/MAL");
				exit (1);
			}
			else
			{
				printf("Eliminado el usuario %s de la BBDD\n",usuarioE);
				sprintf (respuesta, "14/BIEN");
				printf("%s\n",respuesta);
			}
		}
		if (codigo !=0 && codigo != 8 && codigo != 9 && codigo != 10 && codigo != 11 && codigo !=12)
		{
			
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}


int main(int argc, char *argv[])
{
	miLista.num = 0;
	InicializarTabla();
	// Estructura especial para almacenar resultados de consultas 
	
	
	//Creamos una conexion al servidor MYSQL 
	int sock_conn, sock_listen, puerto;
	puerto = 50018;
	struct sockaddr_in serv_adr;
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(puerto);//para shiva poner variable puerto
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread;

	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] =sock_conn;
		//Creamos thread y decimos que tiene que hacer
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;	
		
	}
	
}

