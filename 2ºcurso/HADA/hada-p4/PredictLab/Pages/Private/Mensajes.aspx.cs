using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Private
{
    public partial class Mensajes : System.Web.UI.Page
    {
        /// <summary>
        /// Dependiendo de si hay un chat seleccionado o no, se muestra el panel de mensajes o no. Si no hay un usuario con sesion y intenta acceder a esta pagina, se redirige al login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
            }
            if (Request.QueryString["nickUsuarioChat"] == null)
            {
                MensajesChat.Visible = false;
            }
            else
            {
                MensajesChat.Visible = true;
            }
        }
        
    }
}