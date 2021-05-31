using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CarSharing
{
    public partial class Form22 : Form
    {
        public SqlConnection con { get; set; }
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        bool checkInsert = true;
        public bool closing;
        Logger logger;
        CurrentMethod cm;
        Excel.Application xlApp;

        public Form22()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
        }
        private DataTable GetData()
        {
            con = new SqlConnection(connectionString);
            con.Open();
            DataTable dt = new DataTable();
            string query1 = " SELECT u.[NomerVY], u.[Fio],a.[Marka], a.[Nazvanie], a.[GosNomer], COUNT(idPoezdki)KolvoPoezdok, SUM(COALESCE(Stoim, 0)) Price, SUM(COALESCE(Dlitelnost, 0)) Duration" +
                 " FROM  Poezdka p LEFT JOIN [Polzovatel] u on u.[IdUser] = p.[idUser] INNER JOIN [Avto] a on p.[idAvto] = a.[idAvto] GROUP BY   u.[NomerVY], u.[Fio], a.[Marka],  " +
                 "a.[Nazvanie], a.[GosNomer] ORDER BY   u.[NomerVY], u.[Fio], a.[Marka], a.[Nazvanie] , a.[GosNomer]";

            SqlCommand comm1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(comm1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            dt = ds1.Tables[0];
            return dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

