using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using micro.autom.MqttBroker.Properties;

namespace micro.autom.MqttBroker
{
    public partial class FormBrokeLog : Form
    {
        private string _path;//Διαδρομή αρχείου  mosquitto.log
        public FormBrokeLog()
        {
            InitializeComponent();
        }

        /*
         * Προβολή του αρχείου καταγραφής του μεσίτη mosquitto
         */
        private async void FormBrokeLog_Load(object sender, EventArgs e)
        {
            _path = Path.Combine(Settings.Default.BrokerPath, @"mosquitto.log");
            textBoxBrokerLog.Clear();//Προετοιμασία κελίου κειμένου
            try
            {   // Ανοιγμα του αρχειου μέσω του streamReader
                using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    var line = await sr.ReadToEndAsync();//Ασύγχρονο διάβασμα αρχείου
                    textBoxBrokerLog.Text = line;//Καταχώρηση κειμένου στο κελί κειμένου
                    sr.Close();//Κλείσιμο streamReader
                }
            }
            catch (Exception ex)// Η ανάγνωση του αρχείου απέτυχε
            {
                textBoxBrokerLog.Text += Resources.FormBrokeLog_buttonRefreshLog_Click_Error;
                textBoxBrokerLog.Text += Environment.NewLine + (ex.Message);//Προβολή μυνήματος σφάλματος
            }
        }

        /*
         * Επαναφορτωση του αρχείου καταγραφής του μεσίτη mosquitto
         */
        private async void buttonRefreshLog_Click(object sender, EventArgs e)
        {
            _path = Path.Combine(Settings.Default.BrokerPath, @"mosquitto.log");
            textBoxBrokerLog.Clear();
            try
            {   // Open the text file using a stream reader.
                using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    var line = await sr.ReadToEndAsync();
                    textBoxBrokerLog.Text = line;
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                textBoxBrokerLog.Text += Resources.FormBrokeLog_buttonRefreshLog_Click_Error;
                textBoxBrokerLog.Text += Environment.NewLine + (ex.Message);
            }
        }
        /*
         * Κλείσιμο φόρμας
         */
        private void buttonCloseLog_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
