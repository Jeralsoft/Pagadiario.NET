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
    public partial class FrmPrincipal : Form
    {
        
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        DataSet datos;
        BindingManagerBase bmb;

        string sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        #region "carga"
        private void FrmCobroDiario_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            datos = new DataSet();

            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            dgvClientes.DataSource = datos.Tables[0];
            
            bmb = BindingContext[datos.Tables[0]];

            this.registro();
            this.cargarBusqueda(cboBuscar);
            this.calculoSaldo(); 
        }

        private void registro()
        {
            txtRegistro.Text = "Registro " + (bmb.Position + 1) + " de " + datos.Tables[0].Rows.Count;
        }

        internal void calculoSaldo()
        {
            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                if (row.Cells[14].Value.ToString() == "")
                {
                    row.Cells[14].Value = 0;
                }
                row.Cells[15].Value = Convert.ToDouble(row.Cells[13].Value) - Convert.ToDouble(row.Cells[14].Value);
            }
        }        
        #endregion      

        #region "navegar"
        private void btnPri_Click(object sender, EventArgs e)
        {
            bmb.Position = 0;
            this.registro();
        }

        private void btnAnt_Click(object sender, EventArgs e)
        {
            bmb.Position--;
            this.registro();
        }

        private void btnSig_Click(object sender, EventArgs e)
        {
            bmb.Position++;
            this.registro();
        }

        private void btnUlt_Click(object sender, EventArgs e)
        {
            bmb.Position = datos.Tables[0].Rows.Count;
            this.registro();
        }
        #endregion

        #region "busqueda"
        private void cargarBusqueda(ComboBox cbo)
        {
            cbo.Items.Add("CC cliente");
            cbo.Items.Add("nombres");
            cbo.Items.Add("apellidos");
            cbo.Items.Add("prestamo");
        }
        
        internal void calculoSaldoBusqueda()
        {
            foreach (DataGridViewRow row in dgvClientesBuscar.Rows)
            {
                if (row.Cells[14].Value.ToString() == "")
                {
                    row.Cells[14].Value = 0;
                }
                row.Cells[15].Value = Convert.ToDouble(row.Cells[13].Value) - Convert.ToDouble(row.Cells[14].Value);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string sql;
            DataTable dt;

            if (cboBuscar.Text == "CC cliente")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE cedulaCliente LIKE '%"+txtBuscar.Text+"%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "nombres")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE nombres LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "apellidos")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE apellidos LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "prestamo")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE P.prestamo LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            if (dgvClientesBuscar.Rows.Count < 1)
            {
                errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                errorProvider1.SetError(txtBuscar, "No hay registros");
            }
            else
            {
                errorProvider1.SetError(txtBuscar, "");
            }

            this.calculoSaldoBusqueda(); 
        }
        #endregion

    }//end class
}//end namespace
