using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Wallet
{
    /// <summary>
    /// Representa una wallet (monedero virtual) asociada a un usuario.
    /// Permite consultar, actualizar y eliminar información relacionada con el saldo.
    /// </summary>
    public class ENWallet
    {
        private decimal _saldo;

        /// <summary>
        /// Saldo disponible en la wallet.
        /// Solo permite valores mayores o iguales a cero.
        /// </summary>
        public decimal Saldo
        {
            get { return _saldo; }
            set { if (value >= 0) _saldo = value; }
        }

        private decimal _saldoRetenido;

        /// <summary>
        /// Cantidad de saldo retenido en la wallet.
        /// Este saldo no está disponible para operaciones hasta ser liberado.
        /// </summary>
        public decimal SaldoRetenido
        {
            get { return _saldoRetenido; }
            set { _saldoRetenido = value; }
        }

        private int _id;

        /// <summary>
        /// Identificador único de la wallet en la base de datos.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa el saldo, saldo retenido y asigna un ID inválido (-1)
        /// hasta que sea establecido correctamente.
        /// </summary>
        public ENWallet()
        {
            _saldo = 0;
            _saldoRetenido = 0;
            _id = -1; // Indica que aún no se ha asignado un ID válido
        }

        /// <summary>
        /// Obtiene el saldo disponible de la wallet desde la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa; false en caso contrario.</returns>
        public bool GetSaldo()
        {
            return new CADWallet().GetSaldo(this);
        }

        /// <summary>
        /// Obtiene el saldo retenido de la wallet desde la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa; false en caso contrario.</returns>
        public bool GetSaldoRetenido()
        {
            return new CADWallet().GetSaldoRetenido(this);
        }

        /// <summary>
        /// Actualiza el saldo disponible de la wallet en la base de datos.
        /// </summary>
        /// <returns>True si la actualización fue exitosa; false si no se modificó ninguna fila.</returns>
        public bool UpdateSaldo()
        {
            return new CADWallet().UpdateSaldo(this);
        }

        /// <summary>
        /// Elimina la wallet de la base de datos según su ID.
        /// </summary>
        /// <returns>True si la wallet fue eliminada correctamente; false si no existía o falló la operación.</returns>
        public bool Delete()
        {
            return new CADWallet().Delete(this);
        }
    }
}
