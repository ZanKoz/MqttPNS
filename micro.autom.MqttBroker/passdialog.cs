using micro.autom.MqttBroker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace micro.autom.MqttBroker
{
    public partial class passdialog : Form
    {

        String Username;
        String Password;
        public passdialog()
        {
            InitializeComponent();
        }

        private void passdialog_Load(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            bool hasError = false;
            if (String.IsNullOrWhiteSpace(textBoxNewUsername.Text) || textBoxNewUsername.Text.Contains(":"))
            {

                errorProviderUser.SetError(textBoxNewUsername, Resources.resource_error_field);
                hasError = true;
            }
            else { errorProviderUser.Clear(); }
            if ((String.IsNullOrWhiteSpace(textBoxnewPass.Text))|| textBoxnewPass.Text.Contains(":"))
            {
                errorProviderpass.SetError(textBoxnewPass, Resources.resource_error_field);
                hasError = true;

            }
            else { errorProviderpass.Clear(); }
            if (hasError) {return; }
            Username = textBoxNewUsername.Text;
            Password = textBoxnewPass.Text;
            SaveCredentials(Username+":"+Password);
            this.DialogResult = DialogResult.OK;
        }

        private void SaveCredentials(String credentials)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");
            String fileName = "passws";
            String filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, credentials);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter($@"{path}\passws", true))
                {
                    sw.Write(credentials);
                    sw.Close();
                }
            }
            Process cipherProccess;
            bool IsRunning = false;
            try
            {
                cipherProccess = new Process
                {
                    StartInfo =
                        {
                            WorkingDirectory = path,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = Path.Combine(path,@"mosquitto_passwd.exe"),
                            Arguments = "-U passws",

                        },
                    EnableRaisingEvents = true
                };
                IsRunning = cipherProccess.Start();
            }
            catch (Exception)
            {
                cipherProccess = null;
                MessageBox.Show(this, Resources.resource_failed_contact_admin);
            }
            // Redirect the output stream of the child process.
            // p.StartInfo.Arguments = "/c DIR";
            if (cipherProccess != null)
            {
                if (IsRunning && !cipherProccess.HasExited)
                {
                    Settings.Default.BrokerUsername = Username;
                    Settings.Default.BrokerPassword = Password;
                    Settings.Default.Save();
                    cipherProccess.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, Resources.resource_failed_contact_admin);
                    cipherProccess.Kill();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
