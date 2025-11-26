namespace AC_HalFarDriftServer;

public class ServerCommandsManager
{
    public static ServerCommandsManager Instance { get; set; }
    
    public ServerCommandsManager()
    {
        
    }
    
    public void SendCommandToClient(string webSocketID, ServerCommand command)
    {
        var serializedCommand = command.Serialize();
        
        var acUserManager = ACUserManager.Instance;
        if (acUserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
        {
            Console.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
            webSocket.Send(serializedCommand);
        }
    }
    
    public void SendAsyncCommandToClient(string webSocketID, ServerCommand command)
    {
        var serializedCommand = command.Serialize();
        
        var acUserManager = ACUserManager.Instance;
        if (acUserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
        {
            Console.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
            webSocket.SendAsync(serializedCommand, null);
        }
    }
}