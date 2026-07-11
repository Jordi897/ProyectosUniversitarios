using LibraryEN_CAD.Estadisticas;
using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Transaccion;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Public
{
    public partial class Prediccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code_url = Request.QueryString["id"];

                if (!int.TryParse(code_url, out int code))
                {
                    Response.Redirect("~/Pages/Public/Default.aspx");
                    return;
                }

                btnApostar.Enabled = false;
                labelErrorApuesta.Visible = false;

                ENPrediccion p = new ENPrediccion();
                p.Id = code;
                if (p.Read())
                {
                    cargarDatos(p);
                    ENUsuario user = (ENUsuario)Session["Usuario"];
                    if (user != null && user.admin)
                    {
                        btnResolverPrediccion.Visible = true;
                    }
                    else
                    {
                        btnResolverPrediccion.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/Pages/Public/Default.aspx");
                }
            }
            else
            {
                actualizarDetalles();
            }
        }

        protected void cargarDatos(ENPrediccion p)
        {
            labelCategoria.Text = p.Categoria;
            labelTitulo.Text = p.Titulo;
            labelPregunta.Text = p.Prediccion;
            labelProb.Text = calcularProb(p).ToString("F2");
            labelCantidadApostada.Text = p.CantidadRecaudada.ToString();
            labelFechaLim.Text = p.FechaFin.ToString("dd/MM/yyyy");
            modalTitulo.Text = p.Titulo;
            modalPregunta.Text = p.Prediccion;
            modalCantidad.Text = p.CantidadRecaudada.ToString();
            modalSi.Text = p.VotosSi.ToString();
            modalNo.Text = p.VotosNo.ToString();

            hfTotalSi.Value = p.VotosSi.ToString();
            hfTotalNo.Value = p.VotosNo.ToString();
        }

        protected decimal calcularProb(ENPrediccion p)
        {
            return (p.VotosSi + p.VotosNo) == 0 ? 50 : (p.VotosSi / (p.VotosSi + p.VotosNo)) * 100;
        }

        protected void btnApostar_Click(object sender, EventArgs e)
        {
            ENUsuario user = (ENUsuario)Session["Usuario"];
            if (user == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
            hacerApuesta();
            labelApuesta.Text = "";
            txtCantidad.Text = "";
            btnSi.CssClass = "btn btn-outline-success w-100";
            btnNo.CssClass = "btn btn-outline-danger w-100";
            labelAcciones.Text = "0";
            labelGanancia.Text = "0";
            btnApostar.Enabled = false;

            string code_url = Request.QueryString["id"];
            int.TryParse(code_url, out int code);

            ENPrediccion p = new ENPrediccion();
            p.Id = code;
            if (p.Read())
            {
                cargarDatos(p);
            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            labelApuesta.Text = "SI";
            labelApuesta.CssClass = "fw-semibold text-success";
            btnSi.CssClass = "btn btn-success w-100";
            btnNo.CssClass = "btn btn-outline-danger w-100";
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            labelApuesta.Text = "NO";
            labelApuesta.CssClass = "fw-semibold text-danger";
            btnSi.CssClass = "btn btn-outline-success w-100";
            btnNo.CssClass = "btn btn-danger w-100";
        }

        protected void actualizarDetalles()
        {
            string code_url = Request.QueryString["id"];
            int.TryParse(code_url, out int code);

            ENPrediccion p = new ENPrediccion();
            p.Id = code;
            if (p.Read())
            {
                cargarDatos(p);
            }
        }

        void hacerApuesta()
        {
            string code_url = Request.QueryString["id"];
            int.TryParse(code_url, out int code);
            decimal.TryParse(txtCantidad.Text, out decimal cantidad);

            ENUsuario user = (ENUsuario)Session["Usuario"];
            ENWallet w = new ENWallet();
            w.Id = user.wallet;
            if (w.GetSaldo() && w.Saldo < cantidad)
            {
                Response.Redirect("~/Pages/Public/Divisa.aspx");
                return;
            }

            ENTransaccionApuesta ta = new ENTransaccionApuesta();
            ta.Cantidad = cantidad;
            ta.Voto = labelApuesta.Text;
            ta.Wallet = user.wallet;
            ta.Fecha = DateTime.Now;
            ta.Prediccion = code;
            if (!ta.Create())
            {
                labelErrorApuesta.Visible = true;
                return;
            }

            w.Saldo -= cantidad;
            w.UpdateSaldo();

            ENPrediccion p = new ENPrediccion();
            p.Id = code;
            if (p.Read())
            {
                p.CantidadRecaudada += cantidad;
                if (labelApuesta.Text == "SI")
                {
                    p.VotosSi += cantidad;
                }
                else
                {
                    p.VotosNo += cantidad;
                }
                if (!p.Update())
                {
                    labelErrorApuesta.Visible = true;
                }
            }
        }
        protected void btnConfirmarResolucion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfLadoResolver.Value))
            {
                labelErrorResolver.Text = "Selecciona SÍ o NO antes de confirmar.";
                labelErrorResolver.Visible = true;
                return;
            }

            string code_url = Request.QueryString["id"];
            int.TryParse(code_url, out int code);

            ENPrediccion p = new ENPrediccion();
            p.Id = code;
            //Hecho por Hugo
            ENEstadisticas est = new ENEstadisticas();
            foreach (ENEstadisticas estadistica in est.ReadAllGanada(p, hfLadoResolver.Value))
            {

                if (estadistica.Read())
                {
                    estadistica.RachaActual++;
                    if (estadistica.RachaActual > estadistica.MayorRacha)
                    {
                        estadistica.MayorRacha = estadistica.RachaActual;
                    }
                    estadistica.PrediccionesGanadas++;
                    estadistica.Update();
                }

            }
            foreach (ENEstadisticas estadistica in est.ReadAllPerdida(p, hfLadoResolver.Value))
            {
                if (estadistica.Read())
                {
                    estadistica.RachaActual = 0;

                    estadistica.PrediccionesPerdidas++;
                    estadistica.Update();
                }

            }
            //fin hecho por Hugo

            if (p.ResolverPrediccion(hfLadoResolver.Value))
            {
                Response.Redirect("~/Pages/Public/Default.aspx");
            }
            else
            {
                labelErrorResolver.Text = "Error al resolver la predicción. Inténtalo de nuevo.";
                labelErrorResolver.Visible = true;
            }


        }
    }
}