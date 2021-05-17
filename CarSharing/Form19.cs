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
    public partial class Form19 : Form
    {
        public SqlConnection con { get; set; }
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";
        Logger logger;
        CurrentMethod cm;
        public Form19()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            try
            {
                string v = cm.GetCurrentMethod();
                logger.Info(v);
                con = new SqlConnection(connectionString);
                con.Open();
                string idUserSelect = "SELECT idUser FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand idUser = new SqlCommand(idUserSelect, con);
                Int32 idUserInt = (Int32)(idUser).ExecuteScalar();

                string fioUserSelect = "SELECT Fio  FROM Polzovatel Where IdUser = '" + idUserInt + " '";
                SqlCommand fioUser = new SqlCommand(fioUserSelect, con);
                String fioUserString = (String)(fioUser).ExecuteScalar();
                label3.Text = fioUserString;

                string timeOfStartSelect = "SELECT TimeOfStart  FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand timeOfStart = new SqlCommand(timeOfStartSelect, con);
                DateTime timeOfStartString = (DateTime)(timeOfStart).ExecuteScalar();
                label4.Text = timeOfStartString.ToString();

                string timeOfEndSelect = "SELECT TimeOfEnd  FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand timeOfEnd = new SqlCommand(timeOfEndSelect, con);
                DateTime timeOfEndString = (DateTime)(timeOfEnd).ExecuteScalar();
                label6.Text = timeOfEndString.ToString();

                string timeOfTripSelect = "SELECT Dlitelnost  FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand timeOfTrip = new SqlCommand(timeOfTripSelect, con);
                Int32 timeOfTripInt = (Int32)(timeOfTrip).ExecuteScalar();
                var ts = TimeSpan.FromMinutes(Convert.ToDouble(timeOfTripInt));
                label8.Text = String.Format("{0} д. {1} ч. {2} м. ", ts.Days, ts.Hours, ts.Minutes);

                string costOfTripSelect = "SELECT Stoim  FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand costOfTrip = new SqlCommand(costOfTripSelect, con);
                Int32 costOfTripInt = (Int32)(costOfTrip).ExecuteScalar();
                label10.Text = Convert.ToString(costOfTripInt) + " р.";

                string idAvtoSelect = "SELECT idAvto FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand idAvto = new SqlCommand(idAvtoSelect, con);
                Int32 idAvtoInt = (Int32)(idAvto).ExecuteScalar();

                string markaAvtoSelect = "SELECT Marka  FROM Avto Where idAvto = '" + idAvtoInt + " '";
                SqlCommand markaAvto = new SqlCommand(markaAvtoSelect, con);
                String markaAvtoString = (String)(markaAvto).ExecuteScalar();

                string nameAvtoSelect = "SELECT Nazvanie  FROM Avto Where idAvto = '" + idAvtoInt + " '";
                SqlCommand nameAvto = new SqlCommand(nameAvtoSelect, con);
                String nameAvtoString = (String)(nameAvto).ExecuteScalar();
                label12.Text = markaAvtoString + " " + nameAvtoString;

                string idTarifSelect = "SELECT idTarifa FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                SqlCommand idTarif = new SqlCommand(idTarifSelect, con);
                Int32 idTarifInt = (Int32)(idTarif).ExecuteScalar();

                string nameOfTarifSelect = "SELECT Nazvanie  FROM Tarif Where idTarifa = '" + idTarifInt + " '";
                SqlCommand nameOfTarif = new SqlCommand(nameOfTarifSelect, con);
                String nameOfTarifString = (String)(nameOfTarif).ExecuteScalar();

                string typeOfTarifSelect = "SELECT TipTarifa  FROM Tarif Where idTarifa = '" + idTarifInt + " '";
                SqlCommand typeOfTarif = new SqlCommand(typeOfTarifSelect, con);
                String typeOfTarifString = (String)(typeOfTarif).ExecuteScalar();
                label14.Text = nameOfTarifString + ", " + typeOfTarifString;
                try
                {
                    string idProizSelect = "SELECT idProischestviya FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                    SqlCommand idProiz = new SqlCommand(idProizSelect, con);
                    Int32 idProizInt = (Int32)(idProiz).ExecuteScalar();

                    string opicanieProizSelect = "SELECT Opicanie  FROM Proishestviya Where idProischestviya = '" + idProizInt + " '";
                    SqlCommand opicanieProiz = new SqlCommand(opicanieProizSelect, con);
                    String opicanieProizString = (String)(opicanieProiz).ExecuteScalar();
                    label16.Text = opicanieProizString;
                    label16.ForeColor = Color.Red;
                }
                catch (Exception)
                {
                    label16.Text = "Отсутствуют";
                    label16.ForeColor = Color.Green;
                }
                try
                {
                    string otzyvSelect = "SELECT Otzyv  FROM Poezdka Where idPoezdki = '" + Program.getIdTrip + " '";
                    SqlCommand otzyv = new SqlCommand(otzyvSelect, con);
                    String otzyvString = (String)(otzyv).ExecuteScalar();
                    richTextBox1.Text = otzyvString;
                }
                catch (Exception)
                {
                    richTextBox1.Text = "Пользователь не оставил отзыв на эту поездку";
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
            string v = cm.GetCurrentMethod();
            logger.Info(v);
            this.Close();
        }
    }
}
