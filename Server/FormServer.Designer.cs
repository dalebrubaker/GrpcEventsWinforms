namespace Server
{
    partial class FormServer
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
            this.logControl1 = new Common.LogControl();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.btnSendEvent = new System.Windows.Forms.Button();
            this.cbxAcctNames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // logControl1
            // 
            this.logControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.logControl1.HideTimestamps = false;
            this.logControl1.Location = new System.Drawing.Point(2, 52);
            this.logControl1.MaximumLogLengthChars = 1048576;
            this.logControl1.Name = "logControl1";
            this.logControl1.Size = new System.Drawing.Size(847, 470);
            this.logControl1.TabIndex = 1;
            this.logControl1.Title = "Log";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(12, 12);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(93, 12);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(75, 23);
            this.btnStopServer.TabIndex = 3;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // btnSendEvent
            // 
            this.btnSendEvent.Location = new System.Drawing.Point(174, 12);
            this.btnSendEvent.Name = "btnSendEvent";
            this.btnSendEvent.Size = new System.Drawing.Size(75, 23);
            this.btnSendEvent.TabIndex = 4;
            this.btnSendEvent.Text = "Send Event";
            this.btnSendEvent.UseVisualStyleBackColor = true;
            this.btnSendEvent.Click += new System.EventHandler(this.btnSendEvent_Click);
            // 
            // cbxAcctNames
            // 
            this.cbxAcctNames.FormattingEnabled = true;
            this.cbxAcctNames.Location = new System.Drawing.Point(256, 13);
            this.cbxAcctNames.Name = "cbxAcctNames";
            this.cbxAcctNames.Size = new System.Drawing.Size(121, 21);
            this.cbxAcctNames.TabIndex = 5;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 523);
            this.Controls.Add(this.cbxAcctNames);
            this.Controls.Add(this.btnSendEvent);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.logControl1);
            this.Name = "FormServer";
            this.Text = "Server";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnSendEvent;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.ComboBox cbxAcctNames;
        private Common.LogControl logControl1;

        #endregion
    }
}

