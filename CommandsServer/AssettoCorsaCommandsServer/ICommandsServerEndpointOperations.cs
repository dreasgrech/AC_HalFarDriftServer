namespace AssettoCorsaCommandsServer;

public interface ICommandsServerEndpointOperations
{
    string EndpointName { get; }
    
    void OnOpen(string newPlayerWebsocketServerID);
    void OnMessage(WebSocketSharp.MessageEventArgs e);
    void OnClose(WebSocketSharp.CloseEventArgs e);
    void OnError(WebSocketSharp.ErrorEventArgs e);
}