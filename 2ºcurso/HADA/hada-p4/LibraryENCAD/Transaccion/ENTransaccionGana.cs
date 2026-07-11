using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Transaccion
{
    /// <summary>
    /// Esta clase representa el
    /// EN de TransaccionGana
    /// </summary>
    public class ENTransaccionGana : ENTransaccion
    {
        

        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa la predicción  
        /// de la "TransacciónGana"
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
        /// de la "TransacciónGana"
        /// </summary>
        private int _wallet;
        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        /// <summary>
        /// Este método llama
        /// a Create de CADTransaccionGana
        /// </summary>
        /// <returns>
        /// Verdadero si se crea,falso si no
        /// </returns>
        public override bool Create()
        {
            if (!base.Create()) return false;
            CADTransaccionGana t = new CADTransaccionGana();
            return t.Create(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Read de CADTransaccionGana
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public override bool Read()
        {
            if (!base.Read()) return false;
            CADTransaccionGana t = new CADTransaccionGana();
            return t.Read(this);
        }


        /// <summary>
        /// Este método llama a
        /// Update de CADTransaccionGana
        /// </summary>
        /// <returns>
        /// Verdadero si actualiza,falso si no
        /// </returns>
        public override bool Update()
        {
            if (!base.Update()) return false;
            CADTransaccionGana t = new CADTransaccionGana();
            return t.Update(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Delete de CADTransaccionGana
        /// </summary>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public override bool Delete()
        {
            if (!base.Delete()) return false;
            CADTransaccionGana t = new CADTransaccionGana();
            return t.Delete(this);
        }

        /// <summary>
        /// Este método llama a 
        /// ReadAll de CADTransaccionGana
        /// </summary>
        /// <returns>
        /// Lista de transaccion del tipo gana
        /// </returns>
        public new List<ENTransaccionGana> ReadAll()
        {
            CADTransaccionGana cad = new CADTransaccionGana();
            return cad.ReadAll();
        }

    }
}
