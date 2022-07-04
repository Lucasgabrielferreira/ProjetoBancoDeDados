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
    public partial class FrmHRSample : Form
    {
        public FrmHRSample()
        {
            InitializeComponent();
        }

        private void FrmHRSample_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            CarregarFilmes();
            CarregarSala();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            //1o Estabelecer a conexao
            string strCon = "server=127.0.0.1;uid=root;database=cinema;SslMode=none;";
            //string strCon = "server=serverlab03;uid=alessandra;password=102030;database=alessandra;sslMode=none;";
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
                    cmd.Parameters.AddWithValue("@dtSessao", Convert.ToDateTime(txbSessao.Text));
                }
                else
                {
                    //AQUI O SEU CODIGO PARA ALTERAÇÂO !
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
            txbSessao.Text = "";
        }

        private void CarregarSala()
        {
            //1o Estabelecer a conexao
            string strCon = "server=127.0.0.1;uid=root;database=12ia;SslMode=none;";
            //string strCon = "server=serverlab03;uid=alessandra;password=102030;database=alessandra;sslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                // 2o Recupera dados do Banco e guarda em memória
                MySqlDataAdapter dAdapt = new MySqlDataAdapter("SELECT * FROM sala", con);

                DataTable dTable = new DataTable();

                dAdapt.Fill(dTable);

                cbSala.ValueMember = "idSala";
                cbSala.DisplayMember = "numero";
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

        private void CarregarFilmes()
        {
            string strCon = "server=127.0.0.1;uid=root;database=12ia;SslMode=none";
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

        private void CarregaGrid()
        {
            //1o Estabelecer a conexao
            string strCon = "server=127.0.0.1;uid=root;database=cinema;SslMode=none;";
            //string strCon = "server=serverlab03;uid=alessandra;password=102030;database=alessandra;sslMode=none;";
            MySqlConnection con = new MySqlConnection(strCon);
            try
            {
                con.Open();
                // 2o Recupera dados do Banco e guarda em memória
                MySqlDataAdapter dAdapt = new MySqlDataAdapter("SELECT h.idHorario, h.dtSessao, h.idFilme, " +
                                                               " f.nome, f.genero, f.classificacao, " +
                                                               " s.idSala, s.numero, s.qtdeTickets " +
                                                               " FROM horario h " +
                                                               " join filme f on(h.idFilme = f.idFilme) " +
                                                               " join sala s on(h.idSala = s.idSala)", con);

                DataTable dTable = new DataTable();

                dAdapt.Fill(dTable);

                gridHorario.DataSource = dTable;
                gridHorario.Columns["idHorario"].HeaderText = "ID";

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


        private void gridHorario_DoubleClick(object sender, EventArgs e)
        {
            if (gridHorario.SelectedRows.Count > 0)
            {
                txbId.Text = gridHorario.CurrentRow.Cells[0].Value.ToString();
                cbFilme.SelectedValue = gridHorario.CurrentRow.Cells[2].Value.ToString();
                cbSala.SelectedValue = gridHorario.CurrentRow.Cells[6].Value.ToString();
                txbSessao.Text = gridHorario.CurrentRow.Cells[1].Value.ToString();
            }
        }
    }
}
