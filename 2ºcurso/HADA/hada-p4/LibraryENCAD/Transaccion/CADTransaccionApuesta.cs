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
    /// el CAD de TransaccionApuesta
    /// </summary>
    public class CADTransaccionApuesta
    {

        /// <summary>
        /// Propiedad que representa
        /// la conexión con la base de datos
        /// </summary>
        private string conexion;


        /// <summary>
        /// Constructor con defecto
        /// </summary>
        public CADTransaccionApuesta()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Este método crea una "TransacciónApuesta"
        /// en la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónApuesta" a crear
        /// </param>
        /// <returns>
        /// Verdadero si se crea, falso si no
        /// </returns>
        public bool Create(ENTransaccionApuesta t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            string email = null;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Insert into dbo.tranaapuesta (id,voto,wallet,prediccion)" + "VALUES (@id,@voto,@wallet,@prediccion)", con);

                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@voto", t.Voto);
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
                try
                {
                    ENNotificacion en = new ENNotificacion();
                    en.Titulo = "Participación completada";
                    en.Mensaje = "Se ha participado en una predicción";
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
        /// Este método lee una "TransacciónApuesta" de
        /// la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónApuesta" a leer
        /// </param>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool Read(ENTransaccionApuesta t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from dbo.tranaapuesta where id=@Id", con);
                command.Parameters.AddWithValue("@id", t.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    t.Voto = reader["voto"].ToString().Trim();
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
        /// "TransacciónApuesta" en la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónApuesta" a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si actualiza, falso si no
        /// </returns>
        public bool Update(ENTransaccionApuesta t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Update dbo.tranaapuesta set voto=@voto,wallet=@wallet,prediccion=@prediccion where id=@id;", con);

                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@voto", t.Voto);
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
        /// "TransacciónApuesta" de la base de datos
        /// </summary>
        /// <param name="t">
        /// "TransacciónApuesta" a borrar
        /// </param>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public bool Delete(ENTransaccionApuesta t)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Delete from tranaapuesta where id=@id", con);

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
        /// Este método almacena y devuelve
        /// todas las transacciones asociadas
        /// a una predicción
        /// </summary>
        /// <param name="titulo">
        /// Titulo de la predicción
        /// </param>
        /// <returns>
        /// Lista de transacciones
        /// </returns>
        public List<ENTransaccionApuesta> ObtenerPorPrediccion(string titulo)
        {
            List<ENTransaccionApuesta> lista = new List<ENTransaccionApuesta>();
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();

                SqlCommand command = new SqlCommand("select * from dbo.prediccion where titulo=@titulo", con);
                command.Parameters.AddWithValue("@titulo", titulo);
                int predId = 1;

                SqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                {
                    predId = int.Parse(dr["id"].ToString());
                }
                else
                {
                    dr.Close();
                    return lista;
                }
                dr.Close();


                SqlCommand command2 = new SqlCommand("select id from dbo.tranaapuesta where id=@predId", con);
                command2.Parameters.AddWithValue("@predId", predId);
                SqlDataReader dr2 = command2.ExecuteReader();

                while (dr2.Read())
                {
                    ENTransaccionApuesta en = new ENTransaccionApuesta();
                    en.Id = int.Parse(dr2["id"].ToString());
                    en.Read();
                    lista.Add(en);
                }
                dr2.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {
                con.Close();
            }
            return lista;
        }
        /// <summary>
        /// Este método lee
        /// todas las transacciones de 
        /// tipo apuesta y crea una lista
        /// </summary>
        /// <returns>
        /// Lista de transacciones tipo apuesta
        /// </returns>
        public List<ENTransaccionApuesta> ReadAll()
        {
            {

                List<ENTransaccionApuesta> lista = new List<ENTransaccionApuesta>();
                SqlConnection conn = new SqlConnection(conexion);
                string comando = "Select t.id, t.cantidad, t.fecha, a.voto, a.prediccion, a.wallet " + "from transaccion t " + "inner join tranaapuesta a on t.id = a.id " + "order by t.id";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ENTransaccionApuesta en = new ENTransaccionApuesta();

                        en.Id = int.Parse(dr["id"].ToString());
                        en.Cantidad = decimal.Parse(dr["cantidad"].ToString());
                        if (dr["fecha"] != DBNull.Value)
                        {
                            en.Fecha = DateTime.Parse(dr["fecha"].ToString());
                        }
                        en.Wallet = int.Parse(dr["wallet"].ToString());
                        en.Prediccion = int.Parse(dr["prediccion"].ToString());
                        en.Voto = dr["voto"].ToString();
                        en.Read();
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
                    conn.Close();
                }
                return lista;
            }

        }
    }
}
