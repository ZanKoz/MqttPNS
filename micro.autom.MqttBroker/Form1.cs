using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using micro.autom.MqttBroker.Properties;
using Microsoft.Win32;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace micro.autom.MqttBroker
{

    public partial class MainForm : Form
    {
        #region Variables
        //Αρχικοποίηση μεταβλητών
        private readonly string _defaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");
        private MqttClient _watcherClient;
        private MqttCustomBroker _myMqttBroker;
        private bool _isNotLogging;
        private readonly RegistryKey _startupKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        bool isExitFromIcon = false;
        #endregion

        //Εναρξη κεντρική Φορμας Form1
        public MainForm()
        {
            InitializeComponent();
            //Aπενεργοποίηση εργαλειών αφού δεν έχει ξεκινήσει ο mosquitto broker
            txtboxBrokerOutput.ReadOnly = true;
            txtBoxSendMessage.ReadOnly = true;
            txtBoxSendTopic.ReadOnly = true;
            //Ελεγχος αν υπάρχει "Εναρξη κατα την εκίννηση" στην Registry
            if (_startupKey.GetValue(Application.ProductName) == null)
            {
                checkboxOnWindowsStart.Checked = false;
            }
            else
            {
                checkboxOnWindowsStart.Checked = true;
                //Αν έγινε έναρξη κατα την εκκίνηση των windows προσπάθησε να ξεκινήσεις τον broker
                btnStartBroker.PerformClick();
            }

        }

        //Γεγονότα MQTT
        #region Mqtt Callbacks
        //Mέθοδος που καλείτε αν ο Broker σταματήσει να εκτελείται
        private void MyMqttBroker_OnBrokerExited(object source, MqttCustomBrokerEventArgs e)
        {
            ToogleStartButtonThreadSafe(true);
            WriteMainTextBoxThreadSafe("Broker Stopped" + Environment.NewLine);
        }

        //Mέθοδος που καλείτε αν ο Broker έχει ξεκινήσει επιτυχώς
        private void MyMqttBroker_OnBrokerStarted(object source, MqttCustomBrokerEventArgs e)
        {

            ToogleStartButtonThreadSafe(false);
            //Εμφάνησε στο κεντρικό κελί κειμένου μια ειδοποίηση
            WriteMainTextBoxThreadSafe("Broker Started" + Environment.NewLine + "Process: " + e.ProcessName + Environment.NewLine + "PID: " + e.ProcessId + Environment.NewLine);
            //O πελάτης πρέπει να δημιουργηθεί απο ένα αλλο thread ωστε να μην παγώνει το κυρίως προγραμμα
            Task.Factory.StartNew(() =>
           {
               //Καταχώτρηση στοιχείων σύνδεσης στον αντικείμενο πελάτη _watcherClient
               _watcherClient = new MqttClient("127.0.0.1");
               _watcherClient.MqttMsgPublishReceived += _watcherClient_MqttMsgPublishReceived;
               //Δημιουργία τυχαίου μοναδικού ID
               String clientid = Guid.NewGuid().ToString();
               //Προσπαθεια σύνδεσης στον Broker
               _watcherClient.Connect(clientid, Settings.Default.BrokerUsername, Settings.Default.BrokerPassword);
               //Εγγραφή σε όλα τα θέματα μέχρι 4 επίπεδα
               _watcherClient.Subscribe(new[] { "+/+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+/+/+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               //Ειδοποίηση επιτυχής δημιουργίας πελάτη παρακολουθητή
               _watcherClient.Publish("INFO", Encoding.UTF8.GetBytes("Watcher: Listening to All Topics"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
           });
        }
        //Μέθοδος που καλείτε όταν έρθει μήνυμα
        private void _watcherClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Το μήνυμα και το θέμα βρίσκονται στην μεταβλητή e
            //Ο πελάτης εκτελείτε απο άλλο thread οπότε το γράψιμο εισερχομένου μηνύματος στο κεντρικό κελί κειμένου πρέπει να γίνει μέσω Task και Invoker
            Task.Factory.StartNew(() =>
            {
                WriteMainTextBoxThreadSafe(Environment.NewLine + "**New Message**" + Environment.NewLine + "Topic: " + e.Topic + Environment.NewLine + "Message: " + System.Text.Encoding.UTF8.GetString(e.Message) + Environment.NewLine);
            });
        }
        #endregion

        //Γεγονότα εργαλείων της φόρμας Form1 
        #region UI Events
        //Πάτημα κουμπιού Start
        private void btnStartBroker_Click(object sender, EventArgs e)
        {
            //Ελεγχος αν ΔΕΝ υπάρχει ήδη αποθηκευμένη διαδρομή του φάκελου service στις ρυθμίσεις προγράμματος
            if (string.IsNullOrEmpty(Settings.Default.BrokerPath) && Directory.Exists(_defaultPath))
            {
                Settings.Default.BrokerPath = _defaultPath;
                //Αποθήκευση της προεπιλεγμένης διαδρομής στις ρυθμίσεις της εφαρμογης
                Settings.Default.Save();
            }
            //Ελεγχος αν ΔΕΝ υπάρχει αποθηκευμένος κωδικός και όνομα χρήστη στις ρυθμίσεις προγράμματος
            if ((string.IsNullOrWhiteSpace(Settings.Default.BrokerUsername) || string.IsNullOrWhiteSpace(Settings.Default.BrokerPassword)) || Settings.Default.FirstRun)
            {
                //Νέο παράθυρο για καινούργιο κωδικό
                passdialog newPass = new passdialog();
                if (newPass.ShowDialog() != DialogResult.OK) { return; }
                // η μεταβλητή First Run αποτρέπει το σφάλμα που δημιουργείτε οταν η εφαρμογή ανοίξει για πρώτη φορα και δεν υπάρχει κάν αρχειο κωδικων
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
            _myMqttBroker = new MqttCustomBroker();
            //Εκκίνηση κλάσης Broker για την εκτέλεση της εφαρμογής mosquitto
            _myMqttBroker.Start();
            //Εγγραφη στα γεγονότα εκτέλεσης και τερματισμου του μεσίτη mosquitto
            _myMqttBroker.OnBrokerStarted += MyMqttBroker_OnBrokerStarted;
            _myMqttBroker.OnBrokerExited += MyMqttBroker_OnBrokerExited;
        }



        //Πάτημα κουμπιού Stop
        private void btnStopBroker_Click(object sender, EventArgs e)
        {
            //Αν η εφαρμογη  mosquitto δεν εκτελείτε
            if (_myMqttBroker == null) return;
            if (!_myMqttBroker.IsRunning)
            {
                //Ειδοποιησε οτι δέν ξεκίνησε ποτέ
                MessageBox.Show(this, Resources.Message_BrokerInfo_Process_is_closed);
            }
            else
            {
                //Δεν γίνεται να τερματίσουμε την εφαρμογή απο εδώ γιατι τρέχει σε άλλο thread
                //Τερματισμός της εφαρμογής mosquitto μέσα απο την κλάση MqttCustomBroker
                _myMqttBroker.NeedsKill = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //Μεθοδος που καλειτε με το κλείσιμο της φόρμας
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Αν τερματιστηκε απο το εικονίδιο τότε κανονικός τερματισμός
            if (isExitFromIcon)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
            //Αλλιώς δημιουργησε εικονίδιο και μήν τερματίζεις
            else
            {
                this.Hide();
                e.Cancel = true;
            }


        }
        //Πάτημα κουμπιού Αποστολη μυνήματος
        private void btnSendBrokerMessage_Click(object sender, EventArgs e)
        {
            if (_watcherClient == null) return;
            //Ελεγχος αν τα κελια "SendMessage" και "SendTopic" και αν το αντικείμενο πελάτη _watcherClient ειναι έγκυρα
            if (!string.IsNullOrEmpty(txtBoxSendMessage.Text) && !string.IsNullOrEmpty(txtBoxSendTopic.Text) && _watcherClient.IsConnected)
            {
                //Αποστολή μηνύματος που υπάρχει στο κελί "SendMessage" μέσω του πελάτη παρακολουθητή
                _watcherClient.Publish(txtBoxSendTopic.Text, Encoding.UTF8.GetBytes(txtBoxSendMessage.Text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            //Εκαθάρηση κελιού μηνυμάτων
            txtboxBrokerOutput.Clear();

        }

        private void checkBoxStopLogging_CheckedChanged(object sender, EventArgs e)
        {
            //Σταμάτημα καταγραφής μηνυμάτων
            _isNotLogging = checkBoxStopLogging.Checked;
        }

        //Επιλογή εναρξης κατα την εκκίνηση
        private void CheckboxOnWindowsStart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxOnWindowsStart.Checked)
            {
                //Δημιουργία τιμής στην Registry ωστε να ξεκινάει η εφαρμογή κατα την εκίννηση
                _startupKey.SetValue(Application.ProductName, '"' + Application.ExecutablePath + '"');
            }
            else
            {
                _startupKey.DeleteValue(Application.ProductName, false);
            }
        }
        //Πάτημα Ενεργειας: "Επιλογή Φακέλου Broker"
        private void setBrokerFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Επιλογή διαδρομής φακέλου mosquitto broker
            var folderdDialog = new FolderBrowserDialog
            {
                SelectedPath = Directory.GetCurrentDirectory(),
                Description = Resources.MainForm_setBrokerFolderToolStripMenuItem_Click_Message
            };
            if (folderdDialog.ShowDialog() == DialogResult.OK)
            {
                //Αποθηκευση τιμής απο το FolderBrowserDialog
                Settings.Default.BrokerPath = folderdDialog.SelectedPath;
                Settings.Default.Save();
            }
        }

        //Πάτημα Ενεργειας: "Πληροφορίες Υπηρεσίας"
        private void openDetailedLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Ανοιγμα αρχείου καταγραφής mosquitto.log
            if (!string.IsNullOrEmpty(Settings.Default.BrokerPath) || Directory.Exists(Settings.Default.BrokerPath))
            {
                //Εμφάνιση του παραθύρου FormBrokerLog
                var formBrokeLog = new FormBrokeLog();
                formBrokeLog.Show();
            }
            else
            {
                //Αν το αρχείο δεν βρεθεί ο χρήστης πρέπει να επιλεξει έναν φάκελο που περιέχει το αρχείο mosquitto.log 
                var folderdDialog = new FolderBrowserDialog
                {
                    SelectedPath = Directory.GetCurrentDirectory(),
                    Description = Resources.MainForm_setBrokerFolderToolStripMenuItem_Click_Message
                };
                if (folderdDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.BrokerPath = folderdDialog.SelectedPath;
                    Settings.Default.Save();
                }
            }

        }


        //Πάτημα Ενεργειας: "Αλλαγή στοιχείων σύνδεσης"
        private void changeLoginCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Μην επιτρέψεις την αλλαγή αν ο broker τρέχει ήδη
            var window = MessageBox.Show(this, Resources.resource_change_pass_dialog_info, Resources.resource_header_passdialog_warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (!(window == DialogResult.Yes)) { return; }
            if (_myMqttBroker != null)
            {
                if (_myMqttBroker.IsRunning)
                {
                    _myMqttBroker.NeedsKill = true;
                }
            }
            passdialog mPassdialog = new passdialog();
            mPassdialog.Show(this);
        }

        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Επιλογή γλώσσας και αποθήκευση στις ρυθμίσεις εφαρμογής
            if (comboBoxLang.SelectedItem.Equals("English"))
            {
                Settings.Default.SelectedCulture = "en-US";
                Settings.Default.Save();
                Application.Exit();

            }
            else if (comboBoxLang.SelectedItem.Equals("Ελληνικά"))
            {
                Settings.Default.SelectedCulture = "el-GR";
                Settings.Default.Save();
                Application.Exit();
            }


        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        //Εξοδος μέσω του εικονιδίου ειδοποιήσεων
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var window = MessageBox.Show(this, Resources.MainForm_FormClosing_Message, Resources.MainForm_Form1_FormClosing_Button, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (window == DialogResult.Yes)
            {
                isExitFromIcon = true;
                if (_myMqttBroker != null)
                {
                    if (_myMqttBroker.IsRunning)
                    {
                        _myMqttBroker.NeedsKill = true;
                    }
                }
                Application.Exit();
            }
            else
            {
                return;
            }

        }

        #endregion

        //Βοηθητικές μέθοδοι
        #region Helper Methods
        // Μέθοδος που επιτρέπει την γραφή κειμένου στο κεντρικό κελί απο οποιοδήποτε thread μεσω invoker
        private void WriteMainTextBoxThreadSafe(string text)
        {
            if (InvokeRequired)
            {
                if (_isNotLogging)
                {

                }
                else
                {
                    txtboxBrokerOutput.BeginInvoke((MethodInvoker)delegate
                    {
                        txtboxBrokerOutput.AppendText(text);
                    });
                }
                return;
            }
            txtboxBrokerOutput.AppendText(text);
        }
        //Αλλαγή της οψής των κουμπιών Start και Stop απο οποιοδήποτε thread μέσω invoker
        private void ToogleStartButtonThreadSafe(bool state)
        {
            if (InvokeRequired)
            {
                btnStartBroker.BeginInvoke((MethodInvoker)delegate
                {
                    btnStartBroker.Enabled = state;
                    txtBoxSendMessage.ReadOnly = state;
                    txtBoxSendTopic.ReadOnly = state;
                    if (state)
                    {
                        btnStartBroker.BackColor =
                            Color.MediumSeaGreen;
                    }
                    else
                    {
                        btnStartBroker.BackColor = Color.Gray;
                    }
                });
                pictureBoxcloudStatus.BeginInvoke((MethodInvoker)delegate
               {
                   pictureBoxcloudStatus.Image = !state ? Resources.cloud_off : Resources.cloud_on;
               });
                return;
            }
            pictureBoxcloudStatus.Image = !state ? Resources.cloud_off : Resources.cloud_on;
            btnStartBroker.Enabled = state;
            txtBoxSendMessage.ReadOnly = state;
            txtBoxSendTopic.ReadOnly = state;
        }


        #endregion



    }
}