using AssettoCorsaCommandsServer.ServerCommands;
using Newtonsoft.Json;

namespace HalFarDriftCommandsServer.ServerCommands;

public class ShowWelcomeMessageServerCommand : ServerCommand
{
    [JsonProperty(PropertyName = "M")]
    public string Message { get; set; }

    public ShowWelcomeMessageServerCommand(string message) : base((int)HalFarDriftServerCommandType.ShowWelcomeMessage)
    {
        Message = message;
    }
}