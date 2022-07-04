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
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            // 1o Criar Connection String
            // 2o Criar Objeto de Conexão
            // 3o Abrir a Conexão
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();
                // 4o Criar um Objeto para executar comando SQL
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.Connection = minhaConexao;
                //objCommand.CommandText =
                //    "INSERT INTO usuario ( nome, login, senha ) " +
                //    "VALUES ( '" + txbNome.Text + "','" + txbLogin.Text + "','" + txbSenha.Text + "' )";

                if (txbId.Text == "") // vazio significa inclusão
                {
                    objCommand.CommandText =
                        "INSERT INTO usuario ( nome, login, senha ) VALUES ( @nome, @login, @senha  )";

                    objCommand.Parameters.AddWithValue("@nome", txbNome.Text);
                    objCommand.Parameters.AddWithValue("@login", txbLogin.Text);
                    objCommand.Parameters.AddWithValue("@senha", txbSenha.Text);
                }
                else  // senão 
                {
                    objCommand.CommandText =
                       "UPDATE usuario set nome = @nome, login = @login, senha = @senha " +
                       "where idUsuario = @idUsuario";

                    objCommand.Parameters.AddWithValue("@nome", txbNome.Text);
                    objCommand.Parameters.AddWithValue("@login", txbLogin.Text);
                    objCommand.Parameters.AddWithValue("@senha", txbSenha.Text);
                    objCommand.Parameters.AddWithValue("@idUsuario", txbId.Text);
                }

                objCommand.ExecuteNonQuery();

                txbId.Text = "";
                txbNome.Text = "";
                txbSenha.Text = "";
                txbLogin.Text = "";

                MessageBox.Show("Operação realizada com Sucesso !");               
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
            CarregarGrid();
        }

        // METODO
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        // Metodo
        private void CarregarGrid()
        {
            // trazer dados do meu Banco
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();
                // Prepara a massa de dados para trazer para a sua aplicação
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter("select * from usuario", minhaConexao);

                // organizar as informações no formato linha coluna
                DataTable objDataTable = new DataTable();
                // Preenche o datatable com os dados
                objDataAdapter.Fill(objDataTable);

                gridUsuario.DataSource = objDataTable;
                gridUsuario.Columns["idUsuario"].HeaderText = "Id";
                gridUsuario.Columns["nome"].HeaderText = "Nome do Usuário";
                gridUsuario.Columns["nome"].Width = 300;
                //gridUsuario.Columns["senha"].Visible = false;
              
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

        // metodo criados pelo VS
        private void gridUsuario_DoubleClick(object sender, EventArgs e)
        {
            if (gridUsuario.SelectedRows.Count > 0) 
            {
                txbId.Text = gridUsuario.CurrentRow.Cells[0].Value.ToString();
                txbNome.Text = gridUsuario.CurrentRow.Cells[1].Value.ToString();
                txbLogin.Text = gridUsuario.CurrentRow.Cells[2].Value.ToString();
                txbSenha.Text = gridUsuario.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {          
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();               
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.Connection = minhaConexao;              
                objCommand.CommandText =
                    "UPDATE usuario set nome = @nome, login = @login, senha = @senha " +
                    "where idUsuario = @idUsuario";

                objCommand.Parameters.AddWithValue("@nome", txbNome.Text);
                objCommand.Parameters.AddWithValue("@login", txbLogin.Text);
                objCommand.Parameters.AddWithValue("@senha", txbSenha.Text);
                objCommand.Parameters.AddWithValue("@idUsuario", txbId.Text);

                objCommand.ExecuteNonQuery();

                txbId.Text = "";
                txbNome.Text = "";
                txbSenha.Text = "";
                txbLogin.Text = "";

                MessageBox.Show("Alteração realizada com Sucesso !");
            }
            catch (Exception erro) 
            {
                MessageBox.Show("Erro na operação.");
            }
            finally 
            {
                minhaConexao.Close();
                minhaConexao.Dispose();
            }
            CarregarGrid();
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            if (txbId.Text == "")
            {
                MessageBox.Show("Selecione o usuário a ser excluído !");
                // Força o Método a encerrar o processamento
                return;
            }

            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.Connection = minhaConexao;
                objCommand.CommandText =
                    "DELETE FROM usuario where idUsuario = @idUsuario";

                objCommand.Parameters.AddWithValue("@idUsuario", txbId.Text);

                objCommand.ExecuteNonQuery();

                txbId.Text = "";
                txbNome.Text = "";
                txbSenha.Text = "";
                txbLogin.Text = "";

                MessageBox.Show("Exclusão realizada com Sucesso !");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na operação.");
            }
            finally
            {
                minhaConexao.Close();
                minhaConexao.Dispose();
            }
            CarregarGrid();
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            txbId.Text = "";
            txbNome.Text = "";
            txbSenha.Text = "";
            txbLogin.Text = "";
        }

        private void txbId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
