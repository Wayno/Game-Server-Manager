using Newtonsoft.Json;
using System.Diagnostics;
using System.Management;
using GameServerManager.Properties;
using Timer = System.Windows.Forms.Timer;
using static GameServerManager.TabSettingsForm;

namespace GameServerManager
{
    public partial class Form1 : Form
    {
        private int counter;
        private bool is_server_running = false;
        private DebugForm debugForm;
        public readonly string encryptionKey = "MOS8cGhXtz2M5t0kfpJUINLaMKkDAjgq"; // Hardcode for now, when released move it to get the KEY from userdatabase for more security.

        Dictionary<string, Timer> tabTimers = new Dictionary<string, Timer>();
        private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        private Dictionary<string, int> tabCountdowns = new Dictionary<string, int>();
        private Dictionary<string, GameConfig> gameConfigs = new Dictionary<string, GameConfig>();
        private Dictionary<string, bool> timerRunningStates = new Dictionary<string, bool>();
        private Dictionary<string, bool> serverShutdownInitiated = new Dictionary<string, bool>();
        private Dictionary<string, bool> updateChecks = new Dictionary<string, bool>();
        private Dictionary<string, bool> forceNextRuns = new Dictionary<string, bool>();

        public Form1()
        {
            InitializeComponent();
            gameConfigs = LoadGameConfigsIntoDictionary();
            debugForm = new DebugForm();
            this.Icon = new Icon("icon.ico");
            this.Text = "Game Server Manager";
            GameConfigManager.LoadAllGameConfigs();
            gamesTabControl.SelectedIndexChanged += GamesTabControl_SelectedIndexChanged;
            this.Load += new EventHandler(Form1_Load);
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
        public void UpdateButtonStateBasedOnGameID()
        {
        }

        private Dictionary<string, GameConfig> LoadGameConfigsIntoDictionary()
        {
            var gameConfigsList = GameConfigManager.LoadAllGameConfigs();
            var gameConfigsDictionary = new Dictionary<string, GameConfig>();

            foreach (var config in gameConfigsList)
            {
                if (!gameConfigsDictionary.ContainsKey(config.GameID))
                {
                    gameConfigsDictionary.Add(config.GameID, config);
                }
            }

            return gameConfigsDictionary;
        }
        public void updateloadedconfig(object? sender, EventArgs e)
        {
            // Load all configurations using the GameConfigManager
            var loadedConfigs = GameConfigManager.LoadAllGameConfigs();

            // Clear existing tabs and associated data structures
            gamesTabControl.TabPages.Clear();
            tabTimers.Clear();
            timers.Clear();
            tabCountdowns.Clear();
            gameConfigs.Clear();
            timerRunningStates.Clear();
            serverShutdownInitiated.Clear();
            updateChecks.Clear();
            forceNextRuns.Clear();

            // Reload the configurations into the dictionaries
            foreach (var config in loadedConfigs)
            {
                AddGameTab(config); // Re-add the tabs for each configuration
                gameConfigs[config.GameID] = config;
            }

            // Reset the selected tab to the first one (if available)
            if (gamesTabControl.TabPages.Count > 0)
            {
                gamesTabControl.SelectedTab = gamesTabControl.TabPages[0];
            }
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
                        timerRunningStates[config.GameID] = true;
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
                        timerRunningStates[config.GameID] = false;
                        forceNextRuns[config.GameID] = false;
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
                if (IsTimerRunning(gameID) && counter == 0)
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
            if (IsServerUpdateNeeded(gameID))
            {
                UpdateServer(gameID);
            }
            else
            {
                LogServerStatus(gameID);
            }
        }

        private bool IsServerUpdateNeeded(string gameID)
        {
            // Ensure the gameID exists in the dictionary or assume false as default
            bool updateCheck = updateChecks.ContainsKey(gameID) && updateChecks[gameID];
            bool forceNextRun = forceNextRuns.ContainsKey(gameID) && forceNextRuns[gameID];
            return !IsServerRunning() && !updateCheck && !forceNextRun;
        }

        private void LogServerStatus(string gameID)
        {
            bool updateCheck = updateChecks.ContainsKey(gameID) ? updateChecks[gameID] : false;
            bool forceNextRun = forceNextRuns.ContainsKey(gameID) ? forceNextRuns[gameID] : false;


            if (!IsServerRunning())
            {
                UpdateCountdownForTab(gameID, 30);
            }

            if (IsTimeToRestart(gameID) && AutoRestartEnabled(gameID))
            {
                ShutdownServer(gameID);
            }

            if (IsServerRunning())
            {
                LogToTab(gameID, "Server is currently running.");
            }
            else if (!updateCheck && !forceNextRun)
            {
                LogToTab(gameID, "Server update check is in progress.");
                CheckAndUpdateServer(gameID);
            }
            else if (forceNextRun && !updateCheck)
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
            timerRunningStates[gameID] = true;
            timer.Start();

            // Store the new timer in the dictionary
            timers[gameID] = timer;

            // Additional logic to handle starting the server watchdog
            timerRunningStates[gameID] = true;
            counter = 1;

            // Update the UI or button text here if needed
            UpdateStartButtonOnSelectedTab("Stop");
            LogServerStatus(gameID);
        }

        private bool IsTimeToRestart(string gameID)
        {
            if (!gameConfigs.TryGetValue(gameID, out GameConfig config))
            {
                return false; // Configuration not found for gameID
            }

            // Assuming 30 is an indicator of unset or invalid values,
            // and setting some sensible defaults (e.g., midnight).
            // Adjust these default values as necessary.
            int defaultRestartHour = Properties.Settings.Default.RestartHour;
            int defaultRestartMinute = Properties.Settings.Default.RestartMinute;

            // Assigning config values or defaults
            int restartHour = config.RestartHour != 30 ? config.RestartHour : defaultRestartHour;
            int restartMinute = config.RestartMinute != 30 ? config.RestartMinute : defaultRestartMinute;

            DateTime now = DateTime.Now;

            // Calculate the end minute and possibly adjust the end hour
            int endMinute = restartMinute + 5;
            int endHour = restartHour;
            if (endMinute > 59)
            {
                endMinute -= 60; // Adjust minute back into range
                endHour = (endHour + 1) % 24; // Adjust hour, wrap around if needed
            }

            // Check if the current time is within the restart window for this specific game/server
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

        private bool AutoRestartEnabled(string gameID)
        {
            // Attempt to retrieve the game configuration for the given gameID
            if (gameConfigs.TryGetValue(gameID, out GameConfig config))
            {
                // Return the AutoRestartEnabled property of the found configuration
                return config.AutoRestartEnabled;
            }

            // If the game configuration was not found, default to false
            return false;
        }
        public List<int> GetProcessTree(int pid)
        {
        List<int> processIds = new List<int>();
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(
            "SELECT * FROM Win32_Process WHERE ParentProcessId=" + pid);

            foreach (ManagementObject obj in searcher.Get())
            {
             int childPid = Convert.ToInt32(obj["ProcessId"]);
                processIds.Add(childPid);

                // Recursively find other children
                processIds.AddRange(GetProcessTree(childPid));
            }

            return processIds;
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
        /*private bool IsServerRunning(string gameID)
        {
            if (gameConfigs.TryGetValue(gameID, out GameConfig config))
            {
                string processName = Path.GetFileNameWithoutExtension(config.ServerExecutableFilename);
                Process[] processes = Process.GetProcessesByName(processName);

                foreach (var process in processes)
                {
                    // If the main process is found, the server is considered running.
                    //LogToTab(gameID, $"Found main process: {processName}");
                    return true;
                }

                // If the main process isn't found, check for any running child processes
                foreach (var process in Process.GetProcesses())
                {
                    if (GetProcessTree(process.Id).Count > 0)
                    {
                        //LogToTab(gameID, $"Found child processes for: {processName}");
                        return true; // Found child processes, server is considered running
                    }
                }

                LogToTab(gameID, $"Not Running: {processName}");
                return false;
            }

            LogToTab(gameID, $"Is Server Running failed to get gameID: {gameID}");
            return false;
    }*/
    public void UpdateServer(string gameID)
        {
            bool updateCheck = updateChecks.ContainsKey(gameID) ? updateChecks[gameID] : false;
            bool forceNextRun = forceNextRuns.ContainsKey(gameID) ? forceNextRuns[gameID] : false;

            if (gamesTabControl.SelectedTab?.Tag is GameConfig config && !updateCheck)
            {
                LogToTab(config.GameID, "Checking for server update...");

                Task.Run(() =>
                {
                    updateChecks[gameID] = true;

                    string steamCmdPath = Path.Combine(Settings.Default.SteamCMDDirectory, "steamcmd.exe");
                    string arguments = $"+force_install_dir \"{config.ServerDirectory}\" +login anonymous +app_update {config.GameID} validate +quit";

                    //LogToTab(config.GameID, $"{steamCmdPath}, {arguments}");

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

                        updateChecks[gameID] = false; // Reset the flag after the process completes
                        forceNextRuns[gameID] = true;
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            LogToTab(config.GameID, "Failed to start the update process.");
                        });
                        updateChecks[gameID] = false; // Reset the flag if the process couldn't start
                    }
                });
            }
            else if (updateCheck)
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
                    serverShutdownInitiated[gameID] = false;

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
                            updateChecks[gameID] = false;  // Set UpdateCheck to false as the server has started
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

        private async void ShutdownServer(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                LogToTab(config.GameID, "Attempting to shut down");

                if (serverShutdownInitiated.TryGetValue(gameID, out bool initiated) && initiated)
                {
                    // Shutdown has already been initiated, no need to proceed
                    return;
                }

                serverShutdownInitiated[gameID] = true;

                await Task.Run(async () =>
                {
                    if (!string.IsNullOrEmpty(config.rconIP))
                    {
                        string decryptedPassword = EncryptionHelper.DecryptString(encryptionKey, config.rconPassword);
                        string rconCommandsave = $"echo save | ARRCON.exe -H {config.rconIP} -P {config.rconPort} -p {decryptedPassword}";
                        ExecuteCommand(rconCommandsave);
                        LogToTab(config.GameID, "Waiting 60 secs to make sure server saved the world");
                        tabTimers[gameID].Stop();
                        UpdateCountdownForTab(gameID, 60);
                        tabTimers[gameID].Start();
                        await Task.Delay(60000); // 60 seconds
                        UpdateCountdown(gameID);

                        LogToTab(config.GameID, "Sending Exit Command");

                        string rconCommanddoexit = $"echo doexit | ARRCON.exe -H {config.rconIP} -P {config.rconPort} -p {decryptedPassword}";
                        ExecuteCommand(rconCommanddoexit);
                    }
                });

                LogToTab(config.GameID, "Waiting 30 secs to stop repeating shutdown.");
                UpdateCountdownForTab(gameID, 30);
                await Task.Delay(30000); // 60 seconds
            }
            else
            {
                LogToTab(gameID, "No game tab selected or game configuration missing.");
            }
        }

