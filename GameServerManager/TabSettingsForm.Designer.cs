namespace GameServerManager
{
    partial class TabSettingsForm
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
            ExtraArgsTextBox = new TextBox();
            label5 = new Label();
            GameIDTextBox = new TextBox();
            label6 = new Label();
            ExecutableDirTextBox = new TextBox();
            selectExecutableDirButton = new Button();
            label4 = new Label();
            ServerDirTextBox = new TextBox();
            selectServerDirButton = new Button();
            label3 = new Label();
            ServerExeTextBox = new TextBox();
            selectServerExeButton = new Button();
            label2 = new Label();
            okButton = new Button();
            Countdowntimer = new Label();
            CountDownTimerTextBox = new TextBox();
            TabNameTextBox = new TextBox();
            TabName = new Label();
            label1 = new Label();
            hoursComboBox = new ComboBox();
            minutesComboBox = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            TextBoxIP = new TextBox();
            TextBoxPort = new TextBox();
            MaskedTextBoxPasssword = new MaskedTextBox();
            LabelIP = new Label();
            LabelPort = new Label();
            LabelPassword = new Label();
            label9 = new Label();
            label10 = new Label();
            btnShowPassword = new Button();
            autoRestartCheckBox = new CheckBox();
            label11 = new Label();
            SuspendLayout();
            // 
            // ExtraArgsTextBox
            // 
            ExtraArgsTextBox.Location = new Point(11, 71);
            ExtraArgsTextBox.Name = "ExtraArgsTextBox";
            ExtraArgsTextBox.Size = new Size(494, 23);
            ExtraArgsTextBox.TabIndex = 48;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 53);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 47;
            label5.Text = "Extra Arguments";
            // 
            // GameIDTextBox
            // 
            GameIDTextBox.Location = new Point(11, 27);
            GameIDTextBox.Name = "GameIDTextBox";
            GameIDTextBox.ReadOnly = true;
            GameIDTextBox.Size = new Size(86, 23);
            GameIDTextBox.TabIndex = 46;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 9);
            label6.Name = "label6";
            label6.Size = new Size(52, 15);
            label6.TabIndex = 45;
            label6.Text = "Game ID";
            // 
            // ExecutableDirTextBox
            // 
            ExecutableDirTextBox.Location = new Point(92, 174);
            ExecutableDirTextBox.Name = "ExecutableDirTextBox";
            ExecutableDirTextBox.ReadOnly = true;
            ExecutableDirTextBox.Size = new Size(494, 23);
            ExecutableDirTextBox.TabIndex = 44;
            // 
            // selectExecutableDirButton
            // 
            selectExecutableDirButton.Location = new Point(11, 174);
            selectExecutableDirButton.Name = "selectExecutableDirButton";
            selectExecutableDirButton.Size = new Size(75, 23);
            selectExecutableDirButton.TabIndex = 43;
            selectExecutableDirButton.Text = "Set Dir";
            selectExecutableDirButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 156);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 42;
            label4.Text = "Set Exe Dir";
            // 
            // ServerDirTextBox
            // 
            ServerDirTextBox.Location = new Point(92, 131);
            ServerDirTextBox.Name = "ServerDirTextBox";
            ServerDirTextBox.ReadOnly = true;
            ServerDirTextBox.Size = new Size(494, 23);
            ServerDirTextBox.TabIndex = 41;
            // 
            // selectServerDirButton
            // 
            selectServerDirButton.Location = new Point(11, 130);
            selectServerDirButton.Name = "selectServerDirButton";
            selectServerDirButton.Size = new Size(75, 23);
            selectServerDirButton.TabIndex = 40;
            selectServerDirButton.Text = "Set Dir";
            selectServerDirButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 112);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 39;
            label3.Text = "Set Server Dir";
            // 
            // ServerExeTextBox
            // 
            ServerExeTextBox.Location = new Point(92, 219);
            ServerExeTextBox.Name = "ServerExeTextBox";
            ServerExeTextBox.ReadOnly = true;
            ServerExeTextBox.Size = new Size(494, 23);
            ServerExeTextBox.TabIndex = 38;
            // 
            // selectServerExeButton
            // 
            selectServerExeButton.Location = new Point(11, 218);
            selectServerExeButton.Name = "selectServerExeButton";
            selectServerExeButton.Size = new Size(75, 23);
            selectServerExeButton.TabIndex = 37;
            selectServerExeButton.Text = "Set Exe";
            selectServerExeButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 200);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 36;
            label2.Text = "Set Server Excutable";
            // 
            // okButton
            // 
            okButton.Location = new Point(511, 456);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 35;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // Countdowntimer
            // 
            Countdowntimer.AutoSize = true;
            Countdowntimer.Location = new Point(195, 9);
            Countdowntimer.Name = "Countdowntimer";
            Countdowntimer.Size = new Size(70, 15);
            Countdowntimer.TabIndex = 49;
            Countdowntimer.Text = "Countdown";
            // 
            // CountDownTimerTextBox
            // 
            CountDownTimerTextBox.Location = new Point(195, 27);
            CountDownTimerTextBox.Name = "CountDownTimerTextBox";
            CountDownTimerTextBox.Size = new Size(86, 23);
            CountDownTimerTextBox.TabIndex = 50;
            // 
            // TabNameTextBox
            // 
            TabNameTextBox.Location = new Point(103, 27);
            TabNameTextBox.Name = "TabNameTextBox";
            TabNameTextBox.Size = new Size(86, 23);
            TabNameTextBox.TabIndex = 52;
            // 
            // TabName
            // 
            TabName.AutoSize = true;
            TabName.Location = new Point(103, 9);
            TabName.Name = "TabName";
            TabName.Size = new Size(60, 15);
            TabName.TabIndex = 51;
            TabName.Text = "Tab Name";
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ControlDarkDark;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(11, 250);
            label1.Name = "label1";
            label1.Size = new Size(573, 1);
            label1.TabIndex = 53;
            // 
            // hoursComboBox
            // 
            hoursComboBox.FormattingEnabled = true;
            hoursComboBox.Location = new Point(11, 389);
            hoursComboBox.Name = "hoursComboBox";
            hoursComboBox.Size = new Size(121, 23);
            hoursComboBox.TabIndex = 54;
            // 
            // minutesComboBox
            // 
            minutesComboBox.FormattingEnabled = true;
            minutesComboBox.Location = new Point(138, 389);
            minutesComboBox.Name = "minutesComboBox";
            minutesComboBox.Size = new Size(121, 23);
            minutesComboBox.TabIndex = 55;
            // 
            // label7
            // 
            label7.BackColor = SystemColors.ControlDarkDark;
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(11, 315);
            label7.Name = "label7";
            label7.Size = new Size(573, 1);
            label7.TabIndex = 56;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 260);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 57;
            label8.Text = "Rcon Details";
            // 
            // TextBoxIP
            // 
            TextBoxIP.Location = new Point(37, 278);
            TextBoxIP.Name = "TextBoxIP";
            TextBoxIP.Size = new Size(152, 23);
            TextBoxIP.TabIndex = 58;
            // 
            // TextBoxPort
            // 
            TextBoxPort.Location = new Point(233, 278);
            TextBoxPort.Name = "TextBoxPort";
            TextBoxPort.Size = new Size(100, 23);
            TextBoxPort.TabIndex = 59;
            // 
            // MaskedTextBoxPasssword
            // 
            MaskedTextBoxPasssword.Location = new Point(405, 278);
            MaskedTextBoxPasssword.Name = "MaskedTextBoxPasssword";
            MaskedTextBoxPasssword.Size = new Size(100, 23);
            MaskedTextBoxPasssword.TabIndex = 60;
            MaskedTextBoxPasssword.UseSystemPasswordChar = true;
            // 
            // LabelIP
            // 
            LabelIP.AutoSize = true;
            LabelIP.Location = new Point(11, 281);
            LabelIP.Name = "LabelIP";
            LabelIP.Size = new Size(20, 15);
            LabelIP.TabIndex = 61;
            LabelIP.Text = "IP:";
            // 
            // LabelPort
            // 
            LabelPort.AutoSize = true;
            LabelPort.Location = new Point(195, 282);
            LabelPort.Name = "LabelPort";
            LabelPort.Size = new Size(32, 15);
            LabelPort.TabIndex = 62;
            LabelPort.Text = "Port:";
            // 
            // LabelPassword
            // 
            LabelPassword.AutoSize = true;
            LabelPassword.Location = new Point(339, 281);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new Size(60, 15);
            LabelPassword.TabIndex = 63;
            LabelPassword.Text = "Password:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 331);
            label9.Name = "label9";
            label9.Size = new Size(101, 15);
            label9.TabIndex = 64;
            label9.Text = "Scheduled Restart";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 415);
            label10.Name = "label10";
            label10.Size = new Size(197, 15);
            label10.TabIndex = 65;
            label10.Text = "This will overwrite the Global Restart";
            // 
            // btnShowPassword
            // 
            btnShowPassword.Location = new Point(511, 278);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.Size = new Size(75, 23);
            btnShowPassword.TabIndex = 66;
            btnShowPassword.Text = "Show Pass";
            btnShowPassword.UseVisualStyleBackColor = true;
            btnShowPassword.Click += btnShowPassword_Click_1;
            // 
            // autoRestartCheckBox
            // 
            autoRestartCheckBox.AutoSize = true;
            autoRestartCheckBox.Location = new Point(12, 349);
            autoRestartCheckBox.Name = "autoRestartCheckBox";
            autoRestartCheckBox.Size = new Size(126, 19);
            autoRestartCheckBox.TabIndex = 67;
            autoRestartCheckBox.Text = "Auto Restart Server";
            autoRestartCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 371);
            label11.Name = "label11";
            label11.Size = new Size(101, 15);
            label11.TabIndex = 68;
            label11.Text = "Scheduled Restart";
            // 
            // TabSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 491);
            Controls.Add(label11);
            Controls.Add(autoRestartCheckBox);
            Controls.Add(btnShowPassword);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(LabelPassword);
            Controls.Add(LabelPort);
            Controls.Add(LabelIP);
            Controls.Add(MaskedTextBoxPasssword);
            Controls.Add(TextBoxPort);
            Controls.Add(TextBoxIP);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(minutesComboBox);
            Controls.Add(hoursComboBox);
            Controls.Add(label1);
            Controls.Add(TabNameTextBox);
            Controls.Add(TabName);
            Controls.Add(CountDownTimerTextBox);
            Controls.Add(Countdowntimer);
            Controls.Add(ExtraArgsTextBox);
            Controls.Add(label5);
            Controls.Add(GameIDTextBox);
            Controls.Add(label6);
            Controls.Add(ExecutableDirTextBox);
            Controls.Add(selectExecutableDirButton);
            Controls.Add(label4);
            Controls.Add(ServerDirTextBox);
            Controls.Add(selectServerDirButton);
            Controls.Add(label3);
            Controls.Add(ServerExeTextBox);
            Controls.Add(selectServerExeButton);
            Controls.Add(label2);
            Controls.Add(okButton);
            Name = "TabSettingsForm";
            Text = "TabSettingsForm";
            Load += TabSettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ExtraArgsTextBox;
        private Label label5;
        private TextBox GameIDTextBox;
        private Label label6;
        private TextBox ExecutableDirTextBox;
        private Button selectExecutableDirButton;
        private Label label4;
        private TextBox ServerDirTextBox;
        private Button selectServerDirButton;
        private Label label3;
        private TextBox ServerExeTextBox;
        private Button selectServerExeButton;
        private Label label2;
        private Button okButton;
        private Label Countdowntimer;
        private TextBox CountDownTimerTextBox;
        private TextBox TabNameTextBox;
        private Label TabName;
        private Label label1;
        private ComboBox hoursComboBox;
        private ComboBox minutesComboBox;
        private Label label7;
        private Label label8;
        private TextBox TextBoxIP;
        private TextBox TextBoxPort;
        private MaskedTextBox MaskedTextBoxPasssword;
        private Label LabelIP;
        private Label LabelPort;
        private Label LabelPassword;
        private Label label9;
        private Label label10;
        private Button btnShowPassword;
        private CheckBox autoRestartCheckBox;
        private Label label11;
    }
}