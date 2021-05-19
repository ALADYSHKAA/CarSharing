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
    public partial class Form7 : Form
    {
        Logger logger;
        CurrentMethod cm;
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Form8 f8;
        public Form7()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            // LoadData();

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
            GetData("select * from Polzovatel");
            dataGridView1.DataSource = bindingSource1;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            textBox1.AutoSize = false;
            textBox1.Height = 27;

            ////for (int i = 0; i < dataGridView1.RowCount; i++)
            ////{
            ////    for (int j = 0; j < dataGridView1.ColumnCount; j++)
            ////    {
            ////        if (Convert.ToString(dataGridView1.Rows[i].Cells[6].Value) == "False")
            ////        {

            ////            dataGridView1.Rows[i].Cells[6].Value = "Не подтвержден";
            ////        }
            ////        if (Convert.ToString(dataGridView1.Rows[i].Cells[6].Value) == "True")
            ////        {
            ////            dataGridView1.Rows[i].Cells[6].Value = "Подтвержден";
            ////        }
            ////    }
            ////}
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

            private void Form7_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = bindingSource1;
            //GetData("select * from Polzovatel");
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
            if (e.ColumnIndex == 6)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Подтвержден" : "Не подтвержден";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (comboBox1.Text == "Фио")
            {
                string query;
                query = string.Format("SELECT * FROM Polzovatel WHERE Fio LIKE '{0}%'", textBox1.Text);
                GetData(query);
            }
            else if(comboBox1.Text == "Номеру паспорта")
            {
                string query;
                query = string.Format("SELECT * FROM Polzovatel WHERE NomerPassporta LIKE '{0}%'", textBox1.Text);
                GetData(query);
            }
            else if (comboBox1.Text == "Номеру ВУ")
            {
                string query;
                query = string.Format("SELECT * FROM Polzovatel WHERE NomerVY LIKE '{0}%'", textBox1.Text);
                GetData(query);
            }
            //else if (comboBox1.Text == "Авторы" || comboBox1.Text == "Наименование")
            //{
            //    selKnigi = comboboxValue + " LIKE '" + textBox1.Text + "%'";
            //    view5BindingSource.Filter = selKnigi;
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox1.CheckState == CheckState.Checked)
            {
                GetData("Select * from Polzovatel where StatusPodtverzdeniya = 0 ");
            }
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                GetData("Select * from Polzovatel ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Program.getIdUser = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
           
            this.Size = new Size(0, 0);
            this.CenterToScreen();
            f8 = new Form8();
            f8.ShowDialog();
            this.Size = new Size(1257, 812);
            this.CenterToScreen();
            GetData("Select * from Polzovatel ");
        }
    }
}






