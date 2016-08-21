namespace pagadiario.NET
{
    partial class FrmClientesAlDia
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
            this.components = new System.ComponentModel.Container();
            this.btnExportar = new System.Windows.Forms.Button();
            this.txtRegistro = new System.Windows.Forms.TextBox();
            this.btnUlt = new System.Windows.Forms.Button();
            this.btnSig = new System.Windows.Forms.Button();
            this.btnAnt = new System.Windows.Forms.Button();
            this.btnPri = new System.Windows.Forms.Button();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.empleado = new System.Windows.Forms.TabPage();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.buscar = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cboBuscar = new System.Windows.Forms.ComboBox();
            this.dgvClientesBuscar = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.TabControl1.SuspendLayout();
            this.empleado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(1066, 534);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 91;
            this.btnExportar.Text = "&Excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            // 
            // txtRegistro
            // 
            this.txtRegistro.Location = new System.Drawing.Point(92, 537);
            this.txtRegistro.Name = "txtRegistro";
            this.txtRegistro.Size = new System.Drawing.Size(164, 20);
            this.txtRegistro.TabIndex = 90;
            // 
            // btnUlt
            // 
            this.btnUlt.Location = new System.Drawing.Point(300, 535);
            this.btnUlt.Name = "btnUlt";
            this.btnUlt.Size = new System.Drawing.Size(32, 23);
            this.btnUlt.TabIndex = 89;
            this.btnUlt.Text = ">|";
            this.btnUlt.UseVisualStyleBackColor = true;
            this.btnUlt.Click += new System.EventHandler(this.btnUlt_Click);
            // 
            // btnSig
            // 
            this.btnSig.Location = new System.Drawing.Point(262, 535);
            this.btnSig.Name = "btnSig";
            this.btnSig.Size = new System.Drawing.Size(32, 23);
            this.btnSig.TabIndex = 88;
            this.btnSig.Text = ">>";
            this.btnSig.UseVisualStyleBackColor = true;
            this.btnSig.Click += new System.EventHandler(this.btnSig_Click);
            // 
            // btnAnt
            // 
            this.btnAnt.Location = new System.Drawing.Point(54, 535);
            this.btnAnt.Name = "btnAnt";
            this.btnAnt.Size = new System.Drawing.Size(32, 23);
            this.btnAnt.TabIndex = 87;
            this.btnAnt.Text = "<<";
            this.btnAnt.UseVisualStyleBackColor = true;
            this.btnAnt.Click += new System.EventHandler(this.btnAnt_Click);
            // 
            // btnPri
            // 
            this.btnPri.Location = new System.Drawing.Point(16, 535);
            this.btnPri.Name = "btnPri";
            this.btnPri.Size = new System.Drawing.Size(32, 23);
            this.btnPri.TabIndex = 86;
            this.btnPri.Text = "|<";
            this.btnPri.UseVisualStyleBackColor = true;
            this.btnPri.Click += new System.EventHandler(this.btnPri_Click);
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.empleado);
            this.TabControl1.Controls.Add(this.buscar);
            this.TabControl1.Location = new System.Drawing.Point(12, 11);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1129, 517);
            this.TabControl1.TabIndex = 85;
            // 
            // empleado
            // 
            this.empleado.Controls.Add(this.dgvClientes);
            this.empleado.Location = new System.Drawing.Point(4, 22);
            this.empleado.Name = "empleado";
            this.empleado.Padding = new System.Windows.Forms.Padding(3);
            this.empleado.Size = new System.Drawing.Size(1121, 491);
            this.empleado.TabIndex = 0;
            this.empleado.Text = "Cliente";
            this.empleado.UseVisualStyleBackColor = true;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(6, 6);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(1109, 479);
            this.dgvClientes.TabIndex = 0;
            // 
            // buscar
            // 
            this.buscar.Controls.Add(this.label11);
            this.buscar.Controls.Add(this.label12);
            this.buscar.Controls.Add(this.txtBuscar);
            this.buscar.Controls.Add(this.cboBuscar);
            this.buscar.Controls.Add(this.dgvClientesBuscar);
            this.buscar.Location = new System.Drawing.Point(4, 22);
            this.buscar.Name = "buscar";
            this.buscar.Padding = new System.Windows.Forms.Padding(3);
            this.buscar.Size = new System.Drawing.Size(1121, 638);
            this.buscar.TabIndex = 1;
            this.buscar.Text = "Buscar";
            this.buscar.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(130, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Texto";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Criterio";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(133, 27);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(235, 20);
            this.txtBuscar.TabIndex = 10;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // cboBuscar
            // 
            this.cboBuscar.FormattingEnabled = true;
            this.cboBuscar.Location = new System.Drawing.Point(6, 26);
            this.cboBuscar.Name = "cboBuscar";
            this.cboBuscar.Size = new System.Drawing.Size(121, 21);
            this.cboBuscar.TabIndex = 9;
            // 
            // dgvClientesBuscar
            // 
            this.dgvClientesBuscar.AllowUserToAddRows = false;
            this.dgvClientesBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientesBuscar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientesBuscar.Location = new System.Drawing.Point(6, 53);
            this.dgvClientesBuscar.Name = "dgvClientesBuscar";
            this.dgvClientesBuscar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientesBuscar.Size = new System.Drawing.Size(1109, 579);
            this.dgvClientesBuscar.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmClientesAlDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 570);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.txtRegistro);
            this.Controls.Add(this.btnUlt);
            this.Controls.Add(this.btnSig);
            this.Controls.Add(this.btnAnt);
            this.Controls.Add(this.btnPri);
            this.Controls.Add(this.TabControl1);
            this.Name = "FrmClientesAlDia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENTES AL DIA";
            this.Load += new System.EventHandler(this.FrmClientesAlDia_Load);
            this.TabControl1.ResumeLayout(false);
            this.empleado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TextBox txtRegistro;
        private System.Windows.Forms.Button btnUlt;
        private System.Windows.Forms.Button btnSig;
        private System.Windows.Forms.Button btnAnt;
        private System.Windows.Forms.Button btnPri;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage empleado;
        internal System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.TabPage buscar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cboBuscar;
        internal System.Windows.Forms.DataGridView dgvClientesBuscar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}