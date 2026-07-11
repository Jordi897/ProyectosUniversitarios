using LibraryEN_CAD.Prediccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Estadisticas
{
    internal class CADEstadisticas
    {
        /// <summary>
        /// String de conexión a la base de datos, se obtiene del app.config
        /// </summary>
        string connectionString;
        /// <summary>
        /// Constructor por defecto, obtiene el string de conexión del app.config
        /// </summary>
        public CADEstadisticas()
        {
            connectionString = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }
        /// <summary>
        /// Lee las estadísticas de un usuario específico, utilizando su email como identificador        /// </summary>
        /// <param name="estadisticas"></param>
        /// <returns></returns>
        public bool Read(ENEstadisticas estadisticas)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT * FROM dbo.estadisticasusuario WHERE usuario = @UsuarioEmail";
            SqlCommand command = new SqlCommand(sql, connection);
            if (estadisticas.Usuario == null) return false;
            command.Parameters.AddWithValue("@UsuarioEmail", estadisticas.Usuario.Email);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    estadisticas.MayorRacha = Convert.ToInt32(reader["mayorracha"]);
                    estadisticas.RachaActual = Convert.ToInt32(reader["rachaactual"]);
                    estadisticas.MaxPuntos = Convert.ToInt32(reader["maxpuntos"]);

                    estadisticas.PrediccionesGanadas = Convert.ToInt32(reader["prediccionesganadas"]);
                    estadisticas.PrediccionesPerdidas = Convert.ToInt32(reader["prediccionesperdidas"]);
                    result = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Actualiza las estadísticas de un usuario específico, utilizando su email como identificador
        /// </summary>
        /// <param name="estadisticas"></param>
        /// <returns></returns>
        public bool Update(ENEstadisticas estadisticas)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "UPDATE dbo.estadisticasusuario SET mayorracha = @MayorRacha, rachaactual = @RachaActual, maxpuntos = @MaxPuntos, prediccionesganadas = @PrediccionesGanadas, prediccionesperdidas = @PrediccionesPerdidas WHERE usuario = @UsuarioEmail";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UsuarioEmail", estadisticas.Usuario.Email);
            command.Parameters.AddWithValue("@MayorRacha", estadisticas.MayorRacha);
            command.Parameters.AddWithValue("@RachaActual", estadisticas.RachaActual);
            command.Parameters.AddWithValue("@MaxPuntos", estadisticas.MaxPuntos);
            command.Parameters.AddWithValue("@PrediccionesGanadas", estadisticas.PrediccionesGanadas);
            command.Parameters.AddWithValue("@PrediccionesPerdidas", estadisticas.PrediccionesPerdidas);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al actualizar las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        /// <summary>
        /// Crea un nuevo registro de estadísticas para un usuario específico
        /// </summary>
        /// <param name="estadisticas"></param>
        /// <returns></returns>
        public bool Write(ENEstadisticas estadisticas)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "INSERT INTO dbo.estadisticasusuario (usuario, mayorracha, rachaactual, maxpuntos, prediccionesganadas, prediccionesperdidas) VALUES (@UsuarioEmail, @MayorRacha, @RachaActual, @MaxPuntos, @PrediccionesGanadas, @PrediccionesPerdidas)";
            SqlCommand command = new SqlCommand(sql, connection);
            if (estadisticas.Usuario == null) return false;
            command.Parameters.AddWithValue("@UsuarioEmail", estadisticas.Usuario.Email);
            command.Parameters.AddWithValue("@MayorRacha", estadisticas.MayorRacha);
            command.Parameters.AddWithValue("@RachaActual", estadisticas.RachaActual);
            command.Parameters.AddWithValue("@MaxPuntos", estadisticas.MaxPuntos);
            command.Parameters.AddWithValue("@PrediccionesGanadas", estadisticas.PrediccionesGanadas);
            command.Parameters.AddWithValue("@PrediccionesPerdidas", estadisticas.PrediccionesPerdidas);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al escribir las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Borra las estadisticas de un usuario específico, utilizando su email como identificador
        /// </summary>
        /// <param name="estadisticas"></param>
        /// <returns></returns>
        public bool Delete(ENEstadisticas estadisticas)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "DELETE FROM dbo.estadisticasusuario WHERE usuario = @UsuarioEmail";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UsuarioEmail", estadisticas.Usuario.Email);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al eliminar las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Lee todas las estadisticas de las personas que han ganado la prediccion
        /// </summary>
        /// <param name="prediccion"></param>
        /// <param name="veredicto"></param>
        /// <returns></returns>
        public List<ENEstadisticas> ReadAllGanada(ENPrediccion prediccion, string veredicto)
        {
            List<ENEstadisticas> estadisticasList = new List<ENEstadisticas>();
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT email FROM dbo.tranaapuesta t, dbo.usuario u where t.wallet=u.wallet and t.voto = @Veredicto and t.prediccion = @Prediccion";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Veredicto", veredicto);
            command.Parameters.AddWithValue("@Prediccion", prediccion.Id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENEstadisticas estadisticas = new ENEstadisticas();
                    estadisticas.Usuario.Email = reader["email"].ToString();
                    estadisticasList.Add(estadisticas);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer todas las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return estadisticasList;
        }
        /// <summary>
        /// Lee todas las estadisticas de las personas que han perdido la prediccion
        /// </summary>
        /// <param name="prediccion"></param>
        /// <param name="veredicto"></param>
        /// <returns></returns>
        public List<ENEstadisticas> ReadAllPerdida(ENPrediccion prediccion, string veredicto)
        {
            List<ENEstadisticas> estadisticasList = new List<ENEstadisticas>();
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT email FROM dbo.tranaapuesta t, dbo.usuario u where t.wallet=u.wallet and t.voto != @Veredicto and t.prediccion = @Prediccion";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Veredicto", veredicto);
            command.Parameters.AddWithValue("@Prediccion", prediccion.Id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ENEstadisticas estadisticas = new ENEstadisticas();
                    estadisticas.Usuario.Email = reader["email"].ToString();
                    estadisticasList.Add(estadisticas);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error al leer todas las estadísticas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return estadisticasList;
        }
    }
}
