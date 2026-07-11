using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        private int _fila;
        public int Fila
        {
            get { return _fila; }
            set
            {
                if (value > 9) _fila = 9;
                else if (value < 0) _fila = 0;
                else _fila = value;
            }
        }
        private int _columna;

        public int Columna
        {
            get { return _columna; }
            set
            {
                if (value > 9) _columna = 9;
                else if (value < 0) _columna = 0;
                else _columna = value;
            }
        }
        /// <summary>
        /// Constructor por defecto, inicializa todo a 0
        /// </summary>
        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }
        /// <summary>
        /// Constructor que inicializa a partir de los parametros enteros
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }
        /// <summary>
        /// Constructor que inicializa con los parametros siendo strings asi que los pasa a enteros
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);

        }
        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="cor"></param>
        public Coordenada(Coordenada cor)
        {
            Columna = cor.Columna;
            Fila = cor.Fila;
        }
        public override string ToString()
        {
            return "(" + Fila + "," + Columna + ")";
        }
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();

        }
        /// <summary>
        /// Compara la coordenada con un objeto que de ser una coordenada debuelve true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            Coordenada c = (Coordenada)obj;
            if (c.Fila != this.Fila || c.Columna != this.Columna) return false;
            return true;

        }
        public bool Equals(Coordenada coordenada)
        {
            if (coordenada.Fila != this.Fila || coordenada.Columna != this.Columna) return false;
            return true;
        }
    }
}
