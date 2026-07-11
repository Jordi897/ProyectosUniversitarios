using LibraryEN_CAD.ChatPrivado;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.UserControls.Chats
{
    public partial class Chats : System.Web.UI.UserControl
    {
        /// <summary>
        /// Carga la pagina vinculando los datos de la lista creada con el repetidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ENChatPrivado chat = new ENChatPrivado();
                chat.Usuario1.Email = ((ENUsuario)Session["Usuario"]).Email;

                Repeater1.DataSource = chat.ChatsUsuario();
                Repeater1.DataBind();

            }
        }
        /// <summary>
        /// Mete los datos a las etiquetas dentro del repetidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 1. Buscamos el control por su ID dentro de la fila actual
                
            }
        }
        /// <summary>
        /// Redirige la pagina y mete en nickUsuarioChat el nickname del usuario con el que se ha hecho click para mostrar el chat con ese usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Boton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string nickname = btn.Text;

            Response.Redirect("Mensajes.aspx?nickUsuarioChat=" + Server.UrlEncode(nickname));
        }
        /// <summary>
        /// Boton para buscar un usuario por su nickname y crear un chat con ese usuario, si el chat ya existe o el usuario no existe se muestra un modal con el mensaje correspondiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void botonBusqueda_Click(object sender, EventArgs e)
        {
            ENChatPrivado chat = new ENChatPrivado();
            chat.Usuario1.Email = ((ENUsuario)Session["Usuario"]).Email;
            chat.Usuario1.Read();
            chat.Usuario2.nickname = searchBox.Text;
            chat.Usuario2.Email = "";
            chat.Usuario2.Read();
            if (chat.Write())
                Response.Redirect("Mensajes.aspx?nickUsuarioChat=" + Server.UrlEncode(chat.Usuario2.nickname));
            else
            {
                if (chat.Usuario2.Read())
                {
                    etiquetaModal.Text = "Ya existe un chat con este usuario";
                }
                else
                {
                    etiquetaModal.Text = "No existe ningún usuario con ese nombre";
                }
                string script = $@"
                var modal = new bootstrap.Modal(document.getElementById('Modal'));
                modal.show();
                ";

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "mostrarModal",
                    script,
                    true
                );
            }
        }
    }
}