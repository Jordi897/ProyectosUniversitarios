using LibraryEN_CAD.Usuario;
using LibraryEN_CAD.Wallet;
using System.Collections.Generic;

namespace LibraryEN_CAD.Logro
{
    public class ENLogroUsuario
    {


        private string _logro;
        public string Logro
        {
            get { return _logro; }
            set { _logro = value; }
        }

        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public ENLogroUsuario()
        {
        }
        private bool InsertarRecompensa()
        {
            ENLogro logro = new ENLogro
            {
                Titulo = this.Logro
            };
            ENUsuario eNUsuario = new ENUsuario
            {
                Email = this.Usuario
            };
            if (!eNUsuario.Read())
            {
                return false;
            }
            ENWallet eNWallet = new ENWallet
            {
                Id = eNUsuario.wallet
            };
            if (!eNWallet.GetSaldo())
            {
                return false;
            }
            eNWallet.Saldo += logro.Recompensa ?? 0;
            if (!eNWallet.UpdateSaldo())
            {
                return false;
            }
            return true;
        }

        public bool Create()
        {
            CADLogroUsuario l = new CADLogroUsuario();
            if(!InsertarRecompensa())
                return false;
            return l.Create(this);
        }

        public bool Read()
        {
            CADLogroUsuario l = new CADLogroUsuario();
            return l.Read(this);
        }

        //El metodo Update no se implementa porque no se pueden modificar ni el logro ni el usuario, ya que ambos forman la clave primaria de la tabla LogroUsuario

        public bool Delete()
        {
            CADLogroUsuario l = new CADLogroUsuario();
            return l.Delete(this);
        }




    }
}