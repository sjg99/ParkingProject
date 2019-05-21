CREATE DATABASE ElectivaIVFinal
GO
USE ElectivaIVFinal
CREATE TABLE tbusuarios(
Login varchar(20),
Password varchar(20),
Nombre varchar(50),
Apellido varchar(50),
Direccion varchar(50),
Telefono varchar (10),
Email varchar(50)
)
CREATE TABLE tbingreso(
Placa varchar(6),
Fecha datetime
)
CREATE TABLE tbsalida(
Placa varchar(6),
Fecha datetime,
Precio varchar(10)
)
