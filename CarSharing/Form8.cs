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
    public partial class Form8 : Form
    {
        Logger logger;
        CurrentMethod cm;
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Form9 f9;


        public Form8()
        {
            InitializeComponent();

            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();

                string vyUserSelect = "SELECT FotoOfFace FROM Photos Where idUser ='" + Program.getIdUser + " '";
                SqlCommand vyUser = new SqlCommand(vyUserSelect, con);
                String vyUserString = (String)(vyUser).ExecuteScalar();
                pictureBox1.Image = Image.FromFile(vyUserString);

                string fioUserSelect = "SELECT Fio FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand fioUser = new SqlCommand(fioUserSelect, con);
                String fioUserString = (String)(fioUser).ExecuteScalar();
                label3.Text = fioUserString;

                string numberOfPassportUserSelect = "SELECT NomerPassporta FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand numberOfPassportUser = new SqlCommand(numberOfPassportUserSelect, con);
                String numberOfPassportUserString = (String)(numberOfPassportUser).ExecuteScalar();
                label4.Text = numberOfPassportUserString;

                string propiskaUserSelect = "SELECT Propiska FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand propiskaUser = new SqlCommand(propiskaUserSelect, con);
                String propiskaUserString = (String)(propiskaUser).ExecuteScalar();
                label6.Text = propiskaUserString;

                string dayOfBirthdayUserSelect = "SELECT DataRozdeniya FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand dayOfBirthdayUser = new SqlCommand(dayOfBirthdayUserSelect, con);
                DateTime dayOfBirthdayUserString = (DateTime)(dayOfBirthdayUser).ExecuteScalar();
                var date = dayOfBirthdayUserString.ToShortDateString();
                label8.Text = Convert.ToString(date);

                string numberOfDriverLicenseUserSelect = "SELECT NomerVY FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand numberOfDriverLicenseUser = new SqlCommand(numberOfDriverLicenseUserSelect, con);
                String numberOfDriverLicenseUserString = (String)(numberOfDriverLicenseUser).ExecuteScalar();
                label10.Text = numberOfDriverLicenseUserString;

                string statusUserSelect = "SELECT StatusPodtverzdeniya FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand statusUser = new SqlCommand(statusUserSelect, con);
                Boolean statusUserString = (Boolean)(statusUser).ExecuteScalar();
                var status = statusUserString.ToString();
                label12.Text = status;

                if (label12.Text == "False")
                {
                    label12.Text = "Не подтвержден";
                    label18.Text = "-";
                }
                else if (label12.Text == "True")
                {
                    label12.Text = "Подтвержден";
                }

                string emailUserSelect = "SELECT Email FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand emailUser = new SqlCommand(emailUserSelect, con);
                String emailUserString = (String)(emailUser).ExecuteScalar();
                label14.Text = emailUserString;

                string mobilePhoneUserSelect = "SELECT MobilePhone FROM Polzovatel Where IdUser = '" + Program.getIdUser + " '";
                SqlCommand mobilePhoneUser = new SqlCommand(mobilePhoneUserSelect, con);
                String mobilePhoneUserString = (String)(mobilePhoneUser).ExecuteScalar();
                label16.Text = mobilePhoneUserString;

                string countTripsUserSelect = "Select Count (*) From Poezdka WHERE idUser = '" + Program.getIdUser + " '";
                SqlCommand countTripsUser = new SqlCommand(countTripsUserSelect, con);
                Int32 countTripsUserString = (Int32)(countTripsUser).ExecuteScalar();
                label18.Text = countTripsUserString.ToString();
                if (label12.Text == "Не подтвержден")
                {
                    label18.Text = "-";
                }
                try
                {
                    string sumOfTripsUserSelect = "Select SUM(Stoim) From Poezdka WHERE idUser = '" + Program.getIdUser + " '";
                    SqlCommand sumOfTripsUser = new SqlCommand(sumOfTripsUserSelect, con);
                    Int32 sumOfTripsUserString = (Int32)(sumOfTripsUser).ExecuteScalar();
                    label20.Text = sumOfTripsUserString.ToString() + "р.";
                }
                catch (Exception)
                {
                    if (label12.Text == "Не подтвержден")
                    {
                        label20.Text = "-";
                    }
                    else if (label12.Text == "Подтвержден")
                    {
                        label20.Text = "0";
                    }
                }

                string countProisUserSelect = "Select Count (idProischestviya) From Poezdka WHERE idUser = '" + Program.getIdUser + " '";
                SqlCommand countProisTripsUser = new SqlCommand(countProisUserSelect, con);
                Int32 countProisUserString = (Int32)(countProisTripsUser).ExecuteScalar();
                label22.Text = countProisUserString.ToString();
                if (label22.Text != "0")
                {
                    label22.ForeColor = Color.Red;
                }
                if (label12.Text == "Не подтвержден")
                {
                    label22.Text = "-";
                }

                try
                {
                    string sumOfTimeUserSelect = "Select SUM(Dlitelnost) From Poezdka WHERE idUser = '" + Program.getIdUser + " '";
                    SqlCommand sumOfTimeUser = new SqlCommand(sumOfTimeUserSelect, con);
                    Int32 sumOfTimeUserString = (Int32)(sumOfTimeUser).ExecuteScalar();
                    var ts = TimeSpan.FromMinutes(sumOfTimeUserString);
                    label24.Text = String.Format("{0} д. {1} ч. {2} м. ", ts.Days, ts.Hours, ts.Minutes);
                }
                catch (Exception)
                {
                    if (label12.Text == "Не подтвержден")
                    {
                        label24.Text = "-";
                    }
                    else if (label12.Text == "Подтвержден")
                    {
                        label24.Text = "0";
                    }
                }

                if (label12.Text == "Не подтвержден")
                {
                    button2.Visible = true;
                }
                else if (label12.Text == "Подтвержден")
                {
                    button2.Visible = true;
                }

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Messages Where IdUser = '" + Program.getIdUser + " ' ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    if ((bool)dt.Rows[i]["StatusPolzv"] == true)
                    {
                        string who;
                        who = "Пользователь: ";
                        string name = dt.Rows[i]["Text"].ToString();

                        richTextBox1.Text = who + name + "\r\n" + richTextBox1.Text;
                    }
                    else
                    {
                        string who;
                        who = "Вы: ";
                        string name = dt.Rows[i]["Text"].ToString();

                        richTextBox1.Text = who + name + "\r\n" + richTextBox1.Text;
                    }

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

        

        private void label27_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Program.fotoUser = false;
            f9 = new Form9();
            f9.ShowDialog();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Program.fotoUser = true;
            f9 = new Form9();
            f9.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (button2.Text == "Подтвердить пользователя")
                {

                    con = new SqlConnection(connectionString);
                    con.Open();
                    string updUserStatus = "Update Polzovatel Set StatusPodtverzdeniya = 1 Where IdUser = '" + Program.getIdUser + " '";
                    SqlCommand updUserStatusSql = new SqlCommand(updUserStatus, con);
                    updUserStatusSql.ExecuteNonQuery();
                    con.Close();
                    richTextBox1.Text = "";
                    Form8_Load(sender, e);
                }
                else if (button2.Text == "Заблокировать пользователя")
                {
                    
                    
                        con = new SqlConnection(connectionString);
                        con.Open();
                        string updUserStatus = "Update Polzovatel Set StatusPodtverzdeniya = 0 Where IdUser = '" + Program.getIdUser + " '";
                        SqlCommand updUserStatusSql = new SqlCommand(updUserStatus, con);
                        updUserStatusSql.ExecuteNonQuery();
                        con.Close();





                    
                }

            }
            catch(Exception ex)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label12_TextChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (label12.Text == "Подтвержден")
            {
                button2.Text = "Заблокировать пользователя";
            }
            if (label12.Text == "Не подтвержден")
            {
                button2.Text = "Подтвердить пользователя";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (richTextBox2.Text.Length == 0)
                {
                    MessageBox.Show("Сообщение не может быть пустым", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                string insertValueText = richTextBox2.Text;
                bool statusUser = false;

                string sqlInsertNewMessage = string.Format("INSERT INTO Messages (Text, StatusPolzv, IdUser) " +
                        " VALUES ('{0}', '{1}', {2})", insertValueText, statusUser, Program.getIdUser);
                SqlCommand insNewMessage = new SqlCommand(sqlInsertNewMessage, con);
                insNewMessage.ExecuteNonQuery();
                richTextBox2.Text = "";
                richTextBox1.Text = "";
                Form8_Load(sender, e);
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
