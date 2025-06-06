using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormRootSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRootSettings));
            mlSoftwareVersion = new System.Windows.Forms.Label();
            panelForm = new System.Windows.Forms.Panel();
            mpInfo = new MachinePanelEdgeRounded();
            mlIndustrialPcId = new System.Windows.Forms.Label();
            mlMachineSerial = new System.Windows.Forms.Label();
            mlTitle = new System.Windows.Forms.Label();
            cbReturn = new MachineButton();
            mlSoftwareUpdate = new MachineButtonRectangular();
            mlBackupSystem = new MachineButtonRectangular();
            mlResetSettings = new MachineButtonRectangular();
            mlLocalization = new MachineButtonRectangular();
            mlMachineParameters = new MachineButtonRectangular();
            mlIOSettings = new MachineButtonRectangular();
            mlBackupWorkingsSettings = new MachineButtonRectangular();
            mlLowLevelStatus = new MachineButtonRectangular();
            mlMachineConfiguration = new MachineButtonRectangular();
            mlCmdMaintenance = new MachineButtonRectangular();
            mlMachineEndurance = new MachineButtonRectangular();
            mlAnalogInputsCalibration = new MachineButtonRectangular();
            mlMachineEnduranceLimits = new MachineButtonRectangular();
            mlShutdownApp = new MachineButtonRectangular();
            mlUpdateWorkingSettings = new MachineButtonRectangular();
            mlUpdateSettings = new MachineButtonRectangular();
            mlFilterUWF = new MachineButtonRectangular();
            mlResetMachineEndurance = new MachineButtonRectangular();
            mlBackupLog = new MachineButtonRectangular();
            panelForm.SuspendLayout();
            mpInfo.SuspendLayout();
            SuspendLayout();
            // 
            // mlSoftwareVersion
            // 
            mlSoftwareVersion.AutoSize = true;
            mlSoftwareVersion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSoftwareVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            mlSoftwareVersion.Location = new System.Drawing.Point(15, 13);
            mlSoftwareVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlSoftwareVersion.Name = "mlSoftwareVersion";
            mlSoftwareVersion.Size = new System.Drawing.Size(166, 20);
            mlSoftwareVersion.TabIndex = 23;
            mlSoftwareVersion.Text = "mlSoftwareVersion";
            // 
            // panelForm
            // 
            panelForm.BackColor = System.Drawing.Color.FromArgb(26, 37, 43);
            panelForm.Controls.Add(mpInfo);
            panelForm.Controls.Add(mlTitle);
            panelForm.Controls.Add(cbReturn);
            panelForm.Location = new System.Drawing.Point(0, 0);
            panelForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelForm.Name = "panelForm";
            panelForm.Size = new System.Drawing.Size(1493, 138);
            panelForm.TabIndex = 8;
            // 
            // mpInfo
            // 
            mpInfo.Controls.Add(mlSoftwareVersion);
            mpInfo.Controls.Add(mlIndustrialPcId);
            mpInfo.Controls.Add(mlMachineSerial);
            mpInfo.LineColor = System.Drawing.Color.LightGray;
            mpInfo.LineWidth = 3;
            mpInfo.Location = new System.Drawing.Point(658, 12);
            mpInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpInfo.Name = "mpInfo";
            mpInfo.Radius = 5;
            mpInfo.Size = new System.Drawing.Size(561, 112);
            mpInfo.TabIndex = 39;
            mpInfo.DoubleClick += mpInfo_DoubleClick;
            // 
            // mlIndustrialPcId
            // 
            mlIndustrialPcId.AutoSize = true;
            mlIndustrialPcId.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlIndustrialPcId.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            mlIndustrialPcId.Location = new System.Drawing.Point(15, 73);
            mlIndustrialPcId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlIndustrialPcId.Name = "mlIndustrialPcId";
            mlIndustrialPcId.Size = new System.Drawing.Size(144, 20);
            mlIndustrialPcId.TabIndex = 38;
            mlIndustrialPcId.Text = "mlIndustrialPcId";
            // 
            // mlMachineSerial
            // 
            mlMachineSerial.AutoSize = true;
            mlMachineSerial.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlMachineSerial.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            mlMachineSerial.Location = new System.Drawing.Point(15, 43);
            mlMachineSerial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlMachineSerial.Name = "mlMachineSerial";
            mlMachineSerial.Size = new System.Drawing.Size(146, 20);
            mlMachineSerial.TabIndex = 37;
            mlMachineSerial.Text = "mlMachineSerial";
            // 
            // mlTitle
            // 
            mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlTitle.ForeColor = System.Drawing.Color.White;
            mlTitle.Location = new System.Drawing.Point(139, 9);
            mlTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlTitle.Name = "mlTitle";
            mlTitle.Size = new System.Drawing.Size(337, 118);
            mlTitle.TabIndex = 2;
            mlTitle.Text = "Root Settings Menu";
            mlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbReturn
            // 
            cbReturn.Active = false;
            cbReturn.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbReturn.ActiveBackgroundImage");
            cbReturn.BackColor = System.Drawing.Color.Transparent;
            cbReturn.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbReturn.BackgroundImage");
            cbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbReturn.ButtonSize = 102;
            cbReturn.FlatAppearance.BorderSize = 0;
            cbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbReturn.ForeColor = System.Drawing.Color.Transparent;
            cbReturn.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbReturn.InactiveBackgroundImage");
            cbReturn.Location = new System.Drawing.Point(13, 9);
            cbReturn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbReturn.Name = "cbReturn";
            cbReturn.Size = new System.Drawing.Size(102, 102);
            cbReturn.StateChangeActivated = true;
            cbReturn.TabIndex = 7;
            cbReturn.TabStop = false;
            cbReturn.UseVisualStyleBackColor = false;
            cbReturn.Click += CbReturn_Click;
            // 
            // mlSoftwareUpdate
            // 
            mlSoftwareUpdate.Location = new System.Drawing.Point(423, 317);
            mlSoftwareUpdate.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlSoftwareUpdate.Name = "mlSoftwareUpdate";
            mlSoftwareUpdate.Size = new System.Drawing.Size(343, 69);
            mlSoftwareUpdate.TabIndex = 32;
            mlSoftwareUpdate.Text = "mlSoftwareUpdate";
            mlSoftwareUpdate.Click += MlSoftwareUpdate_Click;
            // 
            // mlBackupSystem
            // 
            mlBackupSystem.Location = new System.Drawing.Point(423, 490);
            mlBackupSystem.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlBackupSystem.Name = "mlBackupSystem";
            mlBackupSystem.Size = new System.Drawing.Size(343, 69);
            mlBackupSystem.TabIndex = 31;
            mlBackupSystem.Text = "mlBackupSystem";
            mlBackupSystem.Click += MlBackupSystemSettings_Click;
            // 
            // mlResetSettings
            // 
            mlResetSettings.Location = new System.Drawing.Point(423, 403);
            mlResetSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlResetSettings.Name = "mlResetSettings";
            mlResetSettings.Size = new System.Drawing.Size(343, 69);
            mlResetSettings.TabIndex = 30;
            mlResetSettings.Text = "mlResetSettings";
            mlResetSettings.Click += MlResetSettings_Click;
            // 
            // mlLocalization
            // 
            mlLocalization.Location = new System.Drawing.Point(423, 144);
            mlLocalization.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlLocalization.Name = "mlLocalization";
            mlLocalization.Size = new System.Drawing.Size(343, 69);
            mlLocalization.TabIndex = 29;
            mlLocalization.Text = "mlLocalization";
            mlLocalization.Click += MlLocalization_Click;
            // 
            // mlMachineParameters
            // 
            mlMachineParameters.Location = new System.Drawing.Point(13, 230);
            mlMachineParameters.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlMachineParameters.Name = "mlMachineParameters";
            mlMachineParameters.Size = new System.Drawing.Size(343, 69);
            mlMachineParameters.TabIndex = 28;
            mlMachineParameters.Text = "mlMachineParameters";
            mlMachineParameters.Click += MlMachineParameters_Click;
            // 
            // mlIOSettings
            // 
            mlIOSettings.Location = new System.Drawing.Point(13, 490);
            mlIOSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlIOSettings.Name = "mlIOSettings";
            mlIOSettings.Size = new System.Drawing.Size(343, 69);
            mlIOSettings.TabIndex = 27;
            mlIOSettings.Text = "mlIOSettings";
            mlIOSettings.Click += MlIOSettings_Click;
            // 
            // mlBackupWorkingsSettings
            // 
            mlBackupWorkingsSettings.Location = new System.Drawing.Point(13, 403);
            mlBackupWorkingsSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlBackupWorkingsSettings.Name = "mlBackupWorkingsSettings";
            mlBackupWorkingsSettings.Size = new System.Drawing.Size(343, 69);
            mlBackupWorkingsSettings.TabIndex = 26;
            mlBackupWorkingsSettings.Text = "mlBackupWorkingsSettings";
            mlBackupWorkingsSettings.Click += MlBackupWorkingSettings_Click;
            // 
            // mlLowLevelStatus
            // 
            mlLowLevelStatus.Location = new System.Drawing.Point(13, 317);
            mlLowLevelStatus.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlLowLevelStatus.Name = "mlLowLevelStatus";
            mlLowLevelStatus.Size = new System.Drawing.Size(343, 69);
            mlLowLevelStatus.TabIndex = 25;
            mlLowLevelStatus.Text = "mlLowLevelStatus";
            mlLowLevelStatus.Click += MlLowLevelStatus_Click;
            // 
            // mlMachineConfiguration
            // 
            mlMachineConfiguration.Location = new System.Drawing.Point(13, 144);
            mlMachineConfiguration.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlMachineConfiguration.Name = "mlMachineConfiguration";
            mlMachineConfiguration.Size = new System.Drawing.Size(343, 69);
            mlMachineConfiguration.TabIndex = 24;
            mlMachineConfiguration.Text = "mlMachineConfiguration";
            mlMachineConfiguration.Click += MlMachineConfiguration_Click;
            // 
            // mlCmdMaintenance
            // 
            mlCmdMaintenance.Location = new System.Drawing.Point(423, 576);
            mlCmdMaintenance.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlCmdMaintenance.Name = "mlCmdMaintenance";
            mlCmdMaintenance.Size = new System.Drawing.Size(343, 69);
            mlCmdMaintenance.TabIndex = 33;
            mlCmdMaintenance.Text = "mlCmdMaintenance";
            mlCmdMaintenance.Click += MlCmdMaintenance_Click;
            // 
            // mlMachineEndurance
            // 
            mlMachineEndurance.Location = new System.Drawing.Point(13, 576);
            mlMachineEndurance.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlMachineEndurance.Name = "mlMachineEndurance";
            mlMachineEndurance.Size = new System.Drawing.Size(343, 69);
            mlMachineEndurance.TabIndex = 34;
            mlMachineEndurance.Text = "mlMachineEndurance";
            mlMachineEndurance.Click += MlMachineEndurance_Click;
            // 
            // mlAnalogInputsCalibration
            // 
            mlAnalogInputsCalibration.Location = new System.Drawing.Point(423, 230);
            mlAnalogInputsCalibration.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlAnalogInputsCalibration.Name = "mlAnalogInputsCalibration";
            mlAnalogInputsCalibration.Size = new System.Drawing.Size(343, 69);
            mlAnalogInputsCalibration.TabIndex = 35;
            mlAnalogInputsCalibration.Text = "mlAnalogInputsCalibration";
            mlAnalogInputsCalibration.Click += MlMachineCalibration_Click;
            // 
            // mlMachineEnduranceLimits
            // 
            mlMachineEnduranceLimits.Location = new System.Drawing.Point(13, 663);
            mlMachineEnduranceLimits.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlMachineEnduranceLimits.Name = "mlMachineEnduranceLimits";
            mlMachineEnduranceLimits.Size = new System.Drawing.Size(343, 69);
            mlMachineEnduranceLimits.TabIndex = 36;
            mlMachineEnduranceLimits.Text = "mlMachineEnduranceLimits";
            mlMachineEnduranceLimits.Click += MlMachineEnduranceLimits_Click;
            // 
            // mlShutdownApp
            // 
            mlShutdownApp.Location = new System.Drawing.Point(423, 663);
            mlShutdownApp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlShutdownApp.Name = "mlShutdownApp";
            mlShutdownApp.Size = new System.Drawing.Size(343, 69);
            mlShutdownApp.TabIndex = 37;
            mlShutdownApp.Text = "mlShutdownApp";
            mlShutdownApp.Click += MlShutdownApp_Click;
            // 
            // mlUpdateWorkingSettings
            // 
            mlUpdateWorkingSettings.Location = new System.Drawing.Point(13, 754);
            mlUpdateWorkingSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlUpdateWorkingSettings.Name = "mlUpdateWorkingSettings";
            mlUpdateWorkingSettings.Size = new System.Drawing.Size(343, 69);
            mlUpdateWorkingSettings.TabIndex = 38;
            mlUpdateWorkingSettings.Text = "mlUpdateWorkingSettings";
            mlUpdateWorkingSettings.Click += MlRestoreWorkingSettings_Click;
            // 
            // mlUpdateSettings
            // 
            mlUpdateSettings.Location = new System.Drawing.Point(423, 754);
            mlUpdateSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlUpdateSettings.Name = "mlUpdateSettings";
            mlUpdateSettings.Size = new System.Drawing.Size(343, 69);
            mlUpdateSettings.TabIndex = 39;
            mlUpdateSettings.Text = "mlUpdateSettings";
            mlUpdateSettings.Click += MlUpdateSettings_Click;
            // 
            // mlFilterUWF
            // 
            mlFilterUWF.Location = new System.Drawing.Point(826, 144);
            mlFilterUWF.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlFilterUWF.Name = "mlFilterUWF";
            mlFilterUWF.Size = new System.Drawing.Size(343, 69);
            mlFilterUWF.TabIndex = 40;
            mlFilterUWF.Text = "mlFilterUWF";
            mlFilterUWF.Click += MlFilterUWF_Click;
            // 
            // mlResetMachineEndurance
            // 
            mlResetMachineEndurance.Location = new System.Drawing.Point(826, 230);
            mlResetMachineEndurance.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlResetMachineEndurance.Name = "mlResetMachineEndurance";
            mlResetMachineEndurance.Size = new System.Drawing.Size(343, 69);
            mlResetMachineEndurance.TabIndex = 41;
            mlResetMachineEndurance.Text = "mlResetMachineEndurance";
            mlResetMachineEndurance.Click += MlResetMachineEndurance_Click;
            // 
            // mlBackupLog
            // 
            mlBackupLog.Location = new System.Drawing.Point(826, 317);
            mlBackupLog.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mlBackupLog.Name = "mlBackupLog";
            mlBackupLog.Size = new System.Drawing.Size(343, 69);
            mlBackupLog.TabIndex = 45;
            mlBackupLog.Text = "mlBackupLog";
            mlBackupLog.Click += mlBackupLog_Click;
            // 
            // FormRootSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(mlBackupLog);
            Controls.Add(mlResetMachineEndurance);
            Controls.Add(mlFilterUWF);
            Controls.Add(mlUpdateSettings);
            Controls.Add(mlUpdateWorkingSettings);
            Controls.Add(mlShutdownApp);
            Controls.Add(mlMachineEnduranceLimits);
            Controls.Add(mlAnalogInputsCalibration);
            Controls.Add(mlMachineEndurance);
            Controls.Add(mlCmdMaintenance);
            Controls.Add(mlSoftwareUpdate);
            Controls.Add(mlBackupSystem);
            Controls.Add(mlResetSettings);
            Controls.Add(mlLocalization);
            Controls.Add(mlMachineParameters);
            Controls.Add(mlIOSettings);
            Controls.Add(mlBackupWorkingsSettings);
            Controls.Add(mlLowLevelStatus);
            Controls.Add(mlMachineConfiguration);
            Controls.Add(panelForm);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormRootSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormRootSettingsMenu";
            Load += FormRootSettings_Load;
            panelForm.ResumeLayout(false);
            mpInfo.ResumeLayout(false);
            mpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label mlTitle;
        private System.Windows.Forms.Panel panelForm;
        private Machine.UI.Controls.MachineButton cbReturn;
        private System.Windows.Forms.Label mlSoftwareVersion;
        private MachineButtonRectangular mlMachineConfiguration;
        private MachineButtonRectangular mlLowLevelStatus;
        private MachineButtonRectangular mlBackupWorkingsSettings;
        private MachineButtonRectangular mlIOSettings;
        private MachineButtonRectangular mlMachineParameters;
        private MachineButtonRectangular mlSoftwareUpdate;
        private MachineButtonRectangular mlBackupSystem;
        private MachineButtonRectangular mlResetSettings;
        private MachineButtonRectangular mlLocalization;
        private MachineButtonRectangular mlCmdMaintenance;
        private MachineButtonRectangular mlMachineEndurance;
        private MachineButtonRectangular mlAnalogInputsCalibration;
        private MachineButtonRectangular mlMachineEnduranceLimits;
        private System.Windows.Forms.Label mlMachineSerial;
        private System.Windows.Forms.Label mlIndustrialPcId;
        private MachinePanelEdgeRounded mpInfo;
        private MachineButtonRectangular mlShutdownApp;
        private MachineButtonRectangular mlUpdateWorkingSettings;
        private MachineButtonRectangular mlUpdateSettings;
        private MachineButtonRectangular mlFilterUWF;
        private MachineButtonRectangular mlResetMachineEndurance;
        private MachineButtonRectangular mlBackupLog;
    }
}