namespace WinFormsApp1
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
            okButton.Location = new Point(504, 289);
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
            // TabSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 321);
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
    }
}