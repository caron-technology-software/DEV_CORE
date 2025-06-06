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
            panelCurrentSettings = new Machine.UI.Controls.MachinePanelEdgeRounded();
            mlCurrentPreFeed = new Machine.UI.Controls.MachineLabel();
            mlCurrentPhotocellAlignment = new Machine.UI.Controls.MachineLabel();
            mlCurrentStraightRollerUp = new Machine.UI.Controls.MachineLabel();
            mlCurrentPhotocellMaterialPresence = new Machine.UI.Controls.MachineLabel();
            mlCurrentCradleVelocity = new Machine.UI.Controls.MachineLabel();
            mlCurrentCutterVelocity = new Machine.UI.Controls.MachineLabel();
            mlCurrentWorkingMode = new Machine.UI.Controls.MachineLabel();
            mlCurrentParameters = new Machine.UI.Controls.MachineLabel();
            panelSelectedSettings = new Machine.UI.Controls.MachinePanelEdgeRounded();
            mlPreFeed = new Machine.UI.Controls.MachineLabel();
            mlPhotocellMaterialPresence = new Machine.UI.Controls.MachineLabel();
            mlDate = new Machine.UI.Controls.MachineLabel();
            mlSelectedWorkingsSettingsVisualization = new Machine.UI.Controls.MachineLabel();
            mlWorkingMode = new Machine.UI.Controls.MachineLabel();
            mlCradleVelocity = new Machine.UI.Controls.MachineLabel();
            mlCutterVelocity = new Machine.UI.Controls.MachineLabel();
            mlPhotocellAlignment = new Machine.UI.Controls.MachineLabel();
            mlStraightRollerUp = new Machine.UI.Controls.MachineLabel();
            panelFlowControl = new Machine.UI.Controls.MachinePanelEdgeRounded();
            clSettings = new Machine.UI.Controls.MachineStringEditableItemsListbox();
            mbUp = new Machine.UI.Controls.MachineButton();
            mbDown = new Machine.UI.Controls.MachineButton();
            panelButtons = new Machine.UI.Controls.MachinePanelEdgeRounded();
            mlRename = new Machine.UI.Controls.MachineLabel();
            mlSaveWithName = new Machine.UI.Controls.MachineLabel();
            mlSave = new Machine.UI.Controls.MachineLabel();
            mlDelete = new Machine.UI.Controls.MachineLabel();
            mlNew = new Machine.UI.Controls.MachineLabel();
            mlApply = new Machine.UI.Controls.MachineLabel();
            mbRename = new Machine.UI.Controls.MachineButton();
            mbSaveWithName = new Machine.UI.Controls.MachineButton();
            mbApply = new Machine.UI.Controls.MachineButton();
            mbSave = new Machine.UI.Controls.MachineButton();
            mbNew = new Machine.UI.Controls.MachineButton();
            mbDelete = new Machine.UI.Controls.MachineButton();
            panelCurrentSettings.SuspendLayout();
            panelSelectedSettings.SuspendLayout();
            panelFlowControl.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelCurrentSettings
            // 
            panelCurrentSettings.Controls.Add(mlCurrentPreFeed);
            panelCurrentSettings.Controls.Add(mlCurrentPhotocellAlignment);
            panelCurrentSettings.Controls.Add(mlCurrentStraightRollerUp);
            panelCurrentSettings.Controls.Add(mlCurrentPhotocellMaterialPresence);
            panelCurrentSettings.Controls.Add(mlCurrentCradleVelocity);
            panelCurrentSettings.Controls.Add(mlCurrentCutterVelocity);
            panelCurrentSettings.Controls.Add(mlCurrentWorkingMode);
            panelCurrentSettings.Controls.Add(mlCurrentParameters);
            panelCurrentSettings.LineColor = System.Drawing.Color.LightGray;
            panelCurrentSettings.LineWidth = 4;
            panelCurrentSettings.Location = new System.Drawing.Point(583, 12);
            panelCurrentSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelCurrentSettings.Name = "panelCurrentSettings";
            panelCurrentSettings.Radius = 5;
            panelCurrentSettings.Size = new System.Drawing.Size(550, 265);
            panelCurrentSettings.TabIndex = 28;
            // 
            // mlCurrentPreFeed
            // 
            mlCurrentPreFeed.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentPreFeed.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentPreFeed.Location = new System.Drawing.Point(10, 220);
            mlCurrentPreFeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentPreFeed.Name = "mlCurrentPreFeed";
            mlCurrentPreFeed.Size = new System.Drawing.Size(527, 23);
            mlCurrentPreFeed.TabIndex = 33;
            mlCurrentPreFeed.Text = "clPreFeed";
            mlCurrentPreFeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentPhotocellAlignment
            // 
            mlCurrentPhotocellAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentPhotocellAlignment.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentPhotocellAlignment.Location = new System.Drawing.Point(10, 196);
            mlCurrentPhotocellAlignment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentPhotocellAlignment.Name = "mlCurrentPhotocellAlignment";
            mlCurrentPhotocellAlignment.Size = new System.Drawing.Size(527, 23);
            mlCurrentPhotocellAlignment.TabIndex = 32;
            mlCurrentPhotocellAlignment.Text = "clPhotocellAlignment";
            mlCurrentPhotocellAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentStraightRollerUp
            // 
            mlCurrentStraightRollerUp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentStraightRollerUp.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentStraightRollerUp.Location = new System.Drawing.Point(10, 148);
            mlCurrentStraightRollerUp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentStraightRollerUp.Name = "mlCurrentStraightRollerUp";
            mlCurrentStraightRollerUp.Size = new System.Drawing.Size(527, 23);
            mlCurrentStraightRollerUp.TabIndex = 31;
            mlCurrentStraightRollerUp.Text = "clCurrentStraightRollerUp";
            mlCurrentStraightRollerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentPhotocellMaterialPresence
            // 
            mlCurrentPhotocellMaterialPresence.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentPhotocellMaterialPresence.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentPhotocellMaterialPresence.Location = new System.Drawing.Point(10, 172);
            mlCurrentPhotocellMaterialPresence.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentPhotocellMaterialPresence.Name = "mlCurrentPhotocellMaterialPresence";
            mlCurrentPhotocellMaterialPresence.Size = new System.Drawing.Size(527, 23);
            mlCurrentPhotocellMaterialPresence.TabIndex = 30;
            mlCurrentPhotocellMaterialPresence.Text = "clPhotocellMaterialPresence";
            mlCurrentPhotocellMaterialPresence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentCradleVelocity
            // 
            mlCurrentCradleVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentCradleVelocity.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentCradleVelocity.Location = new System.Drawing.Point(10, 123);
            mlCurrentCradleVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentCradleVelocity.Name = "mlCurrentCradleVelocity";
            mlCurrentCradleVelocity.Size = new System.Drawing.Size(527, 23);
            mlCurrentCradleVelocity.TabIndex = 29;
            mlCurrentCradleVelocity.Text = "clCurrentCradleVelocity";
            mlCurrentCradleVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentCutterVelocity
            // 
            mlCurrentCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentCutterVelocity.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentCutterVelocity.Location = new System.Drawing.Point(10, 99);
            mlCurrentCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentCutterVelocity.Name = "mlCurrentCutterVelocity";
            mlCurrentCutterVelocity.Size = new System.Drawing.Size(527, 23);
            mlCurrentCutterVelocity.TabIndex = 28;
            mlCurrentCutterVelocity.Text = "clCurrentCutterVelocity";
            mlCurrentCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentWorkingMode
            // 
            mlCurrentWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentWorkingMode.ForeColor = System.Drawing.Color.DarkGray;
            mlCurrentWorkingMode.Location = new System.Drawing.Point(10, 75);
            mlCurrentWorkingMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentWorkingMode.Name = "mlCurrentWorkingMode";
            mlCurrentWorkingMode.Size = new System.Drawing.Size(527, 23);
            mlCurrentWorkingMode.TabIndex = 27;
            mlCurrentWorkingMode.Text = "clCurrentWorkingMode";
            mlCurrentWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCurrentParameters
            // 
            mlCurrentParameters.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCurrentParameters.ForeColor = System.Drawing.Color.DarkSlateBlue;
            mlCurrentParameters.Location = new System.Drawing.Point(19, 8);
            mlCurrentParameters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCurrentParameters.Name = "mlCurrentParameters";
            mlCurrentParameters.Size = new System.Drawing.Size(518, 31);
            mlCurrentParameters.TabIndex = 26;
            mlCurrentParameters.Text = "clCurrentParameters";
            mlCurrentParameters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSelectedSettings
            // 
            panelSelectedSettings.Controls.Add(mlPreFeed);
            panelSelectedSettings.Controls.Add(mlPhotocellMaterialPresence);
            panelSelectedSettings.Controls.Add(mlDate);
            panelSelectedSettings.Controls.Add(mlSelectedWorkingsSettingsVisualization);
            panelSelectedSettings.Controls.Add(mlWorkingMode);
            panelSelectedSettings.Controls.Add(mlCradleVelocity);
            panelSelectedSettings.Controls.Add(mlCutterVelocity);
            panelSelectedSettings.Controls.Add(mlPhotocellAlignment);
            panelSelectedSettings.Controls.Add(mlStraightRollerUp);
            panelSelectedSettings.LineColor = System.Drawing.Color.LightGray;
            panelSelectedSettings.LineWidth = 4;
            panelSelectedSettings.Location = new System.Drawing.Point(13, 12);
            panelSelectedSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelSelectedSettings.Name = "panelSelectedSettings";
            panelSelectedSettings.Radius = 5;
            panelSelectedSettings.Size = new System.Drawing.Size(562, 265);
            panelSelectedSettings.TabIndex = 27;
            // 
            // mlPreFeed
            // 
            mlPreFeed.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlPreFeed.Location = new System.Drawing.Point(13, 220);
            mlPreFeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlPreFeed.Name = "mlPreFeed";
            mlPreFeed.Size = new System.Drawing.Size(510, 23);
            mlPreFeed.TabIndex = 29;
            mlPreFeed.Text = "clPreFeed";
            mlPreFeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlPhotocellMaterialPresence
            // 
            mlPhotocellMaterialPresence.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlPhotocellMaterialPresence.Location = new System.Drawing.Point(13, 172);
            mlPhotocellMaterialPresence.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlPhotocellMaterialPresence.Name = "mlPhotocellMaterialPresence";
            mlPhotocellMaterialPresence.Size = new System.Drawing.Size(510, 23);
            mlPhotocellMaterialPresence.TabIndex = 28;
            mlPhotocellMaterialPresence.Text = "clPhotocellMaterialPresence";
            mlPhotocellMaterialPresence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlDate
            // 
            mlDate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            mlDate.Location = new System.Drawing.Point(13, 51);
            mlDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlDate.Name = "mlDate";
            mlDate.Size = new System.Drawing.Size(510, 23);
            mlDate.TabIndex = 27;
            mlDate.Text = "clDate";
            mlDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlSelectedWorkingsSettingsVisualization
            // 
            mlSelectedWorkingsSettingsVisualization.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSelectedWorkingsSettingsVisualization.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            mlSelectedWorkingsSettingsVisualization.Location = new System.Drawing.Point(19, 7);
            mlSelectedWorkingsSettingsVisualization.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlSelectedWorkingsSettingsVisualization.Name = "mlSelectedWorkingsSettingsVisualization";
            mlSelectedWorkingsSettingsVisualization.Size = new System.Drawing.Size(504, 33);
            mlSelectedWorkingsSettingsVisualization.TabIndex = 26;
            mlSelectedWorkingsSettingsVisualization.Text = "clSelectedSettingsVisualization";
            mlSelectedWorkingsSettingsVisualization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlWorkingMode
            // 
            mlWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlWorkingMode.Location = new System.Drawing.Point(13, 75);
            mlWorkingMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlWorkingMode.Name = "mlWorkingMode";
            mlWorkingMode.Size = new System.Drawing.Size(510, 23);
            mlWorkingMode.TabIndex = 21;
            mlWorkingMode.Text = "clWorkingMode";
            mlWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCradleVelocity
            // 
            mlCradleVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCradleVelocity.Location = new System.Drawing.Point(13, 123);
            mlCradleVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCradleVelocity.Name = "mlCradleVelocity";
            mlCradleVelocity.Size = new System.Drawing.Size(510, 23);
            mlCradleVelocity.TabIndex = 25;
            mlCradleVelocity.Text = "clCradleVelocity";
            mlCradleVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlCutterVelocity
            // 
            mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutterVelocity.Location = new System.Drawing.Point(13, 99);
            mlCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCutterVelocity.Name = "mlCutterVelocity";
            mlCutterVelocity.Size = new System.Drawing.Size(510, 23);
            mlCutterVelocity.TabIndex = 22;
            mlCutterVelocity.Text = "clCutterVelocity";
            mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlPhotocellAlignment
            // 
            mlPhotocellAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlPhotocellAlignment.Location = new System.Drawing.Point(13, 196);
            mlPhotocellAlignment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlPhotocellAlignment.Name = "mlPhotocellAlignment";
            mlPhotocellAlignment.Size = new System.Drawing.Size(510, 23);
            mlPhotocellAlignment.TabIndex = 24;
            mlPhotocellAlignment.Text = "clPhotocellAlignment";
            mlPhotocellAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlStraightRollerUp
            // 
            mlStraightRollerUp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlStraightRollerUp.Location = new System.Drawing.Point(13, 148);
            mlStraightRollerUp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlStraightRollerUp.Name = "mlStraightRollerUp";
            mlStraightRollerUp.Size = new System.Drawing.Size(510, 23);
            mlStraightRollerUp.TabIndex = 23;
            mlStraightRollerUp.Text = "clStraightRollerUp";
            mlStraightRollerUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFlowControl
            // 
            panelFlowControl.Controls.Add(clSettings);
            panelFlowControl.LineColor = System.Drawing.Color.LightGray;
            panelFlowControl.LineWidth = 4;
            panelFlowControl.Location = new System.Drawing.Point(13, 283);
            panelFlowControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelFlowControl.Name = "panelFlowControl";
            panelFlowControl.Radius = 5;
            panelFlowControl.Size = new System.Drawing.Size(562, 426);
            panelFlowControl.TabIndex = 28;
            // 
            // clSettings
            // 
            clSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            clSettings.Location = new System.Drawing.Point(5, 8);
            clSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clSettings.Name = "clSettings";
            clSettings.Size = new System.Drawing.Size(552, 413);
            clSettings.TabIndex = 10;
            // 
            // mbUp
            // 
            mbUp.Active = false;
            mbUp.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbUp.ActiveBackgroundImage");
            mbUp.BackColor = System.Drawing.Color.Transparent;
            mbUp.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbUp.BackgroundImage");
            mbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbUp.ButtonSize = 85;
            mbUp.FlatAppearance.BorderSize = 0;
            mbUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbUp.ForeColor = System.Drawing.Color.Transparent;
            mbUp.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbUp.InactiveBackgroundImage");
            mbUp.Location = new System.Drawing.Point(583, 285);
            mbUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbUp.Name = "mbUp";
            mbUp.Size = new System.Drawing.Size(85, 85);
            mbUp.StateChangeActivated = true;
            mbUp.TabIndex = 16;
            mbUp.TabStop = false;
            mbUp.UseVisualStyleBackColor = false;
            mbUp.Click += MbUp_Click;
            // 
            // mbDown
            // 
            mbDown.Active = false;
            mbDown.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDown.ActiveBackgroundImage");
            mbDown.BackColor = System.Drawing.Color.Transparent;
            mbDown.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbDown.BackgroundImage");
            mbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbDown.ButtonSize = 85;
            mbDown.FlatAppearance.BorderSize = 0;
            mbDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbDown.ForeColor = System.Drawing.Color.Transparent;
            mbDown.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDown.InactiveBackgroundImage");
            mbDown.Location = new System.Drawing.Point(583, 619);
            mbDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbDown.Name = "mbDown";
            mbDown.Size = new System.Drawing.Size(85, 85);
            mbDown.StateChangeActivated = true;
            mbDown.TabIndex = 15;
            mbDown.TabStop = false;
            mbDown.UseVisualStyleBackColor = false;
            mbDown.Click += MbDown_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(mlRename);
            panelButtons.Controls.Add(mlSaveWithName);
            panelButtons.Controls.Add(mlSave);
            panelButtons.Controls.Add(mlDelete);
            panelButtons.Controls.Add(mlNew);
            panelButtons.Controls.Add(mlApply);
            panelButtons.Controls.Add(mbRename);
            panelButtons.Controls.Add(mbSaveWithName);
            panelButtons.Controls.Add(mbApply);
            panelButtons.Controls.Add(mbSave);
            panelButtons.Controls.Add(mbNew);
            panelButtons.Controls.Add(mbDelete);
            panelButtons.LineColor = System.Drawing.Color.LightGray;
            panelButtons.LineWidth = 4;
            panelButtons.Location = new System.Drawing.Point(789, 283);
            panelButtons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Radius = 10;
            panelButtons.Size = new System.Drawing.Size(344, 426);
            panelButtons.TabIndex = 29;
            // 
            // mlRename
            // 
            mlRename.AutoSize = true;
            mlRename.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlRename.Location = new System.Drawing.Point(97, 353);
            mlRename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlRename.Name = "mlRename";
            mlRename.Size = new System.Drawing.Size(101, 22);
            mlRename.TabIndex = 37;
            mlRename.Text = "clRename";
            // 
            // mlSaveWithName
            // 
            mlSaveWithName.AutoSize = true;
            mlSaveWithName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSaveWithName.Location = new System.Drawing.Point(97, 289);
            mlSaveWithName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlSaveWithName.Name = "mlSaveWithName";
            mlSaveWithName.Size = new System.Drawing.Size(165, 22);
            mlSaveWithName.TabIndex = 36;
            mlSaveWithName.Text = "clSaveWithName";
            // 
            // mlSave
            // 
            mlSave.AutoSize = true;
            mlSave.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSave.Location = new System.Drawing.Point(97, 223);
            mlSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlSave.Name = "mlSave";
            mlSave.Size = new System.Drawing.Size(71, 22);
            mlSave.TabIndex = 35;
            mlSave.Text = "clSave";
            // 
            // mlDelete
            // 
            mlDelete.AutoSize = true;
            mlDelete.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlDelete.Location = new System.Drawing.Point(97, 158);
            mlDelete.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlDelete.Name = "mlDelete";
            mlDelete.Size = new System.Drawing.Size(85, 22);
            mlDelete.TabIndex = 34;
            mlDelete.Text = "clDelete";
            // 
            // mlNew
            // 
            mlNew.AutoSize = true;
            mlNew.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlNew.Location = new System.Drawing.Point(97, 93);
            mlNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlNew.Name = "mlNew";
            mlNew.Size = new System.Drawing.Size(66, 22);
            mlNew.TabIndex = 33;
            mlNew.Text = "clNew";
            // 
            // mlApply
            // 
            mlApply.AutoSize = true;
            mlApply.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlApply.ForeColor = System.Drawing.SystemColors.ControlText;
            mlApply.Location = new System.Drawing.Point(97, 29);
            mlApply.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlApply.Name = "mlApply";
            mlApply.Size = new System.Drawing.Size(79, 22);
            mlApply.TabIndex = 32;
            mlApply.Text = "clApply";
            // 
            // mbRename
            // 
            mbRename.Active = false;
            mbRename.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbRename.ActiveBackgroundImage");
            mbRename.BackColor = System.Drawing.Color.Transparent;
            mbRename.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbRename.BackgroundImage");
            mbRename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbRename.ButtonSize = 60;
            mbRename.FlatAppearance.BorderSize = 0;
            mbRename.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbRename.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbRename.ForeColor = System.Drawing.Color.Transparent;
            mbRename.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbRename.InactiveBackgroundImage");
            mbRename.Location = new System.Drawing.Point(18, 338);
            mbRename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbRename.Name = "mbRename";
            mbRename.Size = new System.Drawing.Size(60, 60);
            mbRename.StateChangeActivated = true;
            mbRename.TabIndex = 31;
            mbRename.TabStop = false;
            mbRename.UseVisualStyleBackColor = false;
            mbRename.Click += MbRename_Click;
            // 
            // mbSaveWithName
            // 
            mbSaveWithName.Active = false;
            mbSaveWithName.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSaveWithName.ActiveBackgroundImage");
            mbSaveWithName.BackColor = System.Drawing.Color.Transparent;
            mbSaveWithName.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbSaveWithName.BackgroundImage");
            mbSaveWithName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbSaveWithName.ButtonSize = 60;
            mbSaveWithName.FlatAppearance.BorderSize = 0;
            mbSaveWithName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbSaveWithName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbSaveWithName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbSaveWithName.ForeColor = System.Drawing.Color.Transparent;
            mbSaveWithName.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSaveWithName.InactiveBackgroundImage");
            mbSaveWithName.Location = new System.Drawing.Point(18, 272);
            mbSaveWithName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSaveWithName.Name = "mbSaveWithName";
            mbSaveWithName.Size = new System.Drawing.Size(60, 60);
            mbSaveWithName.StateChangeActivated = true;
            mbSaveWithName.TabIndex = 30;
            mbSaveWithName.TabStop = false;
            mbSaveWithName.UseVisualStyleBackColor = false;
            mbSaveWithName.Click += MbSaveWithName_Click;
            // 
            // mbApply
            // 
            mbApply.Active = false;
            mbApply.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbApply.ActiveBackgroundImage");
            mbApply.BackColor = System.Drawing.Color.Transparent;
            mbApply.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbApply.BackgroundImage");
            mbApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbApply.ButtonSize = 60;
            mbApply.FlatAppearance.BorderSize = 0;
            mbApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbApply.ForeColor = System.Drawing.Color.Transparent;
            mbApply.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbApply.InactiveBackgroundImage");
            mbApply.Location = new System.Drawing.Point(18, 8);
            mbApply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbApply.Name = "mbApply";
            mbApply.Size = new System.Drawing.Size(60, 60);
            mbApply.StateChangeActivated = true;
            mbApply.TabIndex = 26;
            mbApply.TabStop = false;
            mbApply.UseVisualStyleBackColor = false;
            mbApply.Click += MbApply_Click;
            // 
            // mbSave
            // 
            mbSave.Active = false;
            mbSave.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSave.ActiveBackgroundImage");
            mbSave.BackColor = System.Drawing.Color.Transparent;
            mbSave.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbSave.BackgroundImage");
            mbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbSave.ButtonSize = 60;
            mbSave.FlatAppearance.BorderSize = 0;
            mbSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbSave.ForeColor = System.Drawing.Color.Transparent;
            mbSave.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSave.InactiveBackgroundImage");
            mbSave.Location = new System.Drawing.Point(18, 206);
            mbSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSave.Name = "mbSave";
            mbSave.Size = new System.Drawing.Size(60, 60);
            mbSave.StateChangeActivated = true;
            mbSave.TabIndex = 29;
            mbSave.TabStop = false;
            mbSave.UseVisualStyleBackColor = false;
            mbSave.Click += MbSave_Click;
            // 
            // mbNew
            // 
            mbNew.Active = false;
            mbNew.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbNew.ActiveBackgroundImage");
            mbNew.BackColor = System.Drawing.Color.Transparent;
            mbNew.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbNew.BackgroundImage");
            mbNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbNew.ButtonSize = 60;
            mbNew.FlatAppearance.BorderSize = 0;
            mbNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbNew.ForeColor = System.Drawing.Color.Transparent;
            mbNew.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbNew.InactiveBackgroundImage");
            mbNew.Location = new System.Drawing.Point(18, 74);
            mbNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbNew.Name = "mbNew";
            mbNew.Size = new System.Drawing.Size(60, 60);
            mbNew.StateChangeActivated = true;
            mbNew.TabIndex = 27;
            mbNew.TabStop = false;
            mbNew.UseVisualStyleBackColor = false;
            mbNew.Click += MbNew_Click;
            // 
            // mbDelete
            // 
            mbDelete.Active = false;
            mbDelete.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDelete.ActiveBackgroundImage");
            mbDelete.BackColor = System.Drawing.Color.Transparent;
            mbDelete.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbDelete.BackgroundImage");
            mbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbDelete.ButtonSize = 60;
            mbDelete.FlatAppearance.BorderSize = 0;
            mbDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbDelete.ForeColor = System.Drawing.Color.Transparent;
            mbDelete.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDelete.InactiveBackgroundImage");
            mbDelete.Location = new System.Drawing.Point(18, 140);
            mbDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbDelete.Name = "mbDelete";
            mbDelete.Size = new System.Drawing.Size(60, 60);
            mbDelete.StateChangeActivated = true;
            mbDelete.TabIndex = 28;
            mbDelete.TabStop = false;
            mbDelete.UseVisualStyleBackColor = false;
            mbDelete.Click += MbDelete_Click;
            // 
            // FormWorkingsSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(panelCurrentSettings);
            Controls.Add(panelButtons);
            Controls.Add(panelSelectedSettings);
            Controls.Add(mbUp);
            Controls.Add(mbDown);
            Controls.Add(panelFlowControl);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormWorkingsSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormLoadSettings";
            Load += FormWorkingsSettingsManager_Load;
            panelCurrentSettings.ResumeLayout(false);
            panelSelectedSettings.ResumeLayout(false);
            panelFlowControl.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            ResumeLayout(false);
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