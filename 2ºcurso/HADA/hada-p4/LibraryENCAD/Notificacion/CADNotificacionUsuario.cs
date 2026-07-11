using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace LibraryEN_CAD.Notificacion
{
    public class CADNotificacionUsuario
    {
        private string conexion;

        public CADNotificacionUsuario()
        {
            conexion = System.Configuration.ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENNotificacionUsuario n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);

            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "INSERT INTO DBO.NotificacionUsuario (usuario, notificacion, leido, fecha) VALUES (@usuario, @notificacion, @leido, @fecha)", cnctn);
                comando.Parameters.AddWithValue("@usuario", n.Usuario);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                comando.Parameters.AddWithValue("@leido", n.Leido);
                comando.Parameters.AddWithValue("@fecha", n.Fecha);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;

        }

        public bool Read(ENNotificacionUsuario n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);


            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "SELECT * FROM dbo.NotificacionUsuario WHERE usuario = @usuario AND notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@usuario", n.Usuario);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    n.Usuario = reader["usuario"].ToString();
                    n.Notificacion = Convert.ToInt32(reader["notificacion"]);
                    n.Leido = reader["leido"] != DBNull.Value && Convert.ToBoolean(reader["leido"]);
                    n.Fecha = reader["fecha"] != DBNull.Value ? Convert.ToDateTime(reader["fecha"]) : DateTime.MinValue;
                }
                else
                {
                    correct = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public bool Update(ENNotificacionUsuario n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);


            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "UPDATE dbo.NotificacionUsuario SET leido = @leido, fecha = @fecha WHERE usuario = @usuario AND notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@usuario", n.Usuario);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                comando.Parameters.AddWithValue("@leido", n.Leido);
                comando.Parameters.AddWithValue("@fecha", n.Fecha);

                correct = comando.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public bool Delete(ENNotificacionUsuario n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);

            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "DELETE FROM dbo.NotificacionUsuario WHERE usuario = @usuario AND notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@usuario", n.Usuario);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public int ContarNotificacionesNoLeidas(string usuario)
        {
            int count = 0;
            SqlConnection cnctn = new SqlConnection(conexion);
            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "SELECT COUNT(*) FROM dbo.NotificacionUsuario WHERE usuario = @usuario AND (leido = 0 OR leido IS NULL)", cnctn);
                comando.Parameters.AddWithValue("@usuario", usuario);
                count = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                count = 0;
            }
            finally
            {
                cnctn.Close();
            }
            return count;
        }

        public bool MarcarComoLeida(string usuario, int notificacion)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);
            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "UPDATE dbo.NotificacionUsuario SET leido = 1 WHERE usuario = @usuario AND notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@notificacion", notificacion);
                correct = comando.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public bool MarcarTodasComoLeidas(string usuario)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);
            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "UPDATE dbo.NotificacionUsuario SET leido = 1 WHERE usuario = @usuario AND (leido = 0 or leido IS NULL)", cnctn);
                comando.Parameters.AddWithValue("@usuario", usuario);
                correct = comando.ExecuteNonQuery() >= 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public DataTable MostrarNoLeidas(string usuario, int top)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnctn = new SqlConnection(conexion))
            {
                try
                {
                    cnctn.Open();
                    using (SqlCommand comando = new SqlCommand(@"
                SELECT TOP (@top)
                    n.id AS Id,
                    n.titulo AS Titulo,
                    n.mensaje AS Mensaje,
                    nu.fecha AS Fecha,
                    COALESCE(nu.leido, 0) AS Leida
                FROM dbo.notificacion n
                JOIN dbo.notificacionusuario nu ON n.id = nu.notificacion
                WHERE nu.usuario = @usuario
                  AND (nu.leido = 0 OR nu.leido IS NULL)
                ORDER BY nu.fecha DESC;", cnctn))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@top", top);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dt;
        }

        public DataTable MostrarTodas(string usuario)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnctn = new SqlConnection(conexion))
            {
                try
                {
                    cnctn.Open();
                    using (SqlCommand comando = new SqlCommand(@"
                SELECT 
                    n.id AS Id,
                    n.titulo AS Titulo,
                    n.mensaje AS Mensaje,
                    nu.fecha AS Fecha,
                    COALESCE(nu.leido, 0) AS Leida
                FROM dbo.notificacion n
                JOIN dbo.notificacionusuario nu ON n.id = nu.notificacion
                WHERE nu.usuario = @usuario
                ORDER BY nu.fecha DESC;", cnctn))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dt;
        }
    }
}
    
