using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class StartRightDriveByMaltaFlagEffectSequenceCommand : ServerCommand
{
    public StartRightDriveByMaltaFlagEffectSequenceCommand() : base((int)HalFarDriftServerCommandType.StartRightDriveByMaltaFlagEffectSequence)
    {
    }
}