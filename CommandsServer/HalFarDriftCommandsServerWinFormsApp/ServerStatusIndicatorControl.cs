using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalFarDriftCommandsServerWinFormsApp
{
    public partial class ServerStatusIndicatorControl : UserControl
    {
        public ServerStatusIndicatorControl()
        {
            InitializeComponent();
        }

        private Color _lightColor = Color.Red;
        private Color LightColor
        {
            get { return _lightColor; }
            set { _lightColor = value; Invalidate(); }
        }

        public void SetAsConnected()
        {
            LightColor = Color.Green;
        }

        public void SetAsDisconnected()
        {
            LightColor = Color.Red;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int diameter = Math.Min(ClientSize.Width, ClientSize.Height) - 2;
            using (var brush = new SolidBrush(_lightColor))
            {
                g.FillEllipse(brush, 1, 1, diameter, diameter);
            }

            // border
            using (var pen = new Pen(Color.Black, 1))
            {
                g.DrawEllipse(pen, 1, 1, diameter, diameter);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // ensure it's square
            int size = Math.Min(Width, Height);
            Width = size;
            Height = size;
        }
    }
}
