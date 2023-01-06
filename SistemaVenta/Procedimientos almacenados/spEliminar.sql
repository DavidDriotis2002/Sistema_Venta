--Procedimiento eliminar
create proc speliminar_categoria
@idcategoria int
as
delete from Categoria
where idCategoria = @idcategoria
go