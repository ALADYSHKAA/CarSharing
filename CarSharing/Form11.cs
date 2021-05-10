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
    public partial class Form11 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        public Form11()
        {
            InitializeComponent();

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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

        private void Form11_Load(object sender, EventArgs e)
        {
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
            GetData("select * from Povrezdeniya Where idAvto = '" + Program.getIdAvto + " ' ");
            dataGridView1.DataSource = bindingSource1;

            con = new SqlConnection(connectionString);
            con.Open();

            string markaAvtoSelect = "SELECT Marka FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand markaAvto = new SqlCommand(markaAvtoSelect, con);
            String markaAvtoString = (String)(markaAvto).ExecuteScalar();
            label3.Text = markaAvtoString;

            string modelAvtoSelect = "SELECT Nazvanie FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand modelAvto = new SqlCommand(modelAvtoSelect, con);
            String modelAvtoString = (String)(modelAvto).ExecuteScalar();
            label4.Text = modelAvtoString;

            string gosNomerAvtoSelect = "SELECT GosNomer FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand gosNomerAvto = new SqlCommand(gosNomerAvtoSelect, con);
            String gosNomerAvtoString = (String)(gosNomerAvto).ExecuteScalar();
            label6.Text = gosNomerAvtoString;

            string godVipyskaAvtoSelect = "SELECT GodVipyska FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand godVipyskaAvto = new SqlCommand(godVipyskaAvtoSelect, con);
            Int32 godVipyskaAvtoString = (Int32)(godVipyskaAvto).ExecuteScalar();
            label8.Text = godVipyskaAvtoString.ToString();


            string vinAvtoSelect = "SELECT VIN FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand vinAvto = new SqlCommand(vinAvtoSelect, con);
            String vinAvtoString = (String)(vinAvto).ExecuteScalar();
            label10.Text = vinAvtoString;

            string probegAvtoSelect = "SELECT Probeg FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand probegAvto = new SqlCommand(probegAvtoSelect, con);
            Double probegAvtoDouble = (Double)(probegAvto).ExecuteScalar();
            label12.Text = probegAvtoDouble.ToString(); ;

            string idKlassAvtoSelect = "SELECT idKlassa FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand idKlassAvto = new SqlCommand(idKlassAvtoSelect, con);
            Int32 idKlassAvtoInt = (Int32)(idKlassAvto).ExecuteScalar();
            string nameOfKlassAvtoSelect = "SELECT Klass FROM KlassAvto Where idKlassa = '" + idKlassAvtoInt + " '";
            SqlCommand nameOfKlassAvto = new SqlCommand(nameOfKlassAvtoSelect, con);
            String nameOfKlassAvtoString = (String)(nameOfKlassAvto).ExecuteScalar();
            label14.Text = nameOfKlassAvtoString;


            string typeOfKlassAvtoSelect = "SELECT Tip FROM KlassAvto Where idKlassa = '" + idKlassAvtoInt + " '";
            SqlCommand typeOfKlassAvto = new SqlCommand(typeOfKlassAvtoSelect, con);
            String typeOfKlassAvtoString = (String)(typeOfKlassAvto).ExecuteScalar();
            label16.Text = typeOfKlassAvtoString;

            string statusAvtoSelect = "SELECT StatusDostypa FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand statusAvto = new SqlCommand(statusAvtoSelect, con);
            Boolean statusAvtoString = (Boolean)(statusAvto).ExecuteScalar();
            var status = statusAvtoString.ToString();
            label18.Text = status;

            if (label18.Text == "False")
            {
                label18.Text = "Не исправен";

            }
            else if (label18.Text == "True")
            {
                label18.Text = "Исправен";
            }
            string idPolicAvtoSelect = "SELECT KodPolica FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
            SqlCommand idPolicaAvto = new SqlCommand(idPolicAvtoSelect, con);
            Int32 idPolicaAvtoInt = (Int32)(idPolicaAvto).ExecuteScalar();

            string typeOfPolicAvtoSelect = "SELECT TipPolica FROM Strahovka Where KodPolica = '" + idPolicaAvtoInt + " '";
            SqlCommand typeOfPolicAvto = new SqlCommand(typeOfPolicAvtoSelect, con);
            String typeOfPolicAvtoString = (String)(typeOfPolicAvto).ExecuteScalar();
            label20.Text = typeOfPolicAvtoString;

            string numberOfPolicAvtoSelect = "SELECT NomerPolica FROM Strahovka Where KodPolica = '" + idPolicaAvtoInt + " '";
            SqlCommand numberOfPolicAvto = new SqlCommand(numberOfPolicAvtoSelect, con);
            String numberOfPolicAvtoString = (String)(numberOfPolicAvto).ExecuteScalar();
            label22.Text = numberOfPolicAvtoString;

            string countTripsSelect = "Select Count (*) From Poezdka WHERE idAvto = '" + Program.getIdAvto + " '";
            SqlCommand countTrips = new SqlCommand(countTripsSelect, con);
            Int32 countTripsString = (Int32)(countTrips).ExecuteScalar();
            label24.Text = countTripsString.ToString();

            string countProisAvtoSelect = "Select Count (idProischestviya) From Poezdka WHERE idAvto = '" + Program.getIdAvto + " '";
            SqlCommand countProisTripsAvto = new SqlCommand(countProisAvtoSelect, con);
            Int32 countProisAvtoString = (Int32)(countProisTripsAvto).ExecuteScalar();
            label26.Text = countProisAvtoString.ToString();

            string sumAvtoSelect = "Select SUM (Stoim) From Poezdka WHERE idAvto = '" + Program.getIdAvto + " '";
            SqlCommand sumTripsAvto = new SqlCommand(sumAvtoSelect, con);
            try
            {
                Int32 sumAvtoString = (Int32)(sumTripsAvto).ExecuteScalar();
                label28.Text = sumAvtoString.ToString();
            }
            catch (Exception)
            {
                label28.Text = "0";
            }
            string countPovrAvtoSelect = "Select Count (*) From Povrezdeniya  WHERE idAvto = '" + Program.getIdAvto + " '";
            SqlCommand countPovrAvto = new SqlCommand(countPovrAvtoSelect, con);
            Int32 countPovrAvtoString = (Int32)(countPovrAvto).ExecuteScalar();
            label31.Text = countPovrAvtoString.ToString();
            if (label31.Text != "0")
            {
                label31.ForeColor = Color.Red;
            }

            if(label18.Text == "Не исправен")
            {
                button2.Text = "Открыть для использования";
            }

            if (label18.Text == "Исправен")
            {
                button2.Text = "Закрыть для использования";
            }
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From Povrezdeniya WHERE idAvto = '" + Program.getIdAvto + " '", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "0")
            {
                label30.Text = "Данные о повреждениях автомобиля: Отсутсвуют";


            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 3)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Отремонтировано" : "Не отремонтировано";
                    e.FormattingApplied = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            bool status;
            if(button2.Text == "Открыть для использования")
            {
                status = true;
                string sqlUpdateAvto = string.Format("UPDATE Avto SET StatusDostypa = '{0}'  WHERE idAvto = {1}",
                            status, Program.getIdAvto);
                SqlCommand updAvto = new SqlCommand(sqlUpdateAvto, con);
                updAvto.ExecuteNonQuery();
                Form11_Load(sender, e);
            }
            else if(button2.Text == "Закрыть для использования" )
            {
                status = false;
                string sqlUpdateAvto = string.Format("UPDATE Avto SET StatusDostypa = '{0}'  WHERE idAvto = {1}",
                            status, Program.getIdAvto);
                SqlCommand updAvto = new SqlCommand(sqlUpdateAvto, con);
                updAvto.ExecuteNonQuery();
                Form11_Load(sender, e);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            String insertValueTipPolisa = comboBox1.Text;
            String insertValueNumberOfPolic = textBox1.Text;
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Тип полиса не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (textBox1.Text.Length != 10)
            {
                MessageBox.Show("Номер полиса должен состоять из 10 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idPolicAvtoSelect = "SELECT KodPolica FROM Avto Where idAvto = '" + Program.getIdAvto + " '";
                SqlCommand idPolicaAvto = new SqlCommand(idPolicAvtoSelect, con);
                Int32 idPolicaAvtoInt = (Int32)(idPolicaAvto).ExecuteScalar();

                string sqlUpdatePolic = string.Format("UPDATE Strahovka SET TipPolica = '{0}' , NomerPolica = '{1}' WHERE KodPolica ={2}",
                            insertValueTipPolisa, insertValueNumberOfPolic, idPolicaAvtoInt);
                SqlCommand updPolis = new SqlCommand(sqlUpdatePolic, con);
                updPolis.ExecuteNonQuery();
            
            con.Close();
            Form11_Load(sender, e);
        }
    }
}
