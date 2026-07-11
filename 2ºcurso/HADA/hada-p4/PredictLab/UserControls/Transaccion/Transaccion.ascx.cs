using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LibraryEN_CAD.Transaccion;

namespace PredictLab.UserControls.Transaccion
{
    /// <summary>
    /// Esta clase representa
    /// el code behind de Transaccion.ascx
    /// </summary>
    public partial class Transaccion : System.Web.UI.UserControl
    {
        /// <summary>
        /// Propiedad privada que
        /// representa una lista de transacciones
        /// que usaremos durante todo el programa
        /// </summary>
        private List<ENTransaccion> listaAct = new List<ENTransaccion>();


        /// <summary>
        /// Propiedad privada con campo de respaldo
        /// que representa la wallet de un usuario
        /// </summary>
        private int WalletActual
        {
            get
            {
                if (Session["Wallet"] == null)
                {
                    return -1;
                }
                return int.Parse(Session["Wallet"].ToString());
            }
        }
        /// <summary>
        /// Propiedad privada con campo de respaldo
        /// que contiene un indice que usaremos a lo largo del programa
        /// </summary>
        private int indiceAct
        {
            get
            {
                if (Session["IndiceTransaccion"] == null)
                {
                    return 0;
                }
                int valor;
                bool correcto = int.TryParse(Session["IndiceTransaccion"].ToString(), out valor);

                if (correcto)
                {
                    return valor;
                }
                return 0;
            }
            set
            {
                Session["IndiceTransaccion"] = value;
            }
        }
        /// <summary>
        /// Propiedad privada con campo de respaldo
        /// que contiene el filtro aplicado según
        /// el tipo de transacción
        /// </summary>
        private string FiltroActual
        {
            get
            {
                if (Session["FiltroTransaccion"] == null)
                {
                    return "todas";
                }
                return Session["FiltroTransaccion"].ToString();
            }
            set
            {
                Session["FiltroTransaccion"] = value;
            }
        }
        /// <summary>
        /// Este evento se ejecuta cada vez que se carga la página.
        /// Por defecto mostrará la ultima transacción realizada
        /// por el usuario
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTodas();

