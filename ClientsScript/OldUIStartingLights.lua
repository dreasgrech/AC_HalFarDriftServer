--[===[

-- Andreas: this is code I wrote for a countdown timer shown on the ui with three circles representing red, yellow, and green lights.

---@enum COUNTDOWN_TIMER_STATE
local COUNTDOWN_TIMER_STATE = {
  Inactive = 0,
  Red = 1,
  Yellow = 2,
  Green = 3
}

local COUNTDOWN_TIMER_GAP_BETWEEN_STATES_SECONDS = 2.0
local countdownTimerState = COUNTDOWN_TIMER_STATE.Inactive
local countdownTimerStartTime = 0.0
local countdownTimerCurrentStateEnterTimeSeconds = 0.0

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


function script.update(dt)
    -- ac.log('From script')
  if countdownTimerState ~= COUNTDOWN_TIMER_STATE.Inactive then 
    local currentTimeSeconds = ac.getSim().time * 0.001
    local stateMachineFunction = countdownTimerStateMachine[countdownTimerState]
    stateMachineFunction(currentTimeSeconds)
  end
end

script.drawUI = function()
  -- drawTimer()
  drawDreasTimer()
end
--]===]