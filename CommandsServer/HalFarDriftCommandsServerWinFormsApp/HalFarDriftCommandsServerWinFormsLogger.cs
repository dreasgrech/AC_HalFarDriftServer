using System.Threading;
using System.Windows.Forms;
using AssettoCorsaCommandsServer.Loggers;

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
                loggerTextBox.Invoke((MethodInvoker)(() =>
                {
                    loggerTextBox.Text += $"{line}\r\n";
                }));
        }
    }
}