using System;
using System.Windows.Forms;
using GameServerManager.Properties;

namespace GameServerManager
{
    public partial class VariableInputForm : Form
    {
        private Form1 parentForm;

        public VariableInputForm(Form1 parent)
        {
            InitializeComponent();
            this.Text = "Settings";
            this.Icon = new Icon("icon.ico");
            this.Load += new System.EventHandler(this.VariableInputForm_Load);
            this.FormClosing += new FormClosingEventHandler(VariableInputForm_FormClosing);
            this.parentForm = parent;

            // Populate hours
            for (int i = 0; i < 24; i++)
            {
                hoursComboBox.Items.Add(i.ToString("D2"));
            }

            // Populate minutes
            for (int i = 0; i < 60; i++)
            {
                minutesComboBox.Items.Add(i.ToString("D2"));
            }
        }

        private void VariableInputForm_Load(object? sender, EventArgs e)
        {
            SteamCMDText.Text = Settings.Default.SteamCMDDirectory;

            int RestartHour = Properties.Settings.Default.RestartHour;
            int RestartMinute = Properties.Settings.Default.RestartMinute;


            // Check if the Global_Timer setting is null or empty
            if (Settings.Default.Global_Timer == null)
            {
                // Handle the case where Global_Timer is null
                GlobalTimerTextBox.Text = "300"; // Default value
            }
            else
            {
                GlobalTimerTextBox.Text = Settings.Default.Global_Timer.ToString();
            }

            string selectedHour = Settings.Default.RestartHour.ToString("D2");
            string selectedMinute = Settings.Default.RestartMinute.ToString("D2");

            // Set the selected items in the ComboBoxes
            hoursComboBox.SelectedItem = selectedHour;
            minutesComboBox.SelectedItem = selectedMinute;

        }

        private void PopulateHours()
        {
            hoursComboBox.Items.Clear();
            for (int hour = 0; hour < 24; hour++)
            {
                hoursComboBox.Items.Add(hour.ToString("D2")); // Formats the hour as a two-digit number
            }
        }

        private void PopulateMinutes()
        {
            minutesComboBox.Items.Clear();
            for (int minute = 0; minute < 60; minute++)
            {
                minutesComboBox.Items.Add(minute.ToString("D2")); // Formats the minute as a two-digit number
            }
        }

        private void VariableInputForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            parentForm.UpdateButtonStateBasedOnGameID();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Save the selected directory path in Settings

            if (int.TryParse(hoursComboBox.SelectedItem.ToString(), out int selectedHour) &&
                int.TryParse(minutesComboBox.SelectedItem.ToString(), out int selectedMinute) &&
                int.TryParse(GlobalTimerTextBox.Text, out int globalTimer))
            {
                // Save the selected hour and minute to settings
                Settings.Default.RestartHour = selectedHour;
                Settings.Default.RestartMinute = selectedMinute;

                // Save the global timer value
                Settings.Default.Global_Timer = globalTimer;

                // Persist the changes
                Settings.Default.Save();

                // Close the form with OK result
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void setDirectoryButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    SteamCMDText.Text = selectedFolderPath;

                    Settings.Default.SteamCMDDirectory = selectedFolderPath;
                }
            }
        }
    }
}
