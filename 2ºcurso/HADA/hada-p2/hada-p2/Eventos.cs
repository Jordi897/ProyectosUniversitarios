using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Argumentos del evento que se lanza cuando un barco es tocado. 
    /// Contiene el nombre del barco y la coordenada donde ha recibido el impacto.
    /// </summary>
    internal class TocadoArgs : EventArgs
    {
        /// <summary>
        /// Nombre del barco que ha sido alcanzado.
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Coordenada exacta donde se ha producido el impacto.
        /// </summary>
        public Coordenada coordenadaImpacto { get; set; }
        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            this.nombre = nombre;
            this.coordenadaImpacto = coordenadaImpacto;
        }
    }
    /// <summary>
    /// Argumentos del evento que se lanza cuando un barco queda hundido. 
    /// Contiene únicamente el nombre del barco hundido.
    /// </summary>
    internal class HundidoArgs : EventArgs
    {
        public string nombre { get; set; }
        public HundidoArgs(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
