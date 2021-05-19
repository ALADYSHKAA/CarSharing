using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSharing
{
    public partial class Form16 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Logger logger;
        CurrentMethod cm;
        public Form16()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            richTextBox1.Enabled = false;
            button1.Enabled = false;
            richTextBox3.Enabled = false;
            textBox2.Enabled = false;
            checkBox3.Enabled = false;
            textBox3.Enabled = false;
            richTextBox5.Enabled = false;
            button4.Enabled = false;
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();

                string fioUserSelect = "SELECT Fio FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
                SqlCommand fioUser = new SqlCommand(fioUserSelect, con);
                String fioUserString = (String)(fioUser).ExecuteScalar();
                label1.Text = "Спасибо за поездку " + fioUserString + "!";
            }
            catch(Exception ex)
            {
                
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox1.CheckState == CheckState.Checked)
            {
                richTextBox1.Enabled = true;
                button1.Enabled = true;

            }
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                richTextBox1.Enabled = false;
                button1.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (richTextBox1.Text.Length > 2)
                {
                    String insertValueOtzyv = richTextBox1.Text;
                    con = new SqlConnection(connectionString);
                    con.Open();
                    string sqlUpdateTrip = string.Format("UPDATE Poezdka SET Otzyv = '{0}'  WHERE idPoezdki = {1}",
                                     insertValueOtzyv, Program.idTrip);
                    SqlCommand updTrip = new SqlCommand(sqlUpdateTrip, con);
                    updTrip.ExecuteNonQuery();
                    con.Close();
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox1.Enabled = false;
                    MessageBox.Show("Спасибо за Ваш отзыв, мы обязательно прислушаемся к Вашему мнению!", "Отзыв", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (richTextBox1.Text.Length < 2)
                {
                    MessageBox.Show("Отзыв должен содержать более 2 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox2.CheckState == CheckState.Checked)
            {
                textBox2.Enabled = true;
                richTextBox3.Enabled = true;
                button3.Enabled = true;

            }
            if (checkBox2.CheckState == CheckState.Unchecked)
            {
                textBox2.Enabled = false;
                richTextBox3.Enabled = false;
                button3.Enabled = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try { 
            
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Краткое описание не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (richTextBox3.Text.Length == 0)
                {
                    MessageBox.Show("Полное описание не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                String insertValueKratOpic = textBox2.Text;
                String insertValuePolnoeOpicanie = richTextBox3.Text;
                String insertValueStatus = "False";
                con = new SqlConnection(connectionString);
                con.Open();
                string sqlInsertNewProis = string.Format("INSERT INTO Proishestviya (Opicanie, Deistviya, Status) " +
                        " VALUES ('{0}', '{1}', '{2}')", insertValueKratOpic, insertValuePolnoeOpicanie, insertValueStatus);
                SqlCommand insNewProis = new SqlCommand(sqlInsertNewProis, con);
                insNewProis.ExecuteNonQuery();

                string query3;
                query3 = String.Format("SELECT  idProischestviya FROM Proishestviya Where Opicanie = '" + insertValueKratOpic + " ' AND Deistviya = '" + insertValuePolnoeOpicanie + " ' AND Status = '" + insertValueStatus + " ' ", con);
                SqlCommand cmd3 = new SqlCommand(query3, con);
                Int32 idProis = (Int32)(cmd3).ExecuteScalar();

                string sqlUpdateTrip = string.Format("UPDATE Poezdka SET idProischestviya = '{0}'  WHERE idPoezdki = {1}",
                                   idProis, Program.idTrip);
                SqlCommand updTrip = new SqlCommand(sqlUpdateTrip, con);
                updTrip.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Спасибо за подробное описание происшествия, теперь, пожалуйста, опишите повреждения автомобиля, при их наличии", "Происшествие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Enabled = false;
                richTextBox3.Enabled = false;
                button3.Enabled = false;
                textBox2.Text = "";
                richTextBox3.Text = "";
                checkBox2.Enabled = false;
                checkBox3.Enabled = true;
            }
            catch(Exception ex)
            {
                
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox3.CheckState == CheckState.Checked)
            {
                textBox3.Enabled = true;
                richTextBox5.Enabled = true;
                button4.Enabled = true;

            }
            if (checkBox3.CheckState == CheckState.Unchecked)
            {
                textBox3.Enabled = false;
                richTextBox5.Enabled = false;
                button4.Enabled = false;

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (textBox3.Text.Length == 0)
                {
                    MessageBox.Show("Краткое описание не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (richTextBox5.Text.Length == 0)
                {
                    MessageBox.Show("Полное описание не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                String insertValueKratOpic = textBox3.Text;
                String insertValuePolnoeOpicanie = richTextBox5.Text;
                String insertValueStatus = "False";
                String insertValueIdAvto = Convert.ToString(Program.idAvto);

                con = new SqlConnection(connectionString);
                con.Open();
                string sqlInsertNewPovr = string.Format("INSERT INTO Povrezdeniya (Tip, Opicanie, Status, idAvto) " +
                        " VALUES ('{0}', '{1}', '{2}', {3})", insertValueKratOpic, insertValuePolnoeOpicanie, insertValueStatus, insertValueIdAvto);
                SqlCommand insNewPovr = new SqlCommand(sqlInsertNewPovr, con);
                insNewPovr.ExecuteNonQuery();
                MessageBox.Show("Спасибо за подробное описание повреждения, в ближайшее время с Вами свяжется Администратор", "Происшествие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox3.Enabled = false;
                richTextBox5.Enabled = false;
                button4.Enabled = false;
                textBox3.Text = "";
                richTextBox5.Text = "";
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            catch(Exception ex)
            {
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }
    }
}
