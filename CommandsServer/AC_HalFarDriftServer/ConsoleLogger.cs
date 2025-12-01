using AssettoCorsaCommandsServer.Loggers;

namespace AC_HalFarDriftServer;

internal class ConsoleLogger : ICommandsServerLogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}