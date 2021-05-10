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
    public partial class Form10 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool insertAvto;
        bool checkAvto;
        Form11 f11;

        public Form10()
        {
            InitializeComponent();
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
            GetData("select * from ViewAvto");
            dataGridView1.DataSource = bindingSource1;


            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            button2.Text = "Подробная информация";
            button2.Size = new Size(200, 29);
            button2.Location = new Point(1150, 799);
            checkBox1.Enabled = false;


            checkAvto = true;


        }
        private void GetData(string selectCommand)
        {
            try
            {

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
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Да" : "Нет";
                    e.FormattingApplied = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            insertAvto = true;
            checkAvto = false;


            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            button2.Text = "Добавить";
            button2.Size = new Size(102, 29);
            button2.Location = new Point(1262, 799);

            label14.Text = "В данный момент вы находитесь в режиме добавления Автомобиля";
            label14.ForeColor = Color.Green;
            label14.Location = new Point(411, 799);
            button1.Text = "Отмена";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(insertAvto == true)
            {
                insertAvto = false;
                label14.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                checkAvto = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                button2.Text = "Подробная информация";
                button2.Size = new Size(200, 29);
                button2.Location = new Point(1150, 799);
                button1.Text = "Выход";
                return;

            }
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            string query;
            query = ("SELECT DISTINCT  Klass FROM KlassAvto ");
            SqlCommand cmd1 = new SqlCommand(query, con);
            DataTable tbl1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(tbl1);
            this.comboBox2.DataSource = tbl1;
            this.comboBox2.DisplayMember = "Klass";// столбец для отображения
            //this.comboBox2.ValueMember = "idKlassa";//столбец с id
            this.comboBox2.SelectedIndex = -1;

            con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query;
            query = String.Format("SELECT  Tip FROM KlassAvto Where Klass = '" + comboBox2.Text + " '", con); 
            SqlCommand cmd1 = new SqlCommand(query, con);
            DataTable tbl1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(tbl1);
            this.comboBox3.DataSource = tbl1;
            this.comboBox3.DisplayMember = "Tip";// столбец для отображения
            //this.comboBox2.ValueMember = idKlassa";//столбец с id
            this.comboBox3.SelectedIndex = -1;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Гос номеру")
            {
                string query;
                query = string.Format("SELECT * FROM ViewAvto WHERE GosNomer LIKE '{0}%'", textBox7.Text);
                GetData(query);
            }
            else if (comboBox5.Text == "Вин Номеру")
            {
                string query;
                query = string.Format("SELECT * FROM ViewAvto WHERE VIN LIKE '{0}%'", textBox7.Text);
                GetData(query);
            }
            else if (comboBox5.Text == "Году выпуска")
            {
                string query;
                query = string.Format("SELECT * FROM ViewAvto WHERE GodVipyska LIKE '{0}%'", textBox7.Text);
                GetData(query);
            }
            else if (comboBox5.Text == "Марке")
            {
                string query;
                query = string.Format("SELECT * FROM ViewAvto WHERE Marka LIKE '{0}%'", textBox7.Text);
                GetData(query);
            }
            else if (comboBox5.Text == "Названию")
            {
                string query;
                query = string.Format("SELECT * FROM ViewAvto WHERE Nazvanie LIKE '{0}%'", textBox7.Text);
                GetData(query);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked && textBox7.TextLength == 0)
            {
                if (toolStripComboBox1.Text == "Исправные")
                {
                    string query;
                    query = "SELECT * FROM ViewAvto WHERE StatusDostypa = 'True'";
                    GetData(query);
                }
                else if (toolStripComboBox1.Text == "Неисправные")
                {
                    string query;
                    query = "SELECT * FROM ViewAvto WHERE StatusDostypa = 'False'";
                    GetData(query);
                }
                else
                {
                    GetData("select * from ViewAvto");
                }
            }
            if (checkBox1.CheckState == CheckState.Unchecked && textBox7.TextLength == 0)
            {
                GetData("select * from ViewAvto");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkAvto == true)
            {
                Program.getIdAvto = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                f11 = new Form11();
                f11.ShowDialog();
                GetData("select * from ViewAvto");

            }
           else if(insertAvto == true)
            {
                String insertValueMarka = textBox1.Text;
                String insertValueNazvanie = textBox2.Text;
                String insertValueGosNumber = textBox3.Text;
                String insertValueYear = comboBox1.Text;
                String insertValueVin = textBox4.Text;
                String insertValueProbeg = textBox5.Text;
                String insertValueKlass = comboBox2.Text;
                String insertValueTipKlassa = comboBox3.Text;
                String insertValueTipPolica = comboBox4.Text;
                String insertValueNumberPolica = textBox6.Text;
                String insertValueStatus = "1";

                if (textBox1.Text.Length == 0)
                {
                    label14.Text = "Марка автомобиля не может быть пустой";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (textBox2.Text.Length == 0)
                {
                    label14.Text = "Название автомобиля не может быть пустым";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (textBox3.Text.Length != 9)
                {
                    label14.Text = "Гос Номер должен состоять из 9 знаков";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (comboBox1.Text.Length == 0)
                {
                    label14.Text = "Пожалуйста, выберете год выпуска";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (textBox4.Text.Length != 17)
                {
                    label14.Text = "Вин Номер должен состоять из 17 символов";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(500, 799);
                    return;
                }
                if (textBox5.Text.Length == 0)
                {
                    label14.Text = "Пробег не может быть пустым";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(550, 799);
                    return;
                }
                if (comboBox2.Text.Length == 0)
                {
                    label14.Text = "Пожалуйста, выберете Класс автомобиля";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (comboBox3.Text.Length == 0)
                {
                    label14.Text = "Пожалуйста, выберете Тип класса автомобиля";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (comboBox4.Text.Length == 0)
                {
                    label14.Text = "Пожалуйста, выберете Тип полиса автомобиля";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                if (textBox6.Text.Length != 10)
                {
                    label14.Text = "Номер полиса должен состоять из 10 знаков";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(535, 799);
                    return;
                }
                

                con = new SqlConnection(connectionString);
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From Avto WHERE GosNomer = '" + insertValueGosNumber + " '", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    label14.Text = "К сожалению, данный Гос номер уже существует в Базе данных";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(420, 799);
                    return;
                }

                SqlDataAdapter sda1 = new SqlDataAdapter("Select Count (*) From Avto WHERE VIN = '" + insertValueVin + " '", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);

                if (dt1.Rows[0][0].ToString() != "0")
                {
                    label14.Text = "К сожалению, данный Вин номер уже существует в Базе данных";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(420, 799);
                    return;
                }

                SqlDataAdapter sda2 = new SqlDataAdapter("Select Count (*) From Strahovka WHERE NomerPolica = '" + insertValueNumberPolica + " '", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);

                if (dt2.Rows[0][0].ToString() != "0")
                {
                    label14.Text = "К сожалению, данный Номер полиса уже есть в Базе данных";
                    label14.ForeColor = Color.Orange;
                    label14.Location = new Point(420, 799);
                    return;
                }

                string idKlassaSelect = "SELECT idKlassa FROM KlassAvto Where Klass ='" + insertValueKlass + "' AND Tip = '" + insertValueTipKlassa + " '";
                SqlCommand idKlassa = new SqlCommand(idKlassaSelect, con);
                Int32 idKlassaInt = (Int32)(idKlassa).ExecuteScalar();

                string sqlInsertNewPolic = string.Format("INSERT INTO Strahovka (TipPolica, NomerPolica ) " +
                    " VALUES ('{0}', '{1}')", insertValueTipPolica, insertValueNumberPolica);
                SqlCommand insNewPolic = new SqlCommand(sqlInsertNewPolic, con);
                insNewPolic.ExecuteNonQuery();

                string idPolicaSelect = "SELECT KodPolica FROM Strahovka Where TipPolica ='" + insertValueTipPolica + "' AND NomerPolica = '" + insertValueNumberPolica + " '";
                SqlCommand idPolica = new SqlCommand(idPolicaSelect, con);
                Int32 idPolicaInt = (Int32)(idPolica).ExecuteScalar();

                string sqlInsertNewAvto = string.Format("INSERT INTO Avto (Marka, Nazvanie, GosNomer, GodVipyska, VIN, Probeg, idKlassa, KodPolica, StatusDostypa ) " +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, '{8}' )", insertValueMarka, insertValueNazvanie, insertValueGosNumber, insertValueYear, insertValueVin, insertValueProbeg,
                   idKlassaInt, idPolicaInt, insertValueStatus);
                SqlCommand insNewAvto = new SqlCommand(sqlInsertNewAvto, con);
                insNewAvto.ExecuteNonQuery();
                con.Close();

                GetData("select * from ViewAvto");
                label14.Text = "";
                label14.Location = new Point(411, 799);
                insertAvto = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                button2.Text = "Подробная информация";
                button2.Size = new Size(200, 29);
                button2.Location = new Point(1150, 799);
                checkAvto = true;
            }
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
           

        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            
            
                checkBox1.Enabled = true;
            
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
         
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
        }
    }
}
