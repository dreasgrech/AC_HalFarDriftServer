using AssettoCorsaCommandsServer;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public static class HalFarDriftCommandsServerWinFormsManager
    {
        private static HalFarDriftCommandsServerWinFormsLogger logger;
        public static HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer { get; private set; }
        public static CommandsServerUserManager CommandsServerUserManager { get; private set; }
        
        public static void Initialize(System.Windows.Forms.TextBox logTextBox)
        {
            logger = new HalFarDriftCommandsServerWinFormsLogger(logTextBox);
            driftCommandsServer = new HalFarDriftCommandsServer.HalFarDriftCommandsServer(logger);
            CommandsServerUserManager = driftCommandsServer.CommandsServerUserManager;
        }
        
        public static bool StartServer(string serverHost)
        {
            return driftCommandsServer.StartServer(serverHost);
        }

        public static bool StopServer()
        {
            return driftCommandsServer.StopServer();
        }
    }
}