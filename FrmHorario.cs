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
    public partial class FrmHorario : Form
    {
        public FrmHorario()
        {
            InitializeComponent();
        }

        private void FrmHorario_Load(object sender, EventArgs e)
        {
            // Faço o carregamento dos dados necessários para o Formulário funcionar

            CarregarFilmes();
            CarregarSala();
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            string strCon = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                MySqlDataAdapter dAdapt = new MySqlDataAdapter("SELECT h.idHorario, h.dtSessao, h.idFilme, f.nome, " +
                                                               "f.categoria, f.classificacao,  s.idSala, s.nome, s.qtdeTickets " +
                                                               " from horario h join filme f on(h.idFilme = f.idFilme) " +
                                                               " join sala s on(h.idSala = s.idSala)", con);

                DataTable dTable = new DataTable();

                dAdapt.Fill(dTable);

                gridHorario.DataSource = dTable;
                gridHorario.Columns["idHorario"].HeaderText = "ID";
                gridHorario.Columns["dtSessao"].HeaderText = "Data e Hora da Sessão";
                gridHorario.Columns["dtSessao"].Width = 200;
                gridHorario.Columns["nome"].HeaderText = "Nome do Filme";
                gridHorario.Columns["nome"].Width = 200;

                gridHorario.Columns["nome1"].HeaderText = "Sala";
                gridHorario.Columns["nome1"].Width = 100;

                gridHorario.Columns["idSala"].Visible = false;
                gridHorario.Columns["idFilme"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas no acesso aos dados.");
            }
            finally
            {
                // Sempre fechar a conexao !
                con.Close();
                con.Dispose();
            }
        }

        private void CarregarSala()
        {
            string strCon = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                MySqlDataAdapter dAdapt = new MySqlDataAdapter("SELECT * FROM sala", con);
                DataTable dTable = new DataTable();
                dAdapt.Fill(dTable);
                cbSala.ValueMember = "idSala";
                cbSala.DisplayMember = "nome";
                cbSala.DataSource = dTable;

                //cbSala.Items.Add("teste");
                cbSala.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas no acesso aos dados.");
            }
            finally
            {
                // Sempre fechar a conexao !
                con.Close();
                con.Dispose();
            }
        }
        public void CarregarFilmes()
        {
            string strCon = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                MySqlDataAdapter dAdapt = new MySqlDataAdapter("select * from filme", con);
                DataTable dTable = new DataTable();
                dAdapt.Fill(dTable);

                cbFilme.ValueMember = "idFilme";
                cbFilme.DisplayMember = "nome";
                cbFilme.DataSource = dTable;
                cbFilme.SelectedIndex = -1;
            }
            catch (Exception erro)
            {
                MessageBox.Show("falha no carregamento dos dados.");
            }
            finally
            {
                con.Close(); // GarbageCollector
                con.Dispose();
            }
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            //1o Estabelecer a conexao
            string strCon = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;

                if (txbId.Text == "")
                {
                    cmd.CommandText = "INSERT INTO horario ( idFilme, idSala, dtSessao ) VALUE " +
                                      " ( @idFilme, @idSala, @dtSessao ) ";
                    cmd.Parameters.AddWithValue("@idFilme", cbFilme.SelectedValue);
                    cmd.Parameters.AddWithValue("@idSala", cbSala.SelectedValue);
                    cmd.Parameters.AddWithValue("@dtSessao", Convert.ToDateTime(txbHorario.Text));
                }
                else
                {
                    cmd.CommandText = "UPDATE horario SET idFilme = @idFilme, idSala = @idSala, dtSessao = @dtSessao " +
                                      " WHERE idHorario = @idHorario";
                    cmd.Parameters.AddWithValue("@idFilme", cbFilme.SelectedValue);
                    cmd.Parameters.AddWithValue("@idSala", cbSala.SelectedValue);
                    cmd.Parameters.AddWithValue("@dtSessao", Convert.ToDateTime(txbHorario.Text));
                    cmd.Parameters.AddWithValue("@idHorario", txbId.Text);
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Operação concluída com sucesso !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas no acesso aos dados.");
            }
            finally
            {
                // Sempre fechar a conexao !
                con.Close();
                con.Dispose();
            }

            CarregaGrid();
            txbId.Text = "";
            cbSala.SelectedIndex = -1;
            cbFilme.SelectedIndex = -1;
            txbHorario.Text = "";
        }

        private void gridHorario_DoubleClick(object sender, EventArgs e)
        {
            if (gridHorario.SelectedRows.Count > 0)
            {
                txbId.Text = gridHorario.CurrentRow.Cells[0].Value.ToString();
                cbFilme.SelectedValue = gridHorario.CurrentRow.Cells[2].Value.ToString();
                cbSala.SelectedValue = gridHorario.CurrentRow.Cells[6].Value.ToString();
                txbHorario.Text = gridHorario.CurrentRow.Cells[1].Value.ToString();
            }

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            txbId.Text = "";
            cbSala.SelectedIndex = -1;
            cbFilme.SelectedIndex = -1;
            txbHorario.Text = "";
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            if (txbId.Text == "")
            {
                MessageBox.Show("Selecione um horário !");
                return;
            }

            //1o Estabelecer a conexao
            string strCon = "server=127.0.0.1;uid=root;database=banco12ia;SslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM horario WHERE idHorario = @idHorario";                
                cmd.Parameters.AddWithValue("@idHorario", txbId.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Operação concluída com sucesso !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas no acesso aos dados.");
            }
            finally
            {
                // Sempre fechar a conexao !
                con.Close();
                con.Dispose();
            }

            CarregaGrid();
            txbId.Text = "";
            cbSala.SelectedIndex = -1;
            cbFilme.SelectedIndex = -1;
            txbHorario.Text = "";
        }
    }
}
