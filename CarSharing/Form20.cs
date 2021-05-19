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
    public partial class Form20 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Logger logger;
        CurrentMethod cm;

        public Form20()
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
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            GetData("SELECT * FROM ViewProis");
            dataGridView1.Columns[0].Visible = false;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value) == "Обработано")
            {
                button2.Enabled = false;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Обработано" : "Не обработано";
                    e.FormattingApplied = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try{
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (button3.Text == "Показать")
                {
                    String insertValueDateOfStart = dateTimePicker1.Value.ToString();
                    String insertValueDateOfEnd = dateTimePicker2.Value.ToString();
                    GetData("SELECT * FROM ViewProis WHERE TimeOfStart BETWEEN '" + insertValueDateOfStart + "'  AND '" + insertValueDateOfEnd + "'");
                    button3.Text = "Отмена";
                }
                else if (button3.Text == "Отмена")
                {
                    button3.Text = "Показать";

                    GetData("SELECT * FROM ViewProis ORDER BY TimeOfStart");
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                string insertValue = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                bool status = true;
                string statusProisSelect = "SELECT Status FROM Proishestviya Where idProischestviya = '" + insertValue + " '";
                SqlCommand statusProis = new SqlCommand(statusProisSelect, con);
                bool statusProisBool = (bool)(statusProis).ExecuteScalar();
                if (statusProisBool == false)
                {
                    string sqlUpdatePovr = string.Format("UPDATE Proishestviya SET Status = '{0}'  WHERE idProischestviya = {1}",
                                     status, insertValue);
                    SqlCommand updPovr = new SqlCommand(sqlUpdatePovr, con);
                    updPovr.ExecuteNonQuery();
                    GetData("SELECT * FROM ViewProis ORDER BY TimeOfStart");
                }
                else if (statusProisBool == true)
                {
                    status = false;
                    string sqlUpdatePovr = string.Format("UPDATE Proishestviya SET Status = '{0}'  WHERE idProischestviya = {1}",
                                    status, insertValue);
                    SqlCommand updPovr = new SqlCommand(sqlUpdatePovr, con);
                    updPovr.ExecuteNonQuery();
                    GetData("SELECT * FROM ViewProis ORDER BY TimeOfStart");
                }
                con.Close();
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value) == "Обработано")
            {
                button2.Enabled = false;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value) == "Обработано")
            {
                button2.Enabled = false;
            }
        }
    }


}
