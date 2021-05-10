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
    public partial class Form17 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool update;

        public Form17()
        {
            InitializeComponent();
            

        }

        private void Form17_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();

            string fioUserSelect = "SELECT Fio FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand fioUser = new SqlCommand(fioUserSelect, con);
            String fioUserString = (String)(fioUser).ExecuteScalar();
            label2.Text = fioUserString;

            string numberOfPassportUserSelect = "SELECT NomerPassporta FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand numberOfPassportUser = new SqlCommand(numberOfPassportUserSelect, con);
            String numberOfPassportUserString = (String)(numberOfPassportUser).ExecuteScalar();
            label4.Text = numberOfPassportUserString;

            string propiskaUserSelect = "SELECT Propiska FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand propiskaUser = new SqlCommand(propiskaUserSelect, con);
            String propiskaUserString = (String)(propiskaUser).ExecuteScalar();
            label6.Text = propiskaUserString;

            string dayOfBirthdayUserSelect = "SELECT DataRozdeniya FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand dayOfBirthdayUser = new SqlCommand(dayOfBirthdayUserSelect, con);
            DateTime dayOfBirthdayUserString = (DateTime)(dayOfBirthdayUser).ExecuteScalar();
            var date = dayOfBirthdayUserString.ToShortDateString();
            label8.Text = Convert.ToString(date);

            string numberOfDriverLicenseUserSelect = "SELECT NomerVY FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand numberOfDriverLicenseUser = new SqlCommand(numberOfDriverLicenseUserSelect, con);
            String numberOfDriverLicenseUserString = (String)(numberOfDriverLicenseUser).ExecuteScalar();
            label10.Text = numberOfDriverLicenseUserString;

            string statusUserSelect = "SELECT StatusPodtverzdeniya FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand statusUser = new SqlCommand(statusUserSelect, con);
            Boolean statusUserString = (Boolean)(statusUser).ExecuteScalar();
            var status = statusUserString.ToString();
            label12.Text = status;

            if (label12.Text == "False")
            {
                label12.Text = "В данный момент ваша заявка находится на проверке у администрации";
            }

            else if (label12.Text == "True")
            {
                label12.Text = "Подтвержден";
                label12.ForeColor = Color.Green;
            }

            string emailUserSelect = "SELECT Email FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand emailUser = new SqlCommand(emailUserSelect, con);
            String emailUserString = (String)(emailUser).ExecuteScalar();
            label14.Text = emailUserString;

            string mobilePhoneUserSelect = "SELECT MobilePhone FROM Polzovatel Where IdUser = '" + Program.idUser + " '";
            SqlCommand mobilePhoneUser = new SqlCommand(mobilePhoneUserSelect, con);
            String mobilePhoneUserString = (String)(mobilePhoneUser).ExecuteScalar();
            label16.Text = mobilePhoneUserString;

             string countTripsUserSelect = "Select Count (*) From Poezdka WHERE idUser = '" + Program.idUser + " '";
            SqlCommand countTripsUser = new SqlCommand(countTripsUserSelect, con);
            Int32 countTripsUserString = (Int32)(countTripsUser).ExecuteScalar();
            label18.Text = countTripsUserString.ToString();
            if (label12.Text == "В данный момент ваша заявка находится на проверке у администрации")
            {
                label18.Text = "-";
            }

            try
            {
                string sumOfTripsUserSelect = "Select SUM(Stoim) From Poezdka WHERE idUser = '" + Program.idUser + " '";
                SqlCommand sumOfTripsUser = new SqlCommand(sumOfTripsUserSelect, con);
                Int32 sumOfTripsUserString = (Int32)(sumOfTripsUser).ExecuteScalar();
                label20.Text = sumOfTripsUserString.ToString() + "р.";
            }
            catch (Exception)
            {
                if (label12.Text == "В данный момент ваша заявка находится на проверке у администрации")
                {
                    label20.Text = "-";
                }
                else if (label12.Text == "Подтвержден")
                {
                    label20.Text = "0";
                }
            }

            string countProisUserSelect = "Select Count (idProischestviya) From Poezdka WHERE idUser = '" + Program.idUser + " '";
            SqlCommand countProisTripsUser = new SqlCommand(countProisUserSelect, con);
            Int32 countProisUserString = (Int32)(countProisTripsUser).ExecuteScalar();
            label22.Text = countProisUserString.ToString();
            if (label22.Text != "0")
            {
                label22.ForeColor = Color.Red;
            }
            if (label12.Text == "В данный момент ваша заявка находится на проверке у администрации")
            {
                label22.Text = "-";
            }

            try
            {
                string sumOfTimeUserSelect = "Select SUM(Dlitelnost) From Poezdka WHERE idUser = '" + Program.idUser + " '";
                SqlCommand sumOfTimeUser = new SqlCommand(sumOfTimeUserSelect, con);
                Int32 sumOfTimeUserString = (Int32)(sumOfTimeUser).ExecuteScalar();
                var ts = TimeSpan.FromMinutes(sumOfTimeUserString);
                label24.Text = String.Format("{0} д. {1} ч. {2} м. ", ts.Days, ts.Hours, ts.Minutes);
            }
            catch (Exception)
            {
                if (label12.Text == "В данный момент ваша заявка находится на проверке у администрации")
                {
                    label24.Text = "-";
                }
                else if (label12.Text == "Подтвержден")
                {
                    label24.Text = "0";
                }
            }


            string loginUserSelect = "Select Login From AutDate WHERE idUser = '" + Program.idUser + " '";
            SqlCommand loginUser = new SqlCommand(loginUserSelect, con);
            string loginUserString = (string)(loginUser).ExecuteScalar();
            label26.Text = loginUserString.ToString();

            string pasportUserSelect = "SELECT FotoOfPassport FROM Photos Where idUser ='" + Program.idUser + " '";
            SqlCommand pasportUser = new SqlCommand(pasportUserSelect, con);
            String pasportUserString = (String)(pasportUser).ExecuteScalar();
            pictureBox1.Image = Image.FromFile(pasportUserString);

            string vytUserSelect = "SELECT FotoOfDriverLicense FROM Photos Where idUser ='" + Program.idUser + " '";
            SqlCommand vyUser = new SqlCommand(vytUserSelect, con);
            String vytUserString = (String)(vyUser).ExecuteScalar();
            pictureBox2.Image = Image.FromFile(vytUserString);

            string faceUserSelect = "SELECT FotoOfFace FROM Photos Where idUser ='" + Program.idUser + " '";
            SqlCommand faceUser = new SqlCommand(faceUserSelect, con);
            String faceUserString = (String)(faceUser).ExecuteScalar();
            pictureBox3.Image = Image.FromFile(faceUserString);


            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Messages Where IdUser = '" + Program.idUser + " ' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               

                if ((bool)dt.Rows[i]["StatusPolzv"] == true)
                {
                    string who;
                    who = "Вы: ";
                    string name = dt.Rows[i]["Text"].ToString();

                    richTextBox1.Text = who  + name + "\r\n" + richTextBox1.Text;
                }
                else
                {
                    string who;
                    who = "Администаратор: ";
                    string name = dt.Rows[i]["Text"].ToString();

                    richTextBox1.Text = who  + name + "\r\n" + richTextBox1.Text;
                }
               
            }

            //string messageUserSelect = "SELECT Text FROM Messages Where IdUser = '" + Program.idUser + " ' ";
            //SqlCommand messageUser = new SqlCommand(messageUserSelect, con);
            //String messageUserString = (String)(messageUser).ExecuteScalar();
            //richTextBox1.Text = messageUserString;


            con.Close();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(richTextBox2.Text.Length == 0)
            {
                MessageBox.Show("Сообщение не может быть пустым", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            con = new SqlConnection(connectionString);
            con.Open();
            string insertValueText = richTextBox2.Text;
            bool statusUser = true;

            string sqlInsertNewMessage = string.Format("INSERT INTO Messages (Text, StatusPolzv, IdUser) " +
                    " VALUES ('{0}', '{1}', {2})", insertValueText, statusUser, Program.idUser);
            SqlCommand insNewMessage = new SqlCommand(sqlInsertNewMessage, con);
            insNewMessage.ExecuteNonQuery();
            richTextBox2.Text = "";
            richTextBox1.Text = "";
            Form17_Load(sender, e);
        }
    }
}
