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
        public CommandsServerUserManager UserManager { get; private set; }
        
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
    
        public void SendCommandToClient(string webSocketID, ServerCommand command)
        {
            var serializedCommand = command.Serialize();
        
            if (UserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
            {
                Logger.WriteLine($"Sending command to client {webSocketID}: {serializedCommand}");
                webSocket.Send(serializedCommand);
            }
        }
    
        public void SendAsyncCommandToClient(string webSocketID, ServerCommand command)
        {
            var serializedCommand = command.Serialize();
        
            if (UserManager.TryGetPlayerWebSocket(webSocketID, out var webSocket))
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