namespace Machine.UI.Controls
{
    partial class MachineIOStatus
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineIOStatus));
            this.panelIcon = new System.Windows.Forms.Panel();
            this.pannelButton = new Machine.UI.Controls.MachineButton();
            this.mlChannel = new Machine.UI.Controls.MachineLabel();
            this.mlValue = new Machine.UI.Controls.MachineLabel();
            this.mlName = new Machine.UI.Controls.MachineLabel();
            this.mlChannelDecode = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // panelIcon
            // 
            this.panelIcon.Location = new System.Drawing.Point(3, 3);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Size = new System.Drawing.Size(20, 20);
            this.panelIcon.TabIndex = 6;
            this.panelIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelIcon_MouseDown);
            this.panelIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelIcon_MouseUp);
            // 
            // pannelButton
            // 
            this.pannelButton.Active = false;
            this.pannelButton.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("pannelButton.ActiveBackgroundImage")));
            this.pannelButton.BackColor = System.Drawing.Color.Transparent;
            this.pannelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pannelButton.BackgroundImage")));
            this.pannelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pannelButton.ButtonSize = 48;
            this.pannelButton.FlatAppearance.BorderSize = 0;
            this.pannelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.pannelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.pannelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pannelButton.ForeColor = System.Drawing.Color.Transparent;
            this.pannelButton.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("pannelButton.InactiveBackgroundImage")));
            this.pannelButton.Location = new System.Drawing.Point(146, -1);
            this.pannelButton.Name = "pannelButton";
            this.pannelButton.Size = new System.Drawing.Size(48, 48);
            this.pannelButton.StateChangeActivated = false;
            this.pannelButton.TabIndex = 14;
            this.pannelButton.TabStop = false;
            this.pannelButton.UseVisualStyleBackColor = false;
            this.pannelButton.Click += new System.EventHandler(this.pannelButton_Click);
            this.pannelButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelIcon_MouseDown);
            this.pannelButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelIcon_MouseUp);
            // 
            // mlChannel
            // 
            this.mlChannel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mlChannel.AutoSize = true;
            this.mlChannel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlChannel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.mlChannel.Location = new System.Drawing.Point(29, 6);
            this.mlChannel.Name = "mlChannel";
            this.mlChannel.Size = new System.Drawing.Size(61, 15);
            this.mlChannel.TabIndex = 2;
            this.mlChannel.Text = "Channel";
            this.mlChannel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // mlValue
            // 
            this.mlValue.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mlValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlValue.ForeColor = System.Drawing.Color.Gray;
            this.mlValue.Location = new System.Drawing.Point(12, 68);
            this.mlValue.Name = "mlValue";
            this.mlValue.Size = new System.Drawing.Size(122, 28);
            this.mlValue.TabIndex = 1;
            this.mlValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlValue.Click += new System.EventHandler(this.mlValue_Click);
            // 
            // mlName
            // 
            this.mlName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mlName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlName.Location = new System.Drawing.Point(4, 21);
            this.mlName.Name = "mlName";
            this.mlName.Size = new System.Drawing.Size(136, 44);
            this.mlName.TabIndex = 0;
            this.mlName.Text = "Name";
            this.mlName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlChannelDecode
            // 
            this.mlChannelDecode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mlChannelDecode.AutoSize = true;
            this.mlChannelDecode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlChannelDecode.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.mlChannelDecode.Location = new System.Drawing.Point(29, 6);
            this.mlChannelDecode.Name = "mlChannelDecode";
            this.mlChannelDecode.Size = new System.Drawing.Size(111, 15);
            this.mlChannelDecode.TabIndex = 15;
            this.mlChannelDecode.Text = "ChannelDecode";
            this.mlChannelDecode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.mlChannelDecode.Visible = false;
            // 
            // MachineIOStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mlChannelDecode);
            this.Controls.Add(this.pannelButton);
            this.Controls.Add(this.panelIcon);
            this.Controls.Add(this.mlChannel);
            this.Controls.Add(this.mlValue);
            this.Controls.Add(this.mlName);
            this.Name = "MachineIOStatus";
            this.Size = new System.Drawing.Size(194, 105);
            this.Load += new System.EventHandler(this.MachineIOStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MachineLabel mlName;
        private MachineLabel mlValue;
        private MachineLabel mlChannel;
        private System.Windows.Forms.Panel panelIcon;
        public MachineButton pannelButton;
        public MachineLabel mlChannelDecode;
    }
}
