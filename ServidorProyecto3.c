
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

//Variables publicas que vamos a usar
ListaConectados miLista;
MYSQL *conn;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
//Contador de jugadores que tenemos actualmente?
int i=0;
//Definimos el vector de sockets
int sockets[100];
char notificacion[500];

//Funciones del proyecto
int Pon (char nombre [20], int socket){
	//Añade nuevo conectados. Retorna 0 si ok y -1 si la lista ya estaba llena y no lo ha podido añadir.
	pthread_mutex_lock(&mutex);
	if(miLista.num == 100)
	{
		pthread_mutex_unlock(&mutex);
		return -1;
	}

	else {
		strcpy (miLista.conectados[miLista.num].nombre, nombre);
		miLista.conectados[miLista.num].socket = socket;
		miLista.num++;
		pthread_mutex_unlock(&mutex);
		return 0;
	}
	
}

int DamePosicion (char nombre[20]) {
	//Devuelve la posicion o -1 si no está en la lista
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
	//Retorna 0 si elimina y -1 si ese usuario no está en la lista	
{
	pthread_mutex_lock(&mutex);
	int pos = DamePosicion(nombre);
	if (pos == -1)
	{
		pthread_mutex_unlock(&mutex);
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
		pthread_mutex_unlock(&mutex);
		return 0;
	}
	
}

void DameConectados (char conectados[512])
{
	//Pone en conectados los nombres de todos los conectados separados por /. Primero pone el número de connectados. Ejemplo: "3/Juan/Maria/Pedro"
	sprintf(conectados, "7");
    int i;
	for(i = 0; i< miLista.num; i++)
		sprintf(conectados, "%s/%s", conectados, miLista.conectados[i].nombre);
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
		printf ("Error al crear la conexiï¿³n: %u %s\n", 
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
			int num= Elimina(usuario);
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
			else//Guardamos en respuesta el nº de partidas que gana el usuario y lo enviamos 4/2
				sprintf (respuesta,"4/%s",row[0]);
		}
		//Codigo Ines: El usuario pide que jugador ha ganado más partidas
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
				int res =Pon (usuario, sock_conn);
				if (res == -1)
					printf("Lista llena. No se puede añadir.\n");
				else
				{
					sprintf(respuesta, "6/SI");
					printf ("Añadir  bien.\n");
					DameConectados(notificacion);
					//printf(notificacion);
					for (int j = 0; j<miLista.num; j++)
					{
						if (miLista.conectados[j].socket!=sock_conn)
						{
							write (miLista.conectados[j].socket, notificacion, strlen(notificacion));
						}							
					}
					sprintf(respuesta, "6/SI/%s", notificacion);
				}
				
			}
			
			else 
				sprintf(respuesta, "6/NOK");
			
			
		}
/*		else if (codigo==7) *///El usuario nos pide que le demos una lista de los jugadores conectados
/*		{*/
/*		    DameConectados(respuesta);*/
			//printf(respuesta);
/*		}*/
		
		if (codigo !=0)
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
	serv_adr.sin_port = htons(puerto);
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

