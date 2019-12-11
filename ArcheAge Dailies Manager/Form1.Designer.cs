namespace ArcheAge_Dailies_Manager
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.questDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.questListBox = new System.Windows.Forms.ListBox();
            this.findAAButton = new System.Windows.Forms.Button();
            this.AAFoundLabel = new System.Windows.Forms.Label();
            this.questLocationPictureBox = new System.Windows.Forms.PictureBox();
            this.manualReceiveHandInButton = new System.Windows.Forms.Button();
            this.chatboxConfigButton = new System.Windows.Forms.Button();
            this.resetSelectedButton = new System.Windows.Forms.Button();
            this.resetAllButton = new System.Windows.Forms.Button();
            this.x1TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.y1TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.y2TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.x2TextBox = new System.Windows.Forms.TextBox();
            this.resetTimer = new System.Windows.Forms.Timer(this.components);
            this.screenCaptureTimer = new System.Windows.Forms.Timer(this.components);
            this.captureStartButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.questLocationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // questDescriptionTextBox
            // 
            this.questDescriptionTextBox.Location = new System.Drawing.Point(343, 12);
            this.questDescriptionTextBox.Multiline = true;
            this.questDescriptionTextBox.Name = "questDescriptionTextBox";
            this.questDescriptionTextBox.ReadOnly = true;
            this.questDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.questDescriptionTextBox.Size = new System.Drawing.Size(293, 88);
            this.questDescriptionTextBox.TabIndex = 0;
            // 
            // questListBox
            // 
            this.questListBox.FormattingEnabled = true;
            this.questListBox.Location = new System.Drawing.Point(12, 12);
            this.questListBox.Name = "questListBox";
            this.questListBox.Size = new System.Drawing.Size(325, 472);
            this.questListBox.TabIndex = 1;
            this.questListBox.SelectedIndexChanged += new System.EventHandler(this.questListBox_SelectedIndexChanged);
            // 
            // findAAButton
            // 
            this.findAAButton.Location = new System.Drawing.Point(12, 517);
            this.findAAButton.Name = "findAAButton";
            this.findAAButton.Size = new System.Drawing.Size(75, 23);
            this.findAAButton.TabIndex = 2;
            this.findAAButton.Text = "Find AA";
            this.findAAButton.UseVisualStyleBackColor = true;
            // 
            // AAFoundLabel
            // 
            this.AAFoundLabel.AutoSize = true;
            this.AAFoundLabel.Location = new System.Drawing.Point(110, 522);
            this.AAFoundLabel.Name = "AAFoundLabel";
            this.AAFoundLabel.Size = new System.Drawing.Size(76, 13);
            this.AAFoundLabel.TabIndex = 3;
            this.AAFoundLabel.Text = "DEBUG TEXT";
            // 
            // questLocationPictureBox
            // 
            this.questLocationPictureBox.Location = new System.Drawing.Point(343, 106);
            this.questLocationPictureBox.Name = "questLocationPictureBox";
            this.questLocationPictureBox.Size = new System.Drawing.Size(293, 237);
            this.questLocationPictureBox.TabIndex = 4;
            this.questLocationPictureBox.TabStop = false;
            // 
            // manualReceiveHandInButton
            // 
            this.manualReceiveHandInButton.Enabled = false;
            this.manualReceiveHandInButton.Location = new System.Drawing.Point(343, 349);
            this.manualReceiveHandInButton.Name = "manualReceiveHandInButton";
            this.manualReceiveHandInButton.Size = new System.Drawing.Size(97, 23);
            this.manualReceiveHandInButton.TabIndex = 5;
            this.manualReceiveHandInButton.Text = "-";
            this.manualReceiveHandInButton.UseVisualStyleBackColor = true;
            this.manualReceiveHandInButton.Click += new System.EventHandler(this.manualReceiveHandInButton_Click);
            // 
            // chatboxConfigButton
            // 
            this.chatboxConfigButton.Location = new System.Drawing.Point(12, 488);
            this.chatboxConfigButton.Name = "chatboxConfigButton";
            this.chatboxConfigButton.Size = new System.Drawing.Size(174, 23);
            this.chatboxConfigButton.TabIndex = 6;
            this.chatboxConfigButton.Text = "Set Chatbox Location";
            this.chatboxConfigButton.UseVisualStyleBackColor = true;
            this.chatboxConfigButton.Click += new System.EventHandler(this.chatboxConfigButton_Click);
            // 
            // resetSelectedButton
            // 
            this.resetSelectedButton.Enabled = false;
            this.resetSelectedButton.Location = new System.Drawing.Point(343, 378);
            this.resetSelectedButton.Name = "resetSelectedButton";
            this.resetSelectedButton.Size = new System.Drawing.Size(97, 23);
            this.resetSelectedButton.TabIndex = 7;
            this.resetSelectedButton.Text = "Reset Selected";
            this.resetSelectedButton.UseVisualStyleBackColor = true;
            this.resetSelectedButton.Click += new System.EventHandler(this.resetSelectedButton_Click);
            // 
            // resetAllButton
            // 
            this.resetAllButton.Location = new System.Drawing.Point(343, 407);
            this.resetAllButton.Name = "resetAllButton";
            this.resetAllButton.Size = new System.Drawing.Size(97, 23);
            this.resetAllButton.TabIndex = 8;
            this.resetAllButton.Text = "Reset All";
            this.resetAllButton.UseVisualStyleBackColor = true;
            this.resetAllButton.Click += new System.EventHandler(this.resetAllButton_Click);
            // 
            // x1TextBox
            // 
            this.x1TextBox.Location = new System.Drawing.Point(228, 490);
            this.x1TextBox.Name = "x1TextBox";
            this.x1TextBox.Size = new System.Drawing.Size(55, 20);
            this.x1TextBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "X1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 493);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y1";
            // 
            // y1TextBox
            // 
            this.y1TextBox.Location = new System.Drawing.Point(315, 490);
            this.y1TextBox.Name = "y1TextBox";
            this.y1TextBox.Size = new System.Drawing.Size(55, 20);
            this.y1TextBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 493);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Y2";
            // 
            // y2TextBox
            // 
            this.y2TextBox.Location = new System.Drawing.Point(498, 490);
            this.y2TextBox.Name = "y2TextBox";
            this.y2TextBox.Size = new System.Drawing.Size(55, 20);
            this.y2TextBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 493);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "X2";
            // 
            // x2TextBox
            // 
            this.x2TextBox.Location = new System.Drawing.Point(411, 490);
            this.x2TextBox.Name = "x2TextBox";
            this.x2TextBox.Size = new System.Drawing.Size(55, 20);
            this.x2TextBox.TabIndex = 13;
            // 
            // resetTimer
            // 
            this.resetTimer.Enabled = true;
            this.resetTimer.Interval = 5000;
            this.resetTimer.Tick += new System.EventHandler(this.resetTimer_Tick);
            // 
            // screenCaptureTimer
            // 
            this.screenCaptureTimer.Interval = 1500;
            this.screenCaptureTimer.Tick += new System.EventHandler(this.screenCaptureTimer_Tick);
            // 
            // captureStartButton
            // 
            this.captureStartButton.Enabled = false;
            this.captureStartButton.Location = new System.Drawing.Point(679, 517);
            this.captureStartButton.Name = "captureStartButton";
            this.captureStartButton.Size = new System.Drawing.Size(75, 23);
            this.captureStartButton.TabIndex = 17;
            this.captureStartButton.Text = "RUN";
            this.captureStartButton.UseVisualStyleBackColor = true;
            this.captureStartButton.Click += new System.EventHandler(this.captureStartButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(679, 487);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 18;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 552);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.captureStartButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.y2TextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.x2TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.y1TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.x1TextBox);
            this.Controls.Add(this.resetAllButton);
            this.Controls.Add(this.resetSelectedButton);
            this.Controls.Add(this.chatboxConfigButton);
            this.Controls.Add(this.manualReceiveHandInButton);
            this.Controls.Add(this.questLocationPictureBox);
            this.Controls.Add(this.AAFoundLabel);
            this.Controls.Add(this.findAAButton);
            this.Controls.Add(this.questListBox);
            this.Controls.Add(this.questDescriptionTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "ArcheAge Dailies Manager - https://github.com/AutismCrew/AADailiesManager";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questLocationPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox questDescriptionTextBox;
        private System.Windows.Forms.ListBox questListBox;
        private System.Windows.Forms.Button findAAButton;
        private System.Windows.Forms.Label AAFoundLabel;
        private System.Windows.Forms.PictureBox questLocationPictureBox;
        private System.Windows.Forms.Button manualReceiveHandInButton;
        private System.Windows.Forms.Button chatboxConfigButton;
        private System.Windows.Forms.Button resetSelectedButton;
        private System.Windows.Forms.Button resetAllButton;
        private System.Windows.Forms.TextBox x1TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox y1TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox y2TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox x2TextBox;
        private System.Windows.Forms.Timer resetTimer;
        private System.Windows.Forms.Timer screenCaptureTimer;
        private System.Windows.Forms.Button captureStartButton;
        private System.Windows.Forms.Button stopButton;
    }
}

