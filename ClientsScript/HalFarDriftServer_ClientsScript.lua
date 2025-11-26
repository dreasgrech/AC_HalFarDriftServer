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

showIntro()

local serverDataCallback = function(data)
  local dataString = data == nil and 'nil' or tostring(data)
  ac.log('Data from server: ' .. dataString)
end

-- local url = "ws://127.0.0.1/DriftServer"
local url = "ws://127.0.0.1/DriftServer?hello=itsme"

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
local socket = web.socket(url, socketHeaders, serverDataCallback, socketParams)

socket("hello from the client")
