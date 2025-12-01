using HalFarDriftCommandsServer.ServerCommands;

namespace HalFarDriftCommandsServerConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = new ConsoleLogger();
        var serverAddress = "127.0.0.1";
        // var commandsServer = new CommandsServer(logger, serverAddress);
        // commandsServer.StartServer();

        var driftCommandsServer = new HalFarDriftCommandsServer.HalFarDriftCommandsServer(logger);
        driftCommandsServer.StartServer(serverAddress);

        var commandsServerUserManager = driftCommandsServer.CommandsServerUserManager;

        // Run a console loop waiting for commands
        Console.WriteLine("Enter commands (type 'exit' to shut down):");
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            input = input.Trim().ToLowerInvariant();
            switch (input)
            {
                case "exit":
                {
                    Console.WriteLine("Stopping server...");
                    driftCommandsServer.StopServer();
                    goto afterInputLoop;
                }
                case "intro":
                {
                    if (commandsServerUserManager.TryGetFirstPlayerWebSocketID(out var firstWebSocketID))
                    {
                        driftCommandsServer.SendAsyncCommandToClient(firstWebSocketID, new ShowWelcomeMessageServerCommand("HALLO"));
                    }
                } break;
                case "start":
                {
                    // if (commandsServerUserManager.TryGetFirstPlayerWebSocketID(out var firstWebSocketID))
                    // {
                    //     driftCommandsServer.SendAsyncCommandToClient(firstWebSocketID, new StartCountdownTimerServerCommand());
                    // }
                        driftCommandsServer.SendStartStartingLightsInitiationSequenceToAll();
                } break;
                default:
                {
                    Console.WriteLine($"command: {input}");
                }break;
            }
        }
        afterInputLoop:

        Console.WriteLine("Server stopped. Exiting.");
    }
}