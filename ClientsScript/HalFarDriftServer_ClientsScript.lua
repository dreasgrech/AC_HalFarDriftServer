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

local url = "ws://127.0.0.1/DriftServer"
local socket = web.socket(url, nil, function(data)
  ac.log('Message from server: ' .. data)
end, {})

-- socket("hello from the client")
