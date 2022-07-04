using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoBancoDedados
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Chamar o Formulário de usuário . CRIAMOS UM OBJETO
            FrmUsuario frmUsuario = new FrmUsuario();

            frmUsuario.Show();
        }

        private void filmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  FrmFilme é uma Classe
            FrmFilme frmFilme = new FrmFilme();

            frmFilme.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
            
        }

        private void salaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSala frmSala = new FrmSala();
            frmSala.ShowDialog();
        }

        private void horarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHRSample frmSample = new FrmHRSample();
            frmSample.ShowDialog();
        }

        private void horárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHorario frmHorario = new FrmHorario();
            frmHorario.Show();
        }
    }
}
