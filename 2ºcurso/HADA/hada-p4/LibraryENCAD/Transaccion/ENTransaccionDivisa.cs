using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Transaccion
{

    /// <summary>
    /// Esta clase representa el
    /// EN de TransaccionDivisa
    /// </summary>
    public class ENTransaccionDivisa : ENTransaccion
    {

        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa la divisa  
        /// de la "TransacciónDivisa"
        /// </summary>
        private string _divisa;
        public string Divisa
        {
            get { return _divisa; }
            set { _divisa = value; }
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
        /// Este método llama
        /// a Create de CADTransaccionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se crea,falso si no
        /// </returns>
        public override bool Create()
        {
            if (!base.Create()) return false;
            CADTransaccionDivisa t = new CADTransaccionDivisa();
            return t.Create(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Read de CADTransaccionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public override bool Read()
        {
            if (!base.Read()) return false;
            CADTransaccionDivisa t = new CADTransaccionDivisa();
            return t.Read(this);
        }

        /// <summary>
        /// Este método llama a
        /// Update de CADTransaccionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si actualiza,falso si no
        /// </returns>
        public override bool Update()
        {
            if (!base.Update()) return false;
            CADTransaccionDivisa t = new CADTransaccionDivisa();
            return t.Update(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Delete de CADTransaccionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public override bool Delete()
        {
            if (!base.Delete()) return false;
            CADTransaccionDivisa t = new CADTransaccionDivisa();
            return t.Delete(this);
        }

        /// <summary>
        /// Este método llama a 
        /// ReadAll de CADTransaccionDivisa
        /// </summary>
        /// <returns>
        /// Lista de transacciones del tipo divisa
        /// </returns>
        public new List<ENTransaccionDivisa> ReadAll()
        {
            CADTransaccionDivisa cad = new CADTransaccionDivisa();
            return cad.ReadAll();
        }

    }
}
