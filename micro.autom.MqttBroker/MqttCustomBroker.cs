using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using micro.autom.MqttBroker.Properties;

/**
 * Η κλάση αυτή αντιπροσωπεύει ενα αντικείμενο MQTTBroker
 * στην πραγματικότητα αποτελεί την δίαυλο έναρξης και τερματισμού του μεσίτη mosquitto
 */
namespace micro.autom.MqttBroker
{
    public delegate void MqttCustomBrokerEventHandler(object source, MqttCustomBrokerEventArgs e);

    internal class MqttCustomBroker
    {

        #region PROPERTIES
        public bool IsRunning { get; internal set; }//Ο Μεσίτης Mosquitto  είναι ενεργός
        public bool NeedsKill { get; set; }//Σημα τερματισμού μεσίτη απο εξωτερικές κλάσεις
        public bool IsStarted { get; internal set; }//Η διαδικασία έναρξης του μεσίτη έχει ξεκινήσει
        public Process BrokerProcess { get; set; }//Διεργασια Mosquitto.exe
        #endregion

        public event MqttCustomBrokerEventHandler OnBrokerStarted;//Συμβάν που εκτελείτε όταν ξεκινήσει ο μεσίτης
        public event MqttCustomBrokerEventHandler OnBrokerExited;//Συμβάν που εκτελείτε όταν σταματήσει ο μεσίτης


        /* MqttCustomBroker
         *Αρχικοποίηση κλάσης
         */
        public MqttCustomBroker()
        {
            IsRunning = false;
            IsStarted = false;
            NeedsKill = false;
        }

        /* Start
         *Καλείτε για βρεθεί η διεργασία mosquitto.exe
         * στην συνέχεια εκτελεί την μέθοδο DoRunBroker για να εκτελεστεί η διεργασία
         */
        public void Start()
        {
            if (string.IsNullOrEmpty(Settings.Default.BrokerPath) || !Directory.Exists(Settings.Default.BrokerPath))
            {
                var folderdDialog = new FolderBrowserDialog
                {
                    //Αν δεν βρεθεί το αρχείο mosquitto.exe ο χρήστης καλείτε να επιλέξει την τοποθεσία του
                    SelectedPath = Directory.GetCurrentDirectory(),
                    Description = Resources.MqttCustomBroker_Start_folder_Dialog_Message
                };
                if (folderdDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.BrokerPath = folderdDialog.SelectedPath;
                    Settings.Default.Save();
                }

            }
            // Η έναρξη του mosquitto.exe πρέπει να γίνει σε ενα καινούργιο thread
            var brokerThread = new Thread(DoRunBroker);
            brokerThread.Start();
        }

        private void DoRunBroker()
        {

            do
            {
                if (IsRunning)//O Μεσίτης εκτελείται
                {
                    if (NeedsKill)//Δόθηκε εντολή τερματισμού του Μεσίτη
                    {
                        if (!BrokerProcess.HasExited)//Έχουμε το αντικείμενο της διεργασίας
                        {
                            var name = BrokerProcess.ProcessName;//Όνομα διεργασίας (mosquitto.exe)
                            var id = BrokerProcess.Id;//Αναγνωριστικό διεργασίας (PID)
                            BrokerProcess.CloseMainWindow();
                            BrokerProcess.Kill();//Τερματισμός διεργασίας mosquitto.exe
                            IsRunning = false;
                            OnBrokerExited?.Invoke(this, new MqttCustomBrokerEventArgs(name, id, false));
                        }
                        else//Δεν έχουμε το αντικείμενο της διεργασίας
                        {
                            //Αναζήτηση της διεργασίας με βάση το όνομα της (mosquitto.exe)
                            foreach (var process in Process.GetProcessesByName("mosquitto"))
                            {
                                process.Kill();//Τερματισμός διεργασίας mosquitto.exe
                                IsRunning = false;
                                //Εκτέλεση γεγονότος: Ο μεσίτης τερματίστηκε
                                OnBrokerExited?.Invoke(this, new MqttCustomBrokerEventArgs(process.ProcessName, process.Id, false));
                            }


                        }
                    }
                }
                else
                {
                    string pathstr = Settings.Default.BrokerPath;//Θέση αρχείου mosquitto.exe
                    SpinWait.SpinUntil(NotFileinUse);//Αν το αρχείο χρησιμοποιείται , περίμενε
                    try
                    {
                        BrokerProcess = new Process //Δημιουργία αντικειμένου διεργασίας mosquitto.exe
                        {
                            StartInfo =
                        {
                            WorkingDirectory = pathstr,
                            UseShellExecute = false,
                            CreateNoWindow = true,//Διεργασία χωρίς παράθυρο
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = Path.Combine(pathstr,@"mosquitto.exe"),
                            //Ο μεσίτης mosquitto θα πρέπει να ξεκινήσει με τις ρυθμίσεις που υπάρχουν στο αρχείο mosqitto.conf
                            Arguments = "-c mosquitto.conf",

                        },
                            EnableRaisingEvents = true
                        };

                        IsRunning = BrokerProcess.Start();//Εκτέλεση διεργασίας
                    }
                    catch (Exception)
                    {
                        Settings.Default.BrokerPath = null;//Η εκτέλεση απέτυχε
                        BrokerProcess = null;
                    }
                    if (BrokerProcess != null)
                    {
                        if (IsRunning && !BrokerProcess.HasExited)//Η εκτέλεση πέτυχε
                        {
                            OnBrokerStarted?.Invoke(this, new MqttCustomBrokerEventArgs(BrokerProcess.ProcessName, BrokerProcess.Id, BrokerProcess.Responding));
                        }
                    }

                }
            } while (IsRunning);
        }

        /*
         * Έλεγχος αν το αρχείο κωδικών passws χρησιμοποιείται
         */
        protected virtual bool NotFileinUse()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");
            FileInfo file = new FileInfo($@"{path}\passws");
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return false;//Το αρχείο χρησιμοποιείται
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return true;//Το αρχείο δεν χρησιμοποιείται
        }

    }


    /*Μέθοδοι και ιδιότητες γεγονότων που χρησιμοποιούνται από την κλάση
     * mqttcustombroker
     */
    public class MqttCustomBrokerEventArgs : EventArgs
    {
        public string ProcessName { get; internal set; }//Όνομα διεργασίας mosquitto
        public int ProcessId { get; internal set; }//Id διεργασίας mosquitto
        public bool IsProcessResponding { get; internal set; }//True, αν η διεργασία ανταποκρίνεται
        public MqttCustomBrokerEventArgs(string mtextProcessName, int mProcessId, bool mProcessResponding)
        {
            ProcessName = mtextProcessName;
            ProcessId = mProcessId;
            IsProcessResponding = mProcessResponding;
        }

    }
}
