using ControlInventario.BLL;
using ControlInventario.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlInventario.UI.Registros
{
    public partial class rProductos : Form
    {
        int ProductoId;
        public rProductos(int productoId)
        {
            this.ProductoId = productoId;
            InitializeComponent();
            Limpiar();
        }

        private void rProductos_Load(object sender, EventArgs e)
        {
            FechaDeRegistroDateTimePicker.Value = DateTime.Now;

            LlenaComboBoxCategorias();

            if (ProductoId > 0)
            {
                ProductoIdNumericUpDown.Value = ProductoId;
                Buscar();
            }

            CategoriaComboBox.DropDownWidth = 233;
            MyToolTip.SetToolTip(AnadirCategoriasButton, "Agregar una nueva categoría");
        }

        private void rProductos_Activated(object sender, EventArgs e)
        {
            if (ProductoId <= 0)
            {
                LlenaComboBoxCategorias();
                CategoriaComboBox.SelectedIndex = -1;
            }
        }

        private void Limpiar()// Funcion encargada de limpiar todos los campos del registro
        {
            ProductoIdNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            CategoriaComboBox.SelectedIndex = -1;
            ControlAlmacenCheckBox.Checked = true;
            CantidadNumericUpDown.Value = 0;
            PrecioNumericUpDown.Value = 0;
            FechaDeRegistroDateTimePicker.Value = DateTime.Now;
            MyErrorProvider.Clear();

            ProductoIdNumericUpDown.Enabled = true;
            BuscarButton.Enabled = true;
        }

        private Productos LlenaClase()// Funcion encargada de llenar el objeto
        {
            Productos Producto = new Productos();

            Producto.ProductoId = (int)ProductoIdNumericUpDown.Value;
            Producto.Descripcion = DescripcionTextBox.Text;
            Producto.CategoriaId = Convert.ToInt32(CategoriaComboBox.SelectedValue);
            Producto.ControlAlmacen = ControlAlmacenCheckBox.Checked;
            Producto.Cantidad = Convert.ToInt32(CantidadNumericUpDown.Value);
            Producto.Precio = Convert.ToInt32(PrecioNumericUpDown.Value);
            Producto.FechaDeRegistro = FechaDeRegistroDateTimePicker.Value;

            return Producto;
        }

        private void LlenaCampo(Productos Producto)// Funcion encargada de llenar los campos del registro con los datos de un objeto
        {
                ProductoIdNumericUpDown.Value = Producto.ProductoId;
                DescripcionTextBox.Text = Producto.Descripcion;
                CategoriaComboBox.SelectedValue = Producto.CategoriaId;
                ControlAlmacenCheckBox.Checked = Producto.ControlAlmacen;
                CantidadNumericUpDown.Value = Producto.Cantidad;
                PrecioNumericUpDown.Value = Convert.ToDecimal(Producto.Precio);
                FechaDeRegistroDateTimePicker.Value = Producto.FechaDeRegistro;

        }

        public void LlenaComboBoxCategorias() // Funcion encargada de llenar el ComboBox de las categorias
        {
            RepositorioBase<Categorias> Repositorio = new RepositorioBase<Categorias>();
            var categorias = new List<Categorias>();
            categorias = Repositorio.GetList(p => true);
            CategoriaComboBox.DataSource = categorias;
            CategoriaComboBox.ValueMember = "CategoriaId";
            CategoriaComboBox.DisplayMember = "Nombre";
        }

        private bool Validar()//Funcion que valida todo el registro
        {
            bool Paso = true;

            MyErrorProvider.Clear();

            if (DescripcionTextBox.Text == string.Empty) // Condicion encargada de validar que el campo descripcion no este vacio
            {
                MyErrorProvider.SetError(DescripcionTextBox, "La descripción no puede estar vacía!");
                DescripcionTextBox.Focus();
                Paso = false;
            }
            else // Validando que la descripcion del producto no este duplicada
            {
                if (ProductoIdNumericUpDown.Value == 0 || Convert.ToString(ProductoIdNumericUpDown.Value) == string.Empty) // Validando que la descripcion no exista, en caso de registrar un producto nuevo
                {
                    if (ProductosBLL.ExisteDescripcion(DescripcionTextBox.Text) == true)
                    {
                        MyErrorProvider.SetError(DescripcionTextBox, "Ya existe un producto con esta descripción, debe ingresar una diferente!");
                        DescripcionTextBox.Focus();
                        Paso = false;
                    }
                }
                else //Valida que si al modificar un producto, la descripcion sea unica
                {
                    RepositorioBase<Productos> Repositorio = new RepositorioBase<Productos>();
                    var Listado = new List<Productos>();

                    Listado = Repositorio.GetList(p => p.Descripcion.Contains(DescripcionTextBox.Text));

                    if (Listado.Count == 1) // Confirma que solo exista un producto con esa descripcion
                    {
                        Productos ProductoTemporal = new Productos();
                        ProductoTemporal = Listado[0];
                        if (ProductoTemporal.ProductoId != ProductoIdNumericUpDown.Value) // Verifica si el producto que tiene esa descripcion en la base de datos no es al que se esta modificando!
                        {
                            MyErrorProvider.SetError(DescripcionTextBox, "Ya existe un producto con esta descripción, debe ingresar una diferente!");
                            DescripcionTextBox.Focus();
                            Paso = false;
                        }
                    }
                    else if (Listado.Count > 1)
                    {
                        MyErrorProvider.SetError(DescripcionTextBox, "Ya existe un producto con esta descripción, debe ingresar una diferente!");
                        DescripcionTextBox.Focus();
                        Paso = false;
                    }
                }
            }

            if (CategoriaComboBox.SelectedIndex < 0) // Condicion encargada de validar que haya una categoria seleccionada
            {
                MyErrorProvider.SetError(CategoriaComboBox, "Debe seleccionar una categoría!");
                CategoriaComboBox.Focus();
                Paso = false;
            }

            if (CantidadNumericUpDown.Value < 0) // Condicion encargada de validar que la cantidad no sea menor que 0
            {
                MyErrorProvider.SetError(CantidadNumericUpDown, "La cantidad disponible debe no puede ser menor que 0!");
                CantidadNumericUpDown.Focus();
                Paso = false;
            }

            if (PrecioNumericUpDown.Value <= 0) // Condicion encargada de validar que el precio sea mayor a 0
            {
                MyErrorProvider.SetError(PrecioNumericUpDown, "El precio del producto debe ser mayor a 0!");
                PrecioNumericUpDown.Focus();
                Paso = false;
            }

            return Paso;
        }
    }
}
