using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Comunidad
{
    public class CADComunidad
    {
        private string conexion;

        public CADComunidad()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENComunidad c)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.comunidad (titulo, descripcion, fechaincursion, prediccion, voto) " +
                    "VALUES (@titulo, @descripcion, @fechaincursion, @prediccion, @voto)", con);
                command.Parameters.AddWithValue("@titulo", c.Titulo);
                command.Parameters.AddWithValue("@descripcion", c.Descripcion);
                command.Parameters.AddWithValue("@fechaincursion", DateTime.Now);
                command.Parameters.AddWithValue("@prediccion", c.Prediccion);
                command.Parameters.AddWithValue("@voto", c.Voto);

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

        public bool Update(ENComunidad c)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.comunidad SET titulo = @titulo, descripcion = @descripcion, voto = @voto " +
                    "WHERE id = @id", con);
                command.Parameters.AddWithValue("@id", c.Id);
                command.Parameters.AddWithValue("@titulo", c.Titulo);
                command.Parameters.AddWithValue("@descripcion", c.Descripcion);
                command.Parameters.AddWithValue("@voto", c.Voto);

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

        public bool Delete(ENComunidad c)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM dbo.comunidad WHERE id = @Id", con);
                command.Parameters.AddWithValue("@Id", c.Id);

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

        public bool Read(ENComunidad c)
        {
            SqlConnection con = new SqlConnection(conexion);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT c.*, w.saldo FROM dbo.comunidad c " +
                    "LEFT JOIN dbo.wallet w ON c.wallet = w.id " +
                    "WHERE c.id = @id", con);
                command.Parameters.AddWithValue("@id", c.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    c.Wallet = reader["wallet"] != DBNull.Value ? int.Parse(reader["wallet"].ToString()) : 0;
                    c.Titulo = reader["titulo"].ToString().Trim();
                    c.Descripcion = reader["descripcion"].ToString().Trim();
                    c.FechaIncursion = reader["fechaincursion"] != DBNull.Value ? DateTime.Parse(reader["fechaincursion"].ToString()) : DateTime.MinValue;
                    c.Prediccion = int.Parse(reader["prediccion"].ToString());
                    c.Voto = reader["voto"].ToString().Trim();
                    c.Saldo = reader["saldo"] != DBNull.Value ? decimal.Parse(reader["saldo"].ToString()) : 0;
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

        public List<ENComunidad> ReadAll()
        {
            SqlConnection con = new SqlConnection(conexion);
            List<ENComunidad> comunidades = new List<ENComunidad>();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT c.*, w.saldo FROM dbo.comunidad c " +
                    "LEFT JOIN dbo.wallet w ON c.wallet = w.id", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ENComunidad c = new ENComunidad();
                    c.Id = int.Parse(reader["id"].ToString());
                    c.Wallet = reader["wallet"] != DBNull.Value ? int.Parse(reader["wallet"].ToString()) : 0;
                    c.Titulo = reader["titulo"].ToString().Trim();
                    c.Descripcion = reader["descripcion"].ToString().Trim();
                    c.FechaIncursion = reader["fechaincursion"] != DBNull.Value ? DateTime.Parse(reader["fechaincursion"].ToString()) : DateTime.MinValue;
                    c.Prediccion = int.Parse(reader["prediccion"].ToString());
                    c.Voto = reader["voto"].ToString().Trim();
                    c.Saldo = reader["saldo"] != DBNull.Value ? decimal.Parse(reader["saldo"].ToString()) : 0;
                    comunidades.Add(c);
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
            return comunidades;
        }

        public List<ENComunidad> ReadPorPrediccion(int idPrediccion)
        {
            SqlConnection con = new SqlConnection(conexion);
            List<ENComunidad> comunidades = new List<ENComunidad>();

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT c.*, w.saldo FROM dbo.comunidad c " +
                    "LEFT JOIN dbo.wallet w ON c.wallet = w.id " +
                    "WHERE c.prediccion = @prediccion", con);
                command.Parameters.AddWithValue("@prediccion", idPrediccion);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENComunidad c = new ENComunidad();
                    c.Id = int.Parse(reader["id"].ToString());
                    c.Wallet = reader["wallet"] != DBNull.Value ? int.Parse(reader["wallet"].ToString()) : 0;
                    c.Titulo = reader["titulo"].ToString().Trim();
                    c.Descripcion = reader["descripcion"].ToString().Trim();
                    c.FechaIncursion = reader["fechaincursion"] != DBNull.Value ? DateTime.Parse(reader["fechaincursion"].ToString()) : DateTime.MinValue;
                    c.Prediccion = int.Parse(reader["prediccion"].ToString());
                    c.Voto = reader["voto"].ToString().Trim();
                    c.Saldo = reader["saldo"] != DBNull.Value ? decimal.Parse(reader["saldo"].ToString()) : 0;
                    comunidades.Add(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return null;
            }
            finally { con.Close(); }
            return comunidades;
        }

        public bool AportarSaldo(ENComunidad c, decimal cantidad)
        {
            SqlConnection con = new SqlConnection(conexion);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.wallet SET saldo = saldo + @cantidad WHERE id = " +
                    "(SELECT wallet FROM dbo.comunidad WHERE id = @id)", con);
                command.Parameters.AddWithValue("@cantidad", cantidad);
                command.Parameters.AddWithValue("@id", c.Id);

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
    }
}
