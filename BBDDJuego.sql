DROP DATABASE IF EXISTS Campeonato;
CREATE DATABASE Campeonato;
USE Campeonato;
CREATE TABLE Jugadores (
  Id INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(50) NOT NULL,
  Usuario VARCHAR(50) NOT NULL,
  Contrasena VARCHAR(40) NOT NULL
 
)ENGINE = InnoDB;

CREATE TABLE Partidas (
 Id INTEGER PRIMARY KEY NOT NULL,
 FechaInicio DATETIME NOT NULL,
 FechaFinal DATETIME NOT NULL,
 Duracion TIME GENERATED ALWAYS AS (TIMEDIFF(FechaFinal,FechaInicio))
)ENGINE = InnoDB;

CREATE TABLE Participacion (
 IdP INTEGER NOT NULL,
 IdJ INTEGER NOT NULL,
 Posicion INTEGER NOT NULL,
 FOREIGN KEY (IdP) REFERENCES Partidas(Id),
 FOREIGN KEY (IdJ) REFERENCES Jugadores(Id)
)ENGINE = InnoDB;


INSERT INTO Jugadores VALUES(1,'Juan', 'Juan01', SHA1('Juanito'));
INSERT INTO Jugadores VALUES(2,'Maria', 'Maria01', SHA1('Maria123'));
INSERT INTO Jugadores VALUES(3,'Pedro', 'Pedro01', SHA1('Pedro123'));
INSERT INTO Jugadores VALUES(4,'Luis', 'Luis01', SHA1('Luismi'));
INSERT INTO Jugadores VALUES(5,'Julia', 'Julia01', SHA1('Julia123'));


INSERT INTO Partidas(Id, FechaInicio, FechaFinal) VALUES(1,'2022-01-14 20:20:00', '2022-01-14 20:25:00');
INSERT INTO Partidas(Id, FechaInicio, FechaFinal) VALUES(2,'2021-12-12 20:20:00', '2021-12-12 20:30:00');
INSERT INTO Partidas(Id, FechaInicio, FechaFinal) VALUES(3,'2021-12-06 20:20:00', '2021-12-07 23:20:00');


INSERT INTO Participacion VALUES(1,3,4);
INSERT INTO Participacion VALUES(1,4,3);
INSERT INTO Participacion VALUES(1,2,2);
INSERT INTO Participacion VALUES(1,1,1);
INSERT INTO Participacion VALUES(2,4,2);
INSERT INTO Participacion VALUES(2,3,1);

