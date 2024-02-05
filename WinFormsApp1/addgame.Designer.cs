namespace WinFormsApp1
{
    partial class AddGame
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
            extraARgsTextBox = new TextBox();
            label5 = new Label();
            gameIDTextBox = new TextBox();
            label6 = new Label();
            executableDirTextBox = new TextBox();
            selectExecutableDirButton = new Button();
            label4 = new Label();
            serverDirTextBox = new TextBox();
            selectServerDirButton = new Button();
            label3 = new Label();
            serverExeTextBox = new TextBox();
            selectServerExeButton = new Button();
            label2 = new Label();
            okButton = new Button();
            CountDownTimerTextBox = new TextBox();
            Countdowntimer = new Label();
            TabNameTextBox = new TextBox();
            TabName = new Label();
            SuspendLayout();
            // 
            // extraARgsTextBox
            // 
            extraARgsTextBox.Location = new Point(12, 71);
            extraARgsTextBox.Name = "extraARgsTextBox";
            extraARgsTextBox.Size = new Size(494, 23);
            extraARgsTextBox.TabIndex = 34;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 53);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 33;
            label5.Text = "Extra Arguments";
            // 
            // gameIDTextBox
            // 
            gameIDTextBox.Location = new Point(12, 27);
            gameIDTextBox.Name = "gameIDTextBox";
            gameIDTextBox.Size = new Size(86, 23);
            gameIDTextBox.TabIndex = 32;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(19, 9);
            label6.Name = "label6";
            label6.Size = new Size(52, 15);
            label6.TabIndex = 31;
            label6.Text = "Game ID";
            // 
            // executableDirTextBox
            // 
            executableDirTextBox.Location = new Point(86, 207);
            executableDirTextBox.Name = "executableDirTextBox";
            executableDirTextBox.ReadOnly = true;
            executableDirTextBox.Size = new Size(494, 23);
            executableDirTextBox.TabIndex = 30;
            // 
            // selectExecutableDirButton
            // 
            selectExecutableDirButton.Location = new Point(5, 207);
            selectExecutableDirButton.Name = "selectExecutableDirButton";
            selectExecutableDirButton.Size = new Size(75, 23);
            selectExecutableDirButton.TabIndex = 29;
            selectExecutableDirButton.Text = "Set Dir";
            selectExecutableDirButton.UseVisualStyleBackColor = true;
            selectExecutableDirButton.Click += selectExecutableDirButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 189);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 28;
            label4.Text = "Set Exe Dir";
            // 
            // serverDirTextBox
            // 
            serverDirTextBox.Location = new Point(86, 162);
            serverDirTextBox.Name = "serverDirTextBox";
            serverDirTextBox.ReadOnly = true;
            serverDirTextBox.Size = new Size(494, 23);
            serverDirTextBox.TabIndex = 27;
            // 
            // selectServerDirButton
            // 
            selectServerDirButton.Location = new Point(5, 162);
            selectServerDirButton.Name = "selectServerDirButton";
            selectServerDirButton.Size = new Size(75, 23);
            selectServerDirButton.TabIndex = 26;
            selectServerDirButton.Text = "Set Dir";
            selectServerDirButton.UseVisualStyleBackColor = true;
            selectServerDirButton.Click += selectServerDirButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 144);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 25;
            label3.Text = "Set Server Dir";
            // 
            // serverExeTextBox
            // 
            serverExeTextBox.Location = new Point(85, 250);
            serverExeTextBox.Name = "serverExeTextBox";
            serverExeTextBox.ReadOnly = true;
            serverExeTextBox.Size = new Size(494, 23);
            serverExeTextBox.TabIndex = 24;
            // 
            // selectServerExeButton
            // 
            selectServerExeButton.Location = new Point(4, 249);
            selectServerExeButton.Name = "selectServerExeButton";
            selectServerExeButton.Size = new Size(75, 23);
            selectServerExeButton.TabIndex = 23;
            selectServerExeButton.Text = "Set Exe";
            selectServerExeButton.UseVisualStyleBackColor = true;
            selectServerExeButton.Click += selectServerExeButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 231);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 22;
            label2.Text = "Set Server Excutable";
            // 
            // okButton
            // 
            okButton.Location = new Point(505, 289);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 20;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click_1;
            // 
            // CountDownTimerTextBox
            // 
            CountDownTimerTextBox.Location = new Point(195, 27);
            CountDownTimerTextBox.Name = "CountDownTimerTextBox";
            CountDownTimerTextBox.Size = new Size(86, 23);
            CountDownTimerTextBox.TabIndex = 52;
            // 
            // Countdowntimer
            // 
            Countdowntimer.AutoSize = true;
            Countdowntimer.Location = new Point(195, 9);
            Countdowntimer.Name = "Countdowntimer";
            Countdowntimer.Size = new Size(70, 15);
            Countdowntimer.TabIndex = 51;
            Countdowntimer.Text = "Countdown";
            // 
            // TabNameTextBox
            // 
            TabNameTextBox.Location = new Point(103, 27);
            TabNameTextBox.Name = "TabNameTextBox";
            TabNameTextBox.Size = new Size(86, 23);
            TabNameTextBox.TabIndex = 54;
            // 
            // TabName
            // 
            TabName.AutoSize = true;
            TabName.Location = new Point(103, 9);
            TabName.Name = "TabName";
            TabName.Size = new Size(60, 15);
            TabName.TabIndex = 53;
            TabName.Text = "Tab Name";
            // 
            // AddGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 317);
            Controls.Add(TabNameTextBox);
            Controls.Add(TabName);
            Controls.Add(CountDownTimerTextBox);
            Controls.Add(Countdowntimer);
            Controls.Add(extraARgsTextBox);
            Controls.Add(label5);
            Controls.Add(gameIDTextBox);
            Controls.Add(label6);
            Controls.Add(executableDirTextBox);
            Controls.Add(selectExecutableDirButton);
            Controls.Add(label4);
            Controls.Add(serverDirTextBox);
            Controls.Add(selectServerDirButton);
            Controls.Add(label3);
            Controls.Add(serverExeTextBox);
            Controls.Add(selectServerExeButton);
            Controls.Add(label2);
            Controls.Add(okButton);
            Name = "AddGame";
            Text = "addgame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox extraARgsTextBox;
        private Label label5;
        private TextBox gameIDTextBox;
        private Label label6;
        private TextBox executableDirTextBox;
        private Button selectExecutableDirButton;
        private Label label4;
        private TextBox serverDirTextBox;
        private Button selectServerDirButton;
        private Label label3;
        private TextBox serverExeTextBox;
        private Button selectServerExeButton;
        private Label label2;
        private Button okButton;
        private TextBox CountDownTimerTextBox;
        private Label Countdowntimer;
        private TextBox TabNameTextBox;
        private Label TabName;
    }
}