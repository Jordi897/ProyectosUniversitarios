using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Mensaje
{
    internal class CADMensaje
    {
        /// <summary>
        /// String de conexión a la base de datos, 
        /// se obtiene del app.config
        /// </summary>
        private string connectionString;
        /// <summary>
        /// Inicializa el string de conexión a la base de datos, 
        /// se obtiene del app.config
        /// </summary>
        public CADMensaje()
        {
            connectionString = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }
        /// <summary>
        /// Escribe un mensaje en la base de datos, 
        /// utilizando los atributos del mensaje pasado como parámetro
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool Write(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "INSERT INTO dbo.mensaje (emisor, chat1, chat2, contenido, fecha) VALUES (@EmisorEmail, @Chat1Email, @Chat2Email, @Contenido, @Fecha)";
            SqlCommand command = new SqlCommand(sql, connection);
            if (mensaje.Chat1 == null || mensaje.Chat2 == null || mensaje.Emisor == null) return false;
            command.Parameters.AddWithValue("@EmisorEmail", mensaje.Emisor.Email);
            command.Parameters.AddWithValue("@Chat1Email", mensaje.Chat1.Email);
            command.Parameters.AddWithValue("@Chat2Email", mensaje.Chat2.Email);
            command.Parameters.AddWithValue("@Contenido", mensaje.Contenido);
            command.Parameters.AddWithValue("@Fecha", DateTime.Now);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                //Manejar la excepción según sea necesario

            Console.WriteLine("Error al escribir el mensaje: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        /// <summary>
        /// Lee un mensaje de la base de datos en el mensaje recibido, 
        /// utilizando el código del mensaje pasado como parámetro
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool Read(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT emisor, chat1, chat2, id, contenido, fecha FROM dbo.mensaje WHERE id = @Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", mensaje.Id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mensaje.Emisor.Email = reader["Emisor"].ToString();
                    mensaje.Chat1.Email = reader["Chat1"].ToString();
                    mensaje.Chat2.Email = reader["Chat2"].ToString();
                    mensaje.Contenido = reader["Contenido"].ToString();
                    mensaje.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    result = true;
                    mensaje.Emisor.Read();
                    mensaje.Chat1.Read();
                    mensaje.Chat2.Read();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer el mensaje: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public bool ReadLastChat(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT TOP 1 emisor, chat1, chat2, id, contenido, fecha FROM dbo.mensaje WHERE (chat1 = @Chat1Email AND chat2 = @Chat2Email) OR (chat1 = @Chat2Email AND chat2 = @Chat1Email) ORDER BY fecha DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Chat1Email", mensaje.Chat1.Email);
            command.Parameters.AddWithValue("@Chat2Email", mensaje.Chat2.Email);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mensaje.Emisor.Email = reader["Emisor"].ToString();
                    mensaje.Chat1.Email = reader["Chat1"].ToString();
                    mensaje.Chat2.Email = reader["Chat2"].ToString();
                    mensaje.Contenido = reader["Contenido"].ToString();
                    mensaje.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    result = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer el último mensaje del chat: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public bool ReadNextChat(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT TOP 1 emisor, chat1, chat2, id, contenido, fecha FROM dbo.mensaje WHERE ((chat1 = @Chat1Email AND chat2 = @Chat2Email) OR (chat1 = @Chat2Email AND chat2 = @Chat1Email)) AND fecha > @Fecha ORDER BY fecha ASC";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Chat1Email", mensaje.Chat1.Email);
            command.Parameters.AddWithValue("@Chat2Email", mensaje.Chat2.Email);
            command.Parameters.AddWithValue("@Fecha", mensaje.Fecha);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mensaje.Emisor.Email = reader["Emisor"].ToString();
                    mensaje.Chat1.Email = reader["Chat1"].ToString();
                    mensaje.Chat2.Email = reader["Chat2"].ToString();
                    mensaje.Contenido = reader["Contenido"].ToString();
                    mensaje.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    result = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer el siguiente mensaje del chat: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Elimina un mensaje de la base de datos, 
        /// utilizando el código del mensaje pasado como parámetro
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool Delete(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "DELETE FROM dbo.mensaje WHERE id = @Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", mensaje.Id);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al eliminar el mensaje: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Actualiza un mensaje para poderlo editar
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool Update(ENMensaje mensaje)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "UPDATE dbo.mensaje SET emisor = @EmisorEmail, chat1 = @Chat1Email, chat2 = @Chat2Email, contenido = @Contenido, fecha = @Fecha WHERE id = @Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmisorEmail", mensaje.Emisor.Email);
            command.Parameters.AddWithValue("@Chat1Email", mensaje.Chat1.Email);
            command.Parameters.AddWithValue("@Chat2Email", mensaje.Chat2.Email);
            command.Parameters.AddWithValue("@Contenido", mensaje.Contenido);
            command.Parameters.AddWithValue("@Fecha", mensaje.Fecha);
            command.Parameters.AddWithValue("@Id", mensaje.Id);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al actualizar el mensaje: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
