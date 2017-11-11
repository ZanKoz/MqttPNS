namespace micro.autom.MqttBroker
{
    partial class FormBrokeLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBrokeLog));
            this.buttonCloseLog = new System.Windows.Forms.Button();
            this.textBoxBrokerLog = new System.Windows.Forms.TextBox();
            this.buttonRefreshLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCloseLog
            // 
            resources.ApplyResources(this.buttonCloseLog, "buttonCloseLog");
            this.buttonCloseLog.Name = "buttonCloseLog";
            this.buttonCloseLog.UseVisualStyleBackColor = true;
            this.buttonCloseLog.Click += new System.EventHandler(this.buttonCloseLog_Click);
            // 
            // textBoxBrokerLog
            // 
            resources.ApplyResources(this.textBoxBrokerLog, "textBoxBrokerLog");
            this.textBoxBrokerLog.Name = "textBoxBrokerLog";
            // 
            // buttonRefreshLog
            // 
            resources.ApplyResources(this.buttonRefreshLog, "buttonRefreshLog");
            this.buttonRefreshLog.Name = "buttonRefreshLog";
            this.buttonRefreshLog.UseVisualStyleBackColor = true;
            this.buttonRefreshLog.Click += new System.EventHandler(this.buttonRefreshLog_Click);
            // 
            // FormBrokeLog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRefreshLog);
            this.Controls.Add(this.textBoxBrokerLog);
            this.Controls.Add(this.buttonCloseLog);
            this.Name = "FormBrokeLog";
            this.Load += new System.EventHandler(this.FormBrokeLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCloseLog;
        private System.Windows.Forms.TextBox textBoxBrokerLog;
        private System.Windows.Forms.Button buttonRefreshLog;
    }
}