                if (listaAct.Count > 0)
                {
                    indiceAct = listaAct.Count - 1;
                    Mostrar(listaAct[indiceAct]);
                }
                else
                {
                    LimpiarCampos();
                }
            }
            else
            {
                CargarListaSegunFiltro();
            }
        }
        /// <summary>
        /// Este método se encarga de llamar a las funciones auxiliares para
        /// cargar un tipo de lista determinado (o todas si no se ha aplicado filtro)
        /// segun el tipo de transacción
        /// </summary>
        private void CargarListaSegunFiltro()
        {
            if (FiltroActual == "gana")
            {
                CargarListaGana();
                return;
            }
            if (FiltroActual == "divisa")
            {
                CargarListaDivisa();
                return;
            }
            if (FiltroActual == "apuesta")
            {
                CargarListaApuesta();
                return;
            }
            CargarTodas();
        }
        /// <summary>
        /// Este método carga las transacciones
        /// de tipo gana del usuario
        /// </summary>
        private void CargarListaGana()
        {
            ENTransaccionGana en = new ENTransaccionGana();
            List<ENTransaccionGana> datos = en.ReadAll();

            listaAct = new List<ENTransaccion>();

            if (datos != null)
            {
                foreach (ENTransaccionGana x in datos)
                {
                    if (x.Wallet == WalletActual)
                    {
                        listaAct.Add(x);
                    }
                }
            }
        }

        /// <summary>
        /// Este método carga las transacciones
        /// de tipo divisa del usuario
        /// </summary>
        private void CargarListaDivisa()
        {
            ENTransaccionDivisa en = new ENTransaccionDivisa();
            List<ENTransaccionDivisa> datos = en.ReadAll();

            listaAct = new List<ENTransaccion>();

            if (datos != null)
            {
                foreach (ENTransaccionDivisa x in datos)
                {
                    if (x.Wallet == WalletActual)
                    {
                        listaAct.Add(x);
                    }
                }
            }
        }

        /// <summary>
        /// Este método carga las transacciones
        /// de tipo apuesta del usuario
        /// </summary>
        private void CargarListaApuesta()
        {
            ENTransaccionApuesta en = new ENTransaccionApuesta();
            List<ENTransaccionApuesta> datos = en.ReadAll();

            listaAct = new List<ENTransaccion>();

            if (datos != null)
            {
                foreach (ENTransaccionApuesta x in datos)
                {
                    if (x.Wallet == WalletActual)
                    {
                        listaAct.Add(x);
                    }
                }
            }
        }

        /// <summary>
        /// Este método carga todas las transacciones del usuario
        /// </summary>
        private void CargarTodas()
        {
            listaAct = new List<ENTransaccion>();

            ENTransaccionApuesta enA = new ENTransaccionApuesta();
            foreach (var x in enA.ReadAll())
            {
                if (x.Wallet == WalletActual)
                {
                    listaAct.Add(x);
                }
            }
            ENTransaccionGana enG = new ENTransaccionGana();
            foreach (var x in enG.ReadAll())
            {
                if (x.Wallet == WalletActual)
                {
                    listaAct.Add(x);
                }
            }

            ENTransaccionDivisa enD = new ENTransaccionDivisa();
            foreach (var x in enD.ReadAll())
            {
                if (x.Wallet == WalletActual)
                {
                    listaAct.Add(x);
                }
            }
            listaAct.Sort(CompararTransaccionesPorId);
        }

        /// <summary>
        /// Este es un método auxiliar para
        /// ordenar las transacciones
        /// </summary>
        /// <param name="a">
        /// Primera transacción a comparar
        /// </param>
        /// <param name="b">
        /// Segunda transacción a comparar
        /// </param>
        /// <returns>
        /// Comparando IDs,1 si a es mayor, 0 si iguales, -1 si b es mayor
        /// </returns>
        private static int CompararTransaccionesPorId(ENTransaccion a, ENTransaccion b)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            if (a == null)
            {
                return -1;
            }
            if (b == null)
            {
                return 1;
            }
            if (a.Id < b.Id)
            {
                return -1;
            }
            if (a.Id > b.Id)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// Función que limpia todos
        /// los campos de texto
        /// </summary>
        private void LimpiarCampos()
        {
            Id.Text = "—";
            CantidadGastada.Text = "—";
            Fecha.Text = "—";
            Voto.Text = "";
            Prediccion.Text = "";
            Divisa.Text = "";

            FilaVoto.Visible = false;
            FilaPrediccion.Visible = false;
            FilaDivisa.Visible = false;

        }

        /// <summary>
        /// Este evento filtra las transacciones
        /// para mostrar solo las de tipo gana
        /// </summary>
        protected void Gana_Filtrar(object sender, EventArgs e)
        {
            FiltroActual = "gana";
            CargarListaGana();

            if (listaAct.Count == 0)
            {
                LimpiarCampos();
                return;
            }
            indiceAct = listaAct.Count - 1;
            Mostrar(listaAct[indiceAct]);
        }

        /// <summary>
        /// Este evento filtra las transacciones
        /// para mostrar solo las de tipo divisa
        /// </summary>
        protected void Divisa_Filtrar(object sender, EventArgs e)
        {
            FiltroActual = "divisa";
            CargarListaDivisa();

            if (listaAct.Count == 0)
            {
                LimpiarCampos();
                return;
            }
            indiceAct = listaAct.Count - 1;
            Mostrar(listaAct[indiceAct]);
        }

        /// <summary>
        /// Este evento filtra las transacciones
        /// para mostrar solo las de tipo apuesta
        /// </summary>
        protected void Apuesta_Filtrar(object sender, EventArgs e)
        {
            FiltroActual = "apuesta";
            CargarListaApuesta();

            if (listaAct.Count == 0)
            {
                LimpiarCampos();
                return;
            }
            indiceAct = listaAct.Count - 1;
            Mostrar(listaAct[indiceAct]);
        }

        /// <summary>
        /// Este evento "filtra" las transacciones
        /// para mostrar todas
        /// </summary>
        protected void Todas_Filtrar(object sender, EventArgs e)
        {
            FiltroActual = "todas";
            CargarTodas();

            if (listaAct.Count == 0)
            {
                LimpiarCampos();
                return;
            }
            indiceAct = listaAct.Count - 1;
            Mostrar(listaAct[indiceAct]);
        }




        /// <summary>
        /// Este evento se activa al pulsar el botón "Anterior"
        /// </summary>
        protected void Click_Anterior(object sender, EventArgs e)
        {
            CargarListaSegunFiltro();
            if (listaAct.Count == 0)
            {
                return;
            }
            if (indiceAct > 0)
            {
                indiceAct = indiceAct - 1;
                Mostrar(listaAct[indiceAct]);
            }
        }
        /// <summary>
        /// Este evento se activa al pulsar el botón "Posterior"
        /// </summary>
        protected void Click_Posterior(object sender, EventArgs e)
        {
            CargarListaSegunFiltro();
            if (listaAct.Count == 0)
            {
                return;
            }
            if (indiceAct < listaAct.Count - 1)
            {
                indiceAct = indiceAct + 1;
                Mostrar(listaAct[indiceAct]);
            }
        }
        /// <summary>
        /// Este evento se activa al pulsar el botón "Primera"
        /// </summary>
        protected void Click_Primera(object sender, EventArgs e)
        {
            CargarListaSegunFiltro();
            if (listaAct.Count == 0)
            {
                return;
            }

            indiceAct = 0;
            Mostrar(listaAct[indiceAct]);

        }

        /// <summary>
        /// Este evento se activa al pulsar el botón "Última"
        /// </summary>
        protected void Click_Ultima(object sender, EventArgs e)
        {
            CargarListaSegunFiltro();
            if (listaAct.Count == 0)
            {
                return;
            }
            indiceAct = listaAct.Count - 1;
            Mostrar(listaAct[indiceAct]);
        }


        /// <summary>
        /// Esta función se encarga de mostrar
        /// las transacciones según el filtro aplicado
        /// o botón pulsado
        /// </summary>
        private void Mostrar(ENTransaccion en)
        {
            if (en == null)
            {
                LimpiarCampos();
                return;
            }
            Id.Text = en.Id.ToString();
            CantidadGastada.Text = en.Cantidad.ToString();
            Fecha.Text = en.Fecha.ToString("yyyy-MM-dd");
            FilaVoto.Visible = false;
            FilaPrediccion.Visible = false;
            FilaDivisa.Visible = false;

            if (en is ENTransaccionApuesta)
            {
                ENTransaccionApuesta a = (ENTransaccionApuesta)en;
                Id.Text = "Transacción tipo apuesta, ID:" + en.Id.ToString();

                if (a.Prediccion != null)
                {
                    FilaVoto.Visible = true;
                    FilaPrediccion.Visible = true;
                    Prediccion.Text = "Número de predicción: " + a.Prediccion.ToString();
                    Voto.Text = a.Voto.ToString();
                }
            }

            else if (en is ENTransaccionGana)
            {
                ENTransaccionGana g = (ENTransaccionGana)en;
                Id.Text = "Resolución de predicción, ID:" + en.Id.ToString();

                if (g.Prediccion != null)
                {

                    FilaPrediccion.Visible = true;
                    Prediccion.Text = "Número de predicción: " + g.Prediccion.ToString();

                }

            }
            else if (en is ENTransaccionDivisa)
            {
                ENTransaccionDivisa d = (ENTransaccionDivisa)en;
                Id.Text = "Resolución de conversión, ID:" + en.Id.ToString();

                if (d.Divisa != null)
                {
                    Divisa.Text = d.Divisa;
                    FilaDivisa.Visible = true;
                }
            }
        }
    }
}
