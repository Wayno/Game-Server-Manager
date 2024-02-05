namespace WinFormsApp1
{
    partial class VariableInputForm
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
            setSteamCMDButton = new Button();
            label1 = new Label();
            okButton = new Button();
            SteamCMDText = new TextBox();
            GlobalTimerTextBox = new TextBox();
            GlobalTimerText = new Label();
            hoursComboBox = new ComboBox();
            minutesComboBox = new ComboBox();
            SuspendLayout();
            // 
            // setSteamCMDButton
            // 
            setSteamCMDButton.Location = new Point(12, 27);
            setSteamCMDButton.Name = "setSteamCMDButton";
            setSteamCMDButton.Size = new Size(75, 23);
            setSteamCMDButton.TabIndex = 0;
            setSteamCMDButton.Text = "Set Dir";
            setSteamCMDButton.UseVisualStyleBackColor = true;
            setSteamCMDButton.Click += setDirectoryButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(153, 15);
            label1.TabIndex = 1;
            label1.Text = "Set Directory For SeamCMD";
            // 
            // okButton
            // 
            okButton.Location = new Point(425, 171);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // SteamCMDText
            // 
            SteamCMDText.Location = new Point(12, 56);
            SteamCMDText.Name = "SteamCMDText";
            SteamCMDText.ReadOnly = true;
            SteamCMDText.Size = new Size(494, 23);
            SteamCMDText.TabIndex = 3;
            // 
            // GlobalTimerTextBox
            // 
            GlobalTimerTextBox.Location = new Point(12, 100);
            GlobalTimerTextBox.Name = "GlobalTimerTextBox";
            GlobalTimerTextBox.Size = new Size(100, 23);
            GlobalTimerTextBox.TabIndex = 4;
            // 
            // GlobalTimerText
            // 
            GlobalTimerText.AutoSize = true;
            GlobalTimerText.Location = new Point(12, 82);
            GlobalTimerText.Name = "GlobalTimerText";
            GlobalTimerText.Size = new Size(74, 15);
            GlobalTimerText.TabIndex = 5;
            GlobalTimerText.Text = "Global Timer";
            // 
            // hoursComboBox
            // 
            hoursComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            hoursComboBox.FormattingEnabled = true;
            hoursComboBox.Location = new Point(192, 100);
            hoursComboBox.Name = "hoursComboBox";
            hoursComboBox.Size = new Size(78, 23);
            hoursComboBox.TabIndex = 6;
            // 
            // minutesComboBox
            // 
            minutesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            minutesComboBox.FormattingEnabled = true;
            minutesComboBox.Location = new Point(276, 100);
            minutesComboBox.Name = "minutesComboBox";
            minutesComboBox.Size = new Size(78, 23);
            minutesComboBox.TabIndex = 7;
            // 
            // VariableInputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 206);
            Controls.Add(minutesComboBox);
            Controls.Add(hoursComboBox);
            Controls.Add(GlobalTimerText);
            Controls.Add(GlobalTimerTextBox);
            Controls.Add(SteamCMDText);
            Controls.Add(okButton);
            Controls.Add(label1);
            Controls.Add(setSteamCMDButton);
            Name = "VariableInputForm";
            Text = "VariableInputForm";
            Load += VariableInputForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button setSteamCMDButton;
        private Label label1;
        private Button okButton;
        private TextBox SteamCMDText;
        private TextBox GlobalTimerTextBox;
        private Label GlobalTimerText;
        private ComboBox hoursComboBox;
        private ComboBox minutesComboBox;
    }
}