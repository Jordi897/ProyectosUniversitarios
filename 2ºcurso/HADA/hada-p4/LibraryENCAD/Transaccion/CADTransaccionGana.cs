using LibraryEN_CAD.ConversionDivisa;
using LibraryEN_CAD.Notificacion;
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
    /// el CAD de TransaccionGana
    /// </summary>
    public class CADTransaccionGana
    {

        /// <summary>
        /// Propiedad que representa
        /// la conexión con la base de datos
        /// </summary>
        private string conexion;

        /// <summary>
        /// Constructor con defecto
        /// </summary>
        public CADTransaccionGana()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Este método crea una "TransacciónGana"
        /// en la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónGana" a crear
        /// </param>
        /// <returns>
        /// Verdadero si se crea, falso si no
        /// </returns>
        public bool Create(ENTransaccionGana t)
        {

            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            string email = null;
            
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Insert into dbo.trangana (id,wallet,prediccion) VALUES (@id,@wallet,@prediccion)", con);

                    command.Parameters.AddWithValue("@id", t.Id);
                    command.Parameters.AddWithValue("@wallet", t.Wallet);
                    command.Parameters.AddWithValue("@prediccion", t.Prediccion);
                    command.ExecuteNonQuery();
                    correcto = true;


                    SqlCommand commandEmail = new SqlCommand("select email from dbo.usuario where wallet=@wallet", con);
                    commandEmail.Parameters.AddWithValue("@wallet", t.Wallet);
                    email = commandEmail.ExecuteScalar() as String;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            

                if (!string.IsNullOrEmpty(email) && correcto)
                {
                try {
                    ENNotificacion en = new ENNotificacion();
                    en.Titulo = "Prediccion resulta";
                    en.Mensaje = "Se ha resuelto una prediccion";
                    en.Create();

                    ENNotificacionUsuario enu = new ENNotificacionUsuario();
                    enu.Usuario = email;
                    enu.Notificacion = en.Id;
                    enu.Leido = false;
                    enu.Fecha = DateTime.Now;
                    enu.Create();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear la notificacion: " + ex.Message);
                }
            }
            
           
            return correcto;

        }

        /// <summary>
        /// Este método lee una "TransacciónGana" de
        /// la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónGana" a leer
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool Read(ENTransaccionGana t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from dbo.trangana where id=@Id", con);
                command.Parameters.AddWithValue("@id", t.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    t.Wallet = int.Parse(reader["wallet"].ToString());
                    t.Prediccion = int.Parse(reader["prediccion"].ToString());                  
                    correcto = true;
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
        /// "TransacciónGana" en la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónGana" a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si actualiza, falso si no
        /// </returns>
        public bool Update(ENTransaccionGana t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Update dbo.trangana set wallet=@wallet, prediccion=@prediccion where id=@id;", con);

                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@wallet", t.Wallet);
                command.Parameters.AddWithValue("@prediccion", t.Prediccion);
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
        /// "TransacciónGana" de la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónGana" a borrar
        /// </param>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public bool Delete(ENTransaccionGana t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Delete from trangana where id=@id", con);

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
        /// las transacciones de tipo gana
        /// y devuelve una lista con ellas
        /// </summary>
        /// <returns>
        /// Lista de transacciones gana
        /// </returns>

        public List<ENTransaccionGana> ReadAll()
        {
            {

                List<ENTransaccionGana> lista = new List<ENTransaccionGana>();
                SqlConnection conn = null;

                string comando = "Select t.id, t.cantidad, t.fecha, g.prediccion, g.wallet " + "from transaccion t " + "inner join trangana g on t.id = g.id " + "order by t.id";
                conn = new SqlConnection(conexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand(comando, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ENTransaccionGana en = new ENTransaccionGana();

                        en.Id = int.Parse(dr["id"].ToString());
                        en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                        if (dr["fecha"] != DBNull.Value)
                        {
                            en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                        }

                        en.Wallet = int.Parse(dr["wallet"].ToString());
                        en.Prediccion = int.Parse(dr["prediccion"].ToString());
                        lista.Add(en);

                    }
                    dr.Close();

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ha habido un error: " + e.Message);
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


        }
    }
}
