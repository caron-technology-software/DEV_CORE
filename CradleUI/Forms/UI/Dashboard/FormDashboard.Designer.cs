using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDashboard));
            this.mlSync = new Machine.UI.Controls.MachineLabel();
            this.cbCradleSync = new Machine.UI.Controls.MachineButton();
            this.mlCutter = new Machine.UI.Controls.MachineLabel();
            this.mlAlignment = new Machine.UI.Controls.MachineLabel();
            this.panelAlignment = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbAlignmentMotorSide = new Machine.UI.Controls.MachineButton();
            this.mbAlignmentOperatorSide = new Machine.UI.Controls.MachineButton();
            this.mlCradleJog = new Machine.UI.Controls.MachineLabel();
            this.panelJog = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.cbCradleJogACW = new Machine.UI.Controls.MachineButton();
            this.cbCradleJogCW = new Machine.UI.Controls.MachineButton();
            this.panelCuttOff = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbSharpening = new Machine.UI.Controls.MachineButton();
            this.cbCutOff = new Machine.UI.Controls.MachineButton();
            this.panelWorkingMode = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlWorkingModeTitle = new System.Windows.Forms.Label();
            this.mlWorkingMode = new System.Windows.Forms.Label();
            this.mlMaterialRegulation = new Machine.UI.Controls.MachineLabel();
            this.mSearchBox = new Machine.UI.Controls.MachineSettingsSearchBox();
            this.cmbStraightRoller = new Machine.UI.Controls.MachineMultiTwoButtons();
            this.cpbsCradleScalingFactor = new Machine.UI.Controls.MachinePanelButtonSlider();
            this.mlStraightRoller = new Machine.UI.Controls.MachineLabel();
            this.mpStatistics = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlWorkingsStatistics = new Machine.UI.Controls.MachineLabel();
            this.mlCutterVelocity = new Machine.UI.Controls.MachineLabel();
            this.mpbsCutterVelocity = new Machine.UI.Controls.MachinePanelButtonSlider();
            this.cbStop = new Machine.UI.Controls.MachineButton();
            this.panelAlignment.SuspendLayout();
            this.panelJog.SuspendLayout();
            this.panelCuttOff.SuspendLayout();
            this.panelWorkingMode.SuspendLayout();
            this.mpStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // mlSync
            // 
            this.mlSync.BackColor = System.Drawing.Color.Transparent;
            this.mlSync.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSync.Location = new System.Drawing.Point(55, 98);
            this.mlSync.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.mlSync.Name = "mlSync";
            this.mlSync.Size = new System.Drawing.Size(255, 59);
            this.mlSync.TabIndex = 46;
            this.mlSync.Text = "mlSync";
            this.mlSync.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cbCradleSync
            // 
            this.cbCradleSync.Active = false;
            this.cbCradleSync.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleSync.ActiveBackgroundImage")));
            this.cbCradleSync.BackColor = System.Drawing.Color.Transparent;
            this.cbCradleSync.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbCradleSync.BackgroundImage")));
            this.cbCradleSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbCradleSync.ButtonSize = 125;
            this.cbCradleSync.FlatAppearance.BorderSize = 0;
            this.cbCradleSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbCradleSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbCradleSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCradleSync.ForeColor = System.Drawing.Color.Transparent;
            this.cbCradleSync.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleSync.InactiveBackgroundImage")));
            this.cbCradleSync.Location = new System.Drawing.Point(120, 168);
            this.cbCradleSync.Name = "cbCradleSync";
            this.cbCradleSync.Size = new System.Drawing.Size(125, 125);
            this.cbCradleSync.StateChangeActivated = true;
            this.cbCradleSync.TabIndex = 36;
            this.cbCradleSync.TabStop = false;
            this.cbCradleSync.UseVisualStyleBackColor = false;
            this.cbCradleSync.Click += new System.EventHandler(this.CbCradleSync_Click);
            // 
            // mlCutter
            // 
            this.mlCutter.BackColor = System.Drawing.Color.Transparent;
            this.mlCutter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutter.Location = new System.Drawing.Point(856, 486);
            this.mlCutter.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlCutter.Name = "mlCutter";
            this.mlCutter.Size = new System.Drawing.Size(251, 59);
            this.mlCutter.TabIndex = 50;
            this.mlCutter.Text = "mlCutter";
            this.mlCutter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mlAlignment
            // 
            this.mlAlignment.BackColor = System.Drawing.Color.Transparent;
            this.mlAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlAlignment.Location = new System.Drawing.Point(253, 355);
            this.mlAlignment.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlAlignment.Name = "mlAlignment";
            this.mlAlignment.Size = new System.Drawing.Size(175, 68);
            this.mlAlignment.TabIndex = 49;
            this.mlAlignment.Text = "mlAlignment";
            this.mlAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelAlignment
            // 
            this.panelAlignment.Controls.Add(this.mbAlignmentMotorSide);
            this.panelAlignment.Controls.Add(this.mbAlignmentOperatorSide);
            this.panelAlignment.LineColor = System.Drawing.Color.LightGray;
            this.panelAlignment.LineWidth = 5;
            this.panelAlignment.Location = new System.Drawing.Point(270, 427);
            this.panelAlignment.Name = "panelAlignment";
            this.panelAlignment.Radius = 10;
            this.panelAlignment.Size = new System.Drawing.Size(135, 280);
            this.panelAlignment.TabIndex = 45;
            // 
            // mbAlignmentMotorSide
            // 
            this.mbAlignmentMotorSide.Active = false;
            this.mbAlignmentMotorSide.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAlignmentMotorSide.ActiveBackgroundImage")));
            this.mbAlignmentMotorSide.BackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentMotorSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbAlignmentMotorSide.BackgroundImage")));
            this.mbAlignmentMotorSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbAlignmentMotorSide.ButtonSize = 102;
            this.mbAlignmentMotorSide.FlatAppearance.BorderSize = 0;
            this.mbAlignmentMotorSide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentMotorSide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentMotorSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbAlignmentMotorSide.ForeColor = System.Drawing.Color.Transparent;
            this.mbAlignmentMotorSide.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAlignmentMotorSide.InactiveBackgroundImage")));
            this.mbAlignmentMotorSide.Location = new System.Drawing.Point(18, 18);
            this.mbAlignmentMotorSide.Name = "mbAlignmentMotorSide";
            this.mbAlignmentMotorSide.Size = new System.Drawing.Size(102, 102);
            this.mbAlignmentMotorSide.StateChangeActivated = true;
            this.mbAlignmentMotorSide.TabIndex = 40;
            this.mbAlignmentMotorSide.TabStop = false;
            this.mbAlignmentMotorSide.UseVisualStyleBackColor = false;
            this.mbAlignmentMotorSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentMotorSide_MouseDown);
            this.mbAlignmentMotorSide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentMotorSide_MouseUp);
            // 
            // mbAlignmentOperatorSide
            // 
            this.mbAlignmentOperatorSide.Active = false;
            this.mbAlignmentOperatorSide.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAlignmentOperatorSide.ActiveBackgroundImage")));
            this.mbAlignmentOperatorSide.BackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentOperatorSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbAlignmentOperatorSide.BackgroundImage")));
            this.mbAlignmentOperatorSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbAlignmentOperatorSide.ButtonSize = 102;
            this.mbAlignmentOperatorSide.FlatAppearance.BorderSize = 0;
            this.mbAlignmentOperatorSide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentOperatorSide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbAlignmentOperatorSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbAlignmentOperatorSide.ForeColor = System.Drawing.Color.Transparent;
            this.mbAlignmentOperatorSide.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAlignmentOperatorSide.InactiveBackgroundImage")));
            this.mbAlignmentOperatorSide.Location = new System.Drawing.Point(17, 156);
            this.mbAlignmentOperatorSide.Name = "mbAlignmentOperatorSide";
            this.mbAlignmentOperatorSide.Size = new System.Drawing.Size(102, 102);
            this.mbAlignmentOperatorSide.StateChangeActivated = true;
            this.mbAlignmentOperatorSide.TabIndex = 38;
            this.mbAlignmentOperatorSide.TabStop = false;
            this.mbAlignmentOperatorSide.UseVisualStyleBackColor = false;
            this.mbAlignmentOperatorSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentOperatorSide_MouseDown);
            this.mbAlignmentOperatorSide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentOperatorSide_MouseUp);
            // 
            // mlCradleJog
            // 
            this.mlCradleJog.BackColor = System.Drawing.Color.Transparent;
            this.mlCradleJog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCradleJog.Location = new System.Drawing.Point(41, 353);
            this.mlCradleJog.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlCradleJog.Name = "mlCradleJog";
            this.mlCradleJog.Size = new System.Drawing.Size(175, 69);
            this.mlCradleJog.TabIndex = 45;
            this.mlCradleJog.Text = "mlCradleJog";
            this.mlCradleJog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelJog
            // 
            this.panelJog.Controls.Add(this.cbCradleJogACW);
            this.panelJog.Controls.Add(this.cbCradleJogCW);
            this.panelJog.LineColor = System.Drawing.Color.LightGray;
            this.panelJog.LineWidth = 5;
            this.panelJog.Location = new System.Drawing.Point(55, 427);
            this.panelJog.Name = "panelJog";
            this.panelJog.Radius = 10;
            this.panelJog.Size = new System.Drawing.Size(135, 280);
            this.panelJog.TabIndex = 44;
            // 
            // cbCradleJogACW
            // 
            this.cbCradleJogACW.Active = false;
            this.cbCradleJogACW.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleJogACW.ActiveBackgroundImage")));
            this.cbCradleJogACW.BackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogACW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbCradleJogACW.BackgroundImage")));
            this.cbCradleJogACW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbCradleJogACW.ButtonSize = 102;
            this.cbCradleJogACW.FlatAppearance.BorderSize = 0;
            this.cbCradleJogACW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogACW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogACW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCradleJogACW.ForeColor = System.Drawing.Color.Transparent;
            this.cbCradleJogACW.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleJogACW.InactiveBackgroundImage")));
            this.cbCradleJogACW.Location = new System.Drawing.Point(16, 156);
            this.cbCradleJogACW.Name = "cbCradleJogACW";
            this.cbCradleJogACW.Size = new System.Drawing.Size(102, 102);
            this.cbCradleJogACW.StateChangeActivated = true;
            this.cbCradleJogACW.TabIndex = 40;
            this.cbCradleJogACW.TabStop = false;
            this.cbCradleJogACW.UseVisualStyleBackColor = false;
            this.cbCradleJogACW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogACW_MouseDown);
            this.cbCradleJogACW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogACW_MouseUp);
            // 
            // cbCradleJogCW
            // 
            this.cbCradleJogCW.Active = false;
            this.cbCradleJogCW.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleJogCW.ActiveBackgroundImage")));
            this.cbCradleJogCW.BackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogCW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbCradleJogCW.BackgroundImage")));
            this.cbCradleJogCW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbCradleJogCW.ButtonSize = 102;
            this.cbCradleJogCW.FlatAppearance.BorderSize = 0;
            this.cbCradleJogCW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogCW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbCradleJogCW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCradleJogCW.ForeColor = System.Drawing.Color.Transparent;
            this.cbCradleJogCW.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCradleJogCW.InactiveBackgroundImage")));
            this.cbCradleJogCW.Location = new System.Drawing.Point(17, 18);
            this.cbCradleJogCW.Name = "cbCradleJogCW";
            this.cbCradleJogCW.Size = new System.Drawing.Size(102, 102);
            this.cbCradleJogCW.StateChangeActivated = true;
            this.cbCradleJogCW.TabIndex = 38;
            this.cbCradleJogCW.TabStop = false;
            this.cbCradleJogCW.UseVisualStyleBackColor = false;
            this.cbCradleJogCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogCW_MouseDown);
            this.cbCradleJogCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogCW_MouseUp);
            // 
            // panelCuttOff
            // 
            this.panelCuttOff.Controls.Add(this.mbSharpening);
            this.panelCuttOff.Controls.Add(this.cbCutOff);
            this.panelCuttOff.LineColor = System.Drawing.Color.LightGray;
            this.panelCuttOff.LineWidth = 5;
            this.panelCuttOff.Location = new System.Drawing.Point(853, 556);
            this.panelCuttOff.Name = "panelCuttOff";
            this.panelCuttOff.Radius = 10;
            this.panelCuttOff.Size = new System.Drawing.Size(254, 125);
            this.panelCuttOff.TabIndex = 43;
            // 
            // mbSharpening
            // 
            this.mbSharpening.Active = false;
            this.mbSharpening.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSharpening.ActiveBackgroundImage")));
            this.mbSharpening.BackColor = System.Drawing.Color.Transparent;
            this.mbSharpening.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSharpening.BackgroundImage")));
            this.mbSharpening.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSharpening.ButtonSize = 102;
            this.mbSharpening.FlatAppearance.BorderSize = 0;
            this.mbSharpening.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSharpening.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSharpening.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSharpening.ForeColor = System.Drawing.Color.Transparent;
            this.mbSharpening.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSharpening.InactiveBackgroundImage")));
            this.mbSharpening.Location = new System.Drawing.Point(133, 13);
            this.mbSharpening.Name = "mbSharpening";
            this.mbSharpening.Size = new System.Drawing.Size(102, 102);
            this.mbSharpening.StateChangeActivated = true;
            this.mbSharpening.TabIndex = 48;
            this.mbSharpening.TabStop = false;
            this.mbSharpening.UseVisualStyleBackColor = false;
            this.mbSharpening.Click += new System.EventHandler(this.mbSharpening_Click);
            // 
            // cbCutOff
            // 
            this.cbCutOff.Active = false;
            this.cbCutOff.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCutOff.ActiveBackgroundImage")));
            this.cbCutOff.BackColor = System.Drawing.Color.Transparent;
            this.cbCutOff.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbCutOff.BackgroundImage")));
            this.cbCutOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbCutOff.ButtonSize = 102;
            this.cbCutOff.FlatAppearance.BorderSize = 0;
            this.cbCutOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbCutOff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbCutOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCutOff.ForeColor = System.Drawing.Color.Transparent;
            this.cbCutOff.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCutOff.InactiveBackgroundImage")));
            this.cbCutOff.Location = new System.Drawing.Point(18, 13);
            this.cbCutOff.Name = "cbCutOff";
            this.cbCutOff.Size = new System.Drawing.Size(102, 102);
            this.cbCutOff.StateChangeActivated = true;
            this.cbCutOff.TabIndex = 33;
            this.cbCutOff.TabStop = false;
            this.cbCutOff.UseVisualStyleBackColor = false;
            this.cbCutOff.Click += new System.EventHandler(this.CbCutOff_Click);
            this.cbCutOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCutOff_MouseDown);
            this.cbCutOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCutOff_MouseUp);
            // 
            // panelWorkingMode
            // 
            this.panelWorkingMode.Controls.Add(this.mlWorkingModeTitle);
            this.panelWorkingMode.Controls.Add(this.mlWorkingMode);
            this.panelWorkingMode.LineColor = System.Drawing.Color.LightGray;
            this.panelWorkingMode.LineWidth = 4;
            this.panelWorkingMode.Location = new System.Drawing.Point(35, 17);
            this.panelWorkingMode.Name = "panelWorkingMode";
            this.panelWorkingMode.Radius = 10;
            this.panelWorkingMode.Size = new System.Drawing.Size(356, 75);
            this.panelWorkingMode.TabIndex = 42;
            // 
            // mlWorkingModeTitle
            // 
            this.mlWorkingModeTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingModeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlWorkingModeTitle.Location = new System.Drawing.Point(14, 8);
            this.mlWorkingModeTitle.Name = "mlWorkingModeTitle";
            this.mlWorkingModeTitle.Size = new System.Drawing.Size(321, 28);
            this.mlWorkingModeTitle.TabIndex = 38;
            this.mlWorkingModeTitle.Text = "labelWorkingModeTitle";
            this.mlWorkingModeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlWorkingMode
            // 
            this.mlWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingMode.Location = new System.Drawing.Point(14, 39);
            this.mlWorkingMode.Name = "mlWorkingMode";
            this.mlWorkingMode.Size = new System.Drawing.Size(321, 28);
            this.mlWorkingMode.TabIndex = 37;
            this.mlWorkingMode.Text = "labelWorkingMode";
            this.mlWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlMaterialRegulation
            // 
            this.mlMaterialRegulation.BackColor = System.Drawing.Color.Transparent;
            this.mlMaterialRegulation.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlMaterialRegulation.Location = new System.Drawing.Point(337, 98);
            this.mlMaterialRegulation.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.mlMaterialRegulation.Name = "mlMaterialRegulation";
            this.mlMaterialRegulation.Size = new System.Drawing.Size(255, 59);
            this.mlMaterialRegulation.TabIndex = 41;
            this.mlMaterialRegulation.Text = "mlMaterialRegulation";
            this.mlMaterialRegulation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mSearchBox
            // 
            this.mSearchBox.ButtonColor = System.Drawing.Color.DimGray;
            this.mSearchBox.ItemText = "";
            this.mSearchBox.Location = new System.Drawing.Point(521, 17);
            this.mSearchBox.Name = "mSearchBox";
            this.mSearchBox.SearchText = "";
            this.mSearchBox.SelectedIndex = -1;
            this.mSearchBox.Size = new System.Drawing.Size(590, 75);
            this.mSearchBox.TabIndex = 35;
            // 
            // cmbStraightRoller
            // 
            this.cmbStraightRoller.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmbStraightRoller.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmbStraightRoller.Location = new System.Drawing.Point(847, 172);
            this.cmbStraightRoller.Name = "cmbStraightRoller";
            this.cmbStraightRoller.Size = new System.Drawing.Size(260, 125);
            this.cmbStraightRoller.TabIndex = 31;
            this.cmbStraightRoller.Value = 0;
            this.cmbStraightRoller.ValueChangedEventEnabled = false;
            this.cmbStraightRoller.ValueChanged += new System.EventHandler<Machine.UI.Controls.MultiButtonsEventArgs>(this.CmbStraightRoller_ValueChanged);
            // 
            // cpbsCradleScalingFactor
            // 
            this.cpbsCradleScalingFactor.Location = new System.Drawing.Point(400, 168);
            this.cpbsCradleScalingFactor.MaxValue = 100F;
            this.cpbsCradleScalingFactor.MinValue = 0F;
            this.cpbsCradleScalingFactor.Name = "cpbsCradleScalingFactor";
            this.cpbsCradleScalingFactor.PropertyName = "";
            this.cpbsCradleScalingFactor.Size = new System.Drawing.Size(140, 192);
            this.cpbsCradleScalingFactor.TabIndex = 30;
            this.cpbsCradleScalingFactor.Value = 0F;
            this.cpbsCradleScalingFactor.ValueChangedEventEnabled = false;
            // 
            // mlStraightRoller
            // 
            this.mlStraightRoller.BackColor = System.Drawing.Color.Transparent;
            this.mlStraightRoller.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlStraightRoller.Location = new System.Drawing.Point(847, 105);
            this.mlStraightRoller.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlStraightRoller.Name = "mlStraightRoller";
            this.mlStraightRoller.Size = new System.Drawing.Size(260, 59);
            this.mlStraightRoller.TabIndex = 32;
            this.mlStraightRoller.Text = "mlStraightRoller";
            this.mlStraightRoller.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpStatistics
            // 
            this.mpStatistics.Controls.Add(this.mlWorkingsStatistics);
            this.mpStatistics.LineColor = System.Drawing.Color.LightGray;
            this.mpStatistics.LineWidth = 5;
            this.mpStatistics.Location = new System.Drawing.Point(772, 337);
            this.mpStatistics.Name = "mpStatistics";
            this.mpStatistics.Radius = 10;
            this.mpStatistics.Size = new System.Drawing.Size(335, 172);
            this.mpStatistics.TabIndex = 51;
            // 
            // mlWorkingsStatistics
            // 
            this.mlWorkingsStatistics.BackColor = System.Drawing.Color.Transparent;
            this.mlWorkingsStatistics.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingsStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlWorkingsStatistics.Location = new System.Drawing.Point(9, 5);
            this.mlWorkingsStatistics.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlWorkingsStatistics.Name = "mlWorkingsStatistics";
            this.mlWorkingsStatistics.Size = new System.Drawing.Size(320, 162);
            this.mlWorkingsStatistics.TabIndex = 52;
            this.mlWorkingsStatistics.Text = "mlWorkingsStatistics";
            this.mlWorkingsStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mlWorkingsStatistics.DoubleClick += new System.EventHandler(this.mlWorkingsStatistics_DoubleClick);
            // 
            // mlCutterVelocity
            // 
            this.mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutterVelocity.Location = new System.Drawing.Point(583, 98);
            this.mlCutterVelocity.Name = "mlCutterVelocity";
            this.mlCutterVelocity.Size = new System.Drawing.Size(255, 59);
            this.mlCutterVelocity.TabIndex = 68;
            this.mlCutterVelocity.Text = "mlCutterVelocity";
            this.mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpbsCutterVelocity
            // 
            this.mpbsCutterVelocity.Location = new System.Drawing.Point(632, 168);
            this.mpbsCutterVelocity.MaxValue = 100F;
            this.mpbsCutterVelocity.MinValue = 0F;
            this.mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            this.mpbsCutterVelocity.PropertyName = "";
            this.mpbsCutterVelocity.Size = new System.Drawing.Size(140, 192);
            this.mpbsCutterVelocity.TabIndex = 69;
            this.mpbsCutterVelocity.Value = 0F;
            this.mpbsCutterVelocity.ValueChangedEventEnabled = false;
            // 
            // cbStop
            // 
            this.cbStop.Active = false;
            this.cbStop.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbStop.ActiveBackgroundImage")));
            this.cbStop.BackColor = System.Drawing.Color.Transparent;
            this.cbStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbStop.BackgroundImage")));
            this.cbStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbStop.ButtonSize = 125;
            this.cbStop.FlatAppearance.BorderSize = 0;
            this.cbStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStop.ForeColor = System.Drawing.Color.Transparent;
            this.cbStop.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbStop.InactiveBackgroundImage")));
            this.cbStop.Location = new System.Drawing.Point(499, 556);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(125, 125);
            this.cbStop.StateChangeActivated = true;
            this.cbStop.TabIndex = 70;
            this.cbStop.TabStop = false;
            this.cbStop.UseVisualStyleBackColor = false;
            this.cbStop.Click += new System.EventHandler(this.CbStop_Click);
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 720);
            this.Controls.Add(this.cbStop);
            this.Controls.Add(this.mlAlignment);
            this.Controls.Add(this.mpbsCutterVelocity);
            this.Controls.Add(this.mlCutterVelocity);
            this.Controls.Add(this.mpStatistics);
            this.Controls.Add(this.mlSync);
            this.Controls.Add(this.cbCradleSync);
            this.Controls.Add(this.mlCutter);
            this.Controls.Add(this.panelAlignment);
            this.Controls.Add(this.mlCradleJog);
            this.Controls.Add(this.panelJog);
            this.Controls.Add(this.panelCuttOff);
            this.Controls.Add(this.panelWorkingMode);
            this.Controls.Add(this.mlMaterialRegulation);
            this.Controls.Add(this.mSearchBox);
            this.Controls.Add(this.cmbStraightRoller);
            this.Controls.Add(this.mlStraightRoller);
            this.Controls.Add(this.cpbsCradleScalingFactor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDashboard";
            this.Text = "FormDashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDashboard_FormClosing);
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.panelAlignment.ResumeLayout(false);
            this.panelJog.ResumeLayout(false);
            this.panelCuttOff.ResumeLayout(false);
            this.panelWorkingMode.ResumeLayout(false);
            this.mpStatistics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Machine.UI.Controls.MachinePanelButtonSlider cpbsCradleScalingFactor;
        private Machine.UI.Controls.MachineMultiTwoButtons cmbStraightRoller;
        private Machine.UI.Controls.MachineLabel mlStraightRoller;
        private Machine.UI.Controls.MachineButton cbCutOff;
        private Machine.UI.Controls.MachineSettingsSearchBox mSearchBox;
        public Machine.UI.Controls.MachineButton cbCradleSync;
        private System.Windows.Forms.Label mlWorkingMode;
        private Machine.UI.Controls.MachineButton cbCradleJogCW;
        private Machine.UI.Controls.MachineButton cbCradleJogACW;
        private MachineLabel mlMaterialRegulation;
        private MachinePanelEdgeRounded panelWorkingMode;
        private System.Windows.Forms.Label mlWorkingModeTitle;
        private MachinePanelEdgeRounded panelCuttOff;
        private MachinePanelEdgeRounded panelJog;
        private MachineLabel mlCradleJog;
        private MachineLabel mlSync;
        private MachineButton mbSharpening;
        private MachinePanelEdgeRounded panelAlignment;
        private MachineButton mbAlignmentMotorSide;
        private MachineButton mbAlignmentOperatorSide;
        private MachineLabel mlAlignment;
        private MachineLabel mlCutter;
        private MachinePanelEdgeRounded mpStatistics;
        private MachineLabel mlWorkingsStatistics;
        private MachineLabel mlCutterVelocity;
        private MachinePanelButtonSlider mpbsCutterVelocity;
        private MachineButton cbStop;
    }
}