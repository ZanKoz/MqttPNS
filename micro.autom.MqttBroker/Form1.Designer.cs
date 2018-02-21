namespace micro.autom.MqttBroker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartBroker = new System.Windows.Forms.Button();
            this.btnStopBroker = new System.Windows.Forms.Button();
            this.txtboxBrokerOutput = new System.Windows.Forms.TextBox();
            this.txtBoxSendMessage = new System.Windows.Forms.TextBox();
            this.btnSendBrokerMessage = new System.Windows.Forms.Button();
            this.txtBoxSendTopic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.checkBoxStopLogging = new System.Windows.Forms.CheckBox();
            this.groupBoxClient = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkboxOnWindowsStart = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBrokerFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDetailedLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLoginCredentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipSend = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxcloudStatus = new System.Windows.Forms.PictureBox();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.labellang = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxClient.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxcloudStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStripNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartBroker
            // 
            this.btnStartBroker.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnStartBroker.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.btnStartBroker, "btnStartBroker");
            this.btnStartBroker.Name = "btnStartBroker";
            this.btnStartBroker.UseVisualStyleBackColor = false;
            this.btnStartBroker.Click += new System.EventHandler(this.btnStartBroker_Click);
            // 
            // btnStopBroker
            // 
            this.btnStopBroker.BackColor = System.Drawing.Color.IndianRed;
            this.btnStopBroker.FlatAppearance.BorderColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.btnStopBroker, "btnStopBroker");
            this.btnStopBroker.Name = "btnStopBroker";
            this.btnStopBroker.UseVisualStyleBackColor = false;
            this.btnStopBroker.Click += new System.EventHandler(this.btnStopBroker_Click);
            // 
            // txtboxBrokerOutput
            // 
            resources.ApplyResources(this.txtboxBrokerOutput, "txtboxBrokerOutput");
            this.txtboxBrokerOutput.Name = "txtboxBrokerOutput";
            // 
            // txtBoxSendMessage
            // 
            resources.ApplyResources(this.txtBoxSendMessage, "txtBoxSendMessage");
            this.txtBoxSendMessage.Name = "txtBoxSendMessage";
            // 
            // btnSendBrokerMessage
            // 
            this.btnSendBrokerMessage.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnSendBrokerMessage, "btnSendBrokerMessage");
            this.btnSendBrokerMessage.Name = "btnSendBrokerMessage";
            this.toolTipSend.SetToolTip(this.btnSendBrokerMessage, resources.GetString("btnSendBrokerMessage.ToolTip"));
            this.btnSendBrokerMessage.UseVisualStyleBackColor = false;
            this.btnSendBrokerMessage.Click += new System.EventHandler(this.btnSendBrokerMessage_Click);
            // 
            // txtBoxSendTopic
            // 
            resources.ApplyResources(this.txtBoxSendTopic, "txtBoxSendTopic");
            this.txtBoxSendTopic.Name = "txtBoxSendTopic";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonClearLog
            // 
            resources.ApplyResources(this.buttonClearLog, "buttonClearLog");
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // checkBoxStopLogging
            // 
            resources.ApplyResources(this.checkBoxStopLogging, "checkBoxStopLogging");
            this.checkBoxStopLogging.Name = "checkBoxStopLogging";
            this.checkBoxStopLogging.UseVisualStyleBackColor = true;
            this.checkBoxStopLogging.CheckedChanged += new System.EventHandler(this.checkBoxStopLogging_CheckedChanged);
            // 
            // groupBoxClient
            // 
            this.groupBoxClient.BackColor = System.Drawing.Color.LightBlue;
            this.groupBoxClient.Controls.Add(this.label6);
            this.groupBoxClient.Controls.Add(this.btnSendBrokerMessage);
            this.groupBoxClient.Controls.Add(this.txtBoxSendMessage);
            this.groupBoxClient.Controls.Add(this.label2);
            this.groupBoxClient.Controls.Add(this.txtBoxSendTopic);
            this.groupBoxClient.Controls.Add(this.checkboxOnWindowsStart);
            this.groupBoxClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.groupBoxClient, "groupBoxClient");
            this.groupBoxClient.Name = "groupBoxClient";
            this.groupBoxClient.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // checkboxOnWindowsStart
            // 
            resources.ApplyResources(this.checkboxOnWindowsStart, "checkboxOnWindowsStart");
            this.checkboxOnWindowsStart.Name = "checkboxOnWindowsStart";
            this.checkboxOnWindowsStart.UseVisualStyleBackColor = true;
            this.checkboxOnWindowsStart.CheckedChanged += new System.EventHandler(this.CheckboxOnWindowsStart_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setBrokerFolderToolStripMenuItem,
            this.openDetailedLogToolStripMenuItem,
            this.changeLoginCredentialsToolStripMenuItem});
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // setBrokerFolderToolStripMenuItem
            // 
            this.setBrokerFolderToolStripMenuItem.Name = "setBrokerFolderToolStripMenuItem";
            resources.ApplyResources(this.setBrokerFolderToolStripMenuItem, "setBrokerFolderToolStripMenuItem");
            this.setBrokerFolderToolStripMenuItem.Click += new System.EventHandler(this.setBrokerFolderToolStripMenuItem_Click);
            // 
            // openDetailedLogToolStripMenuItem
            // 
            this.openDetailedLogToolStripMenuItem.Name = "openDetailedLogToolStripMenuItem";
            resources.ApplyResources(this.openDetailedLogToolStripMenuItem, "openDetailedLogToolStripMenuItem");
            this.openDetailedLogToolStripMenuItem.Click += new System.EventHandler(this.openDetailedLogToolStripMenuItem_Click);
            // 
            // changeLoginCredentialsToolStripMenuItem
            // 
            this.changeLoginCredentialsToolStripMenuItem.Name = "changeLoginCredentialsToolStripMenuItem";
            resources.ApplyResources(this.changeLoginCredentialsToolStripMenuItem, "changeLoginCredentialsToolStripMenuItem");
            this.changeLoginCredentialsToolStripMenuItem.Click += new System.EventHandler(this.changeLoginCredentialsToolStripMenuItem_Click);
            // 
            // toolTipSend
            // 
            this.toolTipSend.AutomaticDelay = 4000;
            this.toolTipSend.IsBalloon = true;
            this.toolTipSend.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // pictureBoxcloudStatus
            // 
            this.pictureBoxcloudStatus.Image = global::micro.autom.MqttBroker.Properties.Resources.cloud_on;
            resources.ApplyResources(this.pictureBoxcloudStatus, "pictureBoxcloudStatus");
            this.pictureBoxcloudStatus.Name = "pictureBoxcloudStatus";
            this.pictureBoxcloudStatus.TabStop = false;
            // 
            // comboBoxLang
            // 
            this.comboBoxLang.DropDownHeight = 70;
            this.comboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLang.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLang, "comboBoxLang");
            this.comboBoxLang.Items.AddRange(new object[] {
            resources.GetString("comboBoxLang.Items"),
            resources.GetString("comboBoxLang.Items1")});
            this.comboBoxLang.Name = "comboBoxLang";
            this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.comboBoxLang_SelectedIndexChanged);
            // 
            // labellang
            // 
            resources.ApplyResources(this.labellang, "labellang");
            this.labellang.BackColor = System.Drawing.Color.LightBlue;
            this.labellang.Name = "labellang";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::micro.autom.MqttBroker.Properties.Resources.programm_picture_mikroPNS;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripNotify;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            // 
            // contextMenuStripNotify
            // 
            this.contextMenuStripNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStripNotify.Name = "contextMenuStripNotify";
            resources.ApplyResources(this.contextMenuStripNotify, "contextMenuStripNotify");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labellang);
            this.Controls.Add(this.comboBoxLang);
            this.Controls.Add(this.pictureBoxcloudStatus);
            this.Controls.Add(this.checkBoxStopLogging);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxBrokerOutput);
            this.Controls.Add(this.btnStopBroker);
            this.Controls.Add(this.btnStartBroker);
            this.Controls.Add(this.groupBoxClient);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxClient.ResumeLayout(false);
            this.groupBoxClient.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxcloudStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStripNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartBroker;
        private System.Windows.Forms.Button btnStopBroker;
        private System.Windows.Forms.TextBox txtboxBrokerOutput;
        private System.Windows.Forms.TextBox txtBoxSendMessage;
        private System.Windows.Forms.Button btnSendBrokerMessage;
        private System.Windows.Forms.TextBox txtBoxSendTopic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.CheckBox checkBoxStopLogging;
        private System.Windows.Forms.GroupBox groupBoxClient;
        private System.Windows.Forms.CheckBox checkboxOnWindowsStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBrokerFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDetailedLogToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTipSend;
        private System.Windows.Forms.PictureBox pictureBoxcloudStatus;
        private System.Windows.Forms.ComboBox comboBoxLang;
        private System.Windows.Forms.Label labellang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem changeLoginCredentialsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotify;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

