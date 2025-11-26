using System;
using System.Text;
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

      Send ("Message received, thank you!");
    }

    protected override void OnOpen()
    {
      Console.WriteLine("New client connected.");
      Send("Welcome to the Drift Server! (from blocking call)");
      SendAsync("another message (from async call)", b =>
      {
        Console.WriteLine($"Sent async message, success: {b}");
      });
      
      // QueryString[]
      var currentWebSocketContext = Context;
      var currentQueryStringKeyValueCollection = currentWebSocketContext.QueryString;
      var allKeys = currentQueryStringKeyValueCollection.AllKeys;
      var queryStringBuilder = new StringBuilder( /*capacity*/);
      foreach (var queryStringKey in allKeys)
      {
        var queryStringValue = currentQueryStringKeyValueCollection[queryStringKey];
        queryStringBuilder.Append($"{queryStringKey}={queryStringValue}&");
      }
      
      // build the querystring into one string
      Console.WriteLine($"Client QueryString: {queryStringBuilder.ToString()}");
      

      var sessions = Sessions.Sessions.ToArray();
      Console.WriteLine($"Current active sessions count: {sessions.Length}");
      foreach (var webSocketSession in sessions)
      {
        var webSocketSessionContext = webSocketSession.Context;
        var id = webSocketSession.ID;
        var protocol = webSocketSession.Protocol;
        var startTime = webSocketSession.StartTime;
        var state = webSocketSession.State;
        
        Console.WriteLine($"Session Info: ID = {id}, Protocol = {protocol}, StartTime = {startTime}, State = {state}");
      }
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

      var wssv = new WebSocketServer(baseServerAddress);
      wssv.Log.Level = LogLevel.Trace;
      
      wssv.AddWebSocketService<DriftServerEndpoint> ($"/{serverEndpoint}");
      wssv.Start ();
      Console.ReadKey (true);
      wssv.Stop ();
    }
  }
}
