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
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Form6 f6;
        Form1 f1;
        Form7 f7;
        Form10 f10;
        Form12 f12;
        Form13 f13;
        Form14 f14;
        Form18 f18;
        Form20 f20;
        Form21 f21;

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
            con = new SqlConnection(connectionString);
            con.Open();

            string countTripsSelect = "Select Count (idPoezdki) From Poezdka ";
            SqlCommand countTrips = new SqlCommand(countTripsSelect, con);
            Int32 countTripsString = (Int32)(countTrips).ExecuteScalar();
            label3.Text = countTripsString.ToString();

            try
            {
                string sumTripsSelect = "Select SUM (Stoim) From Poezdka ";
                SqlCommand sumTrips = new SqlCommand(sumTripsSelect, con);
                Int32 sumTripsString = (Int32)(sumTrips).ExecuteScalar();
                label4.Text = sumTripsString.ToString() + " р.";
            }
            catch (Exception)
            {
                label4.Text = "0";
            }

            string countProisSelect = "Select Count (idProischestviya) From Poezdka ";
            SqlCommand countProis = new SqlCommand(countProisSelect, con);
            Int32 countProisString = (Int32)(countProis).ExecuteScalar();
            label6.Text = countProisString.ToString();

            string countAvtoSelect = "Select Count (idAvto) From Avto ";
            SqlCommand countAvto = new SqlCommand(countAvtoSelect, con);
            Int32 countAvtoString = (Int32)(countAvto).ExecuteScalar();
            label8.Text = countAvtoString.ToString();

            string countAvtoRepairSelect = "Select Count (idAvto) From Avto WHERE StatusDostypa = 1 ";
            SqlCommand countAvtoRepair = new SqlCommand(countAvtoRepairSelect, con);
            Int32 countAvtoRepairString = (Int32)(countAvtoRepair).ExecuteScalar();
            label11.Text = countAvtoRepairString.ToString();

            string countUsersSelect = "Select Count (idUser) From AutDate ";
            SqlCommand countUsers = new SqlCommand(countUsersSelect, con);
            Int32 countUsersString = (Int32)(countUsers).ExecuteScalar();
            label14.Text = countUsersString.ToString();

            string countUsersPotrSelect = "Select Count (IdUser) From Polzovatel WHERE StatusPodtverzdeniya = 1 ";
            SqlCommand countUsersPotr = new SqlCommand(countUsersPotrSelect, con);
            Int32 countUsersPotrString = (Int32)(countUsersPotr).ExecuteScalar();
            label12.Text = countUsersPotrString.ToString();

            string timeOfTripSelect = "SELECT SUM(Dlitelnost) FROM Poezdka";
            SqlCommand timeOfTrip = new SqlCommand(timeOfTripSelect, con);
            Int32 timeOfTripInt = (Int32)(timeOfTrip).ExecuteScalar();
            Int32 medianTimeOfTrip = timeOfTripInt / countTripsString;
            var ts = TimeSpan.FromMinutes(Convert.ToDouble(timeOfTripInt));
            label16.Text = String.Format("{0} д. {1} ч. {2} м. ", ts.Days, ts.Hours, ts.Minutes);
            var ts2 = TimeSpan.FromMinutes(Convert.ToDouble(medianTimeOfTrip));
            label18.Text = String.Format("{0} д. {1} ч. {2} м. ", ts2.Days, ts2.Hours, ts2.Minutes);

            SqlDataAdapter sda = new SqlDataAdapter("select idAvto, count(*) as num from Poezdka group by idAvto order by count(*) desc", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            String idAvtoString = dt.Rows[0][0].ToString();
            Int32 idAvto = Convert.ToInt32(idAvtoString);

            string markaAvtoSelect = "Select Marka From Avto WHERE idAvto = '" + idAvto + " ' ";
            SqlCommand markaAvto = new SqlCommand(markaAvtoSelect, con);
            string markaAvtoString = (string)(markaAvto).ExecuteScalar();

            string nazvanieAvtoSelect = "Select Nazvanie From Avto WHERE idAvto = '" + idAvto + " ' ";
            SqlCommand nazvanieAvto = new SqlCommand(nazvanieAvtoSelect, con);
            string nazvanieAvtoString = (string)(nazvanieAvto).ExecuteScalar();

            string gosNomerAvtoSelect = "Select GosNomer From Avto WHERE idAvto = '" + idAvto + " ' ";
            SqlCommand gosNomerAvto = new SqlCommand(gosNomerAvtoSelect, con);
            string gosNomerAvtoString = (string)(gosNomerAvto).ExecuteScalar();
            label20.Text = markaAvtoString + ", " + nazvanieAvtoString + ", " + gosNomerAvtoString;

            String countOfTripsString = dt.Rows[0][1].ToString();
            label22.Text = countOfTripsString;


            SqlDataAdapter sda1 = new SqlDataAdapter("select idTarifa, count(*) as num from Poezdka group by idTarifa order by count(*) desc", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            String idTarifaString = dt1.Rows[0][0].ToString();
            Int32 idTarifa = Convert.ToInt32(idTarifaString);

            string nameOfTarifSelect = "Select Nazvanie From Tarif WHERE idTarifa = '" + idTarifa + " ' ";
            SqlCommand nameOfTarif = new SqlCommand(nameOfTarifSelect, con);
            string nameOfTarifString = (string)(nameOfTarif).ExecuteScalar();

            string typeOfTarifSelect = "Select TipTarifa From Tarif WHERE idTarifa = '" + idTarifa + " ' ";
            SqlCommand typeOfTarif = new SqlCommand(typeOfTarifSelect, con);
            string typeOfTarifString = (string)(typeOfTarif).ExecuteScalar();

            string timeOfTarifSelect = "Select VremyaTarifa From Tarif WHERE idTarifa = '" + idTarifa + " ' ";
            SqlCommand timeOfTarif = new SqlCommand(timeOfTarifSelect, con);
            string timeOfTarifString = (string)(timeOfTarif).ExecuteScalar();
            label24.Text = typeOfTarifString + ", " + timeOfTarifString + ", " + nameOfTarifString;

            String countOfTripsTarifString = dt1.Rows[0][1].ToString();
            label26.Text = countOfTripsTarifString;

            string countPovrSelect = "Select Count (idPovrezdeniya) From Povrezdeniya ";
            SqlCommand countPovr = new SqlCommand(countPovrSelect, con);
            Int32 countPovrString = (Int32)(countPovr).ExecuteScalar();
            label28.Text = countPovrString.ToString();
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void администраторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f6 = new Form6();
            f6.ShowDialog();
            Form5_Load(sender, e);

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
            Form5_Load(sender, e);
        }

        private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f10 = new Form10();
            f10.ShowDialog();
            Form5_Load(sender, e);
        }

        private void тарифыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f12 = new Form12();
            f12.ShowDialog();
            Form5_Load(sender, e);
        }

        private void классыАвтомобилейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f13 = new Form13();
            f13.ShowDialog();
            Form5_Load(sender, e);
        }

        private void новостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f14 = new Form14();
            f14.ShowDialog();
            Form5_Load(sender, e);
        }

        private void поездкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f18 = new Form18();
            f18.ShowDialog();
            Form5_Load(sender, e);
        }

        private void происшествияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f20 = new Form20();
            f20.ShowDialog();
            Form5_Load(sender, e);
        }


        private void поврежденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f21 = new Form21();
            f21.ShowDialog();
            Form5_Load(sender, e);
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
