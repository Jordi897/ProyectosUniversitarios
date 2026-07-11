using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LibraryEN_CAD.Logro
{
    public class CADLogroUsuario
    {
        private string conexion;

        public CADLogroUsuario()
        {
            conexion = System.Configuration.ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENLogroUsuario l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);

            string consulta = @"
                IF EXISTS (SELECT 1 FROM dbo.logro WHERE titulo = @logro)
                AND NOT EXISTS (SELECT 1 FROM dbo.logrousuario WHERE logro = @logro AND usuario = @usuario)
                BEGIN
                    INSERT INTO dbo.logrousuario (logro, usuario) VALUES (@logro, @usuario);
                END";

            SqlCommand cmd = new SqlCommand(consulta, cnctn);

            cmd.Parameters.AddWithValue("@logro", l.Logro);
            cmd.Parameters.AddWithValue("@usuario", l.Usuario);


            try
            {
                cnctn.Open();
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

        public bool Read(ENLogroUsuario l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);

            string consulta = "SELECT * FROM LogroUsuario WHERE logro = @logro AND usuario = @usuario";

            SqlCommand cmd = new SqlCommand(consulta, cnctn);
            cmd.Parameters.AddWithValue("@logro", l.Logro);
            cmd.Parameters.AddWithValue("@usuario", l.Usuario);


            try
            {
                cnctn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    l.Logro = reader["logro"].ToString();
                    l.Usuario = reader["usuario"].ToString();
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
                cnctn.Close();
            }
            return correct;
        }

        //El metodo Update no es necesario ya que la tabla LogroUsuario solo tiene dos campos que forman la clave primaria, por lo que no se pueden modificar.

        public bool Delete(ENLogroUsuario l)
        {
            bool correct = false;
            SqlConnection cnctn = new SqlConnection(conexion);


            string consulta = "DELETE FROM LogroUsuario WHERE logro = @logro AND usuario = @usuario";


            SqlCommand cmd = new SqlCommand(consulta, cnctn);
            cmd.Parameters.AddWithValue("@logro", l.Logro);
            cmd.Parameters.AddWithValue("@usuario", l.Usuario);


            try
            {
                cnctn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                correct = rowsAffected > 0;
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


        public DataTable MostrarTodosLogros(string usuario)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnctn = new SqlConnection(conexion))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                    l.titulo AS Titulo,
                    l.descripcion AS Descripcion,
                    l.recompensa AS Recompensa,
                    CASE WHEN lu.usuario IS NULL THEN 0 ELSE 1 END AS Conseguido
                FROM dbo.Logro l
                LEFT JOIN dbo.LogroUsuario lu
                    ON lu.logro = l.titulo AND lu.usuario = @usuario
                ORDER BY l.titulo;", cnctn))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);

                try
                {
                    cnctn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return dt;
        }
    }
}
    