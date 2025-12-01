using System.Threading;
using System.Threading.Tasks;
using AssettoCorsaCommandsServer.EndPoints;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AssettoCorsaCommandsServer
{
    public class CommandsServer
    {
        public static ICommandsServerLogger Logger { get; private set; }

        private const string ServerProtocol = "ws";

        private WebSocketServer wssv;
        private Task serverTask;
        
        private readonly CancellationTokenSource tokenSource;
        private readonly CancellationToken ct;
        
        public CommandsServer(ICommandsServerLogger logger)
        {
            Logger = logger;
            
            tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token; 
        }
        
        public void StartServer(string serverHost, ICommandsServerEndpointOperations operations)
        {
            var acUserManager = new CommandsServerUserManager();
            CommandsServerUserManager.Instance = acUserManager;
            
            var baseServerAddress = $"{ServerProtocol}://{serverHost}";
            
            wssv = new WebSocketServer(baseServerAddress);
            wssv.Log.Level = LogLevel.Trace;
            
            CommandsServerEndpoint.EndpointOperations = operations;
            var serverEndpoint = operations.EndpointName;
            wssv.AddWebSocketService<CommandsServerEndpoint>($"/{serverEndpoint}");
            
            // Start server in background task
            serverTask = Task.Run(() =>
            {
                wssv.Start();
                Logger.WriteLine("WebSocket server started in background.");

                // while (!ct.IsCancellationRequested)
                // {
                //     return;
                // }
            }, ct);

            // Wait for the server to starts
            serverTask.Wait(ct);
        }
    
        public void SendCommandToClient(string webSocketID, ServerCommand command)
        {
            var serializedCommand = command.Serialize();
        
            var acUserManager = CommandsServerUserManager.Instance;
            if (acUserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
            {
                Logger.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
                webSocket.Send(serializedCommand);
            }
        }
    
        public void SendAsyncCommandToClient(string webSocketID, ServerCommand command)
        {
            var serializedCommand = command.Serialize();
        
            var acUserManager = CommandsServerUserManager.Instance;
            if (acUserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
            {
                Logger.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
                webSocket.SendAsync(serializedCommand, null);
            }
        }

        public void StopServer()
        {
            wssv.Stop();
            
            // stop the task
            tokenSource.Cancel();
            
            // todo: check about if we need this
            serverTask.Wait(ct);
        }
    }
}