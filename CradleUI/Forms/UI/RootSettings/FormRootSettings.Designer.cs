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
            this.mlSoftwareVersion = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.mpInfo = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlIndustrialPcId = new System.Windows.Forms.Label();
            this.mlMachineSerial = new System.Windows.Forms.Label();
            this.mlTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.mlSoftwareUpdate = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlBackupSystem = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlResetSettings = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlLocalization = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlMachineParameters = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlIOSettings = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlBackupWorkingsSettings = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlLowLevelStatus = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlMachineConfiguration = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlCmdMaintenance = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlMachineEndurance = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlAnalogInputsCalibration = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlMachineEnduranceLimits = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlShutdownApp = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlUpdateWorkingSettings = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlUpdateSettings = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlFilterUWF = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlResetMachineEndurance = new Machine.UI.Controls.MachineButtonRectangular();
            this.mlBackupLog = new Machine.UI.Controls.MachineButtonRectangular();
            this.panelForm.SuspendLayout();
            this.mpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // mlSoftwareVersion
            // 
            this.mlSoftwareVersion.AutoSize = true;
            this.mlSoftwareVersion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSoftwareVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mlSoftwareVersion.Location = new System.Drawing.Point(13, 11);
            this.mlSoftwareVersion.Name = "mlSoftwareVersion";
            this.mlSoftwareVersion.Size = new System.Drawing.Size(166, 20);
            this.mlSoftwareVersion.TabIndex = 23;
            this.mlSoftwareVersion.Text = "mlSoftwareVersion";
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.mpInfo);
            this.panelForm.Controls.Add(this.mlTitle);
            this.panelForm.Controls.Add(this.cbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1280, 120);
            this.panelForm.TabIndex = 8;
            // 
            // mpInfo
            // 
            this.mpInfo.Controls.Add(this.mlSoftwareVersion);
            this.mpInfo.Controls.Add(this.mlIndustrialPcId);
            this.mpInfo.Controls.Add(this.mlMachineSerial);
            this.mpInfo.LineColor = System.Drawing.Color.LightGray;
            this.mpInfo.LineWidth = 3;
            this.mpInfo.Location = new System.Drawing.Point(774, 13);
            this.mpInfo.Name = "mpInfo";
            this.mpInfo.Radius = 5;
            this.mpInfo.Size = new System.Drawing.Size(481, 97);
            this.mpInfo.TabIndex = 39;
            this.mpInfo.DoubleClick += new System.EventHandler(this.mpInfo_DoubleClick);
            // 
            // mlIndustrialPcId
            // 
            this.mlIndustrialPcId.AutoSize = true;
            this.mlIndustrialPcId.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlIndustrialPcId.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mlIndustrialPcId.Location = new System.Drawing.Point(13, 63);
            this.mlIndustrialPcId.Name = "mlIndustrialPcId";
            this.mlIndustrialPcId.Size = new System.Drawing.Size(144, 20);
            this.mlIndustrialPcId.TabIndex = 38;
            this.mlIndustrialPcId.Text = "mlIndustrialPcId";
            // 
            // mlMachineSerial
            // 
            this.mlMachineSerial.AutoSize = true;
            this.mlMachineSerial.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlMachineSerial.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mlMachineSerial.Location = new System.Drawing.Point(13, 37);
            this.mlMachineSerial.Name = "mlMachineSerial";
            this.mlMachineSerial.Size = new System.Drawing.Size(146, 20);
            this.mlMachineSerial.TabIndex = 37;
            this.mlMachineSerial.Text = "mlMachineSerial";
            // 
            // mlTitle
            // 
            this.mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTitle.ForeColor = System.Drawing.Color.White;
            this.mlTitle.Location = new System.Drawing.Point(119, 8);
            this.mlTitle.Name = "mlTitle";
            this.mlTitle.Size = new System.Drawing.Size(289, 102);
            this.mlTitle.TabIndex = 2;
            this.mlTitle.Text = "Root Settings Menu";
            this.mlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbReturn
            // 
            this.cbReturn.Active = false;
            this.cbReturn.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.ActiveBackgroundImage")));
            this.cbReturn.BackColor = System.Drawing.Color.Transparent;
            this.cbReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbReturn.BackgroundImage")));
            this.cbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbReturn.ButtonSize = 102;
            this.cbReturn.FlatAppearance.BorderSize = 0;
            this.cbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbReturn.ForeColor = System.Drawing.Color.Transparent;
            this.cbReturn.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.InactiveBackgroundImage")));
            this.cbReturn.Location = new System.Drawing.Point(11, 8);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(102, 102);
            this.cbReturn.StateChangeActivated = true;
            this.cbReturn.TabIndex = 7;
            this.cbReturn.TabStop = false;
            this.cbReturn.UseVisualStyleBackColor = false;
            this.cbReturn.Click += new System.EventHandler(this.CbReturn_Click);
            // 
            // mlSoftwareUpdate
            // 
            this.mlSoftwareUpdate.Location = new System.Drawing.Point(514, 277);
            this.mlSoftwareUpdate.Name = "mlSoftwareUpdate";
            this.mlSoftwareUpdate.Size = new System.Drawing.Size(294, 60);
            this.mlSoftwareUpdate.TabIndex = 32;
            this.mlSoftwareUpdate.Text = "mlSoftwareUpdate";
            this.mlSoftwareUpdate.Click += new System.EventHandler(this.MlSoftwareUpdate_Click);
            // 
            // mlBackupSystem
            // 
            this.mlBackupSystem.Location = new System.Drawing.Point(514, 427);
            this.mlBackupSystem.Name = "mlBackupSystem";
            this.mlBackupSystem.Size = new System.Drawing.Size(294, 60);
            this.mlBackupSystem.TabIndex = 31;
            this.mlBackupSystem.Text = "mlBackupSystem";
            this.mlBackupSystem.Click += new System.EventHandler(this.MlBackupSystemSettings_Click);
            // 
            // mlResetSettings
            // 
            this.mlResetSettings.Location = new System.Drawing.Point(514, 352);
            this.mlResetSettings.Name = "mlResetSettings";
            this.mlResetSettings.Size = new System.Drawing.Size(294, 60);
            this.mlResetSettings.TabIndex = 30;
            this.mlResetSettings.Text = "mlResetSettings";
            this.mlResetSettings.Click += new System.EventHandler(this.MlResetSettings_Click);
            // 
            // mlLocalization
            // 
            this.mlLocalization.Location = new System.Drawing.Point(514, 127);
            this.mlLocalization.Name = "mlLocalization";
            this.mlLocalization.Size = new System.Drawing.Size(294, 60);
            this.mlLocalization.TabIndex = 29;
            this.mlLocalization.Text = "mlLocalization";
            this.mlLocalization.Click += new System.EventHandler(this.MlLocalization_Click);
            // 
            // mlMachineParameters
            // 
            this.mlMachineParameters.Location = new System.Drawing.Point(94, 202);
            this.mlMachineParameters.Name = "mlMachineParameters";
            this.mlMachineParameters.Size = new System.Drawing.Size(294, 60);
            this.mlMachineParameters.TabIndex = 28;
            this.mlMachineParameters.Text = "mlMachineParameters";
            this.mlMachineParameters.Click += new System.EventHandler(this.MlMachineParameters_Click);
            // 
            // mlIOSettings
            // 
            this.mlIOSettings.Location = new System.Drawing.Point(94, 427);
            this.mlIOSettings.Name = "mlIOSettings";
            this.mlIOSettings.Size = new System.Drawing.Size(294, 60);
            this.mlIOSettings.TabIndex = 27;
            this.mlIOSettings.Text = "mlIOSettings";
            this.mlIOSettings.Click += new System.EventHandler(this.MlIOSettings_Click);
            // 
            // mlBackupWorkingsSettings
            // 
            this.mlBackupWorkingsSettings.Location = new System.Drawing.Point(94, 352);
            this.mlBackupWorkingsSettings.Name = "mlBackupWorkingsSettings";
            this.mlBackupWorkingsSettings.Size = new System.Drawing.Size(294, 60);
            this.mlBackupWorkingsSettings.TabIndex = 26;
            this.mlBackupWorkingsSettings.Text = "mlBackupWorkingsSettings";
            this.mlBackupWorkingsSettings.Click += new System.EventHandler(this.MlBackupWorkingSettings_Click);
            // 
            // mlLowLevelStatus
            // 
            this.mlLowLevelStatus.Location = new System.Drawing.Point(94, 277);
            this.mlLowLevelStatus.Name = "mlLowLevelStatus";
            this.mlLowLevelStatus.Size = new System.Drawing.Size(294, 60);
            this.mlLowLevelStatus.TabIndex = 25;
            this.mlLowLevelStatus.Text = "mlLowLevelStatus";
            this.mlLowLevelStatus.Click += new System.EventHandler(this.MlLowLevelStatus_Click);
            // 
            // mlMachineConfiguration
            // 
            this.mlMachineConfiguration.Location = new System.Drawing.Point(94, 127);
            this.mlMachineConfiguration.Name = "mlMachineConfiguration";
            this.mlMachineConfiguration.Size = new System.Drawing.Size(294, 60);
            this.mlMachineConfiguration.TabIndex = 24;
            this.mlMachineConfiguration.Text = "mlMachineConfiguration";
            this.mlMachineConfiguration.Click += new System.EventHandler(this.MlMachineConfiguration_Click);
            // 
            // mlCmdMaintenance
            // 
            this.mlCmdMaintenance.Location = new System.Drawing.Point(514, 502);
            this.mlCmdMaintenance.Name = "mlCmdMaintenance";
            this.mlCmdMaintenance.Size = new System.Drawing.Size(294, 60);
            this.mlCmdMaintenance.TabIndex = 33;
            this.mlCmdMaintenance.Text = "mlCmdMaintenance";
            this.mlCmdMaintenance.Click += new System.EventHandler(this.MlCmdMaintenance_Click);
            // 
            // mlMachineEndurance
            // 
            this.mlMachineEndurance.Location = new System.Drawing.Point(94, 502);
            this.mlMachineEndurance.Name = "mlMachineEndurance";
            this.mlMachineEndurance.Size = new System.Drawing.Size(294, 60);
            this.mlMachineEndurance.TabIndex = 34;
            this.mlMachineEndurance.Text = "mlMachineEndurance";
            this.mlMachineEndurance.Click += new System.EventHandler(this.MlMachineEndurance_Click);
            // 
            // mlAnalogInputsCalibration
            // 
            this.mlAnalogInputsCalibration.Location = new System.Drawing.Point(514, 202);
            this.mlAnalogInputsCalibration.Name = "mlAnalogInputsCalibration";
            this.mlAnalogInputsCalibration.Size = new System.Drawing.Size(294, 60);
            this.mlAnalogInputsCalibration.TabIndex = 35;
            this.mlAnalogInputsCalibration.Text = "mlAnalogInputsCalibration";
            this.mlAnalogInputsCalibration.Click += new System.EventHandler(this.MlMachineCalibration_Click);
            // 
            // mlMachineEnduranceLimits
            // 
            this.mlMachineEnduranceLimits.Location = new System.Drawing.Point(94, 577);
            this.mlMachineEnduranceLimits.Name = "mlMachineEnduranceLimits";
            this.mlMachineEnduranceLimits.Size = new System.Drawing.Size(294, 60);
            this.mlMachineEnduranceLimits.TabIndex = 36;
            this.mlMachineEnduranceLimits.Text = "mlMachineEnduranceLimits";
            this.mlMachineEnduranceLimits.Click += new System.EventHandler(this.MlMachineEnduranceLimits_Click);
            // 
            // mlShutdownApp
            // 
            this.mlShutdownApp.Location = new System.Drawing.Point(514, 577);
            this.mlShutdownApp.Name = "mlShutdownApp";
            this.mlShutdownApp.Size = new System.Drawing.Size(294, 60);
            this.mlShutdownApp.TabIndex = 37;
            this.mlShutdownApp.Text = "mlShutdownApp";
            this.mlShutdownApp.Click += new System.EventHandler(this.MlShutdownApp_Click);
            // 
            // mlUpdateWorkingSettings
            // 
            this.mlUpdateWorkingSettings.Location = new System.Drawing.Point(94, 656);
            this.mlUpdateWorkingSettings.Name = "mlUpdateWorkingSettings";
            this.mlUpdateWorkingSettings.Size = new System.Drawing.Size(294, 60);
            this.mlUpdateWorkingSettings.TabIndex = 38;
            this.mlUpdateWorkingSettings.Text = "mlUpdateWorkingSettings";
            this.mlUpdateWorkingSettings.Click += new System.EventHandler(this.MlRestoreWorkingSettings_Click);
            // 
            // mlUpdateSettings
            // 
            this.mlUpdateSettings.Location = new System.Drawing.Point(514, 656);
            this.mlUpdateSettings.Name = "mlUpdateSettings";
            this.mlUpdateSettings.Size = new System.Drawing.Size(294, 60);
            this.mlUpdateSettings.TabIndex = 39;
            this.mlUpdateSettings.Text = "mlUpdateSettings";
            this.mlUpdateSettings.Click += new System.EventHandler(this.MlUpdateSettings_Click);
            // 
            // mlFilterUWF
            // 
            this.mlFilterUWF.Location = new System.Drawing.Point(907, 127);
            this.mlFilterUWF.Name = "mlFilterUWF";
            this.mlFilterUWF.Size = new System.Drawing.Size(294, 60);
            this.mlFilterUWF.TabIndex = 40;
            this.mlFilterUWF.Text = "mlFilterUWF";
            this.mlFilterUWF.Click += new System.EventHandler(this.MlFilterUWF_Click);
            // 
            // mlResetMachineEndurance
            // 
            this.mlResetMachineEndurance.Location = new System.Drawing.Point(907, 202);
            this.mlResetMachineEndurance.Name = "mlResetMachineEndurance";
            this.mlResetMachineEndurance.Size = new System.Drawing.Size(294, 60);
            this.mlResetMachineEndurance.TabIndex = 41;
            this.mlResetMachineEndurance.Text = "mlResetMachineEndurance";
            this.mlResetMachineEndurance.Click += new System.EventHandler(this.MlResetMachineEndurance_Click);
            // 
            // mlBackupLog
            // 
            this.mlBackupLog.Location = new System.Drawing.Point(907, 277);
            this.mlBackupLog.Name = "mlBackupLog";
            this.mlBackupLog.Size = new System.Drawing.Size(294, 60);
            this.mlBackupLog.TabIndex = 45;
            this.mlBackupLog.Text = "mlBackupLog";
            this.mlBackupLog.Click += new System.EventHandler(this.mlBackupLog_Click);
            // 
            // FormRootSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.mlBackupLog);
            this.Controls.Add(this.mlResetMachineEndurance);
            this.Controls.Add(this.mlFilterUWF);
            this.Controls.Add(this.mlUpdateSettings);
            this.Controls.Add(this.mlUpdateWorkingSettings);
            this.Controls.Add(this.mlShutdownApp);
            this.Controls.Add(this.mlMachineEnduranceLimits);
            this.Controls.Add(this.mlAnalogInputsCalibration);
            this.Controls.Add(this.mlMachineEndurance);
            this.Controls.Add(this.mlCmdMaintenance);
            this.Controls.Add(this.mlSoftwareUpdate);
            this.Controls.Add(this.mlBackupSystem);
            this.Controls.Add(this.mlResetSettings);
            this.Controls.Add(this.mlLocalization);
            this.Controls.Add(this.mlMachineParameters);
            this.Controls.Add(this.mlIOSettings);
            this.Controls.Add(this.mlBackupWorkingsSettings);
            this.Controls.Add(this.mlLowLevelStatus);
            this.Controls.Add(this.mlMachineConfiguration);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRootSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRootSettingsMenu";
            this.Load += new System.EventHandler(this.FormRootSettings_Load);
            this.panelForm.ResumeLayout(false);
            this.mpInfo.ResumeLayout(false);
            this.mpInfo.PerformLayout();
            this.ResumeLayout(false);

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