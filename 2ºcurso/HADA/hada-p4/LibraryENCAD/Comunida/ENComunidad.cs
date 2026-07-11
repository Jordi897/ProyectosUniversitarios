using LibraryEN_CAD.Predicción;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Comunida
{
    internal class ENComunidad
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        private int _voto;
        public int Voto
        {
            get { return _voto; }
            set { _voto = value; }
        }

        private string _fechaDeIncursion;
        public string FechaDeIncursion
        {
            get { return _fechaDeIncursion; }
            set { _fechaDeIncursion = value; }
        }

        public ENComunidad() { }

        public bool Create()
        {
            CADComunidad p = new CADComunidad();
            return p.Create(this);
        }

        public bool Update()
        {
            CADComunidad p = new CADComunidad();
            return p.Update(this);
        }

        public bool Delete()
        {
            CADComunidad p = new CADComunidad();
            return p.Delete(this);
        }

        public bool Read()
        {
            CADComunidad p = new CADComunidad();
            return p.Read(this);
        }
    }
}
