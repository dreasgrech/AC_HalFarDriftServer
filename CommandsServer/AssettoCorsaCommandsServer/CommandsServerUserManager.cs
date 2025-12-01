using System;
using System.Collections.Generic;

namespace AssettoCorsaCommandsServer;

public class PlayerAddedEventArgs : EventArgs
{
    public string PlayerWebSocketID { get; set; }

    public PlayerAddedEventArgs(string playerWebSocketID)
    {
        PlayerWebSocketID = playerWebSocketID;
    }
}

public class CommandsServerUserManager
{
    private readonly List<string> webSocketIDs;
    
    private readonly Dictionary<string, WebSocketSharp.WebSocket> playersWebSocket;
    private readonly Dictionary<string, string> playersName;
    private readonly Dictionary<string, int> playersSessionID;
    private readonly Dictionary<string, string> playersCarName;
    
    private readonly object lockObject = new();

    public event EventHandler<PlayerAddedEventArgs> OnPlayerAdded;
    
    public CommandsServerUserManager()
    {
        webSocketIDs = new List<string>();
        
        playersWebSocket = new Dictionary<string, WebSocketSharp.WebSocket>();
        playersSessionID = new Dictionary<string, int>();
        playersName = new Dictionary<string, string>();
        playersCarName = new Dictionary<string, string>();
    }

    public void AddPlayer(string webSocketID, WebSocketSharp.WebSocket webSocket, int acSessionCarID, string acPlayerName, string acPlayerCarName)
    {
        lock (lockObject)
        {
            if (webSocketIDs.Contains(webSocketID))
            {
                Console.WriteLine($"Attempted to add player with WebSocketID: {webSocketID}, but it already exists.");
                return;
            }
            
            webSocketIDs.Add(webSocketID);
            
            playersWebSocket[webSocketID] = webSocket;
            playersSessionID[webSocketID] = acSessionCarID;
            playersName[webSocketID] = acPlayerName;
            playersCarName[webSocketID] = acPlayerCarName;
            
            Console.WriteLine($"Added player.  WebSocketID: {webSocketID}, SessionCarID: {acSessionCarID}, PlayerName: {acPlayerName}, PlayerCarName: {acPlayerCarName}");

            OnPlayerAdded?.Invoke(this, new PlayerAddedEventArgs(webSocketID));
        }
    }
    
    public bool TryGetFirstPlayerWebSocketID(out string firstWebSocketID)
    {
        lock (lockObject)
        {
            if (webSocketIDs.Count > 0)
            {
                firstWebSocketID = webSocketIDs[0];
                return true;
            }
            else
            {
                firstWebSocketID = null;
                return false;
            }
        }
    }
    
    public bool TryGetPlayerWebSocket(string webSocketID, out WebSocketSharp.WebSocket webSocket)
    {
        lock (lockObject)
        {
            return playersWebSocket.TryGetValue(webSocketID, out webSocket);
        }
    }

    public bool TryGetPlayerSessionID(string webSocketID, out int sessionID)
    {
        lock (lockObject)
        {
            return playersSessionID.TryGetValue(webSocketID, out sessionID);
        }
    }

    public bool TryGetPlayerName(string webSocketID, out string name)
    {
        lock (lockObject)
        {
            return playersName.TryGetValue(webSocketID, out name);
        }
    }

    public bool TryGetPlayerCarName(string webSocketID, out string carName)
    {
        lock (lockObject)
        {
            return playersCarName.TryGetValue(webSocketID, out carName);
        }
    }
    
    public void RemovePlayer(string webSocketID)
    {
        lock (lockObject)
        {
            if (!webSocketIDs.Remove(webSocketID))
            {
                Console.WriteLine($"Attempted to remove player with WebSocketID: {webSocketID}, but it was not found.");
                return;
            }
            
            playersWebSocket.Remove(webSocketID);
            playersSessionID.Remove(webSocketID);
            playersName.Remove(webSocketID);
            playersCarName.Remove(webSocketID);
            
            Console.WriteLine($"Removed player.  WebSocketID: {webSocketID}");
        }
    }

    public IEnumerator<string> GetAllPlayersEnumerator()
    {
        return webSocketIDs.GetEnumerator();
    }
}