-- **********************************************************************
-- consultas sin procedimientos 

select * from Categoria

insert into Categoria(nombre, descripcion)
values('Agroservicio','Articulos de agroservicio')

update Categoria 
set nombre='Lavanderia', descripcion = 'Articulos de lavanderia'
where idCategoria = 4

delete from Categoria
where idCategoria = 3

select * from Categoria
where idCategoria = 5

select * from Categoria
where nombre = 'ropa'

select * from Categoria
where nombre LIKE '%a%'