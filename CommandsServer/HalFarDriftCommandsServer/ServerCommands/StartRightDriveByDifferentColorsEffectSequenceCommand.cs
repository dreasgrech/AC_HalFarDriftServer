using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class StartRightDriveByDifferentColorsEffectSequenceCommand : ServerCommand
{
    public StartRightDriveByDifferentColorsEffectSequenceCommand() : base((int)HalFarDriftServerCommandType.StartRightDriveByDifferentColorsEffectSequence)
    {
    }
}