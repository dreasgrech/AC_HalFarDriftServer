using System;
using AC_HalFarDriftServer.EndPoints;
using AC_HalFarDriftServer.ServerCommands;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AC_HalFarDriftServer
{
  public class Program
  {
    public static void Main (string[] args)
    {
      var serverProtocol = "ws";
      var serverAddress = "127.0.0.1";
      var serverEndpoint = DriftServerEndpoint.EndpointName;

      var baseServerAddress = $"{serverProtocol}://{serverAddress}";
      var fullServerAddress = $"{baseServerAddress}/{serverEndpoint}";
      Console.WriteLine($"Starting WebSocket server at {fullServerAddress}");
      
      var acUserManager = new ACUserManager ();
      ACUserManager.Instance = acUserManager;
      
      var serverCommandsManager = new ServerCommandsManager();
      ServerCommandsManager.Instance = serverCommandsManager;

      var wssv = new WebSocketServer(baseServerAddress);
      wssv.Log.Level = LogLevel.Trace;
      
      wssv.AddWebSocketService<DriftServerEndpoint> ($"/{serverEndpoint}");
      wssv.Start ();
      Console.ReadKey (true);
      wssv.Stop ();
    }
  }
}
