using LibraryEN_CAD.ChatPrivado;
using LibraryEN_CAD.Mensaje;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PredictLab.UserControls.Mensaje
{
    public partial class Mensaje : System.Web.UI.UserControl
    {
        /// <summary>
        /// Carga la pagina y bincula la lista con el repetidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ENChatPrivado chat = new ENChatPrivado();
                chat.Usuario1.Email= ((ENUsuario)Session["Usuario"]).Email;
                chat.Usuario2.nickname = Request.QueryString["nickUsuarioChat"];
                chat.Usuario2.Email ="";
                Chat.Text = Request.QueryString["nickUsuarioChat"];
                if (!(chat.Usuario1 == null) && !(chat.Usuario2 == null))
                {
                    chat.Usuario1.Read();
                    chat.Usuario2.Read();
                }
                chat.Read();

                Repeater1.DataSource = chat.ReadMensajes();
                Repeater1.DataBind();
            }

        }
        /// <summary>
        /// Mete los datos a las etiquetas del repetidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    // 1. Buscamos el control por su ID dentro de la fila actual
                    ENMensaje data = (ENMensaje)e.Item.DataItem;
                    
                    data.Emisor.Read();
                    Label usuario = (Label)e.Item.FindControl("UsuarioNick");
                    Label texto = (Label)e.Item.FindControl("Texto");
                    Label fecha = (Label)e.Item.FindControl("Fecha");
                    HtmlGenericControl origen = (HtmlGenericControl)e.Item.FindControl("Mensaje");
                    if (((ENUsuario)Session["Usuario"]).Email == data.Emisor.Email)
                    {
                        origen.Attributes["data-origen"] = "Yo";

                    }
                    else
                    {
                        origen.Attributes["data-origen"] = "Tu";
                    } 
                    usuario.Text = data.Emisor.nickname;
                    texto.Text = data.Contenido;
                    fecha.Text = data.Fecha.ToString("g");
                }
            
        }
        /// <summary>
        /// Envia un mensaje y refresca la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnEnviar_Click(object sender, EventArgs e)
        {
            ENChatPrivado chat = new ENChatPrivado();
            ENMensaje mensaje = new ENMensaje();
            chat.Usuario1.Email = ((ENUsuario)Session["Usuario"]).Email;
            chat.Usuario2.nickname = Request.QueryString["nickUsuarioChat"];
            chat.Usuario2.Email = "";
            chat.Usuario2.Read();
            chat.Read();
            mensaje.Emisor = (ENUsuario)Session["Usuario"];
            mensaje.Chat1 = chat.Usuario1;
            mensaje.Chat2 = chat.Usuario2;
          
            string modificada = txtNuevoMensaje.Text.Replace("\n", "<br>");
            
            mensaje.Contenido = modificada;
            mensaje.Write();
            txtNuevoMensaje.Text = "";


            Response.Redirect(Request.RawUrl);
        }



    }
}