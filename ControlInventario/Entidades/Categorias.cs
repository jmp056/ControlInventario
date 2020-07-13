using System.ComponentModel.DataAnnotations;

namespace ControlInventario.Entidades
{
    class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public Categorias()
        {
            CategoriaId = 0;
            Nombre = string.Empty;
        }
    }
}
