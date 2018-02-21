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

        String Username;//Αρχικοποίηση ονόματος χρήστη μεσίτη
        String Password;//Αρχικοποίηση κωδικού πρόσβασης μεσίτη
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
            //Έλεγχος καταχώρησης Ονόματος χρήστη για  μην έγκυρες εγγραφές
            //Ο χαρακτήρας ':' απαγορεύετε καθώς αποτελεί το αναγνωριστικό της μεταβλητής κωδικού πρόσβασης
            if (String.IsNullOrWhiteSpace(textBoxNewUsername.Text) || textBoxNewUsername.Text.Contains(":"))
            {

                errorProviderUser.SetError(textBoxNewUsername, Resources.resource_error_field);//Ειδοποίηση χρήστη για μη έγκυρη καταχώρηση
                hasError = true;
            }
            else { errorProviderUser.Clear(); }
            if ((String.IsNullOrWhiteSpace(textBoxnewPass.Text)) || textBoxnewPass.Text.Contains(":"))
            {
                errorProviderpass.SetError(textBoxnewPass, Resources.resource_error_field);//Ειδοποίηση χρήστη για μη έγκυρη καταχώρηση
                hasError = true;

            }
            else { errorProviderpass.Clear(); }
            if (hasError) { return; }
            Username = textBoxNewUsername.Text;
            Password = textBoxnewPass.Text;
            SaveCredentials(Username + ":" + Password);//Αποθήκευση στοιχείων στο αρχείο κωδικών του μεσίτη mosquitto
            this.DialogResult = DialogResult.OK;
        }

        /*
         * Καταχώρηση Ονόματος χρήστη και κωδικού πρόσβασης, με τους οποίους θα μπορούν οι πελάτες
         * να συνδεθούν στον μεσίτη mosquitto
         */
        private void SaveCredentials(String credentials)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");//διαδρομή φακέλου mosquitto
            String fileName = "passws";
            String filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))//Αν υπάρχει το αρχείο κωδικών (passws)
            {
                //Τοτε θα αντικατασταθεί ο κωδικός πρόσβασης
                File.WriteAllText(filePath, credentials);
            }
            else//Αν δέν υπάρχει το αρχείο κωδικών
            {
                //Δημιουργία αρχείου κωδικών
                using (StreamWriter sw = new StreamWriter($@"{path}\passws", true))
                {
                    sw.Write(credentials);//Γράψιμο των κωδικών στο αρχείο
                    sw.Close();
                }
            }
            /*Οι κωδικοί δεν μπορούν να διαβαστούν από τον μεσίτη
             * mosquitto αν δεν ειναι κρυπτογραφημένοι σωστά
             * η διεργασία κρυπτογράφησης, υπάρχει στα αρχεία 
             * του μεσίτη mosquitto (mosquitto_passwd.exe)
             */
            Process cipherProccess;//Διεργασια κρυπτογράφισης κωδικων

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
            catch (Exception)// Η διεργασία κρυπτογράφισης απετυχε
            {
                cipherProccess = null;
                MessageBox.Show(this, Resources.resource_failed_contact_admin);
            }
            if (cipherProccess != null)
            {
                if (IsRunning && !cipherProccess.HasExited)//Η διεργασία κρυπτογράφισης πέτυχε
                {
                    Settings.Default.BrokerUsername = Username;//Αποθήκευση Ονόματος χρήστη στις παραμέτρους προγράμματος
                    Settings.Default.BrokerPassword = Password;// Αποθήκευση Κωδικού στις παραμέτρους προγράμματος
                    Settings.Default.Save();//Αποθήκευση παραμέτρων
                    cipherProccess.Dispose();
                    //Τα στοιχεία εισόδου άλλαξαν, πρέπει να γίνει επανεκκίνηση του μεσίτη
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
