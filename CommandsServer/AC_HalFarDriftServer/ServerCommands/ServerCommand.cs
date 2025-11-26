using Newtonsoft.Json;

namespace AC_HalFarDriftServer.ServerCommands;

public abstract class ServerCommand
{
    [JsonProperty(PropertyName = "X")]
    public ServerCommandType CommandType { get; private set; }

    protected ServerCommand(ServerCommandType serverCommandType)
    {
        CommandType = serverCommandType;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}