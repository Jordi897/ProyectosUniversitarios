using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Logro
{
    internal class ENLogro
    {
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private decimal? _recompensa;
        public decimal? Recompensa
        {
            get { return _recompensa; }
            set { _recompensa = value; }
        }

        public ENLogro()
        {

        }

        public bool Create()
        {
            CADLogro l = new CADLogro();
            return l.Create(this);
        }

        public bool Read()
            {
                CADLogro l = new CADLogro();
                return l.Read(this);
        }

        public bool Update()
        {
            CADLogro l = new CADLogro();
            return l.Update(this);
        }

        public bool Delete() { 
            CADLogro l = new CADLogro();
            return l.Delete(this);
        }

    }
}
