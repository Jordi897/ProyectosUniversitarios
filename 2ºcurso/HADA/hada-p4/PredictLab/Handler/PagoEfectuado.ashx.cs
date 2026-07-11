using LibraryEN_CAD.ConversionDivisa;
using LibraryEN_CAD.Transaccion;
using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Wallet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace PredictLab.Handler
{
    /// <summary>
    /// Handler encargado de procesar pagos efectuados desde un servicio externo.
    /// Recibe datos en formato JSON, valida al usuario, registra la transacción
    /// y actualiza el saldo de su wallet.
    /// </summary>
    public class PagoEfectuado : IHttpHandler
    {
        /// <summary>
        /// Procesa la solicitud HTTP entrante.
        /// Espera un cuerpo JSON con los campos:
        /// - amount: cantidad pagada
        /// - currency: divisa utilizada
        /// - usuario: email del usuario
        /// 
        /// El flujo es:
        /// 1. Leer JSON recibido.
        /// 2. Validar usuario.
        /// 3. Obtener valor virtual según la divisa.
        /// 4. Registrar transacción de divisa.
        /// 5. Actualizar saldo del wallet.
        /// </summary>
        /// <param name="context">Contexto HTTP de la solicitud.</param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";

                // Leer cuerpo JSON
                StreamReader reader = new StreamReader(context.Request.InputStream);
                string body = reader.ReadToEnd();

                Dictionary<string, string> json =
                    new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(body);

                Decimal cantidad = Decimal.Parse(json["amount"], CultureInfo.InvariantCulture);
                string divisa = json["currency"];

                // Buscar usuario
                ENUsuario user = new ENUsuario();
                user.Email = json["usuario"];

                if (!user.Read())
                {
                    context.Response.Write("Error: Usuario no encontrado");
                    return;
                }

                // Leer conversión de divisa
                ENConversionDivisa cd = new ENConversionDivisa();
                cd.Moneda = divisa;

                if (cd.Read())
                {
                    Decimal valorVirtual = cd.ValorVirtual * cantidad;

                    ENWallet wallet = new ENWallet();
                    ENTransaccionDivisa Tdivisa = new ENTransaccionDivisa();

                    // Registrar transacción
                    Tdivisa.Cantidad = cantidad;
                    Tdivisa.Divisa = divisa;
                    Tdivisa.Wallet = user.wallet;
                    Tdivisa.Fecha = DateTime.Now;

                    if (Tdivisa.Create())
                    {
                        wallet.Id = user.wallet;

                        // Actualizar saldo
                        if (wallet.GetSaldo())
                        {
                            wallet.Saldo += valorVirtual;
                            wallet.UpdateSaldo();

                            context.Response.Write("Correcto");
                            return;
                        }
                    }
                }

                context.Response.Write("Error");
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
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
