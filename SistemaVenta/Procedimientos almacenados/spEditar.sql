--Procedimiento editar
create proc speditar_categoria 
@idcategoria int,
@nombre varchar(50),
@descripcion varchar (256)
as
update Categoria set nombre = @nombre,
descripcion = @descripcion
where idCategoria = @idcategoria
go