using AssettoCorsaCommandsServer;
using AssettoCorsaCommandsServer.ServerCommands;

namespace DriftCommandServerLibrary.ServerCommands;

public class StartCountdownTimerServerCommand : ServerCommand
{

    public StartCountdownTimerServerCommand() : base((int)ServerCommandType.StartCountdownTimer)
    {
    }
}