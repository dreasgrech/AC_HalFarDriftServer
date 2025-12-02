
local WEBSOCKET_SERVER_PROTOCOL = "ws"
local WEBSOCKET_SERVER_HOST = "127.0.0.1"
--local WEBSOCKET_SERVER_HOST = "5.135.137.227"

---@enum SERVER_COMMAND_TYPE
local SERVER_COMMAND_TYPE = {
  None = 0,
  ShowWelcomeMessage = 1,
  StartCountdownTimer = 2
}

local ParticlesSparks = ac.Particles.Sparks( {
    color = rgbm(0.5, 0.5, 0.5, 0.5),
    life = 4,
    size = 0.5,
    directionSpread = 1,
    positionSpread = 0.2
})

--[===[
-- later in script.update() or something
ParticlesSparks.life = 100
--ParticlesSparks.size = 0.2
--ParticlesSparks.directionSpread = 1
--ParticlesSparks.positionSpread = 0.2
ParticlesSparks:emit(car.position, velocitityVec3, amountF)
--]===]


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

local StartingLights = (function()
  local EFFECTS = {
    None = 0,
    FlickeringGreen = 1,
    CyclingRedGreen = 2,
    -- Gradient RGB sweep 0..255..0
    GradientRGB = 3,
    StartCountdown = 4
  }

  local AVAILABLE_COLOR_TYPES = {
    Red = 1,
    Green = 2,
    Black = 3,
    Yellow = 4
  }

  local COLOR_VALUES = {
    [AVAILABLE_COLOR_TYPES.Red] = vec3(255, 0, 0),
    [AVAILABLE_COLOR_TYPES.Green] = vec3(0, 255, 0),
    [AVAILABLE_COLOR_TYPES.Black] = vec3(0, 0, 0),
    [AVAILABLE_COLOR_TYPES.Yellow] = vec3(255, 255, 0)
  }

   local getTimeSeconds = function ()
     local ms = ac.getSim().time
     return ms * 0.001
   end

  local currentEffect = EFFECTS.None
  local currentColor;

  local ksEmissivePropertyName = 'ksEmissive'

  local startlightsMeshes = ac.findMeshes('{ Cube.005_SUB1, Cube.006_SUB1, Cube.007_SUB1, Cube.008_SUB1 }')
  startlightsMeshes:setMaterialProperty('ksAlphaRef', -193) -- value from ext_config file
-- startlightsMeshes:ensureUniqueMaterials()

  local setColor = function(color)
    currentColor = color
    local colorValue = COLOR_VALUES[color]
    startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, colorValue)
    ac.log(string.format('Set startlights color to R=%f, G=%f, B=%f', colorValue.x, colorValue.y, colorValue.z))
  end

  local currentEffectStateTime = 0.0

  local GradientEffectState = (function()
    -- Converts HSV (with H in degrees, S/V in [0..1]) to RGB [0..255].
    local hsvToRgb255 = function(h, s, v)
      if s <= 0.0 then
        local gray = v * 255.0
        return gray, gray, gray
      end

      h = h % 360.0
      local c = v * s
      local hp = h / 60.0
      local x = c * (1.0 - math.abs((hp % 2.0) - 1.0))

      local r1, g1, b1
      if hp < 1.0 then
        r1, g1, b1 = c, x, 0.0
      elseif hp < 2.0 then
        r1, g1, b1 = x, c, 0.0
      elseif hp < 3.0 then
        r1, g1, b1 = 0.0, c, x
      elseif hp < 4.0 then
        r1, g1, b1 = 0.0, x, c
      elseif hp < 5.0 then
        r1, g1, b1 = x, 0.0, c
      else
        r1, g1, b1 = c, 0.0, x
      end

      local m = v - c
      local r = (r1 + m) * 255.0
      local g = (g1 + m) * 255.0
      local b = (b1 + m) * 255.0

      return r, g, b
    end

    local GRADIENT_SPEED_DEGREES_PER_SECOND = 60.0 -- How many degrees of hue to advance per second (360° = full rainbow).
    local gradientHueDegrees = 0.0 -- Hue in degrees [0, 360). We’ll walk around the HSV color wheel.
    -- Saturation and value for HSV (1 = full saturation/brightness).
    local GRADIENT_SATURATION = 1.0
    local GRADIENT_VALUE = 1.0

    return {
        start = function()
          gradientHueDegrees = 0.0
        end,
        update = function(dt)
          -- Advance hue based on time:
          gradientHueDegrees = gradientHueDegrees + GRADIENT_SPEED_DEGREES_PER_SECOND * dt
          if gradientHueDegrees >= 360.0 then
            gradientHueDegrees = gradientHueDegrees - 360.0
          end

          local r, g, b = hsvToRgb255(gradientHueDegrees, GRADIENT_SATURATION, GRADIENT_VALUE)

          -- This will give values like (255, 0, 0), (0, 255, 0), (0, 0, 255), and many intermediate combinations (e.g. (0, 120, 221)) over time.
          startlightsMeshes:setMaterialProperty(ksEmissivePropertyName, vec3(r, g, b)) --todo: reuse vec3

          ac.log(string.format('Gradient color value: R=%f, G=%f, B=%f', r, g, b))
        end
      }
  end)()

  local StartCountdownState = (function ()
    ---@enum StartCountdownState_States
    local StartCountdownState_States = {
      None = 0,
      ShowingRed = 1,
      ShowingYellow = 2,
      ShowingGreen = 3
    }

    local StartCountdownState_stateStartTime = 0
    local StartCountdownState_timeInStateSeconds = 0
    local startCountdownState_currentState = StartCountdownState_States.None

    local StartCountdownState_StateMachine

    local FLICKER_GREEN_INTERVAL_SECONDS = 0.1

    ---@param newState StartCountdownState_States
    local changeStartCountdownState = function(newState)
      StartCountdownState_stateStartTime = getTimeSeconds()
      StartCountdownState_timeInStateSeconds = 0

      ac.log(string.format('StartCountdownState: Changing state from %d to %d.  Start time: %f', startCountdownState_currentState, newState, StartCountdownState_stateStartTime))
      startCountdownState_currentState = newState

      StartCountdownState_StateMachine[startCountdownState_currentState].start()
    end

    StartCountdownState_StateMachine = {
      [StartCountdownState_States.None] = { start = function() end, update = function(dt) end },
      [StartCountdownState_States.ShowingRed] = {
        start = function()
          ac.log('StartCountdownState: Showing Red')
          setColor(AVAILABLE_COLOR_TYPES.Red)
        end,
        update = function(dt)
          if StartCountdownState_timeInStateSeconds > 5 then
            changeStartCountdownState(StartCountdownState_States.ShowingYellow)
          end
        end
      },
      [StartCountdownState_States.ShowingYellow] = {
        start = function()
          ac.log('StartCountdownState: Showing Yellow')
          setColor(AVAILABLE_COLOR_TYPES.Yellow)
        end,
        update = function(dt)
          if StartCountdownState_timeInStateSeconds > 5 then
            changeStartCountdownState(StartCountdownState_States.ShowingGreen)
          end
        end
      },
      [StartCountdownState_States.ShowingGreen] = {
        start = function()
          ac.log('StartCountdownState: Showing Green')
          setColor(AVAILABLE_COLOR_TYPES.Green)
        end,
        update = function(dt)
          if StartCountdownState_timeInStateSeconds > 5 then
            changeStartCountdownState(StartCountdownState_States.None)
            -- changeStartCountdownState(StartCountdownState_States.ShowingRed)

            setColor(AVAILABLE_COLOR_TYPES.Black)
            ac.log('StartCountdownState: Finished')
            return
          end

          -- continue the flicker between green and black
          local intervalIndex = math.floor(StartCountdownState_timeInStateSeconds / FLICKER_GREEN_INTERVAL_SECONDS)
          if (intervalIndex % 2) == 0 then
            setColor(AVAILABLE_COLOR_TYPES.Green)
          else
            setColor(AVAILABLE_COLOR_TYPES.Black)
          end
        end
      }
    }

    return {
      start = function()
        changeStartCountdownState(StartCountdownState_States.ShowingRed)
      end,
      update = function(dt)
        StartCountdownState_StateMachine[startCountdownState_currentState].update(dt)
        StartCountdownState_timeInStateSeconds = StartCountdownState_timeInStateSeconds + dt
        -- ac.log(string.format('StartCountdownState: In state %d for %f seconds', startCountdownState_currentState, StartCountdownState_timeInStateSeconds))
      end
    }
  end)()

  local effectsStateMachine = {
    [EFFECTS.None] = { start = function() end, update = function(dt) end },
    [EFFECTS.FlickeringGreen] = {
      start = function()
        setColor(AVAILABLE_COLOR_TYPES.Black)
      end,
      update = function(dt)
      end
    },
    [EFFECTS.CyclingRedGreen] = {
      start = function()
      end,
      update = function(dt)
      end
    },
    [EFFECTS.GradientRGB] = GradientEffectState,
    [EFFECTS.StartCountdown] = StartCountdownState
   }

  return {
    EFFECTS = EFFECTS,
    turnOff = function()
      setColor(AVAILABLE_COLOR_TYPES.Black)
    end,
    startEffect = function(effectType)
      currentEffect = effectType
      currentEffectStateTime = getTimeSeconds()
      effectsStateMachine[currentEffect].start()
    end,
    update = function (dt)
      effectsStateMachine[currentEffect].update(dt)
    end
  }
