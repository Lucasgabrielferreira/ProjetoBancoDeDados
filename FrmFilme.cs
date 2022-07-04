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
    public partial class FrmFilme : Form
    {
        public FrmFilme()
        {
            InitializeComponent();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();                   
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.Connection = minhaConexao;      
                objCommand.CommandText = "INSERT INTO filme ( nome, categoria, classificacao ) " +
                                         "VALUES ( @nome, @categoria, @classificacao  )";
                objCommand.Parameters.AddWithValue( "@nome", txbNome.Text);
                objCommand.Parameters.AddWithValue( "@categoria", cbCategoria.SelectedItem.ToString() );
                objCommand.Parameters.AddWithValue( "@classificacao", cbClassificacao.SelectedItem.ToString());
                objCommand.ExecuteNonQuery();
                txbId.Text   = "";
                txbNome.Text = "";
                cbCategoria.SelectedIndex = -1;
                cbClassificacao.SelectedIndex = -1;
                MessageBox.Show("Inclusão realizada com Sucesso !");
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

           // CarregarGrid();

        }
    }
}
