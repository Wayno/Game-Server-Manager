using Newtonsoft.Json;
using System.Diagnostics;
using WinFormsApp1.Properties;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int counter;
        private bool isTimerRunning = false;
        private bool UpdateCheck = false;
        private bool Forcenextrun = false;
        private bool is_server_running;
        private DebugForm debugForm;


        Dictionary<string, Timer> tabTimers = new Dictionary<string, Timer>();
        private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        Dictionary<string, GameConfig> gameConfigs = new Dictionary<string, GameConfig>();
        private Dictionary<string, int> tabCountdowns = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
            debugForm = new DebugForm();
            this.Icon = new Icon("icon.ico");
            this.Text = "Game Server Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            GameConfigManager.LoadAllGameConfigs();
            gamesTabControl.SelectedIndexChanged += GamesTabControl_SelectedIndexChanged;
        }

        private void GamesTabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Stop all timers and start only for the selected tab
            foreach (TabPage page in gamesTabControl.TabPages)
            {
                if (page.Tag is TabData tabData)
                {
                    tabData.Timer.Stop();
                }
            }

            if (gamesTabControl.SelectedTab?.Tag is TabData selectedTabData)
            {
                selectedTabData.Timer.Start();
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            //debugForm.Show();

            var gameConfigs = GameConfigManager.LoadAllGameConfigs();

            foreach (var config in gameConfigs)
            {
                AddGameTab(config);
            }
        }


        private void OpenVariableInputForm()
        {
            VariableInputForm variableInputForm = new VariableInputForm(this);
            variableInputForm.ShowDialog();
        }

        public void UpdateButtonStateBasedOnGameID()
        {
        }


        private void Start_Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Parent is TabPage tabPage && tabPage.Tag is GameConfig config)
            {
                if (tabTimers.TryGetValue(config.GameID, out var timer))
                {
                    if (timers.ContainsKey(config.GameID) && timers[config.GameID].Enabled)
                    {
                        Stop();
                    }
                    else
                    {
                        // Start the timer for this tab
                        isTimerRunning = true;
                        StartServerWatchdog(tabPage, config.GameID);
                    }
                }
            }
        }

        private void Stop()
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig)
            {
                TabPage selectedTab = gamesTabControl.SelectedTab;

                if (selectedTab.Tag is GameConfig config)
                {
                    if (timers.ContainsKey(config.GameID) && timers[config.GameID] != null)
                    {
                        Timer timer = timers[config.GameID];
                        timer.Stop();
                        timer.Dispose(); // Dispose of the timer
                        timers.Remove(config.GameID); // Remove it from the dictionary

                        LogToTab(config.GameID, $"Stopping main loop for game {config.GameID}");
                        isTimerRunning = false;
                        is_server_running = false;
                        Forcenextrun = false;
                        UpdateStartButtonOnSelectedTab("Start");
                    }
                }
            }
        }
        private void Timer1_Tick(object? sender, EventArgs e, TabPage tabPage, string gameID)
        {
            if (sender != null)
            {
                if (string.IsNullOrEmpty(gameID))
                {
                    LogToTab(gameID, "No game tab selected or game configuration missing.");
                    return;
                }

                counter--;
                if (isTimerRunning == true && counter == 0)
                {
                    LogServerStatus(gameID);
                }

                // Update the countdown textbox in the selected tab
                if (gamesTabControl.SelectedTab != null)
                {
                    _ = gamesTabControl.SelectedTab.Name.Replace("tabPage", "");
                    UpdateCountdown(gameID);
                }
            }
            //debugForm?.AppendDebugInfo($"Timer1_Tick for {gameID} executed at {DateTime.Now}");
        }

        private void CheckAndUpdateServer(string gameID)
        {
            if (IsServerUpdateNeeded())
            {
                PerformServerUpdate(gameID);
            }
            else
            {
                LogServerStatus(gameID);
            }
        }

        private bool IsServerUpdateNeeded()
        {
            return !IsServerRunning() && !UpdateCheck && !Forcenextrun;
        }

        private void PerformServerUpdate(string gameID)
        {
            LogToTab(gameID, "Server is not running, doing steamcmd stuff");
            UpdateServer(gameID); // Check and update server when needed
        }

        private void LogServerStatus(string gameID)
        {
            if (!IsServerRunning())
            {
                UpdateCountdownForTab(gameID, 30);
            }

            if (IsTimeToRestart())
            {
                ShutdownServer(gameID);
            }

            if (IsServerRunning())
            {
                LogToTab(gameID, "Server is currently running.");
            }
            else if (!UpdateCheck && !Forcenextrun)
            {
                LogToTab(gameID, "Server update check is in progress.");
                CheckAndUpdateServer(gameID);
            }
            else if (Forcenextrun && !UpdateCheck)
            {
                StartServer(gameID);
            }
            else
            {
                LogToTab(gameID, "Nothing found to do, repeating loop.");
            }
        }


        private void StartServerWatchdog(TabPage tabPage, string gameID)
        {
            LogToTab(gameID, $"Starting main loop for game: {gameID}");

            // Check if a timer already exists for this gameID and stop it if it does
            if (timers.TryGetValue(gameID, out var existingTimer))
            {
                existingTimer.Stop();
                existingTimer.Dispose();
            }

            // Create a new timer for this specific gameID
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) => Timer1_Tick(sender, e, tabPage, gameID);
            timer.Start();

            // Store the new timer in the dictionary
            timers[gameID] = timer;

            // Additional logic to handle starting the server watchdog
            isTimerRunning = true;
            is_server_running = false;
            counter = 1;

            // Update the UI or button text here if needed
            UpdateStartButtonOnSelectedTab("Stop");
            LogServerStatus(gameID);
        }

        private bool IsTimeToRestart()
        {
            DateTime now = DateTime.Now;

            int restartHour = Properties.Settings.Default.RestartHour;
            int restartMinute = Properties.Settings.Default.RestartMinute;

            // Calculate the end minute and possibly adjust the end hour
            int endMinute = restartMinute + 5;
            int endHour = restartHour;
            if (endMinute > 59)
            {
                endMinute -= 60; // Adjust minute back into range
                endHour = (endHour + 1) % 24; // Adjust hour, wrap around if needed
            }

            // Check if the current time is within the restart window
            if (now.Hour == restartHour && now.Minute >= restartMinute)
            {
                // Case where end time is within the same hour
                return now.Hour != endHour || now.Minute <= endMinute;
            }
            else if (now.Hour == endHour && now.Minute <= endMinute)
            {
                // Case where end time wraps to the next hour
                return true;
            }

            return false; // Not time to restart
        }

        private bool IsServerRunning()
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                // Extract just the filename without the extension from the config
                string processName = Path.GetFileNameWithoutExtension(config.ServerExecutableFilename);

                Process[] pname = Process.GetProcessesByName(processName);
                return pname.Length != 0;
            }

            return false; // Return false or handle appropriately if no tab is selected or no config is found
        }

        private void UpdateServer(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config && !UpdateCheck)
            {
                LogToTab(config.GameID, "Checking for server update...");

                Task.Run(() =>
                {
                    UpdateCheck = true;

                    string steamCmdPath = Path.Combine(Settings.Default.SteamCMDDirectory, "steamcmd.exe");
                    string arguments = $"+force_install_dir \"{config.ServerDirectory}\" +login anonymous +app_update {config.GameID} validate +quit";

                    var process = Process.Start(steamCmdPath, arguments);

                    if (process != null)
                    {
                        process.WaitForExit();
                        int exitCode = process.ExitCode;
                        // Handle the exit code or continue with your code

                        this.Invoke((MethodInvoker)delegate
                        {
                            // Update UI here based on exitCode
                        });

                        UpdateCheck = false; // Reset the flag after the process completes
                        Forcenextrun = true;
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            LogToTab(config.GameID, "Failed to start the update process.");
                        });
                        UpdateCheck = false; // Reset the flag if the process couldn't start
                    }
                });
            }
            else if (UpdateCheck)
            {
                LogToTab(gameID, "An update is already in progress.");
            }
            else
            {
                LogToTab(gameID, "No game tab selected or game configuration missing.");
            }
        }


        private void StartServer(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = config.ServerExecutableFilename,
                        Arguments = config.ExtraArgs,
                        UseShellExecute = false,
                    };

                    using (var process = Process.Start(startInfo))
                    {
                        if (process == null)
                        {
                            LogToTab(config.GameID, "Failed to start the server process.");
                        }
                        else
                        {
                            // The process has started, but you might want to wait for the server to be ready.
                            // Consider implementing a check to confirm the server is fully operational.
                            LogToTab(config.GameID, "Server process started successfully.");
                            UpdateCheck = false;  // Set UpdateCheck to false as the server has started
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogToTab(config.GameID, $"Failed to start server: {ex.Message}");
                }
            }
            else
            {
                LogToTab(gameID, "No game tab selected or game configuration missing.");
            }
        }

        private void ShutdownServer(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                string processName = Path.GetFileNameWithoutExtension(config.ServerExecutableFilename);

                LogToTab(config.GameID, $"Attempting to shut down {processName}...");

                foreach (var process in Process.GetProcessesByName(processName))
                {
                    try
                    {
                        process.Kill();
                        LogToTab(config.GameID, $"Shut down process {processName}.");
                    }
                    catch (Exception ex)
                    {
                        LogToTab(config.GameID, $"Failed to shut down process {processName}: {ex.Message}");
                    }
                }

                LogToTab(config.GameID, "Waiting 60 secs to stop repeating shutdown.");
                Thread.Sleep(60000); // 60 seconds
            }
            else
            {
                LogToTab(gameID, "No game tab selected or game configuration missing.");
            }
        }

        private void LogToTab(string gameID, string message)
        {
            string tabPageName = $"tabPage{gameID}";
            var tabPage = gamesTabControl.Controls.Find(tabPageName, true).FirstOrDefault() as TabPage;
            if (tabPage != null)
            {
                var logRichTextBox = tabPage.Controls[$"logRichTextBox{gameID}"] as RichTextBox;
                if (logRichTextBox != null)
                {
                    if (logRichTextBox.InvokeRequired)
                    {
                        logRichTextBox.Invoke(new Action(() => LogToTab(gameID, message)));
                        return;
                    }
                    logRichTextBox.AppendText($"{DateTime.Now:dd-MM-yyy HH:mm:ss} {message}\n");
                    logRichTextBox.ScrollToCaret();
                }
            }
        }

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            using (var variableInputForm = new VariableInputForm(this))
            {
                if (variableInputForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateButtonStateBasedOnGameID();
                }
            }
        }
        // THis is AddGame start stuff.
        private void AddGame_Click(object sender, EventArgs e)
        {
            using (var addGameForm = new AddGame(this, null)) // Pass null if AddGame form can handle it
            {
                if (addGameForm.ShowDialog() == DialogResult.OK)
                {
                    // Update the button state based on the GameID setting
                    UpdateButtonStateBasedOnGameID();
                }
            }
        }

        public class GameConfig
        {
            public required string GameID { get; set; }
            public required string Name { get; set; }
            public required string ServerExecutableFilename { get; set; }
            public required string ServerDirectory { get; set; }
            public required string ExecutableDir { get; set; }
            public required string ExtraArgs { get; set; }
            public int Countdown { get; set; }
        }

        public class GameConfigManager
        {
            private const string ConfigFilePath = "gameConfigs.json";

            public static void SaveGameConfig(GameConfig config)
            {
                List<GameConfig> configs = LoadAllGameConfigs();
                configs.Add(config);
                string json = JsonConvert.SerializeObject(configs, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
            }

            public static void SaveAllGameConfigs(List<GameConfig> gameConfigs)
            {
                string json = JsonConvert.SerializeObject(gameConfigs, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
            }

            public static List<GameConfig> LoadAllGameConfigs()
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    return JsonConvert.DeserializeObject<List<GameConfig>>(json);
                }
                return new List<GameConfig>();
            }
        }

        public void CreateTabsForAllGames()
        {
            var gameConfigs = GameConfigManager.LoadAllGameConfigs();
            foreach (var config in gameConfigs)
            {
                AddGameTab(config);
            }
        }

        public void AddGameTab(GameConfig config)
        {
            var tabPage = new TabPage(config.GameID)
            {
                Name = $"tabPage{config.GameID}",
                Text = config.Name,
                Tag = config  // Storing the entire config object for later use
            };

            var timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) => UpdateCountdown(config.GameID); // Assign the event handler to update countdown

            // Add the timer to the tabPage
            tabTimers[config.GameID] = timer;

            // Create the delete button
            var deleteButton = new Button
            {
                Text = "Delete Tab",
                Location = new Point(300, 310), // Set location as needed
                Size = new Size(50, 20) // Set size as needed
            };

            deleteButton.Click += deleteTabButton_Click; // Assign the event handler

            tabPage.Controls.Add(deleteButton);

            //Start_Button
            var Start_Button = new Button
            {
                Text = "Start Button",
                Name = $"StartButton{config.GameID}", // Unique name for each button
                Location = new Point(60, 9), // Set location as needed
                Size = new Size(50, 25) // Set size as needed
            };

            Start_Button.Click += Start_Button_Click; // Assign the event handler

            tabPage.Controls.Add(Start_Button);

            //TabSettings_Button
            var TabSettings = new Button
            {
                Text = "Settings",
                Location = new Point(290, 10), // Set location as needed
                Size = new Size(60, 25) // Set size as needed
            };

            TabSettings.Click += TabSettings_Click; // Assign the event handler

            tabPage.Controls.Add(TabSettings);

            //Countdown Textbox
            var Countdown = new TextBox
            {
                Name = $"CountdownTextBox{config.GameID}",
                //Text = $"{config.Countdown}", // Initialize with 60 or any valid integer value
                Text = Settings.Default.Global_Timer.ToString(),
                Location = new Point(10, 10),
                Size = new Size(50, 20),
                ReadOnly = true
            };

            tabPage.Controls.Add(Countdown);

            //Log box
            var logRichTextBox = new RichTextBox
            {
                Name = $"logRichTextBox{config.GameID}",
                ReadOnly = true,
                Location = new Point(10, 45),
                Size = new Size(320, 260), //Lenght, Heeght
                Multiline = true
            };
            tabPage.Controls.Add(logRichTextBox);

            //Leave at end
            gamesTabControl.TabPages.Add(tabPage);

        }
        public class TabData
        {
            public required System.Windows.Forms.Timer Timer { get; set; }
        }

        private void UpdateStartButtonOnSelectedTab(string newText)
        {
            TabPage selectedTab = gamesTabControl.SelectedTab;
            if (selectedTab != null && selectedTab.Tag is GameConfig config)
            {
                string buttonName = $"StartButton{config.GameID}";
                if (selectedTab.Controls.ContainsKey(buttonName))
                {
                    Button startButton = selectedTab.Controls[buttonName] as Button;
                    if (startButton != null)
                    {
                        startButton.Text = newText;
                    }
                }
            }
        }

        private void LoadGameConfigToUI(GameConfig config)
        {
            // Use the config object to set up the UI elements in the selected tab
            // For example, displaying game info in text boxes, labels, etc.
        }

        // Settings shit.
        private void TabSettings_Click(object? sender, EventArgs e)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig selectedGameConfig)
            {
                using (TabSettingsForm tabSettingsForm = new TabSettingsForm(this, selectedGameConfig.GameID))
                {
                    tabSettingsForm.LoadGameConfig(selectedGameConfig);
                    tabSettingsForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a game tab first.");
            }
        }
        private void deleteTabButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Parent is TabPage tabPage)
            {
                if (tabPage.Tag is GameConfig config)
                {
                    // Call method to delete the GameConfig data
                    DeleteGameConfigData(config);

                    // Remove the tab page
                    gamesTabControl.TabPages.Remove(tabPage);
                }
                else
                {
                    MessageBox.Show("Game configuration data not found.");
                }
            }
        }

        private void DeleteGameConfigData(GameConfig configToDelete)
        {
            var gameConfigs = GameConfigManager.LoadAllGameConfigs();
            var configToRemove = gameConfigs.FirstOrDefault(c => c.GameID == configToDelete.GameID);

            if (configToRemove != null)
            {
                gameConfigs.Remove(configToRemove);

                // Save the updated list back to the file
                GameConfigManager.SaveAllGameConfigs(gameConfigs);
            }
        }
        private void CloseTab(string gameID)
        {
            if (tabTimers.ContainsKey(gameID))
            {
                tabTimers[gameID].Stop();
                tabTimers[gameID].Dispose();
                tabTimers.Remove(gameID);
            }

            if (gamesTabControl.TabPages.ContainsKey($"tabPage{gameID}"))
            {
                gamesTabControl.TabPages.RemoveByKey($"tabPage{gameID}");
            }
        }



        private void UpdateCountdown(string gameID)
        {
            if (gamesTabControl.SelectedTab != null)
            {
                string tabPageName = $"tabPage{gameID}";
                var tabPage = gamesTabControl.Controls.Find(tabPageName, true).FirstOrDefault() as TabPage;
                var countdownTextBox = tabPage?.Controls[$"CountdownTextBox{gameID}"] as TextBox;

                if (countdownTextBox != null)
                {
                    // Check if there's a specific countdown in progress for this tab
                    if (tabCountdowns.TryGetValue(gameID, out int specificCountdown) && specificCountdown > 0)
                    {
                        countdownTextBox.Text = $"{specificCountdown}";
                        tabCountdowns[gameID]--;
                    }
                    else
                    {
                        // No specific countdown, proceed with regular countdown logic
                        if (int.TryParse(countdownTextBox.Text, out int countdownValue) && countdownValue > 0)
                        {
                            countdownValue--;
                        }
                        else
                        {
                            if (gameConfigs.TryGetValue(gameID, out var config))
                            {
                                countdownValue = config.Countdown;
                            }
                            else
                            {
                                countdownValue = Properties.Settings.Default.Global_Timer;
                            }
                            LogServerStatus(gameID); // Call when the regular countdown resets
                        }
                        countdownTextBox.Text = countdownValue.ToString();
                    }
                }
            }
        }

        private bool areServersRunning = false; // Flag to track the servers' status

        private void StartStopAll_Click(object sender, EventArgs e)
        {
            if (areServersRunning)
            {
                StopAllServers();
                StartStopAllButton.Text = "Start All"; // Update the button text to "Start All"
                areServersRunning = false;
            }
            else
            {
                StartAllServers();
                StartStopAllButton.Text = "Stop All"; // Update the button text to "Stop All"
                areServersRunning = true;
            }
        }

        //private bool AreAllServersRunning()
        //{
        // Implement logic to determine if all servers or tasks are currently running
        // Return true if all are running; otherwise, false
        //}

        private void StartAllServers()
        {
            // Implement logic to start all servers or tasks
            foreach (var config in gameConfigs.Values)
            {
                StartServer(config);
            }
        }

        private void StopAllServers()
        {
            // Implement logic to stop all servers or tasks
            foreach (var config in gameConfigs.Values)
            {
                StopServer(config);
            }
        }

        private void StartServer(GameConfig config)
        {
            // Logic to start a single server or task
            // This might involve starting a process, a service, a timer, etc.
        }

        private void StopServer(GameConfig config)
        {
            // Logic to stop a single server or task
            // This might involve stopping a process, a service, a timer, etc.
        }

        // Dictionary to keep track of countdowns for each tab

        private void UpdateCountdownForTab(string gameID, int initialCountdown)
        {
            if (!tabCountdowns.ContainsKey(gameID))
            {
                tabCountdowns.Add(gameID, initialCountdown);
            }
            else
            {
                tabCountdowns[gameID] = initialCountdown;
            }

            // Now update the countdown display for this tab
            UpdateCountdown(gameID);
        }

        // Call this method periodically, e.g., using a Timer
        private void RefreshCountdowns()
        {
            foreach (var gameID in tabCountdowns.Keys.ToList())
            {
                UpdateCountdown(gameID);
            }
        }


    }
}