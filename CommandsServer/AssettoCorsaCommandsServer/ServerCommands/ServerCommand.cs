using Newtonsoft.Json;

namespace AssettoCorsaCommandsServer.ServerCommands;

public abstract class ServerCommand
{
    [JsonProperty(PropertyName = "X")]
    // public ServerCommandType CommandType { get; private set; }
    public int CommandType { get; private set; }

    // protected ServerCommand(ServerCommandType serverCommandType)
    protected ServerCommand(int serverCommandType)
    {
        CommandType = serverCommandType;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}