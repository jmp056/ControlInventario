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
    public partial class cCuadreDeCaja : Form
    {
        public int IdCuadreSeleccionado { get; set; }
        private List<CuadresDeCaja> ListadoCuadres = new List<CuadresDeCaja>();

        public cCuadreDeCaja()
        {
            InitializeComponent();
        }

        private bool Validar()// Funcion encargada de validar la busqueda 
        {
            bool paso = true;

            MyErrorProvider.Clear();

            if (FiltroComboBox.SelectedIndex >= 1)
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
            RepositorioBase<CuadresDeCaja> Repositorio = new RepositorioBase<CuadresDeCaja>();
            ListadoCuadres = new List<CuadresDeCaja>();
            ListadoCuadres = Repositorio.GetList(p => true);

            if (FiltroComboBox.SelectedIndex > 0)
            {
                if (!Validar())
                    return;
            }
            else
            {
                MyErrorProvider.Clear();
            }

            switch (FiltroComboBox.SelectedIndex)
            {

                case 1://Filtrar por Total vendido
                    ListadoCuadres = ListadoCuadres.Where(l => Convert.ToInt32(l.TotalVendido) >= DesdeNumericUpDown.Value && Convert.ToInt32(l.TotalVendido) <= HastaNumericUpDown.Value).ToList();
                    break;

                case 2://Filtrar por Diferencia
                    ListadoCuadres = ListadoCuadres.Where(l => Convert.ToInt32(l.Diferencia) >= DesdeNumericUpDown.Value && Convert.ToInt32(l.Diferencia) <= HastaNumericUpDown.Value).ToList();
                    break;

                case 3://Filtrar por Total en caja
                    ListadoCuadres = ListadoCuadres.Where(l => Convert.ToInt32(l.TotalEnCaja) >= DesdeNumericUpDown.Value && Convert.ToInt32(l.TotalEnCaja) <= HastaNumericUpDown.Value).ToList();
                    break;
            }

            if (FiltrarPorFechaCheckBox.Checked == true)
            {
                ListadoCuadres = ListadoCuadres.Where(l => l.Fecha.Date >= DesdeDateTimePicker.Value.Date && l.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }

            DatosDelCuadreButton.Enabled = false;
            CuadresDataGridView.DataSource = null;
            CuadresDataGridView.DataSource = ListadoCuadres;
            Formato();
            CuadresDataGridView.ClearSelection();
        }

        private void Formato()// Le da el formato a la consulta
        {
            CuadresDataGridView.Columns[0].Visible = false;
            CuadresDataGridView.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            CuadresDataGridView.Columns[1].Width = 70;
            CuadresDataGridView.Columns[2].Visible = false;
            CuadresDataGridView.Columns[3].Visible = false;
            CuadresDataGridView.Columns[4].Visible = false;
            CuadresDataGridView.Columns[5].Visible = false;
            CuadresDataGridView.Columns[6].Visible = false;
            CuadresDataGridView.Columns[7].Visible = false;
            CuadresDataGridView.Columns[8].Visible = false;
            CuadresDataGridView.Columns[9].Visible = false;
            CuadresDataGridView.Columns[10].Visible = false;
            CuadresDataGridView.Columns[11].Visible = false;
            CuadresDataGridView.Columns[12].Visible = false;
            CuadresDataGridView.Columns[13].HeaderText = "Total Vendido";
            CuadresDataGridView.Columns[13].Width = 100;
            CuadresDataGridView.Columns[13].DefaultCellStyle.Format = "N2";
            CuadresDataGridView.Columns[14].HeaderText = "Diferencia";
            CuadresDataGridView.Columns[14].Width = 70;
            CuadresDataGridView.Columns[14].DefaultCellStyle.Format = "N2";
            CuadresDataGridView.Columns[15].HeaderText = "Total en Caja";
            CuadresDataGridView.Columns[15].Width = 100;
            CuadresDataGridView.Columns[15].DefaultCellStyle.Format = "N2";
        }

        private void cCuadreDeCaja_Load(object sender, EventArgs e)
        {
            DesdeDateTimePicker.Value = DateTime.Now;
            HastaDateTimePicker.Value = DateTime.Now;

            FiltroComboBox.SelectedIndex = 0;
        }

        private void RealizarBusquedaButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void CuadresDataGridView_Click(object sender, EventArgs e)
        {
            if (CuadresDataGridView.CurrentRow != null)
            {
                if (ListadoCuadres.Count > 0)
                {

                    if (CuadresDataGridView.CurrentRow.Index >= 0)
                    {
                        DatosDelCuadreButton.Enabled = true;
                    }
                    else
                    {
                        DatosDelCuadreButton.Enabled = false;
                    }
                }

            }
        }

        private void CuadresDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (CuadresDataGridView.CurrentRow != null)
            {
                if (ListadoCuadres.Count > 0)
                {
                    if (CuadresDataGridView.CurrentRow.Index >= 0)
                    {
                        DatosDelCuadreButton.Enabled = true;
                        IdCuadreSeleccionado = Convert.ToInt32(CuadresDataGridView.CurrentRow.Cells["CuadreDeCajaId"].Value);
                        rCuadreDeCaja rCC = new rCuadreDeCaja(IdCuadreSeleccionado);
                        rCC.ShowDialog();
                    }
                }
            }
        }

        private void DatosDelCuadreButton_Click(object sender, EventArgs e)
        {
            IdCuadreSeleccionado = Convert.ToInt32(CuadresDataGridView.CurrentRow.Cells["CuadreDeCajaId"].Value);
            rCuadreDeCaja rCC = new rCuadreDeCaja(IdCuadreSeleccionado);
            rCC.ShowDialog();
        }

        private void cCuadreDeCaja_Activated(object sender, EventArgs e)
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

        private void FiltrarPorFechaCheckBox_CheckedChanged(object sender, EventArgs e)
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
    }
}
