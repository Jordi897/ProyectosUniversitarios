using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEN_CAD
{
    public interface Busqueda
    {
        /// <summary>
        /// Devuelve una lista de sugerencias que quieres que devuelva tu clase cuando se busque algo
        /// </summary>
        /// <returns>Una lista de string con todas las sugerencias</returns>
        List<string> Sugerencias(string query);
    }
}
