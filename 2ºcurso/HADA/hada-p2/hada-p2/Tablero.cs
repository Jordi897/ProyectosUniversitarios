using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Representa el tablero de juego donde se colocan los barcos,
    /// se registran los disparos y se gestionan los eventos de impacto y hundimiento.
    /// </summary>
    internal class Tablero
    {
        private int _tamTablero;

        /// <summary>
        /// Tamaño del tablero (número de filas y columnas).
        /// Debe estar entre 4 y 9; en caso contrario lanza una excepción.
        /// </summary>
        public int TamTablero
        {
            get { return _tamTablero; }
            set
            {
                if (value < 4 || value > 9)
                    throw new ArgumentException("Tamaño de tablero inadecuado");
                _tamTablero = value;
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        /// <summary>
        /// Crea un nuevo tablero con un tamaño determinado y una lista de barcos.
        /// Inicializa las casillas, registra los eventos de los barcos y prepara el estado del juego.
        /// </summary>
        /// <param name="tamTablero">Tamaño del tablero (entre 4 y 9).</param>
        /// <param name="barcos">Lista de barcos colocados en el tablero.</param>
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            this.barcos = new List<Barco>(barcos);
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            // Suscripción a los eventos de cada barco
            foreach (var item in this.barcos)
            {
                item.eventoTocado += cuandoEventoTocado;
                item.eventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();
        }

        /// <summary>
        /// Realiza un disparo sobre una coordenada del tablero.
        /// Si la coordenada está fuera de los límites, muestra un mensaje y no hace nada.
        /// Si es válida, notifica el disparo a todos los barcos.
        /// </summary>
        /// <param name="c">Coordenada donde se dispara.</param>
        public void Disparar(Coordenada c)
        {
            if (c.Fila >= TamTablero || c.Columna >= TamTablero || c.Fila < 0 || c.Columna < 0)
            {
                Console.WriteLine("La coordenada " + c + " está fuera de las dimensiones del tablero");
                return;
            }

            foreach (var item in this.barcos)
            {
                item.Disparo(c);
            }

            coordenadasDisparadas.Add(c);
        }

        /// <summary>
        /// Genera una representación visual del tablero,
        /// mostrando cada casilla entre corchetes según su estado:
        /// nombre del barco, tocado o agua.
        /// </summary>
        /// <returns>Cadena con el dibujo del tablero.</returns>
        public string DibujarTablero()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < this.TamTablero; i++)
            {
                for (int j = 0; j < this.TamTablero; j++)
                {
                    str.Append("[" + casillasTablero[new Coordenada(i, j)] + "]");
                }
                if (i != TamTablero - 1) str.AppendLine();
            }

            return str.ToString();
        }

        /// <summary>
        /// Devuelve una descripción completa del estado del tablero,
        /// incluyendo barcos, disparos realizados, impactos y el dibujo del tablero.
        /// </summary>
        /// <returns>Cadena con toda la información del tablero.</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            foreach (var item in this.barcos)
            {
                str.AppendLine(item.ToString());
            }

            str.AppendLine();
            str.Append("Coordenadas Disparadas:");
            foreach (var item in coordenadasDisparadas)
            {
                str.Append(" " + item.ToString());
            }

            str.AppendLine();
            str.Append("Coordenadas Tocadas:");
            foreach (var item in coordenadasTocadas)
            {
                str.Append(" " + item.ToString());
            }

            str.Append("\n\n\n");
            str.AppendLine("CASILLAS TABLERO");
            str.AppendLine("-------");
            str.AppendLine(DibujarTablero());

            return str.ToString();
        }

        /// <summary>
        /// Inicializa todas las casillas del tablero asignando:
        /// - El nombre del barco si la casilla pertenece a uno.
        /// - "AGUA" si la casilla está vacía.
        /// </summary>
        private void inicializaCasillasTablero()
        {
            for (int i = 0; i < this.TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    bool noContiene = true;

                    for (int k = 0; k < barcos.Count && noContiene; k++)
                    {
                        if (barcos[k].CoordenadasBarco.ContainsKey(c))
                        {
                            casillasTablero.Add(c, barcos[k].Nombre);
                            noContiene = false;
                        }
                    }

                    if (noContiene)
                    {
                        casillasTablero.Add(c, "AGUA");
                    }
                }
            }
        }

        /// <summary>
        /// Manejador del evento que se dispara cuando un barco queda hundido.
        /// Registra el barco eliminado y comprueba si todos los barcos han sido destruidos.
        /// Si es así, lanza el evento de fin de partida.
        /// </summary>
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine("TABLERO: Barco " + e.nombre + " hundido!!");
            barcosEliminados.Add((Barco)sender);

            if (eventoFinPartida != null)
            {
                bool todoHundido = true;

                foreach (var item in barcos)
                {
                    if (!item.hundido())
                    {
                        todoHundido = false;
                        break;
                    }
                }

                if (todoHundido)
                {
                    eventoFinPartida(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Manejador del evento que se dispara cuando un barco es tocado.
        /// Actualiza la casilla del tablero y registra la coordenada tocada.
        /// </summary>
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            casillasTablero[e.coordenadaImpacto] = e.nombre + "_T";
            Console.WriteLine("TABLERO: Barco " + e.nombre + " tocado en Coordenada: [" + e.coordenadaImpacto + "]");

            if (!coordenadasTocadas.Contains(e.coordenadaImpacto))
                coordenadasTocadas.Add(e.coordenadaImpacto);
        }

        /// <summary>
        /// Evento que se dispara cuando todos los barcos del tablero han sido hundidos.
        /// </summary>
        public event EventHandler<EventArgs> eventoFinPartida;
    }

}
