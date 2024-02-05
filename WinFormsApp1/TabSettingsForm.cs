using static WinFormsApp1.Form1;

namespace WinFormsApp1
{
    public partial class TabSettingsForm : Form
    {
        private GameConfig currentGameConfig;
        private readonly string gameID;

        public TabSettingsForm(Form1 form1, string gameID)
        {
            InitializeComponent();
            this.gameID = gameID;
        }

        private void TabSettingsForm_Load(object sender, EventArgs e)
        {
            // Read the updated game configurations from the file
            List<GameConfig> gameConfigs = GameConfigManager.LoadAllGameConfigs();

            // Get the selected game configuration
            if (currentGameConfig != null)
            {
                // Find the matching configuration based on GameID
                GameConfig selectedGameConfig = gameConfigs.FirstOrDefault(config => config.GameID == currentGameConfig.GameID);

                if (selectedGameConfig != null)
                {
                    // Set the currentGameConfig to the selected configuration
                    currentGameConfig = selectedGameConfig;

                    // Populate TextBox controls with the selected configuration's values
                    GameIDTextBox.Text = currentGameConfig.GameID;
                    TabNameTextBox.Text = currentGameConfig.Name;
                    ExtraArgsTextBox.Text = currentGameConfig.ExtraArgs;
                    ServerDirTextBox.Text = currentGameConfig.ServerDirectory;
                    ExecutableDirTextBox.Text = currentGameConfig.ExecutableDir;
                    ServerExeTextBox.Text = currentGameConfig.ServerExecutableFilename;
                    CountDownTimerTextBox.Text = currentGameConfig.Countdown.ToString();
                }
            }
        }

        public void LoadGameConfig(GameConfig config)
        {
            currentGameConfig = config;
            GameIDTextBox.Text = config.GameID;
            TabNameTextBox.Text = config.Name;
            ExtraArgsTextBox.Text = config.ExtraArgs;
            ServerDirTextBox.Text = config.ServerDirectory;
            ExecutableDirTextBox.Text = config.ExecutableDir;
            ServerExeTextBox.Text = config.ServerExecutableFilename;
            CountDownTimerTextBox.Text = config.Countdown.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string gameID = GameIDTextBox.Text;
            string TabName = TabNameTextBox.Text;
            string extraArgs = ExtraArgsTextBox.Text;
            string serverDirectory = ServerDirTextBox.Text;
            string executableDir = ExecutableDirTextBox.Text;
            string serverExecutableFilename = ServerExeTextBox.Text;
            string countdownText = CountDownTimerTextBox.Text;

            if (int.TryParse(countdownText, out int countdownValue))
            {
                // Load existing configurations
                List<GameConfig> gameConfigs = GameConfigManager.LoadAllGameConfigs();

                // Check if a configuration with the same GameID already exists
                GameConfig existingConfig = gameConfigs.FirstOrDefault(config => config.GameID == gameID);

                if (existingConfig != null)
                {
                    // Update the existing configuration
                    existingConfig.Name = TabName;
                    existingConfig.ExtraArgs = extraArgs;
                    existingConfig.ServerDirectory = serverDirectory;
                    existingConfig.ExecutableDir = executableDir;
                    existingConfig.ServerExecutableFilename = serverExecutableFilename;
                    existingConfig.Countdown = countdownValue;
                }
                else
                {
                    // Create a new configuration
                    GameConfig newConfig = new GameConfig
                    {
                        GameID = gameID,
                        Name = TabName,
                        ExtraArgs = extraArgs,
                        ServerDirectory = serverDirectory,
                        ExecutableDir = executableDir,
                        ServerExecutableFilename = serverExecutableFilename,
                        Countdown = countdownValue
                    };

                    // Add the new configuration to the list
                    gameConfigs.Add(newConfig);
                }

                // Save all configurations
                GameConfigManager.SaveAllGameConfigs(gameConfigs);

                // Close the form
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Handle the case when the input is not a valid integer for countdown
                MessageBox.Show("Please enter a valid integer for the countdown.");
            }
        }
    }
}
