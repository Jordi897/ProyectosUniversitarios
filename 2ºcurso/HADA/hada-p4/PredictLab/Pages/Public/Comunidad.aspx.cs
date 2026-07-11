using LibraryEN_CAD.Comunidad;
using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Transaccion;
using LibraryEN_CAD.Usuario;
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
    public partial class Comunidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarPredicciones();
                cargarComunidades();
            }
        }
        private void cargarComunidades()
        {
            ENComunidad c = new ENComunidad();
            List<ENComunidad> lista = c.ReadAll();

            if (lista != null && lista.Count > 0)
            {
                repeaterComunidades.DataSource = lista;
                repeaterComunidades.DataBind();
                labelError.Visible = false;
            }
            else
            {
                repeaterComunidades.DataSource = null;
                repeaterComunidades.DataBind();
                labelError.Visible = true;
            }
        }

        private void cargarPredicciones()
        {
            ENPrediccion p = new ENPrediccion();
            List<ENPrediccion> lista = p.ReadAll();
            if (lista != null && lista.Count > 0)
            {
                ddlPrediccion.DataSource = lista;
                ddlPrediccion.DataTextField = "Titulo";
                ddlPrediccion.DataValueField = "Id";
                ddlPrediccion.DataBind();
            }
        }

        protected void verComunidad_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);
            mostrarDetalles(id);
        }

        private void mostrarDetalles(int id)
        {
            hfComunidadSeleccionada.Value = id.ToString();
            ENComunidad c = new ENComunidad();
            c.Id = id;
            if (c.Read())
            {
                litTitulo.Text = c.Titulo;
                litDescripcion.Text = c.Descripcion;
                litSaldo.Text = c.Saldo.ToString();
                litPrediccion.Text = c.Prediccion.ToString();

                labelVotoComunidad.Text = "Voto: " + c.Voto;
                labelVotoComunidad.CssClass = c.Voto == "SI" ? "badge text-bg-success" : "badge text-bg-danger";
                labelVotoComunidad.Visible = true;

                ENPrediccion p = new ENPrediccion();
                p.Id = c.Prediccion;
                if (p.Read())
                {
                    litTituloPrediccion.Text = p.Titulo;
                    litDescripcionPrediccion.Text = p.Prediccion;
                    litPrediccion.Text = p.Titulo;
                    litFechaFin.Text = p.FechaFin.ToString();

                    decimal total = p.VotosSi + p.VotosNo;
                    decimal prob = total == 0 ? 50 : Math.Round(p.VotosSi / total * 100, 2);
                    litProbabilidad.Text = prob.ToString();
                    hfProbSi.Value = p.VotosSi.ToString();
                    hfProbNo.Value = p.VotosNo.ToString();
                }

                pnlDetalle.Visible = true;
                pnlPlaceholder.Visible = false;
                labelErrorDetalle.Visible = false;

            }
            else
            {
                labelErrorDetalle.Text = "No se pudo cargar la comunidad.";
                labelErrorDetalle.Visible = true;
                return;
            }            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCrearComunidad_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
            if (string.IsNullOrEmpty(txtTituloNueva.Text) || string.IsNullOrEmpty(txtDescripcionNueva.Text) || string.IsNullOrEmpty(ddlPrediccion.SelectedValue) || string.IsNullOrEmpty(hfLadoResolver.Value))
            {
                labelErrorModal.Text = "Rellena todos los campos y selecciona SÍ o NO.";
                labelErrorModal.Visible = true;
                return;
            }

            ENComunidad c = new ENComunidad();
            c.Titulo = txtTituloNueva.Text;
            c.Descripcion = txtDescripcionNueva.Text;
            c.Prediccion = int.Parse(ddlPrediccion.SelectedValue);
            c.Voto = hfLadoResolver.Value;

            if (c.Create())
            {
                txtTituloNueva.Text = "";
                txtDescripcionNueva.Text = "";
                labelErrorModal.Visible = false;
                cargarComunidades();
            }
            else
            {
                labelErrorModal.Text = "Error al crear la comunidad. Inténtalo de nuevo.";
                labelErrorModal.Visible = true;
            }
        }

        protected void btnConfirmarAportacion_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }

            if (!decimal.TryParse(txtAportacion.Text, out decimal cantidad) || cantidad <= 0)
            {
                labelErrorUnirse.Text = "Introduce una cantidad válida.";
                labelErrorUnirse.Visible = true;
                return;
            }

            ENUsuario user = (ENUsuario)Session["Usuario"];
            ENWallet w = new ENWallet();
            w.Id = user.wallet;
            if (w.GetSaldo() && w.Saldo < cantidad)
            {
                Response.Redirect("~/Pages/Public/Divisa.aspx");
                return;
            }

            int idComunidad = int.Parse(hfComunidadSeleccionada.Value);
            ENComunidad c = new ENComunidad();
            c.Id = idComunidad;
            if (!c.Read())
            {
                labelErrorUnirse.Text = "No se pudo cargar la comunidad.";
                labelErrorUnirse.Visible = true;
                return;
            }

            ENTransaccionApuesta ta = new ENTransaccionApuesta();
            ta.Cantidad = cantidad;
            ta.Voto = c.Voto;
            ta.Wallet = user.wallet;
            ta.Fecha = DateTime.Now;
            ta.Prediccion = c.Prediccion;
            if (!ta.Create())
            {
                labelErrorUnirse.Text = "Error al registrar la transacción.";
                labelErrorUnirse.Visible = true;
                return;
            }

            w.Saldo -= cantidad;
            w.UpdateSaldo();

            if (!c.AportarSaldo(cantidad))
            {
                labelErrorUnirse.Text = "Error al aportar a la comunidad.";
                labelErrorUnirse.Visible = true;
                return;
            }

            ENPrediccion p = new ENPrediccion();
            p.Id = c.Prediccion;
            if (p.Read())
            {
                p.CantidadRecaudada += cantidad;
                if (c.Voto == "SI")
                {
                    p.VotosSi += cantidad;
                }
                else
                {
                    p.VotosNo += cantidad;
                }

                if (!p.Update())
                {
                    labelErrorUnirse.Text = "Error al actualizar la predicción.";
                    labelErrorUnirse.Visible = true;
                    return;
                }
            }

            txtAportacion.Text = "";
            labelErrorUnirse.Visible = false;
            mostrarDetalles(idComunidad);
            cargarComunidades();
        }
    }
}