using ControlInventario.BLL;
using ControlInventario.Entidades;
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
    public partial class cCategorias : Form
    {
        public int IdCategoriaSeleccionada { get; set; }
        private List<Categorias> ListadoCatgorias = new List<Categorias>();
        public cCategorias()
        {
            InitializeComponent();
        }

        private bool Validar()// Funcion encargada de validar la busqueda 
        {
            bool paso = true;

            MyErrorProvider.Clear();

            if (FiltroComboBox.SelectedIndex > 0) // Valida si el indice seleccionado en el combobox  es mayor a 0
            {
                if (CriterioTextBox.Text == string.Empty) // En caso de ser mayor a 0, el criterio no este vacio
                {
                    CriterioTextBox.Width = 175;
                    MyErrorProvider.SetError(CriterioTextBox, "Debe escribir algún criterio de búsqueda!");
                    CriterioTextBox.Focus();
                    paso = false;
                }
                else if (FiltroComboBox.SelectedIndex == 1 && CriterioTextBox.Text.Any(x => !char.IsNumber(x))) // Si desea filtrar por codigo, que el criterio solo tenga numeros
                {
                    CriterioTextBox.Width = 175;
                    MyErrorProvider.SetError(CriterioTextBox, "Si desea filtrar por código, solo digite números!");
                    CriterioTextBox.Focus();
                    paso = false;
                }
            }

            return paso;
        }

        private void Buscar() // Funcion que realiza las bsquedas
        {
            RepositorioBase<Categorias> Repositorio = new RepositorioBase<Categorias>();
            ListadoCatgorias = new List<Categorias>();
            ListadoCatgorias = Repositorio.GetList(p => true);

            if (FiltroComboBox.SelectedIndex > 0)
            {
                if (!Validar())
                    return;
            }
            else
            {
                MyErrorProvider.Clear();
            }
            CriterioTextBox.Width = 195;

            switch (FiltroComboBox.SelectedIndex)
            {

                case 1: //Filtrar por Id
                    ListadoCatgorias = ListadoCatgorias.Where(l => l.CategoriaId.ToString().Contains(CriterioTextBox.Text)).ToList();
                    break;

                case 2://Filtrar por Nombre
                    ListadoCatgorias = ListadoCatgorias.Where(l => l.Nombre.Contains(CriterioTextBox.Text.ToUpper())).ToList();
                    break;

            }

            DatosDeLaCategoriaButton.Enabled = false;
            CategoriasDataGridView.DataSource = null;
            CategoriasDataGridView.DataSource = ListadoCatgorias;
            Formato();
            CategoriasDataGridView.ClearSelection();
        }

        private void Formato()// Le da el formato a la consulta
        {
            CategoriasDataGridView.Columns[0].HeaderText = "Codigo";
            CategoriasDataGridView.Columns[0].Width = 50;
            CategoriasDataGridView.Columns[1].HeaderText = "Nombre";
            CategoriasDataGridView.Columns[1].Width = 300;
        }

        private void cCategorias_Load(object sender, EventArgs e)
        {
            FiltroComboBox.SelectedIndex = 0;
            Buscar();
        }

        private void DatosDeLaCategoriaButton_Click(object sender, EventArgs e)
        {
            IdCategoriaSeleccionada = Convert.ToInt32(CategoriasDataGridView.CurrentRow.Cells["CategoriaId"].Value);
            rCategorias rC = new rCategorias(IdCategoriaSeleccionada);
            rC.ShowDialog();
        }

        private void RealizarBusquedaButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void CriterioTextBox_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void CategoriasDataGridView_Click(object sender, EventArgs e)
        {
            if (ListadoCatgorias.Count > 0)
            {
                if (CategoriasDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDeLaCategoriaButton.Enabled = true;
                }
                else
                {
                    DatosDeLaCategoriaButton.Enabled = false;
                }
            }
        }

        private void CategoriasDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ListadoCatgorias.Count > 0)
            {
                if (CategoriasDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDeLaCategoriaButton.Enabled = true;
                    IdCategoriaSeleccionada = Convert.ToInt32(CategoriasDataGridView.CurrentRow.Cells["CategoriaId"].Value);
                    rCategorias rC = new rCategorias(IdCategoriaSeleccionada);
                    rC.ShowDialog();
                }
            }
        }

        private void FiltroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            CriterioTextBox.Width = 195;
            CriterioTextBox.Focus();
        }

        private void cCategorias_Activated(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
