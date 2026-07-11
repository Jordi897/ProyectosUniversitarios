using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Notificacion
{
    internal class ENNotiPrediccion
    {
        private int _notificacion;
        public int Notificacion
        {
            get { return _notificacion; }
            set { _notificacion = value; }
        }
        private int _prediccion;
        public int Prediccion
        {
            get { return _prediccion; }
            set { _prediccion = value; }
        }


        public ENNotiPrediccion()
        {
        }


        public bool Create()
        {
            CADNotiPrediccion n = new CADNotiPrediccion();
            return n.Create(this);
        }

        public bool Read()
        {
            CADNotiPrediccion n = new CADNotiPrediccion();
            return n.Read(this);
        }

        public bool Update()
        {
            CADNotiPrediccion n = new CADNotiPrediccion();
            return n.Update(this);
        }

        public bool Delete()
        {
            CADNotiPrediccion n = new CADNotiPrediccion();
            return n.Delete(this);
        }


    }
}