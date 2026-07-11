using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.ConversionDivisa
{
    /// <summary>
    /// Esta clase representa el EN
    /// de ConversionDivisa
    /// </summary>
    public class ENConversionDivisa
    {
        /// <summary>
        /// Propiedad con campo de
        /// respaldo que representa
        /// la moneda de la conversión
        /// </summary>
        private string _moneda;
        public string Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        /// <summary>
        /// Propiedad con campo de
        /// respaldo que representa
        /// el valor virtual asociado
        /// a la moneda
        /// </summary>
        private decimal _valorVirtual;
        public decimal ValorVirtual
        {
            get { return _valorVirtual; }
            set { _valorVirtual = value; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ENConversionDivisa() { }

        /// <summary>
        /// Este método llama
        /// a Create de CADConversionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se crea, falso si no
        /// </returns>
        public bool Create()
        {
            CADConversionDivisa c = new CADConversionDivisa();
            return c.Create(this);
        }


        /// <summary>
        /// Este método llama a 
        /// Read de CADConversionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se lee, falso si no
        /// </returns>
        public bool Read()
        {
            CADConversionDivisa c = new CADConversionDivisa();
            return c.Read(this);
        }

        /// <summary>
        /// Este método llama a
        /// Update de CADConversionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se actualiza, falso si no
        /// </returns>
        public bool Update()
        {
            CADConversionDivisa c = new CADConversionDivisa();
            return c.Update(this);
        }


        /// <summary>
        /// Este método llama a
        /// Delete de CADConversionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se borra, falso si no
        /// </returns>
        public bool Delete()
        {
            CADConversionDivisa c = new CADConversionDivisa();
            return c.Delete(this);
        }

        /// <summary>
        /// Este método llama a
        /// ReadAll de CADConversionDivisa
        /// </summary>
        /// <returns>
        /// Verdadero si se puede leer todo,
        /// falso si no
        /// </returns>
        public List<ENConversionDivisa> ReadAll()
        {
            CADConversionDivisa cad = new CADConversionDivisa();
            return cad.ReadAll();
        }
    }
}