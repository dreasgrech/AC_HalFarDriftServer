
---@enum SERVER_COMMAND_TYPE
local SERVER_COMMAND_TYPE = {
  None = 0,
  ShowWelcomeMessage = 1,
  StartCountdownTimer = 2
}


-- local drawTimer = function ()
  -- ui.drawText('From script', vec2(400, 400), rgbm(1, 0, 0, 1))
  -- ui.dwriteDrawText('from script dwrite', 30, vec2(400, 450), rgbm(0, 1, 0, 1))

  -- ui.transparentWindow('timer', vec2(600, 300), vec2(500, 500), function()
    -- ui.text('Timer from script')
    -- if ui.modernButton('Close', vec2(120, 40)) then
      -- ac.log('Closing timer window')
    -- end
  -- end
-- )
  -- -- ui.toolWindow('timer', vec2(300, 300), vec2(500, 500), function()
    -- -- ui.text('Timer from script')
    -- -- if ui.modernButton('Close', vec2(120, 40)) then
      -- -- ac.log('Closing timer window')
    -- -- end
  -- -- end)
  -- -- ac.log('Drawing timer window')
-- end

---@enum COUNTDOWN_TIMER_STATE
local COUNTDOWN_TIMER_STATE = {
  Inactive = 0,
  Red = 1,
  Yellow = 2,
  Green = 3
}

local StartingLights = (function()
  local EFFECTS = {
    None = 0,
    FlickeringGreen = 1,
    CyclingRedGreen = 2
  }

  local AVAILABLE_COLOR_TYPES = {
    Red = 1,
    Green = 2,
    Black = 3
  }

  local COLOR_VALUES = {
    [AVAILABLE_COLOR_TYPES.Red] = vec3(255, 0, 0),
    [AVAILABLE_COLOR_TYPES.Green] = vec3(0, 255, 0),
    [AVAILABLE_COLOR_TYPES.Black] = vec3(0, 0, 0)
  }

  local currentEffect = EFFECTS.None
  local currentColor;

  local ksEmissivePropertyName = 'ksEmissive'

  local startlightsMeshes = ac.findMeshes('{ Cube.005_SUB1, Cube.006_SUB1, Cube.007_SUB1, Cube.008_SUB1 }')
  startlightsMeshes:setMaterialProperty('ksAlphaRef', -193) -- value from ext_config file

  local setColor = function(color)
    currentColor = color
    local colorValue = COLOR_VALUES[color]
    startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, colorValue)
  end

  local setRed = function()
    -- startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, red)
    setColor(AVAILABLE_COLOR_TYPES.Red)
  end

  local setGreen = function()
    -- startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, green)
    setColor(AVAILABLE_COLOR_TYPES.Green)
  end

  local setBlack = function()
    -- startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, black)
    setColor(AVAILABLE_COLOR_TYPES.Black)
  end

  local currentEffectStateTime = 0.0

  local effectsStateMachine = {
    [EFFECTS.None] = {
      start = function()
      end,
      update = function(dt)
      end
    },
    [EFFECTS.FlickeringGreen] = {
      start = function()
        setBlack()
      end,
      update = function(dt)
      end
    },
    [EFFECTS.CyclingRedGreen] = {
      start = function()
      end,
      update = function(dt)
      end
    }
  }

  
  return {
    setRed = setRed,
    setGreen = setGreen,
    turnOff = setBlack,
    startEffect = function(effectType)
      currentEffect = effectType
      currentEffectStateTime = ac.getSim().time
      effectsStateMachine[currentEffect].start()
    end,
    update = function (dt)
      effectsStateMachine[currentEffect].update(dt)
    end
  }
end)()

-- StartingLights.turnOff()

local COUNTDOWN_TIMER_GAP_BETWEEN_STATES_SECONDS = 2.0
local countdownTimerState = COUNTDOWN_TIMER_STATE.Inactive
local countdownTimerStartTime = 0.0
local countdownTimerCurrentStateEnterTimeSeconds = 0.0

