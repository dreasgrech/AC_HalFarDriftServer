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