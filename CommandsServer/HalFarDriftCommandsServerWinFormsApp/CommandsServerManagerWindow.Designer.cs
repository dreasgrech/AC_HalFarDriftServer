using System.ComponentModel;

namespace HalFarDriftCommandsServerWinFormsApp
{
    partial class CommandsServerManagerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConnectedPlayersListView = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ConnectedPlayersListView
            // 
            this.ConnectedPlayersListView.FormattingEnabled = true;
            this.ConnectedPlayersListView.Location = new System.Drawing.Point(32, 45);
            this.ConnectedPlayersListView.Name = "ConnectedPlayersListView";
            this.ConnectedPlayersListView.Size = new System.Drawing.Size(120, 95);
            this.ConnectedPlayersListView.TabIndex = 0;
            // 
            // CommandsServerManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConnectedPlayersListView);
            this.Name = "CommandsServerManagerWindow";
            this.Text = "CommandsServerManagerWindow";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ListBox ConnectedPlayersListView;

        #endregion
    }
}