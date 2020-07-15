namespace ControlInventario
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MyMenuStrip = new System.Windows.Forms.MenuStrip();
            this.registrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroDeCategoriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroDeProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroDeFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entradaDeProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuadreDeCajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyMenuStrip
            // 
            this.MyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrosToolStripMenuItem,
            this.consultasToolStripMenuItem});
            this.MyMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MyMenuStrip.Name = "MyMenuStrip";
            this.MyMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MyMenuStrip.TabIndex = 0;
            this.MyMenuStrip.Text = "menuStrip1";
            // 
            // registrosToolStripMenuItem
            // 
            this.registrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registroDeCategoriasToolStripMenuItem,
            this.registroDeProductosToolStripMenuItem,
            this.registroDeFacturasToolStripMenuItem,
            this.entradaDeProductosToolStripMenuItem,
            this.cuadreDeCajaToolStripMenuItem});
            this.registrosToolStripMenuItem.Name = "registrosToolStripMenuItem";
            this.registrosToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.registrosToolStripMenuItem.Text = "Registros";
            // 
            // registroDeCategoriasToolStripMenuItem
            // 
            this.registroDeCategoriasToolStripMenuItem.Name = "registroDeCategoriasToolStripMenuItem";
            this.registroDeCategoriasToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.registroDeCategoriasToolStripMenuItem.Text = "Registro de Categorias";
            this.registroDeCategoriasToolStripMenuItem.Click += new System.EventHandler(this.registroDeCategoriasToolStripMenuItem_Click);
            // 
            // registroDeProductosToolStripMenuItem
            // 
            this.registroDeProductosToolStripMenuItem.Name = "registroDeProductosToolStripMenuItem";
            this.registroDeProductosToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.registroDeProductosToolStripMenuItem.Text = "Registro de Productos";
            this.registroDeProductosToolStripMenuItem.Click += new System.EventHandler(this.registroDeProductosToolStripMenuItem_Click);
            // 
            // consultasToolStripMenuItem
            // 
            this.consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            this.consultasToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.consultasToolStripMenuItem.Text = "Consultas";
            // 
            // registroDeFacturasToolStripMenuItem
            // 
            this.registroDeFacturasToolStripMenuItem.Name = "registroDeFacturasToolStripMenuItem";
            this.registroDeFacturasToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.registroDeFacturasToolStripMenuItem.Text = "Registro de Facturas";
            this.registroDeFacturasToolStripMenuItem.Click += new System.EventHandler(this.registroDeFacturasToolStripMenuItem_Click);
            // 
            // entradaDeProductosToolStripMenuItem
            // 
            this.entradaDeProductosToolStripMenuItem.Name = "entradaDeProductosToolStripMenuItem";
            this.entradaDeProductosToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.entradaDeProductosToolStripMenuItem.Text = "Entrada de productos";
            this.entradaDeProductosToolStripMenuItem.Click += new System.EventHandler(this.entradaDeProductosToolStripMenuItem_Click);
            // 
            // cuadreDeCajaToolStripMenuItem
            // 
            this.cuadreDeCajaToolStripMenuItem.Name = "cuadreDeCajaToolStripMenuItem";
            this.cuadreDeCajaToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.cuadreDeCajaToolStripMenuItem.Text = "Cuadre de Caja";
            this.cuadreDeCajaToolStripMenuItem.Click += new System.EventHandler(this.cuadreDeCajaToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MyMenuStrip);
            this.MainMenuStrip = this.MyMenuStrip;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.MyMenuStrip.ResumeLayout(false);
            this.MyMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem registrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroDeCategoriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroDeProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroDeFacturasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entradaDeProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuadreDeCajaToolStripMenuItem;
    }
}

