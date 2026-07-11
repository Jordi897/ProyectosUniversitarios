using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Comunidad
{
    public class ENComunidad
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _wallet;
        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
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

        private DateTime _fechaIncursion;
        public DateTime FechaIncursion
        {
            get { return _fechaIncursion; }
            set { _fechaIncursion = value; }
        }

        private int _prediccion;
        public int Prediccion
        {
            get { return _prediccion; }
            set { _prediccion = value; }
        }

        private string _voto;
        public string Voto
        {
            get { return _voto; }
            set { _voto = value; }
        }

        private decimal _saldo;
        public decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public ENComunidad() { }

        public bool Create()
        {
            CADComunidad cad = new CADComunidad();
            return cad.Create(this);
        }
        public bool Update()
        {
            CADComunidad cad = new CADComunidad();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADComunidad cad = new CADComunidad();
            return cad.Delete(this);
        }

        public bool Read()
        {
            CADComunidad cad = new CADComunidad();
            return cad.Read(this);
        }

        public List<ENComunidad> ReadAll()
        {
            CADComunidad cad = new CADComunidad();
            return cad.ReadAll();
        }

        public List<ENComunidad> ReadPorPrediccion(int idPrediccion)
        {
            CADComunidad cad = new CADComunidad();
            return cad.ReadPorPrediccion(idPrediccion);
        }

        public bool AportarSaldo(decimal cantidad)
        {
            CADComunidad cad = new CADComunidad();
            return cad.AportarSaldo(this, cantidad);
        }
    }
}
