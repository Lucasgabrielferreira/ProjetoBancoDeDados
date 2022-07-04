using MySql.Data.MySqlClient;
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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btLogar_Click(object sender, EventArgs e)
        {
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();
                MySqlCommand meuComando = new MySqlCommand();
                meuComando.Connection = minhaConexao;
                meuComando.CommandText = "SELECT * FROM usuario WHERE LOGIN = @login AND SENHA = @senha ";

                meuComando.Parameters.AddWithValue("@login", txbLogin.Text);
                meuComando.Parameters.AddWithValue("@senha", txbSenha.Text);

                MySqlDataReader meuDataReader = meuComando.ExecuteReader();
                if (meuDataReader.HasRows == true)
                {
                    // tem linhas entao eu realizo a leitura
                    meuDataReader.Read();
                    MessageBox.Show("Seja bem vindo(a) "+ meuDataReader["nome"].ToString());
                    // this representa O FORMULARIO /A JANELA !! E aqui estamos fechando ela.
                    this.Dispose();
                }
                else  // nao trouxe dados
                {
                    MessageBox.Show("Usuário não encontrado.");
                    Application.Exit();
                }
            }
            catch (Exception erro) // ocorre quando der RUIM !!
            {
                MessageBox.Show("Erro na operação.");
            }
            finally // sempre ocorre !!
            {
                minhaConexao.Close();
                minhaConexao.Dispose();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }
    }
}
