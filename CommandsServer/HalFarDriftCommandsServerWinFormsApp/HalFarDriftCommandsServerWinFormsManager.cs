namespace HalFarDriftCommandsServerWinFormsApp
{
    public static class HalFarDriftCommandsServerWinFormsManager
    {
        private static HalFarDriftCommandsServerWinFormsLogger logger;
        private static HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer;
        
        public static void Initialize(System.Windows.Forms.TextBox logTextBox)
        {
            logger = new HalFarDriftCommandsServerWinFormsLogger(logTextBox);
            driftCommandsServer = new HalFarDriftCommandsServer.HalFarDriftCommandsServer(logger);
        }
        
        public static void StartServer(string serverHost)
        {
            driftCommandsServer.StartServer(serverHost);
        }
        
    }
}