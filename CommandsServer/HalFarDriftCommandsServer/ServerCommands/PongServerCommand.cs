using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class PongServerCommand : ServerCommand
{
    public PongServerCommand() : base((int)HalFarDriftServerCommandType.Pong)
    {
    }
}