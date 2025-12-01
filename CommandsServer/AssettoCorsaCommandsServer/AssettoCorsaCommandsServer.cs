using System.Threading;
using System.Threading.Tasks;
using AssettoCorsaCommandsServer.EndPoints;
using AssettoCorsaCommandsServer.Loggers;
using AssettoCorsaCommandsServer.ServerCommands;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AssettoCorsaCommandsServer
{
    public class AssettoCorsaCommandsServer
    {
        public static ICommandsServerLogger Logger { get; private set; }
        public static ICommandsServerEndpointOperations EndpointOperations { get; private set; }
        public static CommandsServerUserManager CommandsServerUserManager { get; private set; }
        

        private const string ServerProtocol = "ws";

        private WebSocketServer wssv;
        private Task serverTask;
        public CommandsServerUserManager UserManager { get; }

        public bool ServerRunning => wssv is { IsListening: true };

        private readonly CancellationTokenSource tokenSource;
        private readonly CancellationToken ct;
        
        public AssettoCorsaCommandsServer(ICommandsServerLogger logger)
        {
            Logger = logger;
            UserManager = new CommandsServerUserManager();
            
            tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token; 
        }
        
        public void StartServer(string serverHost, ICommandsServerEndpointOperations operations)
        {
            if (ServerRunning)
            {
                Logger.WriteLine("Server is already running, cannot start another instance.");
                return;
            }
            
            var baseServerAddress = $"{ServerProtocol}://{serverHost}";
            
            wssv = new WebSocketServer(baseServerAddress);
            wssv.Log.Level = LogLevel.Trace;
            
            EndpointOperations = operations;
            CommandsServerUserManager = UserManager;
            
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
    
        public bool SendCommandToClient(string webSocketID, ServerCommand command)
        {
            if (!ServerRunning)
            {
                Logger.WriteLine("Server is not running. Cannot send command.");
                return false;
            }
            
            if (!UserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
            {
                return false;
            }
            
            var serializedCommand = command.Serialize();
            Logger.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
            webSocket.Send(serializedCommand);
            return true;
        }
    
        public bool SendAsyncCommandToClient(string webSocketID, ServerCommand command)
        {
            if (!ServerRunning)
            {
                Logger.WriteLine("Server is not running. Cannot send command.");
                return false;
            }
            
            if (!UserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
            {
                return false;
            }
            
            var serializedCommand = command.Serialize();
            Logger.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
            webSocket.SendAsync(serializedCommand, null);
            return true;
        }

        public bool StopServer()
        {
            if (!ServerRunning)
            {
                Logger.WriteLine("Server is not running. Cannot stop server.");
                return false;
            }
            
            wssv.Stop();
            
            // stop the task
            tokenSource.Cancel();
            
            // todo: check about if we need this
            serverTask.Wait(ct);

            return true;
        }
    }
}