end)()

StartingLights.turnOff()
-- StartingLights.startEffect(StartingLights.EFFECTS.StartCountdown)

function script.update(dt)
  StartingLights.update(dt)
end

local showIntro = function()
	ui.modalDialog('From online script!', function()
    ui.textColored('This modal was created from a script downloaded from the server.', rgbm(1, 0, 0, 1))
    ui.newLine()
    ui.sameLine()
    if ui.modernButton('Close', vec2(120, 40)) then
      return true
    end

    return false
  end, true)
end

--[===[
script.drawUI = function()
end
--]===]

local messageHandlers = {
  [SERVER_COMMAND_TYPE.ShowWelcomeMessage] = function(messageObject)
    ac.log(string.format('Handling ShowWelcomeMessage command from server: %s', tostring(messageObject.M)))

    showIntro()
  end,
  [SERVER_COMMAND_TYPE.StartCountdownTimer] = function(messageObject)
    ac.log(string.format('Handling StartCountdownTimer command from server'))

    StartingLights.startEffect(StartingLights.EFFECTS.StartCountdown)


    ParticlesSparks.life = 1000
    --ParticlesSparks.size = 0.2
    --ParticlesSparks.directionSpread = 1
    --ParticlesSparks.positionSpread = 0.2
    ParticlesSparks:emit(ac.getCar(0).position, vec3(100, 100, 100), 500)
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


local playerCar = ac.getCar(0)
local playerCarSessionID = playerCar.sessionID
local playerCarID = ac.getCarID(0)
-- local driverName = playerCar:driverName()
local playerDriverName = ac.getDriverName(0)

local queryStringParams = {
  SessionID = playerCarSessionID,
  CarID = playerCarID,
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

local WEBSOCKET_SERVER_ADDRESS = string.format("%s://%s/DriftServer?%s", WEBSOCKET_SERVER_PROTOCOL, WEBSOCKET_SERVER_HOST, queryString)

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

ac.log(string.format("Connecting to WebSocket URL: %s", WEBSOCKET_SERVER_ADDRESS))

local socketHeaders = nil
local socket = web.socket(WEBSOCKET_SERVER_ADDRESS, socketHeaders, serverDataCallback, socketParams)

socket("hello from the client")