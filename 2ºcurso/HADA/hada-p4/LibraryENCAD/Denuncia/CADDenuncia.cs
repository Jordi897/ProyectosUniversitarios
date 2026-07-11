using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryEN_CAD.Denuncia
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Denuncia.
    /// Se encarga de realizar las operaciones sobre la tabla dbo.denuncia
    /// y de gestionar la moderación de comentarios denunciados.
    /// </summary>
    internal class CADDenuncia
    {
        private string conexion;

        /// <summary>
        /// Constructor que obtiene la cadena de conexión definida en el archivo de configuración.
        /// </summary>
        public CADDenuncia()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Inserta una nueva denuncia en la base de datos.
        /// </summary>
        /// <param name="denuncia">Denuncia que se quiere crear.</param>
        /// <returns>True si la inserción se realiza correctamente; false en caso contrario.</returns>
        public bool Create(ENDenuncia denuncia)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "INSERT INTO dbo.denuncia (causadenuncia, descripcion, fecha, estado, emisor, comentario) " +
                    "VALUES (@causadenuncia, @descripcion, @fecha, @estado, @emisor, @comentario)",
                    conexionBD);

                comando.Parameters.AddWithValue("@causadenuncia", denuncia.CausaDenuncia);
                comando.Parameters.AddWithValue("@descripcion", denuncia.Descripcion);
                comando.Parameters.AddWithValue("@fecha", denuncia.Fecha);
                comando.Parameters.AddWithValue("@estado", denuncia.Estado);
                comando.Parameters.AddWithValue("@emisor", denuncia.Emisor);
                comando.Parameters.AddWithValue("@comentario", denuncia.Comentario);

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
        /// Lee una denuncia de la base de datos a partir de su identificador.
        /// Si existe, rellena las propiedades del objeto recibido.
        /// </summary>
        /// <param name="denuncia">Denuncia con el Id que se desea buscar.</param>
        /// <returns>True si se encuentra la denuncia; false en caso contrario.</returns>
        public bool Read(ENDenuncia denuncia)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "SELECT * FROM dbo.denuncia WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", denuncia.Id);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    denuncia.CausaDenuncia = reader["causadenuncia"].ToString();
                    denuncia.Descripcion = reader["descripcion"].ToString();
                    denuncia.Fecha = Convert.ToDateTime(reader["fecha"]);
                    denuncia.Estado = reader["estado"].ToString().Trim();
                    denuncia.Emisor = reader["emisor"].ToString();
                    denuncia.Comentario = Convert.ToInt32(reader["comentario"]);
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
        /// Actualiza los datos de una denuncia existente.
        /// </summary>
        /// <param name="denuncia">Denuncia con los nuevos valores.</param>
        /// <returns>True si se actualiza algún registro; false en caso contrario.</returns>
        public bool Update(ENDenuncia denuncia)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "UPDATE dbo.denuncia " +
                    "SET causadenuncia = @causadenuncia, descripcion = @descripcion, fecha = @fecha, estado = @estado, emisor = @emisor, comentario = @comentario " +
                    "WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", denuncia.Id);
                comando.Parameters.AddWithValue("@causadenuncia", denuncia.CausaDenuncia);
                comando.Parameters.AddWithValue("@descripcion", denuncia.Descripcion);
                comando.Parameters.AddWithValue("@fecha", denuncia.Fecha);
                comando.Parameters.AddWithValue("@estado", denuncia.Estado);
                comando.Parameters.AddWithValue("@emisor", denuncia.Emisor);
                comando.Parameters.AddWithValue("@comentario", denuncia.Comentario);

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
        /// Elimina una denuncia de la base de datos a partir de su identificador.
        /// </summary>
        /// <param name="denuncia">Denuncia con el Id que se desea eliminar.</param>
        /// <returns>True si se elimina algún registro; false en caso contrario.</returns>
        public bool Delete(ENDenuncia denuncia)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "DELETE FROM dbo.denuncia WHERE id = @id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", denuncia.Id);

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
        /// Elimina todas las denuncias asociadas a un comentario concreto.
        /// Es útil antes de eliminar un comentario, ya que la tabla denuncia
        /// tiene una clave externa hacia la tabla comentario.
        /// </summary>
        /// <param name="comentario">Identificador del comentario cuyas denuncias se quieren eliminar.</param>
        /// <returns>True si la operación se realiza correctamente; false en caso contrario.</returns>
        public bool DeletePorComentario(int comentario)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool ok = true;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "DELETE FROM dbo.denuncia WHERE comentario = @comentario",
                    conexionBD);

                comando.Parameters.AddWithValue("@comentario", comentario);

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
        /// Obtiene las denuncias pendientes junto con información del comentario denunciado.
        /// Este método utiliza SqlDataAdapter y DataSet para realizar acceso desconectado.
        /// </summary>
        /// <returns>DataSet con las denuncias pendientes.</returns>
        public System.Data.DataSet ListarPendientes()
        {
            System.Data.DataSet datos = new System.Data.DataSet();
            SqlConnection conexionBD = new SqlConnection(conexion);

            try
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(
                    "SELECT d.id, d.causadenuncia, d.descripcion, d.fecha, d.estado, d.emisor, " +
                    "d.comentario, c.usuario, c.mensaje " +
                    "FROM dbo.denuncia d " +
                    "INNER JOIN dbo.comentario c ON d.comentario = c.id " +
                    "WHERE d.estado = 'PENDIENTE' " +
                    "ORDER BY d.fecha DESC",
                    conexionBD);

                adaptador.Fill(datos, "denuncia");
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

        /// <summary>
        /// Comprueba si un usuario ya ha denunciado un comentario concreto.
        /// Se usa para evitar denuncias duplicadas del mismo usuario sobre el mismo comentario.
        /// </summary>
        /// <param name="comentario">Identificador del comentario denunciado.</param>
        /// <param name="emisor">Email del usuario que realiza la denuncia.</param>
        /// <returns>True si ya existe una denuncia; false si no existe.</returns>
        public bool ExisteDenuncia(int comentario, string emisor)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            bool existe = false;

            try
            {
                conexionBD.Open();

                SqlCommand comando = new SqlCommand(
                    "SELECT COUNT(*) FROM dbo.denuncia WHERE comentario = @comentario AND emisor = @emisor",
                    conexionBD);

                comando.Parameters.AddWithValue("@comentario", comentario);
                comando.Parameters.AddWithValue("@emisor", emisor);

                int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                existe = cantidad > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                existe = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return existe;
        }

        /// <summary>
        /// Acepta una denuncia y aplica su consecuencia de moderación:
        /// elimina todas las denuncias asociadas al mismo comentario y después
        /// elimina el comentario denunciado.
        /// 
        /// La operación se realiza dentro de una transacción para evitar que la
        /// base de datos quede en un estado incoherente si falla alguna consulta.
        /// </summary>
        /// <param name="denuncia">Denuncia que se quiere aceptar.</param>
        /// <returns>True si la denuncia se acepta correctamente; false en caso contrario.</returns>
        public bool AceptarDenuncia(ENDenuncia denuncia)
        {
            SqlConnection conexionBD = new SqlConnection(conexion);
            SqlTransaction transaccion = null;
            bool ok = true;

            try
            {
                conexionBD.Open();
                transaccion = conexionBD.BeginTransaction();

                SqlCommand obtenerComentario = new SqlCommand(
                    "SELECT comentario FROM dbo.denuncia WHERE id = @id",
                    conexionBD,
                    transaccion);

                obtenerComentario.Parameters.AddWithValue("@id", denuncia.Id);

                object resultado = obtenerComentario.ExecuteScalar();

                if (resultado == null)
                {
                    transaccion.Rollback();
                    return false;
                }

                int idComentario = Convert.ToInt32(resultado);

                SqlCommand borrarDenuncias = new SqlCommand(
                    "DELETE FROM dbo.denuncia WHERE comentario = @comentario",
                    conexionBD,
                    transaccion);

                borrarDenuncias.Parameters.AddWithValue("@comentario", idComentario);
                borrarDenuncias.ExecuteNonQuery();

                SqlCommand borrarComentario = new SqlCommand(
                    "DELETE FROM dbo.comentario WHERE id = @comentario",
                    conexionBD,
                    transaccion);

                borrarComentario.Parameters.AddWithValue("@comentario", idComentario);

                if (borrarComentario.ExecuteNonQuery() == 0)
                {
                    transaccion.Rollback();
                    return false;
                }

                transaccion.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                ok = false;
            }
            finally
            {
                conexionBD.Close();
            }

            return ok;
        }
    }
}