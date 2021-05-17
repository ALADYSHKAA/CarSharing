using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSharing
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Debug("Start Programm");

            Application.Run(new Form1());
          

            //logger.Trace("trace message");
           // logger.Debug("debug message");
          //  logger.Info("info message");
          //  logger.Warn("warn message");
          //  logger.Error("error message");
          //  logger.Fatal("fatal message");
        }

        public static string serverName;
        public static string bdName;
        public static bool adminOrUser;
        public static string checkAdminKod;
        public static string checkAdminEmail;
        public static string checkAdminLogin;
        public static string insertValueAdmin;
        public static string checkAdminFio;
        public static string checkAdminPas;
        public static string getIdUser;
        public static bool fotoUser;
        public static bool connectionError;

        public static string getIdAvto;
        public static bool confirmUserOrNo;

        public static int idUser;
        public static int idTrip;
        public static int idAvto;
        public static string getIdTrip;



    }
}
