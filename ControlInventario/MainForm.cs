using ControlInventario.UI.Consultas;
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

namespace ControlInventario
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void registroDeCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCategorias rC = new rCategorias(0);
            rC.ShowDialog();
        }

        private void registroDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rProductos rP = new rProductos(0);
            rP.ShowDialog();
        }

        private void cuadreDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCuadreDeCaja rCC = new rCuadreDeCaja(0);
            rCC.ShowDialog();
        }

        private void entradaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rEntradaProductos rE = new rEntradaProductos(0, 0);
            rE.ShowDialog();
        }

        private void registroDeFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rFacturas rF = new rFacturas(0);
            rF.ShowDialog();
        }

        private void consultarCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCategorias cC = new cCategorias();
            cC.ShowDialog();
        }

        private void consultaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cProductos cP = new cProductos();
            cP.ShowDialog();
        }

        private void consultaDeEntradaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cEntradaProductos cEP = new cEntradaProductos();
            cEP.ShowDialog();
        }
    }
}
