using LibraryEN_CAD.ConversionDivisa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LibraryEN_CAD.Transaccion
{

    /// <summary>
    /// Esta clase representa
    /// el CAD de Transaccion
    /// </summary>
    public class CADTransaccion
    {
        /// <summary>
        /// Propiedad que representa
        /// la conexión con la base de datos
        /// </summary>
        private string conexion;


        /// <summary>
        /// Constructor con defecto
        /// </summary>
        public CADTransaccion()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Este método crea una transacción
        /// en la base de datos
        /// </summary>
        /// <param name="t">
        /// Transacción a crear
        /// </param>
        /// <returns>
        /// Verdadero si se crea, falso si no
        /// </returns>
        public bool Create(ENTransaccion t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO dbo.transaccion (cantidad, fecha) VALUES (@cantidad, @fecha); SELECT SCOPE_IDENTITY();", con);
                command.Parameters.AddWithValue("@cantidad", t.Cantidad);
                command.Parameters.AddWithValue("@fecha", t.Fecha);
                object result = command.ExecuteScalar();
                t.Id = Convert.ToInt32(result);
                correcto = true;
            }
            catch (Exception ex) {
                
                Console.WriteLine(ex.Message); 
            }
            finally { 
                con.Close();
            }
            return correcto;
        }


        /// <summary>
        /// Este método lee una transacción de
        /// la base de datos
        /// </summary>
        /// <param name="t">
        /// Transacción a leer
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool Read(ENTransaccion t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from dbo.transaccion where id=@Id", con);
                command.Parameters.AddWithValue("@id", t.Id);
                SqlDataReader reader = command.ExecuteReader();
                correcto = true;
                if (reader.Read())
                {
                    t.Cantidad = decimal.Parse(reader["cantidad"].ToString());

                    if (reader["fecha"] != DBNull.Value)
                    {
                        t.Fecha = DateTime.Parse(reader["fecha"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }

        /// <summary>
        /// Este método actualiza una
        /// transacción en la base de datos
        /// </summary>
        /// <param name="t">
        /// Transacción a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si actualiza, falso si no
        /// </returns>
        public bool Update(ENTransaccion t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Update dbo.transaccion set cantidad=@cantidad,fecha=@fecha where id=@id;", con);

                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@cantidad", t.Cantidad);
                command.Parameters.AddWithValue("@fecha", t.Fecha);
                command.ExecuteNonQuery();
                correcto = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }

        /// <summary>
        /// Este método borra una
        /// transacción de la base de datos
        /// </summary>
        /// <param name="t">
        /// Transacción a borrar
        /// </param>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public bool Delete(ENTransaccion t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Delete from transaccion where id=@id", con);

                command.Parameters.AddWithValue("@id", t.Id);
                command.ExecuteNonQuery();
                correcto = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }

        /// <summary>
        /// Este método lee todas
        /// las transacciones de la base de datos
        /// </summary>
        /// <returns>
        /// Verdadero si las lee, falso si no
        /// </returns>
        public List<ENTransaccion> ReadAll()
        {


            List<ENTransaccion> lista = new List<ENTransaccion>();
            SqlConnection conn = null;

            string comando = "Select * from transaccion";
            conn = new SqlConnection(conexion);
            conn.Open();
            SqlCommand cmd = new SqlCommand(comando, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    ENTransaccion en = new ENTransaccion();

                    en.Id = int.Parse(dr["id"].ToString());
                    en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                    if (dr["fecha"] != DBNull.Value)
                    {
                        en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                    }
                    lista.Add(en);

                }
                dr.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine("Ha habido un error");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return lista;

        }

        /// <summary>
        /// Este método lee la primera transacción
        /// de la base de datos
        /// </summary>
        /// <param name="en">
        /// Transacción de la base de datos
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadFirst(ENTransaccion en)
        {
            bool correcto = false;
            SqlConnection conn = null;
            string comando = "select top 1 * from transaccion order by id ASC";
            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand(comando, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    en.Id = int.Parse(dr["id"].ToString());
                    en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                    if (dr["fecha"] != DBNull.Value)
                    {
                        en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                    }
                    correcto = true;

                }
                dr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ha habido un error");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return correcto;

        }

        /// <summary>
        /// Este método lee la ultima transacción
        /// de la base de datos
        /// </summary>
        /// <param name="en">
        /// Transaccion de la base de datos
        /// </param>
        /// <returns></returns>
        public bool ReadLast(ENTransaccion en)
        {
            bool correcto = false;
            SqlConnection conn = null;
            string comando = "select top 1 * from transaccion order by id DESC";
            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand(comando, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    en.Id = int.Parse(dr["id"].ToString());
                    en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                    if (dr["fecha"] != DBNull.Value)
                    {
                        en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                    }
                    correcto = true;

                }
                dr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ha habido un error");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return correcto;

        }

        /// <summary>
        /// Este método lee
        /// la transacción previa a la
        /// base de datos
        /// </summary>
        /// <param name="en">
        /// Transacción de la que se leerá la previa
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadPrev(ENTransaccion en)
        {
            bool correcto = false;
            SqlConnection conn = null;
            string comando = @"select top 1 * from transaccion where id<@id order by id DESC";
            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand(comando, conn);

                cmd.Parameters.AddWithValue("@id", en.Id);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    en.Id = int.Parse(dr["id"].ToString());
                    en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                    if (dr["fecha"] != DBNull.Value)
                    {
                        en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                    }
                    correcto = true;

                }
                dr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ha habido un error");
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return correcto;

        }

        /// <summary>
        /// Este método lee la siguiente
        /// transacción en la base de datos
        /// </summary>
        /// <param name="en">
        /// Transacción de la que se leerá
        /// la próxima
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadNext(ENTransaccion en)
        {
            bool correcto = false;
            SqlConnection conn = null;
            string comando = @"select top 1 * from transaccion where id>@id order by id ASC";
            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand(comando, conn);

                cmd.Parameters.AddWithValue("@id", en.Id);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    en.Id = int.Parse(dr["id"].ToString());
                    en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                    if (dr["fecha"] != DBNull.Value)
                    {
                        en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                    }
                    correcto = true;

                }
                dr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ha habido un error");
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return correcto;

        }

    }
}