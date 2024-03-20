using static GameServerManager.Form1;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace GameServerManager
{
    public partial class TabSettingsForm : Form
    {
        private GameConfig currentGameConfig;
        private readonly string gameID;
        public readonly string encryptionKey = "MOS8cGhXtz2M5t0kfpJUINLaMKkDAjgq"; // Hardcode for now, when released move it to get the KEY from userdatabase for more security.

        public TabSettingsForm(Form1 form1, string gameID)
        {
            InitializeComponent();
            this.gameID = gameID;
            MaskedTextBoxPasssword.UseSystemPasswordChar = true;
            btnShowPassword.Text = "Show"; // Initial text
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
                    string decryptedPassword = EncryptionHelper.DecryptString(encryptionKey, currentGameConfig.rconPassword);

                    // Populate TextBox controls with the selected configuration's values
                    GameIDTextBox.Text = currentGameConfig.GameID;
                    TabNameTextBox.Text = currentGameConfig.Name;
                    ExtraArgsTextBox.Text = currentGameConfig.ExtraArgs;
                    ServerDirTextBox.Text = currentGameConfig.ServerDirectory;
                    ExecutableDirTextBox.Text = currentGameConfig.ExecutableDir;
                    ServerExeTextBox.Text = currentGameConfig.ServerExecutableFilename;
                    CountDownTimerTextBox.Text = currentGameConfig.Countdown.ToString();
                    TextBoxIP.Text = currentGameConfig.rconIP;
                    TextBoxPort.Text = currentGameConfig.rconPort;
                    MaskedTextBoxPasssword.Text = decryptedPassword;
                    autoRestartCheckBox.Checked = currentGameConfig.AutoRestartEnabled;
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
            TextBoxIP.Text = config.rconIP;
            TextBoxPort.Text = config.rconPort;
            string decryptedPassword = EncryptionHelper.DecryptString(encryptionKey, config.rconPassword);
            MaskedTextBoxPasssword.Text = decryptedPassword;
            autoRestartCheckBox.Checked = config.AutoRestartEnabled;

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
            string rconIP = TextBoxIP.Text;
            string rconPort = TextBoxPort.Text;
            string encryptedPassword = EncryptionHelper.EncryptString(encryptionKey, MaskedTextBoxPasssword.Text);
            bool autorestartCheckBox = autoRestartCheckBox.Checked;


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
                    existingConfig.rconIP = rconIP;
                    existingConfig.rconPort = rconPort;
                    existingConfig.Countdown = countdownValue;
                    existingConfig.rconPassword = encryptedPassword;
                    existingConfig.AutoRestartEnabled = autoRestartCheckBox.Checked;
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
                        Countdown = countdownValue,
                        rconIP = rconIP,
                        rconPort = rconPort,
                        rconPassword = encryptedPassword,
                        AutoRestartEnabled = autoRestartCheckBox.Checked
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

        private void btnShowPassword_Click(object sender, EventArgs e)
        {

        }

        public static class EncryptionHelper
        {
            public static string EncryptString(string key, string plainText)
            {
                if (string.IsNullOrEmpty(plainText))
                    return string.Empty;

                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array);
            }

            public static string DecryptString(string key, string cipherText)
            {
                if (string.IsNullOrEmpty(cipherText))
                    return string.Empty;

                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        private void btnShowPassword_Click_1(object sender, EventArgs e)
        {
            // Toggle the UseSystemPasswordChar property
            MaskedTextBoxPasssword.UseSystemPasswordChar = !MaskedTextBoxPasssword.UseSystemPasswordChar;

            // Optionally, change the button's image or text to indicate the current state
            if (MaskedTextBoxPasssword.UseSystemPasswordChar)
            {
                // If password is masked, show an "eye" icon or relevant text
                btnShowPassword.Text = "Show"; // or set an "eye-closed" icon
            }
            else
            {
                // If password is visible, show an "eye-slash" icon or relevant text
                btnShowPassword.Text = "Hide"; // or set an "eye" icon
            }
        }
    }
}
