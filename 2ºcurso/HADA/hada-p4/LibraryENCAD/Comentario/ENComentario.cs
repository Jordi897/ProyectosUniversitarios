using System;

namespace LibraryEN_CAD.Comentario
{
    /// <summary>
    /// Representa un comentario realizado por un usuario sobre una predicción.
    /// Esta clase actúa como entidad de negocio y delega las operaciones de
    /// acceso a datos en CADComentario.
    /// </summary>
    public class ENComentario
    {
        private int _id;

        /// <summary>
        /// Identificador único del comentario en la base de datos.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _mensaje;

        /// <summary>
        /// Texto del comentario escrito por el usuario.
        /// </summary>
        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        private DateTime _fecha;

        /// <summary>
        /// Fecha y hora en la que se creó el comentario.
        /// </summary>
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _usuario;

        /// <summary>
        /// Email del usuario que ha escrito el comentario.
        /// Se usa como clave foránea hacia la tabla usuario, aunque en la interfaz pueda mostrarse el nickname.
        /// </summary>
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private int _prediccion;

        /// <summary>
        /// Identificador de la predicción a la que pertenece el comentario.
        /// </summary>
        public int Prediccion
        {
            get { return _prediccion; }
            set { _prediccion = value; }
        }

        /// <summary>
        /// Constructor por defecto. Inicializa el comentario con valores seguros
        /// antes de ser rellenado desde la interfaz o desde la base de datos.
        /// </summary>
        public ENComentario()
        {
            _id = 0;
            _mensaje = "";
            _fecha = DateTime.Now;
            _usuario = "";
            _prediccion = 0;
        }

        /// <summary>
        /// Crea el comentario actual en la base de datos.
        /// </summary>
        /// <returns>True si el comentario se crea correctamente; false en caso contrario.</returns>
        public bool Create()
        {
            CADComentario c = new CADComentario();
            return c.Create(this);
        }

        /// <summary>
        /// Lee desde la base de datos los datos del comentario identificado por Id.
        /// </summary>
        /// <returns>True si el comentario existe y se carga correctamente; false en caso contrario.</returns>
        public bool Read()
        {
            CADComentario c = new CADComentario();
            return c.Read(this);
        }

        /// <summary>
        /// Actualiza en la base de datos los datos del comentario actual.
        /// </summary>
        /// <returns>True si la actualización se realiza correctamente; false en caso contrario.</returns>
        public bool Update()
        {
            CADComentario c = new CADComentario();
            return c.Update(this);
        }

        /// <summary>
        /// Elimina de la base de datos el comentario identificado por Id.
        /// </summary>
        /// <returns>True si el comentario se elimina correctamente; false en caso contrario.</returns>
        public bool Delete()
        {
            CADComentario c = new CADComentario();
            return c.Delete(this);
        }

        /// <summary>
        /// Obtiene todos los comentarios asociados a la predicción indicada
        /// en la propiedad Prediccion.
        /// </summary>
        /// <returns>DataSet con los comentarios de la predicción.</returns>
        public System.Data.DataSet ListarPorPrediccion()
        {
            CADComentario c = new CADComentario();
            return c.ListarPorPrediccion(this.Prediccion);
        }
    }
}