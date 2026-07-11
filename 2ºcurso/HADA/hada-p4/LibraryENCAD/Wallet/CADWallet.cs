using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Wallet
{
    /// <summary>
    /// Clase de acceso a datos (CAD) encargada de gestionar las operaciones
    /// relacionadas con la tabla Wallet en la base de datos.
    /// Permite consultar, actualizar y eliminar wallets.
    /// </summary>
    public class CADWallet
    {
        /// <summary>
        /// Cadena de conexión a la base de datos obtenida desde el archivo de configuración.
        /// </summary>
        private string conexion;

        /// <summary>
        /// Constructor que inicializa la cadena de conexión desde Web.config.
        /// </summary>
        public CADWallet()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Obtiene el saldo actual de una wallet desde la base de datos.
        /// </summary>
        /// <param name="wallet">Objeto ENWallet con el ID de la wallet a consultar.</param>
        /// <returns>True si se obtuvo el saldo correctamente; false si ocurrió un error.</returns>
        public bool GetSaldo(ENWallet wallet)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool success = false;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT saldo FROM dbo.Wallet WHERE id = @id", c);

                command.Parameters.AddWithValue("@id", wallet.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    wallet.Saldo = reader.GetDecimal(0);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return success;
        }

        /// <summary>
        /// Obtiene el saldo retenido de una wallet desde la base de datos.
        /// </summary>
        /// <param name="wallet">Objeto ENWallet con el ID de la wallet a consultar.</param>
        /// <returns>True si se obtuvo el saldo retenido correctamente; false si ocurrió un error.</returns>
        public bool GetSaldoRetenido(ENWallet wallet)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool success = false;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT saldoRetenido FROM Wallet WHERE id = @id", c);

                command.Parameters.AddWithValue("@id", wallet.Id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    wallet.SaldoRetenido = reader.GetDecimal(0);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return success;
        }

        /// <summary>
        /// Actualiza el saldo disponible de una wallet en la base de datos.
        /// </summary>
        /// <param name="wallet">Wallet cuyo saldo se desea actualizar.</param>
        /// <returns>True si la actualización afectó al menos una fila; false en caso contrario.</returns>
        public bool UpdateSaldo(ENWallet wallet)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool success = false;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "UPDATE Wallet SET saldo = @saldo WHERE id = @id", c);

                command.Parameters.AddWithValue("@saldo", wallet.Saldo);
                command.Parameters.AddWithValue("@id", wallet.Id);

                int rowsAffected = command.ExecuteNonQuery();
                success = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return success;
        }

        /// <summary>
        /// Actualiza el saldo retenido de una wallet en la base de datos.
        /// </summary>
        /// <param name="wallet">Wallet cuyo saldo retenido se desea actualizar.</param>
        /// <returns>True si la actualización fue exitosa; false si no se modificó ninguna fila.</returns>
        public bool UpdateSaldoRetenido(ENWallet wallet)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool success = false;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "UPDATE Wallet SET saldoretenido = @saldoretenido WHERE id = @id", c);

                command.Parameters.AddWithValue("@saldoretenido", wallet.SaldoRetenido);
                command.Parameters.AddWithValue("@id", wallet.Id);

                int rowsAffected = command.ExecuteNonQuery();
                success = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return success;
        }

        /// <summary>
        /// Elimina una wallet de la base de datos según su ID.
        /// </summary>
        /// <param name="wallet">Objeto ENWallet con el ID de la wallet a eliminar.</param>
        /// <returns>True si se eliminó correctamente; false si no existía o no se pudo borrar.</returns>
        public bool Delete(ENWallet wallet)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool success = false;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "DELETE FROM Wallet WHERE id = @id", c);

                command.Parameters.AddWithValue("@id", wallet.Id);

                int rowsAffected = command.ExecuteNonQuery();
                success = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return success;
        }
    }
}

