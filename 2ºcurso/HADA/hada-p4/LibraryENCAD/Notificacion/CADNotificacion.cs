using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryEN_CAD.Notificacion
{
    internal class CADNotificacion
    {
        private string conexion;

        public CADNotificacion()
        {
            conexion = System.Configuration.ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }



        public bool Create(ENNotificacion n){

            bool correct = true;
            SqlConnection conexionBD = new SqlConnection(conexion);

            try
            {
                conexionBD.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "INSERT INTO dbo.Notificacion (titulo, mensaje) OUTPUT INSERTED.Id VALUES (@titulo, @mensaje)", conexionBD);

                comando.Parameters.AddWithValue("@titulo", n.Titulo);
                comando.Parameters.AddWithValue("@mensaje", n.Mensaje);

                n.Id = Convert.ToInt32(comando.ExecuteScalar());

                correct = n.Id > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                conexionBD.Close();
            }
            return correct;
        }

        public bool Read(ENNotificacion n)
        {
            bool correct = true;
            SqlConnection conexionBD = new SqlConnection(conexion);
            try
            {
                conexionBD.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "SELECT * FROM dbo.Notificacion WHERE Id = @Id", conexionBD);
                comando.Parameters.AddWithValue("@Id", n.Id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    n.Titulo = (string)reader["titulo"];
                    n.Mensaje = (string)reader["mensaje"];
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
                conexionBD.Close();
            }
            return correct;
        }

        public bool Delete(ENNotificacion n)
        {
            bool correct = true;
            SqlConnection conexionBD = new SqlConnection(conexion);
            try
            {
                conexionBD.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "DELETE FROM dbo.Notificacion WHERE Id = @Id", conexionBD);
                comando.Parameters.AddWithValue("@Id", n.Id);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                conexionBD.Close();
            }
            return correct;
        }

        public bool Update(ENNotificacion n)
        {
            bool correct = true;
            SqlConnection conexionBD = new SqlConnection(conexion);
            try
            {
                conexionBD.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "UPDATE dbo.Notificacion SET titulo = @titulo, mensaje = @mensaje WHERE Id = @Id", conexionBD);

                comando.Parameters.AddWithValue("@Id", n.Id);
                comando.Parameters.AddWithValue("@titulo", n.Titulo);
                comando.Parameters.AddWithValue("@mensaje", n.Mensaje);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correct = false;
            }
            finally
            {
                conexionBD.Close();
            }
            return correct;
        }
    }
        
}
