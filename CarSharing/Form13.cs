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
    public partial class Form13 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool updateKlass;
        bool insertKlass;
        bool deleteKlass;
        Logger logger;
        CurrentMethod cm;

        public Form13()
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
            GetData("Select * From KlassAvto");
            dataGridView1.DataSource = bindingSource1;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Visible = false;

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
            insertKlass = true;
            updateKlass = false;
            deleteKlass = false;
            button2.Visible = true;
            button2.Text = "Добавить";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            updateKlass = true;
            insertKlass = false;
            deleteKlass = false;
            button2.Visible = true;
            button2.Text = "Изменить";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            deleteKlass = true;
            insertKlass = false;
            updateKlass = false;
            button2.Visible = true;
            button2.Text = "Удалить";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (insertKlass == true)
                {
                    updateKlass = false;
                    deleteKlass = false;

                    String insertValueNameOfKlass = textBox1.Text;
                    String insertValueTypeOfKlass = textBox2.Text;

                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        MessageBox.Show("Название тарифа должно быть больше 5 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBox2.Text.Length < 5)
                    {
                        MessageBox.Show("Тип тарифа должно быть больше 5 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string sqlInsertNewKlass = string.Format("INSERT INTO KlassAvto (Klass, Tip) " +
                        " VALUES ('{0}', '{1}')", insertValueNameOfKlass, insertValueTypeOfKlass);
                    SqlCommand insNewKlass = new SqlCommand(sqlInsertNewKlass, con);
                    insNewKlass.ExecuteNonQuery();
                    con.Close();
                    GetData("Select * From KlassAvto");
                    insertKlass = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    button2.Visible = false;
                }
                else if (updateKlass == true)
                {
                    insertKlass = false;
                    deleteKlass = false;
                    String insertValueNameOfKlass = textBox1.Text;
                    String insertValueTypeOfKlass = textBox2.Text;
                    String insertValueIdKlass = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        MessageBox.Show("Название тарифа должно быть больше 5 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBox2.Text.Length < 5)
                    {
                        MessageBox.Show("Тип тарифа должно быть больше 5 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string sqlUpdateKlass = string.Format("UPDATE KlassAvto SET Klass = '{0}' , Tip = '{1}'  WHERE idKlassa = {2}",
                                insertValueNameOfKlass, insertValueTypeOfKlass, insertValueIdKlass);
                    SqlCommand updKlass = new SqlCommand(sqlUpdateKlass, con);
                    updKlass.ExecuteNonQuery();
                    con.Close();
                    GetData("Select * From KlassAvto");
                    updateKlass = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    button2.Visible = false;

                }
                else if (deleteKlass == true)
                {
                    insertKlass = false;
                    updateKlass = false;
                    con = new SqlConnection(connectionString);
                    con.Open();
                    String insertValueIdKlass = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    string message = "Вы действительно хотите удалить данный Класс?";
                    string caption = "Удаление класса";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string sqlDelKlass = string.Format("DELETE FROM KlassAvto WHERE idKlassa = {0}", insertValueIdKlass);
                        SqlCommand delKlass = new SqlCommand(sqlDelKlass, con);
                        delKlass.ExecuteNonQuery();
                        GetData("select * from KlassAvto");

                        con.Close();

                        deleteKlass = false;
                        button2.Visible = false;
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
    }
}
