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
    public partial class Form15 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool check;
        DateTime dateNow;

        DateTime dateEnd;
        Int64 dlitelnost;
        Form16 f16;
        Logger logger;
        CurrentMethod cm;
        public Form15()
        {
            InitializeComponent();
            dateNow =  dateTimePicker1.Value;
            dateTimePicker1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            con = new SqlConnection(connectionString);
            con.Open();
            string query;
            query = ("SELECT DISTINCT  Klass FROM KlassAvto ");
            SqlCommand cmd1 = new SqlCommand(query, con);
            DataTable tbl1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(tbl1);
            this.comboBox1.DataSource = tbl1;
            this.comboBox1.DisplayMember = "Klass";// столбец для отображения
            //this.comboBox2.ValueMember = "idKlassa";//столбец с id
            this.comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
            con.Close();
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
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            label12.Visible = false;
            button1.Enabled = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateTimePicker1.MinDate = DateTime.Now;
            

        }

        private void Form15_Load(object sender, EventArgs e)
        {
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                comboBox2.Enabled = true;
                string query;
                query = String.Format("SELECT  Tip FROM KlassAvto Where Klass = '" + comboBox1.Text + " '", con);
                SqlCommand cmd1 = new SqlCommand(query, con);
                DataTable tbl1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(tbl1);
                this.comboBox2.DataSource = tbl1;
                this.comboBox2.DisplayMember = "Tip";// столбец для отображения
                                                     //this.comboBox2.ValueMember = idKlassa";//столбец с id
                                                     // this.comboBox2.SelectedIndex = 1;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueNameOfKlass = comboBox1.Text;
                String insertValueTypeOfKlass = comboBox2.Text;
                string query;
                comboBox3.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
                try
                {
                    query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                    string query1;

                    query1 = String.Format("SELECT  * FROM Tarif Where idKlassa= '" + idKlass + " ' ", con);
                    SqlCommand cmd2 = new SqlCommand(query1, con);

                    DataTable tbl1 = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    da.Fill(tbl1);
                    this.comboBox3.DataSource = tbl1;
                    this.comboBox3.DisplayMember = "Nazvanie";// столбец для отображения
                                                              //this.comboBox2.ValueMember = idKlassa";//столбец с id
                    this.comboBox3.SelectedIndex = 0;
                    con.Close();
                }
                catch (Exception)
                {

                }
                con = new SqlConnection(connectionString);
                con.Open();



                try
                {
                    string query2;
                    query2 = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                    SqlCommand cmd1 = new SqlCommand(query2, con);
                    Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                    GetData("Select * from ViewAvto WHERE Klass= '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' AND StatusDostypa = 1 ");

                }
                catch (Exception)
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueNameOfKlass = comboBox1.Text;
                String insertValueTypeOfKlass = comboBox2.Text;
                String insertValueNameOFTarif = comboBox3.Text;
                dateTimePicker1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Text = "";

                string query;
                query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                SqlCommand cmd1 = new SqlCommand(query, con);
                Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                string query2;
                query2 = String.Format("SELECT  Cena FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                SqlCommand cmd2 = new SqlCommand(query2, con);
                Double cena = (Double)(cmd2).ExecuteScalar();
                label7.Text = cena + " р.";

                string query3;
                query3 = String.Format("SELECT  VremyaTarifa FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                SqlCommand cmd3 = new SqlCommand(query3, con);
                String time = (String)(cmd3).ExecuteScalar();

                if (time == "Минуты")
                {
                    label10.Text = "Длительность в минутах";
                }
                if (time == "Часы")
                {
                    label10.Text = "Длительность в часах";
                }
                if (time == "Дни")
                {
                    label10.Text = "Длительность в днях";
                }


                con.Close();
                textBox1.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                String insertValueNameOfKlass = comboBox1.Text;
                String insertValueTypeOfKlass = comboBox2.Text;
                String insertValueNameOFTarif = comboBox3.Text;
                if (label10.Text == "Длительность в минутах")
                {
                    Int32 timeInMinute;
                    if (textBox1.Text == "")
                    {
                        timeInMinute = 0;
                        textBox2.Text = "";
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) > 60)
                    {
                        label12.Visible = true;
                        label12.Text = "Для данного тарифа длительность не может быть более 60 минут";
                        label12.ForeColor = Color.Red;
                        label12.Location = new Point(227, 600);
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) < 60)
                    {
                        label12.Visible = false;
                        label12.Text = "";
                        label12.Location = new Point(227, 600);



                    }
                    timeInMinute = Convert.ToInt32(textBox1.Text);

                    con = new SqlConnection(connectionString);
                    con.Open();
                    string query;
                    query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                    string query2;
                    query2 = String.Format("SELECT  Cena FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    Double cena = (Double)(cmd2).ExecuteScalar();
                    Double finalCost = cena * Convert.ToDouble(timeInMinute);

                    textBox2.Text = Convert.ToString(finalCost) + " р";
                    button1.Enabled = true;

                }
                else if (label10.Text == "Длительность в часах")
                {
                    Int32 timeInMinute;
                    if (textBox1.Text == "")
                    {
                        timeInMinute = 0;
                        textBox2.Text = "";
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) > 24)
                    {
                        label12.Visible = true;
                        label12.Text = "Для данного тарифа длительность не может быть более 24 часов";
                        label12.ForeColor = Color.Red;
                        label12.Location = new Point(227, 600);
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) < 24)
                    {
                        label12.Visible = false;
                        label12.Text = "";
                        label12.Location = new Point(227, 600);


                    }
                    timeInMinute = Convert.ToInt32(textBox1.Text);

                    con = new SqlConnection(connectionString);
                    con.Open();
                    string query;
                    query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                    string query2;
                    query2 = String.Format("SELECT  Cena FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    Double cena = (Double)(cmd2).ExecuteScalar();
                    Double finalCost = cena * Convert.ToDouble(timeInMinute * 60);

                    textBox2.Text = Convert.ToString(finalCost) + " р";
                    button1.Enabled = true;
                }
                else if (label10.Text == "Длительность в днях")
                {
                    Int32 timeInMinute;
                    if (textBox1.Text == "")
                    {
                        timeInMinute = 0;
                        textBox2.Text = "";
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) > 30)
                    {
                        label12.Visible = true;
                        label12.Text = "Для данного тарифа длительность не может быть более 30 дней";
                        label12.ForeColor = Color.Red;
                        label12.Location = new Point(227, 600);
                        button1.Enabled = false;
                        return;
                    }
                    if (Convert.ToInt32(textBox1.Text) < 30)
                    {
                        label12.Visible = false;
                        label12.Text = "";
                        label12.Location = new Point(227, 600);



                    }
                    timeInMinute = Convert.ToInt32(textBox1.Text);

                    con = new SqlConnection(connectionString);
                    con.Open();
                    string query;
                    query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                    string query2;
                    query2 = String.Format("SELECT  Cena FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    Double cena = (Double)(cmd2).ExecuteScalar();
                    Double finalCost = cena * Convert.ToDouble(timeInMinute * 1440);

                    textBox2.Text = Convert.ToString(finalCost) + " р";
                    button1.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                dateNow = dateTimePicker1.Value;
                if (label10.Text == "Длительность в днях")
                {
                    dlitelnost = Convert.ToInt64(textBox1.Text) * 1440;
                    var ts = TimeSpan.FromDays(Convert.ToDouble(textBox1.Text));
                    dateEnd = dateNow + ts;

                }
                if (label10.Text == "Длительность в часах")
                {
                    dlitelnost = Convert.ToInt64(textBox1.Text) * 60;
                    var ts = TimeSpan.FromHours(Convert.ToDouble(textBox1.Text));
                    dateEnd = dateNow + ts;

                }
                if (label10.Text == "Длительность в минутах")
                {
                    dlitelnost = Convert.ToInt64(textBox1.Text);
                    var ts = TimeSpan.FromMinutes(Convert.ToDouble(textBox1.Text));
                    dateEnd = dateNow + ts;

                }
                if (comboBox1.Text == "")
                {
                    label12.Visible = true;
                    label12.Text = "Класс автомобиля не может быть пустым";
                    label12.ForeColor = Color.Red;
                    label12.Location = new Point(350, 600);
                    return;

                }
                if (comboBox2.Text == "")
                {
                    label12.Visible = true;
                    label12.Text = "Тип класса автомобиля не может быть пустым";
                    label12.ForeColor = Color.Red;
                    label12.Location = new Point(350, 600);
                    return;

                }
                if (comboBox3.Text == "")
                {
                    label12.Visible = true;
                    label12.Text = "Пожалуйста, выберите тариф из предложенных";
                    label12.ForeColor = Color.Red;
                    label12.Location = new Point(350, 600);
                    return;

                }
                if (dataGridView1.CurrentRow == null)
                {
                    label12.Visible = true;
                    label12.Text = "Пожалуйста выберите автомобиль из списка";
                    label12.ForeColor = Color.Red;
                    label12.Location = new Point(350, 600);
                    return;

                }
                String insertValueNameOfKlass = comboBox1.Text;
                String insertValueTypeOfKlass = comboBox2.Text;
                String insertValueNameOFTarif = comboBox3.Text;

                con = new SqlConnection(connectionString);
                con.Open();
                string query;
                query = String.Format("SELECT  idKlassa FROM KlassAvto Where Klass = '" + insertValueNameOfKlass + " ' AND Tip = '" + insertValueTypeOfKlass + " ' ", con);
                SqlCommand cmd1 = new SqlCommand(query, con);
                Int32 idKlass = (Int32)(cmd1).ExecuteScalar();

                string query2;
                query2 = String.Format("SELECT  idTarifa FROM Tarif Where Nazvanie = '" + insertValueNameOFTarif + " ' AND idKlassa = '" + idKlass + " ' ", con);
                SqlCommand cmd2 = new SqlCommand(query2, con);
                Int32 idTarifa = (Int32)(cmd2).ExecuteScalar();

                String insertValueDateOfStart = dateNow.ToString();
                String insertValueDateOFEnd = dateEnd.ToString();
                String insertValueDlitelnos = dlitelnost.ToString();
                string str = textBox2.Text;
                string find = "р";
                if (str.IndexOf(find) != 0)
                    str = str.Remove(str.IndexOf(find), 1);
                string str1 = str;
                string find1 = " ";
                if (str1.IndexOf(find1) != 0)
                    str1 = str1.Remove(str1.IndexOf(find1), 1);
                String insertValueCost = str1;
                String insertValueIdAvto = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                String insertValueIdTarif = idTarifa.ToString();
                String insertValueIdUser = Program.idUser.ToString();
                Program.idAvto = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                string sqlInsertNewTrip = string.Format("INSERT INTO Poezdka (TimeOfStart, TimeOfEnd, Dlitelnost, Stoim, idAvto, idTarifa, idUser) " +
                         " VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6})", insertValueDateOfStart, insertValueDateOFEnd, insertValueDlitelnos, insertValueCost, insertValueIdAvto, insertValueIdTarif, insertValueIdUser);
                SqlCommand insNewTrip = new SqlCommand(sqlInsertNewTrip, con);
                insNewTrip.ExecuteNonQuery();

                string query3;
                query3 = String.Format("SELECT  idPoezdki FROM Poezdka Where TimeOfStart = '" + insertValueDateOfStart + " ' AND TimeOfEnd = '" + insertValueDateOFEnd + " ' AND Dlitelnost = '" + insertValueDlitelnos + " ' AND Stoim = '" + insertValueCost + " ' AND idAvto = ' " + insertValueIdAvto + " ' AND idTarifa = ' " + insertValueIdTarif + " ' AND idUser = ' " + insertValueIdUser + " ' ", con);
                SqlCommand cmd3 = new SqlCommand(query3, con);
                Int32 idTrip = (Int32)(cmd3).ExecuteScalar();
                Program.idTrip = idTrip;
                this.Close();
                f16 = new Form16();
                f16.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox1.CheckState == CheckState.Checked)
            {
                timer1.Start();
            }
            else if(checkBox1.CheckState == CheckState.Unchecked)
            {
                timer1.Stop();
                dateTimePicker1.Value = DateTime.Now;
            }
        }
    }
}
