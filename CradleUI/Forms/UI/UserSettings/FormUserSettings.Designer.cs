using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormUserSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserSettings));
            this.labelTitle = new System.Windows.Forms.Label();
            this.cbPhotocellMaterialPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.cbPhotocellAlignment = new Machine.UI.Controls.MachineButtonLabel();
            this.cbDanceBarEnabled = new Machine.UI.Controls.MachineButtonLabel();
            this.cbEncoderEnabled = new Machine.UI.Controls.MachineButtonLabel();
            this.mlCutterVelocity = new Machine.UI.Controls.MachineLabel();
            this.mpbsCutterVelocity = new Machine.UI.Controls.MachinePanelButtonSlider();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mpPreFeed = new Machine.UI.Controls.MachinePropertyNumericEditBox();
            this.cbEnablePhotocellRollPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.Location = new System.Drawing.Point(12, 18);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(203, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "User settings";
            // 
            // cbPhotocellMaterialPresence
            // 
            this.cbPhotocellMaterialPresence.Active = false;
            this.cbPhotocellMaterialPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbPhotocellMaterialPresence.ActiveBackgroundImage")));
            this.cbPhotocellMaterialPresence.ButtonSize = 102;
            this.cbPhotocellMaterialPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbPhotocellMaterialPresence.InactiveBackgroundImage")));
            this.cbPhotocellMaterialPresence.Location = new System.Drawing.Point(602, 166);
            this.cbPhotocellMaterialPresence.Name = "cbPhotocellMaterialPresence";
            this.cbPhotocellMaterialPresence.PropertyName = "Material Presence";
            this.cbPhotocellMaterialPresence.Size = new System.Drawing.Size(450, 110);
            this.cbPhotocellMaterialPresence.TabIndex = 5;
            // 
            // cbPhotocellAlignment
            // 
            this.cbPhotocellAlignment.Active = false;
            this.cbPhotocellAlignment.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbPhotocellAlignment.ActiveBackgroundImage")));
            this.cbPhotocellAlignment.ButtonSize = 102;
            this.cbPhotocellAlignment.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbPhotocellAlignment.InactiveBackgroundImage")));
            this.cbPhotocellAlignment.Location = new System.Drawing.Point(602, 50);
            this.cbPhotocellAlignment.Name = "cbPhotocellAlignment";
            this.cbPhotocellAlignment.PropertyName = "Photocell Alignment";
            this.cbPhotocellAlignment.Size = new System.Drawing.Size(450, 110);
            this.cbPhotocellAlignment.TabIndex = 4;
            // 
            // cbDanceBarEnabled
            // 
            this.cbDanceBarEnabled.Active = false;
            this.cbDanceBarEnabled.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbDanceBarEnabled.ActiveBackgroundImage")));
            this.cbDanceBarEnabled.ButtonSize = 102;
            this.cbDanceBarEnabled.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbDanceBarEnabled.InactiveBackgroundImage")));
            this.cbDanceBarEnabled.Location = new System.Drawing.Point(99, 166);
            this.cbDanceBarEnabled.Name = "cbDanceBarEnabled";
            this.cbDanceBarEnabled.PropertyName = "Dance Bar";
            this.cbDanceBarEnabled.Size = new System.Drawing.Size(450, 110);
            this.cbDanceBarEnabled.TabIndex = 3;
            // 
            // cbEncoderEnabled
            // 
            this.cbEncoderEnabled.Active = false;
            this.cbEncoderEnabled.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEncoderEnabled.ActiveBackgroundImage")));
            this.cbEncoderEnabled.ButtonSize = 102;
            this.cbEncoderEnabled.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEncoderEnabled.InactiveBackgroundImage")));
            this.cbEncoderEnabled.Location = new System.Drawing.Point(99, 50);
            this.cbEncoderEnabled.Name = "cbEncoderEnabled";
            this.cbEncoderEnabled.PropertyName = "Enable Encoder";
            this.cbEncoderEnabled.Size = new System.Drawing.Size(450, 110);
            this.cbEncoderEnabled.TabIndex = 2;
            // 
            // mlCutterVelocity
            // 
            this.mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutterVelocity.Location = new System.Drawing.Point(625, 413);
            this.mlCutterVelocity.Name = "mlCutterVelocity";
            this.mlCutterVelocity.Size = new System.Drawing.Size(246, 30);
            this.mlCutterVelocity.TabIndex = 8;
            this.mlCutterVelocity.Text = "mlCutterVelocity";
            this.mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mpbsCutterVelocity
            // 
            this.mpbsCutterVelocity.Location = new System.Drawing.Point(674, 455);
            this.mpbsCutterVelocity.MaxValue = 100F;
            this.mpbsCutterVelocity.MinValue = 0F;
            this.mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            this.mpbsCutterVelocity.PropertyName = "";
            this.mpbsCutterVelocity.Size = new System.Drawing.Size(140, 192);
            this.mpbsCutterVelocity.TabIndex = 9;
            this.mpbsCutterVelocity.Value = 0F;
            this.mpbsCutterVelocity.ValueChangedEventEnabled = false;
            // 
            // panelBorder
            // 
            this.panelBorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelBorder.Location = new System.Drawing.Point(6, 398);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Size = new System.Drawing.Size(1135, 4);
            this.panelBorder.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(4, 666);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1135, 4);
            this.panel1.TabIndex = 57;
            // 
            // mpPreFeed
            // 
            this.mpPreFeed.BackColor = System.Drawing.SystemColors.Control;
            this.mpPreFeed.ForeColor = System.Drawing.Color.DarkGray;
            this.mpPreFeed.Location = new System.Drawing.Point(186, 490);
            this.mpPreFeed.MaxValue = null;
            this.mpPreFeed.MinValue = null;
            this.mpPreFeed.Name = "mpPreFeed";
            this.mpPreFeed.PropertyName = "Pre Feed";
            this.mpPreFeed.PropertyValue = 0F;
            this.mpPreFeed.Size = new System.Drawing.Size(190, 85);
            this.mpPreFeed.TabIndex = 58;
            this.mpPreFeed.TabStop = false;
            this.mpPreFeed.UnitMeasure = "";
            this.mpPreFeed.ValueChangedEventEnabled = false;
            this.mpPreFeed.ValueChanged += new System.EventHandler<Machine.UI.Controls.MachinePropertyNumericEditBox.MachinePropertyBoxEvent>(this.MpPreFeed_ValueChanged);
            // 
            // cbEnablePhotocellRollPresence
            // 
            this.cbEnablePhotocellRollPresence.Active = false;
            this.cbEnablePhotocellRollPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEnablePhotocellRollPresence.ActiveBackgroundImage")));
            this.cbEnablePhotocellRollPresence.ButtonSize = 102;
            this.cbEnablePhotocellRollPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEnablePhotocellRollPresence.InactiveBackgroundImage")));
            this.cbEnablePhotocellRollPresence.Location = new System.Drawing.Point(99, 282);
            this.cbEnablePhotocellRollPresence.Name = "cbEnablePhotocellRollPresence";
            this.cbEnablePhotocellRollPresence.PropertyName = "Enable Photocell Roll Presence";
            this.cbEnablePhotocellRollPresence.Size = new System.Drawing.Size(450, 110);
            this.cbEnablePhotocellRollPresence.TabIndex = 59;
            // 
            // FormUserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 720);
            this.Controls.Add(this.cbEnablePhotocellRollPresence);
            this.Controls.Add(this.mpPreFeed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBorder);
            this.Controls.Add(this.mpbsCutterVelocity);
            this.Controls.Add(this.mlCutterVelocity);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.cbPhotocellMaterialPresence);
            this.Controls.Add(this.cbPhotocellAlignment);
            this.Controls.Add(this.cbDanceBarEnabled);
            this.Controls.Add(this.cbEncoderEnabled);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormUserSettings";
            this.Load += new System.EventHandler(this.FormUserSettings_Load);
            this.Shown += new System.EventHandler(this.FormUserSettings_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Machine.UI.Controls.MachineButtonLabel cbEncoderEnabled;
        private Machine.UI.Controls.MachineButtonLabel cbDanceBarEnabled;
        private Machine.UI.Controls.MachineButtonLabel cbPhotocellAlignment;
        private Machine.UI.Controls.MachineButtonLabel cbPhotocellMaterialPresence;
        private System.Windows.Forms.Label labelTitle;
        private MachineLabel mlCutterVelocity;
        private MachinePanelButtonSlider mpbsCutterVelocity;
        private System.Windows.Forms.Panel panelBorder;
        private System.Windows.Forms.Panel panel1;
        private MachinePropertyNumericEditBox mpPreFeed;
        public MachineButtonLabel cbEnablePhotocellRollPresence;
    }
}