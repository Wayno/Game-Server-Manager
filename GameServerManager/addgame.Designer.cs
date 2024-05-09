namespace GameServerManager
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
            gameIDTextBox = new TextBox();
            label6 = new Label();
            serverDirTextBox = new TextBox();
            selectServerDirButton = new Button();
            label3 = new Label();
            okButton = new Button();
            TabNameTextBox = new TextBox();
            TabName = new Label();
            SuspendLayout();
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
            // serverDirTextBox
            // 
            serverDirTextBox.Location = new Point(12, 112);
            serverDirTextBox.Name = "serverDirTextBox";
            serverDirTextBox.ReadOnly = true;
            serverDirTextBox.Size = new Size(494, 23);
            serverDirTextBox.TabIndex = 27;
            // 
            // selectServerDirButton
            // 
            selectServerDirButton.Location = new Point(12, 83);
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
            label3.Location = new Point(12, 65);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 25;
            label3.Text = "Set Server Dir";
            // 
            // okButton
            // 
            okButton.Location = new Point(431, 153);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 20;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click_1;
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
            ClientSize = new Size(516, 186);
            Controls.Add(TabNameTextBox);
            Controls.Add(TabName);
            Controls.Add(gameIDTextBox);
            Controls.Add(label6);
            Controls.Add(serverDirTextBox);
            Controls.Add(selectServerDirButton);
            Controls.Add(label3);
            Controls.Add(okButton);
            Name = "AddGame";
            Text = "addgame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox gameIDTextBox;
        private Label label6;
        private TextBox serverDirTextBox;
        private Button selectServerDirButton;
        private Label label3;
        private Button okButton;
        private TextBox TabNameTextBox;
        private Label TabName;
    }
}