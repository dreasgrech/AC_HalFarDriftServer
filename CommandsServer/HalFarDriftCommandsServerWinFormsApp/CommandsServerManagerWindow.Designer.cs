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
            this.ConnectedPlayersListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.StartServerButton = new System.Windows.Forms.Button();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serverStatusIndicatorControl = new HalFarDriftCommandsServerWinFormsApp.ServerStatusIndicatorControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InitiateStartingLightsSequenceForAllButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectedPlayersListView
            // 
            this.ConnectedPlayersListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectedPlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.ConnectedPlayersListView.HideSelection = false;
            this.ConnectedPlayersListView.Location = new System.Drawing.Point(12, 12);
            this.ConnectedPlayersListView.Name = "ConnectedPlayersListView";
            this.ConnectedPlayersListView.Size = new System.Drawing.Size(985, 97);
            this.ConnectedPlayersListView.TabIndex = 1;
            this.ConnectedPlayersListView.UseCompatibleStateImageBehavior = false;
            this.ConnectedPlayersListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "WS ID";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Session ID";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Car";
            this.columnHeader3.Width = 200;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LogTextBox.Location = new System.Drawing.Point(12, 276);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(985, 232);
            this.LogTextBox.TabIndex = 2;
            // 
            // StartServerButton
            // 
            this.StartServerButton.Location = new System.Drawing.Point(210, 6);
            this.StartServerButton.Name = "StartServerButton";
            this.StartServerButton.Size = new System.Drawing.Size(150, 20);
            this.StartServerButton.TabIndex = 4;
            this.StartServerButton.Text = "Start Server";
            this.StartServerButton.UseVisualStyleBackColor = true;
            this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Location = new System.Drawing.Point(3, 6);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.Size = new System.Drawing.Size(201, 20);
            this.ServerAddressTextBox.TabIndex = 3;
            this.ServerAddressTextBox.Text = "127.0.0.1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.serverStatusIndicatorControl);
            this.panel1.Controls.Add(this.ServerAddressTextBox);
            this.panel1.Controls.Add(this.StartServerButton);
            this.panel1.Location = new System.Drawing.Point(601, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 29);
            this.panel1.TabIndex = 5;
            // 
            // serverStatusIndicatorControl
            // 
            this.serverStatusIndicatorControl.Location = new System.Drawing.Point(366, 5);
            this.serverStatusIndicatorControl.Name = "serverStatusIndicatorControl";
            this.serverStatusIndicatorControl.Size = new System.Drawing.Size(18, 18);
            this.serverStatusIndicatorControl.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.InitiateStartingLightsSequenceForAllButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(985, 120);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operations";
            // 
            // InitiateStartingLightsSequenceForAllButton
            // 
            this.InitiateStartingLightsSequenceForAllButton.Location = new System.Drawing.Point(7, 20);
            this.InitiateStartingLightsSequenceForAllButton.Name = "InitiateStartingLightsSequenceForAllButton";
            this.InitiateStartingLightsSequenceForAllButton.Size = new System.Drawing.Size(196, 42);
            this.InitiateStartingLightsSequenceForAllButton.TabIndex = 0;
            this.InitiateStartingLightsSequenceForAllButton.Text = "Initiate Starting Lights sequence to all";
            this.InitiateStartingLightsSequenceForAllButton.UseVisualStyleBackColor = true;
            this.InitiateStartingLightsSequenceForAllButton.Click += new System.EventHandler(this.InitiateStartingLightsSequenceForAllButton_Click);
            // 
            // CommandsServerManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 520);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.ConnectedPlayersListView);
            this.Name = "CommandsServerManagerWindow";
            this.Text = "Hal Far Drift Server Commands Manager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.Button StartServerButton;
        private System.Windows.Forms.TextBox ServerAddressTextBox;

        private System.Windows.Forms.TextBox LogTextBox;

        private System.Windows.Forms.ColumnHeader columnHeader1;

        #endregion

        private System.Windows.Forms.ListView ConnectedPlayersListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button InitiateStartingLightsSequenceForAllButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private ServerStatusIndicatorControl serverStatusIndicatorControl;
    }
}