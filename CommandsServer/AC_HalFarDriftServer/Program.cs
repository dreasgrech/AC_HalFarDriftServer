using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AC_HalFarDriftServer
{
  public class DriftServerEndpoint : WebSocketBehavior
  {
    protected override void OnMessage (MessageEventArgs e)
    {
      var dataString = e.Data;
      var isBinary = e.IsBinary;
      var isPing = e.IsPing;
      var isText = e.IsText;
      var rawData = e.RawData;
      
      Console.WriteLine($"Message received: Data = {dataString}, Data Length = {dataString.Length}, IsBinary = {isBinary}, IsPing = {isPing}, IsText = {isText}, RawData Length = {rawData.Length}");

      // Send ("Message received, thank you!");
    }

    protected override void OnOpen()
    {
      Console.WriteLine("New client connected.");
      // Send("Welcome to the Drift Server!");
      SendAsync("ok", b =>
      {
        Console.WriteLine(b);
      });
    }
  }

  public class Program
  {
    public static void Main (string[] args)
    {
      var serverProtocol = "ws";
      var serverAddress = "127.0.0.1";
      var serverEndpoint = "DriftServer";

      var baseServerAddress = $"{serverProtocol}://{serverAddress}";
      var fullServerAddress = $"{baseServerAddress}/{serverEndpoint}";
      Console.WriteLine($"Starting WebSocket server at {fullServerAddress}");

      var wssv = new WebSocketServer (baseServerAddress);
      wssv.AddWebSocketService<DriftServerEndpoint> ($"/{serverEndpoint}");
      wssv.Start ();
      Console.ReadKey (true);
      wssv.Stop ();
    }
  }
}
