using AssettoCorsaCommandsServer.Loggers;

namespace HalFarDriftCommandsServerConsoleApp;

internal class ConsoleLogger : ICommandsServerLogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}