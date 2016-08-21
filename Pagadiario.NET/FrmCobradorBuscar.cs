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
    public partial class FrmCobradorBuscar : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        DataSet datos;
        static internal FrmCobradorBuscar frmCobradorBuscar;
        string sql = "SELECT * FROM COBRADOR"; 

        public FrmCobradorBuscar()
        {
            FrmCobradorBuscar.frmCobradorBuscar = this; 
            InitializeComponent();
        }

        private void FrmCobradorBuscar_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);

            datos = new DataSet();

            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            dgvBuscar.DataSource = datos.Tables[0];

            this.buscar(cboBuscar); 
        }

        private void buscar(ComboBox cbo)
        {
            cbo.Items.Add("cedula");
            cbo.Items.Add("nombres");
            cbo.Items.Add("apellidos");
            cbo.Items.Add("direccion");
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (dgvBuscar.Rows.Count < 1)
            {
                MessageBox.Show("No ha elegido ningun registro");
            }
            else
            {
                FrmPrestamo.frmPrestamo.txtCedulaCobrador.Text = this.dgvBuscar.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string sql;
            DataTable dt;

            if (cboBuscar.Text == "cedula")
            {
                sql = "SELECT * FROM COBRADOR WHERE cedula LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "nombres")
            {
                sql = "SELECT * FROM COBRADOR WHERE nombres LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "apellidos")
            {
                sql = "SELECT * FROM COBRADOR WHERE apellidos LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "direccion")
            {
                sql = "SELECT * FROM COBRADOR WHERE direccion LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvBuscar.DataSource = dt;
            }

            if (dgvBuscar.Rows.Count < 1)
            {
                errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                errorProvider1.SetError(txtBuscar, "No hay registros");
            }
            else
            {
                errorProvider1.SetError(txtBuscar, "");
            }
        }

        private void btnEnviar2_Click(object sender, EventArgs e)
        {
            if (dgvBuscar.Rows.Count < 1)
            {
                MessageBox.Show("No ha elegido ningun registro");
            }
            else
            {
                FrmPrestamoActualizar.frmPrestamoActualizar.txtCedulaCobrador.Text = this.dgvBuscar.CurrentRow.Cells[0].Value.ToString();
            }
        }

    }//end class
}//end namespace
