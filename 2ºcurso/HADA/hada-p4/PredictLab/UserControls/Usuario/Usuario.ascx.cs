using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryEN_CAD.Notificacion;


namespace PredictLab.UserControls.Usuario
{
    /// <summary>
    /// Control de usuario encargado de mostrar la información del usuario logueado,
    /// su imagen de perfil y sus notificaciones no leídas.
    /// </summary>
    public partial class Usuario : System.Web.UI.UserControl
    {

        /// <summary>
        /// Evento que se ejecuta cada vez que se carga el control.
        /// Carga los datos del usuario, su imagen de perfil, las notificaciones
        /// y actualiza el contador de notificaciones no leídas.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ENUsuario user = (ENUsuario)Session["Usuario"];
                if (user != null)
                {
                    NickName.Text = user.nickname;
                    NombreApellidos.Text = $"{user.nombre} {user.apellidos}";
                    Wallet.Text = $"{user.GetSaldo()} Coins";

                    string folder = Server.MapPath("~/FileUploads/img/");
                    string email = Regex.Replace(user.Email, "[^a-zA-Z0-9]", "_");

                    foreach (string item in (string[])Application["ImgPermitidas"])
                    {
                        string ruta = Path.Combine(folder, email + item);
                        if (File.Exists(ruta))
                        {
                            ImagePerfil.ImageUrl = "~/FileUploads/img/" + email + item;
                        }
                    }
                }

                if (!IsPostBack)
                {
                    BindNotificaciones();
                }

                UpdateBadge();
            }
            catch (Exception ex)
            {
                // Manejo de errores, log, etc.
                Console.WriteLine("Error en Usuario.ascx: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el email del usuario almacenado en sesión.
        /// Devuelve una cadena vacía si no existe usuario.
        /// </summary>
        /// <returns>Email del usuario o cadena vacía.</returns>
        private string GetUsuarioEmail()
        {
            ENUsuario user = Session["Usuario"] as ENUsuario;
            return (user?.Email ?? "").Trim();
        }

        /// <summary>
        /// Carga las notificaciones no leídas del usuario y las enlaza al Repeater.
        /// Solo se muestran las últimas 5.
        /// </summary>
        private void BindNotificaciones()
        {
            try
            {
                string email = GetUsuarioEmail();
                if (string.IsNullOrEmpty(email)) { return; }

                CADNotificacionUsuario cadNotificacion = new CADNotificacionUsuario();
                RepeaterNotificaciones.DataSource = cadNotificacion.MostrarNoLeidas(email, 5);
                RepeaterNotificaciones.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar notificaciones: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el contador visual (badge) de notificaciones no leídas.
        /// Si no hay notificaciones, el badge se oculta.
        /// </summary>
        private void UpdateBadge()
        {
            try
            {
                string email = GetUsuarioEmail();
                if (string.IsNullOrEmpty(email)) { return; }

                CADNotificacionUsuario cadNotificacion = new CADNotificacionUsuario();
                int unreadCount = cadNotificacion.ContarNotificacionesNoLeidas(email);

                LblNumNotis.Text = unreadCount > 0 ? unreadCount.ToString() : "";
                LblNumNotis.Visible = unreadCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar badge: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja los comandos ejecutados desde el Repeater de notificaciones.
        /// Permite marcar una notificación como leída.
        /// </summary>
        /// <param name="source">Origen del evento.</param>
        /// <param name="e">Argumentos del comando ejecutado.</param>
        protected void RepeaterNotis_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e == null) return;

            // Mantengo el CommandName del .ascx ("MarcarLeida")
            if (!string.Equals(e.CommandName, "MarcarLeida", StringComparison.OrdinalIgnoreCase))
                return;

            if (!int.TryParse(Convert.ToString(e.CommandArgument), out int id))
                return;

            string email = GetUsuarioEmail();
            if (string.IsNullOrEmpty(email)) { return; }

            CADNotificacionUsuario cadNotificacion = new CADNotificacionUsuario();
            cadNotificacion.MarcarComoLeida(email, id);

            BindNotificaciones();
            UpdateBadge();
        }
    }
}

