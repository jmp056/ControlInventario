using ControlInventario.BLL;
using ControlInventario.Entidades;
using ControlInventario.Entidades.EntidadesParaConsultas;
using ControlInventario.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlInventario.UI.Consultas
{
    public partial class cProductos : Form
    {
        public int IdProductoeleccionado { get; set; }
        private List<Productos> ListadoProductos = new List<Productos>();
        private List<ProductosConsulta> ListadoProductosConsulta = new List<ProductosConsulta>();

        public cProductos()
        {
            InitializeComponent();
        }

        private bool Validar()//Funcion encargada de validar la busqueda 
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (FiltroComboBox.SelectedIndex > 0 && FiltroComboBox.SelectedIndex <= 3)
            {
                if (CriterioTextBox.Text == string.Empty)
                {
                    CriterioTextBox.Width = 160;
                    MyErrorProvider.SetError(CriterioTextBox, "Debe escribir algún criterio de búsqueda!");
                    CriterioTextBox.Focus();
                    paso = false;
                }
                else if (FiltroComboBox.SelectedIndex == 1 && CriterioTextBox.Text.Any(x => !char.IsNumber(x)))
                {
                    CriterioTextBox.Width = 160;
                    MyErrorProvider.SetError(CriterioTextBox, "Si desea filtrar por código, solo digite números!");
                    CriterioTextBox.Focus();
                    paso = false;
                }
            }
            else if (FiltroComboBox.SelectedIndex >= 4)
            {
                if (DesdeNumericUpDown.Value > HastaNumericUpDown.Value)
                {
                    MyErrorProvider.SetError(DesdeNumericUpDown, "El valor inicial no puede ser mayor al valor limite!");
                    DesdeNumericUpDown.Focus();
                    paso = false;
                }
            }

            return paso;
        }

        private List<ProductosConsulta> CargarLista(List<Productos> ListaSinProcesar) // Funcion que intercambia el codigo de la categoria del producto porel nombre de la categoria
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
            List<ProductosConsulta> ListaProcesada = new List<ProductosConsulta>();

            foreach (var item in ListaSinProcesar)
            {
                ProductosConsulta p = new ProductosConsulta();
                p.ProductoId = item.ProductoId;
                p.Descripcion = item.Descripcion;
                Categorias c = repositorio.Buscar(item.CategoriaId);
                p.Categoria = c.Nombre;
                p.Cantidad = item.Cantidad;
                p.Precio = item.Precio;

                ListaProcesada.Add(p);
            }

            return ListaProcesada;
        }

        private void Buscar() // Funcion que realiza las bsquedas
        {
            RepositorioBase<Productos> Repositorio = new RepositorioBase<Productos>();
            ListadoProductos = new List<Productos>();
            ListadoProductos = Repositorio.GetList(p => true);
            ListadoProductosConsulta = new List<ProductosConsulta>();
            ListadoProductosConsulta = CargarLista(ListadoProductos);

            if (FiltroComboBox.SelectedIndex > 0)
            {
                if (!Validar())
                    return;
            }
            else
            {
                MyErrorProvider.Clear();
            }

            CriterioTextBox.Width = 180;

            switch (FiltroComboBox.SelectedIndex)
            {

                case 1: //Filtrar por Id
                    ListadoProductosConsulta = ListadoProductosConsulta.Where(l => l.ProductoId.ToString().Contains(CriterioTextBox.Text)).ToList();
                    break;

                case 2://Filtrar por descripcion
                    ListadoProductosConsulta = ListadoProductosConsulta.Where(l => l.Descripcion.Contains(CriterioTextBox.Text.ToUpper())).ToList();
                    break;

                case 3://Filtrar por Categoría
                    ListadoProductosConsulta = ListadoProductosConsulta.Where(l => l.Categoria.Contains(CriterioTextBox.Text.ToUpper())).ToList();
                    break;

                case 4://Filtrar por Cantidad
                    ListadoProductosConsulta = ListadoProductosConsulta.Where(l => l.Cantidad >= DesdeNumericUpDown.Value && l.Cantidad <= HastaNumericUpDown.Value).ToList();
                    break;

                case 5://Filtrar por Precio
                    ListadoProductosConsulta = ListadoProductosConsulta.Where(l => l.Precio >= Convert.ToSingle(DesdeNumericUpDown.Value) && l.Precio <= Convert.ToSingle(HastaNumericUpDown.Value)).ToList();
                    break;
            }

            DatosDelProductoButton.Enabled = false;
            GenerarEntradaButton.Enabled = false;
            ProductosDataGridView.DataSource = null;
            ProductosDataGridView.DataSource = ListadoProductosConsulta;
            Formato();
            ProductosDataGridView.ClearSelection();
        }

        private void Formato()//Le da el formato a la consulta
        {
            ProductosDataGridView.Columns[0].HeaderText = "Código";
            ProductosDataGridView.Columns[0].Width = 60;
            ProductosDataGridView.Columns[1].HeaderText = "Descripcion";
            ProductosDataGridView.Columns[1].Width = 275;
            ProductosDataGridView.Columns[2].HeaderText = "Categoria";
            ProductosDataGridView.Columns[2].Width = 150;
            ProductosDataGridView.Columns[3].HeaderText = "Cantidad";
            ProductosDataGridView.Columns[3].Width = 95;
            ProductosDataGridView.Columns[4].HeaderText = "Precio";
            ProductosDataGridView.Columns[4].Width = 95;
            ProductosDataGridView.Columns[4].DefaultCellStyle.Format = "N2";
        }

        private void RealizarBusquedaButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void FiltroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            CriterioTextBox.Width = 180;
            if (FiltroComboBox.SelectedIndex == 4 || FiltroComboBox.SelectedIndex == 5)
                BuscarPorRango();
            else
                BuscarPorCriterio();
        }

        private void BuscarPorRango() // Funcion que activa los campos necesarios para la busqueda por un rango entre dos numeros
        {
            CriterioTextBox.Visible = false;
            CriterioLabel.Text = "Desde";
            HastaLabel.Visible = true;
            DesdeNumericUpDown.Visible = true;
            HastaNumericUpDown.Visible = true;
            DesdeNumericUpDown.Focus();
        }

        private void BuscarPorCriterio() // Funcion que activa los campos necesarios para la busqueda por un criterio
        {
            HastaLabel.Visible = false;
            DesdeNumericUpDown.Visible = false;
            HastaNumericUpDown.Visible = false;
            CriterioTextBox.Visible = true;
            CriterioLabel.Text = "Criterio";
            CriterioTextBox.Focus();
        }

        private void cProductos_Load(object sender, EventArgs e)
        {
            BuscarPorCriterio();
            Buscar();
        }

        private void ProductosDataGridView_Click(object sender, EventArgs e)
        {
            if (ListadoProductosConsulta.Count > 0)
            {
                if (ProductosDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDelProductoButton.Enabled = true;
                    GenerarEntradaButton.Enabled = true;
                }
                else
                {
                    DatosDelProductoButton.Enabled = false;
                    GenerarEntradaButton.Enabled = false;
                }
            }
        }

        private void ProductosDataGridView_DoubleClick(object sender, EventArgs e)
        {

            if (ListadoProductosConsulta.Count > 0)
            {
                if (ProductosDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDelProductoButton.Enabled = true;
                    GenerarEntradaButton.Enabled = true;
                    IdProductoeleccionado = Convert.ToInt32(ProductosDataGridView.CurrentRow.Cells["ProductoId"].Value);
                    rProductos rP = new rProductos(IdProductoeleccionado);
                    rP.ShowDialog();
                }
            }
            
        }

        private void DatosDelProductoButton_Click(object sender, EventArgs e)
        {
            IdProductoeleccionado = Convert.ToInt32(ProductosDataGridView.CurrentRow.Cells["ProductoId"].Value);
            rProductos rP = new rProductos(IdProductoeleccionado);
            rP.ShowDialog();
        }

        private void GenerarEntradaButton_Click(object sender, EventArgs e)
        {
            IdProductoeleccionado = Convert.ToInt32(ProductosDataGridView.CurrentRow.Cells["ProductoId"].Value);
            rEntradaProductos rE = new rEntradaProductos(0, IdProductoeleccionado);
            rE.ShowDialog();
        }

        private void cProductos_Activated(object sender, EventArgs e)
        {
            Buscar();
        }

        private void CriterioTextBox_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void DesdeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void HastaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
