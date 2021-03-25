namespace RedlinesApp
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleOverlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.currentColorHexValueLabel = new System.Windows.Forms.Label();
            this.currentColorRgbValueLabel = new System.Windows.Forms.Label();
            this.currentColorPanel = new System.Windows.Forms.Panel();
            this.colorsLabel = new System.Windows.Forms.Label();
            this.sepratorLabel1 = new System.Windows.Forms.Label();
            this.separatorLabel1 = new System.Windows.Forms.Label();
            this.nameValueLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.leftValueLabel = new System.Windows.Forms.Label();
            this.topValueLabel = new System.Windows.Forms.Label();
            this.heightValueLabel = new System.Windows.Forms.Label();
            this.widthValueLabel = new System.Windows.Forms.Label();
            this.leftLabel = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.trayIconContextMenuStrip.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.trayIconContextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "redlines";
            this.notifyIcon.Visible = true;
            // 
            // trayIconContextMenuStrip
            // 
            this.trayIconContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.trayIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleOverlayToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayIconContextMenuStrip.Name = "trayIconContextMenuStrip";
            this.trayIconContextMenuStrip.Size = new System.Drawing.Size(203, 68);
            // 
            // toggleOverlayToolStripMenuItem
            // 
            this.toggleOverlayToolStripMenuItem.Name = "toggleOverlayToolStripMenuItem";
            this.toggleOverlayToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.toggleOverlayToolStripMenuItem.Text = "Toggle Overlay";
            this.toggleOverlayToolStripMenuItem.Click += new System.EventHandler(this.toggleOverlayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.BackColor = System.Drawing.Color.White;
            this.propertiesPanel.Controls.Add(this.currentColorHexValueLabel);
            this.propertiesPanel.Controls.Add(this.currentColorRgbValueLabel);
            this.propertiesPanel.Controls.Add(this.currentColorPanel);
            this.propertiesPanel.Controls.Add(this.colorsLabel);
            this.propertiesPanel.Controls.Add(this.sepratorLabel1);
            this.propertiesPanel.Controls.Add(this.separatorLabel1);
            this.propertiesPanel.Controls.Add(this.nameValueLabel);
            this.propertiesPanel.Controls.Add(this.nameLabel);
            this.propertiesPanel.Controls.Add(this.leftValueLabel);
            this.propertiesPanel.Controls.Add(this.topValueLabel);
            this.propertiesPanel.Controls.Add(this.heightValueLabel);
            this.propertiesPanel.Controls.Add(this.widthValueLabel);
            this.propertiesPanel.Controls.Add(this.leftLabel);
            this.propertiesPanel.Controls.Add(this.topLabel);
            this.propertiesPanel.Controls.Add(this.heightLabel);
            this.propertiesPanel.Controls.Add(this.widthLabel);
            this.propertiesPanel.Controls.Add(this.propertiesLabel);
            this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertiesPanel.Location = new System.Drawing.Point(570, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(230, 450);
            this.propertiesPanel.TabIndex = 1;
            // 
            // currentColorHexValueLabel
            // 
            this.currentColorHexValueLabel.AutoSize = true;
            this.currentColorHexValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentColorHexValueLabel.Location = new System.Drawing.Point(64, 315);
            this.currentColorHexValueLabel.Name = "currentColorHexValueLabel";
            this.currentColorHexValueLabel.Size = new System.Drawing.Size(0, 21);
            this.currentColorHexValueLabel.TabIndex = 16;
            // 
            // currentColorRgbValueLabel
            // 
            this.currentColorRgbValueLabel.AutoSize = true;
            this.currentColorRgbValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentColorRgbValueLabel.Location = new System.Drawing.Point(64, 283);
            this.currentColorRgbValueLabel.Name = "currentColorRgbValueLabel";
            this.currentColorRgbValueLabel.Size = new System.Drawing.Size(0, 21);
            this.currentColorRgbValueLabel.TabIndex = 15;
            // 
            // currentColorPanel
            // 
            this.currentColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentColorPanel.Location = new System.Drawing.Point(26, 296);
            this.currentColorPanel.Name = "currentColorPanel";
            this.currentColorPanel.Size = new System.Drawing.Size(20, 20);
            this.currentColorPanel.TabIndex = 14;
            // 
            // colorsLabel
            // 
            this.colorsLabel.AutoSize = true;
            this.colorsLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorsLabel.Location = new System.Drawing.Point(22, 243);
            this.colorsLabel.Name = "colorsLabel";
            this.colorsLabel.Size = new System.Drawing.Size(58, 21);
            this.colorsLabel.TabIndex = 13;
            this.colorsLabel.Text = "Colors";
            // 
            // sepratorLabel1
            // 
            this.sepratorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sepratorLabel1.Location = new System.Drawing.Point(0, 228);
            this.sepratorLabel1.Name = "sepratorLabel1";
            this.sepratorLabel1.Size = new System.Drawing.Size(200, 1);
            this.sepratorLabel1.TabIndex = 12;
            // 
            // separatorLabel1
            // 
            this.separatorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separatorLabel1.Location = new System.Drawing.Point(0, 228);
            this.separatorLabel1.Name = "separatorLabel1";
            this.separatorLabel1.Size = new System.Drawing.Size(230, 1);
            this.separatorLabel1.TabIndex = 11;
            // 
            // nameValueLabel
            // 
            this.nameValueLabel.AutoSize = true;
            this.nameValueLabel.Location = new System.Drawing.Point(96, 57);
            this.nameValueLabel.Name = "nameValueLabel";
            this.nameValueLabel.Size = new System.Drawing.Size(0, 20);
            this.nameValueLabel.TabIndex = 10;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(22, 57);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(51, 20);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Name";
            // 
            // leftValueLabel
            // 
            this.leftValueLabel.AutoSize = true;
            this.leftValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftValueLabel.Location = new System.Drawing.Point(96, 189);
            this.leftValueLabel.Name = "leftValueLabel";
            this.leftValueLabel.Size = new System.Drawing.Size(0, 21);
            this.leftValueLabel.TabIndex = 8;
            // 
            // topValueLabel
            // 
            this.topValueLabel.AutoSize = true;
            this.topValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topValueLabel.Location = new System.Drawing.Point(96, 157);
            this.topValueLabel.Name = "topValueLabel";
            this.topValueLabel.Size = new System.Drawing.Size(0, 21);
            this.topValueLabel.TabIndex = 7;
            // 
            // heightValueLabel
            // 
            this.heightValueLabel.AutoSize = true;
            this.heightValueLabel.Location = new System.Drawing.Point(96, 126);
            this.heightValueLabel.Name = "heightValueLabel";
            this.heightValueLabel.Size = new System.Drawing.Size(0, 20);
            this.heightValueLabel.TabIndex = 6;
            // 
            // widthValueLabel
            // 
            this.widthValueLabel.AutoSize = true;
            this.widthValueLabel.Location = new System.Drawing.Point(96, 93);
            this.widthValueLabel.Name = "widthValueLabel";
            this.widthValueLabel.Size = new System.Drawing.Size(0, 20);
            this.widthValueLabel.TabIndex = 5;
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.Location = new System.Drawing.Point(22, 188);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(36, 21);
            this.leftLabel.TabIndex = 4;
            this.leftLabel.Text = "Left";
            // 
            // topLabel
            // 
            this.topLabel.AutoSize = true;
            this.topLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLabel.Location = new System.Drawing.Point(22, 157);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(34, 21);
            this.topLabel.TabIndex = 3;
            this.topLabel.Text = "Top";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.Location = new System.Drawing.Point(22, 125);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(56, 21);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.Location = new System.Drawing.Point(22, 93);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(52, 21);
            this.widthLabel.TabIndex = 1;
            this.widthLabel.Text = "Width";
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.AutoSize = true;
            this.propertiesLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesLabel.Location = new System.Drawing.Point(18, 14);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(88, 21);
            this.propertiesLabel.TabIndex = 0;
            this.propertiesLabel.Text = "Properties";
            // 
            // MainPage
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.propertiesPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.Text = "redlines";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPage_Paint);
            this.trayIconContextMenuStrip.ResumeLayout(false);
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleOverlayToolStripMenuItem;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Label propertiesLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.Label leftValueLabel;
        private System.Windows.Forms.Label topValueLabel;
        private System.Windows.Forms.Label heightValueLabel;
        private System.Windows.Forms.Label widthValueLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label nameValueLabel;
        private System.Windows.Forms.Label separatorLabel1;
        private System.Windows.Forms.Label sepratorLabel1;
        private System.Windows.Forms.Label currentColorRgbValueLabel;
        private System.Windows.Forms.Panel currentColorPanel;
        private System.Windows.Forms.Label colorsLabel;
        private System.Windows.Forms.Label currentColorHexValueLabel;
    }
}

