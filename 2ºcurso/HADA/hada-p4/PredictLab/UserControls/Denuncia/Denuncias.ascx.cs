using System;
using System.Web.UI;
using LibraryEN_CAD.Denuncia;
using LibraryEN_CAD.Usuario;

namespace PredictLab.UserControls.Denuncia
{
    public partial class Denuncias : System.Web.UI.UserControl
    {
        public int ComentarioDenunciado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se obtiene desde la URL el identificador del comentario que se va a denunciar.
            if (Request.QueryString["comentario"] != null)
            {
                int comentario;
                // Si el valor del comentario es un número válido, se asigna a la propiedad ComentarioDenunciado.
                if (int.TryParse(Request.QueryString["comentario"], out comentario))
                {
                    ComentarioDenunciado = comentario;
                }
            }
        }

        /// <summary>
        /// Envía una denuncia sobre el comentario seleccionado si el formulario es válido
        /// y el usuario ha iniciado sesión.
        /// </summary>
        protected void enviarDenunciaButton_Click(object sender, EventArgs e)
        {
            ErrorText.Visible = false;
            Page.Validate("denuncia");

            if (!Page.IsValid)
            {
                return;
            }

            ENUsuario usuario = Session["Usuario"] as ENUsuario;
            if (usuario == null)
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "Debes iniciar sesión para denunciar.";
                ErrorText.Visible = true;
                return;
            }

            if (ComentarioDenunciado == 0)
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "No hay comentario seleccionado para denunciar.";
                ErrorText.Visible = true;
                return;
            }

            ENDenuncia denuncia = new ENDenuncia();

            denuncia.CausaDenuncia = causaDenuncia.SelectedItem.Text;
            denuncia.Descripcion = descripcionText.Text;
            denuncia.Fecha = DateTime.Now;
            denuncia.Estado = "PENDIENTE";
            denuncia.Emisor = usuario.Email;
            denuncia.Comentario = ComentarioDenunciado;

            // Evita que el mismo usuario denuncie varias veces el mismo comentario.
            if (denuncia.ExisteDenuncia())
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "Ya has denunciado este comentario.";
                ErrorText.Visible = true;
                return;
            }

            if (denuncia.Create())
            {
                descripcionText.Text = "";
                ErrorText.ForeColor = System.Drawing.Color.Green;
                ErrorText.Text = "Denuncia enviada correctamente.";
                ErrorText.Visible = true;
            }
            else
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "Error al enviar la denuncia.";
                ErrorText.Visible = true;
            }
        }
    }
}