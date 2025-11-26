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

        // Wait for the server to starts
        serverTask.Wait();

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
                    wssv.Stop();
                    goto afterInputLoop;
                }
                case "intro":
                {
                    if (ACUserManager.Instance.TryGetFirstPlayerWebSocketID(out var firstWebSocketID))
                    {
                        ServerCommandsManager.Instance.SendAsyncCommandToClient(firstWebSocketID, new ShowWelcomeMessageServerCommand("HALLO"));
                    }
                } break;
                case "start":
                {
                    if (ACUserManager.Instance.TryGetFirstPlayerWebSocketID(out var firstWebSocketID))
                    {
                        ServerCommandsManager.Instance.SendAsyncCommandToClient(firstWebSocketID, new StartCountdownTimerServerCommand());
                    }
                } break;
                default:
                {
                    // ServerCommandsManager.Instance.HandleConsoleCommand(input);
                    Console.WriteLine($"command: {input}");
                }break;
            }
        }
        afterInputLoop:

        // make sure server task is complete before exiting
        serverTask.Wait();
        
        Console.WriteLine("Server stopped. Exiting.");
    }
}