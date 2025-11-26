namespace AC_HalFarDriftServer;

public class ACUserManager
{
    public static ACUserManager Instance { get; set; }

    private readonly List<string> webSocketIDs;
    
    private readonly Dictionary<string, string> playersName;
    private readonly Dictionary<string, int> playersSessionID;
    private readonly Dictionary<string, string> playersCarName;
    
    private readonly object lockObject = new object();
    
    public ACUserManager()
    {
        webSocketIDs = new List<string>();
        
        playersSessionID = new Dictionary<string, int>();
        playersName = new Dictionary<string, string>();
        playersCarName = new Dictionary<string, string>();
    }

    public void AddPlayer(string webSocketID, int acSessionCarID, string acPlayerName, string acPlayerCarName)
    {
        lock (lockObject)
        {
            if (webSocketIDs.Contains(webSocketID))
            {
                Console.WriteLine($"Attempted to add player with WebSocketID: {webSocketID}, but it already exists.");
                return;
            }
            
            webSocketIDs.Add(webSocketID);
            
            playersSessionID[webSocketID] = acSessionCarID;
            playersName[webSocketID] = acPlayerName;
            playersCarName[webSocketID] = acPlayerCarName;
            
            Console.WriteLine($"Added player.  WebSocketID: {webSocketID}, SessionCarID: {acSessionCarID}, PlayerName: {acPlayerName}, PlayerCarName: {acPlayerCarName}");
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
            
            playersSessionID.Remove(webSocketID);
            playersName.Remove(webSocketID);
            playersCarName.Remove(webSocketID);
            
            Console.WriteLine($"Removed player.  WebSocketID: {webSocketID}");
        }
    }
}