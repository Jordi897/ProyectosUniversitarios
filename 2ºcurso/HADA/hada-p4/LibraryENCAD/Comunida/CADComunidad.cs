using LibraryEN_CAD.Predicción;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Comunida
{
    internal class CADComunidad
    {
        private string conexion;

        public CADComunidad()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENComunidad p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Comunidad (Id, titulo, descripcion, voto, fechaDeIncursion)" +
                    "VALUES (@Id, @titulo, @descripcion, @voto, @fechaDeIncursion)", con);
                command.Parameters.AddWithValue("@Id", p.Id);
                command.Parameters.AddWithValue("@titulo", p.Titulo);
                command.Parameters.AddWithValue("@descripcion", p.Descripcion);
                command.Parameters.AddWithValue("@voto", p.Voto);
                command.Parameters.AddWithValue("@fechaDeIncursion", p.FechaDeIncursion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public bool Update(ENComunidad p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.Comunidad SET (titulo = @titulo, pregunta = @pregunta, cantidadApostada = @cantidadApostada, tag = @tag, estado = @estado)" +
                    "WHERE Id = @Id;", con);
                command.Parameters.AddWithValue("@Id", p.Id);
                command.Parameters.AddWithValue("@titulo", p.Titulo);
                command.Parameters.AddWithValue("@descripcion", p.Descripcion);
                command.Parameters.AddWithValue("@voto", p.Voto);
                command.Parameters.AddWithValue("@fechaDeIncursion", p.FechaDeIncursion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public bool Delete(ENComunidad p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM dbo.Comunidad WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", p.Id);

                if (command.ExecuteNonQuery() == 0) { return false; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public bool Read(ENComunidad p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * dbo.Comunidad WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", p.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    p.Titulo = reader["Titulo"].ToString();
                    p.Descripcion = reader["Descripcion"].ToString();
                    p.Voto = int.Parse(reader["Voto"].ToString());
                    p.FechaDeIncursion = reader["FechaDeIncursion"].ToString();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }
}
