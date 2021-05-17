using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSharing
{
    public partial class Form12 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Boolean insertTarif;
        Boolean updateTarif;
        Boolean deleteTarif;
        Logger logger;
        CurrentMethod cm;

        public Form12()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            GetData("Select * From ViewTarif");
            dataGridView1.DataSource = bindingSource1;

            comboBox1.Size = new Size(260, 26);
            comboBox2.Size = new Size(121, 26);
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox4.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            label2.Text = "";
            button2.Visible = false;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }

        private void GetData(string selectCommand)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                dataGridView1.AutoGenerateColumns = true;
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertTarif = true;
            updateTarif = false;
            deleteTarif = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            comboBox4.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            label2.Text = "В данный момент вы находитесь в режиме добавления нового тарифа";
            label2.ForeColor = Color.Green;
            label2.Location = new Point(192, 551);
            button2.Visible = true;
            button2.Text = "Добавить";
            button2.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox4.Text = "";
            textBox4.Text = "";
            comboBox1.Text = " ";
            comboBox2.Text = " ";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertTarif = false;
            updateTarif = false;
            deleteTarif = true;
            label2.Text = "В данный момент вы находитесь в режиме удаления тарифа";
            label2.ForeColor = Color.Red;
            label2.Location = new Point(230, 551);
            button2.Text = "Удалить";
            button2.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox4.Text = "";
            textBox4.Text = "";
            comboBox1.Text = " ";
            comboBox2.Text = " ";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox4.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertTarif = false; 
            updateTarif = true;
            deleteTarif = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            comboBox4.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            label2.Text = "В данный момент вы находитесь в режиме редактирования тарифа";
            label2.ForeColor = Color.Blue;
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            comboBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            comboBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
            comboBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
            label2.Location = new Point(205, 551);
            button2.Text = "Изменить";
            button2.Visible = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                string query;
                query = String.Format("SELECT  Tip FROM KlassAvto Where Klass = '" + comboBox1.Text + " '", con);
                SqlCommand cmd1 = new SqlCommand(query, con);
                DataTable tbl1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(tbl1);
                this.comboBox2.DataSource = tbl1;
                this.comboBox2.DisplayMember = "Tip";// столбец для отображения
                                                     //this.comboBox2.ValueMember = idKlassa";//столбец с id
                this.comboBox2.SelectedIndex = -1;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                string query;
                query = ("SELECT DISTINCT Klass FROM KlassAvto ");
                SqlCommand cmd1 = new SqlCommand(query, con);
                DataTable tbl1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(tbl1);
                this.comboBox1.DataSource = tbl1;
                this.comboBox1.DisplayMember = "Klass";// столбец для отображения
                                                       //this.comboBox2.ValueMember = "idKlassa";//столбец с id
                this.comboBox1.SelectedIndex = -1;

                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (insertTarif == true)
                {
                    updateTarif = false;
                    deleteTarif = false;
                    String insertValueNazvanie = textBox1.Text;
                    String insertValueTip = textBox2.Text;
                    String insertValueTime = comboBox4.Text;
                    String insertValueCena = textBox4.Text;
                    String insertValueKlass = comboBox1.Text;
                    String insertValueTypeOfKlass = comboBox2.Text;
                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        label2.Text = "Название тарифа должно быть не короче 5 символов";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(250, 551);
                        return;
                    }

                    if (textBox2.Text.Length < 3)
                    {
                        label2.Text = "Тип тарифа должен быть не короче 3 символов";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(270, 551);
                        return;
                    }

                    if (comboBox4.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите время тарифа ";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(280, 551);
                        return;
                    }

                    if (textBox4.Text.Length == 0)
                    {
                        label2.Text = "Цена за минуту не может быть пустой";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(290, 551);
                        return;
                    }

                    if (comboBox1.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите класс автомобиля";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(290, 551);
                        return;
                    }

                    if (comboBox2.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите тип класса автомобиля";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(280, 551);
                        return;
                    }

                    string idKlassaSelect = "SELECT idKlassa FROM KlassAvto Where Klass ='" + insertValueKlass + "' AND Tip = '" + insertValueTypeOfKlass + " '";
                    SqlCommand idKlassa = new SqlCommand(idKlassaSelect, con);
                    Int32 idKlassaInt = (Int32)(idKlassa).ExecuteScalar();

                    string sqlInsertNewTarif = string.Format("INSERT INTO Tarif (Nazvanie, TipTarifa, VremyaTarifa, Cena, idKlassa ) " +
                        " VALUES ('{0}', '{1}', '{2}', {3}, {4})", insertValueNazvanie, insertValueTip, insertValueTime, insertValueCena, idKlassaInt);
                    SqlCommand insNewTarif = new SqlCommand(sqlInsertNewTarif, con);
                    insNewTarif.ExecuteNonQuery();
                    con.Close();
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    //  textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox4.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    //  textBox3.Text = "";
                    textBox4.Text = "";
                    comboBox1.Text = " ";
                    comboBox2.Text = " ";
                    label2.Text = "";
                    comboBox4.Text = "";
                    button2.Visible = false;
                    label2.Location = new Point(192, 551);
                    GetData("Select * From ViewTarif");
                    insertTarif = false;
                }
                if (updateTarif == true)
                {
                    insertTarif = false;
                    deleteTarif = false;
                    String insertValueNazvanie = textBox1.Text;
                    String insertValueTip = textBox2.Text;
                    String insertValueTime = comboBox4.Text;
                    String insertValueCena = textBox4.Text;
                    String insertValueKlass = comboBox1.Text;
                    String insertValueTypeOfKlass = comboBox2.Text;
                    String insertValueIdTarif = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        label2.Text = "Название тарифа должно быть не короче 5 символов";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(250, 551);
                        return;
                    }

                    if (textBox2.Text.Length < 3)
                    {
                        label2.Text = "Тип тарифа должен быть не короче 3 символов";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(270, 551);
                        return;
                    }



                    if (textBox4.Text.Length == 0)
                    {
                        label2.Text = "Цена за минуту не может быть пустой";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(290, 551);
                        return;
                    }

                    if (comboBox1.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите класс автомобиля";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(290, 551);
                        return;
                    }

                    if (comboBox2.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите тип класса автомобиля";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(280, 551);
                        return;
                    }
                    if (comboBox4.Text.Length == 0)
                    {
                        label2.Text = "Пожалуйста, выберите время тарифа ";
                        label2.ForeColor = Color.Orange;
                        label2.Location = new Point(280, 551);
                        return;
                    }
                    string idKlassaSelect = "SELECT idKlassa FROM KlassAvto Where Klass ='" + insertValueKlass + "' AND Tip = '" + insertValueTypeOfKlass + " '";
                    SqlCommand idKlassa = new SqlCommand(idKlassaSelect, con);
                    Int32 idKlassaInt = (Int32)(idKlassa).ExecuteScalar();

                    string sqlUpdateTarif = string.Format("UPDATE Tarif SET Nazvanie = '{0}' , TipTarifa = '{1}' , VremyaTarifa = '{2}', Cena = {3}, idKlassa = {4}  WHERE idTarifa = {5}",
                                 insertValueNazvanie, insertValueTip, insertValueTime, insertValueCena, idKlassaInt, insertValueIdTarif);
                    SqlCommand updTarif = new SqlCommand(sqlUpdateTarif, con);
                    updTarif.ExecuteNonQuery();
                    con.Close();
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    //   textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox4.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    //  textBox3.Text = "";
                    textBox4.Text = "";
                    comboBox1.Text = " ";
                    comboBox2.Text = " ";
                    comboBox4.Text = "";
                    label2.Text = "";
                    button2.Visible = false;
                    label2.Location = new Point(192, 551);
                    GetData("Select * From ViewTarif");
                    updateTarif = false;

                }
                if (deleteTarif == true)
                {
                    con = new SqlConnection(connectionString);
                    con.Open();
                    insertTarif = false;
                    updateTarif = false;
                    String insertValueIdTarif = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    string message = "Вы действительно хотите удалить данный тариф?";
                    string caption = "Удаление тарифа";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string sqlDelTarif = string.Format("DELETE FROM Tarif WHERE idTarifa = {0}", insertValueIdTarif);
                        SqlCommand delTarif = new SqlCommand(sqlDelTarif, con);
                        delTarif.ExecuteNonQuery();
                        GetData("select * from ViewTarif");

                        con.Close();
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        comboBox4.Enabled = false;
                        textBox4.Enabled = false;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox4.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = " ";
                        comboBox2.Text = " ";
                        label2.Text = "";
                        button2.Visible = false;
                        label2.Location = new Point(192, 551);
                        deleteTarif = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (comboBox3.Text == "Названию")
            {
                string query;
                query = string.Format("SELECT * FROM ViewTarif WHERE Nazvanie LIKE '{0}%'", textBox5.Text);
                GetData(query);
            }
            else if (comboBox3.Text == "Типу")
            {
                string query;
                query = string.Format("SELECT * FROM ViewTarif WHERE TipTarifa LIKE '{0}%'", textBox5.Text);
                GetData(query);
            }
            else if (comboBox3.Text == "Времени")
            {
                string query;
                query = string.Format("SELECT * FROM ViewTarif WHERE VremyaTarifa LIKE '{0}%'", textBox5.Text);
                GetData(query);
            }
            else if (comboBox3.Text == "Цене")
            {
                string query;
                query = string.Format("SELECT * FROM ViewTarif WHERE Cena LIKE '{0}%'", textBox5.Text);
                GetData(query);
            }
           
        }
    }

}
