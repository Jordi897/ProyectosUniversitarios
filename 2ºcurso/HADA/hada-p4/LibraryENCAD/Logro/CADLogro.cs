using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;


namespace LibraryEN_CAD.Logro
{
    internal class CADLogro
    {
        private string conexion;
        public CADLogro()
        {
            conexion = System.Configuration.ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENLogro l)
        {
            bool correct = false;
            SqlConnection con = new SqlConnection(conexion);

            string query = "INSERT INTO Logro (titulo, descripcion, recompensa) VALUES (@titulo, @descripcion, @recompensa)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@titulo", l.Titulo);
            cmd.Parameters.AddWithValue("@descripcion", l.Descripcion);
            cmd.Parameters.AddWithValue("@recompensa", l.Recompensa);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return correct;


        }

        public bool Read(ENLogro l)
        {
            bool correct = false;
            SqlConnection con = new SqlConnection(conexion);
            string query = "SELECT * FROM Logro WHERE titulo = @Titulo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    l.Descripcion = reader["Descripcion"].ToString();
                    l.Recompensa = reader["Recompensa"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Recompensa"]) : null;
                    correct = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return correct;
        }

        public bool Write(ENLogro l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);

            string query = "UPDATE Logro SET Descripcion = @Descripcion, Recompensa = @Recompensa WHERE titulo = @Titulo";

            SqlCommand cmd = new SqlCommand(query, cnctn);
            cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
            cmd.Parameters.AddWithValue("@Descripcion", l.Descripcion);
            cmd.Parameters.AddWithValue("@Recompensa", l.Recompensa);

            try
            {
                cnctn.Open();
                cmd.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public bool Update(ENLogro l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);
            string query = "UPDATE Logro SET Descripcion = @Descripcion, Recompensa = @Recompensa WHERE titulo = @Titulo";
            SqlCommand cmd = new SqlCommand(query, cnctn);
            cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
            cmd.Parameters.AddWithValue("@Descripcion", l.Descripcion);
            cmd.Parameters.AddWithValue("@Recompensa", l.Recompensa);
            try
            {
                cnctn.Open();
                cmd.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }

        public bool Delete(ENLogro l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);
            string query = "DELETE FROM Logro WHERE titulo = @Titulo";
            SqlCommand cmd = new SqlCommand(query, cnctn);
            cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
            try
            {
                cnctn.Open();
                cmd.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cnctn.Close();
            }
            return correct;
        }
    }
}
