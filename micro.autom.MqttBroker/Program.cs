using System;
using System.Globalization;
using System.Windows.Forms;
using micro.autom.MqttBroker.Properties;

namespace micro.autom.MqttBroker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!string.IsNullOrEmpty(Settings.Default.SelectedCulture))
            {
                var cultureInfo = new CultureInfo(Settings.Default.SelectedCulture);
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                Application.CurrentCulture = cultureInfo;

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
           
        }
    }
}
