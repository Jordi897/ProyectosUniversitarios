using System;
using System.Web.UI.WebControls;
using LibraryEN_CAD.Comentario;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Denuncia;

namespace PredictLab.UserControls.Comentario
{
    public partial class Comentarios : System.Web.UI.UserControl
    {
        public int PrediccionActual { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se obtiene el identificador de la predicción desde la URL.
            string valorPrediccion = Request.QueryString["id"];

            if (valorPrediccion == null)
            {
                valorPrediccion = Request.QueryString["prediccion"];
            }

            int prediccion;

            // Si el valor de la predicción es un número válido, se asigna a la propiedad PrediccionActual.
            if (int.TryParse(valorPrediccion, out prediccion))
            {
                PrediccionActual = prediccion;
            }

            // Solo se cargan los comentarios la primera vez que se carga la página, no en cada postback.
            if (!IsPostBack)
            {
                CargarComentarios();
            }

            // Si la URL contiene un comentario seleccionado, se muestra el formulario de denuncia.
            if (Request.QueryString["comentario"] != null)
            {
                panelDenunciaComentario.Visible = true;
            }
        }

        /// <summary>
        /// Carga los comentarios asociados a la predicción actual y los muestra en el Repeater.
        /// </summary>
        private void CargarComentarios()
        {
            if (PrediccionActual == 0)
            {
                return;
            }

            ENComentario comentario = new ENComentario();
            comentario.Prediccion = PrediccionActual;

            System.Data.DataSet datos = comentario.ListarPorPrediccion();

            // Si se han obtenido comentarios, se asignan al Repeater para su visualización.
            if (datos.Tables.Count > 0)
            {
                comentariosRepeater.DataSource = datos.Tables[0];
                comentariosRepeater.DataBind();
            }
        }

        /// <summary>
        /// (Función auxiliar) 
        /// Comprueba si el usuario autenticado puede modificar un comentario.
        /// Solo el autor del comentario puede ver las opciones de modificación.
        /// </summary>
        protected bool PuedeModificarComentario(object usuarioComentario)
        {
            ENUsuario usuario = Session["Usuario"] as ENUsuario;

            if (usuario == null)
            {
                return false;
            }

            if (usuarioComentario == null)
            {
                return false;
            }

            return usuario.Email == usuarioComentario.ToString();
        }

        /// <summary>
        /// Publica un nuevo comentario si el formulario es válido, existe una predicción seleccionada
        /// y el usuario ha iniciado sesión.
        /// </summary>
        protected void publicarButton_Click(object sender, EventArgs e)
        {
            ErrorText.Visible = false;

            Page.Validate("comentarios");

            if (!Page.IsValid)
            {
                return;
            }

            if (PrediccionActual == 0)
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "No hay predicción seleccionada.";
                ErrorText.Visible = true;
                return;
            }

            ENUsuario usuario = Session["Usuario"] as ENUsuario;
            if (usuario == null)
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "Debes iniciar sesión para comentar.";
                ErrorText.Visible = true;
                return;
            }

            ENComentario comentario = new ENComentario();
            comentario.Mensaje = comentarioText.Text;
            comentario.Fecha = DateTime.Now;
            comentario.Usuario = usuario.Email;
            comentario.Prediccion = PrediccionActual;

            if (comentario.Create())
            {
                comentarioText.Text = "";

                ErrorText.ForeColor = System.Drawing.Color.Green;
                ErrorText.Text = "Comentario publicado correctamente.";
                ErrorText.Visible = true;
                CargarComentarios();
            }
            else
            {
                ErrorText.ForeColor = System.Drawing.Color.Red;
                ErrorText.Text = "Error al publicar el comentario.";
                ErrorText.Visible = true;
            }
        }

        /// <summary>
        /// Gestiona las acciones disponibles sobre cada comentario del listado:
        /// denunciar un comentario o borrar un comentario del propio usuario.
        /// </summary>
        protected void comentariosRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Denunciar")
            {
                int idComentario;

                if (int.TryParse(e.CommandArgument.ToString(), out idComentario))
                {
                    Response.Redirect("Prediccion.aspx?id=" + PrediccionActual + "&comentario=" + idComentario);
                }
            }
            else if (e.CommandName == "Borrar")
            {
                int idComentario;

                if (int.TryParse(e.CommandArgument.ToString(), out idComentario))
                {
                    ENUsuario usuario = Session["Usuario"] as ENUsuario;

                    if (usuario == null)
                    {
                        ErrorText.ForeColor = System.Drawing.Color.Red;
                        ErrorText.Text = "Debes iniciar sesión para borrar comentarios.";
                        ErrorText.Visible = true;
                        return;
                    }

                    ENComentario comentario = new ENComentario();
                    comentario.Id = idComentario;

                    if (comentario.Read())
                    {
                        if (comentario.Usuario != usuario.Email)
                        {
                            ErrorText.ForeColor = System.Drawing.Color.Red;
                            ErrorText.Text = "Solo puedes borrar tus propios comentarios.";
                            ErrorText.Visible = true;
                            return;
                        }

                        ENDenuncia denuncia = new ENDenuncia();
                        denuncia.Comentario = idComentario;
                        denuncia.DeletePorComentario();

                        if (comentario.Delete())
                        {
                            ErrorText.ForeColor = System.Drawing.Color.Green;
                            ErrorText.Text = "Comentario borrado correctamente.";
                            ErrorText.Visible = true;
                            CargarComentarios();
                        }
                        else
                        {
                            ErrorText.ForeColor = System.Drawing.Color.Red;
                            ErrorText.Text = "Error al borrar el comentario.";
                            ErrorText.Visible = true;
                        }
                    }
                    else
                    {
                        ErrorText.ForeColor = System.Drawing.Color.Red;
                        ErrorText.Text = "No se ha encontrado el comentario.";
                        ErrorText.Visible = true;
                    }
                }
            }
        }
    }
}