namespace GameServerManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Timer1 = new System.Windows.Forms.Timer(components);
            SettingsButton = new Button();
            gamesTabControl = new TabControl();
            AddGame = new Button();
            StartStopAllButton = new Button();
            SuspendLayout();
            // 
            // Timer1
            // 
            Timer1.Interval = 1000;
            // 
            // SettingsButton
            // 
            SettingsButton.Location = new Point(452, 12);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(75, 23);
            SettingsButton.TabIndex = 4;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += Settings_Button_Click;
            // 
            // gamesTabControl
            // 
            gamesTabControl.Location = new Point(12, 41);
            gamesTabControl.Name = "gamesTabControl";
            gamesTabControl.SelectedIndex = 0;
            gamesTabControl.Size = new Size(515, 362);
            gamesTabControl.TabIndex = 5;
            // 
            // AddGame
            // 
            AddGame.Location = new Point(12, 12);
            AddGame.Name = "AddGame";
            AddGame.Size = new Size(75, 23);
            AddGame.TabIndex = 6;
            AddGame.Text = "Add Game";
            AddGame.UseVisualStyleBackColor = true;
            AddGame.Click += AddGame_Click;
            // 
            // StartStopAllButton
            // 
            StartStopAllButton.Location = new Point(93, 12);
            StartStopAllButton.Name = "StartStopAllButton";
            StartStopAllButton.Size = new Size(75, 23);
            StartStopAllButton.TabIndex = 7;
            StartStopAllButton.Text = "Start All";
            StartStopAllButton.UseVisualStyleBackColor = true;
            StartStopAllButton.Click += StartStopAll_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(539, 415);
            Controls.Add(StartStopAllButton);
            Controls.Add(AddGame);
            Controls.Add(gamesTabControl);
            Controls.Add(SettingsButton);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer Timer1;
        private Button SettingsButton;
        private TabControl gamesTabControl;
        private Button AddGame;
        private Button StartStopAllButton;
    }
}
