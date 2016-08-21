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
    public partial class FrmLogin : Form
    {
        public static String user; 

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text; 
            string contraseña = txtContraseña.Text;

            if (validar(usuario, contraseña))
            {
                FrmMenu frmMenu = new FrmMenu();
                frmMenu.Show();

                this.Close(); 
            }
            else 
            {
                MessageBox.Show("Datos incorrectos", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Clear();
                txtContraseña.Clear();

                txtUsuario.Focus(); 
            }
        }

        private bool validar(string usuario, string contraseña) 
        {
            OleDbConnection conexion = new OleDbConnection(Conexion.conectar());
            OleDbCommand comando;

            conexion.Open();

            string sql = "SELECT COUNT(*) FROM USUARIO WHERE usuario='"+txtUsuario.Text+"' AND contraseña='"+txtContraseña.Text+"' ";

            comando = conexion.CreateCommand();
            comando.CommandText = sql;

            int i;
            i = Convert.ToInt16(comando.ExecuteScalar());

            conexion.Close();

            if(i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir de la aplicacion", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.ExitThread(); 
            }
        }

    }//end class
}//end namespace



