using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Private
{
    /// <summary>
    /// Página de perfil del usuario.
    /// Solo es accesible si el usuario ha iniciado sesión.
    /// </summary>
    public partial class Perfil : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta cada vez que se carga la página.
        /// Verifica si existe un usuario en sesión; si no, redirige a la página de login.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
                return;
            }
        }
    }
}
