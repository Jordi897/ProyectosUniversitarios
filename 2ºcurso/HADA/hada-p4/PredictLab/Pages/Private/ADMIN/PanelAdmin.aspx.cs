using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredictLab.Pages.Private.ADMIN
{
    /// <summary>
    /// Página principal del panel de administración.
    /// Solo accesible para usuarios con permisos de administrador.
    /// Muestra estadísticas generales, usuarios destacados y predicciones relevantes.
    /// </summary>
    public partial class PanelAdmin : System.Web.UI.Page
    {
        /// <summary>
        /// Tabla en memoria con los usuarios ordenados por dinero ganado.
        /// </summary>
        private DataTable dtU;

        /// <summary>
        /// Tabla en memoria con los usuarios ordenados por gasto.
        /// </summary>
        private DataTable dtG;

        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Verifica si el usuario tiene permisos de administrador.
        /// Si es la primera carga, inicializa estadísticas y listas.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            ENUsuario user = Session["Usuario"] as ENUsuario;

            // Validación de acceso
            if (user == null || !user.admin)
            {
                Response.Redirect("../../Public/Acceso/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    // Cargar estadísticas generales
                    usuariosTotales.Text = user.ReadAll().Count.ToString();
                    usuariosConectados.Text = Application["UserCount"] != null
                        ? Application["UserCount"].ToString()
                        : "0";

                    // Cargar datos del panel
                    cargarUsuarios();
                    cargarPredicciones();
                }
            }
        }

        /// <summary>
        /// Carga las predicciones finalizadas y las predicciones destacadas.
        /// Las finalizadas son aquellas con estado "CERRADO".
        /// Las destacadas son las que más dinero han recaudado (top 10).
        /// </summary>
        private void cargarPredicciones()
        {
            ENPrediccion p = new ENPrediccion();
            List<ENPrediccion> lp = p.ReadAll();

            // Predicciones cerradas
            List<ENPrediccion> lpFinalizadas = lp
                .Where(pred => pred.Estado == "CERRADO")
                .ToList();

            // Predicciones con mayor recaudación
            List<ENPrediccion> lpDestacadas = lp
                .Where(pred => pred.CantidadRecaudada > 0)
                .OrderByDescending(pred => pred.CantidadRecaudada)
                .Take(10)
                .ToList();

            PrediccionesFinalizadas.DataSource = lpFinalizadas;
            PrediccionesFinalizadas.DataBind();

            PrediccionesDestacadas.DataSource = lpDestacadas;
            PrediccionesDestacadas.DataBind();
        }

        /// <summary>
        /// Carga los usuarios destacados según dinero ganado y dinero gastado.
        /// Si ya existen tablas en memoria, las reutiliza.
        /// Si no, genera un DataSet temporal desde la base de datos.
        /// </summary>
        private void cargarUsuarios()
        {
            ENUsuario u = new ENUsuario();

            // Si ya están cargadas las tablas, reutilizarlas
            if (dtU != null && dtG != null)
            {
                UsuariosTopGasto.DataSource = dtG;
                UsuariosTopGasto.DataBind();

                UsuariosTop.DataSource = dtU;
                UsuariosTop.DataBind();
            }
            else if (u.CreateDataSETUserByMoney())
            {
                // Obtener top 10 usuarios por dinero y por gasto
                dtU = u.GetData("Usuario", 10);
                dtG = u.GetData("Gasto", 10);

                // Limpiar dataset temporal
                u.DeleteData();

                UsuariosTopGasto.DataSource = dtG;
                UsuariosTopGasto.DataBind();

                UsuariosTop.DataSource = dtU;
                UsuariosTop.DataBind();
            }
        }

        /// <summary>
        /// Evento ejecutado al hacer clic en una predicción finalizada.
        /// Redirige a la página de detalle de la predicción.
        /// </summary>
        protected void PrediccionesFinalizadas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect($"../Prediccion.aspx?id={e.CommandArgument}");
        }

        /// <summary>
        /// Evento ejecutado al interactuar con el listado de usuarios con mayor gasto.
        /// Actualmente no implementado.
        /// </summary>
        protected void UsuariosTopGasto_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        /// <summary>
        /// Evento ejecutado al interactuar con el listado de predicciones destacadas.
        /// Actualmente no implementado.
        /// </summary>
        protected void PrediccionesDestacadas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        /// <summary>
        /// Evento ejecutado al interactuar con el listado de usuarios con mayor ganancia.
        /// Actualmente no implementado.
        /// </summary>
        protected void UsuariosTop_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}
