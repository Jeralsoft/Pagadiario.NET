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
    public partial class FrmAbono1 : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor; 
        //DataSet datos;
            
        static internal FrmAbono1 frmAbono1;
        public FrmAbono1()
        {
            FrmAbono1.frmAbono1 = this; 
            InitializeComponent();
        }
        
        private void FrmAbono1_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter();
            constructor = new OleDbCommandBuilder(adaptador);

            OleDbCommand insertar = new OleDbCommand("INSERT INTO ABONO (prestamo, abono, fechaAbono, proximoPago) VALUES (@prestamo, @abono, @fechaAbono, @proximoPago)", conexion);
            adaptador.InsertCommand = insertar;
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@prestamo", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@abono", OleDbType.Double));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@fechaAbono", OleDbType.DBDate));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@proximoPago", OleDbType.DBDate));
        }
        
        private void btnAbonar_Click(object sender, EventArgs e)
        {
            if(txtPrestamo.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Llene todos los campos ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    adaptador.InsertCommand.Parameters["@prestamo"].Value = txtPrestamo.Text;
                    adaptador.InsertCommand.Parameters["@abono"].Value = Convert.ToDouble(txtCantidad.Text);
                    adaptador.InsertCommand.Parameters["@fechaAbono"].Value = Convert.ToDateTime(dtpFechaAbono.Text);
                    adaptador.InsertCommand.Parameters["@proximoPago"].Value = Convert.ToDateTime(dtpProximoPago.Text);

                    conexion.Open();
                    int i = adaptador.InsertCommand.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(i + " Registros añadidos", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch(FormatException fe)
                {
                    MessageBox.Show(fe.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }

            FrmPrestamo.frmPrestamo.datos.Clear();
            FrmPrestamo.frmPrestamo.adaptador.Fill(FrmPrestamo.frmPrestamo.datos);
            FrmPrestamo.frmPrestamo.dgvPrestamo.Refresh();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }//end class
}//end namespace

