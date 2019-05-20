CREATE DATABASE ElectivaIV
GO
USE ElectivaIV
CREATE TABLE tbusuarios(
Login varchar(20),
Password varchar(20)
)
CREATE TABLE tbingreso(
Placa varchar(6),
Fecha datetime
)
CREATE TABLE tbsalida(
Placa varchar(6),
Fecha datetime
)