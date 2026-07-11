using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD.Prediccion
{
    public class ENCategoria
    {
        private string _categoria;
        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public ENCategoria() { }

        public bool Create()
        {
            CADCategoria c = new CADCategoria();
            return c.Create(this);
        }

        public List<ENCategoria> ReadAll()
        {
            CADCategoria c = new CADCategoria();
            return c.ReadAll();
        }
    }
}
