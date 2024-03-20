using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameServerManager
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        public void AppendDebugInfo(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendDebugInfo), message);
                return;
            }

            // Append the message to the RichTextBox
            debugRichTextBox.AppendText(message + Environment.NewLine);

            // Set the caret at the end of the text and scroll to the caret's position
            debugRichTextBox.SelectionStart = debugRichTextBox.TextLength;
            debugRichTextBox.ScrollToCaret();
        }

    }
}
