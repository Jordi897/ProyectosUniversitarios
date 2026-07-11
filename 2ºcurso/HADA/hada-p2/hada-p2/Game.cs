using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida; //Si se pone true se termina la partida
        /// <summary>
        /// Se instancia la clase
        /// </summary>
        public Game()
        {
            finPartida = false;
            gameLoop();
        }
        /// <summary>
        /// Donde va a estar todo el juego
        /// </summary>
        private void gameLoop()
        {
            // Inicializo los barcos y el tablero donde los añado
            Barco b1 = new Barco("LOKI", 3, 'h', new Coordenada(4, 6));
            Barco b2 = new Barco("MASA", 2, 'v', new Coordenada(3, 3));
            Barco b3 = new Barco("POPY", 1, 'h', new Coordenada(1, 1));
            List<Barco> barcos = new List<Barco>();
            barcos.Add(b1);
            barcos.Add(b2);
            barcos.Add(b3);
            Tablero tablero = new Tablero(9, barcos);
            tablero.eventoFinPartida += cuandoEventoFinPartida;
            string entrada;
            // Bucle del juego, si finPartida es true se termina el juego
            while (!finPartida)
            {
                do
                {
                    System.Console.WriteLine(tablero);
                    System.Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir)");
                    entrada = Console.ReadLine();
                    if (entrada == "S" || entrada == "s") return;// Si se escribe una s se termina el juego
                } while (!char.IsDigit(entrada[0]) || entrada[1] != ',' || !char.IsDigit(entrada[2]));// Se comprueba el formato correcto
                tablero.Disparar(new Coordenada((int)char.GetNumericValue(entrada[0]), (int)char.GetNumericValue(entrada[2])));// Dispara a la coordenada indicada
            }

        }
        /// <summary>
        /// Si se invoca se termina la partida 
        /// </summary>
        /// <param name="sender">objecto que dispara el evento</param>
        /// <param name="a"></param>
        private void cuandoEventoFinPartida(object sender, EventArgs a)
        {
            System.Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }

    }

}
