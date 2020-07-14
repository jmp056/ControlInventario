using ControlInventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventario.BLL
{
    class ProductosBLL
    {
        public static bool ExisteDescripcion(string Descripcion)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Productos.Any(p => p.Descripcion.Equals(Descripcion)))
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
    }
}
