using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
﻿using AssettoCorsaCommandsServer.Loggers;

public class HalFarDriftCommandsServerWinFormsLogger : ICommandsServerLogger, IDisposable
{
    private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
    private readonly TextBox loggerTextBox;
    private readonly System.Windows.Forms.Timer uiTimer;

    private const int MAX_LINES = 2000;

    public HalFarDriftCommandsServerWinFormsLogger(TextBox textBox)
    {
        loggerTextBox = textBox;

        uiTimer = new System.Windows.Forms.Timer
        {
            Interval = 500 // flush every 500 ms
        };
        uiTimer.Tick += FlushToTextBox;
        uiTimer.Start();
    }

    public void WriteLine(string line)
    {
        logQueue.Enqueue(line);
    }

    private void FlushToTextBox(object sender, EventArgs e)
    {
        if (logQueue.IsEmpty) return;

        if (loggerTextBox.IsDisposed) return;

        loggerTextBox.BeginInvoke(new Action(() =>
        {
            // Append all queued lines
            while (logQueue.TryDequeue(out var ln))
            {
                loggerTextBox.AppendText(ln + Environment.NewLine);
            }

            // Optionally trim lines to max scrollback
            TrimTextBox();
            
            // Scroll to bottom
            loggerTextBox.SelectionStart = loggerTextBox.Text.Length;
            loggerTextBox.ScrollToCaret();
        }));
    }

    private void TrimTextBox()
    {
        // if too many lines, drop oldest.
        var lines = loggerTextBox.Lines;
        if (lines.Length <= MAX_LINES) return;

        var start = lines.Length - MAX_LINES;
        var trimmed = new string[MAX_LINES];
        Array.Copy(lines, start, trimmed, 0, MAX_LINES);
        loggerTextBox.Lines = trimmed;
    }

    public void Dispose()
    {
        uiTimer.Stop();
        uiTimer.Tick -= FlushToTextBox;
        uiTimer.Dispose();
    }
}
