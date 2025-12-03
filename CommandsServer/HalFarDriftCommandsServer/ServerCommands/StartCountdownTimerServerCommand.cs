using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class StartCountdownTimerServerCommand : ServerCommand
{
    public StartCountdownTimerServerCommand() : base((int)HalFarDriftServerCommandType.StartCountdownTimer)
    {
    }
}