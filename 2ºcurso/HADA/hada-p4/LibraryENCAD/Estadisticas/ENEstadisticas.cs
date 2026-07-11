using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Estadisticas
{
    public class ENEstadisticas
    {
        /// <summary>
        /// Todas las variables necesarias para poder 
        /// almacenar las estadísticas en la base de datos
        /// </summary>
        private ENUsuario _usuario;
        public ENUsuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private int _mayorRacha;
        public int MayorRacha
        {
            get { return _mayorRacha; }
            set { _mayorRacha = value; }

        }
        private int _rachaActual;
        public int RachaActual
        {
            get { return _rachaActual; }
            set { _rachaActual = value; }
        }
        private decimal _maxPuntos;
        public decimal MaxPuntos
        {
            get
            {
                return _maxPuntos;
            }
            set
            {
                _maxPuntos = value;
            }
        }

        private int _prediccionesGanadas;
        public int PrediccionesGanadas
        {
            get { return _prediccionesGanadas; }
            set { _prediccionesGanadas = value; }

        }
        private int _prediccionesPerdidas;
        public int PrediccionesPerdidas
        {
            get { return _prediccionesPerdidas; }
            set { _prediccionesPerdidas = value; }

        }
        /// <summary>
        /// Las predicciones totales se calculan a partir de las predicciones ganadas y perdidas, 
        ///  ya que no se almacenan en la base de datos
        /// </summary>
        public int PrediccionesTotales
        {
            get { return PrediccionesGanadas + PrediccionesPerdidas; }
        }
        /// <summary>
        /// Constructor por defecto, inicializa los valores por defecto
        /// </summary>
        public ENEstadisticas()
        {
            Usuario = new ENUsuario();
            MayorRacha = 0;
            MaxPuntos = 0;
            PrediccionesGanadas = 0;
            PrediccionesPerdidas = 0;


        }
        /// <summary>
        /// Constructor por parametro de tipo usuario, 
        /// sirve para crear las estadísticas de un usuario concreto,
        /// inicializa los valores por defecto
        /// </summary>
        /// <param name="usuario"></param>
        public ENEstadisticas(ENUsuario usuario)
        {
            Usuario = usuario;
            MayorRacha = 0;
            MaxPuntos = 0;
            PrediccionesGanadas = 0;
            PrediccionesPerdidas = 0;

        }
        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="mayorRacha"></param>
        /// <param name="maxPuntos"></param>
        /// <param name="minPuntos"></param>
        /// <param name="prediccionesTotales"></param>
        /// <param name="prediccionesGanadas"></param>
        /// <param name="prediccionesPerdidas"></param>
        public ENEstadisticas(ENUsuario usuario, int mayorRacha, int maxPuntos, int minPuntos, int prediccionesTotales, int prediccionesGanadas, int prediccionesPerdidas)
        {
            Usuario = usuario;
            MayorRacha = mayorRacha;
            MaxPuntos = maxPuntos;
            PrediccionesGanadas = prediccionesGanadas;
            PrediccionesPerdidas = prediccionesPerdidas;
        }
        /// <summary>
        /// Lee las estadísticas de un usuario concreto
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.Read(this);
        }
        /// <summary>
        /// Crea las estadísticas de un usuario concreto, 
        /// se utiliza para crear las estadísticas de un nuevo usuario
        /// </summary>
        /// <returns></returns>
        public bool Write()
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.Write(this);
        }
        /// <summary>
        /// Elimina las estadísticas de un usuario concreto,
        /// para poder borrar un usuario sin que queden sus estadísticas
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.Delete(this);
        }
        /// <summary>
        /// Actualiza las estadísticas de un usuario concreto
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.Update(this);
        }
        public List<ENEstadisticas> ReadAllGanada(ENPrediccion pred, string resolucion)
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.ReadAllGanada(pred, resolucion);
        }
        public List<ENEstadisticas> ReadAllPerdida(ENPrediccion pred, string resolucion)
        {
            CADEstadisticas cadEstadisticas = new CADEstadisticas();
            return cadEstadisticas.ReadAllPerdida(pred, resolucion);
        }
    }
}
