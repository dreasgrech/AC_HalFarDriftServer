using AssettoCorsaCommandsServer;
using HalFarDriftCommandsServer.ServerCommands;
using WebSocketSharp;

namespace HalFarDriftCommandsServer;

public class DriftServerEndpointImplementation : ICommandsServerEndpointOperations
{
    public string EndpointName => "DriftServer";
    
    private readonly AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer;

    public DriftServerEndpointImplementation(AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer)
    {
        this.assettoCorsaCommandsServer = assettoCorsaCommandsServer;
    }

    public void OnOpen(string newPlayerWebsocketServerID)
    {
        assettoCorsaCommandsServer.SendAsyncCommandToClient(newPlayerWebsocketServerID, new ShowWelcomeMessageServerCommand("This is my welcome message!  Fidelio**."));
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