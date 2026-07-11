using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Notificacion 
{
    public class ENNotificacionUsuario
    {
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private int notificacion;
        public int Notificacion
        {
            get { return notificacion; }
            set { notificacion = value; }
        }

        private bool _leido;
        public bool Leido
        {
            get { return _leido; }
            set { _leido = value; }
        }

        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public ENNotificacionUsuario()
        {
        }

        public bool Create()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.Create(this);
        }

        public bool Read()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.Read(this);
        }

        public bool Update()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.Update(this);
        }

        public bool Delete()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.Delete(this);
        }
        public int ContarNotificacionesNoLeidas()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.ContarNotificacionesNoLeidas(this.Usuario);
        }

        public bool MarcarComoLeida()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.MarcarComoLeida(this.Usuario, this.Notificacion);
        }

        public bool MarcarTodasComoLeidas()
        {
            CADNotificacionUsuario n = new CADNotificacionUsuario();
            return n.MarcarTodasComoLeidas(this.Usuario);
        }
    }
}