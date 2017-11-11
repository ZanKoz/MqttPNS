namespace micro.autom.MqttBroker
{
    partial class passdialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(passdialog));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxnewPass = new System.Windows.Forms.TextBox();
            this.labelNewUsername = new System.Windows.Forms.Label();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.textBoxNewUsername = new System.Windows.Forms.TextBox();
            this.errorProviderpass = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderUser = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderpass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUser)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.errorProviderpass.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProviderUser.SetError(this.buttonOk, resources.GetString("buttonOk.Error1"));
            this.errorProviderpass.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProviderUser.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding1"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProviderpass.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProviderUser.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error1"));
            this.errorProviderpass.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProviderUser.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding1"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxnewPass
            // 
            resources.ApplyResources(this.textBoxnewPass, "textBoxnewPass");
            this.errorProviderpass.SetError(this.textBoxnewPass, resources.GetString("textBoxnewPass.Error"));
            this.errorProviderUser.SetError(this.textBoxnewPass, resources.GetString("textBoxnewPass.Error1"));
            this.errorProviderUser.SetIconAlignment(this.textBoxnewPass, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxnewPass.IconAlignment"))));
            this.errorProviderpass.SetIconAlignment(this.textBoxnewPass, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxnewPass.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.textBoxnewPass, ((int)(resources.GetObject("textBoxnewPass.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.textBoxnewPass, ((int)(resources.GetObject("textBoxnewPass.IconPadding1"))));
            this.textBoxnewPass.Name = "textBoxnewPass";
            this.textBoxnewPass.UseSystemPasswordChar = true;
            // 
            // labelNewUsername
            // 
            resources.ApplyResources(this.labelNewUsername, "labelNewUsername");
            this.errorProviderpass.SetError(this.labelNewUsername, resources.GetString("labelNewUsername.Error"));
            this.errorProviderUser.SetError(this.labelNewUsername, resources.GetString("labelNewUsername.Error1"));
            this.errorProviderUser.SetIconAlignment(this.labelNewUsername, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNewUsername.IconAlignment"))));
            this.errorProviderpass.SetIconAlignment(this.labelNewUsername, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNewUsername.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.labelNewUsername, ((int)(resources.GetObject("labelNewUsername.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.labelNewUsername, ((int)(resources.GetObject("labelNewUsername.IconPadding1"))));
            this.labelNewUsername.Name = "labelNewUsername";
            // 
            // labelNewPassword
            // 
            resources.ApplyResources(this.labelNewPassword, "labelNewPassword");
            this.errorProviderpass.SetError(this.labelNewPassword, resources.GetString("labelNewPassword.Error"));
            this.errorProviderUser.SetError(this.labelNewPassword, resources.GetString("labelNewPassword.Error1"));
            this.errorProviderUser.SetIconAlignment(this.labelNewPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNewPassword.IconAlignment"))));
            this.errorProviderpass.SetIconAlignment(this.labelNewPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNewPassword.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.labelNewPassword, ((int)(resources.GetObject("labelNewPassword.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.labelNewPassword, ((int)(resources.GetObject("labelNewPassword.IconPadding1"))));
            this.labelNewPassword.Name = "labelNewPassword";
            // 
            // textBoxNewUsername
            // 
            resources.ApplyResources(this.textBoxNewUsername, "textBoxNewUsername");
            this.errorProviderpass.SetError(this.textBoxNewUsername, resources.GetString("textBoxNewUsername.Error"));
            this.errorProviderUser.SetError(this.textBoxNewUsername, resources.GetString("textBoxNewUsername.Error1"));
            this.errorProviderUser.SetIconAlignment(this.textBoxNewUsername, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxNewUsername.IconAlignment"))));
            this.errorProviderpass.SetIconAlignment(this.textBoxNewUsername, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxNewUsername.IconAlignment1"))));
            this.errorProviderUser.SetIconPadding(this.textBoxNewUsername, ((int)(resources.GetObject("textBoxNewUsername.IconPadding"))));
            this.errorProviderpass.SetIconPadding(this.textBoxNewUsername, ((int)(resources.GetObject("textBoxNewUsername.IconPadding1"))));
            this.textBoxNewUsername.Name = "textBoxNewUsername";
            // 
            // errorProviderpass
            // 
            this.errorProviderpass.ContainerControl = this;
            resources.ApplyResources(this.errorProviderpass, "errorProviderpass");
            // 
            // errorProviderUser
            // 
            this.errorProviderUser.ContainerControl = this;
            resources.ApplyResources(this.errorProviderUser, "errorProviderUser");
            // 
            // passdialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxNewUsername);
            this.Controls.Add(this.labelNewPassword);
            this.Controls.Add(this.labelNewUsername);
            this.Controls.Add(this.textBoxnewPass);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "passdialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.passdialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderpass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxnewPass;
        private System.Windows.Forms.Label labelNewUsername;
        private System.Windows.Forms.Label labelNewPassword;
        private System.Windows.Forms.TextBox textBoxNewUsername;
        private System.Windows.Forms.ErrorProvider errorProviderpass;
        private System.Windows.Forms.ErrorProvider errorProviderUser;
    }
}