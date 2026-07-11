using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryEN_CAD.Notificacion
{
    internal class CADNotiPrediccion
    {
        private string conexion;


        public CADNotiPrediccion()
        {
            conexion = System.Configuration.ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }


        public bool Create(ENNotiPrediccion n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);
            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "INSERT INTO dbo.NotiPrediccion (notificacion, prediccion) VALUES (@notificacion, @prediccion)", cnctn);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                comando.Parameters.AddWithValue("@prediccion", n.Prediccion);
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


        public bool Read(ENNotiPrediccion n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);

            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "SELECT * FROM dbo.NotiPrediccion WHERE notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    n.Prediccion = reader.GetInt32(reader.GetOrdinal("prediccion"));
                    correct = true;
                }
                else
                {
                    correct = false;
                }
                reader.Close();
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

        public bool Update(ENNotiPrediccion n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);


            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "UPDATE dbo.NotiPrediccion SET prediccion = @prediccion WHERE notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                comando.Parameters.AddWithValue("@prediccion", n.Prediccion);
                int rowsAffected = comando.ExecuteNonQuery();
                correct = rowsAffected > 0;
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

        public bool Delete(ENNotiPrediccion n)
        {
            bool correct = true;
            SqlConnection cnctn = new SqlConnection(conexion);


            try
            {
                cnctn.Open();
                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(
                    "DELETE FROM dbo.NotiPrediccion WHERE notificacion = @notificacion", cnctn);
                comando.Parameters.AddWithValue("@notificacion", n.Notificacion);
                int rowsAffected = comando.ExecuteNonQuery();
                correct = rowsAffected > 0;
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
    }
}