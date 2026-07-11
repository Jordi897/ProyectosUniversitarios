using System;
using System.Data;
using System.Web.UI.WebControls;
using LibraryEN_CAD.Denuncia;
using LibraryEN_CAD.Usuario;

namespace PredictLab.Pages.Private.ADMIN
{
    public partial class denuncias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no hay usuario autenticado, se redirige al login.
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }

            // Solo los administradores pueden acceder a la gestión de denuncias.
            ENUsuario user = Session["Usuario"] as ENUsuario; // Como cast pero evita lanzar excepción si no es del tipo correcto
            if (user == null || !user.admin)
            {
                Response.Redirect("~/Pages/Public/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDenuncias();
            }
        }

        /// <summary>
        /// Carga las denuncias pendientes desde la base de datos y las muestra en el Repeater.
        /// Si no existen denuncias pendientes, muestra un mensaje informativo.
        /// </summary>
        private void CargarDenuncias()
        {
            ENDenuncia denuncia = new ENDenuncia();
            DataSet datos = denuncia.ListarPendientes();

            if (datos.Tables.Count > 0 && datos.Tables[0].Rows.Count > 0)
            {
                denunciasRepeater.DataSource = datos.Tables[0];
                denunciasRepeater.DataBind();

                denunciasRepeater.Visible = true;
                sinDenunciasLabel.Visible = false;
            }
            else
            {
                denunciasRepeater.Visible = false;
                sinDenunciasLabel.Visible = true;
            }
        }

        /// <summary>
        /// Gestiona las acciones del administrador sobre una denuncia.
        /// Permite aceptar una denuncia, eliminando el comentario denunciado,
        /// o rechazarla, eliminando únicamente la denuncia.
        /// </summary>
        protected void denunciasRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idDenuncia;

            if (!int.TryParse(e.CommandArgument.ToString(), out idDenuncia))
            {
                MostrarMensaje("No se ha podido identificar la denuncia.", false);
                return;
            }

            ENDenuncia denuncia = new ENDenuncia();
            denuncia.Id = idDenuncia;

            if (e.CommandName == "Aceptar")
            {
                if (denuncia.AceptarDenuncia())
                {
                    MostrarMensaje("Denuncia aceptada. El comentario ha sido eliminado.", true);
                }
                else
                {
                    MostrarMensaje("Error al aceptar la denuncia.", false);
                }
            }

            if (e.CommandName == "Rechazar")
            {
                if (denuncia.Delete())
                {
                    MostrarMensaje("Denuncia rechazada correctamente.", true);
                }
                else
                {
                    MostrarMensaje("Error al rechazar la denuncia.", false);
                }
            }

            CargarDenuncias();
        }

        /// <summary>
        /// Muestra un mensaje de resultado después de aceptar o rechazar una denuncia.
        /// El color del mensaje cambia según si la operación ha sido correcta o no.
        /// </summary>
        private void MostrarMensaje(string texto, bool correcto)
        {
            mensajeLabel.Text = texto;
            mensajeLabel.Visible = true;

            if (correcto)
            {
                mensajeLabel.CssClass = "d-block mb-3 text-success";
            }
            else
            {
                mensajeLabel.CssClass = "d-block mb-3 text-danger";
            }
        }
    }
}