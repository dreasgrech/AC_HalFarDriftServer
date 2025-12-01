using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer;

public enum ServerCommandType
{
    None = 0,
    ShowWelcomeMessage = 1,
    StartCountdownTimer = 2
}

public class HalFarDriftCommandsServer
{
    private readonly CommandsServer commandsServer;
    private readonly DriftServerEndpointImplementation endpointImplementation;
    
    public HalFarDriftCommandsServer(ICommandServerLogger commandServerLogger)
    {
        commandsServer = new CommandsServer(commandServerLogger);
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