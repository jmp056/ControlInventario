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
    public partial class Calculadora : Form
    {
        decimal MontoFactura;

        public Calculadora(decimal montoFactura)
        {
            this.MontoFactura = montoFactura;
            InitializeComponent();
        }

        private void LimpiarButton_Click(object sender, EventArgs e)
        {
            MontoFacturaNumericUpDown.Value = 0;
            MontoRecibidoNumericUpDown.Value = 0;
            RetornoTextBox.Text = "0.00";
        }

        private void Calculadora_Load(object sender, EventArgs e)
        {
            if (MontoFactura > 0)
            {
                MontoFacturaNumericUpDown.Value = MontoFactura;
                MontoRecibidoNumericUpDown.Focus();
            }
        }

        private void CalcularButton_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Calcular() // Funcion encargada de realizar el calculo
        {
            RetornoTextBox.Text = Convert.ToString(MontoRecibidoNumericUpDown.Value - MontoFacturaNumericUpDown.Value);
        }

        private void MontoFacturaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void MontoRecibidoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void MontoRecibidoNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            Calcular();
            if (e.KeyCode == Keys.Enter)
                MontoFacturaNumericUpDown.Focus();
        }

        private void MontoFacturaNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            Calcular();
            if (e.KeyCode == Keys.Enter)
                MontoRecibidoNumericUpDown.Focus();
        }

        private void MontoFacturaNumericUpDown_Enter(object sender, EventArgs e)
        {
            if (MontoFacturaNumericUpDown.Value == 0)
                MontoFacturaNumericUpDown.Text = "";
        }

        private void MontoRecibidoNumericUpDown_Enter(object sender, EventArgs e)
        {
            if (MontoRecibidoNumericUpDown.Value == 0)
                MontoRecibidoNumericUpDown.Text = "";
        }
    }
}
