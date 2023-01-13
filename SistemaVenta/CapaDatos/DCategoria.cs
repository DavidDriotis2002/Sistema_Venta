using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DCategoria
    {
        private int _Idcategoria;
        private string _Nombre;
        private string _Descripcion;
        private string _TextoBuscar;

        public int Idcategoria { get => _Idcategoria; set => _Idcategoria = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        //constructor vacio
        public DCategoria()
        {

        }

        //constructor con parametros
        public DCategoria(int idcategoria, string nombre, string descripcion, string textobuscar)
        {
            this.Idcategoria = idcategoria;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.TextoBuscar = textobuscar;
        }

        public string Insertar(DCategoria Categoria)
        {
            // Definicion de variables e instancias del metodo Insertar
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = Conexion.Cn;
                // Creamos el comando insert a la base de datos
                SqlCommand cmd = new SqlCommand("insert into Categoria(nombre, descripcion) values(@nombre,@descripcion)",sqlCon);
                
                // Definiendo parametros para el comando insert
                cmd.Parameters.Add(new SqlParameter("nombre", Categoria.Nombre));
                cmd.Parameters.Add(new SqlParameter("descripcion", Categoria.Descripcion));

                // Abriendo conexion a la base de datos
                sqlCon.Open();

                // Ejecutamos nuestro comando insert a la base de datos
                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                // Cerramos conexion a la base de datos
                if (sqlCon.State == ConnectionState.Open) 
                    sqlCon.Close();
            }
            return rpta;
        }

        public string Editar (DCategoria Categoria)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon.ConnectionString = Conexion.Cn;
                //Creamos el comando update a la base de datos
                SqlCommand cmd = new SqlCommand("update Categoria set nombre=@nombre, descripcion=@descripcion where idCategoria = @idCategoria", sqlCon);
                
                cmd.Parameters.Add(new SqlParameter("nombre",Categoria.Nombre));
                cmd.Parameters.Add(new SqlParameter("descripcion",Categoria.Descripcion));
                cmd.Parameters.Add(new SqlParameter("idCategoria",Categoria.Idcategoria));

                // Abriendo la conexion a la base de datos
                sqlCon.Open();


                //Ejecutamos nuestro comando update a la base de datos

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) 
                    sqlCon.Close();
            }
            return rpta;
        }

        public string Eliminar(DCategoria Categoria)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                //Creamos el comando delete a la base de datos
                SqlCommand cmd = new SqlCommand("delete from Categoria where idCategoria=@idCategoria", sqlCon);

                cmd.Parameters.Add(new SqlParameter("idCategoria", Categoria.Idcategoria));

                //Abriendo la conexion a la base de datos
                sqlCon.Open();

                //Ejecutamos nuestro comando delete a nuestra base de datos

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ELIMINO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;
        }
             
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("Categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //Creamos el comando select a la base de datos
                sqlCon.ConnectionString = Conexion.Cn;
                SqlCommand cmd = new SqlCommand("select * from Categoria", sqlCon);

                //Abrimos conexion a base de datos
                sqlCon.Open();

                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message);
                DtResultado = null;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) 
                    sqlCon.Close();
            }

            return DtResultado;
        }

        public DataTable BuscarNombre(DCategoria Categoria)
        {
            DataTable DtResultado = new DataTable("Categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexion.Cn;
                
                //Creamos el comando select a la base de datos
              
                SqlCommand cmd = new SqlCommand("select * from Categoria where nombre LIKE '%' + @nombre + '%'", sqlCon);

                cmd.Parameters.Add(new SqlParameter("nombre", Categoria.TextoBuscar));

                //Abrimos conexion a base de datos
                sqlCon.Open();
                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message);
                DtResultado = null;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
            return DtResultado;
        }
    }

   
}
