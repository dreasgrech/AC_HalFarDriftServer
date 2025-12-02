using AssettoCorsaCommandsServer;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public class HalFarDriftCommandsServerWinFormsManager
    {
        public HalFarDriftCommandsServerWinFormsLogger logger;
        public HalFarDriftCommandsServer.HalFarDriftCommandsServer driftCommandsServer { get; private set; }
        public CommandsServerUserManager CommandsServerUserManager { get; private set; }
        
        public  HalFarDriftCommandsServerWinFormsManager(System.Windows.Forms.TextBox logTextBox)
        {
            logger = new HalFarDriftCommandsServerWinFormsLogger(logTextBox);
            driftCommandsServer = new HalFarDriftCommandsServer.HalFarDriftCommandsServer(logger);
            CommandsServerUserManager = driftCommandsServer.CommandsServerUserManager;
        }
        
        public bool StartServer(string webSocketProtocol, string serverHost, WebSocketSharp.LogLevel logLevel)
        {
            return driftCommandsServer.StartServer(webSocketProtocol, serverHost, logLevel);
        }
        
        public void SetLogLevel(WebSocketSharp.LogLevel logLevel)
        {
            driftCommandsServer.SetLogLevel(logLevel);
        }

        public bool StopServer()
        {
            return driftCommandsServer.StopServer();
        }
    }
}