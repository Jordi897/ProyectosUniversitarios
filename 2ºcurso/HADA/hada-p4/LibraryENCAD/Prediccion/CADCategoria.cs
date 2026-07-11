using LibraryEN_CAD.Prediccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Prediccion
{
    public class CADCategoria
    {
        private string conexion;

        public CADCategoria()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENCategoria c)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.catPrediccion (Categoria)" +
                    "VALUES (@Categoria)", con);
                command.Parameters.AddWithValue("@Categoria", c.Categoria);

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

        public List<ENCategoria> ReadAll()
        {
            SqlConnection con = new SqlConnection(conexion);
            List<ENCategoria> categorias = new List<ENCategoria>();

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.catPrediccion", con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENCategoria c = new ENCategoria();
                    c.Categoria = reader["categoria"].ToString();
                    categorias.Add(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
            return categorias;
        }
    }
}
