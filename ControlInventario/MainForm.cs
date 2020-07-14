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
    }
}
