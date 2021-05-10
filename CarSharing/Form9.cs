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

        public Form9()
        {
            InitializeComponent();
            if(Program.fotoUser == false)
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
    }
}
