using Microsoft.Reporting.WinForms;
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
     
     

        public Form22()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            cm = new CurrentMethod();

            
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            var beginDate = dateTimePicker1.Value;
            var endDate = dateTimePicker2.Value;
            var myDateTable1 = new DiplomDataSet.MyReportDataTable();
            var myTableAdapter1 = new DiplomDataSetTableAdapters.MyReportTableAdapter();
            myTableAdapter1.Fill(myDateTable1, beginDate, endDate);
            var rds = new ReportDataSource("DataSet1", myDateTable1 as DataTable);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            Microsoft.Reporting.WinForms.ReportParameter[] pars = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("beg_date", beginDate.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("end_date", endDate.ToShortDateString()),
            };
            reportViewer1.RefreshReport();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

       
        
        private void Form22_Load(object sender, EventArgs e)
        {
            this.MyReportTableAdapter.Fill(this.DiplomDataSet.MyReport, dateTimePicker1.Value, dateTimePicker2.Value);


          this.reportViewer1.RefreshReport();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}

