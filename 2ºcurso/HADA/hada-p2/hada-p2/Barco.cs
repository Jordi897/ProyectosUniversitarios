using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Representa un barco dentro del juego, compuesto por varias coordenadas
    /// y capaz de recibir disparos, quedar tocado y hundirse.
    /// </summary>
    internal class Barco
    {
        /// <summary>
        /// Diccionario que almacena las coordenadas ocupadas por el barco.
        /// La clave es la coordenada y el valor indica el estado:
        /// - Nombre del barco → intacto
        /// - Nombre + "_T" → tocado
        /// </summary>
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }

        /// <summary>
        /// Nombre del barco.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Número de impactos recibidos por el barco.
        /// </summary>
        public int NumDanyos { get; set; }

        /// <summary>
        /// Crea un nuevo barco con un nombre, longitud, orientación y coordenada inicial.
        /// Genera automáticamente todas las coordenadas que ocupa el barco.
        /// </summary>
        /// <param name="nombre">Nombre del barco.</param>
        /// <param name="longitud">Número de casillas que ocupa el barco.</param>
        /// <param name="orientacion">'h' para horizontal, 'v' para vertical.</param>
        /// <param name="coordenadaInicio">Coordenada inicial donde comienza el barco.</param>
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.CoordenadasBarco = new Dictionary<Coordenada, string>();
            this.Nombre = nombre;
            NumDanyos = 0;

            // Generación de las coordenadas del barco según su orientación
            for (int i = 0; i < longitud; i++)
            {
                switch (orientacion)
                {
                    case 'h': // Barco horizontal
                        CoordenadasBarco.Add(new Coordenada(coordenadaInicio), nombre);
                        coordenadaInicio.Columna++;
                        break;

                    case 'v': // Barco vertical
                        CoordenadasBarco.Add(new Coordenada(coordenadaInicio), nombre);
                        coordenadaInicio.Fila++;
                        break;
                }
            }
        }

        /// <summary>
        /// Procesa un disparo sobre una coordenada. Si el disparo impacta en el barco,
        /// marca la sección como tocada, lanza el evento de impacto y, si corresponde,
        /// el evento de hundimiento.
        /// </summary>
        /// <param name="c">Coordenada donde se realiza el disparo.</param>
        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c) && eventoTocado != null)
            {
                // Marca la coordenada como tocada
                CoordenadasBarco[c] = Nombre + "_T";

                // Notifica que el barco ha sido tocado
                eventoTocado(this, new TocadoArgs(Nombre, c));

                NumDanyos++;

                // Si todas las partes están tocadas, se hunde
                if (hundido() && eventoHundido != null)
                {
                    eventoHundido(this, new HundidoArgs(Nombre));
                }
            }
        }

        /// <summary>
        /// Indica si el barco está completamente hundido.
        /// </summary>
        /// <returns>
        /// true si todas las coordenadas están marcadas como tocadas;
        /// false si queda alguna parte intacta.
        /// </returns>
        public bool hundido()
        {
            foreach (var values in CoordenadasBarco.Values)
            {
                if (values == Nombre) return false; // Aún queda una parte sin tocar
            }

            return true; // Todas las partes están tocadas
        }

        /// <summary>
        /// Devuelve una representación en texto del estado del barco,
        /// incluyendo daños, hundimiento y coordenadas.
        /// </summary>
        /// <returns>Cadena con la información del barco.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[" + Nombre + "]" + " - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [" + hundido() + "] - COORDENADAS:");

            foreach (var dicc in CoordenadasBarco)
            {
                sb.Append(" [" + dicc.Key + " :" + dicc.Value + "]");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Evento que se dispara cuando el barco recibe un impacto.
        /// </summary>
        public event EventHandler<TocadoArgs> eventoTocado;

        /// <summary>
        /// Evento que se dispara cuando el barco queda completamente hundido.
        /// </summary>
        public event EventHandler<HundidoArgs> eventoHundido;
    }

}
