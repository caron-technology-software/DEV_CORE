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
            labelTitle = new System.Windows.Forms.Label();
            cbPhotocellMaterialPresence = new MachineButtonLabel();
            cbPhotocellAlignment = new MachineButtonLabel();
            cbDanceBarEnabled = new MachineButtonLabel();
            cbEncoderEnabled = new MachineButtonLabel();
            mlCutterVelocity = new MachineLabel();
            mpbsCutterVelocity = new MachinePanelButtonSlider();
            panelBorder = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            mpPreFeed = new MachinePropertyNumericEditBox();
            cbEnablePhotocellRollPresence = new MachineButtonLabel();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelTitle.ForeColor = System.Drawing.Color.Black;
            labelTitle.Location = new System.Drawing.Point(14, 21);
            labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(203, 33);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "User settings";
            // 
            // cbPhotocellMaterialPresence
            // 
            cbPhotocellMaterialPresence.Active = false;
            cbPhotocellMaterialPresence.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbPhotocellMaterialPresence.ActiveBackgroundImage");
            cbPhotocellMaterialPresence.ButtonSize = 102;
            cbPhotocellMaterialPresence.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbPhotocellMaterialPresence.InactiveBackgroundImage");
            cbPhotocellMaterialPresence.Location = new System.Drawing.Point(601, 191);
            cbPhotocellMaterialPresence.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cbPhotocellMaterialPresence.Name = "cbPhotocellMaterialPresence";
            cbPhotocellMaterialPresence.PropertyName = "Material Presence";
            cbPhotocellMaterialPresence.Size = new System.Drawing.Size(525, 127);
            cbPhotocellMaterialPresence.TabIndex = 5;
            // 
            // cbPhotocellAlignment
            // 
            cbPhotocellAlignment.Active = false;
            cbPhotocellAlignment.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbPhotocellAlignment.ActiveBackgroundImage");
            cbPhotocellAlignment.ButtonSize = 102;
            cbPhotocellAlignment.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbPhotocellAlignment.InactiveBackgroundImage");
            cbPhotocellAlignment.Location = new System.Drawing.Point(601, 57);
            cbPhotocellAlignment.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cbPhotocellAlignment.Name = "cbPhotocellAlignment";
            cbPhotocellAlignment.PropertyName = "Photocell Alignment";
            cbPhotocellAlignment.Size = new System.Drawing.Size(525, 127);
            cbPhotocellAlignment.TabIndex = 4;
            // 
            // cbDanceBarEnabled
            // 
            cbDanceBarEnabled.Active = false;
            cbDanceBarEnabled.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbDanceBarEnabled.ActiveBackgroundImage");
            cbDanceBarEnabled.ButtonSize = 102;
            cbDanceBarEnabled.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbDanceBarEnabled.InactiveBackgroundImage");
            cbDanceBarEnabled.Location = new System.Drawing.Point(14, 191);
            cbDanceBarEnabled.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cbDanceBarEnabled.Name = "cbDanceBarEnabled";
            cbDanceBarEnabled.PropertyName = "Dance Bar";
            cbDanceBarEnabled.Size = new System.Drawing.Size(525, 127);
            cbDanceBarEnabled.TabIndex = 3;
            // 
            // cbEncoderEnabled
            // 
            cbEncoderEnabled.Active = false;
            cbEncoderEnabled.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbEncoderEnabled.ActiveBackgroundImage");
            cbEncoderEnabled.ButtonSize = 102;
            cbEncoderEnabled.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbEncoderEnabled.InactiveBackgroundImage");
            cbEncoderEnabled.Location = new System.Drawing.Point(14, 57);
            cbEncoderEnabled.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cbEncoderEnabled.Name = "cbEncoderEnabled";
            cbEncoderEnabled.PropertyName = "Enable Encoder";
            cbEncoderEnabled.Size = new System.Drawing.Size(525, 127);
            cbEncoderEnabled.TabIndex = 2;
            // 
            // mlCutterVelocity
            // 
            mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutterVelocity.Location = new System.Drawing.Point(712, 457);
            mlCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCutterVelocity.Name = "mlCutterVelocity";
            mlCutterVelocity.Size = new System.Drawing.Size(287, 35);
            mlCutterVelocity.TabIndex = 8;
            mlCutterVelocity.Text = "mlCutterVelocity";
            mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mpbsCutterVelocity
            // 
            mpbsCutterVelocity.Location = new System.Drawing.Point(774, 495);
            mpbsCutterVelocity.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            mpbsCutterVelocity.Size = new System.Drawing.Size(163, 217);
            mpbsCutterVelocity.TabIndex = 9;
            // 
            // panelBorder
            // 
            panelBorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panelBorder.Location = new System.Drawing.Point(13, 446);
            panelBorder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBorder.Name = "panelBorder";
            panelBorder.Size = new System.Drawing.Size(1324, 5);
            panelBorder.TabIndex = 56;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panel1.Location = new System.Drawing.Point(14, 727);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1324, 5);
            panel1.TabIndex = 57;
            // 
            // mpPreFeed
            // 
            mpPreFeed.BackColor = System.Drawing.SystemColors.Control;
            mpPreFeed.ForeColor = System.Drawing.Color.DarkGray;
            mpPreFeed.Location = new System.Drawing.Point(165, 457);
            mpPreFeed.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mpPreFeed.Name = "mpPreFeed";
            mpPreFeed.Size = new System.Drawing.Size(222, 98);
            mpPreFeed.TabIndex = 58;
            mpPreFeed.TabStop = false;
            mpPreFeed.ValueChanged += MpPreFeed_ValueChanged;
            // 
            // cbEnablePhotocellRollPresence
            // 
            cbEnablePhotocellRollPresence.Active = false;
            cbEnablePhotocellRollPresence.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbEnablePhotocellRollPresence.ActiveBackgroundImage");
            cbEnablePhotocellRollPresence.ButtonSize = 102;
            cbEnablePhotocellRollPresence.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbEnablePhotocellRollPresence.InactiveBackgroundImage");
            cbEnablePhotocellRollPresence.Location = new System.Drawing.Point(14, 324);
            cbEnablePhotocellRollPresence.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cbEnablePhotocellRollPresence.Name = "cbEnablePhotocellRollPresence";
            cbEnablePhotocellRollPresence.PropertyName = "Enable Photocell Roll Presence";
            cbEnablePhotocellRollPresence.Size = new System.Drawing.Size(525, 116);
            cbEnablePhotocellRollPresence.TabIndex = 59;
            // 
            // FormUserSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(cbEnablePhotocellRollPresence);
            Controls.Add(mpPreFeed);
            Controls.Add(panel1);
            Controls.Add(panelBorder);
            Controls.Add(mpbsCutterVelocity);
            Controls.Add(mlCutterVelocity);
            Controls.Add(labelTitle);
            Controls.Add(cbPhotocellMaterialPresence);
            Controls.Add(cbPhotocellAlignment);
            Controls.Add(cbDanceBarEnabled);
            Controls.Add(cbEncoderEnabled);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormUserSettings";
            Load += FormUserSettings_Load;
            Shown += FormUserSettings_Shown;
            ResumeLayout(false);
            PerformLayout();
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