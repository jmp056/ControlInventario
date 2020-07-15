using ControlInventario.DAL;
using ControlInventario.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventario.BLL
{
    class FacturasBLL
    {
        public static bool Guardar(Facturas Factura)
        {

            bool Paso = false;
            Contexto contexto = new Contexto();
            Productos Producto = new Productos();

            try
            {

                if (contexto.Facturas.Add(Factura) != null)
                {
                    foreach (var item in Factura.Detalle)
                    {
                        contexto.Productos.Find(item.ProductoId).Cantidad -= item.Cantidad;
                    }
                }
                
                Paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                contexto.Dispose();
            }

            return Paso;
        }


        public static bool Modificar(Facturas Factura)
        {

            bool Paso = false;
            Contexto contexto = new Contexto();
            RepositorioBase<Facturas> Repositorio = new RepositorioBase<Facturas>();
            Productos Producto = new Productos();

            try
            {

                Facturas FacturaAnterior = contexto.Facturas.Where(e => e.FacturaId == Factura.FacturaId).Include(d => d.Detalle).FirstOrDefault();

                contexto = new Contexto();
                foreach (var item in FacturaAnterior.Detalle)
                {
                    if (!Factura.Detalle.Any(d => d.DetalleFacturaId == item.DetalleFacturaId))
                    {
                        contexto.Productos.Find(item.ProductoId).Cantidad += item.Cantidad;
                        contexto.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in Factura.Detalle)
                {
                    if (item.DetalleFacturaId == 0)
                    {
                        contexto.Productos.Find(item.ProductoId).Cantidad -= item.Cantidad;
                        contexto.Entry(item).State = EntityState.Added;

                    }
                    else
                    {
                        contexto.Entry(item).State = EntityState.Modified;

                    }
                }
                contexto.Entry(Factura).State = EntityState.Modified;
                Paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                contexto.Dispose();
            }

            return Paso;
        }


        public static bool Eliminar(int Id)
        {

            bool Paso = false;
            Contexto contexto = new Contexto();

            try
            {

                Facturas Factura = contexto.Facturas.Where(e => e.FacturaId == Id).Include(d => d.Detalle).FirstOrDefault();

                foreach (var item in Factura.Detalle)
                {
                    contexto.Productos.Find(item.ProductoId).Cantidad += item.Cantidad;

                }
                contexto.Facturas.Remove(Factura);
                Paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                contexto.Dispose();
            }

            return Paso;
        }

        public static Facturas Buscar(int id)
        {

            Contexto contexto = new Contexto();
            Facturas Factura = new Facturas();
            try
            {

                Factura = contexto.Facturas.Where(e => e.FacturaId == id).Include(d => d.Detalle).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                contexto.Dispose();
            }

            return Factura;
        }

        public List<Facturas> GetList(Expression<Func<Facturas, bool>> expression)
        {
            Contexto contexto = new Contexto();
            List<Facturas> lista;

            try
            {
                lista = contexto.Facturas.Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

    }
}
