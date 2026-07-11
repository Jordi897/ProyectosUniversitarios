using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryEN_CAD.Comentario
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Comentario.
    /// Se encarga de realizar las operaciones sobre la tabla dbo.comentario.
    /// </summary>
    internal class CADComentario
    {
        private string conexion;

        /// <summary>
        /// Constructor que obtiene la cadena de conexión definida en el archivo de configuración.
        /// </summary>
        public CADComentario()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Inserta un nuevo comentario en la base de datos.
        /// </summary>
        /// <param name="comentario">Comentario que se desea crear.</param>
        /// <returns>True si la inserción se realiza correctamente; false en caso contrario.</returns>
        public bool Create(ENComentario comentario)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "INSERT INTO dbo.comentario (mensaje, fecha, usuario, prediccion) " +
                    "VALUES (@mensaje, @fecha, @usuario, @prediccion)",
                    conexionBD);

                comando.Parameters.AddWithValue("@mensaje", comentario.Mensaje);
                comando.Parameters.AddWithValue("@fecha", comentario.Fecha);
                comando.Parameters.AddWithValue("@usuario", comentario.Usuario);
                comando.Parameters.AddWithValue("@prediccion", comentario.Prediccion);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ok = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return ok;
        }

        /// <summary>
        /// Lee un comentario de la base de datos a partir de su identificador.
        /// Si existe, rellena las propiedades del objeto recibido.
        /// </summary>
        /// <param name="comentario">Comentario con el Id que se desea buscar.</param>
        /// <returns>True si se encuentra el comentario; false en caso contrario.</returns>
        public bool Read(ENComentario comentario)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "SELECT * FROM dbo.comentario WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", comentario.Id);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    comentario.Mensaje = reader["mensaje"].ToString();
                    comentario.Fecha = Convert.ToDateTime(reader["fecha"]);
                    comentario.Usuario = reader["usuario"].ToString();
                    comentario.Prediccion = Convert.ToInt32(reader["prediccion"]);
                }
                else
                {
                    ok = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ok = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return ok;
        }

        /// <summary>
        /// Actualiza los datos de un comentario existente.
        /// </summary>
        /// <param name="comentario">Comentario con los nuevos valores.</param>
        /// <returns>True si se actualiza algún registro; false en caso contrario.</returns>
        public bool Update(ENComentario comentario)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "UPDATE dbo.comentario " +
                    "SET mensaje = @mensaje, fecha = @fecha, usuario = @usuario, prediccion = @prediccion " +
                    "WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", comentario.Id);
                comando.Parameters.AddWithValue("@mensaje", comentario.Mensaje);
                comando.Parameters.AddWithValue("@fecha", comentario.Fecha);
                comando.Parameters.AddWithValue("@usuario", comentario.Usuario);
                comando.Parameters.AddWithValue("@prediccion", comentario.Prediccion);

                if (comando.ExecuteNonQuery() == 0)
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ok = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return ok;
        }

        /// <summary>
        /// Elimina un comentario de la base de datos a partir de su identificador.
        /// </summary>
        /// <param name="comentario">Comentario con el Id que se desea eliminar.</param>
        /// <returns>True si se elimina algún registro; false en caso contrario.</returns>
        public bool Delete(ENComentario comentario)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "DELETE FROM dbo.comentario WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", comentario.Id);

                if (comando.ExecuteNonQuery() == 0)
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ok = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return ok;
        }

        /// <summary>
        /// Obtiene todos los comentarios asociados a una predicción concreta,
        /// ordenados por fecha de creación (desde el más reciente al más antiguo).
        /// Este método utiliza SqlDataAdapter y DataSet para realizar acceso desconectado.
        /// </summary>
        /// <param name="prediccion">Identificador de la predicción.</param>
        /// <returns>DataSet con los comentarios de la predicción indicada.</returns>
        public System.Data.DataSet ListarPorPrediccion(int prediccion)
        {
            System.Data.DataSet datos = new System.Data.DataSet();
            SqlConnection conexionBD = new SqlConnection(conexion);

            try
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(
                    "SELECT c.id, c.usuario, u.nickname, c.mensaje, c.fecha " +
                    "FROM dbo.comentario c " +
                    "INNER JOIN dbo.usuario u ON c.usuario = u.email " +
                    "WHERE c.prediccion = @prediccion " +
                    "ORDER BY c.fecha DESC",
                    conexionBD);

                adaptador.SelectCommand.Parameters.AddWithValue("@prediccion", prediccion);
                adaptador.Fill(datos, "comentario");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return datos;
        }
    }
}