using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NCategoria
    {
        //metodo insertar que llame al metodo insertar que se encuentra en la clase DCategoria de la CapaDatos
        
        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria obj = new DCategoria();
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Insertar(obj);
        }

        //metodo editar que llame al metodo editar que se encuentra en la clase DCategoria de la CapaDatos

        public static string Editar(int idcategoria, string nombre, string descripcion)
        {
            DCategoria obj = new DCategoria();
            obj.Idcategoria = idcategoria;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;

            return obj.Editar(obj);
        }

        //metodo eliminar que llame al metodo eliminar que se encuentra en la clase DCategoria de la CapaDatos

        public static string Eliminar(int idcategoria)
        {
            DCategoria obj = new DCategoria();
            obj.Idcategoria = idcategoria;

            return obj.Eliminar(obj);
        }

        //metodo mostrar que llame al metodo mostrar que se encuentra en la clase DCategoria de la CapaDatos

        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }
    }
}
