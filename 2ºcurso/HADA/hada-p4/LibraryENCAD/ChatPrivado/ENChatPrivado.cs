using LibraryEN_CAD.Mensaje;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.ChatPrivado
{
    public class ENChatPrivado
    {
        /// <summary>
        /// Todas las variables necesarias para poder 
        /// almacenar el chat en la base de datos
        /// </summary>
        private ENUsuario _usuario1;
        public ENUsuario Usuario1
        {
            get { return _usuario1; }
            set { _usuario1 = value; }
        }
        private ENUsuario _usuario2;
        public ENUsuario Usuario2
        {
            get { return _usuario2; }
            set { _usuario2 = value; }
        }
        private DateTime _fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ENChatPrivado()
        {
            Usuario1 = new ENUsuario();
            Usuario2 = new ENUsuario();
            FechaCreacion = DateTime.Now;

        }
        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="usuario1"></param>
        /// <param name="usuario2"></param>
        /// <param name="fechaCreacion"></param>
        public ENChatPrivado(ENUsuario usuario1, ENUsuario usuario2, DateTime fechaCreacion)
        {
            Usuario1 = usuario1;
            Usuario2 = usuario2;
            FechaCreacion = fechaCreacion;

        }
        /// <summary>
        /// Invoca al read de la clase cad que lee el chat 
        /// a partir de los 2 usuarios
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            CADChatPrivado chat = new CADChatPrivado();
            return chat.Read(this);

        }
        /// <summary>
        /// Crea un nuevo chat a partir de los 2 usuarios y 
        /// la fecha de creacion, invocando al write de la clase cad
        /// </summary>
        /// <returns></returns>
        public bool Write()
        {
            CADChatPrivado chat = new CADChatPrivado();
            return chat.Write(this);
        }
        /// <summary>
        /// Borra el chat a partir de los 2 usuarios, 
        /// invocando al delete de la clase cad
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            CADChatPrivado chat = new CADChatPrivado();
            chat.VaciarChat(this); // Primero se vacía el chat para borrar todos los mensajes asociados a ese chat
            return chat.Delete(this);
        }
        /// <summary>
        /// Borra todos los mensajes del chat a partir de los 2 usuarios,
        /// invocando al método correspondiente de la clase CAD
        /// </summary>
        /// <returns></returns>
        public bool VaciarChat()
        {
            CADChatPrivado chat = new CADChatPrivado();
            return chat.VaciarChat(this);
        }
        /// <summary>
        /// Devuelve una lista con todos los mensajes del chat a partir de los 2 usuarios,
        /// invocando al método correspondiente de la clase CAD
        /// </summary>
        /// <returns></returns>
        public List<ENMensaje> ReadMensajes()
        {
            CADChatPrivado chat = new CADChatPrivado();
            return chat.ReadMensajes(this);
        }
        public List<ENChatPrivado> ChatsUsuario()
        {
            CADChatPrivado chat = new CADChatPrivado();
            return chat.ChatsUsuario(this);
        }
        // No se implementa el update porque no tiene sentido actualizar un chat privado
    }
}
