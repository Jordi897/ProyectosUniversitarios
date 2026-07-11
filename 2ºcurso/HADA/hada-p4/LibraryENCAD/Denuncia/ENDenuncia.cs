using System;

namespace LibraryEN_CAD.Denuncia
{
    /// <summary>
    /// Representa una denuncia realizada por un usuario sobre un comentario.
    /// Esta clase actúa como entidad de negocio y delega las operaciones de
    /// acceso a datos en CADDenuncia.
    /// </summary>
    public class ENDenuncia
    {
        private int _id;

        /// <summary>
        /// Identificador único de la denuncia en la base de datos.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _causadenuncia;

        /// <summary>
        /// Motivo principal de la denuncia, por ejemplo spam o contenido inapropiado.
        /// </summary>
        public string CausaDenuncia
        {
            get { return _causadenuncia; }
            set { _causadenuncia = value; }
        }

        private string _descripcion;

        /// <summary>
        /// Descripción adicional introducida por el usuario al enviar la denuncia.
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private DateTime _fecha;

        /// <summary>
        /// Fecha en la que se creó la denuncia.
        /// </summary>
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _estado;

        /// <summary>
        /// Estado de la denuncia. En la base de datos se utilizan valores como
        /// PENDIENTE, ENPROCESO o RESUELTA.
        /// </summary>
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private string _emisor;

        /// <summary>
        /// Email del usuario que ha realizado la denuncia.
        /// Este valor se corresponde con la clave primaria de la tabla usuario.
        /// </summary>
        public string Emisor
        {
            get { return _emisor; }
            set { _emisor = value; }
        }

        private int _comentario;

        /// <summary>
        /// Identificador del comentario denunciado.
        /// </summary>
        public int Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Constructor por defecto. Inicializa la denuncia con estado PENDIENTE
        /// y valores seguros antes de ser rellenada desde la interfaz o la base de datos.
        /// </summary>
        public ENDenuncia()
        {
            _id = 0;
            _causadenuncia = "";
            _descripcion = "";
            _fecha = DateTime.Now;
            _estado = "PENDIENTE";
            _emisor = "";
            _comentario = 0;
        }

        /// <summary>
        /// Crea la denuncia actual en la base de datos.
        /// </summary>
        /// <returns>True si la denuncia se crea correctamente; false en caso contrario.</returns>
        public bool Create()
        {
            CADDenuncia d = new CADDenuncia();
            return d.Create(this);
        }

        /// <summary>
        /// Actualiza los datos de la denuncia actual en la base de datos.
        /// </summary>
        /// <returns>True si la actualización se realiza correctamente; false en caso contrario.</returns>
        public bool Update()
        {
            CADDenuncia d = new CADDenuncia();
            return d.Update(this);
        }

        /// <summary>
        /// Elimina de la base de datos la denuncia identificada por Id.
        /// </summary>
        /// <returns>True si la denuncia se elimina correctamente; false en caso contrario.</returns>
        public bool Delete()
        {
            CADDenuncia d = new CADDenuncia();
            return d.Delete(this);
        }

        /// <summary>
        /// Lee desde la base de datos los datos de la denuncia identificada por Id.
        /// </summary>
        /// <returns>True si la denuncia existe y se carga correctamente; false en caso contrario.</returns>
        public bool Read()
        {
            CADDenuncia d = new CADDenuncia();
            return d.Read(this);
        }

        /// <summary>
        /// Elimina todas las denuncias asociadas al comentario indicado en la propiedad Comentario.
        /// </summary>
        /// <returns>True si la operación se realiza correctamente; false en caso contrario.</returns>
        public bool DeletePorComentario()
        {
            CADDenuncia d = new CADDenuncia();
            return d.DeletePorComentario(this.Comentario);
        }

        /// <summary>
        /// Obtiene las denuncias pendientes junto con la información del comentario denunciado.
        /// </summary>
        /// <returns>DataSet con las denuncias pendientes.</returns>
        public System.Data.DataSet ListarPendientes()
        {
            CADDenuncia d = new CADDenuncia();
            return d.ListarPendientes();
        }

        /// <summary>
        /// Acepta la denuncia actual. Como consecuencia, se elimina el comentario denunciado
        /// y las denuncias asociadas a dicho comentario.
        /// </summary>
        /// <returns>True si la denuncia se acepta correctamente; false en caso contrario.</returns>
        public bool AceptarDenuncia()
        {
            CADDenuncia d = new CADDenuncia();
            return d.AceptarDenuncia(this);
        }

        /// <summary>
        /// Comprueba si el usuario emisor ya ha denunciado el comentario indicado.
        /// </summary>
        /// <returns>True si ya existe una denuncia del mismo usuario sobre el comentario; false en caso contrario.</returns>
        public bool ExisteDenuncia()
        {
            CADDenuncia d = new CADDenuncia();
            return d.ExisteDenuncia(this.Comentario, this.Emisor);
        }
    }
}