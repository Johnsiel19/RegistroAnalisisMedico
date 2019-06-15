create database ProyectoFinalAplicadaI
go
use ProyectoFinalAplicadaI
go
create table Usuarios (
UsuarioId int primary key identity,
Nombres varchar(50),
Email varchar(50),
NivelUsuario int,
Usuario varchar(30),
Clave varchar(30),
FechaIngreso datetime
)

create table Cargos(
CargoId int primary key identity,
Descripcion varchar(50)
)

