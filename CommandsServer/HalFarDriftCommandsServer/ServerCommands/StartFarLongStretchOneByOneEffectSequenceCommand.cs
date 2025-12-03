using AssettoCorsaCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServer.ServerCommands;

public class StartFarLongStretchOneByOneEffectSequenceCommand : ServerCommand
{
    public StartFarLongStretchOneByOneEffectSequenceCommand() : base((int)HalFarDriftServerCommandType.StartFarLongStretchOneByOneEffectSequence)
    {
    }
}