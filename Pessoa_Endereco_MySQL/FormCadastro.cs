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
        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            string estado = comboBoxESTADO.Text;
            string uf = comboBoxUF.Text;

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
                MessageBox.Show("Cadastrado com sucesso!");

                textBoxLOGRADOURO.Clear();
                textBoxBAIRRO.Clear();
                textBoxCIDADE.Clear();
            }

            try
            {
                conexao.Open();
                comando.CommandText = "SELECT MAX(id) FROM tbl_endereco";
                MySqlDataReader readerID = comando.ExecuteReader();
                
                string ultimoID;
                
                if(readerID.Read())
                {
                    ultimoID = readerID.GetString(0);
                    MessageBox.Show(ultimoID);
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

            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_pessoa (nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "', '" + textBoxNOMESOCIAL.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePicker1.Checked + "', '" + comboBoxETNIA.SelectedItem + "', '";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Suas informações não foram cadastradas verifique com o administrador do sistema");
            }
            finally
            {
                conexao.Close();
                MessageBox.Show("Cadastrado com sucesso!");
            }
            textBoxNOME.Clear();
            textBoxSOBRENOME.Clear();
            textBoxNOMESOCIAL.Clear();
            textBoxRG.Clear();
            textBoxCPF.Clear();
        }
    }
}

