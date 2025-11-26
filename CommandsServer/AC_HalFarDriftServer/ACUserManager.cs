namespace AC_HalFarDriftServer;

public class ACUserManager
{
    public static ACUserManager Instance { get; set; }
    
    private List<string> playersNames;
    private List<int> playersSessionID;
    private List<string> playersCarID;
    
    private int nextACUserManagerPlayerId = 0;
    
    private readonly object lockObject = new object();
    
    public ACUserManager(int initialCapacity)
    {
        playersSessionID = new List<int>(initialCapacity);
        playersNames = new List<string>(initialCapacity);
        playersCarID = new List<string>(initialCapacity);
    }

    public int AddPlayer(int acSessionID, string acPlayerName, string acPlayerCarID)
    {
        lock (lockObject)
        {
            var acUserManagerPlayerId = nextACUserManagerPlayerId++;
        
            playersSessionID.Add(acSessionID);
            playersNames.Add(acPlayerName);
            playersCarID.Add(acPlayerCarID);
            
            Console.WriteLine($"Added player: ACUserManagerID = {acUserManagerPlayerId}, Name = {acPlayerName}");
        
            return acUserManagerPlayerId;
        }
    }
}