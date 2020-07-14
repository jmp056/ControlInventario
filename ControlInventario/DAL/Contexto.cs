using ControlInventario.Entidades;
using System.Data.Entity;

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
