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
            this.togglePropertiesPaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleOverlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.foregroundColorRgbValueLabel = new System.Windows.Forms.Label();
            this.foregroundColorPanel = new System.Windows.Forms.Panel();
            this.fontWeightValueLabel = new System.Windows.Forms.Label();
            this.fontSizeValueLabel = new System.Windows.Forms.Label();
            this.fontNameValueLabel = new System.Windows.Forms.Label();
            this.fontWeightLabel = new System.Windows.Forms.Label();
            this.fontSizeLabel = new System.Windows.Forms.Label();
            this.fontNameLabel = new System.Windows.Forms.Label();
            this.textPropertiesLabel = new System.Windows.Forms.Label();
            this.separatorLabel2 = new System.Windows.Forms.Label();
            this.currentColorHexValueLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.currentColorRgbValueLabel = new System.Windows.Forms.Label();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.currentColorPanel = new System.Windows.Forms.Panel();
            this.colorsLabel = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.topValueLabel = new System.Windows.Forms.Label();
            this.separatorLabel1 = new System.Windows.Forms.Label();
            this.leftValueLabel = new System.Windows.Forms.Label();
            this.leftLabel = new System.Windows.Forms.Label();
            this.heightValueLabel = new System.Windows.Forms.Label();
            this.nameValueLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.widthValueLabel = new System.Windows.Forms.Label();
            this.textLayerPanel = new System.Windows.Forms.Panel();
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
            this.togglePropertiesPaneToolStripMenuItem,
            this.toggleOverlayToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayIconContextMenuStrip.Name = "trayIconContextMenuStrip";
            this.trayIconContextMenuStrip.Size = new System.Drawing.Size(265, 100);
            // 
            // togglePropertiesPaneToolStripMenuItem
            // 
            this.togglePropertiesPaneToolStripMenuItem.Name = "togglePropertiesPaneToolStripMenuItem";
            this.togglePropertiesPaneToolStripMenuItem.Size = new System.Drawing.Size(264, 32);
            this.togglePropertiesPaneToolStripMenuItem.Text = "Toggle Properties Pane";
            this.togglePropertiesPaneToolStripMenuItem.Click += new System.EventHandler(this.togglePropertiesPaneToolStripMenuItem_Click);
            // 
            // toggleOverlayToolStripMenuItem
            // 
            this.toggleOverlayToolStripMenuItem.Name = "toggleOverlayToolStripMenuItem";
            this.toggleOverlayToolStripMenuItem.Size = new System.Drawing.Size(264, 32);
            this.toggleOverlayToolStripMenuItem.Text = "Toggle Overlay";
            this.toggleOverlayToolStripMenuItem.Click += new System.EventHandler(this.toggleOverlayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(264, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.BackColor = System.Drawing.Color.White;
            this.propertiesPanel.Controls.Add(this.foregroundColorRgbValueLabel);
            this.propertiesPanel.Controls.Add(this.foregroundColorPanel);
            this.propertiesPanel.Controls.Add(this.fontWeightValueLabel);
            this.propertiesPanel.Controls.Add(this.fontSizeValueLabel);
            this.propertiesPanel.Controls.Add(this.fontNameValueLabel);
            this.propertiesPanel.Controls.Add(this.fontWeightLabel);
            this.propertiesPanel.Controls.Add(this.fontSizeLabel);
            this.propertiesPanel.Controls.Add(this.fontNameLabel);
            this.propertiesPanel.Controls.Add(this.textPropertiesLabel);
            this.propertiesPanel.Controls.Add(this.separatorLabel2);
            this.propertiesPanel.Controls.Add(this.currentColorHexValueLabel);
            this.propertiesPanel.Controls.Add(this.widthLabel);
            this.propertiesPanel.Controls.Add(this.currentColorRgbValueLabel);
            this.propertiesPanel.Controls.Add(this.propertiesLabel);
            this.propertiesPanel.Controls.Add(this.heightLabel);
            this.propertiesPanel.Controls.Add(this.currentColorPanel);
            this.propertiesPanel.Controls.Add(this.colorsLabel);
            this.propertiesPanel.Controls.Add(this.topLabel);
            this.propertiesPanel.Controls.Add(this.topValueLabel);
            this.propertiesPanel.Controls.Add(this.separatorLabel1);
            this.propertiesPanel.Controls.Add(this.leftValueLabel);
            this.propertiesPanel.Controls.Add(this.leftLabel);
            this.propertiesPanel.Controls.Add(this.heightValueLabel);
            this.propertiesPanel.Controls.Add(this.nameValueLabel);
            this.propertiesPanel.Controls.Add(this.nameLabel);
            this.propertiesPanel.Controls.Add(this.widthValueLabel);
            this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertiesPanel.Location = new System.Drawing.Point(534, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(266, 630);
            this.propertiesPanel.TabIndex = 1;
            // 
            // foregroundColorRgbValueLabel
            // 
            this.foregroundColorRgbValueLabel.AutoSize = true;
            this.foregroundColorRgbValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foregroundColorRgbValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.foregroundColorRgbValueLabel.Location = new System.Drawing.Point(73, 574);
            this.foregroundColorRgbValueLabel.Name = "foregroundColorRgbValueLabel";
            this.foregroundColorRgbValueLabel.Size = new System.Drawing.Size(0, 21);
            this.foregroundColorRgbValueLabel.TabIndex = 21;
            // 
            // foregroundColorPanel
            // 
            this.foregroundColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.foregroundColorPanel.Location = new System.Drawing.Point(30, 575);
            this.foregroundColorPanel.Name = "foregroundColorPanel";
            this.foregroundColorPanel.Size = new System.Drawing.Size(20, 20);
            this.foregroundColorPanel.TabIndex = 15;
            // 
            // fontWeightValueLabel
            // 
            this.fontWeightValueLabel.AutoSize = true;
            this.fontWeightValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontWeightValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fontWeightValueLabel.Location = new System.Drawing.Point(140, 532);
            this.fontWeightValueLabel.Name = "fontWeightValueLabel";
            this.fontWeightValueLabel.Size = new System.Drawing.Size(0, 21);
            this.fontWeightValueLabel.TabIndex = 20;
            // 
            // fontSizeValueLabel
            // 
            this.fontSizeValueLabel.AutoSize = true;
            this.fontSizeValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontSizeValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fontSizeValueLabel.Location = new System.Drawing.Point(140, 493);
            this.fontSizeValueLabel.Name = "fontSizeValueLabel";
            this.fontSizeValueLabel.Size = new System.Drawing.Size(0, 21);
            this.fontSizeValueLabel.TabIndex = 20;
            // 
            // fontNameValueLabel
            // 
            this.fontNameValueLabel.AutoSize = true;
            this.fontNameValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontNameValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fontNameValueLabel.Location = new System.Drawing.Point(140, 452);
            this.fontNameValueLabel.Name = "fontNameValueLabel";
            this.fontNameValueLabel.Size = new System.Drawing.Size(0, 21);
            this.fontNameValueLabel.TabIndex = 19;
            // 
            // fontWeightLabel
            // 
            this.fontWeightLabel.AutoSize = true;
            this.fontWeightLabel.ForeColor = System.Drawing.Color.Gray;
            this.fontWeightLabel.Location = new System.Drawing.Point(26, 532);
            this.fontWeightLabel.Name = "fontWeightLabel";
            this.fontWeightLabel.Size = new System.Drawing.Size(59, 20);
            this.fontWeightLabel.TabIndex = 12;
            this.fontWeightLabel.Text = "Weight";
            // 
            // fontSizeLabel
            // 
            this.fontSizeLabel.AutoSize = true;
            this.fontSizeLabel.ForeColor = System.Drawing.Color.Gray;
            this.fontSizeLabel.Location = new System.Drawing.Point(26, 493);
            this.fontSizeLabel.Name = "fontSizeLabel";
            this.fontSizeLabel.Size = new System.Drawing.Size(40, 20);
            this.fontSizeLabel.TabIndex = 11;
            this.fontSizeLabel.Text = "Size";
            // 
            // fontNameLabel
            // 
            this.fontNameLabel.AutoSize = true;
            this.fontNameLabel.ForeColor = System.Drawing.Color.Gray;
            this.fontNameLabel.Location = new System.Drawing.Point(26, 452);
            this.fontNameLabel.Name = "fontNameLabel";
            this.fontNameLabel.Size = new System.Drawing.Size(42, 20);
            this.fontNameLabel.TabIndex = 10;
            this.fontNameLabel.Text = "Font";
            // 
            // textPropertiesLabel
            // 
            this.textPropertiesLabel.AutoSize = true;
            this.textPropertiesLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPropertiesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textPropertiesLabel.Location = new System.Drawing.Point(22, 411);
            this.textPropertiesLabel.Name = "textPropertiesLabel";
            this.textPropertiesLabel.Size = new System.Drawing.Size(101, 21);
            this.textPropertiesLabel.TabIndex = 18;
            this.textPropertiesLabel.Text = "Typography";
            // 
            // separatorLabel2
            // 
            this.separatorLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separatorLabel2.ForeColor = System.Drawing.Color.Gray;
            this.separatorLabel2.Location = new System.Drawing.Point(0, 131);
            this.separatorLabel2.Name = "separatorLabel2";
            this.separatorLabel2.Size = new System.Drawing.Size(266, 1);
            this.separatorLabel2.TabIndex = 17;
            // 
            // currentColorHexValueLabel
            // 
            this.currentColorHexValueLabel.AutoSize = true;
            this.currentColorHexValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentColorHexValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.currentColorHexValueLabel.Location = new System.Drawing.Point(64, 81);
            this.currentColorHexValueLabel.Name = "currentColorHexValueLabel";
            this.currentColorHexValueLabel.Size = new System.Drawing.Size(0, 21);
            this.currentColorHexValueLabel.TabIndex = 16;
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.ForeColor = System.Drawing.Color.Gray;
            this.widthLabel.Location = new System.Drawing.Point(26, 235);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(52, 21);
            this.widthLabel.TabIndex = 1;
            this.widthLabel.Text = "Width";
            // 
            // currentColorRgbValueLabel
            // 
            this.currentColorRgbValueLabel.AutoSize = true;
            this.currentColorRgbValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentColorRgbValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.currentColorRgbValueLabel.Location = new System.Drawing.Point(64, 49);
            this.currentColorRgbValueLabel.Name = "currentColorRgbValueLabel";
            this.currentColorRgbValueLabel.Size = new System.Drawing.Size(0, 21);
            this.currentColorRgbValueLabel.TabIndex = 15;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.AutoSize = true;
            this.propertiesLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertiesLabel.Location = new System.Drawing.Point(22, 155);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(88, 21);
            this.propertiesLabel.TabIndex = 0;
            this.propertiesLabel.Text = "Properties";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.ForeColor = System.Drawing.Color.Gray;
            this.heightLabel.Location = new System.Drawing.Point(26, 270);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(56, 21);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height";
            // 
            // currentColorPanel
            // 
            this.currentColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentColorPanel.Location = new System.Drawing.Point(27, 62);
            this.currentColorPanel.Name = "currentColorPanel";
            this.currentColorPanel.Size = new System.Drawing.Size(20, 20);
            this.currentColorPanel.TabIndex = 14;
            // 
            // colorsLabel
            // 
            this.colorsLabel.AutoSize = true;
            this.colorsLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colorsLabel.Location = new System.Drawing.Point(22, 20);
            this.colorsLabel.Name = "colorsLabel";
            this.colorsLabel.Size = new System.Drawing.Size(58, 21);
            this.colorsLabel.TabIndex = 13;
            this.colorsLabel.Text = "Colors";
            // 
            // topLabel
            // 
            this.topLabel.AutoSize = true;
            this.topLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLabel.ForeColor = System.Drawing.Color.Gray;
            this.topLabel.Location = new System.Drawing.Point(26, 305);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(34, 21);
            this.topLabel.TabIndex = 3;
            this.topLabel.Text = "Top";
            // 
            // topValueLabel
            // 
            this.topValueLabel.AutoSize = true;
            this.topValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.topValueLabel.Location = new System.Drawing.Point(100, 305);
            this.topValueLabel.Name = "topValueLabel";
            this.topValueLabel.Size = new System.Drawing.Size(0, 21);
            this.topValueLabel.TabIndex = 7;
            // 
            // separatorLabel1
            // 
            this.separatorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separatorLabel1.Location = new System.Drawing.Point(0, 381);
            this.separatorLabel1.Name = "separatorLabel1";
            this.separatorLabel1.Size = new System.Drawing.Size(266, 1);
            this.separatorLabel1.TabIndex = 11;
            // 
            // leftValueLabel
            // 
            this.leftValueLabel.AutoSize = true;
            this.leftValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftValueLabel.Location = new System.Drawing.Point(100, 340);
            this.leftValueLabel.Name = "leftValueLabel";
            this.leftValueLabel.Size = new System.Drawing.Size(0, 21);
            this.leftValueLabel.TabIndex = 8;
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.ForeColor = System.Drawing.Color.Gray;
            this.leftLabel.Location = new System.Drawing.Point(26, 340);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(36, 21);
            this.leftLabel.TabIndex = 4;
            this.leftLabel.Text = "Left";
            // 
            // heightValueLabel
            // 
            this.heightValueLabel.AutoSize = true;
            this.heightValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.heightValueLabel.Location = new System.Drawing.Point(100, 270);
            this.heightValueLabel.Name = "heightValueLabel";
            this.heightValueLabel.Size = new System.Drawing.Size(0, 20);
            this.heightValueLabel.TabIndex = 6;
            // 
            // nameValueLabel
            // 
            this.nameValueLabel.AutoSize = true;
            this.nameValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameValueLabel.Location = new System.Drawing.Point(100, 200);
            this.nameValueLabel.Name = "nameValueLabel";
            this.nameValueLabel.Size = new System.Drawing.Size(0, 20);
            this.nameValueLabel.TabIndex = 10;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.Gray;
            this.nameLabel.Location = new System.Drawing.Point(26, 200);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(51, 20);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Name";
            // 
            // widthValueLabel
            // 
            this.widthValueLabel.AutoSize = true;
            this.widthValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.widthValueLabel.Location = new System.Drawing.Point(100, 235);
            this.widthValueLabel.Name = "widthValueLabel";
            this.widthValueLabel.Size = new System.Drawing.Size(0, 20);
            this.widthValueLabel.TabIndex = 5;
            // 
            // textLayerPanel
            // 
            this.textLayerPanel.BackColor = System.Drawing.Color.Transparent;
            this.textLayerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLayerPanel.Location = new System.Drawing.Point(0, 0);
            this.textLayerPanel.Name = "textLayerPanel";
            this.textLayerPanel.Size = new System.Drawing.Size(534, 630);
            this.textLayerPanel.TabIndex = 2;
            this.textLayerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.textLayerPanel_Paint);
            // 
            // MainPage
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 630);
            this.Controls.Add(this.textLayerPanel);
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
        private System.Windows.Forms.Label currentColorRgbValueLabel;
        private System.Windows.Forms.Panel currentColorPanel;
        private System.Windows.Forms.Label colorsLabel;
        private System.Windows.Forms.Label currentColorHexValueLabel;
        private System.Windows.Forms.Label separatorLabel2;
        private System.Windows.Forms.Label textPropertiesLabel;
        private System.Windows.Forms.Label fontNameLabel;
        private System.Windows.Forms.Label fontSizeLabel;
        private System.Windows.Forms.Label fontWeightLabel;
        private System.Windows.Forms.Label fontNameValueLabel;
        private System.Windows.Forms.Label fontWeightValueLabel;
        private System.Windows.Forms.Label fontSizeValueLabel;
        private System.Windows.Forms.Label foregroundColorRgbValueLabel;
        private System.Windows.Forms.Panel foregroundColorPanel;
        private System.Windows.Forms.ToolStripMenuItem togglePropertiesPaneToolStripMenuItem;
        private System.Windows.Forms.Panel textLayerPanel;
    }
}

