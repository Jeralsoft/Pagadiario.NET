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
    public partial class FrmClienteActualizar : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor;

        static internal FrmClienteActualizar frmClienteActualizar;

        public FrmClienteActualizar()
        {
            FrmClienteActualizar.frmClienteActualizar = this; 
            InitializeComponent();
        }

        private void FrmClienteActualizar_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter();
            constructor = new OleDbCommandBuilder(adaptador);

            OleDbCommand actualizar = new OleDbCommand("UPDATE CLIENTE set cedula=@cedula, nombres=@nombres, apellidos=@apellidos, telefono=@telefono, celular=@celular, direccion=@direccion WHERE cedula=@cedula", conexion);
            adaptador.UpdateCommand = actualizar;
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@cedula", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@nombres", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@apellidos", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@telefono", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@celular", OleDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new OleDbParameter("@direccion", OleDbType.VarChar));
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtCedula.Text == "" || txtNombres.Text == "" || txtApellidos.Text == "" || txtTelefono.Text == "" || txtCelular.Text == "" || txtDireccion.Text == "")
            {
                MessageBox.Show("No pueden quedar campos vacios", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    adaptador.UpdateCommand.Parameters["@cedula"].Value = txtCedula.Text;
                    adaptador.UpdateCommand.Parameters["@nombres"].Value = txtNombres.Text;
                    adaptador.UpdateCommand.Parameters["@apellidos"].Value = txtApellidos.Text;
                    adaptador.UpdateCommand.Parameters["@telefono"].Value = txtTelefono.Text;
                    adaptador.UpdateCommand.Parameters["@celular"].Value = txtCelular.Text;
                    adaptador.UpdateCommand.Parameters["@direccion"].Value = txtDireccion.Text;

                    conexion.Open();
                    int i = adaptador.UpdateCommand.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("Datos guardados", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (OleDbException oE)
                {
                    MessageBox.Show(oE.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            FrmCliente.frmCliente.datos.Clear();
            FrmCliente.frmCliente.adaptador.Fill(FrmCliente.frmCliente.datos);
            FrmCliente.frmCliente.dgvCliente.ResetBindings();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

    }//end class
}//end namespace
