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
    public partial class FrmBalanceDiario : Form
    {
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;
        DataSet datos;
        BindingManagerBase bmb;
               
        public FrmBalanceDiario()
        {
            InitializeComponent();
        }

        private void FrmBalanceDiario_Load(object sender, EventArgs e)
        {
            DateTime x = DateTime.Now;
            cboAño.Text = x.Year.ToString();
            cboMes.Text = x.Month.ToString();

            string sql = "TRANSFORM SUM(abono) SELECT cedula, nombres, apellidos FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND YEAR(fechaAbono) = '" + x.Year + "' AND MONTH(fechaAbono) = '" + x.Month + "' GROUP BY cedula, nombres, apellidos PIVOT DAY(fechaAbono)";

            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            datos = new DataSet();

            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            dgvClientes.DataSource = datos.Tables[0];

            bmb = BindingContext[datos.Tables[0]];

            this.registro();
            this.año(cboAño, cboMes); 
        }

        private void registro()
        {
            txtRegistro.Text = "Registro " + (bmb.Position + 1) + " de " + datos.Tables[0].Rows.Count;
        }

        private void año(ComboBox cboAño, ComboBox cboMes)
        {           
            for (int i = 2000; i <= 2015; i++ )
            {
                cboAño.Items.Add("" + i);
            }

            for (int i = 1; i <= 12; i++ )
            {
                cboMes.Items.Add("" + i);
            }
        }

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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //DateTime x = DateTime.Now;
            //cboAño.Text = x.Year.ToString();
            //cboMes.Text = x.Month.ToString();

            string sql = "TRANSFORM SUM(abono) SELECT cedula, nombres, apellidos FROM CLIENTE AS C, PRESTAMO AS P, ABONO AS A WHERE C.cedula=P.cedulaCliente And P.prestamo=A.prestamo AND YEAR(fechaAbono) = '" + cboAño.Text.ToString() + "' AND MONTH(fechaAbono) = '" + cboMes.Text.ToString() + "' GROUP BY cedula, nombres, apellidos PIVOT DAY(fechaAbono)";

            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            datos = new DataSet();

            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            dgvClientes.DataSource = datos.Tables[0];

            bmb = BindingContext[datos.Tables[0]];
        }       

    }//end class
}//end namespace 
