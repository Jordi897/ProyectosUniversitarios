using LibraryEN_CAD.Usuario;
using PredictLab.UserControls.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab
{
    /// <summary>
    /// Página de inicio de sesión del sistema.
    /// Permite validar las credenciales del usuario y almacenarlo en sesión.
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta cada vez que se carga la página.
        /// Si el usuario viene de un registro exitoso (registro=ok) y la página
        /// se está recargando por un PostBack, se redirige nuevamente a Login.aspx.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["registro"] == "ok" && IsPostBack)
            {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de inicio de sesión.
        /// Valida los datos del formulario, comprueba si el usuario existe,
        /// verifica la contraseña y, si es correcto, lo guarda en sesión.
        /// </summary>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            ErrorText.Visible = false;
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    // Se crea un objeto usuario con los datos introducidos
                    ENUsuario user = new ENUsuario
                    {
                        Email = emailUserText.Text,
                        nickname = emailUserText.Text
                    };

                    // Verifica si el usuario existe en la base de datos
                    if (!user.Read())
                    {
                        throw new Exception("Error: no existe el usuario");
                    }

                    // Verifica si la contraseña es correcta
                    if (!user.VerifyPassword(passwordText.Text))
                    {
                        throw new Exception("Error: la contraseña es incorrecta");
                    }

                    Session["Usuario"] = user;
                    Session["Wallet"] = user.wallet;

                    // Redirección a la página principal
                    Response.Redirect("../Default.aspx");
                }
                catch (Exception ex)
                {
                    // Muestra el error en pantalla
                    ErrorText.Text = ex.Message;
                    ErrorText.Visible = true;
                }
            }
        }
    }
}
