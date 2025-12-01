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
    private readonly CommandsServer commandsServer;
    private readonly DriftServerEndpointImplementation endpointImplementation;
    
    public HalFarDriftCommandsServer(ICommandsServerLogger commandsServerLogger)
    {
        commandsServer = new CommandsServer(commandsServerLogger);
        endpointImplementation = new DriftServerEndpointImplementation(commandsServer);
    }
    
    public void StartServer(string serverHost)
    {
        commandsServer.StartServer(serverHost, endpointImplementation);
    }

    public void SendAsyncCommandToClient(string webSocketID, ServerCommand command)
    {
        commandsServer.SendAsyncCommandToClient(webSocketID, command);
    }
    
    public void StopServer()
    {
        commandsServer.StopServer();
    }
}