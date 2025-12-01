using AssettoCorsaCommandsServer.Loggers;
using System;
using System.Threading;
using System.Windows.Forms;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public class HalFarDriftCommandsServerWinFormsLogger : ICommandsServerLogger
    {
        private TextBox loggerTextBox;
        public HalFarDriftCommandsServerWinFormsLogger(TextBox textBox)
        {
            loggerTextBox = textBox;
        }
        
        public void WriteLine(string line)
        {
            loggerTextBox.BeginInvoke(new Action<string>(UpdateTextBox), line);

        }

        void UpdateTextBox(string text)
        {
            loggerTextBox.Text += $"{text}\r\n";
        }
    }
}