using AssettoCorsaCommandsServer;
using HalFarDriftCommandsServer.ServerCommands;
using WebSocketSharp;

namespace HalFarDriftCommandsServer;

public class DriftServerEndpointImplementation : ICommandsServerEndpointOperations
{
    public string EndpointName => "DriftServer";
    
    private readonly CommandsServer commandsServer;

    public DriftServerEndpointImplementation(CommandsServer commandsServer)
    {
        this.commandsServer = commandsServer;
    }

    public void OnOpen(string newPlayerWebsocketServerID)
    {
        commandsServer.SendAsyncCommandToClient(newPlayerWebsocketServerID, new ShowWelcomeMessageServerCommand("This is my welcome message!  Fidelio**."));
    }

    public void OnMessage(MessageEventArgs e)
    {
    }

    public void OnClose(CloseEventArgs e)
    {
    }

    public void OnError(ErrorEventArgs e)
    {
    }
}