        private async void SaveandExit(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                LogToTab(config.GameID, "Attempting to shut down");

                await Task.Run(async () =>
                {
                    if (!string.IsNullOrEmpty(config.rconIP))
                    {
                        LogToTab(config.GameID, "Sending save command");
                        string decryptedPassword = EncryptionHelper.DecryptString(encryptionKey, config.rconPassword);
                        string rconCommandsave = $"echo save | ARRCON.exe -H {config.rconIP} -P {config.rconPort} -p {decryptedPassword}";
                        ExecuteCommand(rconCommandsave);
                        LogToTab(config.GameID, "Waiting 60 secs to make sure server saved the world");
                        StopTimersForTab(config.GameID);
                        UpdateCountdownForTab(gameID, 60);
                        tabTimers[gameID].Start();
                        await Task.Delay(60000); // 60 seconds
                        UpdateCountdown(gameID);

                        LogToTab(config.GameID, "Sending Exit Command");

                        string rconCommanddoexit = $"echo doexit | ARRCON.exe -H {config.rconIP} -P {config.rconPort} -p {decryptedPassword}";
                        ExecuteCommand(rconCommanddoexit);
                        LogToTab(config.GameID, "Waiting 60 secs to make sure server exists");
                        UpdateCountdownForTab(gameID, 60);
                        tabTimers[gameID].Start();
                        await Task.Delay(60000); // 60 seconds

                        LogToTab(config.GameID, "Server saved and exited");
                        Stop();
                    }
                    else
                    {
                        LogToTab(config.GameID, "Will do ctrl+c for on games that use it.");
                    }
                });
            }
            else
            {
                LogToTab(gameID, "No game tab selected or game configuration missing.");
            }
        }

