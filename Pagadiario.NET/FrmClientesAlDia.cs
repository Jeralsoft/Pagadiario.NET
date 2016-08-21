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
    public partial class FrmClientesAlDia : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        DataSet datos;
        BindingManagerBase bmb;

        string sql = "SELECT cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo, (P.cantidad+(P.cantidad*(recargo/100))) AS total, SUM(A.abono) AS totalAbono, ((P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)) AS saldo FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo GROUP BY cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo HAVING (P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)=0";

        public FrmClientesAlDia()
        {
            InitializeComponent();
        }

        #region "carga"
        private void FrmClientesAlDia_Load(object sender, EventArgs e)
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
        }

        private void registro()
        {
            txtRegistro.Text = "Registro " + (bmb.Position + 1) + " de " + datos.Tables[0].Rows.Count;
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string sql;
            DataTable dt;

            if (cboBuscar.Text == "CC cliente")
            {
                sql = "SELECT cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo, (P.cantidad+(P.cantidad*(recargo/100))) AS total, SUM(A.abono) AS totalAbono, ((P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)) AS saldo FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND cedula LIKE '%"+txtBuscar.Text+"%' GROUP BY cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo HAVING (P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)=0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "nombres")
            {
                sql = "SELECT cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo, (P.cantidad+(P.cantidad*(recargo/100))) AS total, SUM(A.abono) AS totalAbono, ((P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)) AS saldo FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND nombres LIKE '%"+txtBuscar.Text+"%' GROUP BY cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo HAVING (P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)=0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "apellidos")
            {
                sql = "SELECT cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo, (P.cantidad+(P.cantidad*(recargo/100))) AS total, SUM(A.abono) AS totalAbono, ((P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)) AS saldo FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND apellidos LIKE '%"+txtBuscar.Text+"%' GROUP BY cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo HAVING (P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)=0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvClientesBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "prestamo")
            {
                sql = "SELECT cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo, (P.cantidad+(P.cantidad*(recargo/100))) AS total, SUM(A.abono) AS totalAbono, ((P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)) AS saldo FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND P.prestamo LIKE '%"+txtBuscar.Text+"%' GROUP BY cedula, nombres, apellidos, telefono, celular, direccion, P.prestamo, P.cantidad, P.recargo HAVING (P.cantidad+(P.cantidad*(recargo/100)))-SUM(A.abono)=0";
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
        }
        #endregion
                
    }//end class
}//end namespace
