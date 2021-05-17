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
    public partial class Form21 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Logger logger;
        CurrentMethod cm;

        public Form21()
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
            GetData("SELECT * FROM ViewPovr ORDER BY GosNomer");
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
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
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            string query;
            query = string.Format("SELECT * FROM ViewPovr WHERE GosNomer LIKE '{0}%'", textBox1.Text);
            GetData(query);
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
                string statusPovrSelect = "SELECT Status FROM ViewPovr Where idPovrezdeniya = '" + insertValue + " '";
                SqlCommand statusPovr = new SqlCommand(statusPovrSelect, con);
                bool statusPovrBool = (bool)(statusPovr).ExecuteScalar();
                if (statusPovrBool == false)
                {
                    string sqlUpdatePovr = string.Format("UPDATE Povrezdeniya SET Status = '{0}'  WHERE idPovrezdeniya = {1}",
                                     status, insertValue);
                    SqlCommand updPovr = new SqlCommand(sqlUpdatePovr, con);
                    updPovr.ExecuteNonQuery();
                    GetData("SELECT * FROM ViewPovr ORDER BY GosNomer");
                }
                else if (statusPovrBool == true)
                {
                    MessageBox.Show("Данное повреждение было уже отремонтировано.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }
    }
}
