using LibraryEN_CAD.Mensaje;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.ChatPrivado
{
    internal class CADChatPrivado
    {
        /// <summary>
        /// String de conexión a la base de datos, se obtiene del app.config
        /// </summary>
        private string conString;
        /// <summary>
        /// Constructor por defecto, obtiene el string de conexión del app.config
        /// </summary>
        public CADChatPrivado()
        {
            conString = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }
        /// <summary>
        /// Guarda un nuevo chat privado en la base de datos, 
        /// si el chat no es admitido por la base de datos, 
        /// se intenta guardar con los usuarios intercambiados
        /// ya que la base de datos solo admite que el usuario1 sea el que tenga el email menor al usuario2
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public bool Write(ENChatPrivado chat)
        {
            bool result = false;
            
            if (chat.Usuario1 == null || chat.Usuario2 == null) return false;
            
            string sql = "INSERT INTO dbo.chat (usuario1, usuario2) VALUES (@Usuario1Email, @Usuario2Email)";
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {

                    SqlCommand command = new SqlCommand(sql, connection);


                    command.Parameters.AddWithValue("@Usuario1Email", chat.Usuario1.Email);
                    command.Parameters.AddWithValue("@Usuario2Email", chat.Usuario2.Email);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected > 0;
                }

            }
            catch (SqlException)
            {
                using (SqlConnection connection2 = new SqlConnection(conString))
                {
                    connection2.Open();

                    string sql2 = "INSERT INTO dbo.chat (usuario1, usuario2) VALUES (@Usuario1Email, @Usuario2Email)";

                    using (SqlCommand command2 = new SqlCommand(sql2, connection2))
                    {
                        command2.Parameters.AddWithValue("@Usuario1Email", chat.Usuario2.Email);
                        command2.Parameters.AddWithValue("@Usuario2Email", chat.Usuario1.Email);

                        try
                        {
                            int rowsAffected = command2.ExecuteNonQuery();
                            result = rowsAffected > 0;
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al escribir el chat privado: " + ex.Message);
                result = false;
                throw;
            }

            return result;
        }

            
        /// <summary>
        /// Lee un chat privado de la base de datos,
        /// devuelve true si el chat existe, false en caso contrario
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public bool Read(ENChatPrivado chat)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(conString);
            string sql = "SELECT usuario1, usuario2 FROM dbo.chat WHERE (usuario1 = @Chat1Email AND usuario2 = @Chat2Email) OR (usuario1 = @Chat2Email AND usuario2 = @Chat1Email)";
            SqlCommand command = new SqlCommand(sql, connection);
            if (chat.Usuario1 == null || chat.Usuario2 == null) return false;
            command.Parameters.AddWithValue("@Chat1Email", chat.Usuario1.Email);
            command.Parameters.AddWithValue("@Chat2Email", chat.Usuario2.Email);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    chat.Usuario1.Email = reader["usuario1"].ToString();
                    chat.Usuario2.Email = reader["usuario2"].ToString();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer el chat privado: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Borra un chat privado de la base de datos
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public bool Delete(ENChatPrivado chat)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(conString);
            string sql = "DELETE FROM dbo.chat WHERE usuario1 = @Chat1Email AND usuario2 = @Chat2Email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Chat1Email", chat.Usuario1.Email);
            command.Parameters.AddWithValue("@Chat2Email", chat.Usuario2.Email);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al eliminar el chat privado: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Devuelve una lista con todos los mensajes de un chat privado, si el chat no existe devuelve una lista vacía
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public List<ENMensaje> ReadMensajes(ENChatPrivado chat)
        {
            List<ENMensaje> mensajes = new List<ENMensaje>();
            SqlConnection connection = new SqlConnection(conString);
            string sql = "SELECT * FROM dbo.mensaje WHERE @Chat1 = chat1 AND @Chat2 = chat2";
            SqlCommand command = new SqlCommand(sql, connection);
            if (chat.Usuario1 == null || chat.Usuario2 == null) return mensajes;
            command.Parameters.AddWithValue("@Chat1", chat.Usuario1.Email);
            command.Parameters.AddWithValue("@Chat2", chat.Usuario2.Email);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENMensaje mensaje = new ENMensaje();
                    mensaje.Chat1.Email = reader["chat1"].ToString();
                    mensaje.Chat2.Email = reader["chat2"].ToString();
                    mensaje.Contenido = reader["contenido"].ToString();
                    mensaje.Fecha = Convert.ToDateTime(reader["fecha"]);
                    mensaje.Emisor.Email = reader["emisor"].ToString();
                    mensaje.Id = Convert.ToInt32(reader["Id"]);
                    mensajes.Add(mensaje);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer todos los chats privados: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return mensajes;
        }
        /// <summary>
        /// Borra todos los mensajes de un chat privado, si el chat no existe devuelve false
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public bool VaciarChat(ENChatPrivado chat)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(conString);
            string sql = "DELETE FROM dbo.mensaje WHERE @Chat1 = chat1 AND @Chat2 = chat2";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Chat1", chat.Usuario1.Email);
            command.Parameters.AddWithValue("@Chat2", chat.Usuario2.Email);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al eliminar los mensajes del chat privado: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public List<ENChatPrivado> ChatsUsuario(ENChatPrivado chat) {
            List<ENChatPrivado> chats = new List<ENChatPrivado>();
            if (chat.Usuario1 == null||chat.Usuario1.Email==null) return chats;
            SqlConnection connection = new SqlConnection(conString);
            string sql = "SELECT usuario1, usuario2 FROM dbo.chat WHERE usuario1 = @UsuarioEmail OR usuario2 = @UsuarioEmail";
            SqlCommand command = new SqlCommand(sql, connection);
            
            try
            {
                
                command.Parameters.AddWithValue("@UsuarioEmail", chat.Usuario1.Email);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENChatPrivado chatPrivado = new ENChatPrivado();
                    if(chat.Usuario1.Email== reader["usuario1"].ToString())
                    {
                        chatPrivado.Usuario1.Email = reader["usuario1"].ToString();
                        chatPrivado.Usuario2.Email = reader["usuario2"].ToString();
                    }
                    else
                    {
                        chatPrivado.Usuario2.Email = reader["usuario1"].ToString();
                        chatPrivado.Usuario1.Email = reader["usuario2"].ToString();
                    }

                        chatPrivado.Usuario1.Read();
                    chatPrivado.Usuario2.Read();
                    chats.Add(chatPrivado);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer todos los chats privados de un usuario: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return chats;
        }
        //No se implementa un método para actualizar un chat privado, ya que no tiene sentido actualizar un chat privado, lo que se puede hacer es eliminarlo y crear uno nuevo con los datos actualizados
    }
}
