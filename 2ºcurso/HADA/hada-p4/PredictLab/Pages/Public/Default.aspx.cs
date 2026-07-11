using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Usuario;
using PredictLab.Pages.Public;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryEN_CAD.Usuario;

namespace PredictLab
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                labelError.Visible = false;
                cargarPredicciones();
                cargarCategorias();
            }
        }

        protected void cargarPredicciones()
        {
            ENPrediccion p = new ENPrediccion();
            List<ENPrediccion> predicciones = p.ReadAll().Where(pred => pred.Estado != "FINALIZADO").ToList();

            if (predicciones != null && predicciones.Count > 0)
            {
                labelTotal.Text = "Predicciones: " + predicciones.Count.ToString();
                repeaterPredicciones.DataSource = predicciones;
                repeaterPredicciones.DataBind();
            }
            else
            {
                labelError.Visible = true;
            }
        }

        protected void cargarCategorias()
        {
            ENCategoria c = new ENCategoria();
            List<ENCategoria> categorias = c.ReadAll();

            if (categorias != null && categorias.Count > 0)
            {
                txtCategoria.DataSource = categorias;
                txtCategoria.DataTextField = "Categoria";
                txtCategoria.DataValueField = "categoria";
                txtCategoria.DataBind();
            }
        }

        protected void btnCrearPrediccion_Click(object sender, EventArgs e)
        {
            ENUsuario user = Session["Usuario"] as ENUsuario;
            if (user == null)
            {
                Response.Redirect("Acceso/Login.aspx");
            }
            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtPrediccion.Text) || string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(txtHora.Text))
            {
                labelModal.Visible = true;
                labelModal.Text = "Rellena todos los campos";
                return;
            }

            ENPrediccion p = new ENPrediccion();
            p.Titulo = txtTitulo.Text;
            p.Prediccion = txtPrediccion.Text;
            p.Categoria = txtCategoria.SelectedValue;
            p.FechaFin = DateTime.Parse(txtFecha.Text + " " + txtHora.Text);
            p.CantidadRecaudada = 0;
            p.Estado = "ACTIVO";
            p.VotosSi = 0;
            p.VotosNo = 0;
            p.Creador = user.Email;

            if (p.Create())
            {
                txtTitulo.Text = "";
                txtPrediccion.Text = "";
                txtFecha.Text = "";
                cargarPredicciones();
            }
            else
            {
                labelModal.Text = "Ha ocurrido un error, inténtelo de nuevo.";
                labelModal.Visible = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBox.Text))
            {
                cargarPredicciones();
                return;
            }

            ENPrediccion p = new ENPrediccion();
            p.Titulo = SearchBox.Text;

            if (p.ReadPorTitulo())
            {
                List<ENPrediccion> prediccion_filtrada = new List<ENPrediccion> { p };
                repeaterPredicciones.DataSource = prediccion_filtrada;
                repeaterPredicciones.DataBind();
            }
            else
            {
                repeaterPredicciones.DataSource = null;
                repeaterPredicciones.DataBind();
                labelError.Visible = true;
            }
        }
    }
}