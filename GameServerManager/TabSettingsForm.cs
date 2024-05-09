using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameServerManager.Properties;
using static GameServerManager.Form1;

namespace GameServerManager
{
    public partial class TabSettingsForm : Form
    {
        private GameConfig currentGameConfig;
        private readonly string gameID;
        public readonly string encryptionKey = "MOS8cGhXtz2M5t0kfpJUINLaMKkDAjgq"; // For testing only

        public TabSettingsForm(Form1 form1, string gameID)
        {
            InitializeComponent();
            this.gameID = gameID;
            this.Icon = new Icon("icon.ico");
            MaskedTextBoxPasssword.UseSystemPasswordChar = true;
            btnShowPassword.Text = "Show"; // Initial text

            PopulateHours();
            PopulateMinutes();
        }

        private void TabSettingsForm_Load(object sender, EventArgs e)
        {
            List<GameConfig> gameConfigs = GameConfigManager.LoadAllGameConfigs();
            currentGameConfig = gameConfigs.FirstOrDefault(config => config.GameID == gameID);

            if (currentGameConfig != null)
            {
                LoadGameConfig(currentGameConfig);
            }
        }

        public void LoadGameConfig(GameConfig config)
        {
            currentGameConfig = config;
            GameIDTextBox.Text = config.GameID;
            TabNameTextBox.Text = config.Name;
            ExtraArgsTextBox.Text = config.ExtraArgs;
            ServerDirTextBox.Text = config.ServerDirectory;
            executableDirTextBox.Text = config.ExecutableDir;
            serverExeTextBox.Text = config.ServerExecutableFilename;
            CountDownTimerTextBox.Text = config.Countdown.ToString();
            TextBoxIP.Text = config.rconIP;
            TextBoxPort.Text = config.rconPort;
            MaskedTextBoxPasssword.Text = EncryptionHelper.DecryptString(encryptionKey, config.rconPassword);
            autoRestartCheckBox.Checked = config.AutoRestartEnabled;
            minutesComboBox.Text = config.RestartMinute.ToString("D2");
            hoursComboBox.Text = config.RestartHour.ToString("D2");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(CountDownTimerTextBox.Text, out int countdownValue) ||
                !int.TryParse(minutesComboBox.Text, out int minutesValue) ||
                !int.TryParse(hoursComboBox.Text, out int hoursValue))
            {
                MessageBox.Show("Please enter valid integers for the countdown, hours, and minutes.");
                return;
            }

            string encryptedPassword = EncryptionHelper.EncryptString(encryptionKey, MaskedTextBoxPasssword.Text);

            GameConfig config = new GameConfig
            {
                GameID = GameIDTextBox.Text,
                Name = TabNameTextBox.Text,
                ExtraArgs = ExtraArgsTextBox.Text,
                ServerDirectory = ServerDirTextBox.Text,
                ExecutableDir = executableDirTextBox.Text,
                ServerExecutableFilename = serverExeTextBox.Text,
                Countdown = countdownValue,
                rconIP = TextBoxIP.Text,
                rconPort = TextBoxPort.Text,
                rconPassword = encryptedPassword,
                AutoRestartEnabled = autoRestartCheckBox.Checked,
                RestartHour = hoursValue,
                RestartMinute = minutesValue
            };

            List<GameConfig> gameConfigs = GameConfigManager.LoadAllGameConfigs();
            GameConfig existingConfig = gameConfigs.FirstOrDefault(cfg => cfg.GameID == config.GameID);

            if (existingConfig != null)
            {
                int index = gameConfigs.IndexOf(existingConfig);
                gameConfigs[index] = config;
            }
            else
            {
                gameConfigs.Add(config);
            }

            GameConfigManager.SaveAllGameConfigs(gameConfigs);

            var mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            mainForm?.updateloadedconfig(sender, e);

            DialogResult = DialogResult.OK;
            Close();
        }



        private void btnShowPassword_Click_1(object sender, EventArgs e)
        {
            MaskedTextBoxPasssword.UseSystemPasswordChar = !MaskedTextBoxPasssword.UseSystemPasswordChar;
            btnShowPassword.Text = MaskedTextBoxPasssword.UseSystemPasswordChar ? "Show" : "Hide";
        }

        private void PopulateHours()
        {
            hoursComboBox.Items.Clear();
            for (int hour = 0; hour < 24; hour++)
            {
                hoursComboBox.Items.Add(hour.ToString("D2"));
            }
        }

        private void PopulateMinutes()
        {
            minutesComboBox.Items.Clear();
            for (int minute = 0; minute < 60; minute++)
            {
                minutesComboBox.Items.Add(minute.ToString("D2"));
            }
        }

        private void selectExecutableDirButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select the Server Directory To Install To";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    executableDirTextBox.Text = folderBrowserDialog.SelectedPath;

                    if (currentGameConfig != null)
                    {
                        currentGameConfig.ServerDirectory = folderBrowserDialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("Game configuration is not initialized.");
                    }
                }
            }
        }

        private void selectServerExeButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable files (*.exe)|*.exe";
                openFileDialog.Title = "Select the Server Executable";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    serverExeTextBox.Text = openFileDialog.FileName;

                    if (currentGameConfig != null)
                    {
                        currentGameConfig.ServerExecutableFilename = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Game configuration is not initialized.");
                    }
                }
            }
        }

        public static class EncryptionHelper
        {
            public static string EncryptString(string key, string plainText)
            {
                if (string.IsNullOrEmpty(plainText)) return string.Empty;

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
                if (string.IsNullOrEmpty(cipherText)) return string.Empty;

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
    }
}
