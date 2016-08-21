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

    public partial class FrmPrestamo : Form
    {
        static internal FrmPrestamo frmPrestamo;

        OleDbConnection conexion;
        internal OleDbDataAdapter adaptador;
        internal DataSet datos;
        BindingManagerBase bmb;
        OleDbCommandBuilder constructor;

        string sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, fechaLimite, DATEDIFF('d', fecha, fechalimite) AS tiempo,cantidad, recargo, formaPago, total / tiempo AS cuota_diaria, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0;";
        
        public FrmPrestamo()
        {
            FrmPrestamo.frmPrestamo = this;
            InitializeComponent();
        }

        #region "carga"
        private void FrmPrestamo_Load_1(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            constructor = new OleDbCommandBuilder(adaptador);

            datos = new DataSet();

            OleDbCommand insertar = new OleDbCommand("INSERT INTO PRESTAMO (prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, cedulaCliente, cedulaCobrador, total) VALUES (@prestamo, @fecha, @cantidad, @recargo, @formaPago, @fechaLimite, @cedulaCliente, @cedulaCobrador, @total)", conexion);
            adaptador.InsertCommand = insertar;
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@prestamo", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@fecha", OleDbType.DBDate));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@cantidad", OleDbType.Double));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@recargo", OleDbType.Double));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@formaPago", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@fechaLimite", OleDbType.DBDate));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@cedulaCliente", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@cedulaCobrador", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@total", OleDbType.Double));
                       
            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            txtPrestamo.DataBindings.Add(new Binding("Text", datos.Tables[0], "prestamo"));
            dtpFecha.DataBindings.Add(new Binding("Text", datos.Tables[0], "fecha"));
            txtCantidad.DataBindings.Add(new Binding("Text", datos.Tables[0], "cantidad"));
            txtRecargo.DataBindings.Add(new Binding("Text", datos.Tables[0], "recargo"));
            cboFormaPago.DataBindings.Add(new Binding("Text", datos.Tables[0], "formaPago"));
            dtpFechaLimite.DataBindings.Add(new Binding("Text", datos.Tables[0], "fechaLimite"));
            txtTotal.DataBindings.Add(new Binding("Text", datos.Tables[0], "total"));
            txtTotalAbono.DataBindings.Add(new Binding("Text", datos.Tables[0], "totalAbono"));
            txtSaldo.DataBindings.Add(new Binding("Text", datos.Tables[0], "saldo"));
            txtCedulaCliente.DataBindings.Add(new Binding("Text", datos.Tables[0], "cedulaCliente"));
            txtCedulaCobrador.DataBindings.Add(new Binding("Text", datos.Tables[0], "cedulaCobrador"));

            //txtNombres.DataBindings.Add(new Binding("Text", datos.Tables[0], "nombres"));
            //txtApellidos.DataBindings.Add(new Binding("Text", datos.Tables[0], "apellidos"));
            //txtTelefono.DataBindings.Add(new Binding("Text", datos.Tables[0], "telefono"));
            //txtCelular.DataBindings.Add(new Binding("Text", datos.Tables[0], "celular"));
            //txtDireccion.DataBindings.Add(new Binding("Text", datos.Tables[0], "direccion"));

            dgvPrestamo.DataSource = datos.Tables[0];
                        
            bmb = BindingContext[datos.Tables[0]];
            
            this.registro();
            this.cargarBusqueda(cboBuscar);
            //this.calculoSaldo(); 
        }

        private void registro()
        {
            txtRegistro.Text = "Registro " + (bmb.Position + 1) + " de " + datos.Tables[0].Rows.Count;
        }

        internal void calculoSaldo()
        {
            foreach (DataGridViewRow row in dgvPrestamo.Rows)
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

        #region "editar"
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtPrestamo.Clear();
            dtpFecha.Text = "";
            txtCantidad.Clear();
            txtRecargo.Clear();
            cboFormaPago.Text = "";
            dtpFechaLimite.Text = "";
            txtTotal.Clear();
            txtTotalAbono.Clear();
            txtSaldo.Clear();
            txtCedulaCliente.Clear();
            txtCedulaCobrador.Clear();

            txtPrestamo.Focus();
            btnNuevo.Enabled = false;
            btnInsertar.Enabled = true;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            btnInsertar.Enabled = false;
            btnNuevo.Enabled = true;         

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

                    adaptador.InsertCommand.Parameters["@prestamo"].Value = txtPrestamo.Text;
                    adaptador.InsertCommand.Parameters["@fecha"].Value = Convert.ToDateTime(dtpFecha.Text);
                    adaptador.InsertCommand.Parameters["@cantidad"].Value = Convert.ToDouble(txtCantidad.Text);
                    adaptador.InsertCommand.Parameters["@recargo"].Value = Convert.ToDouble(txtRecargo.Text);
                    adaptador.InsertCommand.Parameters["@formaPago"].Value = cboFormaPago.Text;
                    adaptador.InsertCommand.Parameters["@fechaLimite"].Value = Convert.ToDateTime(dtpFechaLimite.Text);
                    adaptador.InsertCommand.Parameters["@total"].Value = Convert.ToDouble(txtTotal.Text);
                    adaptador.InsertCommand.Parameters["@cedulaCliente"].Value = txtCedulaCliente.Text;
                    adaptador.InsertCommand.Parameters["@cedulaCobrador"].Value = txtCedulaCobrador.Text;

                    conexion.Open();
                    int i = adaptador.InsertCommand.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(i + " Registros Insertados", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    datos.Clear();
                    adaptador.Fill(datos);
                    dgvPrestamo.Refresh();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(TabControl1.TabPages[0].Focus())
            {
                if (dgvPrestamo.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmPrestamoActualizar frmPrestamoActualizar = new FrmPrestamoActualizar();
                    frmPrestamoActualizar.Show();
                    
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCedulaCobrador.Text = dgvPrestamo.CurrentRow.Cells[0].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCedulaCliente.Text = dgvPrestamo.CurrentRow.Cells[1].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtPrestamo.Text = dgvPrestamo.CurrentRow.Cells[7].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.dtpFecha.Text = dgvPrestamo.CurrentRow.Cells[8].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.dtpFechaLimite.Text = dgvPrestamo.CurrentRow.Cells[9].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCantidad.Text = dgvPrestamo.CurrentRow.Cells[11].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtRecargo.Text = dgvPrestamo.CurrentRow.Cells[12].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.cboFormaPago.Text = dgvPrestamo.CurrentRow.Cells[13].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtTotal.Text = dgvPrestamo.CurrentRow.Cells[15].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtTotalAbono.Text = dgvPrestamo.CurrentRow.Cells[16].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtSaldo.Text = dgvPrestamo.CurrentRow.Cells[17].Value.ToString(); 
                }
            }

            else if (TabControl1.TabPages[1].Focus())
            {
                if (dgvPrestamoBuscar.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmPrestamoActualizar frmPrestamoActualizar = new FrmPrestamoActualizar();
                    frmPrestamoActualizar.Show();

                    FrmPrestamoActualizar.frmPrestamoActualizar.txtPrestamo.Text = dgvPrestamoBuscar.CurrentRow.Cells[7].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.dtpFecha.Text = dgvPrestamoBuscar.CurrentRow.Cells[8].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCantidad.Text = dgvPrestamoBuscar.CurrentRow.Cells[9].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtRecargo.Text = dgvPrestamoBuscar.CurrentRow.Cells[10].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.cboFormaPago.Text = dgvPrestamoBuscar.CurrentRow.Cells[11].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.dtpFechaLimite.Text = dgvPrestamoBuscar.CurrentRow.Cells[12].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCedulaCobrador.Text = dgvPrestamoBuscar.CurrentRow.Cells[0].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtCedulaCliente.Text = dgvPrestamoBuscar.CurrentRow.Cells[1].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtTotal.Text = dgvPrestamoBuscar.CurrentRow.Cells[13].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtTotalAbono.Text = dgvPrestamoBuscar.CurrentRow.Cells[14].Value.ToString();
                    FrmPrestamoActualizar.frmPrestamoActualizar.txtSaldo.Text = dgvPrestamoBuscar.CurrentRow.Cells[15].Value.ToString();
                }
            }
        }
        #endregion

        #region "foraneas"
        private void btnCedulaCliente_Click_1(object sender, EventArgs e)
        {
            FrmClienteBuscar frmClienteBuscar = new FrmClienteBuscar();
            frmClienteBuscar.Show();
            FrmClienteBuscar.frmClienteBuscar.btnEnviar.Visible = true;
            FrmClienteBuscar.frmClienteBuscar.btnEnviarClienteActualizar.Visible = false;
        }

        private void btnCedulaCobrador_Click_1(object sender, EventArgs e)
        {
            FrmCobradorBuscar frmCobradorBuscar = new FrmCobradorBuscar();
            frmCobradorBuscar.Show();
        }
        #endregion
             
        #region "busqueda"
        private void cargarBusqueda(ComboBox cbo)
        {
            cbo.Items.Add("CC cliente");
            cbo.Items.Add("prestamo");
            cbo.Items.Add("nombres");
            cbo.Items.Add("apellidos");
        }
        
        private void calculoSaldoBuscar()
        {
            foreach (DataGridViewRow row in dgvPrestamoBuscar.Rows)
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

                dgvPrestamoBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "prestamo")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE P.prestamo LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvPrestamoBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "nombres")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE nombres LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvPrestamoBuscar.DataSource = dt;
            }
            else if (cboBuscar.Text == "apellidos")
            {
                sql = "SELECT cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total, SUM(abono) AS totalAbono, (total-totalAbono) AS saldo FROM (CLIENTE AS C LEFT JOIN PRESTAMO AS P ON C.cedula=P.cedulaCliente) LEFT JOIN ABONO AS A ON P.prestamo=A.prestamo WHERE apellidos LIKE '%" + txtBuscar.Text + "%' GROUP BY cedulaCobrador, cedulaCliente, nombres, apellidos, telefono, celular, direccion, P.prestamo, fecha, cantidad, recargo, formaPago, fechaLimite, total HAVING total>0";
                adaptador = new OleDbDataAdapter(sql, conexion);
                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvPrestamoBuscar.DataSource = dt;
            }

            if (dgvPrestamoBuscar.Rows.Count < 1)
            {
                errorProvider3.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                errorProvider3.SetError(txtBuscar, "No hay registros");
            }
            else
            {
                errorProvider3.SetError(txtBuscar, "");
            }

            this.calculoSaldoBuscar(); 
        }
        #endregion

        private void btnAbonar_Click(object sender, EventArgs e)
        {
            if(TabControl1.TabPages[0].Focus())
            {
                if (dgvPrestamo.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmAbono1 frmAbono1 = new FrmAbono1();
                    frmAbono1.Show();

                    FrmAbono1.frmAbono1.txtPrestamo.Text = dgvPrestamo.CurrentRow.Cells[7].Value.ToString();
                    FrmAbono1.frmAbono1.txtCedulaCliente.Text = dgvPrestamo.CurrentRow.Cells[1].Value.ToString();
                }
            }
            else if(TabControl1.TabPages[1].Focus())
            {
                if (dgvPrestamoBuscar.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmAbono1 frmAbono1 = new FrmAbono1();
                    frmAbono1.Show();

                    FrmAbono1.frmAbono1.txtPrestamo.Text = dgvPrestamoBuscar.CurrentRow.Cells[7].Value.ToString();
                    FrmAbono1.frmAbono1.txtCedulaCliente.Text = dgvPrestamoBuscar.CurrentRow.Cells[1].Value.ToString();
                }
            }
        }

        private void dtpFechaLimite_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaLimite.Text == dtpFecha.Text)
            {
                MessageBox.Show("Debe haber al menos un día de diferencia", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }//end class
}//end namespace
