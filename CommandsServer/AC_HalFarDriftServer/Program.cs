using System;
using System.Drawing;
using AC_HalFarDriftServer.EndPoints;
using AC_HalFarDriftServer.ServerCommands;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AC_HalFarDriftServer;

public class Program
{
    public static void Main(string[] args)
    {
        var serverProtocol = "ws";
        var serverAddress = "127.0.0.1";
        var serverEndpoint = DriftServerEndpoint.EndpointName;

        var baseServerAddress = $"{serverProtocol}://{serverAddress}";
        var fullServerAddress = $"{baseServerAddress}/{serverEndpoint}";
        Console.WriteLine($"Starting WebSocket server at {fullServerAddress}");

        var acUserManager = new ACUserManager();
        ACUserManager.Instance = acUserManager;

        var serverCommandsManager = new ServerCommandsManager();
        ServerCommandsManager.Instance = serverCommandsManager;

        var wssv = new WebSocketServer(baseServerAddress);
        wssv.Log.Level = LogLevel.Trace;

        wssv.AddWebSocketService<DriftServerEndpoint>($"/{serverEndpoint}");

        // Start server in background task
        Task serverTask = Task.Run(() =>
        {
            wssv.Start();
            Console.WriteLine("WebSocket server started in background.");
        });

        // Meanwhile, run a console loop waiting for commands
        Console.WriteLine("Enter commands (type 'exit' to shut down):");
        while (true)
        {
            var input = Console.ReadLine();
            if (input == null) continue;

            input = input.Trim();
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Stopping server...");
                wssv.Stop();
                break;
            }

            // Otherwise pass to your server-command manager
            // ServerCommandsManager.Instance.HandleConsoleCommand(input);
            Console.WriteLine($"command: {input}");
        }

        // Optional: wait for server task to complete if needed
        serverTask.Wait();
        Console.WriteLine("Server stopped. Exiting.");
    }
}