--[===[
-- local startlightsMeshes = ac.findMeshes('material:fresnel4')
local startlightsMeshes = ac.findMeshes('{ Cube.005_SUB1, Cube.006_SUB1, Cube.007_SUB1, Cube.008_SUB1 }')

-- make sure we don’t affect other things using the same material
-- startlightsMeshes:ensureUniqueMaterials()

-- values from config file:
startlightsMeshes:setMaterialProperty('ksAlphaRef', -193)

local beforeChange = startlightsMeshes:getMaterialPropertyValue('ksEmissive')
-- startlightsMeshes:setMaterialProperty('ksEmissive', rgbm(0, 0, 255, 0.5))
-- startlightsMeshes:setMaterialProperty('ksEmissive', vec3(255,192,203))
startlightsMeshes:setMaterialProperty('ksEmissive', vec3(0,255,0))
local afterChange = startlightsMeshes:getMaterialPropertyValue('ksEmissive')

ac.log(string.format('Found %d startlight meshes. beforeChange: %s, afterChange: %s', #startlightsMeshes, tostring(beforeChange), tostring(afterChange)))
--]===]

---@type table<COUNTDOWN_TIMER_STATE, fun(currentTimeSeconds: number)>
local countdownTimerStateMachine = {
  [COUNTDOWN_TIMER_STATE.Inactive] = function(currentTimeSeconds)
  end,
  [COUNTDOWN_TIMER_STATE.Red] = function(currentTimeSeconds)
    if currentTimeSeconds - countdownTimerCurrentStateEnterTimeSeconds >= COUNTDOWN_TIMER_GAP_BETWEEN_STATES_SECONDS then
      ac.log(string.format('Countdown Timer State changing to Yellow. currentTimeSeconds: %f, stateEnterTime: %f', currentTimeSeconds, countdownTimerCurrentStateEnterTimeSeconds))
      countdownTimerState = COUNTDOWN_TIMER_STATE.Yellow
      countdownTimerCurrentStateEnterTimeSeconds = currentTimeSeconds
    end
  end,
  [COUNTDOWN_TIMER_STATE.Yellow] = function(currentTimeSeconds)
    if currentTimeSeconds - countdownTimerCurrentStateEnterTimeSeconds >= COUNTDOWN_TIMER_GAP_BETWEEN_STATES_SECONDS then
      ac.log(string.format('Countdown Timer State changing to Green. currentTimeSeconds: %f, stateEnterTime: %f', currentTimeSeconds, countdownTimerCurrentStateEnterTimeSeconds))
      countdownTimerState = COUNTDOWN_TIMER_STATE.Green
      countdownTimerCurrentStateEnterTimeSeconds = currentTimeSeconds
    end
  end,
  [COUNTDOWN_TIMER_STATE.Green] = function(currentTimeSeconds)
    if currentTimeSeconds - countdownTimerCurrentStateEnterTimeSeconds >= COUNTDOWN_TIMER_GAP_BETWEEN_STATES_SECONDS then
      ac.log(string.format('Countdown Timer State changing to Inactive. currentTimeSeconds: %f, stateEnterTime: %f', currentTimeSeconds, countdownTimerCurrentStateEnterTimeSeconds))
      countdownTimerState = COUNTDOWN_TIMER_STATE.Inactive
      countdownTimerCurrentStateEnterTimeSeconds = currentTimeSeconds
    end
  end
}

---@type table<COUNTDOWN_TIMER_STATE, fun()>
local countdownTimerStateMachine_UI = {
  [COUNTDOWN_TIMER_STATE.Red] = function()
    local windowSize = ui.windowSize()
    ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.25), 100, rgbm(1, 0, 0, 1), 24)
  end,
  [COUNTDOWN_TIMER_STATE.Yellow] = function()
    local windowSize = ui.windowSize()
    ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.5), 100, rgbm(1, 1, 0, 1), 24)
  end,
  [COUNTDOWN_TIMER_STATE.Green] = function()
    local windowSize = ui.windowSize()
    ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.75), 100, rgbm(0, 1, 0, 1), 24)
  end
}

function script.update(dt)
    -- ac.log('From script')
  if countdownTimerState ~= COUNTDOWN_TIMER_STATE.Inactive then 
    local currentTimeSeconds = ac.getSim().time * 0.001
    local stateMachineFunction = countdownTimerStateMachine[countdownTimerState]
    stateMachineFunction(currentTimeSeconds)
  end

  StartingLights.update(dt)
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

local startCountdownTimer = function()
  countdownTimerState = COUNTDOWN_TIMER_STATE.Red
  local currentTimeSeconds = ac.getSim().time * 0.001

  ac.log(string.format('Starting Countdown Timer at time: %f seconds', currentTimeSeconds))
  countdownTimerCurrentStateEnterTimeSeconds = currentTimeSeconds
  countdownTimerStartTime = currentTimeSeconds
end

local drawDreasTimer = function()
  -- local windowSize = ui.windowSize()
  -- ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.25), 100, rgbm(1, 0, 0, 1), 24)
  -- ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.5), 100, rgbm(1, 1, 0, 1), 24)
  -- ui.drawCircleFilled(vec2(windowSize.x*0.5, windowSize.y*0.75), 100, rgbm(0, 1, 0, 1), 24)

    local stateMachineFunction_UI = countdownTimerStateMachine_UI[countdownTimerState]
    if stateMachineFunction_UI then
      stateMachineFunction_UI()
    end
end

script.drawUI = function()
  -- drawTimer()
  drawDreasTimer()
end

-- startCountdownTimer()

local messageHandlers = {
  [SERVER_COMMAND_TYPE.ShowWelcomeMessage] = function(messageObject)
    ac.log(string.format('Handling ShowWelcomeMessage command from server: %s', tostring(messageObject.M)))

    showIntro()
  end,
  [SERVER_COMMAND_TYPE.StartCountdownTimer] = function(messageObject)
    ac.log(string.format('Handling StartCountdownTimer command from server'))

    startCountdownTimer()
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
