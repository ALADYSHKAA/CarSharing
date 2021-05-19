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
    public partial class Form18 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Form19 f19;
        Logger logger;
        CurrentMethod cm;

        public Form18()
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
            

            GetData("SELECT * FROM ViewTrips ORDER BY TimeOfStart");
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
            catch (Exception ex )
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
            if (e.ColumnIndex == 3)
            {
                if (Convert.ToDouble(e.Value) <= 60)
                {
                    var ts = TimeSpan.FromMinutes(Convert.ToDouble(e.Value));
                    e.Value = String.Format("{0} м. ", ts.Minutes);
                }
                else if (Convert.ToDouble(e.Value) <= 1440)
                {
                    var ts = TimeSpan.FromMinutes(Convert.ToDouble(e.Value));
                    e.Value = String.Format("{0} ч. {1} м. ", ts.Hours, ts.Minutes);
                }
                else if (Convert.ToDouble(e.Value) > 1440)
                {
                    var ts = TimeSpan.FromMinutes(Convert.ToDouble(e.Value));
                    e.Value = String.Format("{0} д. {1} ч. {2} м. ", ts.Days, ts.Hours, ts.Minutes);
                }

            }
            if (e.ColumnIndex == 4)
            {
                e.Value = e.Value + " р.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (button1.Text == "Показать")
                {
                    String insertValueDateOfStart = dateTimePicker1.Value.ToString();
                    String insertValueDateOfEnd = dateTimePicker2.Value.ToString();
                    GetData("SELECT * FROM ViewTrips WHERE TimeOfStart BETWEEN '" + insertValueDateOfStart + "'  AND '" + insertValueDateOfEnd + "'");
                    button1.Text = "Отмена";
                }
                else if (button1.Text == "Отмена")
                {
                    button1.Text = "Показать";

                    GetData("SELECT * FROM ViewTrips ORDER BY TimeOfStart");
                }


            }
            catch (Exception ex)
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

        private void button3_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);

            Program.getIdTrip = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            this.Size = new Size(0, 0);
            this.CenterToScreen();
            f19 = new Form19();
            f19.ShowDialog();
            this.Size = new Size(1233, 686);
            this.CenterToScreen();

        }
    }

}
