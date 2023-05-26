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

        string id_pessoa = "";
        string id_endereco = "";

        private void atualizar_dataGRID()
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT *  FROM tbl_pessoa INNER JOIN tbl_endereco ON (tbl_endereco.id = fk_endereco);";

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

        private void dataGridViewCADASTRO_MouseClick(object sender, MouseEventArgs e)
        {
            id_pessoa = dataGridViewCADASTRO.CurrentRow.Cells[0].Value.ToString();
            textBoxNOME.Text = dataGridViewCADASTRO.CurrentRow.Cells[1].Value.ToString();
            textBoxSOBRENOME.Text = dataGridViewCADASTRO.CurrentRow.Cells[2].Value.ToString();
            textBoxNOMESOCIAL.Text = dataGridViewCADASTRO.CurrentRow.Cells[3].Value.ToString();
            textBoxRG.Text = dataGridViewCADASTRO.CurrentRow.Cells[4].Value.ToString();
            textBoxCPF.Text = dataGridViewCADASTRO.CurrentRow.Cells[5].Value.ToString();
            dateTimePickerDATANASC.Text = dataGridViewCADASTRO.CurrentRow.Cells[6].Value.ToString();
            comboBoxETNIA.Text = dataGridViewCADASTRO.CurrentRow.Cells[7].Value.ToString();
            textBoxLOGRADOURO.Text = dataGridViewCADASTRO.CurrentRow.Cells[11].Value.ToString();
            textBoxBAIRRO.Text = dataGridViewCADASTRO.CurrentRow.Cells[12].Value.ToString();
            textBoxCIDADE.Text = dataGridViewCADASTRO.CurrentRow.Cells[13].Value.ToString();
            comboBoxESTADO.Text = dataGridViewCADASTRO.CurrentRow.Cells[14].Value.ToString();
            comboBoxUF.Text = dataGridViewCADASTRO.CurrentRow.Cells[15].Value.ToString();

            if (dataGridViewCADASTRO.CurrentRow.Cells[8].Value.ToString() == "Masculino")
            {
                radioButtonMASC.Checked = true;
            }

            if (dataGridViewCADASTRO.CurrentRow.Cells[8].Value.ToString() == "Feminino")
            {
                radioButtonFEM.Checked = true;
            }

            if (dataGridViewCADASTRO.CurrentRow.Cells[8].Value.ToString() == "OUTRO")
            {

                radioButtonOUT.Checked = true;
            }

            id_endereco = dataGridViewCADASTRO.CurrentRow.Cells[9].Value.ToString();
            MessageBox.Show(id_endereco);
        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "DELETE FROM tbl_pessoa WHERE id = " + id_pessoa + ";";
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("O cadastro não foi deletado, fale com o administrador do sistema!");
            }
            finally
            {
                conexao.Close();
            }
            atualizar_dataGRID();
        }

        private void buttonALTERAR_Click(object sender, EventArgs e)
        {
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
                comando.CommandText = "UPDATE tbl_pessoa SET nome = '" + textBoxNOME.Text + "', sobrenome = '" + textBoxSOBRENOME.Text + "', nome_social = '" + textBoxNOMESOCIAL.Text + "', rg = '" + textBoxRG.Text + "', cpf = '" + textBoxCPF.Text + "', data_nasc = '" + dateTimePickerDATANASC.Value.ToString("yyyy-MM-dd") + "', etnia = '" + comboBoxETNIA.Text + "', genero = '" + genero + "', fk_endereco = " + id_endereco + " WHERE id = " + id_pessoa + ";";
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seu cadastro não foi atualizado, verifique com o administrador do sistema!");
            }
            finally
            {
                conexao.Close();
            }
            MessageBox.Show("Seu cadastro foi alterado com sucesso!");
            atualizar_dataGRID();
            textBoxNOME.Clear();
            textBoxSOBRENOME.Clear();
            textBoxNOMESOCIAL.Clear();
            textBoxRG.Clear();
            textBoxCPF.Clear();           

            //--------------------------------------------------//

            try
            {
                conexao.Open();
                comando.CommandText = "UPDATE tbl_endereco SET logradouro = '" + textBoxLOGRADOURO.Text + "', bairro = '" + textBoxBAIRRO.Text + "', cidade = '" + textBoxCIDADE.Text + "', estado = '" + comboBoxESTADO.Text + "', uf = '" + comboBoxUF.Text + "' WHERE id = " + id_endereco + ";";
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seu cadastro não foi atualizado, verifique com o administrador do sistema!");
            }
            finally
            {
                conexao.Close();
            }
            MessageBox.Show("Seu cadastro foi alterado com sucesso!");
            atualizar_dataGRID();           
            textBoxLOGRADOURO.Clear();
            textBoxBAIRRO.Clear();
            textBoxCIDADE.Clear();
        }
    }
}