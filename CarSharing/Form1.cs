using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSharing
{
    public partial class Form1 : Form
    {
        Form2 f2;
        Form3 f3;
        Form5 f5;
        Form15 f15;
        CurrentMethod cm;
        //  Form6 f6;
        // Form7 f7;
        Form17 f17;
        Logger logger;

        public SqlConnection con { get; set; }
         public SqlConnection con1 { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";

       

        public Form1()
        {
            InitializeComponent();
            StreamReader streamReader1;
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            this.Text = "Главное меню";
           
            if (System.IO.File.Exists("ServerName.txt") && System.IO.File.Exists("DataBaseName.txt"))
            {
                string path = "ServerName.txt";
                string path1 = "DataBaseName.txt";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                       
                        Program.serverName = line;
                        
                    }
                }
                using (StreamReader sr = new StreamReader(path1, System.Text.Encoding.Default))
                {
                    string line1;
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        
                        Program.bdName = line1;
                        
                    }
                }
                try
                {
                    String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
 
                    con = new SqlConnection(connectionString);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select * From Admins", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();
                }

                catch(Exception)
                {
                    f2 = new Form2();
                    /*settingsForm.*/
                    f2.ShowDialog();
                    Program.connectionError = true;
                    
                }
            }
            else
            {
                f2 = new Form2();
                /*settingsForm.*/
                f2.ShowDialog();
            }
            
            f3 = new Form3();
            /*settingsForm.*/
            f3.ShowDialog();

           
            if (Program.adminOrUser == false)
            {
                button3.Visible = false;
                button6.Enabled = false;
            }
            if(Program.adminOrUser == true)
            {
                button3.Visible = true;
                button6.Visible = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
           
            if(Program.confirmUserOrNo == true)
            {
                button3.Visible = false;
                button6.Enabled = false;
                button1.Enabled = false;
            }

            
            


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                String connectionString1 = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                     "Integrated Security=True";

                con1 = new SqlConnection(connectionString1);
                con1.Open();
                CarSharing.Properties.Settings.Default.DiplomConnectionString = connectionString1;
                string firstNewSelect = "SELECT TOP (1)  KratkoeOpicanie  FROM News ORDER BY idNews DESC";
                SqlCommand firstNew = new SqlCommand(firstNewSelect, con);
                String firstNewString = (String)(firstNew).ExecuteScalar();
                label3.Text = firstNewString;

                string firstNewFullSelect = "SELECT TOP (1)  PolnoeOpicanie  FROM News ORDER BY idNews DESC";
                SqlCommand firstNewFull = new SqlCommand(firstNewFullSelect, con);
                String firstNewFullString = (String)(firstNewFull).ExecuteScalar();
                label2.Text = firstNewFullString;

                string newPicture = "SELECT TOP (1) Izobrazenie FROM News ORDER BY idNews DESC";
                SqlCommand sqlnewPicture = new SqlCommand(newPicture, con);
                String pictureString = (String)(sqlnewPicture).ExecuteScalar();
                pictureBox1.Image = Image.FromFile(pictureString);


                SqlDataAdapter sda1 = new SqlDataAdapter("select top(2) * from News order by idNews desc", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                label4.Text = dt1.Rows[1][1].ToString();
                label5.Text = dt1.Rows[1][2].ToString();
                pictureBox2.Image = Image.FromFile(dt1.Rows[1][3].ToString());
                con1.Close();
                string v = cm.GetCurrentMethod();
                logger.Info(v);

            }
            catch(Exception ex)
            {
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Size = new Size(0, 0);
            this.CenterToScreen();
            f17 = new Form17();
            f17.ShowDialog();
            this.Size = new Size(1070, 769);
            this.CenterToScreen();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Size = new Size(0, 0);
            this.CenterToScreen();
            Form5 f5 = new Form5();
            f5.ShowDialog();
            Form1_Load_1(sender, e);
            this.Size = new Size(1070, 769);
            this.CenterToScreen();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Application.Exit();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
               

                string v = cm.GetCurrentMethod();
                logger.Info(v);
                String connectionString1 = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                     "Integrated Security=True";
                CarSharing.Properties.Settings.Default.DiplomConnectionString = connectionString1;
                con1 = new SqlConnection(connectionString1);
                con1.Open();

                string firstNewSelect = "SELECT TOP (1)  KratkoeOpicanie  FROM News ORDER BY idNews DESC";
                SqlCommand firstNew = new SqlCommand(firstNewSelect, con1);
                String firstNewString = (String)(firstNew).ExecuteScalar();
                label3.Text = firstNewString;

                string firstNewFullSelect = "SELECT TOP (1)  PolnoeOpicanie  FROM News ORDER BY idNews DESC";
                SqlCommand firstNewFull = new SqlCommand(firstNewFullSelect, con1);
                String firstNewFullString = (String)(firstNewFull).ExecuteScalar();
                label2.Text = firstNewFullString;

                string newPicture = "SELECT TOP (1) Izobrazenie FROM News ORDER BY idNews DESC";
                SqlCommand sqlnewPicture = new SqlCommand(newPicture, con1);
                String pictureString = (String)(sqlnewPicture).ExecuteScalar();
                pictureBox1.Image = Image.FromFile(pictureString);


                SqlDataAdapter sda1 = new SqlDataAdapter("select top(2) * from News order by idNews desc", con1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                label4.Text = dt1.Rows[1][1].ToString();
                label5.Text = dt1.Rows[1][2].ToString();
                pictureBox2.Image = Image.FromFile(dt1.Rows[1][3].ToString());
                con1.Close();

            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);

            string writePath = "ServerName.txt";

            string text = "Изменение";
            string writePath1 = "DataBaseName.txt";

            string text1 = "Изменение";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath1, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text1);
                }
                MessageBox.Show("Перезапуск приложения может занять некоторое время, пожалуйста дождитесь открытия окна выбора подключения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex )
            {
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
                MessageBox.Show(Convert.ToString(ex));
            }
            Application.Restart();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Size = new Size(0, 0);
            this.CenterToScreen();
            f15 = new Form15();
            f15.ShowDialog();
            this.Size = new Size(1070, 769);
            this.CenterToScreen();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Application.Restart();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
