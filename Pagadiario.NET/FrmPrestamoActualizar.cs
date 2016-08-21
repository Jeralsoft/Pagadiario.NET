using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pagadiario.NET
{
    public partial class FrmPrestamoActualizar : Form
    {
        OleDbConnection conexion; 
        OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor;

        static internal FrmPrestamoActualizar frmPrestamoActualizar;

        public FrmPrestamoActualizar()
        {
            FrmPrestamoActualizar.frmPrestamoActualizar = this; 
            InitializeComponent();
        }

        private void FrmPrestamoActualizar_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter();
            constructor = new OleDbCommandBuilder(adaptador);

            OleDbCommand actualizar = new OleDbCommand("UPDATE PRESTAMO SET prestamo=@prestamo, fecha=@fecha, cantidad=@cantidad, recargo=@recargo, formaPago=@formaPago, fechaLimite=@fechaLimite, cedulaCliente=@cedulaCliente, cedulaCobrador=@cedulaCobrador, total=@total WHERE prestamo=@prestamo", conexion);
            adaptador.UpdateCommand = actualizar;
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@prestamo", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@fecha", OleDbType.DBDate));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@cantidad", OleDbType.Double));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@recargo", OleDbType.Double));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@formaPago", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@fechaLimite", OleDbType.DBDate));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@cedulaCliente", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@cedulaCobrador", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@total", OleDbType.Double));
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtPrestamo.Text == "" || txtCantidad.Text == "" || txtRecargo.Text == "" || cboFormaPago.Text == "")
            {
                MessageBox.Show("No pueden quedar campos vacios", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    double cantidad = Convert.ToDouble(txtCantidad.Text);
                    double recargo = Convert.ToDouble(txtRecargo.Text);
                    double total;

                    total = cantidad + (cantidad * (recargo / 100));
                    txtTotal.Text = total.ToString();

                    adaptador.UpdateCommand.Parameters["@prestamo"].Value = txtPrestamo.Text;
                    adaptador.UpdateCommand.Parameters["@fecha"].Value = Convert.ToDateTime(dtpFecha.Text);
                    adaptador.UpdateCommand.Parameters["@cantidad"].Value = Convert.ToDouble(txtCantidad.Text);
                    adaptador.UpdateCommand.Parameters["@recargo"].Value = Convert.ToDouble(txtRecargo.Text);
                    adaptador.UpdateCommand.Parameters["@formaPago"].Value = cboFormaPago.Text;
                    adaptador.UpdateCommand.Parameters["@fechaLimite"].Value = Convert.ToDateTime(dtpFechaLimite.Text);
                    adaptador.UpdateCommand.Parameters["@total"].Value = Convert.ToDouble(txtTotal.Text);
                    adaptador.UpdateCommand.Parameters["@cedulaCliente"].Value = txtCedulaCliente.Text;
                    adaptador.UpdateCommand.Parameters["@cedulaCobrador"].Value = txtCedulaCobrador.Text;

                    conexion.Open();
                    int i = adaptador.UpdateCommand.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(i + " Registros Actualizados", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FrmPrestamo.frmPrestamo.datos.Clear();
                    FrmPrestamo.frmPrestamo.adaptador.Fill(FrmPrestamo.frmPrestamo.datos);
                    FrmPrestamo.frmPrestamo.dgvPrestamo.Refresh();
                }
                catch (FormatException fe)
                {
                    MessageBox.Show(fe.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (OleDbException oe)
                {
                    MessageBox.Show(oe.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                    
    }//end class
}//end namespace
