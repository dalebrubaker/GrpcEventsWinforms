namespace Client
{
    partial class FormClient
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
            this.btnSubscribeEvent = new System.Windows.Forms.Button();
            this.btnStopClient = new System.Windows.Forms.Button();
            this.btnStartClient = new System.Windows.Forms.Button();
            this.btnUnsubscribeEvent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logControl1
            // 
            this.logControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logControl1.HideTimestamps = false;
            this.logControl1.Location = new System.Drawing.Point(-1, 49);
            this.logControl1.MaximumLogLengthChars = 1048576;
            this.logControl1.Name = "logControl1";
            this.logControl1.Size = new System.Drawing.Size(845, 428);
            this.logControl1.TabIndex = 0;
            this.logControl1.Title = "Log";
            // 
            // btnSubscribeEvent
            // 
            this.btnSubscribeEvent.Location = new System.Drawing.Point(174, 12);
            this.btnSubscribeEvent.Name = "btnSubscribeEvent";
            this.btnSubscribeEvent.Size = new System.Drawing.Size(99, 23);
            this.btnSubscribeEvent.TabIndex = 7;
            this.btnSubscribeEvent.Text = "Subscribe Event";
            this.btnSubscribeEvent.UseVisualStyleBackColor = true;
            // 
            // btnStopClient
            // 
            this.btnStopClient.Location = new System.Drawing.Point(93, 12);
            this.btnStopClient.Name = "btnStopClient";
            this.btnStopClient.Size = new System.Drawing.Size(75, 23);
            this.btnStopClient.TabIndex = 6;
            this.btnStopClient.Text = "Stop Client";
            this.btnStopClient.UseVisualStyleBackColor = true;
            // 
            // btnStartClient
            // 
            this.btnStartClient.Location = new System.Drawing.Point(12, 12);
            this.btnStartClient.Name = "btnStartClient";
            this.btnStartClient.Size = new System.Drawing.Size(75, 23);
            this.btnStartClient.TabIndex = 5;
            this.btnStartClient.Text = "Start Client";
            this.btnStartClient.UseVisualStyleBackColor = true;
            // 
            // btnUnsubscribeEvent
            // 
            this.btnUnsubscribeEvent.Location = new System.Drawing.Point(279, 12);
            this.btnUnsubscribeEvent.Name = "btnUnsubscribeEvent";
            this.btnUnsubscribeEvent.Size = new System.Drawing.Size(108, 23);
            this.btnUnsubscribeEvent.TabIndex = 8;
            this.btnUnsubscribeEvent.Text = "Unsubscribe Event";
            this.btnUnsubscribeEvent.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Account Name:";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(480, 13);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(100, 20);
            this.txtAccountName.TabIndex = 10;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 478);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUnsubscribeEvent);
            this.Controls.Add(this.btnSubscribeEvent);
            this.Controls.Add(this.btnStopClient);
            this.Controls.Add(this.btnStartClient);
            this.Controls.Add(this.logControl1);
            this.Name = "FormClient";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.LogControl logControl1;
        private System.Windows.Forms.Button btnSubscribeEvent;
        private System.Windows.Forms.Button btnStopClient;
        private System.Windows.Forms.Button btnStartClient;
        private System.Windows.Forms.Button btnUnsubscribeEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccountName;
    }
}