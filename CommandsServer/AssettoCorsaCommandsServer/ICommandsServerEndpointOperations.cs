using WebSocketSharp;

namespace AssettoCorsaCommandsServer;

public interface ICommandsServerEndpointOperations
{
    string EndpointName { get; }
    
    void OnOpen(string newPlayerWebsocketServerID);
    void OnMessage(string playerWebSocketServerID, MessageEventArgs e);
    void OnClose(WebSocketSharp.CloseEventArgs e);
    void OnError(WebSocketSharp.ErrorEventArgs e);
}