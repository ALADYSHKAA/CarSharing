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
    public partial class Form5 : Form
    {
        public SqlConnection con { get; set; }
        Form6 f6;
        Form1 f1;
        Form7 f7;
        Form10 f10;
        Form12 f12;
        Form13 f13;
        Form14 f14;
        Form18 f18;
        Form20 f20;

        public Form5()
        {
            InitializeComponent();
            
            
        }
        //private void LoadData()
        //{
        //    String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=Diplom;Integrated Security=True";
        //    con = new SqlConnection(connectionString);



        //    con.Open();

            

        //    string query = "SELECT * FROM Strahovka";

        //    SqlCommand command = new SqlCommand(query, con);

        //    SqlDataReader reader = command.ExecuteReader();

        //    List<string[]> data = new List<string[]>();

        //    while (reader.Read())
        //    {
        //        data.Add(new string[3]);

        //        data[data.Count - 1][0] = reader[0].ToString();
        //        data[data.Count - 1][1] = reader[1].ToString();
        //        data[data.Count - 1][2] = reader[2].ToString();
        //    }

        //    reader.Close();

        //    con.Close();

        //    foreach (string[] s in data)
        //        dataGridView1.Rows.Add(s);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //LoadData();
        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void администраторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f6 = new Form6();
            f6.ShowDialog();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f7 = new Form7();
            f7.ShowDialog();
        }

        private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f10 = new Form10();
            f10.ShowDialog();
        }

        private void тарифыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f12 = new Form12();
            f12.ShowDialog();
        }

        private void классыАвтомобилейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f13 = new Form13();
            f13.ShowDialog();
        }

        private void новостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f14 = new Form14();
            f14.ShowDialog();
        }

        private void поездкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f18 = new Form18();
            f18.ShowDialog();
        }

        private void происшествияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f20 = new Form20();
            f20.ShowDialog();
        }
    }
}