        private void ForceUpdate(string gameID)
        {
            if (gamesTabControl.SelectedTab?.Tag is GameConfig config)
            {
                updateChecks[gameID] = false; // Reset the flag after the process completes
                forceNextRuns[gameID] = false;
                LogToTab(config.GameID, "Making sure game isnt running");

                if (IsServerRunning())
                {
                    ShutdownServer(gameID);
                }

                UpdateServer(gameID);
            }
        }

        private void ExecuteCommand(string command)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process proc = new Process { StartInfo = procStartInfo })
                {
                    proc.Start();
                    string result = proc.StandardOutput.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error executing command: " + ex.Message);
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
            public required string rconIP { get; set; }
            public required string rconPort { get; set; }
            public required string rconPassword { get; set; }
            public int RestartHour { get; set; }
            public int RestartMinute { get; set; }
            public bool AutoRestartEnabled { get; set; }
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
        public void AddGameTab(GameConfig config)
        {
            var tabPage = new TabPage(config.GameID)
            {
                Name = $"tabPage{config.GameID}",
                Text = config.Name,
                Tag = config // Storing the entire config object for later use
            };

            // Timer setup
            var timer = new Timer { Interval = 1000 }; // 1 second
            timer.Tick += (sender, e) => UpdateCountdown(config.GameID);

            // Add the timer to the tabPage
            tabTimers[config.GameID] = timer;

            // Create the delete button
            var deleteButton = new Button
            {
                Text = "Delete Tab",
                Location = new Point(435, 310),
                Size = new Size(70, 20)
            };

            deleteButton.Click += deleteTabButton_Click;

            // Start Button
            var Start_Button = new Button
            {
                Text = "Start",
                Name = $"StartButton{config.GameID}",
                Location = new Point(60, 9),
                Size = new Size(50, 25)
            };

            Start_Button.Click += Start_Button_Click;

            // Shutdown Button
            var Shutdown_Button = new Button
            {
                Text = "Save & Exit",
                Name = $"ShutdownButton{config.GameID}",
                Location = new Point(110, 9),
                Size = new Size(70, 25)
            };

            Shutdown_Button.Click += (sender, e) => SaveandExit(config.GameID);

            // Force Update Button
            var ForceUpdate_Button = new Button
            {
                Text = "Force Update",
                Name = $"ForceUpdateButton{config.GameID}",
                Location = new Point(187, 9),
                Size = new Size(90, 25)
            };

            ForceUpdate_Button.Click += (sender, e) => ForceUpdate(config.GameID);

            // Tab Settings Button
            var TabSettings_Button = new Button
            {
                Text = "Settings",
                Location = new Point(440, 10),
                Size = new Size(60, 25)
            };

            TabSettings_Button.Click += TabSettings_Click;

            // Countdown Textbox
            var Countdown = new TextBox
            {
                Name = $"CountdownTextBox{config.GameID}",
                Text = config.Countdown.ToString(), // Convert Countdown integer to string
                Location = new Point(10, 10),
                Size = new Size(50, 20),
                ReadOnly = true
            };

            // Log RichTextBox
            var logRichTextBox = new RichTextBox
            {
                Name = $"logRichTextBox{config.GameID}",
                ReadOnly = true,
                Location = new Point(10, 45),
                Size = new Size(470, 260),
                Multiline = true
            };

            // Add controls to the tabPage
            tabPage.Controls.Add(deleteButton);
            tabPage.Controls.Add(Start_Button);
            tabPage.Controls.Add(Shutdown_Button);
            tabPage.Controls.Add(ForceUpdate_Button);
            tabPage.Controls.Add(TabSettings_Button);
            tabPage.Controls.Add(Countdown);
            tabPage.Controls.Add(logRichTextBox);

            // Add the new tab page to the TabControl
            gamesTabControl.TabPages.Add(tabPage);

            // Automatically select the newly added tab page
            gamesTabControl.SelectedTab = tabPage;
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
                timerRunningStates[gameID] = false;
            }

            if (gamesTabControl.TabPages.ContainsKey($"tabPage{gameID}"))
            {
                gamesTabControl.TabPages.RemoveByKey($"tabPage{gameID}");
            }
        }

        private bool IsTimerRunning(string gameID)
        {
            if (timerRunningStates.TryGetValue(gameID, out bool isRunning))
            {
                return isRunning;
            }
            return false; // Default to false if the game ID isn't found
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
                StartAllGamesAsync();
                StartStopAllButton.Text = "Stop All"; // Update the button text to "Stop All"
                areServersRunning = true;
            }
        }
        private async void StartAllGamesAsync()
        {
            foreach (var kvp in gameConfigs)
            {
                string gameID = kvp.Key;
                GameConfig config = kvp.Value;

                if (!IsServerRunning(config))
                {
                    await StartGameAsync(config);
                }
            }

            StartStopAllButton.Text = "Stop";
        }

        private bool IsServerRunning(GameConfig config)
        {
            // Extract the executable name from the ServerExecutableFilename
            string executableName = Path.GetFileNameWithoutExtension(config.ServerExecutableFilename);

            // Get all processes by the executable name
            Process[] processes = Process.GetProcessesByName(executableName);

            // If any processes are found, the server is considered to be running
            return processes.Length > 0;
        }

        private async Task StartGameAsync(GameConfig config)
        {
            await Task.Run(() =>
            {
                try
                {
                    // Check if the server is already running
                    if (IsServerRunning(config))
                    {
                        LogToTab(config.GameID, "Server is already running.");
                        return;
                    }

                    // Create a new process start info
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = config.ServerExecutableFilename,
                        Arguments = config.ExtraArgs,
                        WorkingDirectory = config.ExecutableDir, // Or config.ServerDirectory, depending on your needs
                        UseShellExecute = false,
                        CreateNoWindow = false, // Set to false if you want to see the window
                        RedirectStandardOutput = true, // Set to true if you want to capture output (depends on your requirements)
                    };

                    // Start the server process
                    using (Process process = Process.Start(startInfo))
                    {
                        if (process != null)
                        {
                            LogToTab(config.GameID, "Server started successfully.");
                            // Optionally read the output
                            string output = process.StandardOutput.ReadToEnd();
                            LogToTab(config.GameID, output);
                        }
                        else
                        {
                            LogToTab(config.GameID, "Failed to start the server process.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogToTab(config.GameID, $"Error starting server: {ex.Message}");
                }
            });
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

        private void StopTimersForTab(string gameID)
        {
            if (tabTimers.TryGetValue(gameID, out Timer timer))
            {
                timer.Stop();
                // If you also want to dispose of the timer, uncomment the following line:
                // timer.Dispose();
            }
        }
    }
}