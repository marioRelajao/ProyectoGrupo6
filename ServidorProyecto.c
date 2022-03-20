
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>



int main(int argc, char *argv[])
{
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexiï¿³n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexin
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Campeonato",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	char consulta[300];
	
	
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
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
	serv_adr.sin_port = htons(9060);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
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
				terminar=1;
			else if (codigo ==5) //registrarse
			{
				p = strtok( NULL, "/");
				strcpy(nombre,p);
				p = strtok( NULL, "/");
				strcpy(usuario,p);
				p =strtok( NULL, "/");
			    strcpy(contrasena,p);
				sprintf(consulta,"INSERT INTO Jugadores (Nombre,Usuario,Contrasena) VALUES('%s', '%s', SHA1('%s'))", nombre,usuario,contrasena);
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al introducir los datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					sprintf (respuesta, "5/MAL");
					exit (1);
				}
				else
				{
					printf("Insertado correctamente");
			    	sprintf (respuesta, "5/BIEN");
				}
			}
			//Me llega 1/2020-11-12 Mario
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
					sprintf (respuesta,"NOK");
				}
				
				else
				{

					sprintf(respuesta,"%s",row[0]);
					row = mysql_fetch_row(resultado);
					while (row != NULL) 
					{
						//Respuesta = Maria/Juan/
						sprintf(respuesta,"%s/%s",respuesta,row[0]);
						row = mysql_fetch_row(resultado);
					}
					//Guardo contador/nombre1/nombre2
					
				}
					
			}	//Nos llega 4/usuario Codigo Ana
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
					sprintf (respuesta,"NOK");
				}
				else//Guardamos en respuesta el nº de partidas que gana el usuario y lo enviamos
					sprintf (respuesta,"%s",row[0]);
			}
			//Codigo Ines
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
					sprintf (respuesta,"NOK");
				}
				else
					sprintf (respuesta,"%s",row[0]);
			}
			else if (codigo ==3)
			{
				printf("PINGO");
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
					sprintf (respuesta,"NOK");
				}
				else //Enviamos cuantos jugadores hay en esa partida
					sprintf (respuesta,"%s",row[0]);
			}
			else if (codigo==6)
			{
				
				p = strtok( NULL, "/");
				strcpy(usuario,p);
				p = strtok( NULL, "/");
				strcpy(contrasena,p);
				sprintf (consulta , "SELECT COUNT(*) FROM Jugadores WHERE Usuario='%s' AND Contrasena= SHA1('%s')",usuario, contrasena);
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al introducir los datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					exit (1);
				}
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				int num =atoi(row[0]);
				if (num==1)
					sprintf(respuesta, "6/SI");
				else 
					sprintf(respuesta, "6/NOK");
						
			}
			
			
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
}

