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
    public partial class Form14 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool updateNews;
        bool insertNews;
        bool deleteNews;
        String fileName;
        Logger logger;
        CurrentMethod cm;

        public Form14()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            GetData("SELECT * FROM News ORDER BY idNews DESC");
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

            textBox1.Enabled = false;
            richTextBox1.Enabled = false;
            pictureBox1.Enabled = false;
            label5.Visible = false;
            button2.Visible = true;

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertNews = true;
            updateNews = false;
            deleteNews = false;
            textBox1.Enabled = true;
            richTextBox1.Enabled = true;
            pictureBox1.Enabled = true;
            label5.Text = "В данный момент вы находитесь в режиме добавления новой новости";
            label5.ForeColor = Color.Green;
            pictureBox1.Image = null;
            textBox1.Text = "";
            richTextBox1.Text = "";
            label5.Visible = true;
            button2.Visible = true;
            button2.Text = "Добавить";
            fileName = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertNews = false;
            updateNews = true;
            deleteNews = false;
            textBox1.Enabled = true;
            richTextBox1.Enabled = true;
            pictureBox1.Enabled = true;
            label5.Text = "В данный момент вы находитесь в режиме редактирования новости";
            label5.ForeColor = Color.Blue;
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            richTextBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            String imagePath = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            pictureBox1.Image = Image.FromFile(imagePath);
            label5.Visible = true;
            button2.Visible = true;
            button2.Text = "Изменить";
            fileName = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            insertNews = false;
            updateNews = false;
            deleteNews = true;
            label5.Text = "В данный момент вы находитесь в режиме удаления новости";
            label5.ForeColor = Color.Red;
            pictureBox1.Image = null;
            textBox1.Text = "";
            richTextBox1.Text = "";
            label5.Visible = true;
            button2.Text = "Удалить";
            textBox1.Enabled = false;
            richTextBox1.Enabled = false;
            pictureBox1.Enabled = false;
            fileName = null;
            button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (insertNews == true)
                {
                    updateNews = false;
                    deleteNews = false;

                    String insertValueKratkNews = textBox1.Text;
                    String insertValueFullNews = richTextBox1.Text;
                    String insertValueFileName = fileName;
                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        label5.Text = "Краткое описание новости не может быть короче 5 символов";
                        label5.ForeColor = Color.Orange;
                        return;
                    }

                    if (richTextBox1.Text.Length < 20)
                    {
                        label5.Text = "Полное описание новости не может быть короче 20 символов";
                        label5.ForeColor = Color.Orange;
                        return;
                    }
                    if (fileName == null)
                    {
                        label5.Text = "Фотография не выбрана";
                        label5.ForeColor = Color.Orange;
                        label5.Location = new Point(480, 733);
                        return;
                    }
                    string message = "Вы действительно хотите добавить данную новость, её увидят все пользователи.";
                    string caption = "Добавление новости";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string sqlInsertNewNews = string.Format("INSERT INTO News (KratkoeOpicanie, PolnoeOpicanie, Izobrazenie) " +
                         " VALUES ('{0}', '{1}', '{2}')", insertValueKratkNews, insertValueFullNews, insertValueFileName);
                        SqlCommand insNewNews = new SqlCommand(sqlInsertNewNews, con);
                        insNewNews.ExecuteNonQuery();
                        GetData("SELECT * FROM News ORDER BY idNews DESC");
                        con.Close();
                        button2.Visible = false;
                        textBox1.Text = "";
                        richTextBox1.Text = "";
                        pictureBox1.Image = null;
                        textBox1.Enabled = false;
                        richTextBox1.Enabled = false;
                        pictureBox1.Enabled = false;
                        label5.Visible = false;
                        label5.Location = new Point(338, 733);
                        fileName = null;
                        insertNews = false;

                    }

                }
                if (updateNews == true)
                {
                    insertNews = false;
                    deleteNews = false;
                    String insertValueKratkNews = textBox1.Text;
                    String insertValueFullNews = richTextBox1.Text;
                    String insertValueFileName = fileName;
                    String insertValueIdNews = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    con = new SqlConnection(connectionString);
                    con.Open();
                    if (textBox1.Text.Length < 5)
                    {
                        label5.Text = "Краткое описание новости не может быть короче 5 символов";
                        label5.ForeColor = Color.Orange;
                        return;
                    }

                    if (richTextBox1.Text.Length < 20)
                    {
                        label5.Text = "Полное описание новости не может быть короче 20 символов";
                        label5.ForeColor = Color.Orange;
                        return;
                    }
                    if (fileName == null)
                    {
                        label5.Text = "Фотография не выбрана";
                        label5.ForeColor = Color.Orange;
                        label5.Location = new Point(480, 733);
                        return;
                    }
                    string message = "Вы действительно хотите изменить данную новость, её увидят все пользователи.";
                    string caption = "Изменение новости";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string sqlUpdateNews = string.Format("UPDATE News SET KratkoeOpicanie = '{0}' , PolnoeOpicanie = '{1}' , Izobrazenie = '{2}'  WHERE idNews = {3}",
                                 insertValueKratkNews, insertValueFullNews, insertValueFileName, insertValueIdNews);
                        SqlCommand updNews = new SqlCommand(sqlUpdateNews, con);
                        updNews.ExecuteNonQuery();
                        con.Close();
                        GetData("SELECT * FROM News ORDER BY idNews DESC");
                        button2.Visible = false;
                        textBox1.Text = "";
                        richTextBox1.Text = "";
                        pictureBox1.Image = null;
                        textBox1.Enabled = false;
                        richTextBox1.Enabled = false;
                        pictureBox1.Enabled = false;
                        label5.Visible = false;
                        label5.Location = new Point(338, 733);
                        fileName = null;
                        updateNews = false;

                    }

                }
                if (deleteNews == true)
                {
                    insertNews = false;
                    updateNews = false;
                    String insertValueIdNews = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                    con = new SqlConnection(connectionString);
                    con.Open();
                    string message = "Вы действительно хотите удалить данную новость";
                    string caption = "Удаление новости";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string sqlDelNews = string.Format("DELETE FROM News WHERE idNews = {0}", insertValueIdNews);
                        SqlCommand delNews = new SqlCommand(sqlDelNews, con);
                        delNews.ExecuteNonQuery();
                        GetData("SELECT * FROM News ORDER BY idNews DESC");

                        con.Close();

                        button2.Visible = false;
                        textBox1.Text = "";
                        richTextBox1.Text = "";
                        pictureBox1.Image = null;
                        textBox1.Enabled = false;
                        richTextBox1.Enabled = false;
                        pictureBox1.Enabled = false;
                        label5.Visible = false;
                        label5.Location = new Point(338, 733);
                        fileName = null;
                        deleteNews = false;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                var ofd = new OpenFileDialog();
                ofd.Filter = "Image Files(*.BMP; *.PNG; *.JPG; *.GIF)| *.BMP; *.PNG; *.JPG; *.GIF";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                fileName = ofd.FileName;
                if (ofd.FileName == "")
                {
                    pictureBox1.Image = null;
                    fileName = null;
                    return;
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
