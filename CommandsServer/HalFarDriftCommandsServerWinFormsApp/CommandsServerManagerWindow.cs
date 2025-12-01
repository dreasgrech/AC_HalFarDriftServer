using HalFarDriftCommandsServer;
using System;
using System.Windows.Forms;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public partial class CommandsServerManagerWindow : Form
    {
        private System.Windows.Forms.Timer serverStatusTimer;

        HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer;

        private bool serverRunning;

        public CommandsServerManagerWindow()
        {
            InitializeComponent();
            
            HalFarDriftCommandsServerWinFormsManager.Initialize(LogTextBox);
            driftCommandsServer = HalFarDriftCommandsServerWinFormsManager.driftCommandsServer;

            var commandsServerUserManager = driftCommandsServer.CommandsServerUserManager;
            commandsServerUserManager.OnPlayerAdded += CommandsServerUserManager_OnPlayerAdded;

            serverStatusTimer = new System.Windows.Forms.Timer()
            {
                Interval = 1000,
            };

            serverStatusTimer.Tick += ServerStatusTimer_Tick;
            serverStatusTimer.Start();
        }

        private void ServerStatusTimer_Tick(object sender, EventArgs e)
        {
            CheckServerStatus();
        }

        private void CheckServerStatus()
        {
            serverRunning = driftCommandsServer.ServerRunning;
            if (serverRunning)
            {
                serverStatusIndicatorControl.SetAsConnected();
                StartServerButton.Text = "Stop Server";
            } else
            {
                serverStatusIndicatorControl.SetAsDisconnected();
                StartServerButton.Text = "Start Server";
            }

            ServerAddressTextBox.Enabled = !serverRunning;
        }

        private void CommandsServerUserManager_OnPlayerAdded(object sender, AssettoCorsaCommandsServer.PlayerAddedEventArgs e)
        {
            var playerWebSocketID = e.PlayerWebSocketID;

            var commandsServerUserManager = HalFarDriftCommandsServerWinFormsManager.CommandsServerUserManager;
            var listViewItem = new ListViewItem(playerWebSocketID);
            commandsServerUserManager.TryGetPlayerName(playerWebSocketID, out var playerName);
            commandsServerUserManager.TryGetPlayerSessionID(playerWebSocketID, out var playerSessionID);
            commandsServerUserManager.TryGetPlayerCarName(playerWebSocketID, out var playerCarName);

            listViewItem.SubItems.Add(playerName);
            listViewItem.SubItems.Add(playerSessionID.ToString());
            listViewItem.SubItems.Add(playerCarName);

            ConnectedPlayersListView.BeginInvoke(new Action<ListViewItem>(AddListViewItem), listViewItem);
        }

        private void AddListViewItem(ListViewItem listViewItem)
        {
            ConnectedPlayersListView.Items.Add(listViewItem);
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            if (serverRunning)
            {
                var serverStarted = HalFarDriftCommandsServerWinFormsManager.StopServer();
            }
            else
            {
                var serverHost = ServerAddressTextBox.Text;
                var serverStarted = HalFarDriftCommandsServerWinFormsManager.StartServer(serverHost);
            }
        }

        private void InitiateStartingLightsSequenceForAllButton_Click(object sender, EventArgs e)
        {
            var driftCommandsServer = HalFarDriftCommandsServerWinFormsManager.driftCommandsServer;
            driftCommandsServer.SendStartStartingLightsInitiationSequenceToAll();
        }
    }
}