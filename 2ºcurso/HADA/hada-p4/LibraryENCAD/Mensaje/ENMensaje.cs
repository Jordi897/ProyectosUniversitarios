using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Mensaje
{
    public class ENMensaje
    {
        /// <summary>
        /// Todas las variables necesarias para poder 
        /// almacenar el mensaje en la base de datos
        /// </summary>
        private ENUsuario _emisor;
        public ENUsuario Emisor
        {  
            get { return _emisor; }
            set { _emisor = value; }
        }
        private ENUsuario _chat1;
        public ENUsuario Chat1
        {
            get { return _chat1; }
            set { _chat1 = value; }
        }
        private ENUsuario _chat2;
        public ENUsuario Chat2
        {
            get { return _chat2; }
            set { _chat2 = value; }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _contenido;
        public string Contenido
        {
            get { return _contenido; }
            set { _contenido = value; }
        }
        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        /// <summary>
        /// Mete los valores por defecto
        /// </summary>
        public ENMensaje()
        {
            _emisor = new ENUsuario();
            _chat1 = new ENUsuario();
            _chat2 = new ENUsuario();
            _id = 0;
            _contenido = string.Empty;
            _fecha = DateTime.Now;
        }
        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="enviador"></param>
        /// <param name="chat1"></param>
        /// <param name="chat2"></param>
        /// <param name="Id"></param>
        /// <param name="contenido"></param>
        /// <param name="fecha"></param>
        public ENMensaje(ENUsuario enviador, ENUsuario chat1, ENUsuario chat2, int Id, string contenido, DateTime fecha)
        {
            _emisor = enviador;
            _chat1 = chat1;
            _chat2 = chat2;
            _id = Id;
            _contenido = contenido;
            _fecha = fecha;
        }
        /// <summary>
        /// Almacena el mensaje en la base de datos, 
        /// devuelve true si se ha podido almacenar y false si no
        /// </summary>
        /// <returns></returns>
        public bool Write()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.Write(this);
        }
        /// <summary>
        /// Lee el mensaje de la base de datos, 
        /// devuelve true si se ha podido leer y false si no
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.Read(this);
        }
        public bool ReadLastChat()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.ReadLastChat(this);
        }
        public bool ReadNextChat()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.ReadNextChat(this);
        }
        /// <summary>
        /// Borra el mensaje de la base de datos
        /// y devuelve true si se ha podido borrar y false si no
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.Delete(this);
        }
        /// <summary>
        /// Actualiza el mensaje en la base de datos,
        /// para poder añadir la funcion de poder editarlo
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            CADMensaje cadMensaje = new CADMensaje();
            return cadMensaje.Update(this);
        }
    }
}
