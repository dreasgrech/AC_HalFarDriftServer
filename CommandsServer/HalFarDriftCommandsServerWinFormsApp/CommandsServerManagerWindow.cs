using HalFarDriftCommandsServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public partial class CommandsServerManagerWindow : Form
    {
        private readonly System.Windows.Forms.Timer serverStatusTimer;

        private readonly HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer;
        private readonly HalFarDriftCommandsServerWinFormsManager halFarDriftCommandsServerWinFormsManager;
        private readonly HalFarDriftCommandsServerWinFormsLogger logger;
        

        private bool serverRunning;
        private int connectedPlayersInListView;

        private readonly Dictionary<string, ListViewItem> connectedPlayersListViewItems = new Dictionary<string, ListViewItem>(EqualityComparer<string>.Default);
        
        
        private ListViewColumnSorter lvwColumnSorter;


        public CommandsServerManagerWindow()
        {
            InitializeComponent();

            webSocketServerProtocolComboBox.SelectedIndex = 0;
            
            var logLevelNames = Enum.GetNames(typeof(LogLevel)).Cast<object>().ToArray();
            webSocketServerLogLevel.Items.AddRange(logLevelNames);
            webSocketServerLogLevel.SelectedIndex = (int)LogLevel.Error;
            webSocketServerLogLevel.SelectedIndexChanged += webSocketServerLogLevel_SelectedIndexChanged; 
            
            halFarDriftCommandsServerWinFormsManager = new HalFarDriftCommandsServerWinFormsManager(LogTextBox);
            driftCommandsServer = halFarDriftCommandsServerWinFormsManager.driftCommandsServer;
            logger = halFarDriftCommandsServerWinFormsManager.logger;

            var commandsServerUserManager = driftCommandsServer.CommandsServerUserManager;
            commandsServerUserManager.OnPlayerAdded += CommandsServerUserManager_OnPlayerAdded;
            commandsServerUserManager.OnPlayerRemoved += CommandsServerUserManager_OnPlayerRemoved;

            serverStatusTimer = new System.Windows.Forms.Timer()
            {
                Interval = 1000,
            };

            serverStatusTimer.Tick += ServerStatusTimer_Tick;
            serverStatusTimer.Start();
            
            lvwColumnSorter = new ListViewColumnSorter();
            ConnectedPlayersListView.ListViewItemSorter = lvwColumnSorter;
            
            /*****************/
            // for (int i = 0; i < 20; i++)
            // {
            //     AddListViewItem(new ListViewItem("asdadasd"));
            // }
            /*****************/
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
            webSocketServerProtocolComboBox.Enabled = !serverRunning;
        }

        private void CommandsServerUserManager_OnPlayerAdded(object sender, AssettoCorsaCommandsServer.PlayerAddedEventArgs e)
        {
            var playerWebSocketID = e.PlayerWebSocketID;

            var commandsServerUserManager = halFarDriftCommandsServerWinFormsManager.CommandsServerUserManager;
            commandsServerUserManager.TryGetPlayerName(playerWebSocketID, out var playerName);
            commandsServerUserManager.TryGetPlayerSessionID(playerWebSocketID, out var playerSessionID);
            commandsServerUserManager.TryGetPlayerCarName(playerWebSocketID, out var playerCarName);
            commandsServerUserManager.TryGetPlayerCSPVersion(playerWebSocketID, out var playerCSPVersion);

            var listViewItem = new ListViewItem(playerWebSocketID);
            listViewItem.SubItems.Add(playerName);
            listViewItem.SubItems.Add(playerCarName);
            listViewItem.SubItems.Add(playerCSPVersion);
            listViewItem.SubItems.Add(playerSessionID.ToString());

            connectedPlayersListViewItems.Add(playerWebSocketID, listViewItem);

            ConnectedPlayersListView.BeginInvoke(new Action<ListViewItem>(AddListViewItem), listViewItem);
        }

        private void CommandsServerUserManager_OnPlayerRemoved(object sender, AssettoCorsaCommandsServer.PlayerRemovedEventArgs e)
        {
            var playerWebSocketID = e.PlayerWebSocketID;

            if (connectedPlayersListViewItems.TryGetValue(playerWebSocketID, out var listViewItem))
            {
                // remove the listviewitem from our listviewitems collection
                connectedPlayersListViewItems.Remove(playerWebSocketID);

                // remove the listviewitem from the listview
                ConnectedPlayersListView.BeginInvoke(new Action<ListViewItem>(RemoveListViewItem), listViewItem);
            }
        }


        private void AddListViewItem(ListViewItem listViewItem)
        {
            ConnectedPlayersListView.Items.Add(listViewItem);
            Interlocked.Increment(ref connectedPlayersInListView);
            UpdateConnectedPlayersStatus();
        }

        private void RemoveListViewItem(ListViewItem listViewItem)
        {
            ConnectedPlayersListView.Items.Remove(listViewItem);
            Interlocked.Decrement(ref connectedPlayersInListView);
            UpdateConnectedPlayersStatus();
        }

        private void UpdateConnectedPlayersStatus()
        {
            ConnectedPlayersLabel.Text = $"Connected Players: {connectedPlayersInListView}";
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            if (serverRunning)
            {
                var wasServerStopped = halFarDriftCommandsServerWinFormsManager.StopServer();
                if (!wasServerStopped)
                {
                    logger.WriteLine($"Unable to stop server.");
                }
            }
            else
            {
                var serverHost = ServerAddressTextBox.Text;

                string webServerProtocol = null;
                switch (webSocketServerProtocolComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            webServerProtocol = "ws";
                        }
                        break;
                    case 1:
                        {
                            webServerProtocol = "wss";
                        }
                        break;
                }

                var logLevelIndex = webSocketServerLogLevel.SelectedIndex;
                var logLevel = (LogLevel)logLevelIndex;
                var serverStarted = halFarDriftCommandsServerWinFormsManager.StartServer(webServerProtocol, serverHost, logLevel);
                if (!serverStarted)
                {
                    logger.WriteLine($"Unable to start server at address: {webServerProtocol}://{serverHost}/{driftCommandsServer.endpointImplementation.EndpointName}");
                }
            }
        }

        private void webSocketServerLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLogLevelIndex = webSocketServerLogLevel.SelectedIndex;
            var selectedLogLevel = (LogLevel)selectedLogLevelIndex;
            halFarDriftCommandsServerWinFormsManager.SetLogLevel(selectedLogLevel);
        }

        private void InitiateStartingLightsSequenceForAllButton_Click(object sender, EventArgs e)
        {
            driftCommandsServer.SendStartStartingLightsInitiationSequenceCommandToAll();
        }

        private void InitiateRightDriveByMaltaFlagEffectSequenceButton_Click(object sender, EventArgs e)
        {
            driftCommandsServer.SendStartRightDriveByMaltaFlagEffectSequenceCommandToAll();
        }

        private void ConnectedPlayersListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            ConnectedPlayersListView.Sort();
        }
    }
}