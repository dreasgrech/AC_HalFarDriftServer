using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class StartRightDriveByWhiteEffectSequenceCommand : ServerCommand
{
    public StartRightDriveByWhiteEffectSequenceCommand() : base((int)HalFarDriftServerCommandType.StartRightDriveByWhiteEffectSequence)
    {
    }
}