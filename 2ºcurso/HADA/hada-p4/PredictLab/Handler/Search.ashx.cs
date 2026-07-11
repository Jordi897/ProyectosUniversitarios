using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.Script.Serialization;
using LibraryEN_CAD;
using LibraryEN_CAD.Prediccion;
using LibraryEN_CAD.Usuario;
using Newtonsoft.Json;

namespace PredictLab
{
    /// <summary>
    /// Manejador HTTP encargado de procesar solicitudes de búsqueda
    /// y devolver sugerencias en formato JSON.
    /// 
    /// Este handler recibe un cuerpo JSON con:
    /// - "tipo": indica el tipo de búsqueda ("predicciones" o "usuario")
    /// - "query": texto parcial introducido por el usuario
    /// 
    /// Devuelve una lista de sugerencias (máximo 10) en formato JSON.
    /// </summary>
    public class Search : IHttpHandler
    {
        /// <summary>
        /// Procesa la solicitud HTTP entrante.
        /// Lee el cuerpo JSON, determina el tipo de búsqueda solicitado
        /// y devuelve una lista de sugerencias en formato JSON.
        /// </summary>
        /// <param name="context">Contexto HTTP de la solicitud.</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            // Leer cuerpo JSON enviado por POST
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string body = reader.ReadToEnd();

            // Deserializar JSON recibido
            Dictionary<string, string> json =
                new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(body);

            List<string> sugerencias = new List<string>();

            // Determinar tipo de búsqueda
            switch (json["tipo"])
            {
                case "predicciones":
                    ENPrediccion prediccion = new ENPrediccion();
                    sugerencias = prediccion.Sugerencias(json["query"])
                                             .Take(10)
                                             .ToList();
                    break;

                case "usuario":
                    ENUsuario user = new ENUsuario();
                    sugerencias = user.Sugerencias(json["query"])
                                      .Take(10)
                                      .ToList();
                    break;
            }

            // Serializar respuesta a JSON
            string newjson = JsonConvert.SerializeObject(sugerencias, Formatting.Indented);
            context.Response.Write(newjson);
        }

        /// <summary>
        /// Indica si el handler puede reutilizarse para múltiples solicitudes.
        /// En este caso, siempre devuelve false.
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }
    }
}
