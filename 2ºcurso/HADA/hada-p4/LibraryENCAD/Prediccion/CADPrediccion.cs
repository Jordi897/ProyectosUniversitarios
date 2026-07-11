using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryEN_CAD.Transaccion;
using LibraryEN_CAD.Wallet;
using LibraryEN_CAD.Comunidad;

namespace LibraryEN_CAD.Prediccion
{
    public class CADPrediccion
    {
        private string conexion;

        public CADPrediccion()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENPrediccion p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Prediccion (titulo, prediccion, cantidadrecaudada, categoria, estado, votossi, votosno, fechafin, creador)" +
                    "VALUES (@titulo, @prediccion, @cantidadrecaudada, @categoria, @estado, @votossi, @votosno, @fechafin, @creador)", con);
                command.Parameters.AddWithValue("@titulo", p.Titulo);
                command.Parameters.AddWithValue("@prediccion", p.Prediccion);
                command.Parameters.AddWithValue("@cantidadrecaudada", p.CantidadRecaudada);
                command.Parameters.AddWithValue("@categoria", p.Categoria);
                command.Parameters.AddWithValue("@estado", p.Estado);
                command.Parameters.AddWithValue("@votossi", p.VotosSi);
                command.Parameters.AddWithValue("@votosno", p.VotosNo);
                command.Parameters.AddWithValue("@fechafin", p.FechaFin);
                command.Parameters.AddWithValue("@creador", (object)p.Creador ?? DBNull.Value);

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

        public bool Update(ENPrediccion p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.Prediccion SET titulo = @titulo, prediccion = @prediccion, cantidadrecaudada = @cantidadrecaudada, categoria = @categoria, estado = @estado, votossi = @votossi, votosno = @votosno, fechafin = @fechafin, creador = @creador " +
                    "WHERE id = @Id;", con);
                command.Parameters.AddWithValue("@Id", p.Id);
                command.Parameters.AddWithValue("@titulo", p.Titulo);
                command.Parameters.AddWithValue("@prediccion", p.Prediccion);
                command.Parameters.AddWithValue("@cantidadrecaudada", p.CantidadRecaudada);
                command.Parameters.AddWithValue("@categoria", p.Categoria);
                command.Parameters.AddWithValue("@estado", p.Estado);
                command.Parameters.AddWithValue("@votossi", p.VotosSi);
                command.Parameters.AddWithValue("@votosno", p.VotosNo);
                command.Parameters.AddWithValue("@fechafin", p.FechaFin);
                command.Parameters.AddWithValue("@creador", (object)p.Creador ?? DBNull.Value);

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

