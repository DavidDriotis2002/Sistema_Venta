--Procedimiento buscar nombre 
create proc spbuscar_categoria 
@textobuscar varchar(50)
as
select * from Categoria
where nombre like @textobuscar + '%' --alt + 39
go