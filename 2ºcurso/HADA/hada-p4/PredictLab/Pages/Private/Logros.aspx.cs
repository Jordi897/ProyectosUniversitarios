using System;
using System.Collections.Generic;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Logro;

namespace PredictLab.Pages.Private
{
    public partial class Logros : System.Web.UI.Page
    {
        private string GetEmailUsuario()
        {
            ENUsuario usuario = Session["Usuario"] as ENUsuario;
            return usuario?.Email;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
            if (!IsPostBack)
            { 
                string email = GetEmailUsuario();
                if (email == null)
                {
                    Response.Redirect("~/Pages/Public/Login.aspx");
                    return;
                }

                CADLogroUsuario cadLogroUsuario = new CADLogroUsuario();



                // Asegura que el email coincide exactamente con lo que guardas en dbo.logrousuario.usuario
                string emailLimpio = (email ?? "").Trim();

                // Cargar logros
                RptLogros.DataSource = cadLogroUsuario.MostrarTodosLogros(emailLimpio);
                RptLogros.DataBind();
            }
        }
    }
}