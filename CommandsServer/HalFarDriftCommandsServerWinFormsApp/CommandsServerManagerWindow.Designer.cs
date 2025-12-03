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
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.StartServerButton = new System.Windows.Forms.Button();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webSocketServerProtocolComboBox = new System.Windows.Forms.ComboBox();
            this.serverStatusIndicatorControl = new HalFarDriftCommandsServerWinFormsApp.ServerStatusIndicatorControl();
            this.label1 = new System.Windows.Forms.Label();
            this.webSocketServerLogLevel = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InitiateStartingLightsSequenceForAllButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ConnectedPlayersLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectedPlayersListView
            // 
            this.ConnectedPlayersListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectedPlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader4, this.columnHeader3, this.columnHeader5, this.columnHeader2 });
            this.ConnectedPlayersListView.FullRowSelect = true;
            this.ConnectedPlayersListView.HideSelection = false;
            this.ConnectedPlayersListView.Location = new System.Drawing.Point(12, 12);
            this.ConnectedPlayersListView.Name = "ConnectedPlayersListView";
            this.ConnectedPlayersListView.Size = new System.Drawing.Size(1277, 267);
            this.ConnectedPlayersListView.TabIndex = 1;
            this.ConnectedPlayersListView.UseCompatibleStateImageBehavior = false;
            this.ConnectedPlayersListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "WebSocket ID";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Car";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "CSP Version";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Session ID";
            this.columnHeader2.Width = 100;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LogTextBox.Location = new System.Drawing.Point(12, 549);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(1277, 232);
            this.LogTextBox.TabIndex = 2;
            // 
            // StartServerButton
            // 
            this.StartServerButton.Location = new System.Drawing.Point(281, 6);
            this.StartServerButton.Name = "StartServerButton";
            this.StartServerButton.Size = new System.Drawing.Size(150, 20);
            this.StartServerButton.TabIndex = 4;
            this.StartServerButton.Text = "Start Server";
            this.StartServerButton.UseVisualStyleBackColor = true;
            this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Location = new System.Drawing.Point(74, 5);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.Size = new System.Drawing.Size(201, 20);
            this.ServerAddressTextBox.TabIndex = 3;
            this.ServerAddressTextBox.Text = "127.0.0.1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.webSocketServerProtocolComboBox);
            this.panel1.Controls.Add(this.serverStatusIndicatorControl);
            this.panel1.Controls.Add(this.ServerAddressTextBox);
            this.panel1.Controls.Add(this.StartServerButton);
            this.panel1.Location = new System.Drawing.Point(825, 508);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 35);
            this.panel1.TabIndex = 5;
            // 
            // webSocketServerProtocolComboBox
            // 
            this.webSocketServerProtocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.webSocketServerProtocolComboBox.FormattingEnabled = true;
            this.webSocketServerProtocolComboBox.Items.AddRange(new object[] { "ws://", "wss://" });
            this.webSocketServerProtocolComboBox.Location = new System.Drawing.Point(6, 5);
            this.webSocketServerProtocolComboBox.Name = "webSocketServerProtocolComboBox";
            this.webSocketServerProtocolComboBox.Size = new System.Drawing.Size(62, 21);
            this.webSocketServerProtocolComboBox.TabIndex = 1;
            // 
            // serverStatusIndicatorControl
            // 
            this.serverStatusIndicatorControl.Location = new System.Drawing.Point(437, 7);
            this.serverStatusIndicatorControl.Name = "serverStatusIndicatorControl";
            this.serverStatusIndicatorControl.Size = new System.Drawing.Size(18, 18);
            this.serverStatusIndicatorControl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log level:";
            // 
            // webSocketServerLogLevel
            // 
            this.webSocketServerLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.webSocketServerLogLevel.FormattingEnabled = true;
            this.webSocketServerLogLevel.Location = new System.Drawing.Point(60, 7);
            this.webSocketServerLogLevel.Name = "webSocketServerLogLevel";
            this.webSocketServerLogLevel.Size = new System.Drawing.Size(94, 21);
            this.webSocketServerLogLevel.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 321);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1277, 149);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operations";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(230, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 77);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pyrotechnics";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Initiate Far One-by-One sequence";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InitiateStartingLightsSequenceForAllButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 77);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Starting Lights";
            // 
            // InitiateStartingLightsSequenceForAllButton
            // 
            this.InitiateStartingLightsSequenceForAllButton.Location = new System.Drawing.Point(6, 19);
            this.InitiateStartingLightsSequenceForAllButton.Name = "InitiateStartingLightsSequenceForAllButton";
            this.InitiateStartingLightsSequenceForAllButton.Size = new System.Drawing.Size(196, 42);
            this.InitiateStartingLightsSequenceForAllButton.TabIndex = 0;
            this.InitiateStartingLightsSequenceForAllButton.Text = "Initiate Starting Lights sequence";
            this.InitiateStartingLightsSequenceForAllButton.UseVisualStyleBackColor = true;
            this.InitiateStartingLightsSequenceForAllButton.Click += new System.EventHandler(this.InitiateStartingLightsSequenceForAllButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.webSocketServerLogLevel);
            this.panel2.Location = new System.Drawing.Point(12, 508);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(163, 35);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ConnectedPlayersLabel);
            this.panel3.Location = new System.Drawing.Point(14, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 27);
            this.panel3.TabIndex = 8;
            // 
            // ConnectedPlayersLabel
            // 
            this.ConnectedPlayersLabel.Location = new System.Drawing.Point(0, 0);
            this.ConnectedPlayersLabel.Name = "ConnectedPlayersLabel";
            this.ConnectedPlayersLabel.Size = new System.Drawing.Size(192, 16);
            this.ConnectedPlayersLabel.TabIndex = 0;
            this.ConnectedPlayersLabel.Text = "Connected Players: 0";
            // 
            // CommandsServerManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 793);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.ConnectedPlayersListView);
            this.Name = "CommandsServerManagerWindow";
            this.Text = "Hal Far Drift Server Commands Manager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader columnHeader5;

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label ConnectedPlayersLabel;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Panel panel2;

        private System.Windows.Forms.ComboBox webSocketServerLogLevel;
        private System.Windows.Forms.Label label1;

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
        private HalFarDriftCommandsServerWinFormsApp.ServerStatusIndicatorControl serverStatusIndicatorControl;
        private System.Windows.Forms.ComboBox webSocketServerProtocolComboBox;
    }
}