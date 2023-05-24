using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pessoa_Endereco_MySQL
{
    public partial class FormCadastro : Form
    {
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;

        public FormCadastro()
        {
            InitializeComponent();

            servidor = "Server=localhost;Database=pessoa_endereco;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();

            atualizar_dataGRID();
        }

        private void atualizar_dataGRID()
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT nome, cpf, logradouro, estado FROM tbl_endereco INNER JOIN tbl_pessoa ON (tbl_endereco.id = fk_endereco);";

                MySqlDataAdapter adaptadorCADASTRO = new MySqlDataAdapter(comando);
                DataTable tabelaCADASTRO = new DataTable();

                adaptadorCADASTRO.Fill(tabelaCADASTRO);

                dataGridViewCADASTRO.DataSource = tabelaCADASTRO;

                dataGridViewCADASTRO.Columns["nome"].HeaderText = "Nome";
                dataGridViewCADASTRO.Columns["sobrenome"].HeaderText = "Sobrenome";
                dataGridViewCADASTRO.Columns["nome_social"].HeaderText = "Nome social";
                dataGridViewCADASTRO.Columns["rg"].HeaderText = "RG";
                dataGridViewCADASTRO.Columns["cpf"].HeaderText = "CPF";
                dataGridViewCADASTRO.Columns["data_nasc"].HeaderText = "Data de nascimento";
                dataGridViewCADASTRO.Columns["etnia"].HeaderText = "Etnia";
                dataGridViewCADASTRO.Columns["genero"].HeaderText = "Gênero";
                dataGridViewCADASTRO.Columns["logradouro"].HeaderText = "Logradouro";
                dataGridViewCADASTRO.Columns["bairro"].HeaderText = "Bairro";
                dataGridViewCADASTRO.Columns["cidade"].HeaderText = "Cidade";
                dataGridViewCADASTRO.Columns["estado"].HeaderText = "Estado";
                dataGridViewCADASTRO.Columns["uf"].HeaderText = "UF";
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            string estado = comboBoxESTADO.Text;
            string uf = comboBoxUF.Text;
            string ultimoID = "";

            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_endereco (logradouro, bairro, cidade, estado, uf) VALUES ('" + textBoxLOGRADOURO.Text + "', '" + textBoxBAIRRO.Text + "', '" + textBoxCIDADE.Text + "', '" + comboBoxESTADO.SelectedItem + "', '" + comboBoxUF.SelectedItem + "');";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Suas informações não foram cadastradas verifique com o administrador do sistema");
            }
            finally
            {
                conexao.Close();
            }
            textBoxLOGRADOURO.Clear();
            textBoxBAIRRO.Clear();
            textBoxCIDADE.Clear();

            //--------------------------------------------------//

            try
            {
                conexao.Open();
                comando.CommandText = "SELECT MAX(id) FROM tbl_endereco";
                MySqlDataReader readerID = comando.ExecuteReader();

                if (readerID.Read())
                {
                    ultimoID = readerID.GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro verifique com o administrador do sistema");
            }
            finally
            {
                conexao.Close();
            }

            //--------------------------------------------------//

            string genero = "";

            if (radioButtonMASC.Checked)
            {
                genero = "Masculino";
            }
            if (radioButtonFEM.Checked)
            {
                genero = "Feminino";
            }
            if (radioButtonOUT.Checked)
            {
                genero = "Outros";
            }

            //--------------------------------------------------//

            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_pessoa (nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxNOMESOCIAL.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePickerDATANASC.Value.ToString("yyyy-MM-dd") + "', '" + comboBoxETNIA.Text + "', '" + genero + "', " + ultimoID + ");";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Suas informações não foram cadastradas verifique com o administrador do sistema");
            }
            finally
            {
                conexao.Close();
            }
            atualizar_dataGRID();
            MessageBox.Show("Cadastrado com sucesso!");
            textBoxNOME.Clear();
            textBoxSOBRENOME.Clear();
            textBoxNOMESOCIAL.Clear();
            textBoxRG.Clear();
            textBoxCPF.Clear();
        }

        private void buttonFECHAR_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}