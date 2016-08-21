using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pagadiario.NET {
    public partial class FrmCobrador : Form 
    {
        static internal FrmCobrador frmCobrador;
        OleDbConnection conexion;
        internal OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor;
        internal DataSet datos;
        BindingManagerBase bmb;

        string sql = "SELECT * FROM COBRADOR ORDER BY nombres";

        public FrmCobrador() 
        {
            FrmCobrador.frmCobrador = this; 
            InitializeComponent();
        }

        #region "carga"
        private void FrmCobrador_Load(object sender, EventArgs e) 
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            constructor = new OleDbCommandBuilder(adaptador);

            OleDbCommand insertar = new OleDbCommand("INSERT INTO COBRADOR (cedula, nombres, apellidos, telefono, celular, direccion) VALUES(@cedula, @nombres, @apellidos, @telefono, @celular, @direccion)", conexion);
            adaptador.InsertCommand = insertar;
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@cedula", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@nombres", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@apellidos", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@telefono", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@celular", OleDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new OleDbParameter("@direccion", OleDbType.VarChar));
                       
            datos = new DataSet();

            conexion.Open();
            adaptador.Fill(datos);
            conexion.Close();

            txtCedula.DataBindings.Add(new Binding("Text", datos.Tables[0], "cedula"));
            txtNombres.DataBindings.Add(new Binding("Text", datos.Tables[0], "nombres"));
            txtApellidos.DataBindings.Add(new Binding("Text", datos.Tables[0], "apellidos"));
            txtTelefono.DataBindings.Add(new Binding("Text", datos.Tables[0], "telefono"));
            txtCelular.DataBindings.Add(new Binding("Text", datos.Tables[0], "celular"));
            txtDireccion.DataBindings.Add(new Binding("Text", datos.Tables[0], "direccion"));

            dgvCobrador.DataSource = datos.Tables[0];

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

        #region "edicion"
        private void btnNuevo_Click(object sender, EventArgs e) 
        {
            txtCedula.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();

            txtCedula.Focus();
            btnInsertar.Enabled = true;
            btnNuevo.Enabled = false; 
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = true;
            btnInsertar.Enabled = false;

            bool q = false;

            foreach (Control control in this.Controls) 
            {
                if (control is TextBox)
                {
                    if (control.Text == "")
                    {
                        q = true;
                    }
                }
            }

            if (q == true) 
            {
                MessageBox.Show("No pueden quedar campos vacios", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                try 
                {
                    adaptador.InsertCommand.Parameters["@cedula"].Value = txtCedula.Text;
                    adaptador.InsertCommand.Parameters["@nombres"].Value = txtNombres.Text;
                    adaptador.InsertCommand.Parameters["@apellidos"].Value = txtApellidos.Text;
                    adaptador.InsertCommand.Parameters["@telefono"].Value = txtTelefono.Text;
                    adaptador.InsertCommand.Parameters["@celular"].Value = txtCelular.Text;
                    adaptador.InsertCommand.Parameters["@direccion"].Value = txtDireccion.Text;

                    conexion.Open();
                    int i = adaptador.InsertCommand.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("Datos insertados", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch (OleDbException oE) 
                {
                    MessageBox.Show(oE.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            datos.Clear(); 
            adaptador.Fill(datos);
            dgvCobrador.Refresh(); 
        }

        private void btnActualizar_Click(object sender, EventArgs e) 
        {
            if (TabControl1.TabPages[0].Focus())
            {
                if (dgvCobrador.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmCobradorActualizar frmCobradorActualizar = new FrmCobradorActualizar();
                    frmCobradorActualizar.Show();

                    FrmCobradorActualizar.frmCobradorActualziar.txtCedula.Text = dgvCobrador.CurrentRow.Cells[0].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtNombres.Text = dgvCobrador.CurrentRow.Cells[1].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtApellidos.Text = dgvCobrador.CurrentRow.Cells[2].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtTelefono.Text = dgvCobrador.CurrentRow.Cells[3].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtCelular.Text = dgvCobrador.CurrentRow.Cells[4].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtDireccion.Text = dgvCobrador.CurrentRow.Cells[5].Value.ToString();
                }
            }
            else if (TabControl1.TabPages[1].Focus())
            {
                if (dgvCobradorBuscar.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    FrmCobradorActualizar frmCobradorActualizar = new FrmCobradorActualizar();
                    frmCobradorActualizar.Show(); 

                    FrmCobradorActualizar.frmCobradorActualziar.txtCedula.Text = dgvCobradorBuscar.CurrentRow.Cells[0].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtNombres.Text = dgvCobradorBuscar.CurrentRow.Cells[1].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtApellidos.Text = dgvCobradorBuscar.CurrentRow.Cells[2].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtTelefono.Text = dgvCobradorBuscar.CurrentRow.Cells[3].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtCelular.Text = dgvCobradorBuscar.CurrentRow.Cells[4].Value.ToString();
                    FrmCobradorActualizar.frmCobradorActualziar.txtDireccion.Text = dgvCobradorBuscar.CurrentRow.Cells[5].Value.ToString();
                }
            }       
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region "buscar"
        private void cargarBusqueda(ComboBox cbo)
        {
            cbo.Items.Add("CC cobrador");
            cbo.Items.Add("nombres");
            cbo.Items.Add("apellidos");
            cbo.Items.Add("direccion");
        }
                       
        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            string sql;
            DataTable dt;

            if (cboBuscar.Text == "CC cobrador")
            {
                sql = "SELECT * FROM COBRADOR WHERE cedula LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvCobradorBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "nombres")
            {
                sql = "SELECT * FROM COBRADOR WHERE nombres LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvCobradorBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "apellidos")
            {
                sql = "SELECT * FROM COBRADOR WHERE apellidos LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvCobradorBuscar.DataSource = dt;
            }

            else if (cboBuscar.Text == "direccion")
            {
                sql = "SELECT * FROM COBRADOR WHERE direccion LIKE '%" + txtBuscar.Text + "%' ";
                adaptador = new OleDbDataAdapter(sql, conexion);

                dt = new DataTable();

                conexion.Open();
                adaptador.Fill(dt);
                conexion.Close();

                dgvCobradorBuscar.DataSource = dt;
            }

            if (dgvCobradorBuscar.Rows.Count < 1)
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
