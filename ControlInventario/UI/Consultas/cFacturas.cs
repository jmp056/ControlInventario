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
    public partial class cFacturas : Form
    {
        public int IdFacturaSeleccionada { get; set; }
        private List<Facturas> ListadoFacturas = new List<Facturas>();

        public cFacturas()
        {
            InitializeComponent();
        }

        private bool Validar() // Funcion encargada de validar la busqueda 
        {
            bool paso = true;

            MyErrorProvider.Clear();

            if (FiltroComboBox.SelectedIndex > 0 && FiltroComboBox.SelectedIndex <= 2)
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
            else if (FiltroComboBox.SelectedIndex >= 3)
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

        private void Buscar() // Funcion que realiza las bsquedas
        {
            RepositorioBase<Facturas> Repositorio = new RepositorioBase<Facturas>();
            ListadoFacturas = new List<Facturas>();
            ListadoFacturas = Repositorio.GetList(p => true);

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
                    ListadoFacturas = ListadoFacturas.Where(l => l.FacturaId.ToString().Contains(CriterioTextBox.Text)).ToList();
                    break;

                case 2://Filtrar por Vendedor
                    ListadoFacturas = ListadoFacturas.Where(l => l.Usuario.Contains(CriterioTextBox.Text.ToUpper())).ToList();
                    break;

                case 3://Filtrar por Cliente
                    ListadoFacturas = ListadoFacturas.Where(l => l.Cliente.Contains(CriterioTextBox.Text.ToUpper())).ToList();
                    break;

                case 4://Filtrar por Monto
                    ListadoFacturas = ListadoFacturas.Where(l => Convert.ToInt32(l.Total) >= DesdeNumericUpDown.Value && Convert.ToInt32(l.Total) <= HastaNumericUpDown.Value).ToList();
                    break;
            }

            if (FiltrarPorFechaCheckBox.Checked == true)
            {
                ListadoFacturas = ListadoFacturas.Where(l => l.Fecha.Date >= DesdeDateTimePicker.Value.Date && l.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }

            DatosDeLaFacturaButton.Enabled = false;
            FacturaDataGridView.DataSource = null;
            FacturaDataGridView.DataSource = ListadoFacturas;
            Formato();
            FacturaDataGridView.ClearSelection();
        }

        private void Formato() // Le da el formato a la consulta
        {
            FacturaDataGridView.Columns[0].HeaderText = "Código";
            FacturaDataGridView.Columns[0].Width = 60;
            FacturaDataGridView.Columns[1].HeaderText = "Vendedor";
            FacturaDataGridView.Columns[1].Width = 200;
            FacturaDataGridView.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy - hh:mm:ss tt";
            FacturaDataGridView.Columns[2].HeaderText = "Fecha";
            FacturaDataGridView.Columns[2].Width = 150;
            FacturaDataGridView.Columns[3].HeaderText = "Cliente";
            FacturaDataGridView.Columns[3].Width = 190;
            FacturaDataGridView.Columns[4].HeaderText = "Monto";
            FacturaDataGridView.Columns[4].Width = 80;
            FacturaDataGridView.Columns[4].DefaultCellStyle.Format = "N2";
        }

        private void cFacturas_Load(object sender, EventArgs e)
        {
            HastaDateTimePicker.Value = DateTime.Now;
            DesdeDateTimePicker.Value = DateTime.Now;

            BuscarPorCriterio();
            FiltroComboBox.SelectedIndex = 0;
        }

        private void DatosDeLaFacturaButton_Click(object sender, EventArgs e)
        {
            IdFacturaSeleccionada = Convert.ToInt32(FacturaDataGridView.CurrentRow.Cells["FacturaId"].Value);
            rFacturas rF = new rFacturas(IdFacturaSeleccionada);
            rF.ShowDialog();
        }

        private void RealizarBusquedaButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void FiltroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            CriterioTextBox.Width = 180;
            if (FiltroComboBox.SelectedIndex == 4)
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

        private void FacturaDataGridView_Click(object sender, EventArgs e)
        {
            if (ListadoFacturas.Count > 0)
            {
                if (FacturaDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDeLaFacturaButton.Enabled = true;
                }
                else
                {
                    DatosDeLaFacturaButton.Enabled = false;
                }
            }
        }

        private void FacturaDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ListadoFacturas.Count > 0)
            {
                if (FacturaDataGridView.CurrentRow.Index >= 0)
                {
                    DatosDeLaFacturaButton.Enabled = true;
                    IdFacturaSeleccionada = Convert.ToInt32(FacturaDataGridView.CurrentRow.Cells["FacturaId"].Value);
                    rFacturas rF = new rFacturas(IdFacturaSeleccionada);
                    rF.ShowDialog();
                }
            }
        }

        private void cFacturas_Activated(object sender, EventArgs e)
        {
            Buscar();
        }

        private void DesdeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void CriterioTextBox_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void HastaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void DesdeDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void HastaDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void FiltrarPorFechaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
