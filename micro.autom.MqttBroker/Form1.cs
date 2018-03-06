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

        private readonly string _defaultPath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");
        private MqttClient _watcherClient;
        private MqttCustomBroker _myMqttBroker;
        private bool _isNotLogging;
        private readonly RegistryKey _startupKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        bool isExitFromIcon = false;
        #endregion
         

        public MainForm()
        {
            InitializeComponent();

            txtboxBrokerOutput.ReadOnly = true;
            txtBoxSendMessage.ReadOnly = true;
            txtBoxSendTopic.ReadOnly = true;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (_startupKey.GetValue(Application.ProductName) == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                checkBox1.Checked = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                checkBox1.Checked = true;
            }

        }

        #region Mqtt Callbacks
        private void MyMqttBroker_OnBrokerExited(object source, MqttCustomBrokerEventArgs e)
        {
            ToogleStartButtonThreadSafe(true);
            WriteMainTextBoxThreadSafe("Broker Stopped" + Environment.NewLine);
        }

        private void MyMqttBroker_OnBrokerStarted(object source, MqttCustomBrokerEventArgs e)
        {

            ToogleStartButtonThreadSafe(false);
            WriteMainTextBoxThreadSafe("Broker Started" + Environment.NewLine + "Process: " + e.ProcessName + Environment.NewLine + "PID: " + e.ProcessId + Environment.NewLine);
            Task.Factory.StartNew(() => 
           {
               _watcherClient = new MqttClient("127.0.0.1");
               _watcherClient.MqttMsgPublishReceived += _watcherClient_MqttMsgPublishReceived;
               String clientid = Guid.NewGuid().ToString();
               _watcherClient.Connect(clientid, Settings.Default.BrokerUsername, Settings.Default.BrokerPassword);
               _watcherClient.Subscribe(new[] { "+/+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Subscribe(new[] { "+/+/+/+" }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
               _watcherClient.Publish("INFO", Encoding.UTF8.GetBytes("Watcher: Listening to All Topics"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
           });
        }

        private void _watcherClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
               WriteMainTextBoxThreadSafe(Environment.NewLine + "******New Message******" + Environment.NewLine + "Topic: " + e.Topic + Environment.NewLine + "Message: " + System.Text.Encoding.UTF8.GetString(e.Message) + Environment.NewLine);
            });
        }
        #endregion

        #region UI Events
        private void btnStartBroker_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings.Default.BrokerPath) && Directory.Exists(_defaultPath))
            {
                Settings.Default.BrokerPath = _defaultPath;
                Settings.Default.Save();
            }
            if ((string.IsNullOrWhiteSpace(Settings.Default.BrokerUsername) || string.IsNullOrWhiteSpace(Settings.Default.BrokerPassword)) || Settings.Default.FirstRun )
            {
                passdialog newPass = new passdialog();
                if (newPass.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }         
            _myMqttBroker = new MqttCustomBroker();
            _myMqttBroker.Start();
            _myMqttBroker.OnBrokerStarted += MyMqttBroker_OnBrokerStarted;
            _myMqttBroker.OnBrokerExited += MyMqttBroker_OnBrokerExited;
        }

           

  
        private void btnStopBroker_Click(object sender, EventArgs e)
        {
            if (_myMqttBroker == null) return;
            if (!_myMqttBroker.IsRunning)
            {
                MessageBox.Show(this, Resources.Message_BrokerInfo_Process_is_closed);
            }
            else
            {
                _myMqttBroker.NeedsKill = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExitFromIcon)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
            else {
                this.Hide();
                e.Cancel = true;
            }
            
    
        }

        private void btnSendBrokerMessage_Click(object sender, EventArgs e)
        {
            if (_watcherClient == null) return;
            if (!string.IsNullOrEmpty(txtBoxSendMessage.Text) && !string.IsNullOrEmpty(txtBoxSendTopic.Text) && _watcherClient.IsConnected)
            {
                _watcherClient.Publish(txtBoxSendTopic.Text, Encoding.UTF8.GetBytes(txtBoxSendMessage.Text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {

            txtboxBrokerOutput.Clear();

        }

        private void checkBoxStopLogging_CheckedChanged(object sender, EventArgs e)
        {
            _isNotLogging = checkBoxStopLogging.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Add the value in the registry so that the application runs at startup
                _startupKey.SetValue(Application.ProductName, '"' + Application.ExecutablePath + '"');
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                _startupKey.DeleteValue(Application.ProductName, false);
            }
        }

        private void setBrokerFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void openDetailedLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.BrokerPath) || Directory.Exists(Settings.Default.BrokerPath))
            {
                var formBrokeLog = new FormBrokeLog();
                formBrokeLog.Show();
            }
            else
            {
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

        void test() {

        }
        #endregion

        #region Helper Methods
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

        private void ToogleStartButtonThreadSafe(bool state)
        {
            if (InvokeRequired)
            {
                btnStartBroker.BeginInvoke((MethodInvoker)delegate
                {
                    btnStartBroker.Enabled = state;
                    txtBoxSendMessage.ReadOnly = state;
                    txtBoxSendTopic.ReadOnly = state;
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
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

        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        

        private void changeLoginCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

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
    }
}