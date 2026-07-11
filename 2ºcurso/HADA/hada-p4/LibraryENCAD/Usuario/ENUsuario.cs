using LibraryEN_CAD.Wallet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Usuario
{
    /// <summary>
    /// Representa un usuario del sistema y proporciona operaciones CRUD
    /// (crear, leer, actualizar, eliminar) mediante la capa CADUsuario.
    /// Hereda de la clase Busqueda.
    /// </summary>
    public class ENUsuario : Busqueda
    {
        // ===========================
        //        PROPIEDADES
        // ===========================

        private string _email;

        /// <summary>
        /// Correo electrónico del usuario.
        /// Se almacena siempre como cadena no nula.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value ?? ""; }
        }

        private string _password;

        /// <summary>
        /// Contraseña del usuario.
        /// Puede contener el hash o la contraseña en texto plano antes de ser procesada.
        /// </summary>
        public string password
        {
            get { return _password; }
            set { _password = value ?? ""; }
        }

        private string _nombre;

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value ?? ""; }
        }

        private string _apellidos;

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value ?? ""; }
        }

        private string _telefono;

        /// <summary>
        /// Número de teléfono del usuario.
        /// Si se asigna una cadena vacía, se almacena como null.
        /// </summary>
        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value == "" ? null : value; }
        }

        private string _nickname;

        /// <summary>
        /// Apodo o nombre público del usuario.
        /// </summary>
        public string nickname
        {
            get { return _nickname; }
            set { _nickname = value ?? ""; }
        }

        private int _wallet;

        /// <summary>
        /// Identificador del wallet asociado al usuario.
        /// No es el saldo, sino el ID del wallet en la base de datos.
        /// </summary>
        public int wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        private bool _admin;

        /// <summary>
        /// Indica si el usuario tiene permisos de administrador.
        /// </summary>
        public bool admin
        {
            get { return _admin; }
            set { _admin = value; }
        }

        private string _salt;

        /// <summary>
        /// Sal utilizada para generar el hash de la contraseña.
        /// </summary>
        public string salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        private DataSet _info;

        /// <summary>
        /// Constructor por defecto de la clase ENUsuario.
        /// Inicializa valores básicos y evita nulos.
        /// </summary>
        public ENUsuario()
        {
            _admin = false;
            _email = "";
            nombre = "";
            apellidos = "";
            telefono = null;
            nickname = "";
        }

        // ===========================
        //        MÉTODOS CRUD
        // ===========================

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool Create()
        {
            CADUsuario u = new CADUsuario();
            return u.Create(this);
        }

        /// <summary>
        /// Actualiza los datos del usuario en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool Update()
        {
            CADUsuario u = new CADUsuario();
            return u.Update(this);
        }

        /// <summary>
        /// Elimina el usuario de la base de datos y su wallet asociado.
        /// </summary>
        /// <returns>True si ambas operaciones fueron exitosas.</returns>
        public bool Delete()
        {
            CADUsuario u = new CADUsuario();
            ENWallet wallet = new ENWallet();
            wallet.Id = this.wallet;
            return u.Delete(this) && wallet.Delete();
        }

        /// <summary>
        /// Lee los datos del usuario desde la base de datos.
        /// Puede buscar por email o por nickname.
        /// </summary>
        /// <returns>True si se encontró el usuario.</returns>
        public bool Read()
        {
            CADUsuario u = new CADUsuario();
            return u.Read(this);
        }

        /// <summary>
        /// Obtiene una lista de sugerencias de nicknames según un texto de búsqueda.
        /// </summary>
        /// <param name="query">Texto parcial introducido por el usuario.</param>
        /// <returns>Lista de sugerencias de nicknames.</returns>
        public List<string> Sugerencias(string query)
        {
            CADUsuario u = new CADUsuario();
            return u.ListaSugerenciasNickname(query);
        }

        /// <summary>
        /// Verifica si una contraseña introducida coincide con la almacenada.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>True si la contraseña es correcta.</returns>
        public bool VerifyPassword(string password)
        {
            return CADUsuario.VerifyPassword(password, this.password, this.salt);
        }

        /// <summary>
        /// Obtiene el saldo actual del wallet del usuario.
        /// </summary>
        /// <returns>Saldo del wallet.</returns>
        /// <exception cref="Exception">Si no se puede obtener el saldo.</exception>
        public decimal GetSaldo()
        {
            this.Read();
            ENWallet wallet = new ENWallet();
            wallet.Id = this.wallet;

            if (!wallet.GetSaldo())
                throw new Exception("Error al obtener el saldo del wallet.");

            return wallet.Saldo;
        }

        /// <summary>
        /// Obtiene una lista con todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public List<ENUsuario> ReadAll()
        {
            CADUsuario u = new CADUsuario();
            return u.readAll();
        }

        /// <summary>
        /// Carga un DataSet con información de usuarios ordenados por dinero y gasto.
        /// </summary>
        /// <returns>True si se cargaron datos correctamente.</returns>
        public bool CreateDataSETUserByMoney()
        {
            CADUsuario u = new CADUsuario();
            _info = u.getUserByMoney();
            return _info != null && _info.Tables.Count > 0;
        }

        /// <summary>
        /// Indica si el DataSet interno contiene datos.
        /// </summary>
        public bool HaveData()
        {
            return _info != null && _info.Tables.Count > 0;
        }

        /// <summary>
        /// Elimina el DataSet interno de memoria.
        /// </summary>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool DeleteData()
        {
            try
            {
                _info.Clear();
                _info = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el DataSet: " + ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Obtiene una tabla del DataSet interno por nombre.
        /// </summary>
        /// <param name="tableName">Nombre de la tabla.</param>
        /// <returns>DataTable o null si no existe.</returns>
        public DataTable GetData(string tableName)
        {
            if (_info != null && _info.Tables.Contains(tableName))
                return _info.Tables[tableName];

            return null;
        }

        /// <summary>
        /// Obtiene una tabla del DataSet interno limitada a un número de filas.
        /// </summary>
        /// <param name="tableName">Nombre de la tabla.</param>
        /// <param name="numberFilas">Número máximo de filas.</param>
        /// <returns>DataTable con las filas solicitadas o null si no existe.</returns>
        public DataTable GetData(string tableName, int numberFilas)
        {
            if (_info != null && _info.Tables.Contains(tableName))
            {
                return _info.Tables[tableName]
                    .AsEnumerable()
                    .Take(numberFilas)
                    .CopyToDataTable();
            }

            return null;
        }
    }
}

