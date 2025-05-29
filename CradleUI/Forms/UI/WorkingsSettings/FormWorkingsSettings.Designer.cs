namespace Caron.Cradle.UI
{
    partial class FormWorkingsSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkingsSettings));
            this.panelCurrentSettings = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlCurrentPhotocellAlignment = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentStraightRollerUp = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentPhotocellMaterialPresence = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentCradleVelocity = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentCutterVelocity = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentWorkingMode = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentParameters = new Machine.UI.Controls.MachineLabel();
            this.panelSelectedSettings = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlPhotocellMaterialPresence = new Machine.UI.Controls.MachineLabel();
            this.mlDate = new Machine.UI.Controls.MachineLabel();
            this.mlSelectedWorkingsSettingsVisualization = new Machine.UI.Controls.MachineLabel();
            this.mlWorkingMode = new Machine.UI.Controls.MachineLabel();
            this.mlCradleVelocity = new Machine.UI.Controls.MachineLabel();
            this.mlCutterVelocity = new Machine.UI.Controls.MachineLabel();
            this.mlPhotocellAlignment = new Machine.UI.Controls.MachineLabel();
            this.mlStraightRollerUp = new Machine.UI.Controls.MachineLabel();
            this.panelFlowControl = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.clSettings = new Machine.UI.Controls.MachineStringEditableItemsListbox();
            this.mbUp = new Machine.UI.Controls.MachineButton();
            this.mbDown = new Machine.UI.Controls.MachineButton();
            this.panelButtons = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlRename = new Machine.UI.Controls.MachineLabel();
            this.mlSaveWithName = new Machine.UI.Controls.MachineLabel();
            this.mlSave = new Machine.UI.Controls.MachineLabel();
            this.mlDelete = new Machine.UI.Controls.MachineLabel();
            this.mlNew = new Machine.UI.Controls.MachineLabel();
            this.mlApply = new Machine.UI.Controls.MachineLabel();
            this.mbRename = new Machine.UI.Controls.MachineButton();
            this.mbSaveWithName = new Machine.UI.Controls.MachineButton();
            this.mbApply = new Machine.UI.Controls.MachineButton();
            this.mbSave = new Machine.UI.Controls.MachineButton();
            this.mbNew = new Machine.UI.Controls.MachineButton();
            this.mbDelete = new Machine.UI.Controls.MachineButton();
            this.mlPreFeed = new Machine.UI.Controls.MachineLabel();
            this.mlCurrentPreFeed = new Machine.UI.Controls.MachineLabel();
            this.panelCurrentSettings.SuspendLayout();
            this.panelSelectedSettings.SuspendLayout();
            this.panelFlowControl.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCurrentSettings
            // 
            this.panelCurrentSettings.Controls.Add(this.mlCurrentPreFeed);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentPhotocellAlignment);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentStraightRollerUp);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentPhotocellMaterialPresence);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentCradleVelocity);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentCutterVelocity);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentWorkingMode);
            this.panelCurrentSettings.Controls.Add(this.mlCurrentParameters);
            this.panelCurrentSettings.LineColor = System.Drawing.Color.LightGray;
            this.panelCurrentSettings.LineWidth = 4;
            this.panelCurrentSettings.Location = new System.Drawing.Point(580, 18);
            this.panelCurrentSettings.Name = "panelCurrentSettings";
            this.panelCurrentSettings.Radius = 5;
            this.panelCurrentSettings.Size = new System.Drawing.Size(482, 230);
            this.panelCurrentSettings.TabIndex = 28;
            // 
            // mlCurrentPhotocellAlignment
            // 
            this.mlCurrentPhotocellAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentPhotocellAlignment.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentPhotocellAlignment.Location = new System.Drawing.Point(9, 170);
            this.mlCurrentPhotocellAlignment.Name = "mlCurrentPhotocellAlignment";
            this.mlCurrentPhotocellAlignment.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentPhotocellAlignment.TabIndex = 32;
            this.mlCurrentPhotocellAlignment.Text = "clPhotocellAlignment";
            this.mlCurrentPhotocellAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentStraightRollerUp
            // 
            this.mlCurrentStraightRollerUp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentStraightRollerUp.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentStraightRollerUp.Location = new System.Drawing.Point(9, 128);
            this.mlCurrentStraightRollerUp.Name = "mlCurrentStraightRollerUp";
            this.mlCurrentStraightRollerUp.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentStraightRollerUp.TabIndex = 31;
            this.mlCurrentStraightRollerUp.Text = "clCurrentStraightRollerUp";
            this.mlCurrentStraightRollerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentPhotocellMaterialPresence
            // 
            this.mlCurrentPhotocellMaterialPresence.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentPhotocellMaterialPresence.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentPhotocellMaterialPresence.Location = new System.Drawing.Point(9, 149);
            this.mlCurrentPhotocellMaterialPresence.Name = "mlCurrentPhotocellMaterialPresence";
            this.mlCurrentPhotocellMaterialPresence.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentPhotocellMaterialPresence.TabIndex = 30;
            this.mlCurrentPhotocellMaterialPresence.Text = "clPhotocellMaterialPresence";
            this.mlCurrentPhotocellMaterialPresence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentCradleVelocity
            // 
            this.mlCurrentCradleVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentCradleVelocity.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentCradleVelocity.Location = new System.Drawing.Point(9, 107);
            this.mlCurrentCradleVelocity.Name = "mlCurrentCradleVelocity";
            this.mlCurrentCradleVelocity.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentCradleVelocity.TabIndex = 29;
            this.mlCurrentCradleVelocity.Text = "clCurrentCradleVelocity";
            this.mlCurrentCradleVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentCutterVelocity
            // 
            this.mlCurrentCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentCutterVelocity.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentCutterVelocity.Location = new System.Drawing.Point(9, 86);
            this.mlCurrentCutterVelocity.Name = "mlCurrentCutterVelocity";
            this.mlCurrentCutterVelocity.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentCutterVelocity.TabIndex = 28;
            this.mlCurrentCutterVelocity.Text = "clCurrentCutterVelocity";
            this.mlCurrentCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentWorkingMode
            // 
            this.mlCurrentWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentWorkingMode.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentWorkingMode.Location = new System.Drawing.Point(9, 65);
            this.mlCurrentWorkingMode.Name = "mlCurrentWorkingMode";
            this.mlCurrentWorkingMode.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentWorkingMode.TabIndex = 27;
            this.mlCurrentWorkingMode.Text = "clCurrentWorkingMode";
            this.mlCurrentWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentParameters
            // 
            this.mlCurrentParameters.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentParameters.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.mlCurrentParameters.Location = new System.Drawing.Point(16, 7);
            this.mlCurrentParameters.Name = "mlCurrentParameters";
            this.mlCurrentParameters.Size = new System.Drawing.Size(460, 27);
            this.mlCurrentParameters.TabIndex = 26;
            this.mlCurrentParameters.Text = "clCurrentParameters";
            this.mlCurrentParameters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSelectedSettings
            // 
            this.panelSelectedSettings.Controls.Add(this.mlPreFeed);
            this.panelSelectedSettings.Controls.Add(this.mlPhotocellMaterialPresence);
            this.panelSelectedSettings.Controls.Add(this.mlDate);
            this.panelSelectedSettings.Controls.Add(this.mlSelectedWorkingsSettingsVisualization);
            this.panelSelectedSettings.Controls.Add(this.mlWorkingMode);
            this.panelSelectedSettings.Controls.Add(this.mlCradleVelocity);
            this.panelSelectedSettings.Controls.Add(this.mlCutterVelocity);
            this.panelSelectedSettings.Controls.Add(this.mlPhotocellAlignment);
            this.panelSelectedSettings.Controls.Add(this.mlStraightRollerUp);
            this.panelSelectedSettings.LineColor = System.Drawing.Color.LightGray;
            this.panelSelectedSettings.LineWidth = 4;
            this.panelSelectedSettings.Location = new System.Drawing.Point(83, 18);
            this.panelSelectedSettings.Name = "panelSelectedSettings";
            this.panelSelectedSettings.Radius = 5;
            this.panelSelectedSettings.Size = new System.Drawing.Size(482, 230);
            this.panelSelectedSettings.TabIndex = 27;
            // 
            // mlPhotocellMaterialPresence
            // 
            this.mlPhotocellMaterialPresence.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlPhotocellMaterialPresence.Location = new System.Drawing.Point(11, 149);
            this.mlPhotocellMaterialPresence.Name = "mlPhotocellMaterialPresence";
            this.mlPhotocellMaterialPresence.Size = new System.Drawing.Size(437, 20);
            this.mlPhotocellMaterialPresence.TabIndex = 28;
            this.mlPhotocellMaterialPresence.Text = "clPhotocellMaterialPresence";
            this.mlPhotocellMaterialPresence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlDate
            // 
            this.mlDate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDate.Location = new System.Drawing.Point(11, 44);
            this.mlDate.Name = "mlDate";
            this.mlDate.Size = new System.Drawing.Size(437, 20);
            this.mlDate.TabIndex = 27;
            this.mlDate.Text = "clDate";
            this.mlDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlSelectedWorkingsSettingsVisualization
            // 
            this.mlSelectedWorkingsSettingsVisualization.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSelectedWorkingsSettingsVisualization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlSelectedWorkingsSettingsVisualization.Location = new System.Drawing.Point(16, 6);
            this.mlSelectedWorkingsSettingsVisualization.Name = "mlSelectedWorkingsSettingsVisualization";
            this.mlSelectedWorkingsSettingsVisualization.Size = new System.Drawing.Size(432, 29);
            this.mlSelectedWorkingsSettingsVisualization.TabIndex = 26;
            this.mlSelectedWorkingsSettingsVisualization.Text = "clSelectedSettingsVisualization";
            this.mlSelectedWorkingsSettingsVisualization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlWorkingMode
            // 
            this.mlWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingMode.Location = new System.Drawing.Point(11, 65);
            this.mlWorkingMode.Name = "mlWorkingMode";
            this.mlWorkingMode.Size = new System.Drawing.Size(437, 20);
            this.mlWorkingMode.TabIndex = 21;
            this.mlWorkingMode.Text = "clWorkingMode";
            this.mlWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCradleVelocity
            // 
            this.mlCradleVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCradleVelocity.Location = new System.Drawing.Point(11, 107);
            this.mlCradleVelocity.Name = "mlCradleVelocity";
            this.mlCradleVelocity.Size = new System.Drawing.Size(437, 20);
            this.mlCradleVelocity.TabIndex = 25;
            this.mlCradleVelocity.Text = "clCradleVelocity";
            this.mlCradleVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCutterVelocity
            // 
            this.mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutterVelocity.Location = new System.Drawing.Point(11, 86);
            this.mlCutterVelocity.Name = "mlCutterVelocity";
            this.mlCutterVelocity.Size = new System.Drawing.Size(437, 20);
            this.mlCutterVelocity.TabIndex = 22;
            this.mlCutterVelocity.Text = "clCutterVelocity";
            this.mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlPhotocellAlignment
            // 
            this.mlPhotocellAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlPhotocellAlignment.Location = new System.Drawing.Point(11, 170);
            this.mlPhotocellAlignment.Name = "mlPhotocellAlignment";
            this.mlPhotocellAlignment.Size = new System.Drawing.Size(437, 20);
            this.mlPhotocellAlignment.TabIndex = 24;
            this.mlPhotocellAlignment.Text = "clPhotocellAlignment";
            this.mlPhotocellAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlStraightRollerUp
            // 
            this.mlStraightRollerUp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlStraightRollerUp.Location = new System.Drawing.Point(11, 128);
            this.mlStraightRollerUp.Name = "mlStraightRollerUp";
            this.mlStraightRollerUp.Size = new System.Drawing.Size(437, 20);
            this.mlStraightRollerUp.TabIndex = 23;
            this.mlStraightRollerUp.Text = "clStraightRollerUp";
            this.mlStraightRollerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFlowControl
            // 
            this.panelFlowControl.Controls.Add(this.clSettings);
            this.panelFlowControl.LineColor = System.Drawing.Color.LightGray;
            this.panelFlowControl.LineWidth = 4;
            this.panelFlowControl.Location = new System.Drawing.Point(83, 254);
            this.panelFlowControl.Name = "panelFlowControl";
            this.panelFlowControl.Radius = 5;
            this.panelFlowControl.Size = new System.Drawing.Size(482, 446);
            this.panelFlowControl.TabIndex = 28;
            // 
            // clSettings
            // 
            this.clSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clSettings.Location = new System.Drawing.Point(7, 7);
            this.clSettings.Name = "clSettings";
            this.clSettings.Size = new System.Drawing.Size(465, 432);
            this.clSettings.TabIndex = 10;
            // 
            // mbUp
            // 
            this.mbUp.Active = false;
            this.mbUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbUp.ActiveBackgroundImage")));
            this.mbUp.BackColor = System.Drawing.Color.Transparent;
            this.mbUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbUp.BackgroundImage")));
            this.mbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbUp.ButtonSize = 85;
            this.mbUp.FlatAppearance.BorderSize = 0;
            this.mbUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbUp.ForeColor = System.Drawing.Color.Transparent;
            this.mbUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbUp.InactiveBackgroundImage")));
            this.mbUp.Location = new System.Drawing.Point(571, 254);
            this.mbUp.Name = "mbUp";
            this.mbUp.Size = new System.Drawing.Size(85, 85);
            this.mbUp.StateChangeActivated = true;
            this.mbUp.TabIndex = 16;
            this.mbUp.TabStop = false;
            this.mbUp.UseVisualStyleBackColor = false;
            this.mbUp.Click += new System.EventHandler(this.MbUp_Click);
            // 
            // mbDown
            // 
            this.mbDown.Active = false;
            this.mbDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDown.ActiveBackgroundImage")));
            this.mbDown.BackColor = System.Drawing.Color.Transparent;
            this.mbDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDown.BackgroundImage")));
            this.mbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDown.ButtonSize = 85;
            this.mbDown.FlatAppearance.BorderSize = 0;
            this.mbDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDown.ForeColor = System.Drawing.Color.Transparent;
            this.mbDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDown.InactiveBackgroundImage")));
            this.mbDown.Location = new System.Drawing.Point(571, 625);
            this.mbDown.Name = "mbDown";
            this.mbDown.Size = new System.Drawing.Size(85, 85);
            this.mbDown.StateChangeActivated = true;
            this.mbDown.TabIndex = 15;
            this.mbDown.TabStop = false;
            this.mbDown.UseVisualStyleBackColor = false;
            this.mbDown.Click += new System.EventHandler(this.MbDown_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.mlRename);
            this.panelButtons.Controls.Add(this.mlSaveWithName);
            this.panelButtons.Controls.Add(this.mlSave);
            this.panelButtons.Controls.Add(this.mlDelete);
            this.panelButtons.Controls.Add(this.mlNew);
            this.panelButtons.Controls.Add(this.mlApply);
            this.panelButtons.Controls.Add(this.mbRename);
            this.panelButtons.Controls.Add(this.mbSaveWithName);
            this.panelButtons.Controls.Add(this.mbApply);
            this.panelButtons.Controls.Add(this.mbSave);
            this.panelButtons.Controls.Add(this.mbNew);
            this.panelButtons.Controls.Add(this.mbDelete);
            this.panelButtons.LineColor = System.Drawing.Color.LightGray;
            this.panelButtons.LineWidth = 4;
            this.panelButtons.Location = new System.Drawing.Point(757, 254);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Radius = 10;
            this.panelButtons.Size = new System.Drawing.Size(305, 446);
            this.panelButtons.TabIndex = 29;
            // 
            // mlRename
            // 
            this.mlRename.AutoSize = true;
            this.mlRename.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlRename.Location = new System.Drawing.Point(83, 386);
            this.mlRename.Name = "mlRename";
            this.mlRename.Size = new System.Drawing.Size(101, 22);
            this.mlRename.TabIndex = 37;
            this.mlRename.Text = "clRename";
            // 
            // mlSaveWithName
            // 
            this.mlSaveWithName.AutoSize = true;
            this.mlSaveWithName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSaveWithName.Location = new System.Drawing.Point(83, 316);
            this.mlSaveWithName.Name = "mlSaveWithName";
            this.mlSaveWithName.Size = new System.Drawing.Size(165, 22);
            this.mlSaveWithName.TabIndex = 36;
            this.mlSaveWithName.Text = "clSaveWithName";
            // 
            // mlSave
            // 
            this.mlSave.AutoSize = true;
            this.mlSave.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSave.Location = new System.Drawing.Point(83, 246);
            this.mlSave.Name = "mlSave";
            this.mlSave.Size = new System.Drawing.Size(71, 22);
            this.mlSave.TabIndex = 35;
            this.mlSave.Text = "clSave";
            // 
            // mlDelete
            // 
            this.mlDelete.AutoSize = true;
            this.mlDelete.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDelete.Location = new System.Drawing.Point(83, 176);
            this.mlDelete.Name = "mlDelete";
            this.mlDelete.Size = new System.Drawing.Size(85, 22);
            this.mlDelete.TabIndex = 34;
            this.mlDelete.Text = "clDelete";
            // 
            // mlNew
            // 
            this.mlNew.AutoSize = true;
            this.mlNew.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlNew.Location = new System.Drawing.Point(83, 106);
            this.mlNew.Name = "mlNew";
            this.mlNew.Size = new System.Drawing.Size(66, 22);
            this.mlNew.TabIndex = 33;
            this.mlNew.Text = "clNew";
            // 
            // mlApply
            // 
            this.mlApply.AutoSize = true;
            this.mlApply.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlApply.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mlApply.Location = new System.Drawing.Point(83, 36);
            this.mlApply.Name = "mlApply";
            this.mlApply.Size = new System.Drawing.Size(79, 22);
            this.mlApply.TabIndex = 32;
            this.mlApply.Text = "clApply";
            // 
            // mbRename
            // 
            this.mbRename.Active = false;
            this.mbRename.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbRename.ActiveBackgroundImage")));
            this.mbRename.BackColor = System.Drawing.Color.Transparent;
            this.mbRename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbRename.BackgroundImage")));
            this.mbRename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbRename.ButtonSize = 60;
            this.mbRename.FlatAppearance.BorderSize = 0;
            this.mbRename.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbRename.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbRename.ForeColor = System.Drawing.Color.Transparent;
            this.mbRename.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbRename.InactiveBackgroundImage")));
            this.mbRename.Location = new System.Drawing.Point(15, 373);
            this.mbRename.Name = "mbRename";
            this.mbRename.Size = new System.Drawing.Size(60, 60);
            this.mbRename.StateChangeActivated = true;
            this.mbRename.TabIndex = 31;
            this.mbRename.TabStop = false;
            this.mbRename.UseVisualStyleBackColor = false;
            this.mbRename.Click += new System.EventHandler(this.MbRename_Click);
            // 
            // mbSaveWithName
            // 
            this.mbSaveWithName.Active = false;
            this.mbSaveWithName.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSaveWithName.ActiveBackgroundImage")));
            this.mbSaveWithName.BackColor = System.Drawing.Color.Transparent;
            this.mbSaveWithName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSaveWithName.BackgroundImage")));
            this.mbSaveWithName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSaveWithName.ButtonSize = 60;
            this.mbSaveWithName.FlatAppearance.BorderSize = 0;
            this.mbSaveWithName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSaveWithName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSaveWithName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSaveWithName.ForeColor = System.Drawing.Color.Transparent;
            this.mbSaveWithName.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSaveWithName.InactiveBackgroundImage")));
            this.mbSaveWithName.Location = new System.Drawing.Point(15, 302);
            this.mbSaveWithName.Name = "mbSaveWithName";
            this.mbSaveWithName.Size = new System.Drawing.Size(60, 60);
            this.mbSaveWithName.StateChangeActivated = true;
            this.mbSaveWithName.TabIndex = 30;
            this.mbSaveWithName.TabStop = false;
            this.mbSaveWithName.UseVisualStyleBackColor = false;
            this.mbSaveWithName.Click += new System.EventHandler(this.MbSaveWithName_Click);
            // 
            // mbApply
            // 
            this.mbApply.Active = false;
            this.mbApply.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbApply.ActiveBackgroundImage")));
            this.mbApply.BackColor = System.Drawing.Color.Transparent;
            this.mbApply.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbApply.BackgroundImage")));
            this.mbApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbApply.ButtonSize = 60;
            this.mbApply.FlatAppearance.BorderSize = 0;
            this.mbApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbApply.ForeColor = System.Drawing.Color.Transparent;
            this.mbApply.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbApply.InactiveBackgroundImage")));
            this.mbApply.Location = new System.Drawing.Point(15, 18);
            this.mbApply.Name = "mbApply";
            this.mbApply.Size = new System.Drawing.Size(60, 60);
            this.mbApply.StateChangeActivated = true;
            this.mbApply.TabIndex = 26;
            this.mbApply.TabStop = false;
            this.mbApply.UseVisualStyleBackColor = false;
            this.mbApply.Click += new System.EventHandler(this.MbApply_Click);
            // 
            // mbSave
            // 
            this.mbSave.Active = false;
            this.mbSave.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSave.ActiveBackgroundImage")));
            this.mbSave.BackColor = System.Drawing.Color.Transparent;
            this.mbSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSave.BackgroundImage")));
            this.mbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSave.ButtonSize = 60;
            this.mbSave.FlatAppearance.BorderSize = 0;
            this.mbSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSave.ForeColor = System.Drawing.Color.Transparent;
            this.mbSave.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSave.InactiveBackgroundImage")));
            this.mbSave.Location = new System.Drawing.Point(15, 231);
            this.mbSave.Name = "mbSave";
            this.mbSave.Size = new System.Drawing.Size(60, 60);
            this.mbSave.StateChangeActivated = true;
            this.mbSave.TabIndex = 29;
            this.mbSave.TabStop = false;
            this.mbSave.UseVisualStyleBackColor = false;
            this.mbSave.Click += new System.EventHandler(this.MbSave_Click);
            // 
            // mbNew
            // 
            this.mbNew.Active = false;
            this.mbNew.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbNew.ActiveBackgroundImage")));
            this.mbNew.BackColor = System.Drawing.Color.Transparent;
            this.mbNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbNew.BackgroundImage")));
            this.mbNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbNew.ButtonSize = 60;
            this.mbNew.FlatAppearance.BorderSize = 0;
            this.mbNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbNew.ForeColor = System.Drawing.Color.Transparent;
            this.mbNew.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbNew.InactiveBackgroundImage")));
            this.mbNew.Location = new System.Drawing.Point(15, 89);
            this.mbNew.Name = "mbNew";
            this.mbNew.Size = new System.Drawing.Size(60, 60);
            this.mbNew.StateChangeActivated = true;
            this.mbNew.TabIndex = 27;
            this.mbNew.TabStop = false;
            this.mbNew.UseVisualStyleBackColor = false;
            this.mbNew.Click += new System.EventHandler(this.MbNew_Click);
            // 
            // mbDelete
            // 
            this.mbDelete.Active = false;
            this.mbDelete.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDelete.ActiveBackgroundImage")));
            this.mbDelete.BackColor = System.Drawing.Color.Transparent;
            this.mbDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDelete.BackgroundImage")));
            this.mbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDelete.ButtonSize = 60;
            this.mbDelete.FlatAppearance.BorderSize = 0;
            this.mbDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDelete.ForeColor = System.Drawing.Color.Transparent;
            this.mbDelete.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDelete.InactiveBackgroundImage")));
            this.mbDelete.Location = new System.Drawing.Point(15, 160);
            this.mbDelete.Name = "mbDelete";
            this.mbDelete.Size = new System.Drawing.Size(60, 60);
            this.mbDelete.StateChangeActivated = true;
            this.mbDelete.TabIndex = 28;
            this.mbDelete.TabStop = false;
            this.mbDelete.UseVisualStyleBackColor = false;
            this.mbDelete.Click += new System.EventHandler(this.MbDelete_Click);
            // 
            // clPreFeed
            // 
            this.mlPreFeed.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlPreFeed.Location = new System.Drawing.Point(11, 191);
            this.mlPreFeed.Name = "clPreFeed";
            this.mlPreFeed.Size = new System.Drawing.Size(437, 20);
            this.mlPreFeed.TabIndex = 29;
            this.mlPreFeed.Text = "clPreFeed";
            this.mlPreFeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clCurrentPreFeed
            // 
            this.mlCurrentPreFeed.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCurrentPreFeed.ForeColor = System.Drawing.Color.DarkGray;
            this.mlCurrentPreFeed.Location = new System.Drawing.Point(9, 191);
            this.mlCurrentPreFeed.Name = "clCurrentPreFeed";
            this.mlCurrentPreFeed.Size = new System.Drawing.Size(467, 20);
            this.mlCurrentPreFeed.TabIndex = 33;
            this.mlCurrentPreFeed.Text = "clPreFeed";
            this.mlCurrentPreFeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormWorkingsSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1145, 720);
            this.Controls.Add(this.panelCurrentSettings);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelSelectedSettings);
            this.Controls.Add(this.mbUp);
            this.Controls.Add(this.mbDown);
            this.Controls.Add(this.panelFlowControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWorkingsSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLoadSettings";
            this.Load += new System.EventHandler(this.FormWorkingsSettingsManager_Load);
            this.panelCurrentSettings.ResumeLayout(false);
            this.panelSelectedSettings.ResumeLayout(false);
            this.panelFlowControl.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Machine.UI.Controls.MachineStringEditableItemsListbox clSettings;
        private Machine.UI.Controls.MachineButton mbUp;
        private Machine.UI.Controls.MachineButton mbDown;
        private Machine.UI.Controls.MachineLabel mlWorkingMode;
        private Machine.UI.Controls.MachineLabel mlCutterVelocity;
        private Machine.UI.Controls.MachineLabel mlStraightRollerUp;
        private Machine.UI.Controls.MachineLabel mlPhotocellAlignment;
        private Machine.UI.Controls.MachineLabel mlCradleVelocity;
        private Machine.UI.Controls.MachineLabel mlSelectedWorkingsSettingsVisualization;
        private Machine.UI.Controls.MachinePanelEdgeRounded panelSelectedSettings;
        private Machine.UI.Controls.MachineLabel mlDate;
        private Machine.UI.Controls.MachinePanelEdgeRounded panelCurrentSettings;
        private Machine.UI.Controls.MachineLabel mlCurrentParameters;
        private Machine.UI.Controls.MachineLabel mlCurrentStraightRollerUp;
        private Machine.UI.Controls.MachineLabel mlCurrentPhotocellMaterialPresence;
        private Machine.UI.Controls.MachineLabel mlCurrentCradleVelocity;
        private Machine.UI.Controls.MachineLabel mlCurrentCutterVelocity;
        private Machine.UI.Controls.MachineLabel mlCurrentWorkingMode;
        private Machine.UI.Controls.MachinePanelEdgeRounded panelFlowControl;
        private Machine.UI.Controls.MachinePanelEdgeRounded panelButtons;
        private Machine.UI.Controls.MachineButton mbApply;
        private Machine.UI.Controls.MachineButton mbNew;
        private Machine.UI.Controls.MachineButton mbDelete;
        private Machine.UI.Controls.MachineButton mbSave;
        private Machine.UI.Controls.MachineButton mbRename;
        private Machine.UI.Controls.MachineButton mbSaveWithName;
        private Machine.UI.Controls.MachineLabel mlRename;
        private Machine.UI.Controls.MachineLabel mlSaveWithName;
        private Machine.UI.Controls.MachineLabel mlSave;
        private Machine.UI.Controls.MachineLabel mlDelete;
        private Machine.UI.Controls.MachineLabel mlNew;
        private Machine.UI.Controls.MachineLabel mlApply;
        private Machine.UI.Controls.MachineLabel mlPhotocellMaterialPresence;
        private Machine.UI.Controls.MachineLabel mlCurrentPhotocellAlignment;
        private Machine.UI.Controls.MachineLabel mlCurrentPreFeed;
        private Machine.UI.Controls.MachineLabel mlPreFeed;
    }
}