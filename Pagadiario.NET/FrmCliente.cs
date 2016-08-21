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
    
    public partial class FrmCliente : Form 
    {
        static internal FrmCliente frmCliente;
        OleDbConnection conexion;
        internal OleDbDataAdapter adaptador;
        OleDbCommandBuilder constructor; 
        internal DataSet datos;
        BindingManagerBase bmb; 

        string sql = "SELECT * FROM CLIENTE ORDER BY nombres";

        public FrmCliente() 
        {
            FrmCliente.frmCliente = this; 
            InitializeComponent();
        }

        #region "carga"
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            conexion = new OleDbConnection(Conexion.conectar());
            adaptador = new OleDbDataAdapter(sql, conexion);
            constructor = new OleDbCommandBuilder(adaptador);

            OleDbCommand insertar = new OleDbCommand("INSERT INTO CLIENTE (cedula, nombres, apellidos, telefono, celular, direccion) VALUES(@cedula, @nombres, @apellidos, @telefono, @celular, @direccion)", conexion);
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

            dgvCliente.DataSource = datos.Tables[0];

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
                try {
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
                catch(OleDbException oE){
                    MessageBox.Show(oE.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            datos.Clear();
            adaptador.Fill(datos);
            dgvCliente.Refresh();
        }

        private void btnActualizar_Click(object sender, EventArgs e) 
        {
            if (TabControl1.TabPages[0].Focus())
            {
                if (dgvCliente.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmClienteActualizar frmClienteActualizar = new FrmClienteActualizar();
                    frmClienteActualizar.Show();

                    FrmClienteActualizar.frmClienteActualizar.txtCedula.Text = dgvCliente.CurrentRow.Cells[0].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtNombres.Text = dgvCliente.CurrentRow.Cells[1].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtApellidos.Text = dgvCliente.CurrentRow.Cells[2].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtTelefono.Text = dgvCliente.CurrentRow.Cells[3].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtCelular.Text = dgvCliente.CurrentRow.Cells[4].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtDireccion.Text = dgvCliente.CurrentRow.Cells[5].Value.ToString();
                }
            }
            else if(TabControl1.TabPages[1].Focus())
            {
                if (dgvClienteBuscar.Rows.Count < 1)
                {
                    MessageBox.Show("No hay registros para actualizar", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    FrmClienteActualizar frmClienteActualizar = new FrmClienteActualizar();
                    frmClienteActualizar.Show();

                    FrmClienteActualizar.frmClienteActualizar.txtCedula.Text = dgvClienteBuscar.CurrentRow.Cells[0].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtNombres.Text = dgvClienteBuscar.CurrentRow.Cells[1].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtApellidos.Text = dgvClienteBuscar.CurrentRow.Cells[2].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtTelefono.Text = dgvClienteBuscar.CurrentRow.Cells[3].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtCelular.Text = dgvClienteBuscar.CurrentRow.Cells[4].Value.ToString();
                    FrmClienteActualizar.frmClienteActualizar.txtDireccion.Text = dgvClienteBuscar.CurrentRow.Cells[5].Value.ToString();
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
            cbo.Items.Add("CC cliente");
            cbo.Items.Add("nombres");
            cbo.Items.Add("apellidos");
            cbo.Items.Add("direccion");
         } 

         private void txtBuscar_TextChanged(object sender, EventArgs e)
         {
             string sql;
             DataTable dt; 

             if(cboBuscar.Text == "CC cliente")
             {
                 sql = "SELECT * FROM CLIENTE WHERE cedula LIKE '%"+txtBuscar.Text+"%' ORDER BY cedula";
                 adaptador = new OleDbDataAdapter(sql, conexion);

                 dt = new DataTable();

                 conexion.Open();
                 adaptador.Fill(dt);
                 conexion.Close();

                 dgvClienteBuscar.DataSource = dt;
             }

             else if (cboBuscar.Text == "nombres")
             {
                 sql = "SELECT * FROM CLIENTE WHERE nombres LIKE '%" + txtBuscar.Text + "%' ORDER BY nombres";
                 adaptador = new OleDbDataAdapter(sql, conexion);

                 dt = new DataTable();

                 conexion.Open();
                 adaptador.Fill(dt);
                 conexion.Close();

                 dgvClienteBuscar.DataSource = dt;
             }

             else if (cboBuscar.Text == "apellidos")
             {
                 sql = "SELECT * FROM CLIENTE WHERE apellidos LIKE '%" + txtBuscar.Text + "%' ORDER BY apellidos";
                 adaptador = new OleDbDataAdapter(sql, conexion);

                 dt = new DataTable();

                 conexion.Open();
                 adaptador.Fill(dt);
                 conexion.Close();

                 dgvClienteBuscar.DataSource = dt;
             }

             else if (cboBuscar.Text == "direccion")
             {
                 sql = "SELECT * FROM CLIENTE WHERE direccion LIKE '%" + txtBuscar.Text + "%' ORDER BY direccion";
                 adaptador = new OleDbDataAdapter(sql, conexion);

                 dt = new DataTable();

                 conexion.Open();
                 adaptador.Fill(dt);
                 conexion.Close();

                 dgvClienteBuscar.DataSource = dt;
             }

             if (dgvClienteBuscar.Rows.Count < 1)
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
