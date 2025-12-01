using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;
using HalFarDriftCommandsServer.ServerCommands;

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
    public bool ServerRunning => assettoCorsaCommandsServer.ServerRunning;

    private readonly ICommandsServerLogger commandsServerLogger;
    
    public HalFarDriftCommandsServer(ICommandsServerLogger commandsServerLogger)
    {
        this.commandsServerLogger = commandsServerLogger;
        assettoCorsaCommandsServer = new AssettoCorsaCommandsServer.AssettoCorsaCommandsServer(commandsServerLogger);
        endpointImplementation = new DriftServerEndpointImplementation(assettoCorsaCommandsServer);

        CommandsServerUserManager = assettoCorsaCommandsServer.UserManager;
    }
    
    public bool StartServer(string serverHost)
    {
        return assettoCorsaCommandsServer.StartServer(serverHost, endpointImplementation);
    }

    public bool SendAsyncCommandToClient(string webSocketID, ServerCommand command)
    {
        return assettoCorsaCommandsServer.SendAsyncCommandToClient(webSocketID, command);
    }

    public void SendStartStartingLightsInitiationSequenceToAll()
    {
        if (!assettoCorsaCommandsServer.ServerRunning)
        {
            commandsServerLogger.WriteLine("Cannot send StartStartingLightsInitiationSequence command - server is not running.");
            return;
        }
        
        var playersEnumerator = CommandsServerUserManager.GetAllPlayersEnumerator();
        var command = new StartCountdownTimerServerCommand();
        var sentToTotal = 0;
        while (playersEnumerator.MoveNext())
        {
            var playerID = playersEnumerator.Current;
            var commandSent = assettoCorsaCommandsServer.SendAsyncCommandToClient(playerID, command);
            if (commandSent)
            {
                sentToTotal++;
            }
        }
        
        commandsServerLogger.WriteLine($"Sent StartStartingLightsInitiationSequence command to {sentToTotal} clients.");
    }
    
    public bool StopServer()
    {
        return assettoCorsaCommandsServer.StopServer();
    }
}