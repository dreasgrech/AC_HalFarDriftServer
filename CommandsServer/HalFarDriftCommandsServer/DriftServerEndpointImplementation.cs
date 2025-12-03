using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.Loggers;
using HalFarDriftCommandsServer.ServerCommands;
using WebSocketSharp;

namespace HalFarDriftCommandsServer;

public class DriftServerEndpointImplementation : ICommandsServerEndpointOperations
{
    public string EndpointName => "DriftServer";
    
    private readonly AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer;
    private readonly ICommandsServerLogger logger;

    public DriftServerEndpointImplementation(AssettoCorsaCommandsServer.AssettoCorsaCommandsServer assettoCorsaCommandsServer)
    {
        this.assettoCorsaCommandsServer = assettoCorsaCommandsServer;
        logger = AssettoCorsaCommandsServer.AssettoCorsaCommandsServer.Logger;
    }

    public void OnOpen(string newPlayerWebsocketServerID)
    {
        assettoCorsaCommandsServer.SendAsyncCommandToClient(newPlayerWebsocketServerID, new ShowWelcomeMessageServerCommand("This is my welcome message!  Fidelio**."));
    }

    public void OnMessage(string playerWebSocketServerID, MessageEventArgs e)
    {
        var data = e.Data;
        if (string.IsNullOrEmpty(data))
        {
            return;
        }

        var isPing = string.Equals(data, "p");
        if (isPing)
        {
            logger.WriteLine($"Received Ping from Client ID = {playerWebSocketServerID}, sending Pong response.");
            assettoCorsaCommandsServer.SendAsyncCommandToClient(playerWebSocketServerID, new PongServerCommand());
        }
    }

    public void OnClose(CloseEventArgs e)
    {
    }

    public void OnError(ErrorEventArgs e)
    {
    }
}