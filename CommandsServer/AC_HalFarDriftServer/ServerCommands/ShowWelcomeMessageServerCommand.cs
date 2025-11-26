using Newtonsoft.Json;

namespace AC_HalFarDriftServer.ServerCommands;

public class ShowWelcomeMessageServerCommand : ServerCommand
{
    [JsonProperty(PropertyName = "M")]
    public string Message { get; set; }

    public ShowWelcomeMessageServerCommand(string message) : base(ServerCommandType.ShowWelcomeMessage)
    {
        Message = message;
    }
}