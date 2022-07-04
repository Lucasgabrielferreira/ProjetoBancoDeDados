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
    public partial class FrmSala : Form
    {
        public FrmSala()
        {
            InitializeComponent();
        }

        private void FrmSala_Load(object sender, EventArgs e)
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
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter("select * from sala", minhaConexao);

                // organizar as informações no formato linha coluna
                DataTable objDataTable = new DataTable();
                // Preenche o datatable com os dados
                objDataAdapter.Fill(objDataTable);

                gridSala.DataSource = objDataTable;
                gridSala.Columns["idSala"].HeaderText = "Id";
                gridSala.Columns["nome"].HeaderText = "Nome do Sala";
                gridSala.Columns["qtdeTickets"].HeaderText = "Qtde de Tickets";              
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

        private void btGravar_Click(object sender, EventArgs e)
        {            
            string conStr = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection minhaConexao = new MySqlConnection(conStr);
            try
            {
                minhaConexao.Open();
               
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.Connection = minhaConexao;
               
                if (txbId.Text == "") // vazio significa inclusão
                {
                    objCommand.CommandText =
                        "INSERT INTO sala ( nome, qtdeTickets ) VALUES ( @nome, @qtdeTickets  )";

                    objCommand.Parameters.AddWithValue("@nome", txbNome.Text);
                    objCommand.Parameters.AddWithValue("@qtdeTickets", nrTickets.Value);                   
                }
                else  // senão 
                {
                    objCommand.CommandText =
                       "UPDATE sala set nome = @nome, qtdeTickets = @qtdeTickets "+
                       "where idSala = @idSala";

                    objCommand.Parameters.AddWithValue("@nome", txbNome.Text);
                    objCommand.Parameters.AddWithValue("@qtdeTickets", nrTickets.Value);
                    objCommand.Parameters.AddWithValue("@idSala", txbId.Text);
                }

                objCommand.ExecuteNonQuery();

                txbId.Text = "";
                txbNome.Text = "";
                nrTickets.Value = 0;

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

        private void gridSala_DoubleClick(object sender, EventArgs e)
        {
            if (gridSala.SelectedRows.Count > 0)
            {
                txbId.Text = gridSala.CurrentRow.Cells[0].Value.ToString();
                txbNome.Text = gridSala.CurrentRow.Cells[1].Value.ToString();
                nrTickets.Value = Convert.ToInt32(gridSala.CurrentRow.Cells[2].Value.ToString());               
            }
        }
    }
}
