using LibraryEN_CAD.Logro;
using LibraryEN_CAD.Notificacion;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Notificacion;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab
{
    /// <summary>
    /// Página de registro de nuevos usuarios.
    /// Permite crear un usuario, asignarle un logro inicial y generar una notificación de bienvenida.
    /// </summary>
    public partial class Registro : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta cada vez que se carga la página.
        /// Actualmente no realiza ninguna acción.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de registro.
        /// Valida los datos del formulario, crea un nuevo usuario si no existe,
        /// asigna un logro de bienvenida y genera una notificación inicial.
        /// </summary>
        protected void Button_Registrarse_Click(object sender, EventArgs e)
        {
            ErrorText.Visible = false;
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    // Crear objeto usuario con los datos del formulario
                    ENUsuario user = new ENUsuario
                    {
                        Email = emailText.Text,
                        nickname = nicknameText.Text,
                        nombre = nombreText.Text,
                        apellidos = ApellidosText.Text
                    };

                    // Teléfono opcional
                    if (TelefonoText.Text.Length == 0)
                        user.telefono = null;
                    else
                        user.telefono = TelefonoText.Text;

                    // Asignar contraseña
                    user.password = passwordText.Text;

                    // Verificar si el usuario ya existe
                    if (user.Read())
                    {
                        throw new Exception("Error: ya existe este usuario");
                    }

                    // Intentar crear el usuario
                    if (!user.Create())
                    {
                        throw new Exception("Error: no se pudo registrar");
                    }

                    // ---------------------------------------------------------
                    // Asignar logro de bienvenida al registrarse
                    // ---------------------------------------------------------
                    ENLogroUsuario enlogro = new ENLogroUsuario
                    {
                        Logro = "BIENVENIDA",
                        Usuario = (user.Email ?? "")
                    };

                    bool asignado = enlogro.Create();

                    // ---------------------------------------------------------
                    // Crear notificación inicial para el usuario
                    // ---------------------------------------------------------
                    ENNotificacionUsuario eNNotificacionUsuario = new ENNotificacionUsuario
                    {
                        Notificacion = 1,
                        Usuario = user.Email,
                        Leido = false,
                        Fecha = DateTime.Now
                    };

                    eNNotificacionUsuario.Create();

                    // Redirigir a login con indicador de registro exitoso
                    Response.Redirect("Login.aspx?registro=ok");
                }
                catch (Exception ex)
                {
                    // Mostrar error en pantalla
                    ErrorText.Text = ex.Message;
                    ErrorText.Visible = true;
                }
            }
        }
    }
}
