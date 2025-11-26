using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace AC_HalFarDriftServer.EndPoints;

public class DriftServerEndpoint : WebSocketBehavior
{
    public static string EndpointName => "DriftServer";

    protected override void OnMessage (MessageEventArgs e)
    {
        var dataString = e.Data;
        var isBinary = e.IsBinary;
        var isPing = e.IsPing;
        var isText = e.IsText;
        var rawData = e.RawData;

        var currentWebSocketContext = Context;
        //currentWebSocketContext.WebSocket.
        var currentWebSocketContextUser = currentWebSocketContext.User;
        //currentWebSocketContextUser.
        var userEndPoint = currentWebSocketContext.UserEndPoint;
        var ipAddress = userEndPoint.Address.ToString();
      
        Console.WriteLine($"Message received: ID = {this.ID}, IP = {ipAddress}, Data = {dataString}, Data Length = {dataString.Length}, IsBinary = {isBinary}, IsPing = {isPing}, IsText = {isText}, RawData Length = {rawData.Length}");

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
      
        var currentWebSocketContext = Context;
        var currentQueryStringKeyValueCollection = currentWebSocketContext.QueryString;
        var allKeys = currentQueryStringKeyValueCollection.AllKeys;
        var queryStringBuilder = new StringBuilder( /*capacity*/);
        foreach (var queryStringKey in allKeys)
        {
            var queryStringValue = currentQueryStringKeyValueCollection[queryStringKey];
            queryStringValue = Uri.UnescapeDataString(queryStringValue);
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

        var sessionID = Convert.ToInt16(currentQueryStringKeyValueCollection["SessionID"]);
        var playerName = currentQueryStringKeyValueCollection["DriverName"];
        var playerCarID = currentQueryStringKeyValueCollection["CarID"];
        
        var acUserManager = ACUserManager.Instance;
        var acUserManagerPlayerID = acUserManager.AddPlayer(sessionID, playerName, playerCarID);
        
        SendAsync($"ACUserManagerPlayerID={acUserManagerPlayerID}", b =>
        {
            Console.WriteLine($"Sent async message, success: {b}");
        });
        
        Console.WriteLine($"(OnOpen) WebSocket Session ID: {this.ID}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        var code = e.Code;
        var reason = e.Reason;
        var wasClean = e.WasClean;
      
        Console.WriteLine($"OnClose called: ID = {this.ID} Code = {code}, Reason = {reason}, WasClean = {wasClean}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        var exception = e.Exception;
        var message = e.Message;
      
        Console.WriteLine($"OnError called: ID = {this.ID}, Message = {message}, Exception = {exception}");
    }
}