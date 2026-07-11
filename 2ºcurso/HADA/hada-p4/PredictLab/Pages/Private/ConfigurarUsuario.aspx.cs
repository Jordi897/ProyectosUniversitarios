using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Private
{
    public partial class configurarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Usuario"] == null)
            {
                Response.Redirect("../Public/Acceso/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    // Cargar datos del usuario en los campos del formulario
                    var usuario = (LibraryEN_CAD.Usuario.ENUsuario)Session["Usuario"];
                    emailText.Text = usuario.Email;
                    nicknameText.Text = usuario.nickname;
                    nombreText.Text = usuario.nombre;
                    ApellidosText.Text = usuario.apellidos;
                    TelefonoText.Text = usuario.telefono ?? string.Empty;
                    try
                    {
                        string folder = Server.MapPath("~/FileUploads/img/");
                        string email = Regex.Replace(usuario.Email, "[^a-zA-Z0-9]", "_");

                        foreach (string item in (string[])Application["ImgPermitidas"]) 
                        {
                            string ruta = Path.Combine(folder, email+item);
                            if (File.Exists(ruta))
                            {
                                ImagePerfil.ImageUrl = "~/FileUploads/img/" + email + item;
                            }
                        }
                    }
                    catch(Exception ex) 
                    {
                        ErrorText.Visible = true;
                        ErrorText.Text = ex.Message;
                        return;
                    }
                }
            }
        }

        private bool FileValidate()
        {
            string extension = Path.GetExtension(FileFoto.FileName).ToLower();
            string[] permitidas = (string[])Application["ImgPermitidas"];
            return permitidas.Contains(extension);
        }

        protected void GuardarCambios_Click(object sender, EventArgs e)
        {
            ErrorText.Visible = false;
            Page.Validate();
            if (Page.IsValid)
            {
                if (FileFoto.HasFile)
                {
                    if (!FileValidate())
                    {
                        ErrorText.Visible = true;
                        ErrorText.Text = "Error la imagen que se subio no es del formato imagen: .jpg, .png, .gif, jpeg";
                        return;
                    }
                    try
                    {
                        string folder = Server.MapPath("~/FileUploads/img/");
                        string extension = Path.GetExtension(FileFoto.FileName);
                        ENUsuario user = (ENUsuario)Session["Usuario"];
                        string filename = Regex.Replace(user.Email,"[^a-zA-Z0-9]","_") + extension;
                        string path = Path.Combine(folder, filename);
                        FileFoto.SaveAs(path);
                        ImagePerfil.ImageUrl = "~/FileUploads/img/" + filename;
                    }
                    catch (Exception ex)
                    {
                        ErrorText.Visible = true;
                        ErrorText.Text = ex.Message;
                        return;
                    }
                }
                ENUsuario usuario = (ENUsuario)Session["Usuario"];
                usuario.Email = emailText.Text;
                usuario.nombre = nicknameText.Text;
                usuario.nombre = nombreText.Text;
                usuario.apellidos = ApellidosText.Text;
                usuario.telefono = TelefonoText.Text;
                usuario.Update();
                Session["Usuario"] = usuario; // Actualizar sesión con los nuevos datos
            }
        }
    }
}