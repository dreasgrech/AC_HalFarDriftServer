using System;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace AssettoCorsaCommandsServer.EndPoints;

internal class CommandsServerEndpoint : WebSocketBehavior
{
    // public static string EndpointName => "DriftServer";
    
    private static readonly object LockObject = new object();

    protected override void OnOpen()
    {
        lock (LockObject)
        {
            var logger = AssettoCorsaCommandsServer.Logger;
            
            logger.WriteLine("New client connected.");

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
            logger.WriteLine($"Client QueryString: {queryStringBuilder}");


            var sessions = Sessions.Sessions.ToArray();
            logger.WriteLine($"Current active sessions count: {sessions.Length}");
            foreach (var webSocketSession in sessions)
            {
                var webSocketSessionContext = webSocketSession.Context;
                var id = webSocketSession.ID;
                var protocol = webSocketSession.Protocol;
                var startTime = webSocketSession.StartTime;
                var state = webSocketSession.State;

                logger.WriteLine(
                    $"Session Info: ID = {id}, Protocol = {protocol}, StartTime = {startTime}, State = {state}");
            }

            var sessionID = Convert.ToInt16(currentQueryStringKeyValueCollection["SessionID"]);
            var playerName = currentQueryStringKeyValueCollection["DriverName"];
            var playerCarID = currentQueryStringKeyValueCollection["CarID"];

            var webSocketID = this.ID;
            var webSocket = Context.WebSocket;
            AssettoCorsaCommandsServer.CommandsServerUserManager.AddPlayer(webSocketID, webSocket, sessionID, playerName, playerCarID);

            // SendAsync($"ACUserManagerPlayerID={acUserManagerPlayerID}", b =>
            // {
            //     logger.WriteLine($"Sent async message, success: {b}");
            // });

            logger.WriteLine($"(OnOpen) WebSocket Session ID: {webSocketID}");
            
            // ServerCommandsManager.Instance.SendAsyncCommandToClient(webSocketID, new ShowWelcomeMessageServerCommand("This is my welcome message!  Fidelio**."));
            
            AssettoCorsaCommandsServer.EndpointOperations.OnOpen(webSocketID);
        }
    }

    protected override void OnMessage (MessageEventArgs e)
    {
        lock (LockObject)
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

            AssettoCorsaCommandsServer.Logger.WriteLine($"Message received: ID = {this.ID}, IP = {ipAddress}, Data = {dataString}, Data Length = {dataString.Length}, IsBinary = {isBinary}, IsPing = {isPing}, IsText = {isText}, RawData Length = {rawData.Length}");

            // Send("Message received, thank you!");
            
            AssettoCorsaCommandsServer.EndpointOperations.OnMessage(e);
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        lock (LockObject)
        {
            var code = e.Code;
            var reason = e.Reason;
            var wasClean = e.WasClean;

            var webSocketID = this.ID;
            AssettoCorsaCommandsServer.Logger.WriteLine($"(OnClose) ID = {webSocketID} Code = {code}, Reason = {reason}, WasClean = {wasClean}");

            AssettoCorsaCommandsServer.CommandsServerUserManager.RemovePlayer(webSocketID);

            AssettoCorsaCommandsServer.EndpointOperations.OnClose(e);
        }
    }

    protected override void OnError(ErrorEventArgs e)
    {
        lock (LockObject)
        {
            var exception = e.Exception;
            var message = e.Message;

            var webSocketID = this.ID;
            AssettoCorsaCommandsServer.Logger.WriteLine($"(OnError) ID = {webSocketID}, Message = {message}, Exception = {exception}");

            AssettoCorsaCommandsServer.CommandsServerUserManager.RemovePlayer(webSocketID);
            
            AssettoCorsaCommandsServer.EndpointOperations.OnError(e);
        }
    }
}