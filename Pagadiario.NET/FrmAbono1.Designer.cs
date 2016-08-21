namespace pagadiario.NET
{
    partial class FrmAbono1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dtpProximoPago = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaAbono = new System.Windows.Forms.DateTimePicker();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnAbonar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtCedulaCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrestamo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Proximo Pago";
            // 
            // dtpProximoPago
            // 
            this.dtpProximoPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProximoPago.Location = new System.Drawing.Point(235, 104);
            this.dtpProximoPago.Name = "dtpProximoPago";
            this.dtpProximoPago.Size = new System.Drawing.Size(100, 20);
            this.dtpProximoPago.TabIndex = 96;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 95;
            this.label6.Text = "Fecha ";
            // 
            // dtpFechaAbono
            // 
            this.dtpFechaAbono.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAbono.Location = new System.Drawing.Point(235, 47);
            this.dtpFechaAbono.Name = "dtpFechaAbono";
            this.dtpFechaAbono.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaAbono.TabIndex = 94;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(194, 162);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 93;
            this.btnCerrar.Text = "&Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnAbonar
            // 
            this.btnAbonar.Location = new System.Drawing.Point(99, 162);
            this.btnAbonar.Name = "btnAbonar";
            this.btnAbonar.Size = new System.Drawing.Size(75, 23);
            this.btnAbonar.TabIndex = 92;
            this.btnAbonar.Text = "&Abonar";
            this.btnAbonar.UseVisualStyleBackColor = true;
            this.btnAbonar.Click += new System.EventHandler(this.btnAbonar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "CC Cliente";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 90;
            this.label10.Text = "Prestamo";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(88, 105);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(100, 20);
            this.txtCantidad.TabIndex = 91;
            // 
            // txtCedulaCliente
            // 
            this.txtCedulaCliente.Enabled = false;
            this.txtCedulaCliente.Location = new System.Drawing.Point(88, 70);
            this.txtCedulaCliente.Name = "txtCedulaCliente";
            this.txtCedulaCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCedulaCliente.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Cantidad $";
            // 
            // txtPrestamo
            // 
            this.txtPrestamo.Enabled = false;
            this.txtPrestamo.Location = new System.Drawing.Point(88, 35);
            this.txtPrestamo.Name = "txtPrestamo";
            this.txtPrestamo.Size = new System.Drawing.Size(100, 20);
            this.txtPrestamo.TabIndex = 86;
            // 
            // FrmAbono1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 210);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpProximoPago);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpFechaAbono);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnAbonar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtCedulaCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrestamo);
            this.Name = "FrmAbono1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABONO";
            this.Load += new System.EventHandler(this.FrmAbono1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnAbonar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtCantidad;
        internal System.Windows.Forms.TextBox txtCedulaCliente;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtPrestamo;
        internal System.Windows.Forms.DateTimePicker dtpProximoPago;
        internal System.Windows.Forms.DateTimePicker dtpFechaAbono;
    }
}