using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Notificacion;

namespace PredictLab
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si el usuario autenticado es administrador, se añade al menú
            // la opción para acceder a la gestión de denuncias.
            ENUsuario user = (ENUsuario)Session["Usuario"];
            if (user != null && user.admin)
            {
                MenuItem userItem = Menu1.FindItem("User");

                // Se comprueba que la opción no exista ya para evitar duplicados
                // al recargar la página.
                if (userItem != null && Menu1.FindItem("User/GestionDenuncias") == null)
                {
                    MenuItem denunciasItem = new MenuItem(
                        "Gestión de denuncias",
                        "GestionDenuncias",
                        "",
                        "~/Pages/Private/ADMIN/denuncias.aspx"
                    );
                    MenuItem panelAdmin = new MenuItem(
                        "Panel de administración",
                        "PanelAdmin",
                        "",
                        "~/Pages/Private/ADMIN/PanelAdmin.aspx"
                    );

                    userItem.ChildItems.Add(denunciasItem);
                    userItem.ChildItems.Add(panelAdmin);
                }
            }
            if (user != null)
            {
                Menu1.Items[Menu1.Items.IndexOf(Menu1.FindItem("Acceso"))].Enabled = false;

                MenuItem userItem = Menu1.FindItem("User");

                // Se comprueba que la opción no exista ya para evitar duplicados
                // al recargar la página.
                if (userItem != null && Menu1.FindItem("User/CerrarSesion") == null)
                {
                    MenuItem CerrarSesionItem = new MenuItem(
                        "Cerrar Sesion",
                        "CerrarSesion"
                    );

                    userItem.ChildItems.Add(CerrarSesionItem);
                }
            }
            if (user == null)
            {
                Menu1.Items[Menu1.Items.IndexOf(Menu1.FindItem("User"))].Enabled = false;
                Menu1.Items[Menu1.Items.IndexOf(Menu1.FindItem("Chat"))].Enabled = false;
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "CerrarSesion")
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Pages/Public/Acceso/Login.aspx");
            }
        }

    }
}