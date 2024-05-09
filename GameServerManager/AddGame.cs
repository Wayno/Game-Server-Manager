using static GameServerManager.Form1;

namespace GameServerManager
{
    public partial class AddGame : Form
    {
        private Form1 parentForm;

        private GameConfig currentGameConfig; // Declare at the class level
        public AddGame(Form1 parent, GameConfig config = null)
        {
            InitializeComponent();
            this.Text = "Add Game";
            this.Icon = new Icon("icon.ico");
            //this.Load += new System.EventHandler(this.AddGame_Load);
            this.currentGameConfig = config;
            this.FormClosing += new FormClosingEventHandler(AddGame_FormClosing);
            this.parentForm = parent;
            this.gameIDTextBox.KeyPress += gameIDTextBox_KeyPress;

            this.currentGameConfig = config ?? new GameConfig
            {
                GameID = "DefaultGameID", // Set a default GameID
                Name = "DefaultName", // Set a default Name
                ServerExecutableFilename = "DefaultServerExecutable.exe", // Set a default executable filename
                ServerDirectory = "DefaultServerDirectory", // Set a default server directory
                ExecutableDir = "DefaultExecutableDir", // Set a default executable directory
                ExtraArgs = "DefaultExtraArgs", // Set default extra arguments
                Countdown = 60, // Set a default countdown value (integer)
                rconIP = "",
                rconPort = "",
                rconPassword = "",
                AutoRestartEnabled = false,
                RestartHour = 30,
                RestartMinute = 30
            };
        }

        private void gameIDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignore the event, effectively blocking the character
            }
        }

        private void CountDownTimerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignore the event, effectively blocking the character
            }
        }


        private void AddGame_FormClosing(object? sender, FormClosingEventArgs e)
        {
            parentForm.UpdateButtonStateBasedOnGameID();
        }

        private async void ExecuteAfterDelayAsync(int delayMilliseconds, Action action)
        {
            await Task.Delay(delayMilliseconds);
            action();
        }

        private void okButton_Click_1(object sender, EventArgs e)
        {
            // Validate that all input fields are filled in
            if (string.IsNullOrWhiteSpace(gameIDTextBox.Text) ||
                string.IsNullOrWhiteSpace(TabNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(serverDirTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return; // Exit the method and do not proceed further
            }


                // Update currentGameConfig with the values from the input fields
                if (currentGameConfig != null)
                {
                    currentGameConfig.GameID = gameIDTextBox.Text;
                    currentGameConfig.Name = TabNameTextBox.Text;
                    currentGameConfig.ServerDirectory = serverDirTextBox.Text;
                }

                // Save the updated configuration
                GameConfigManager.SaveGameConfig(currentGameConfig);

            parentForm.AddGameTab(currentGameConfig);
            DialogResult = DialogResult.OK;
            Close();

            ExecuteAfterDelayAsync(3000, () => parentForm.UpdateServer(gameIDTextBox.Text));

        }


        private void selectServerDirButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select the Server Directory To Install Too";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    if (currentGameConfig != null)
                    {
                        currentGameConfig.ServerDirectory = selectedFolderPath;
                        serverDirTextBox.Text = selectedFolderPath; // Update TextBox
                    }
                    else
                    {
                        MessageBox.Show("Game configuration is not initialized.");
                    }
                }
            }
        }
    }
}
