using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer;

public enum HalFarDriftServerCommandType
{
    None = 0,
    ShowWelcomeMessage = 1,
    StartCountdownTimer = 2
}

public class HalFarDriftCommandsServer
{
    private readonly AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer;
    private readonly DriftServerEndpointImplementation endpointImplementation;
    
    public CommandsServerUserManager CommandsServerUserManager { get; }
    
    public HalFarDriftCommandsServer(ICommandsServerLogger commandsServerLogger)
    {
        assettoCorsaCommandsServer = new AssettoCorsaCommandsServer.AssettoCorsaCommandsServer(commandsServerLogger);
        endpointImplementation = new DriftServerEndpointImplementation(assettoCorsaCommandsServer);

        CommandsServerUserManager = assettoCorsaCommandsServer.UserManager;
    }
    
    public void StartServer(string serverHost)
    {
        assettoCorsaCommandsServer.StartServer(serverHost, endpointImplementation);
    }

    public void SendAsyncCommandToClient(string webSocketID, ServerCommand command)
    {
        assettoCorsaCommandsServer.SendAsyncCommandToClient(webSocketID, command);
    }
    
    public void StopServer()
    {
        assettoCorsaCommandsServer.StopServer();
    }
}