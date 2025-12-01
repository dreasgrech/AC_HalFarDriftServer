using System;
using System.Windows.Forms;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public partial class CommandsServerManagerWindow : Form
    {
        public CommandsServerManagerWindow()
        {
            InitializeComponent();
            
            HalFarDriftCommandsServerWinFormsManager.Initialize(LogTextBox);

            var commandsServerUserManager = HalFarDriftCommandsServerWinFormsManager.driftCommandsServer.CommandsServerUserManager;
            commandsServerUserManager.OnPlayerAdded += CommandsServerUserManager_OnPlayerAdded;
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

        void AddListViewItem(ListViewItem listViewItem)
        {
            ConnectedPlayersListView.Items.Add(listViewItem);
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            var serverHost = ServerAddressTextBox.Text;
            HalFarDriftCommandsServerWinFormsManager.StartServer(serverHost);
        }

        private void InitiateStartingLightsSequenceForAllButton_Click(object sender, EventArgs e)
        {
            var driftCommandsServer = HalFarDriftCommandsServerWinFormsManager.driftCommandsServer;
            driftCommandsServer.SendStartStartingLightsInitiationSequenceToAll();

        }
    }
}