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
    public partial class Form6 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Boolean insertAdmin = false;
        Boolean updateAdmin = false;
        Boolean deleteAdmin = false;
        
        public Form6()
        {
            InitializeComponent();

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

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
           
            
        }
        
        private void GetData(string selectCommand)
        {
            try
            {
                
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
                bindingSource2.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = bindingSource2;
            GetData("select * from ViewAdmins");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 280;
            dataGridView1.Columns[1].HeaderText = "Фио";
            dataGridView1.Columns[2].HeaderText = "Код";
            dataGridView1.Columns[2].Width = 183;
            dataGridView1.Columns[3].HeaderText = "Электронная почта";
            dataGridView1.Columns[3].Width = 222;
            dataGridView1.Columns[4].HeaderText = "Логин";
            dataGridView1.Columns[4].Width = 180;
            dataGridView1.Columns[5].HeaderText = "Пароль";
            dataGridView1.Columns[5].Width = 180;
            button3.Visible = false;



        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            insertAdmin = true;
            button3.Text = "Сохранить";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            updateAdmin = false;
            deleteAdmin = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            
            label7.Text = "В данный момент вы находитесь в режиме добавления нового Администратора";
            label7.ForeColor = Color.Green;
            label7.Location = new Point(240, 559);
            button3.Visible = true; 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Измения не сохранены, вы действительно хотите выйти?";
            string caption = "Изменения не сохранены";
            if (insertAdmin == true || updateAdmin == true || deleteAdmin == true)
            {
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                if(result == DialogResult.No)
                {
                    return;
                }
            }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            con = new SqlConnection(connectionString);
            con.Open();
            
           

            if (insertAdmin == true)
            {
                String insertValueFio = textBox3.Text;
                String insertValueKod = textBox2.Text;
                String insertValueEmail = textBox1.Text;
                String insertValueLogin = textBox5.Text;
                String insertValuePassword = textBox4.Text;
                String insertValueIdAdmin = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                if (textBox3.Text.Length < 15)
                {
                    label7.Text = "Поле Фио не может быть короче 15 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox2.Text.Length < 10)
                {
                    label7.Text = "Код не может быть короче 10 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox1.Text.Length < 8)
                {
                    label7.Text = "Электронная почта не может быть короче 10 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox5.Text.Length < 5)
                {
                    label7.Text = "Логин не может быть короче 5 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox4.Text.Length < 10)
                {
                    label7.Text = "Пароль не может быть короче 10 символов ";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }


                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From Admins WHERE KodAdmin = '" + insertValueKod + " '", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Count (*) From Admins WHERE Email = '" + insertValueEmail + " '", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                SqlDataAdapter sda2 = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE Email = '" + insertValueEmail + " '", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                SqlDataAdapter sda3 = new SqlDataAdapter("Select Count (*) From AutDate WHERE Login = '" + insertValueLogin + " '", con);
                DataTable dt3 = new DataTable();
                sda3.Fill(dt3);
                

                if (dt.Rows[0][0].ToString() != "0")
                {
                    label7.Text = "Данный код уже используется, пожалуйста, введите другой";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);
                    return;
                }
                if (dt1.Rows[0][0].ToString() != "0")
                {
                    label7.Text = "Данная электронная почта уже принадлежит другому Администратору";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);
                    return;
                }
                if (dt2.Rows[0][0].ToString() != "0")
                {
                    label7.Text = "Данная электронная почта уже принадлежит другому пользователю";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);
                    return;
                }
                if (dt3.Rows[0][0].ToString() != "0")
                {
                    label7.Text = "К сожалению, данный логин уже занят";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(370, 559);
                    return;
                }

                string sqlInsertNewAdmin = string.Format("INSERT INTO Admins (FIO, KodAdmin, Email )  VALUES ('{0}', '{1}', '{2}')", insertValueFio,
                    insertValueKod, insertValueEmail);
                SqlCommand insNewAdmin = new SqlCommand(sqlInsertNewAdmin, con);
                insNewAdmin.ExecuteNonQuery();
                string idAdminSelect = "SELECT idAdmin FROM Admins Where Email = '" + insertValueEmail + " '";
                SqlCommand idAdmin = new SqlCommand(idAdminSelect, con);
                Int32 idAdminInt = (Int32)(idAdmin).ExecuteScalar();
                MessageBox.Show(Convert.ToString(idAdminInt));
                string sqlInsertNewAdminAut = string.Format("INSERT INTO AutDate (Login, Password, idAdmin )  VALUES ('{0}', '{1}', {2})", insertValueLogin,
                    insertValuePassword, idAdminInt);
                SqlCommand insNewAdminAut = new SqlCommand(sqlInsertNewAdminAut, con);
                insNewAdminAut.ExecuteNonQuery();

                GetData("select * from ViewAdmins");
                MessageBox.Show("Успешно");
                insertAdmin = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button3.Visible = false;

                con.Close();
                label7.Text = "Пожалуйста выберите действие, которое вы хотите совершить с Админстратором.";
                label7.ForeColor = Color.Black;
                label7.Location = new Point(223, 559);
            }
            else if(updateAdmin == true)
            {
                String insertValueFio = textBox3.Text;
                String insertValueKod = textBox2.Text;
                String insertValueEmail = textBox1.Text;
                String insertValueLogin = textBox5.Text;
                String insertValuePassword = textBox4.Text;

               
                if (textBox3.Text.Length < 15)
                {
                    label7.Text = "Поле Фио не может быть короче 15 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox2.Text.Length < 10)
                {
                    label7.Text = "Код не может быть короче 10 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox1.Text.Length < 8)
                {
                    label7.Text = "Электронная почта не может быть короче 10 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox5.Text.Length < 5)
                {
                    label7.Text = "Логин не может быть короче 5 символов";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                if (textBox4.Text.Length < 10)
                {
                    label7.Text = "Пароль не может быть короче 10 символов ";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(350, 559);
                    return;
                }
                
                string sqlUpdateAuthDate = string.Format("UPDATE AutDate SET Login = '{0}' , Password = '{1}' WHERE idAdmin ={2}",
                            insertValueLogin, insertValuePassword, Program.insertValueAdmin);
                SqlCommand updDate = new SqlCommand(sqlUpdateAuthDate, con);
                updDate.ExecuteNonQuery();

                string sqlUpdateAdmin = string.Format("UPDATE Admins SET FIO = '{0}' , KodAdmin = '{1}', Email = '{2}' WHERE idAdmin ={3}",
                    insertValueFio, insertValueKod, insertValueEmail, Program.insertValueAdmin);
                SqlCommand updAdmin = new SqlCommand(sqlUpdateAdmin, con);
                updAdmin.ExecuteNonQuery();
                GetData("select * from ViewAdmins");

                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From Admins WHERE KodAdmin = '" + insertValueKod + " '", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Count (*) From Admins WHERE Email = '" + insertValueEmail + " '", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                SqlDataAdapter sda2 = new SqlDataAdapter("Select Count (*) From Polzovatel WHERE Email = '" + insertValueEmail + " '", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                SqlDataAdapter sda3 = new SqlDataAdapter("Select Count (*) From AutDate WHERE Login = '" + insertValueLogin + " '", con);
                DataTable dt3 = new DataTable();
                sda3.Fill(dt3);

                if (dt.Rows[0][0].ToString() !=  "0" && dt.Rows[0][0].ToString() != "1" )
                {

                    
                    label7.Text = "Данный код уже используется, пожалуйста, введите новый";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);
                    string sqlUpdateAuthDate1 = string.Format("UPDATE AutDate SET Login = '{0}' , Password = '{1}' WHERE idAdmin ={2}",
                            Program.checkAdminLogin, Program.checkAdminPas, Program.insertValueAdmin);
                    SqlCommand updDate1 = new SqlCommand(sqlUpdateAuthDate1, con);
                    updDate1.ExecuteNonQuery();

                    string sqlUpdateAdmin1 = string.Format("UPDATE Admins SET FIO = '{0}' , KodAdmin = '{1}', Email = '{2}' WHERE idAdmin ={3}",
                        Program.checkAdminFio, Program.checkAdminKod, Program.checkAdminEmail, Program.insertValueAdmin);
                    SqlCommand updAdmin1 = new SqlCommand(sqlUpdateAdmin1, con);
                    updAdmin1.ExecuteNonQuery();
                    GetData("select * from ViewAdmins");
                    con.Close();
                    return;
                }
                if (dt1.Rows[0][0].ToString() != "0" && dt1.Rows[0][0].ToString() != "1")
                {
                    
                    label7.Text = "Данная электронная почта уже принадлежит другому Администратору";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);

                    string sqlUpdateAuthDate1 = string.Format("UPDATE AutDate SET Login = '{0}' , Password = '{1}' WHERE idAdmin ={2}",
                            Program.checkAdminLogin, Program.checkAdminPas, Program.insertValueAdmin);
                    SqlCommand updDate1 = new SqlCommand(sqlUpdateAuthDate1, con);
                    updDate1.ExecuteNonQuery();

                    string sqlUpdateAdmin1 = string.Format("UPDATE Admins SET FIO = '{0}' , KodAdmin = '{1}', Email = '{2}' WHERE idAdmin ={3}",
                        Program.checkAdminFio, Program.checkAdminKod, Program.checkAdminEmail, Program.insertValueAdmin);
                    SqlCommand updAdmin1 = new SqlCommand(sqlUpdateAdmin1, con);
                    updAdmin1.ExecuteNonQuery();
                    GetData("select * from ViewAdmins");
                    con.Close();
                    con.Close();
                    return;
                }
                if (dt2.Rows[0][0].ToString() != "0" )
                {
                    
                    label7.Text = "Данная электронная почта уже принадлежит другому пользователю";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(300, 559);
                    string sqlUpdateAuthDate1 = string.Format("UPDATE AutDate SET Login = '{0}' , Password = '{1}' WHERE idAdmin ={2}",
                            Program.checkAdminLogin, Program.checkAdminPas, Program.insertValueAdmin);
                    SqlCommand updDate1 = new SqlCommand(sqlUpdateAuthDate1, con);
                    updDate1.ExecuteNonQuery();

                    string sqlUpdateAdmin1 = string.Format("UPDATE Admins SET FIO = '{0}' , KodAdmin = '{1}', Email = '{2}' WHERE idAdmin ={3}",
                        Program.checkAdminFio, Program.checkAdminKod, Program.checkAdminEmail, Program.insertValueAdmin);
                    SqlCommand updAdmin1 = new SqlCommand(sqlUpdateAdmin1, con);
                    updAdmin1.ExecuteNonQuery();
                    GetData("select * from ViewAdmins");
                    con.Close();
                    con.Close();
                    return;
                }
                if (dt3.Rows[0][0].ToString() != "0" && dt3.Rows[0][0].ToString() != "1")
                {
                    
                    label7.Text = "К сожалению, данный логин уже занят";
                    label7.ForeColor = Color.Orange;
                    label7.Location = new Point(370, 559);
                    string sqlUpdateAuthDate1 = string.Format("UPDATE AutDate SET Login = '{0}' , Password = '{1}' WHERE idAdmin ={2}",
                             Program.checkAdminLogin, Program.checkAdminPas, Program.insertValueAdmin);
                    SqlCommand updDate1 = new SqlCommand(sqlUpdateAuthDate1, con);
                    updDate1.ExecuteNonQuery();

                    string sqlUpdateAdmin1 = string.Format("UPDATE Admins SET FIO = '{0}' , KodAdmin = '{1}', Email = '{2}' WHERE idAdmin ={3}",
                        Program.checkAdminFio, Program.checkAdminKod, Program.checkAdminEmail, Program.insertValueAdmin);
                    SqlCommand updAdmin1 = new SqlCommand(sqlUpdateAdmin1, con);
                    updAdmin1.ExecuteNonQuery();
                    GetData("select * from ViewAdmins");
                    con.Close();
                    
                    return;

                }


                updateAdmin = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button3.Visible = false;
                label7.Text = "Пожалуйста выберите действие, которое вы хотите совершить с Админстратором.";
                label7.ForeColor = Color.Black;
                label7.Location = new Point(223, 559);

                con.Close();
                    

                
               


            }
            else if (deleteAdmin == true)
            {
                
                String insertValueIdAdmin = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                string message = "Вы действительно хотите удалить данного администратора?";
                string caption = "Удаление Администратора";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string sqlDelAdminAuth = string.Format("DELETE FROM AutDate WHERE idAdmin = '{0}'", insertValueIdAdmin);
                    SqlCommand delAdminAuth = new SqlCommand(sqlDelAdminAuth, con);
                    delAdminAuth.ExecuteNonQuery();
                    string sqlDelAdmin = string.Format("DELETE FROM Admins WHERE idAdmin = '{0}'", insertValueIdAdmin);
                    SqlCommand delAdmin = new SqlCommand(sqlDelAdmin, con);
                    delAdmin.ExecuteNonQuery();
                    GetData("select * from ViewAdmins");
                   
                    con.Close();
                    label7.Text = "Пожалуйста выберите действие, которое вы хотите совершить с Админстратором.";
                    label7.ForeColor = Color.Black;
                    label7.Location = new Point(223, 559);
                    button3.Visible = false;
                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                updateAdmin = true;
                button3.Text = "Изменить";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                insertAdmin = false;
                deleteAdmin = false;
                textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                Program.checkAdminFio = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                Program.checkAdminKod = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                Program.checkAdminEmail = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                Program.checkAdminLogin = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                Program.insertValueAdmin = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                Program.checkAdminPas = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                label7.Visible = true;
                label7.Text = "В данный момент вы находитесь в режиме редактирования Администратора";
                label7.ForeColor = Color.Blue;
                label7.Location = new Point(233, 559);
                button3.Visible = true;
            }
            else
            {
                MessageBox.Show("Веберите Администратора, которого хотите изменить");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deleteAdmin = true;
            updateAdmin = false;
            insertAdmin = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button3.Text = "Удалить";
            label7.Visible = true;
            label7.Text = "В данный момент вы находитесь в режиме удаления Администратора";
            label7.ForeColor = Color.Red;
            label7.Location = new Point(260,559);
            button3.Visible = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
    }
