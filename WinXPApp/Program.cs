using System;
using System.Windows.Forms;
using WinXPCore.Base;

namespace WinXPApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppLogger.InitializeLogger();
            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShellForm());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            NLog.LogManager.Shutdown();
        }
    }
}
