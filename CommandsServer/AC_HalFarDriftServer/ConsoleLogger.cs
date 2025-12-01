using AssettoCorsaCommandsServer.Loggers;

namespace AC_HalFarDriftServer;

internal class ConsoleLogger : ICommandServerLogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}