--Procedimiento mostrar
create proc spmostrar_categoria
as
select top 200 * from categoria 
order by idcategoria desc
go