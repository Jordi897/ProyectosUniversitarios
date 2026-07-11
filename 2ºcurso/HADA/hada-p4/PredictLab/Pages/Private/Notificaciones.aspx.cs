using LibraryEN_CAD.Notificacion;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Private
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        

        private string GetUsuarioEmail()
        {
            ENUsuario usuario = Session["Usuario"] as ENUsuario;
            return usuario?.Email;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarNotificaciones();
            }
        }

        public void CargarNotificaciones()
        {
            string email = GetUsuarioEmail();
            if (string.IsNullOrEmpty(email)) { return; }

            CADNotificacionUsuario cadNotificacionUsuario = new CADNotificacionUsuario();
            RptNotificaciones.DataSource = cadNotificacionUsuario.MostrarTodas(email);
            RptNotificaciones.DataBind();
        }
        protected void BtnMarcarTodasLeidas_Click(object sender, EventArgs e)
        {   
            string email = GetUsuarioEmail();
            if (string.IsNullOrEmpty(email)) { return; }

            CADNotificacionUsuario cadNotificacionUsuario = new CADNotificacionUsuario();
            cadNotificacionUsuario.MarcarTodasComoLeidas(email);
            CargarNotificaciones();
        }

        protected void RptNotificaciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.Equals(e.CommandName, "MarcarLeida", StringComparison.OrdinalIgnoreCase))
                return;

            if (!int.TryParse(Convert.ToString(e.CommandArgument), out int idNotificacion))
                return;

            string email = GetUsuarioEmail();
            if (string.IsNullOrEmpty(email)) { return; }

            CADNotificacionUsuario cadNotificacionUsuario = new CADNotificacionUsuario();
            cadNotificacionUsuario.MarcarComoLeida(email, idNotificacion);

            CargarNotificaciones();
        }




    }
}