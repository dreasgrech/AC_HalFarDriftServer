using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
﻿using AssettoCorsaCommandsServer.Loggers;

public class HalFarDriftCommandsServerWinFormsLogger : ICommandsServerLogger, IDisposable
{
    private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
    private readonly TextBox _loggerTextBox;
    private readonly System.Windows.Forms.Timer _uiTimer;

    private readonly int _maxLines = 2000;

    public HalFarDriftCommandsServerWinFormsLogger(TextBox textBox)
    {
        _loggerTextBox = textBox;

        _uiTimer = new System.Windows.Forms.Timer
        {
            Interval = 500 // flush every 500 ms
        };
        _uiTimer.Tick += FlushToTextBox;
        _uiTimer.Start();
    }

    public void WriteLine(string line)
    {
        _logQueue.Enqueue(line);
    }

    private void FlushToTextBox(object sender, EventArgs e)
    {
        if (_logQueue.IsEmpty) return;

        if (_loggerTextBox.IsDisposed) return;

        _loggerTextBox.BeginInvoke(new Action(() =>
        {
            // Append all queued lines
            while (_logQueue.TryDequeue(out var ln))
            {
                _loggerTextBox.AppendText(ln + Environment.NewLine);
            }

            // Optionally trim lines to max scrollback
            TrimTextBox();
            
            // Scroll to bottom
            _loggerTextBox.SelectionStart = _loggerTextBox.Text.Length;
            _loggerTextBox.ScrollToCaret();
        }));
    }

    private void TrimTextBox()
    {
        // if too many lines, drop oldest.
        var lines = _loggerTextBox.Lines;
        if (lines.Length <= _maxLines) return;

        var start = lines.Length - _maxLines;
        var trimmed = new string[_maxLines];
        Array.Copy(lines, start, trimmed, 0, _maxLines);
        _loggerTextBox.Lines = trimmed;
    }

    public void Dispose()
    {
        _uiTimer.Stop();
        _uiTimer.Tick -= FlushToTextBox;
        _uiTimer.Dispose();
    }
}
