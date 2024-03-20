namespace GameServerManager
{
    partial class DebugForm
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
            debugRichTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // debugRichTextBox
            // 
            debugRichTextBox.Dock = DockStyle.Fill;
            debugRichTextBox.Location = new Point(0, 0);
            debugRichTextBox.Name = "debugRichTextBox";
            debugRichTextBox.Size = new Size(800, 450);
            debugRichTextBox.TabIndex = 0;
            debugRichTextBox.Text = "";
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(debugRichTextBox);
            Name = "DebugForm";
            Text = "DebugForm";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox debugRichTextBox;
    }
}