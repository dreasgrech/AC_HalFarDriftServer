using System;
using System.Windows.Forms;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public partial class ConnectToServerWindow : Form
    {
        public ConnectToServerWindow()
        {
            InitializeComponent();
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            var serverHost = ServerAddressTextBox.Text;
            HalFarDriftCommandsServerWinFormsManager.StartServer(serverHost);
        }
    }
}