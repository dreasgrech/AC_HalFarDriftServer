namespace HalFarDriftCommandsServerWinFormsApp
{
    public static class HalFarDriftCommandsServerWinFormsManager
    {
        public static void StartServer(string serverHost)
        {
            var logger = new HalFarDriftCommandsServerWinFormsLogger();
            var driftCommandsServer = new HalFarDriftCommandsServer.HalFarDriftCommandsServer(logger);
            
            driftCommandsServer.StartServer(serverHost);
        }
        
    }
}