        public bool Delete(ENPrediccion p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM dbo.Prediccion WHERE id = @Id", con);
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

        public bool Read(ENPrediccion p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Prediccion WHERE id = @Id", con);
                command.Parameters.AddWithValue("@Id", p.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    p.Titulo = reader["titulo"].ToString();
                    p.Prediccion = reader["prediccion"].ToString();
                    p.CantidadRecaudada = decimal.Parse(reader["cantidadrecaudada"].ToString());
                    p.Categoria = reader["categoria"].ToString();
                    p.Estado = reader["estado"].ToString();
                    p.VotosSi = decimal.Parse(reader["votossi"].ToString());
                    p.VotosNo = decimal.Parse(reader["votosno"].ToString());
                    p.FechaFin = DateTime.Parse(reader["fechafin"].ToString());
                    p.Creador = reader["creador"] == DBNull.Value ? null : reader["creador"].ToString();
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

        public bool ReadPorTitulo(ENPrediccion p)
        {
            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Prediccion WHERE titulo = @Titulo", con);
                command.Parameters.AddWithValue("@Titulo", p.Titulo);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    p.Id = int.Parse(reader["id"].ToString());
                    p.Prediccion = reader["prediccion"].ToString();
                    p.CantidadRecaudada = decimal.Parse(reader["cantidadrecaudada"].ToString());
                    p.Categoria = reader["categoria"].ToString();
                    p.Estado = reader["estado"].ToString();
                    p.VotosSi = decimal.Parse(reader["votossi"].ToString());
                    p.VotosNo = decimal.Parse(reader["votosno"].ToString());
                    p.FechaFin = DateTime.Parse(reader["fechafin"].ToString());
                    p.Creador = reader["creador"] == DBNull.Value ? null : reader["creador"].ToString();
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

        public List<ENPrediccion> ReadAll()
        {
            SqlConnection con = new SqlConnection(conexion);
            List<ENPrediccion> predicciones = new List<ENPrediccion>();

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Prediccion", con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENPrediccion p = new ENPrediccion();
                    p.Id = int.Parse(reader["id"].ToString());
                    p.Titulo = reader["titulo"].ToString();
                    p.Prediccion = reader["prediccion"].ToString();
                    p.CantidadRecaudada = decimal.Parse(reader["cantidadrecaudada"].ToString());
                    p.Categoria = reader["categoria"].ToString();
                    p.Estado = reader["estado"].ToString();
                    p.VotosSi = decimal.Parse(reader["votosSi"].ToString());
                    p.VotosNo = decimal.Parse(reader["votosNo"].ToString());
                    p.FechaFin = DateTime.Parse(reader["fechafin"].ToString());
                    p.Creador = reader["creador"] == DBNull.Value ? null : reader["creador"].ToString();
                    predicciones.Add(p);
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
            return predicciones;
        }

        public bool ResolverPrediccion(ENPrediccion p, string lado)
        {
            string ganador = lado;
            if (ganador != "SI" && ganador != "NO")
            {
                return false;
            }

            SqlConnection con = new SqlConnection(conexion);
            SqlTransaction transaccion = null;

            try
            {
                con.Open();
                transaccion = con.BeginTransaction();
                SqlCommand cmdApuestas = new SqlCommand("SELECT ta.wallet, ta.voto, t.cantidad " +
                    "FROM dbo.tranaapuesta ta " +
                    "JOIN dbo.transaccion t ON ta.id = t.id " +
                    "WHERE ta.prediccion = @prediccion", con, transaccion);
                cmdApuestas.Parameters.AddWithValue("@prediccion", p.Id);

                var apuestas = new List<(int wallet, string voto, decimal cantidad)>();
                decimal totalBote = 0;
                decimal totalGanadores = 0;

                SqlDataReader reader = cmdApuestas.ExecuteReader();
                while (reader.Read())
                {
                    int wallet = reader.GetInt32(0);
                    string voto = reader.GetString(1).Trim();
                    decimal cantidad = reader.GetDecimal(2);
                    apuestas.Add((wallet, voto, cantidad));
                    totalBote += cantidad;
                    if (voto == ganador)
                    {
                        totalGanadores += cantidad;
                    }
                }
                reader.Close();

                if (totalGanadores > 0)
                {
                    foreach (var apuesta in apuestas.FindAll(a => a.voto == ganador))
                    {
                        decimal premio = (apuesta.cantidad / totalGanadores) * totalBote;

                        SqlCommand cmdTran = new SqlCommand("INSERT INTO dbo.transaccion (cantidad, fecha) VALUES (@cantidad, @fecha); " +
                            "SELECT SCOPE_IDENTITY();", con, transaccion);
                        cmdTran.Parameters.AddWithValue("@cantidad", premio);
                        cmdTran.Parameters.AddWithValue("@fecha", DateTime.Now);
                        int tranId = Convert.ToInt32(cmdTran.ExecuteScalar());

                        SqlCommand cmdTranGana = new SqlCommand("INSERT INTO dbo.trangana (id, wallet, prediccion) " +
                            "VALUES (@id, @wallet, @prediccion)", con, transaccion);
                        cmdTranGana.Parameters.AddWithValue("@id", tranId);
                        cmdTranGana.Parameters.AddWithValue("@wallet", apuesta.wallet);
                        cmdTranGana.Parameters.AddWithValue("@prediccion", p.Id);
                        cmdTranGana.ExecuteNonQuery();

                        SqlCommand cmdWallet = new SqlCommand("UPDATE dbo.wallet SET saldo = saldo + @premio WHERE id = @wallet",
                            con, transaccion);
                        cmdWallet.Parameters.AddWithValue("@premio", premio);
                        cmdWallet.Parameters.AddWithValue("@wallet", apuesta.wallet);
                        cmdWallet.ExecuteNonQuery();
                    }
                }

                SqlCommand cmd;

                cmd = new SqlCommand("DELETE FROM dbo.tranaapuesta WHERE prediccion = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM dbo.tag WHERE prediccion = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM dbo.notiprediccion WHERE prediccion = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM dbo.comentario WHERE prediccion = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM dbo.comunidad WHERE prediccion = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM dbo.prediccion WHERE id = @id", con, transaccion);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR ResolverPrediccion: " + ex.Message);
                transaccion?.Rollback();
                con.Close();
                return false;
            }
            finally { con.Close(); }
            return true;
        }

        public List<string> ListaSugerencias(string query)
        {
            SqlConnection con = new SqlConnection(conexion);
            List<string> result = new List<string>();
            if (query.Length == 0) return result;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT titulo FROM dbo.prediccion WHERE LOWER(titulo) LIKE @sugerencia", con);
                command.Parameters.AddWithValue("@sugerencia", "%" + query + "%");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["titulo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return result;
        }
    }
}
