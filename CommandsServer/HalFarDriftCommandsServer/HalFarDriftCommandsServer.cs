using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;
using HalFarDriftCommandsServer.ServerCommands;
using WebSocketSharp;

namespace HalFarDriftCommandsServer;

public enum HalFarDriftServerCommandType
{
    None = 0,
    ShowWelcomeMessage = 1,
    StartCountdownTimer = 2,
    StartRightDriveByMaltaFlagEffectSequence = 3,
    Pong = 4
}

public class HalFarDriftCommandsServer
{
    private readonly AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer;
    public readonly DriftServerEndpointImplementation endpointImplementation;
    
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
    
    public bool StartServer(string webSocketProtocol, string serverHost, LogLevel logLevel)
    {
        return assettoCorsaCommandsServer.StartServer(webSocketProtocol, serverHost, endpointImplementation, logLevel);
    }

    public bool SendAsyncCommandToClient(string webSocketID, ServerCommand command)
    {
        return assettoCorsaCommandsServer.SendAsyncCommandToClient(webSocketID, command);
    }

    public void SendStartStartingLightsInitiationSequenceCommandToAll()
    {
        var command = new StartCountdownTimerServerCommand();
        SendMessageToAll(command);
    }
    
    public void SendStartRightDriveByMaltaFlagEffectSequenceCommandToAll()
    {
        var command = new StartRightDriveByMaltaFlagEffectSequenceCommand();
        SendMessageToAll(command);
    }
    
    private bool SendMessageToAll(ServerCommand command)
    {
        if (!assettoCorsaCommandsServer.ServerRunning)
        {
            commandsServerLogger.WriteLine($"Cannot send command {command.GetType().Name} ({command.CommandType}) - server is not running.");
            return false;
        }
        
        var playersEnumerator = CommandsServerUserManager.GetAllPlayersEnumerator();
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
        
        commandsServerLogger.WriteLine($"Sent {command.GetType().Name} ({command.CommandType}) command to {sentToTotal} clients.");

        return true;
    }
    
    public void SetLogLevel(LogLevel logLevel)
    {
        assettoCorsaCommandsServer.SetLogLevel(logLevel);
    }
    
    public bool StopServer()
    {
        return assettoCorsaCommandsServer.StopServer();
    }
}