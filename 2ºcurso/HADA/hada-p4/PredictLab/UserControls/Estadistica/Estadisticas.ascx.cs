using LibraryEN_CAD.Estadisticas;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.UserControls.Estadisticas
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        /// <summary>
        /// Si existen las estadisticas muestra los datos, si no, muestra un boton para crear las estadisticas del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ENEstadisticas est = new ENEstadisticas();
                est.Usuario = (ENUsuario)Session["Usuario"];
                if (est.Read())
                {
                    if (est.Usuario.GetSaldo() > est.MaxPuntos)
                    {
                        est.MaxPuntos = est.Usuario.GetSaldo();
                        est.Update();
                    }
                    estadisticasUsuario.Visible = true;
                    btnCrearEstadisticas.Visible = false;
                    RActual.Text = est.RachaActual.ToString();
                    MRacha.Text = est.MayorRacha.ToString();
                    MPuntos.Text = est.MaxPuntos.ToString();
                    PGanadas.Text = est.PrediccionesGanadas.ToString();
                    PPerdidas.Text = est.PrediccionesPerdidas.ToString();
                    PTotales.Text = est.PrediccionesTotales.ToString();
                }
                else
                {
                    estadisticasUsuario.Visible = false;
                    btnCrearEstadisticas.Visible = true;
                }
                

            }
            
        }
        /// <summary>
        /// Un boton que inicializa las estadisticas del usuario, con los valores a 0, y redirige al perfil del usuario para mostrar las estadisticas creadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            ENEstadisticas est = new ENEstadisticas();
            est.Usuario = (ENUsuario)Session["Usuario"];
            est.MayorRacha = 0;
            est.MaxPuntos = 0;
            est.PrediccionesGanadas = 0;
            est.PrediccionesPerdidas = 0;
            est.Write();
            Response.Redirect("~/Pages/Private/Perfil.aspx");
        }
    }
}