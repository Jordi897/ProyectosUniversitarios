using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Transaccion
{

    /// <summary>
    /// Esta clase representa el
    /// EN de TransaccionApuesta
    /// </summary>
    public class ENTransaccionApuesta : ENTransaccion
    {

        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa el voto de la
        /// "TransacciónApuesta"
        /// </summary>
        private string _voto;
        public string Voto
        {
            get { return _voto; }
            set { _voto = value; }
        }


        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa la predicción  
        /// de la "TransacciónApuesta"
        /// </summary>
        private int _prediccion;
        public int Prediccion
        {
            get { return _prediccion; }
            set { _prediccion = value; }
        }
        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa la wallet del usuario  
        /// de la "TransacciónDivisa"
        /// </summary>
        private int _wallet;
        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ENTransaccionApuesta() { }


        /// <summary>
        /// Este método llama
        /// a Create de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Verdadero si se crea,falso si no
        /// </returns>
        public override bool Create()
        {
            if(!base.Create()) return false;
            CADTransaccionApuesta t = new CADTransaccionApuesta();
            return t.Create(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Read de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public override bool Read()
        {
            if(!base.Read()) return false;
            CADTransaccionApuesta t = new CADTransaccionApuesta();
            return t.Read(this);
        }

        /// <summary>
        /// Este método llama a
        /// Update de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Verdadero si actualiza,falso si no
        /// </returns>
        public override bool Update()
        {
            if(!base.Update()) return false;
            CADTransaccionApuesta cad = new CADTransaccionApuesta();
            return cad.Update(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Delete de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public override bool Delete()
        {
            if(!base.Delete()) return false;
            CADTransaccionApuesta cad = new CADTransaccionApuesta();
            return cad.Delete(this);
        }

        /// <summary>
        /// Este método llama a 
        /// ObtenerPorPrediccion de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Lista de transacciones del tipo apuesta
        /// </returns>
        public List<ENTransaccionApuesta> ObtenerPorPrediccion(string titulo)
        {
            CADTransaccionApuesta cad=new CADTransaccionApuesta();
            return cad.ObtenerPorPrediccion(titulo);
        }

        /// <summary>
        /// Este método llama a 
        /// ReadAll de CADTransaccionApuesta
        /// </summary>
        /// <returns>
        /// Lista de transacciones del tipo apuesta
        /// </returns>
        public new List<ENTransaccionApuesta> ReadAll()
        {
            CADTransaccionApuesta cad = new CADTransaccionApuesta();
            return cad.ReadAll();
        }

    }
}
