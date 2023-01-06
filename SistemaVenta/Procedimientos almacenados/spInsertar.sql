--Procedimiento insertar
create proc spinsertar_categoria
@idcategoria int output,
@nombre varchar(50),
@descripcion varchar(256)
as
insert into Categoria (nombre, descripcion)
values (@nombre, @descripcion)
go