using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryEN_CAD.ConversionDivisa;
using LibraryEN_CAD.Transaccion;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Wallet;
using PredictLab.UserControls.Transaccion;


namespace PredictLab.Pages.Public
{

    /// <summary>
    /// Esta clase representa
    /// el code behind de Divisa.aspx
    /// </summary>
    public partial class Divisa : System.Web.UI.Page
    {
        /// <summary>
        /// Propiedad privada con la divisa elegida
        /// </summary>
        private string SessionMoneda = "Divisa_Moneda";
        /// <summary>
        /// Propiedad privada con la cantidad elegida
        /// </summary>
        private string SessionCantidad = "Divisa_Cantidad";
        /// <summary>
        /// Propiedad privada con la correspondiente cantidad de 
        /// monedas virtuales
        /// </summary>
        private string SessionMonedaVirtual = "Divisa_MonedasVirtuales";

        /// <summary>
        /// Este evento se ejecuta al cargar la página.
        /// Por defecto muestra el buscador
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Moneda.Text = "";
                Cantidad.Text = "";
                PanelDeResultado.Visible = false;
                PanelConfirmacion.Visible = false;
                PanelFinal.Visible = false;

                Session.Remove(SessionMoneda);
                Session.Remove(SessionCantidad);
                Session.Remove(SessionMonedaVirtual);
            }

        }

        /// <summary>
        /// Este evento se activa con la selección de divisa
        /// </summary>
        protected void SeleccionarDivisa(object sender, EventArgs e)
        {
            var d = (DropDownList)sender;
            if (!string.IsNullOrEmpty(d.SelectedValue))
            {
                Moneda.Text = d.SelectedValue;
            }
        }
        /// <summary>
        /// Este evento se activa con la selección de un
        /// cuadro de cantidad que sale predeterminado
        /// </summary>
        protected void CantidadRapidaClick(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Cantidad.Text = b.CommandArgument;
        }

        /// <summary>
        /// Este evento se activa al escribir en el cuadro de divisa
        /// </summary>
        protected void Moneda_TextoCambiado(object sender, EventArgs e)
        {
            PanelDeResultado.Visible = false;
        }
        /// <summary>
        /// Este evento se activa al escribir en el cuadro de cantidad
        /// </summary>
        protected void Cantidad_TextoCambiado(object sender, EventArgs e)
        {
            PanelDeResultado.Visible = false;
        }

        /// <summary>
        /// Este evento se activa al hacer click en el botón convertir.
        /// </summary>
        protected void Click_Convertir(object sender, EventArgs e)
        {
            string moneda = Moneda.Text;
            string cantidadtexto = Cantidad.Text;
            ENConversionDivisa en = new ENConversionDivisa();
            en.Moneda = moneda;
            PanelDeResultado.Visible = false;

            if (string.IsNullOrEmpty(moneda) || string.IsNullOrEmpty(cantidadtexto))
            {
                MostrarResultado("No se han completado ambos campos del buscador");
                return;
            }

            if (!decimal.TryParse(cantidadtexto, out decimal cant) || cant <= 0)
            {
                MostrarResultado("Introduzca una cantidad valida");
                return;
            }
            if (!en.Read())
            {
                MostrarResultado("No existe conversión para la moneda introducida");
                return;
            }


            decimal resultado = cant * en.ValorVirtual;
            ResultadoLabel.Text = $"{cant} {moneda} equivalen a {resultado} monedas virtuales";
            PanelDeResultado.Visible = true;

        }

        /// <summary>
        /// Este método se activa al hacer click en el botón conversión real
        /// </summary>
        protected void ClickConversionReal(object sender, EventArgs e)
        {
            PanelDeResultado.Visible = false;
            PanelFinal.Visible = false;
            ENUsuario usuario = Session["Usuario"] as ENUsuario;
            if (usuario == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
            string monedaTexto = Moneda.Text;
            string cantidadTexto = Cantidad.Text;

            if (string.IsNullOrEmpty(monedaTexto) || string.IsNullOrEmpty(cantidadTexto))
            {
                MostrarResultado("No se han completado ambos campos del buscador");
                return;
            }

            if (!decimal.TryParse(cantidadTexto, out decimal cant) || cant <= 0)
            {
                MostrarResultado("Introduzca una cantidad valida");
                return;
            }
            ENConversionDivisa en = new ENConversionDivisa();
            en.Moneda = monedaTexto;
            if (!en.Read())
            {
                MostrarResultado("No existe conversión para la moneda introducida");
                return;
            }

            decimal monedasVirtuales = cant * en.ValorVirtual;
            Session[SessionMoneda] = monedaTexto;
            Session[SessionCantidad] = cant.ToString();
            Session[SessionMonedaVirtual] = monedasVirtuales.ToString();

            ConfirmacionDivisa.Text = monedaTexto;
            ConfirmacionCantidad.Text = $"{cant} {monedaTexto}";
            ConfirmacionMonedasVirtuales.Text = $"{monedasVirtuales} monedas virtuales";

            PanelDelFormulario.Visible = false;
            PanelConfirmacion.Visible = true;

            decimal precio = cant;
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "precio",
                $"var precioPaypal = {precio.ToString().Replace(",", ".")};",
                true
            );
            string divisa = monedaTexto;
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "divisa",
                $"var divisaPaypal = {precio.ToString().Replace(",", ".")};",
                true
            );
        }

        /// <summary>
        /// Este método se activa al 
        /// hacer click en el botón confirmar
        /// tras hacer click en conversion real
        /// </summary>       
        protected void ConfirmarClick(object sender, EventArgs e)
        {
            PanelConfirmacion.Visible = false;
            PanelDelFormulario.Visible = true;


            string monedaVirtualTexto = Session[SessionMonedaVirtual] as string;
            string cantidadTexto = Session[SessionCantidad] as string;

            if (!decimal.TryParse(monedaVirtualTexto, out decimal monedasVirtuales) || !decimal.TryParse(cantidadTexto, out decimal cant))
            {
                MostrarResultadoFinal("Error en la transacción");
                return;
            }
            string moneda = Session[SessionMoneda] as string ?? string.Empty;

            ENUsuario usuario = Session["usuario"] as ENUsuario;
            if (usuario == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }

            ENTransaccionDivisa t = new ENTransaccionDivisa();
            t.Cantidad = monedasVirtuales;
            t.Fecha = DateTime.Now;
            t.Wallet = usuario.wallet;
            t.Divisa = moneda;

            if (!t.Create())
            {
                MostrarResultadoFinal("Error registrando la transacción");
                return;
            }

            ENWallet w = new ENWallet();
            w.Id = usuario.wallet;
            if (!w.GetSaldo())
            {
                MostrarResultadoFinal("Error al obtener el saldo actual");
                return;
            }

            w.Saldo += monedasVirtuales;

            if (!w.UpdateSaldo())
            {
                MostrarResultadoFinal("Error al actualizar la wallet");
                return;
            }

            Session.Remove(SessionMoneda);
            Session.Remove(SessionCantidad);
            Session.Remove(SessionMonedaVirtual);

            LimpiarCampos();
            MostrarResultadoFinal("Recarga completada correctamente");

        }
        /// <summary>
        /// Este método se activa al 
        /// hacer click en el botón cancelar
        /// tras hacer click en conversion real
        /// </summary> 
        protected void CancelarClick(object sender, EventArgs e)
        {
            Moneda.Text = Session[SessionMoneda] as string ?? "";
            Cantidad.Text = Session[SessionCantidad] as string ?? "";

            Session.Remove(SessionMoneda);
            Session.Remove(SessionCantidad);
            Session.Remove(SessionMonedaVirtual);

            PanelConfirmacion.Visible = false;
            PanelDelFormulario.Visible = true;
            PanelFinal.Visible = false;
        }

        /// <summary>
        /// Esta función muestra el contenido
        /// del texto resultado
        /// </summary>
        private void MostrarResultado(string mensaje)
        {
            PanelDeResultado.Visible = true;
            ResultadoLabel.Text = mensaje;
        }
        /// <summary>
        /// Esta función muestra el contenido
        /// del texto resultadoFinal
        /// </summary>
        private void MostrarResultadoFinal(string mensaje)
        {
            PanelFinal.Visible = true;
            ResultadoFinal.Text = mensaje;
        }
        /// <summary>
        /// Esta función se encarga de limpiar
        /// los campos moneda y cantidad
        /// </summary>
        private void LimpiarCampos()
        {
            Moneda.Text = "";
            Cantidad.Text = "";

        }
    }
}