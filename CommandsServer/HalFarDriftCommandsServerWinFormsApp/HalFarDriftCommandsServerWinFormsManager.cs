namespace HalFarDriftCommandsServerWinFormsApp
{
    public static class HalFarDriftCommandsServerWinFormsManager
    {
        private static HalFarDriftCommandsServerWinFormsLogger logger;
        public static HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer { get; private set; }
        
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