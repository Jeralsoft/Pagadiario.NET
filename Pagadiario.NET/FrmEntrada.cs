using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pagadiario.NET
{
    public partial class FrmEntrada : Form
    {
        static internal FrmEntrada frmEntrada;
        public FrmEntrada()
        {
            FrmEntrada.frmEntrada = this;
            InitializeComponent();
        }

        private void FrmEntrada_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value+=5;
            progressBar2.Value+=5;
            label2.Text = "Loading... " + progressBar1.Value.ToString() + " %";

            if(progressBar1.Value == 100 || progressBar2.Value == 100){
                timer1.Enabled = false;
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Show();
            }
        }

    }//end class
}//end namespace
