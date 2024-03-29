﻿using NLog;
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
    public partial class Form2 : Form
    {
        CurrentMethod cm;
        Logger logger;
        public SqlConnection con { get; set; }
        public bool closing = true;

        public Form2()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
            
            if (Program.connectionError == true) 
            {
                label4.Location = new Point(180, 356);
                label4.Visible = true;
                label4.Text = "Ошибка в файлах подключения, пожалуйста введите новые данные";
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(System.Net.Dns.GetHostName());
            MessageBox.Show("Успешно");
        }

        internal bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            bool isRole = IsRunAsAdmin();
            if (isRole is false)
            {
                MessageBox.Show("В процессе работы данного приложения происходит чтение и запись файлов.\r Для корректной работы, пожалуйста запустите программу от имени администратора.\r Приложение будет закрыто.", "Не обнаружены права администратора", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            textBox1.Text = System.Net.Dns.GetHostName() + "\\SQLEXPRESS;";
            
            textBox2.Text = "Diplom";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // System.IO.File.AppendAllText("ServerName.txt", textBox1.Text + "\n");
            // System.IO.File.AppendAllText("DataBaseName.txt", textBox2.Text + "\n");
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            string writePath = "ServerName.txt";

            string text = textBox1.Text;
            string writePath1 = "DataBaseName.txt";

            string text1 = textBox2.Text;
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

                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }

       
            Program.serverName = textBox1.Text;
            Program.bdName = textBox2.Text;
            
            String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
            try
            {
               
                con = new SqlConnection(connectionString);
                con.Open();
                string query = "SELECT * FROM Strahovka";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                closing = false;
                CarSharing.Properties.Settings.Default.DiplomConnectionString = connectionString;
                
                this.Close();

            }
            catch(Exception ex)
            {
                
                label4.Visible = true;
                string method = cm.GetCurrentMethod();
                logger.Error(ex.ToString() + method);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closing == true)
            {
                Application.Exit();
            }

        }

       
    }
}
