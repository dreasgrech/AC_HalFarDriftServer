
---@enum SERVER_COMMAND_TYPE
local SERVER_COMMAND_TYPE = {
  None = 0,
  ShowWelcomeMessage = 1,
}


function script.update()
    -- ac.log('From script')
end

local showIntro = function()
	ui.modalDialog('From online script!', function()
    ui.textColored('This modal was created from a script downloaded from the server.', rgbm(1, 0, 0, 1))
    ui.newLine()
--     if ui.modernButton('Copy', vec2(110, 40)) then
--       ac.setClipboardText('yes') 
--     end
    ui.sameLine()
    if ui.modernButton('Close', vec2(120, 40)) then
      return true
    end

    return false
  end, true)
end

-- showIntro()

local messageHandlers = {
  [SERVER_COMMAND_TYPE.ShowWelcomeMessage] = function(messageObject)
    ac.log(string.format('Handling ShowWelcomeMessage command from server: %s', tostring(messageObject.M)))

    showIntro()
  end
}

---@param data binary
local serverDataCallback = function(data)
  if data == nil then
    ac.log('Data from server: nil data received')
    return
  end

  local dataString = tostring(data)
  ac.log('Data from server: ' .. dataString)
  local messageObject = JSON.parse(dataString)
  local messageType = messageObject.X
  local handlerFunction = messageHandlers[messageType]
  if handlerFunction ~= nil then
    handlerFunction(messageObject)
  else
    ac.log('No handler for message type: ' .. tostring(messageType))
  end
end

--[===[
SessionID: 0-based index of entry list used by the player;
SteamID: player’s Steam ID;
CarID: name of the user’s car folder;
CarSkinID: name of the user’s skin folder;
CSPBuildID: CSP build number;
ServerIP: IP used to connect to the server;
ServerName: server name;
ServerHTTPPort: server HTTP port;
ServerTCPPort: server TCP port;
ServerUDPPort: server UDP port;
--]===]

--[===[
local cspQueryStringParams = {
  "SessionID",
  "SteamID",
  "CarID",
  "CarSkinID",
  "CSPBuildID",
  "ServerIP",
  "ServerName",
  "ServerHTTPPort",
  "ServerTCPPort",
  "ServerUDPPort"
}

-- build querystring
local queryString = ""
for i, paramName in ipairs(cspQueryStringParams) do
  local keyValue = string.format("%s={%s}", paramName, paramName)
  if queryString == "" then
    queryString = "?" .. keyValue
  else
    queryString = string.format("%s&%s", queryString, keyValue)
  end
end
--]===]

local playerCar = ac.getCar(0)
local playerCarSessionID = playerCar.sessionID
local carID = ac.getCarID(0)
-- local driverName = playerCar:driverName()
local playerDriverName = ac.getDriverName(0)

local queryStringParams = {
  SessionID = playerCarSessionID,
  CarID = carID,
  DriverName = playerDriverName
}

-- build querystring
local queryString = ""
for paramName, paramValue in pairs(queryStringParams) do
  -- local keyValue = string.format("%s=%s", paramName, paramValue)
  local keyValue = string.format("%s=%s", paramName, string.urlEncode(tostring(paramValue), false))
  if queryString == "" then
    queryString = keyValue
  else
    queryString = string.format("%s&%s", queryString, keyValue)
  end
end


ac.log("QueryString: " .. queryString)

local url = string.format("ws://127.0.0.1/DriftServer?%s", queryString)

---@type web.SocketParams
local socketParams = {
  reconnect = true,
  -- encoding = 'json',
  encoding = 'utf8',
  onClose = function()
    ac.log('Socket closed')
  end,
  onError = function(err)
    ac.log('Socket error: ' .. err)
  end
}

local socketHeaders = nil
ac.log(string.format("Connecting to WebSocket URL: %s", url))
local socket = web.socket(url, socketHeaders, serverDataCallback, socketParams)

socket("hello from the client")
