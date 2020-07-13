using ControlInventario.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventario.DAL
{
    class Contexto : DbContext
    {
        public DbSet<Categorias> Categoria { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<EntradaProductos> EntradaProductos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<CuadresDeCaja> CuadresDeCajas { get; set; }

        public Contexto() : base("ConStr")
        { }
    }
}
