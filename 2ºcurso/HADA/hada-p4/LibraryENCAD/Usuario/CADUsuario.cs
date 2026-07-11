using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Usuario
{
    /// <summary>
    /// Clase de acceso a datos (CAD) encargada de gestionar las operaciones CRUD
    /// sobre la tabla Usuario en la base de datos.
    /// </summary>
    internal class CADUsuario
    {
        /// <summary>
        /// Cadena de conexión a la base de datos obtenida desde el archivo de configuración.
        /// </summary>
        private string conexion;

        /// <summary>
        /// Constructor que inicializa la cadena de conexión desde Web.config.
        /// </summary>
        public CADUsuario()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Genera un hash seguro de la contraseña utilizando PBKDF2 con una sal aleatoria.
        /// </summary>
        /// <param name="password">Contraseña en texto plano introducida por el usuario.</param>
        /// <param name="salt">Salt generado aleatoriamente y devuelto como parámetro de salida.</param>
        /// <returns>Hash de la contraseña codificado en Base64.</returns>
        private string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            salt = Convert.ToBase64String(saltBytes);

            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 50000);
            byte[] hash = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifica si una contraseña ingresada coincide con el hash almacenado.
        /// </summary>
        /// <param name="password">Contraseña en texto plano ingresada por el usuario.</param>
        /// <param name="storedHash">Hash almacenado en la base de datos.</param>
        /// <param name="storedSalt">Salt utilizado originalmente para generar el hash.</param>
        /// <returns>True si la contraseña coincide; false en caso contrario.</returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 50000);
            byte[] hash = pbkdf2.GetBytes(32);

            return (Convert.ToBase64String(hash) == storedHash);
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="u">Objeto ENUsuario con los datos del usuario.</param>
        /// <returns>True si la operación fue exitosa; false si ocurrió un error.</returns>
        public bool Create(ENUsuario u)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool correcto = true;
            try
            {
                c.Open();
                string passwordHash = HashPassword(u.password, out string salt);

                SqlCommand command = new SqlCommand(
                    "INSERT INTO dbo.Usuario (email, nombre, apellidos, telefono, nickname, password, salt, admin) " +
                    "VALUES (@email, @nombre, @apellidos, @telefono, @nickname, @password, @salt, 0)", c);

                command.Parameters.AddWithValue("@email", u.Email);
                command.Parameters.AddWithValue("@nombre", u.nombre);
                command.Parameters.AddWithValue("@apellidos", u.apellidos);
                command.Parameters.AddWithValue("@telefono", (object)u.telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@nickname", u.nickname);
                command.Parameters.AddWithValue("@password", passwordHash);
                command.Parameters.AddWithValue("@salt", salt);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            finally
            {
                c.Close();
            }

            return correcto;
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="u">Objeto ENUsuario con los datos actualizados.</param>
        /// <returns>True si la operación fue exitosa; false si ocurrió un error.</returns>
        public bool Update(ENUsuario u)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool correcto = true;
            try
            {
                c.Open();
                string passwordHash = HashPassword(u.password, out string salt);

                SqlCommand command = new SqlCommand(
                    "UPDATE dbo.Usuario SET nombre = @nombre, apellidos = @apellidos, telefono = @telefono, " +
                    "nickname = @nickname, password = @password, salt = @salt WHERE email = @email;", c);

                command.Parameters.AddWithValue("@email", u.Email);
                command.Parameters.AddWithValue("@nombre", u.nombre);
                command.Parameters.AddWithValue("@apellidos", u.apellidos);
                command.Parameters.AddWithValue("@telefono", (object)u.telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@nickname", u.nickname);
                command.Parameters.AddWithValue("@password", passwordHash);
                command.Parameters.AddWithValue("@salt", salt);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            finally
            {
                c.Close();
            }

            return correcto;
        }

        /// <summary>
        /// Elimina un usuario de la base de datos según su email.
        /// </summary>
        /// <param name="u">Objeto ENUsuario con el email del usuario a eliminar.</param>
        /// <returns>True si se eliminó algún registro; false si no existía.</returns>
        public bool Delete(ENUsuario u)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool correcto = true;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand("DELETE FROM dbo.Usuario WHERE email = @email", c);
                command.Parameters.AddWithValue("@email", u.Email);

                if (command.ExecuteNonQuery() == 0)
                    correcto = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            finally
            {
                c.Close();
            }

            return correcto;
        }

        /// <summary>
        /// Lee los datos de un usuario desde la base de datos.
        /// Primero intenta buscar por email; si no existe, busca por nickname.
        /// </summary>
        /// <param name="u">Objeto ENUsuario con email o nickname.</param>
        /// <returns>True si se encontró el usuario; false en caso contrario.</returns>
        public bool Read(ENUsuario u)
        {
            SqlConnection c = new SqlConnection(conexion);
            bool correcto = true;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Usuario WHERE email = @email", c);
                command.Parameters.AddWithValue("@email", u.Email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    u.nombre = reader["nombre"].ToString();
                    u.apellidos = reader["apellidos"].ToString();
                    u.nickname = reader["nickname"].ToString();
                    u.wallet = int.Parse(reader["wallet"].ToString());
                    u.password = reader["Password"].ToString();
                    u.salt = reader["salt"].ToString();
                    u.admin = Convert.ToBoolean(reader["admin"]);
                }
                else
                {
                    reader.Close();
                    command = new SqlCommand("SELECT * FROM dbo.Usuario WHERE nickname = @nickname", c);
                    command.Parameters.AddWithValue("@nickname", u.nickname);

                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        u.nombre = reader["nombre"].ToString();
                        u.apellidos = reader["apellidos"].ToString();
                        u.Email = reader["email"].ToString();
                        u.wallet = int.Parse(reader["wallet"].ToString());
                        u.password = reader["Password"].ToString();
                        u.salt = reader["salt"].ToString();
                        u.admin = Convert.ToBoolean(reader["admin"]);
                    }
                    else correcto = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            finally
            {
                c.Close();
            }

            return correcto;
        }

        /// <summary>
        /// Devuelve una lista de nicknames que coinciden parcialmente con el texto buscado.
        /// </summary>
        /// <param name="s">Texto parcial a buscar.</param>
        /// <returns>Lista de sugerencias de nickname.</returns>
        public List<string> ListaSugerenciasNickname(string s)
        {
            SqlConnection c = new SqlConnection(conexion);
            List<string> suggestions;

            if (s.Length == 0) return new List<string>();

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT nickname FROM dbo.Usuario WHERE LOWER(nickname) LIKE @sugerencia", c);

                command.Parameters.AddWithValue("@sugerencia", "%" + s.ToLower() + "%");

                SqlDataReader reader = command.ExecuteReader();
                suggestions = new List<string>();

                while (reader.Read())
                {
                    suggestions.Add(reader["nickname"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                suggestions = new List<string>() { "Error" };
            }
            finally
            {
                c.Close();
            }

            return suggestions;
        }

        /// <summary>
        /// Obtiene el saldo (wallet) del usuario desde la base de datos.
        /// </summary>
        /// <param name="user">Usuario del cual obtener el saldo.</param>
        /// <returns>Saldo del usuario.</returns>
        public double GetWallet(ENUsuario user)
        {
            SqlConnection c = new SqlConnection(conexion);
            double wallet = 0;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT wallet FROM dbo.Usuario WHERE email = @email", c);

                command.Parameters.AddWithValue("@email", user.Email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    wallet = double.Parse(reader["saldo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                c.Close();
            }

            return wallet;
        }

        /// <summary>
        /// Devuelve una lista con todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos ENUsuario.</returns>
        public List<ENUsuario> readAll()
        {
            SqlConnection c = new SqlConnection(conexion);
            List<ENUsuario> users;

            try
            {
                c.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Usuario", c);
                SqlDataReader reader = command.ExecuteReader();

                users = new List<ENUsuario>();

                while (reader.Read())
                {
                    ENUsuario u = new ENUsuario
                    {
                        Email = reader["email"].ToString(),
                        nombre = reader["nombre"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        nickname = reader["nickname"].ToString(),
                        wallet = int.Parse(reader["wallet"].ToString()),
                        password = reader["Password"].ToString(),
                        salt = reader["salt"].ToString(),
                        admin = Convert.ToBoolean(reader["admin"])
                    };

                    users.Add(u);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                users = new List<ENUsuario>();
            }
            finally
            {
                c.Close();
            }

            return users;
        }

        /// <summary>
        /// Obtiene un DataSet con información de usuarios ordenados por dinero y por gasto.
        /// </summary>
        /// <returns>DataSet con tablas "Usuario" y "Gasto".</returns>
        public DataSet getUserByMoney()
        {
            DataSet users = new DataSet();
            SqlConnection c = new SqlConnection(conexion);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT u.nickname, u.email, w.saldo FROM usuario u " +
                    "INNER JOIN wallet w ON u.wallet = w.id ORDER BY w.saldo DESC;", c);

                da.Fill(users, "Usuario");

                da = new SqlDataAdapter(
                    "SELECT u.email, u.nickname, d.divisa, SUM(t.cantidad) AS totalCantidad " +
                    "FROM usuario u INNER JOIN wallet w ON u.wallet = w.id " +
                    "INNER JOIN trandivisa d ON d.wallet = w.id " +
                    "INNER JOIN transaccion t ON t.id = d.id " +
                    "GROUP BY u.email, u.nickname, d.divisa " +
                    "ORDER BY totalCantidad DESC", c);

                da.Fill(users, "Gasto");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                c.Close();
            }

            return users;
        }
    }
}

