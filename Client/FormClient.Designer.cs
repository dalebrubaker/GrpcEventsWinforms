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
            this.btnUnsubscribeEvent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.btnCrash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logControl1
            // 
            this.logControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnSubscribeEvent.Location = new System.Drawing.Point(170, 8);
            this.btnSubscribeEvent.Name = "btnSubscribeEvent";
            this.btnSubscribeEvent.Size = new System.Drawing.Size(99, 23);
            this.btnSubscribeEvent.TabIndex = 7;
            this.btnSubscribeEvent.Text = "Subscribe Event";
            this.btnSubscribeEvent.UseVisualStyleBackColor = true;
            this.btnSubscribeEvent.Click += new System.EventHandler(this.btnSubscribeEvent_Click);
            // 
            // btnUnsubscribeEvent
            // 
            this.btnUnsubscribeEvent.Location = new System.Drawing.Point(275, 8);
            this.btnUnsubscribeEvent.Name = "btnUnsubscribeEvent";
            this.btnUnsubscribeEvent.Size = new System.Drawing.Size(108, 23);
            this.btnUnsubscribeEvent.TabIndex = 8;
            this.btnUnsubscribeEvent.Text = "Unsubscribe Event";
            this.btnUnsubscribeEvent.UseVisualStyleBackColor = true;
            this.btnUnsubscribeEvent.Click += new System.EventHandler(this.btnUnsubscribeEvent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Account Name:";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(99, 10);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(65, 20);
            this.txtAccountName.TabIndex = 10;
            // 
            // btnCrash
            // 
            this.btnCrash.Location = new System.Drawing.Point(389, 7);
            this.btnCrash.Name = "btnCrash";
            this.btnCrash.Size = new System.Drawing.Size(75, 23);
            this.btnCrash.TabIndex = 11;
            this.btnCrash.Text = "Crash";
            this.btnCrash.UseVisualStyleBackColor = true;
            this.btnCrash.Click += new System.EventHandler(this.btnCrash_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 478);
            this.Controls.Add(this.btnCrash);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUnsubscribeEvent);
            this.Controls.Add(this.btnSubscribeEvent);
            this.Controls.Add(this.logControl1);
            this.Name = "FormClient";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClient_FormClosing);
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnCrash;
        private System.Windows.Forms.Button btnSubscribeEvent;
        private System.Windows.Forms.Button btnUnsubscribeEvent;
        private System.Windows.Forms.Label label1;
        private Common.LogControl logControl1;
        private System.Windows.Forms.TextBox txtAccountName;

        #endregion
    }
}