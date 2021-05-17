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
    public partial class Form9 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Logger logger;
        CurrentMethod cm;

        public Form9()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                if (Program.fotoUser == false)
                {
                    con = new SqlConnection(connectionString);
                    con.Open();
                    string vyUserSelect = "SELECT FotoOfDriverLicense FROM Photos Where idUser ='" + Program.getIdUser + " '";
                    SqlCommand vyUser = new SqlCommand(vyUserSelect, con);
                    String vyUserString = (String)(vyUser).ExecuteScalar();
                    pictureBox1.Image = Image.FromFile(vyUserString);
                    con.Close();

                }
                if (Program.fotoUser == true)
                {
                    con = new SqlConnection(connectionString);
                    con.Open();
                    string vyUserSelect = "SELECT FotoOfPassport FROM Photos Where idUser ='" + Program.getIdUser + " '";
                    SqlCommand vyUser = new SqlCommand(vyUserSelect, con);
                    String vyUserString = (String)(vyUser).ExecuteScalar();
                    pictureBox1.Image = Image.FromFile(vyUserString);
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
    }
}
