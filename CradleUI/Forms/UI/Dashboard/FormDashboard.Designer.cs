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
            mlSync = new MachineLabel();
            cbCradleSync = new MachineButton();
            mlCutter = new MachineLabel();
            mlAlignment = new MachineLabel();
            panelAlignment = new MachinePanelEdgeRounded();
            mbAlignmentMotorSide = new MachineButton();
            mbAlignmentOperatorSide = new MachineButton();
            mlCradleJog = new MachineLabel();
            panelJog = new MachinePanelEdgeRounded();
            cbCradleJogACW = new MachineButton();
            cbCradleJogCW = new MachineButton();
            panelCuttOff = new MachinePanelEdgeRounded();
            mbSharpening = new MachineButton();
            cbCutOff = new MachineButton();
            panelWorkingMode = new MachinePanelEdgeRounded();
            mlWorkingModeTitle = new System.Windows.Forms.Label();
            mlWorkingMode = new System.Windows.Forms.Label();
            mlMaterialRegulation = new MachineLabel();
            mSearchBox = new MachineSettingsSearchBox();
            cmbStraightRoller = new MachineMultiTwoButtons();
            cpbsCradleScalingFactor = new MachinePanelButtonSlider();
            mlStraightRoller = new MachineLabel();
            mpStatistics = new MachinePanelEdgeRounded();
            mlWorkingsStatistics = new MachineLabel();
            mlCutterVelocity = new MachineLabel();
            mpbsCutterVelocity = new MachinePanelButtonSlider();
            cbStop = new MachineButton();
            panelAlignment.SuspendLayout();
            panelJog.SuspendLayout();
            panelCuttOff.SuspendLayout();
            panelWorkingMode.SuspendLayout();
            mpStatistics.SuspendLayout();
            SuspendLayout();
            // 
            // mlSync
            // 
            mlSync.BackColor = System.Drawing.Color.Transparent;
            mlSync.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSync.Location = new System.Drawing.Point(-2, 112);
            mlSync.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            mlSync.Name = "mlSync";
            mlSync.Size = new System.Drawing.Size(298, 30);
            mlSync.TabIndex = 46;
            mlSync.Text = "mlSync";
            mlSync.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cbCradleSync
            // 
            cbCradleSync.Active = false;
            cbCradleSync.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleSync.ActiveBackgroundImage");
            cbCradleSync.BackColor = System.Drawing.Color.Transparent;
            cbCradleSync.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbCradleSync.BackgroundImage");
            cbCradleSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbCradleSync.ButtonSize = 125;
            cbCradleSync.FlatAppearance.BorderSize = 0;
            cbCradleSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbCradleSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbCradleSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbCradleSync.ForeColor = System.Drawing.Color.Transparent;
            cbCradleSync.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleSync.InactiveBackgroundImage");
            cbCradleSync.Location = new System.Drawing.Point(66, 151);
            cbCradleSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCradleSync.Name = "cbCradleSync";
            cbCradleSync.Size = new System.Drawing.Size(125, 125);
            cbCradleSync.StateChangeActivated = true;
            cbCradleSync.TabIndex = 36;
            cbCradleSync.TabStop = false;
            cbCradleSync.UseVisualStyleBackColor = false;
            cbCradleSync.Click += CbCradleSync_Click;
            // 
            // mlCutter
            // 
            mlCutter.BackColor = System.Drawing.Color.Transparent;
            mlCutter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutter.Location = new System.Drawing.Point(844, 520);
            mlCutter.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlCutter.Name = "mlCutter";
            mlCutter.Size = new System.Drawing.Size(293, 35);
            mlCutter.TabIndex = 50;
            mlCutter.Text = "mlCutter";
            mlCutter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mlAlignment
            // 
            mlAlignment.BackColor = System.Drawing.Color.Transparent;
            mlAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlAlignment.Location = new System.Drawing.Point(253, 376);
            mlAlignment.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlAlignment.Name = "mlAlignment";
            mlAlignment.Size = new System.Drawing.Size(204, 32);
            mlAlignment.TabIndex = 49;
            mlAlignment.Text = "mlAlignment";
            mlAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelAlignment
            // 
            panelAlignment.Controls.Add(mbAlignmentMotorSide);
            panelAlignment.Controls.Add(mbAlignmentOperatorSide);
            panelAlignment.LineColor = System.Drawing.Color.LightGray;
            panelAlignment.LineWidth = 5;
            panelAlignment.Location = new System.Drawing.Point(273, 413);
            panelAlignment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelAlignment.Name = "panelAlignment";
            panelAlignment.Radius = 10;
            panelAlignment.Size = new System.Drawing.Size(158, 295);
            panelAlignment.TabIndex = 45;
            // 
            // mbAlignmentMotorSide
            // 
            mbAlignmentMotorSide.Active = false;
            mbAlignmentMotorSide.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAlignmentMotorSide.ActiveBackgroundImage");
            mbAlignmentMotorSide.BackColor = System.Drawing.Color.Transparent;
            mbAlignmentMotorSide.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbAlignmentMotorSide.BackgroundImage");
            mbAlignmentMotorSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbAlignmentMotorSide.ButtonSize = 102;
            mbAlignmentMotorSide.FlatAppearance.BorderSize = 0;
            mbAlignmentMotorSide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbAlignmentMotorSide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbAlignmentMotorSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbAlignmentMotorSide.ForeColor = System.Drawing.Color.Transparent;
            mbAlignmentMotorSide.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAlignmentMotorSide.InactiveBackgroundImage");
            mbAlignmentMotorSide.Location = new System.Drawing.Point(31, 21);
            mbAlignmentMotorSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAlignmentMotorSide.Name = "mbAlignmentMotorSide";
            mbAlignmentMotorSide.Size = new System.Drawing.Size(102, 102);
            mbAlignmentMotorSide.StateChangeActivated = true;
            mbAlignmentMotorSide.TabIndex = 40;
            mbAlignmentMotorSide.TabStop = false;
            mbAlignmentMotorSide.UseVisualStyleBackColor = false;
            mbAlignmentMotorSide.MouseDown += mbAlignmentMotorSide_MouseDown;
            mbAlignmentMotorSide.MouseUp += mbAlignmentMotorSide_MouseUp;
            // 
            // mbAlignmentOperatorSide
            // 
            mbAlignmentOperatorSide.Active = false;
            mbAlignmentOperatorSide.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAlignmentOperatorSide.ActiveBackgroundImage");
            mbAlignmentOperatorSide.BackColor = System.Drawing.Color.Transparent;
            mbAlignmentOperatorSide.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbAlignmentOperatorSide.BackgroundImage");
            mbAlignmentOperatorSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbAlignmentOperatorSide.ButtonSize = 102;
            mbAlignmentOperatorSide.FlatAppearance.BorderSize = 0;
            mbAlignmentOperatorSide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbAlignmentOperatorSide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbAlignmentOperatorSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbAlignmentOperatorSide.ForeColor = System.Drawing.Color.Transparent;
            mbAlignmentOperatorSide.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAlignmentOperatorSide.InactiveBackgroundImage");
            mbAlignmentOperatorSide.Location = new System.Drawing.Point(31, 180);
            mbAlignmentOperatorSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAlignmentOperatorSide.Name = "mbAlignmentOperatorSide";
            mbAlignmentOperatorSide.Size = new System.Drawing.Size(102, 102);
            mbAlignmentOperatorSide.StateChangeActivated = true;
            mbAlignmentOperatorSide.TabIndex = 38;
            mbAlignmentOperatorSide.TabStop = false;
            mbAlignmentOperatorSide.UseVisualStyleBackColor = false;
            mbAlignmentOperatorSide.MouseDown += mbAlignmentOperatorSide_MouseDown;
            mbAlignmentOperatorSide.MouseUp += mbAlignmentOperatorSide_MouseUp;
            // 
            // mlCradleJog
            // 
            mlCradleJog.BackColor = System.Drawing.Color.Transparent;
            mlCradleJog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCradleJog.Location = new System.Drawing.Point(25, 376);
            mlCradleJog.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlCradleJog.Name = "mlCradleJog";
            mlCradleJog.Size = new System.Drawing.Size(204, 31);
            mlCradleJog.TabIndex = 45;
            mlCradleJog.Text = "mlCradleJog";
            mlCradleJog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelJog
            // 
            panelJog.Controls.Add(cbCradleJogACW);
            panelJog.Controls.Add(cbCradleJogCW);
            panelJog.LineColor = System.Drawing.Color.LightGray;
            panelJog.LineWidth = 5;
            panelJog.Location = new System.Drawing.Point(41, 413);
            panelJog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelJog.Name = "panelJog";
            panelJog.Radius = 10;
            panelJog.Size = new System.Drawing.Size(158, 295);
            panelJog.TabIndex = 44;
            // 
            // cbCradleJogACW
            // 
            cbCradleJogACW.Active = false;
            cbCradleJogACW.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleJogACW.ActiveBackgroundImage");
            cbCradleJogACW.BackColor = System.Drawing.Color.Transparent;
            cbCradleJogACW.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbCradleJogACW.BackgroundImage");
            cbCradleJogACW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbCradleJogACW.ButtonSize = 102;
            cbCradleJogACW.FlatAppearance.BorderSize = 0;
            cbCradleJogACW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbCradleJogACW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbCradleJogACW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbCradleJogACW.ForeColor = System.Drawing.Color.Transparent;
            cbCradleJogACW.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleJogACW.InactiveBackgroundImage");
            cbCradleJogACW.Location = new System.Drawing.Point(25, 180);
            cbCradleJogACW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCradleJogACW.Name = "cbCradleJogACW";
            cbCradleJogACW.Size = new System.Drawing.Size(102, 102);
            cbCradleJogACW.StateChangeActivated = true;
            cbCradleJogACW.TabIndex = 40;
            cbCradleJogACW.TabStop = false;
            cbCradleJogACW.UseVisualStyleBackColor = false;
            cbCradleJogACW.MouseDown += CbCradleJogACW_MouseDown;
            cbCradleJogACW.MouseUp += CbCradleJogACW_MouseUp;
            // 
            // cbCradleJogCW
            // 
            cbCradleJogCW.Active = false;
            cbCradleJogCW.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleJogCW.ActiveBackgroundImage");
            cbCradleJogCW.BackColor = System.Drawing.Color.Transparent;
            cbCradleJogCW.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbCradleJogCW.BackgroundImage");
            cbCradleJogCW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbCradleJogCW.ButtonSize = 102;
            cbCradleJogCW.FlatAppearance.BorderSize = 0;
            cbCradleJogCW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbCradleJogCW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbCradleJogCW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbCradleJogCW.ForeColor = System.Drawing.Color.Transparent;
            cbCradleJogCW.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCradleJogCW.InactiveBackgroundImage");
            cbCradleJogCW.Location = new System.Drawing.Point(25, 21);
            cbCradleJogCW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCradleJogCW.Name = "cbCradleJogCW";
            cbCradleJogCW.Size = new System.Drawing.Size(102, 102);
            cbCradleJogCW.StateChangeActivated = true;
            cbCradleJogCW.TabIndex = 38;
            cbCradleJogCW.TabStop = false;
            cbCradleJogCW.UseVisualStyleBackColor = false;
            cbCradleJogCW.MouseDown += CbCradleJogCW_MouseDown;
            cbCradleJogCW.MouseUp += CbCradleJogCW_MouseUp;
            // 
            // panelCuttOff
            // 
            panelCuttOff.Controls.Add(mbSharpening);
            panelCuttOff.Controls.Add(cbCutOff);
            panelCuttOff.LineColor = System.Drawing.Color.LightGray;
            panelCuttOff.LineWidth = 5;
            panelCuttOff.Location = new System.Drawing.Point(841, 563);
            panelCuttOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelCuttOff.Name = "panelCuttOff";
            panelCuttOff.Radius = 10;
            panelCuttOff.Size = new System.Drawing.Size(296, 144);
            panelCuttOff.TabIndex = 43;
            // 
            // mbSharpening
            // 
            mbSharpening.Active = false;
            mbSharpening.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSharpening.ActiveBackgroundImage");
            mbSharpening.BackColor = System.Drawing.Color.Transparent;
            mbSharpening.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbSharpening.BackgroundImage");
            mbSharpening.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbSharpening.ButtonSize = 102;
            mbSharpening.FlatAppearance.BorderSize = 0;
            mbSharpening.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbSharpening.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbSharpening.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbSharpening.ForeColor = System.Drawing.Color.Transparent;
            mbSharpening.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSharpening.InactiveBackgroundImage");
            mbSharpening.Location = new System.Drawing.Point(163, 19);
            mbSharpening.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSharpening.Name = "mbSharpening";
            mbSharpening.Size = new System.Drawing.Size(102, 102);
            mbSharpening.StateChangeActivated = true;
            mbSharpening.TabIndex = 48;
            mbSharpening.TabStop = false;
            mbSharpening.UseVisualStyleBackColor = false;
            mbSharpening.Click += mbSharpening_Click;
            // 
            // cbCutOff
            // 
            cbCutOff.Active = false;
            cbCutOff.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCutOff.ActiveBackgroundImage");
            cbCutOff.BackColor = System.Drawing.Color.Transparent;
            cbCutOff.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbCutOff.BackgroundImage");
            cbCutOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbCutOff.ButtonSize = 102;
            cbCutOff.FlatAppearance.BorderSize = 0;
            cbCutOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbCutOff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbCutOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbCutOff.ForeColor = System.Drawing.Color.Transparent;
            cbCutOff.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbCutOff.InactiveBackgroundImage");
            cbCutOff.Location = new System.Drawing.Point(31, 19);
            cbCutOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCutOff.Name = "cbCutOff";
            cbCutOff.Size = new System.Drawing.Size(102, 102);
            cbCutOff.StateChangeActivated = true;
            cbCutOff.TabIndex = 33;
            cbCutOff.TabStop = false;
            cbCutOff.UseVisualStyleBackColor = false;
            cbCutOff.Click += CbCutOff_Click;
            cbCutOff.MouseDown += CbCutOff_MouseDown;
            cbCutOff.MouseUp += CbCutOff_MouseUp;
            // 
            // panelWorkingMode
            // 
            panelWorkingMode.Controls.Add(mlWorkingModeTitle);
            panelWorkingMode.Controls.Add(mlWorkingMode);
            panelWorkingMode.LineColor = System.Drawing.Color.LightGray;
            panelWorkingMode.LineWidth = 4;
            panelWorkingMode.Location = new System.Drawing.Point(16, 12);
            panelWorkingMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelWorkingMode.Name = "panelWorkingMode";
            panelWorkingMode.Radius = 10;
            panelWorkingMode.Size = new System.Drawing.Size(415, 87);
            panelWorkingMode.TabIndex = 42;
            // 
            // mlWorkingModeTitle
            // 
            mlWorkingModeTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlWorkingModeTitle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            mlWorkingModeTitle.Location = new System.Drawing.Point(16, 9);
            mlWorkingModeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlWorkingModeTitle.Name = "mlWorkingModeTitle";
            mlWorkingModeTitle.Size = new System.Drawing.Size(374, 32);
            mlWorkingModeTitle.TabIndex = 38;
            mlWorkingModeTitle.Text = "labelWorkingModeTitle";
            mlWorkingModeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlWorkingMode
            // 
            mlWorkingMode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlWorkingMode.Location = new System.Drawing.Point(16, 45);
            mlWorkingMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlWorkingMode.Name = "mlWorkingMode";
            mlWorkingMode.Size = new System.Drawing.Size(374, 32);
            mlWorkingMode.TabIndex = 37;
            mlWorkingMode.Text = "labelWorkingMode";
            mlWorkingMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlMaterialRegulation
            // 
            mlMaterialRegulation.BackColor = System.Drawing.Color.Transparent;
            mlMaterialRegulation.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlMaterialRegulation.Location = new System.Drawing.Point(253, 112);
            mlMaterialRegulation.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            mlMaterialRegulation.Name = "mlMaterialRegulation";
            mlMaterialRegulation.Size = new System.Drawing.Size(298, 30);
            mlMaterialRegulation.TabIndex = 41;
            mlMaterialRegulation.Text = "mlMaterialRegulation";
            mlMaterialRegulation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mSearchBox
            // 
            mSearchBox.ButtonColor = System.Drawing.Color.DimGray;
            mSearchBox.ItemText = "";
            mSearchBox.Location = new System.Drawing.Point(449, 12);
            mSearchBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mSearchBox.Name = "mSearchBox";
            mSearchBox.SearchText = "";
            mSearchBox.SelectedIndex = -1;
            mSearchBox.Size = new System.Drawing.Size(688, 87);
            mSearchBox.TabIndex = 35;
            // 
            // cmbStraightRoller
            // 
            cmbStraightRoller.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            cmbStraightRoller.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            cmbStraightRoller.Location = new System.Drawing.Point(834, 155);
            cmbStraightRoller.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cmbStraightRoller.Name = "cmbStraightRoller";
            cmbStraightRoller.Size = new System.Drawing.Size(303, 144);
            cmbStraightRoller.TabIndex = 31;
            cmbStraightRoller.Value = 0;
            cmbStraightRoller.ValueChangedEventEnabled = false;
            cmbStraightRoller.ValueChanged += CmbStraightRoller_ValueChanged;
            // 
            // cpbsCradleScalingFactor
            // 
            cpbsCradleScalingFactor.Location = new System.Drawing.Point(327, 155);
            cpbsCradleScalingFactor.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cpbsCradleScalingFactor.Name = "cpbsCradleScalingFactor";
            cpbsCradleScalingFactor.Size = new System.Drawing.Size(163, 222);
            cpbsCradleScalingFactor.TabIndex = 30;
            // 
            // mlStraightRoller
            // 
            mlStraightRoller.BackColor = System.Drawing.Color.Transparent;
            mlStraightRoller.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlStraightRoller.Location = new System.Drawing.Point(834, 112);
            mlStraightRoller.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlStraightRoller.Name = "mlStraightRoller";
            mlStraightRoller.Size = new System.Drawing.Size(303, 30);
            mlStraightRoller.TabIndex = 32;
            mlStraightRoller.Text = "mlStraightRoller";
            mlStraightRoller.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpStatistics
            // 
            mpStatistics.Controls.Add(mlWorkingsStatistics);
            mpStatistics.LineColor = System.Drawing.Color.LightGray;
            mpStatistics.LineWidth = 5;
            mpStatistics.Location = new System.Drawing.Point(746, 315);
            mpStatistics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpStatistics.Name = "mpStatistics";
            mpStatistics.Radius = 10;
            mpStatistics.Size = new System.Drawing.Size(391, 198);
            mpStatistics.TabIndex = 51;
            // 
            // mlWorkingsStatistics
            // 
            mlWorkingsStatistics.BackColor = System.Drawing.Color.Transparent;
            mlWorkingsStatistics.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlWorkingsStatistics.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            mlWorkingsStatistics.Location = new System.Drawing.Point(10, 6);
            mlWorkingsStatistics.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlWorkingsStatistics.Name = "mlWorkingsStatistics";
            mlWorkingsStatistics.Size = new System.Drawing.Size(373, 187);
            mlWorkingsStatistics.TabIndex = 52;
            mlWorkingsStatistics.Text = "mlWorkingsStatistics";
            mlWorkingsStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            mlWorkingsStatistics.DoubleClick += mlWorkingsStatistics_DoubleClick;
            // 
            // mlCutterVelocity
            // 
            mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutterVelocity.Location = new System.Drawing.Point(523, 112);
            mlCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCutterVelocity.Name = "mlCutterVelocity";
            mlCutterVelocity.Size = new System.Drawing.Size(298, 30);
            mlCutterVelocity.TabIndex = 68;
            mlCutterVelocity.Text = "mlCutterVelocity";
            mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpbsCutterVelocity
            // 
            mpbsCutterVelocity.Location = new System.Drawing.Point(580, 155);
            mpbsCutterVelocity.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            mpbsCutterVelocity.Size = new System.Drawing.Size(163, 222);
            mpbsCutterVelocity.TabIndex = 69;
            // 
            // cbStop
            // 
            cbStop.Active = false;
            cbStop.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbStop.ActiveBackgroundImage");
            cbStop.BackColor = System.Drawing.Color.Transparent;
            cbStop.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbStop.BackgroundImage");
            cbStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbStop.ButtonSize = 125;
            cbStop.FlatAppearance.BorderSize = 0;
            cbStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbStop.ForeColor = System.Drawing.Color.Transparent;
            cbStop.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbStop.InactiveBackgroundImage");
            cbStop.Location = new System.Drawing.Point(580, 582);
            cbStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbStop.Name = "cbStop";
            cbStop.Size = new System.Drawing.Size(125, 125);
            cbStop.StateChangeActivated = true;
            cbStop.TabIndex = 70;
            cbStop.TabStop = false;
            cbStop.UseVisualStyleBackColor = false;
            cbStop.Click += CbStop_Click;
            // 
            // FormDashboard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(cbStop);
            Controls.Add(mlAlignment);
            Controls.Add(mpbsCutterVelocity);
            Controls.Add(mlCutterVelocity);
            Controls.Add(mpStatistics);
            Controls.Add(mlSync);
            Controls.Add(cbCradleSync);
            Controls.Add(mlCutter);
            Controls.Add(panelAlignment);
            Controls.Add(mlCradleJog);
            Controls.Add(panelJog);
            Controls.Add(panelCuttOff);
            Controls.Add(panelWorkingMode);
            Controls.Add(mlMaterialRegulation);
            Controls.Add(mSearchBox);
            Controls.Add(cmbStraightRoller);
            Controls.Add(mlStraightRoller);
            Controls.Add(cpbsCradleScalingFactor);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FormDashboard";
            Text = "FormDashboard";
            FormClosing += FormDashboard_FormClosing;
            Load += FormDashboard_Load;
            panelAlignment.ResumeLayout(false);
            panelJog.ResumeLayout(false);
            panelCuttOff.ResumeLayout(false);
            panelWorkingMode.ResumeLayout(false);
            mpStatistics.ResumeLayout(false);
            ResumeLayout(false);
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