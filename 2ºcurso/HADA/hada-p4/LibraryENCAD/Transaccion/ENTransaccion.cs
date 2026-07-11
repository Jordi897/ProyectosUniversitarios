using LibraryEN_CAD.ConversionDivisa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Transaccion
{
    /// <summary>
    /// Esta clase representa el
    /// EN de Transaccion
    /// </summary>
    public class ENTransaccion
    {
        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa el codigo de la
        /// transacción
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad con campo de respaldo
        /// que representa la cantidad de valor 
        /// de la transacción
        /// </summary>
        private decimal _cantidad;
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        /// <summary>
        /// Propiedad con campo de
        /// respaldo que representa la
        /// fecha de la transacción
        /// </summary>
        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ENTransaccion() { }

        /// <summary>
        /// Este método llama
        /// a Create de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se crea,falso si no
        /// </returns>
        public virtual bool Create()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Create(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Read de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public virtual bool Read()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Read(this);
        }

        /// <summary>
        /// Este método llama a
        /// Update de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si actualiza,falso si no
        /// </returns>

        public virtual bool Update()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Update(this);
        }

        /// <summary>
        /// Este método llama a 
        /// Delete de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public virtual bool Delete()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Delete(this);
        }

        /// <summary>
        /// Este método llama a
        /// ReadAll de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si lee todo, falso si no
        /// </returns>
        public List<ENTransaccion> ReadAll()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadAll();
        }

        /// <summary>
        /// Este método llama
        /// a ReadFirst de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadFirst()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadFirst(this);
        }

        /// <summary>
        /// Este método llama
        /// a ReadLast de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadLast()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadLast(this);
        }

        /// <summary>
        /// Este método llama
        /// a ReadNext de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadNext()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadNext(this);
        }

        /// <summary>
        /// Este método llama
        /// a ReadPrev de CADTransaccion
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool ReadPrev()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadPrev(this);
        }
    }
}