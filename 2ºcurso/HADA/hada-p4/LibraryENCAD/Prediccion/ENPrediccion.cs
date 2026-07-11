using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Prediccion
{
    public class ENPrediccion: Busqueda
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

        private string _prediccion;
        public string Prediccion
        {
            get { return _prediccion; }
            set { _prediccion = value; }
        }

        private decimal _cantidadrecaudada;
        public decimal CantidadRecaudada
        {
            get { return _cantidadrecaudada; }
            set { _cantidadrecaudada = value; }
        }

        private string _estado;
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private string _categoria;
        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        private string _creador;
        public string Creador
        {
            get { return _creador; }
            set { _creador = value; }
        }

        private decimal _votossi;
        public decimal VotosSi
        {
            get { return _votossi; }
            set { _votossi = value; }
        }

        private decimal _votosno;
        public decimal VotosNo
        {
            get { return _votosno; }
            set { _votosno = value; }
        }

        private DateTime _fechafin;
        public DateTime FechaFin
        {
            get { return _fechafin; }
            set { _fechafin = value; }
        }

        public ENPrediccion() { }

        public bool Create()
        {
            CADPrediccion p = new CADPrediccion();
            return p.Create(this);
        }

        public bool Update()
        {
            CADPrediccion p = new CADPrediccion();
            return p.Update(this);
        }

        public bool Delete()
        {
            CADPrediccion p = new CADPrediccion();
            return p.Delete(this);
        }

        public bool Read()
        {
            CADPrediccion p = new CADPrediccion();
            return p.Read(this);
        }

        public bool ReadPorTitulo()
        {
            CADPrediccion p = new CADPrediccion();
            return p.ReadPorTitulo(this);
        }

        public List<ENPrediccion> ReadAll()
        {
            CADPrediccion p = new CADPrediccion();
            return p.ReadAll();
        }

        public bool ResolverPrediccion(string lado)
        {
            CADPrediccion p = new CADPrediccion();
            return p.ResolverPrediccion(this, lado);
        }

        public List<string> Sugerencias(string query)
        {
            CADPrediccion p = new CADPrediccion();
            return p.ListaSugerencias(query);
        }
    }
}
