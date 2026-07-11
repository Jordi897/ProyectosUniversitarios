using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryEN_CAD.Notificacion
{
    public class ENNotificacion
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        private string _mensaje;
        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public ENNotificacion() { }

        public bool Create()
        {
            CADNotificacion n = new CADNotificacion();
            return n.Create(this);
        }

        public bool Read()
        {
            CADNotificacion n = new CADNotificacion();
            return n.Read(this);
        }

        public bool Update()
        {
            CADNotificacion n = new CADNotificacion();
            return n.Update(this);
        }

        public bool Delete()
        {
            CADNotificacion n = new CADNotificacion();
            return n.Delete(this);
        }


    }
}
