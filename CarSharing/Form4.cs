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
    public partial class Form4 : Form
    {
        public SqlConnection con { get; set; }
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool checkInsert = true;
        public bool closing;
        Logger logger;
        CurrentMethod cm;
        public Form4()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            textBox9.Enabled = false;
            textBox8.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            textBox5.Enabled = false;
            textBox2.Select();
            button3.Enabled = false;
            textBox5.UseSystemPasswordChar = true;
            textBox8.UseSystemPasswordChar = true;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-18);
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                String insertValueName = textBox1.Text;
                String insertValueSurname = textBox2.Text;
                String insertValueSecondName = textBox3.Text;
                String insertValueFio = insertValueSurname + " " + insertValueName + " " + insertValueSecondName;
                String insertValueNumberOfPassport = maskedTextBox2.Text;
                String insertValuePlaceOfRegistration = richTextBox1.Text;
                String insertValueDateOfBirthday = dateTimePicker1.Value.ToString();
                String insertValueNumberOfDriverLicense = maskedTextBox1.Text;
                String insertValueEmail = textBox6.Text;
                String insertValueMobilePhone = maskedTextBox3.Text;
                String insertValueStatus = "0";
                if (maskedTextBox2.Text.Length != 10)
                {
                    MessageBox.Show("Пожалуйста, заполните до конца номер паспорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedTextBox2.Focus();
                    return;
                }
                if (maskedTextBox1.Text.Length != 10)
                {
                    MessageBox.Show("Пожалуйста, заполните до конца номер водительского удостоверения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedTextBox1.Focus();
                    return;
                }
                if (maskedTextBox3.Text.Length != 18)
                {
                    MessageBox.Show("Пожалуйста, заполните до конца номер телефона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedTextBox3.Focus();
                    return;
                }


                con = new SqlConnection(connectionString);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE Email = '" + insertValueEmail + " '", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlDataAdapter sd1 = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE NomerPassporta = '" + insertValueNumberOfPassport + " '", con);
                DataTable dt1 = new DataTable();
                sd1.Fill(dt1);
                SqlDataAdapter sd2 = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE NomerVY = '" + insertValueNumberOfDriverLicense + " '", con);
                DataTable dt2 = new DataTable();
                sd2.Fill(dt2);
                SqlDataAdapter sd3 = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE MobilePhone = '" + insertValueMobilePhone + " '", con);
                DataTable dt3 = new DataTable();
                sd3.Fill(dt3);
                if (dt1.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Данный номер паспорта уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt2.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Данный номер Водительского Удостоверения уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Такой адрес электронной почты уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt3.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Такой номер телефона уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    string sqlInsertNewUser = string.Format("INSERT INTO Polzovatel (Fio, NomerPassporta, Propiska, DataRozdeniya, NomerVY, StatusPodtverzdeniya, Email, MobilePhone)  VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}')", insertValueFio,
                    insertValueNumberOfPassport, insertValuePlaceOfRegistration,
                    insertValueDateOfBirthday, insertValueNumberOfDriverLicense, insertValueStatus, insertValueEmail,
                    insertValueMobilePhone);
                    SqlCommand insNewUser = new SqlCommand(sqlInsertNewUser, con);
                    insNewUser.ExecuteNonQuery();
                  
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    maskedTextBox1.Enabled = false;
                    richTextBox1.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;
                    maskedTextBox3.Enabled = false;
                    button2.Enabled = false;
                    textBox9.Enabled = true;
                    textBox8.Enabled = true;
                    button4.Enabled = true;
                    textBox5.Enabled = true;
                    checkInsert = false;
                    closing = false;
                    checkBox2.Enabled = true;

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
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (checkInsert == false)
                {


                    string message = "Форма регистрации была заполнена не до конца, если вы решите выйти сейчас, то все данные будут удалены и вам заново придется пройти процесс регистрации, Вы уверены?";
                    string caption = "Внимание";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        con = new SqlConnection(connectionString);
                        con.Open();
                        String insertValueEmail = textBox6.Text;
                        string idUserSelect = "SELECT IdUser FROM Polzovatel Where Email = '" + insertValueEmail + " '";
                        SqlCommand idUser = new SqlCommand(idUserSelect, con);
                        Int32 idUserInt = (Int32)(idUser).ExecuteScalar();

                        string sqlDelUser = string.Format("DELETE FROM Polzovatel WHERE IdUser = '" + idUserInt + " '");
                        SqlCommand delUser = new SqlCommand(sqlDelUser, con);
                        delUser.ExecuteNonQuery();


                        con.Close();
                        closing = true;
                        this.Close();
                    }
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueEmail = textBox6.Text;

                string idUserSelect = "SELECT IdUser FROM Polzovatel Where Email = '" + insertValueEmail + " '";
                SqlCommand idUser = new SqlCommand(idUserSelect, con);
                Int32 idUserInt = (Int32)(idUser).ExecuteScalar();
               

                String insertValueLogin = textBox9.Text;
                String insertValuePassword = textBox8.Text;
                String insertValuePassword2 = textBox5.Text;

                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From AutDate WHERE Login = '" + insertValueLogin + " '", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Данный логин уже занят", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textBox8.Text.Length < 10)
                {
                    MessageBox.Show("Пароль должен состоять минимум из 10 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (matching(insertValuePassword, insertValuePassword2) == false)
                {
                    MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {

                    string sqlInsertNewAuthDate = string.Format("INSERT INTO AutDate (Login, Password, idUser)  VALUES ('{0}', '{1}', {2})", insertValueLogin,
                        insertValuePassword, idUserInt);
                    SqlCommand insNewAuthDate = new SqlCommand(sqlInsertNewAuthDate, con);
                    insNewAuthDate.ExecuteNonQuery();
                   
                    textBox9.Enabled = false;
                    textBox8.Enabled = false;
                    button4.Enabled = false;

                    textBox5.Enabled = false;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }

        }
        public bool matching(string tb2, string tb6)
        {
            tb2 = Convert.ToString(textBox8.Text);
            tb6 = Convert.ToString(textBox5.Text);
            return tb2 == tb6 ? true : false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
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
                ;
                if (ofd.FileName == "")
                {
                    MessageBox.Show("Фотография не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueEmail = textBox6.Text;
                string idUserSelect = "SELECT IdUser FROM Polzovatel Where Email = '" + insertValueEmail + " '";
                SqlCommand idUser = new SqlCommand(idUserSelect, con);
                Int32 idUserInt = (Int32)(idUser).ExecuteScalar();
                
                String insertValueFileName = ofd.FileName;
                string sqlInsertPhotoPassporta = string.Format("INSERT INTO Photos (FotoOfPassport, idUser)  VALUES ('{0}', {1})", insertValueFileName, idUserInt);
                SqlCommand insNewAuthDate = new SqlCommand(sqlInsertPhotoPassporta, con);
                insNewAuthDate.ExecuteNonQuery();
                
                button5.Enabled = false;
                button6.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                var ofd = new OpenFileDialog();
                ofd.Filter = "Image Files(*.BMP; *.PNG; *.JPG; *.GIF)| *.BMP; *.PNG; *.JPG; *.GIF";
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                    pictureBox2.Image = Image.FromFile(ofd.FileName);
                if (ofd.FileName == "")
                {
                    MessageBox.Show("Фотография не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueEmail = textBox6.Text;
                string idUserSelect = "SELECT IdUser FROM Polzovatel Where Email = '" + insertValueEmail + " '";
                SqlCommand idUser = new SqlCommand(idUserSelect, con);
                Int32 idUserInt = (Int32)(idUser).ExecuteScalar();
                
                String insertValueFileName = ofd.FileName;
                string sqlInsertPhotoPassporta = string.Format("UPDATE Photos SET FotoOfDriverLicense = '{0}' WHERE idUser = {1}", insertValueFileName, idUserInt);
                SqlCommand insNewAuthDate = new SqlCommand(sqlInsertPhotoPassporta, con);
                insNewAuthDate.ExecuteNonQuery();
                
                button6.Enabled = false;
                button3.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }

        }

        private void maskedTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (maskedTextBox2.Text.Length == 0 )
                ((MaskedTextBox)sender).SelectionStart = 0;
            
        } 

        private void maskedTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (maskedTextBox1.Text.Length == 0)
            ((MaskedTextBox)sender).SelectionStart = 0;
        }

        private void maskedTextBox3_MouseClick(object sender, MouseEventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (maskedTextBox3.Text.Length == 16)
            ((MaskedTextBox)sender).SelectionStart = 4;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            textBox2.Focus();


        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                var ofd = new OpenFileDialog();
                ofd.Filter = "Image Files(*.BMP; *.PNG; *.JPG; *.GIF)| *.BMP; *.PNG; *.JPG; *.GIF";
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                    pictureBox3.Image = Image.FromFile(ofd.FileName);
                if (ofd.FileName == "")
                {
                    MessageBox.Show("Фотография не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                String insertValueEmail = textBox6.Text;
                string idUserSelect = "SELECT IdUser FROM Polzovatel Where Email = '" + insertValueEmail + " '";
                SqlCommand idUser = new SqlCommand(idUserSelect, con);
                Int32 idUserInt = (Int32)(idUser).ExecuteScalar();
               
                String insertValueFileName = ofd.FileName;
                string sqlInsertPhotoPassporta = string.Format("UPDATE Photos SET FotoOfFace = '{0}' WHERE idUser = {1}", insertValueFileName, idUserInt);
                SqlCommand insNewAuthDate = new SqlCommand(sqlInsertPhotoPassporta, con);
                insNewAuthDate.ExecuteNonQuery();
                MessageBox.Show("Спасибо за регистрацию, проверка вашей заявки займет от 1 часа до нескольких дней.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Enabled = false;
                checkInsert = true;
                closing = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox2.CheckState == CheckState.Checked)
            {
                textBox5.UseSystemPasswordChar = false;
                textBox8.UseSystemPasswordChar = false;
            }
            else
            {
                textBox5.UseSystemPasswordChar = true;
                textBox8.UseSystemPasswordChar = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            if (checkBox1.CheckState == CheckState.Checked)
            {
                button5.Enabled = true;
                checkBox1.Enabled = false;
            }
        }
    }
    
}
