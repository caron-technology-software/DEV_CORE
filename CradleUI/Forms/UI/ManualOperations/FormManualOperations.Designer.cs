using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormManualOperations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManualOperations));
            mlCradleJog = new MachineLabel();
            mpCradleJog = new MachinePanelEdgeRounded();
            cbCradleJogCW = new MachineButton();
            cbCradleJogACW = new MachineButton();
            panelBorderBottom = new System.Windows.Forms.Panel();
            cbStop = new MachineButton();
            mlTitan = new MachineLabel();
            mpTitan = new MachinePanelEdgeRounded();
            mbTitanDown = new MachineButton();
            mbTitanUp = new MachineButton();
            mbAutoCentering = new MachineButton();
            mlAutoAlignment = new MachineLabel();
            panelBorderTop = new System.Windows.Forms.Panel();
            mlAlignment = new MachineLabel();
            mpAlignment = new MachinePanelEdgeRounded();
            mbAlignmentMotorSide = new MachineButton();
            mbAlignmentOperatorSide = new MachineButton();
            mlOverturning = new MachineLabel();
            mpOverturning = new MachinePanelEdgeRounded();
            mbOverturningUp = new MachineButton();
            mbOverturningDown = new MachineButton();
            mlSpoon = new MachineLabel();
            mpSpoon = new MachinePanelEdgeRounded();
            mbSpoonUp = new MachineButton();
            mbSpoonDown = new MachineButton();
            mlTitle = new System.Windows.Forms.Label();
            mlCutter = new MachineLabel();
            panelCuttOff = new MachinePanelEdgeRounded();
            mbSharpening = new MachineButton();
            cbCutOff = new MachineButton();
            mlCutterVelocity = new MachineLabel();
            mpbsCutterVelocity = new MachineButtonSlider();
            mpCradleJog.SuspendLayout();
            mpTitan.SuspendLayout();
            mpAlignment.SuspendLayout();
            mpOverturning.SuspendLayout();
            mpSpoon.SuspendLayout();
            panelCuttOff.SuspendLayout();
            SuspendLayout();
            // 
            // mlCradleJog
            // 
            mlCradleJog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCradleJog.Location = new System.Drawing.Point(0, 55);
            mlCradleJog.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCradleJog.Name = "mlCradleJog";
            mlCradleJog.Size = new System.Drawing.Size(239, 34);
            mlCradleJog.TabIndex = 63;
            mlCradleJog.Text = "mlCradleJog";
            mlCradleJog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpCradleJog
            // 
            mpCradleJog.Controls.Add(cbCradleJogCW);
            mpCradleJog.Controls.Add(cbCradleJogACW);
            mpCradleJog.LineColor = System.Drawing.Color.LightGray;
            mpCradleJog.LineWidth = 5;
            mpCradleJog.Location = new System.Drawing.Point(36, 93);
            mpCradleJog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpCradleJog.Name = "mpCradleJog";
            mpCradleJog.Radius = 10;
            mpCradleJog.Size = new System.Drawing.Size(158, 283);
            mpCradleJog.TabIndex = 62;
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
            cbCradleJogCW.Location = new System.Drawing.Point(19, 14);
            cbCradleJogCW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCradleJogCW.Name = "cbCradleJogCW";
            cbCradleJogCW.Size = new System.Drawing.Size(102, 102);
            cbCradleJogCW.StateChangeActivated = true;
            cbCradleJogCW.TabIndex = 1;
            cbCradleJogCW.TabStop = false;
            cbCradleJogCW.UseVisualStyleBackColor = false;
            cbCradleJogCW.MouseDown += CbCradleJogCW_MouseDown;
            cbCradleJogCW.MouseUp += CbCradleJogCW_MouseUp;
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
            cbCradleJogACW.Location = new System.Drawing.Point(19, 149);
            cbCradleJogACW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCradleJogACW.Name = "cbCradleJogACW";
            cbCradleJogACW.Size = new System.Drawing.Size(102, 102);
            cbCradleJogACW.StateChangeActivated = true;
            cbCradleJogACW.TabIndex = 0;
            cbCradleJogACW.TabStop = false;
            cbCradleJogACW.UseVisualStyleBackColor = false;
            cbCradleJogACW.MouseDown += CbCradleJogACW_MouseDown;
            cbCradleJogACW.MouseUp += CbCradleJogACW_MouseUp;
            // 
            // panelBorderBottom
            // 
            panelBorderBottom.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panelBorderBottom.Location = new System.Drawing.Point(6, 819);
            panelBorderBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBorderBottom.Name = "panelBorderBottom";
            panelBorderBottom.Size = new System.Drawing.Size(1324, 5);
            panelBorderBottom.TabIndex = 56;
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
            cbStop.Location = new System.Drawing.Point(499, 581);
            cbStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbStop.Name = "cbStop";
            cbStop.Size = new System.Drawing.Size(125, 125);
            cbStop.StateChangeActivated = true;
            cbStop.TabIndex = 54;
            cbStop.TabStop = false;
            cbStop.UseVisualStyleBackColor = false;
            cbStop.Click += CbStop_Click;
            // 
            // mlTitan
            // 
            mlTitan.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlTitan.Location = new System.Drawing.Point(892, 55);
            mlTitan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlTitan.Name = "mlTitan";
            mlTitan.Size = new System.Drawing.Size(239, 34);
            mlTitan.TabIndex = 61;
            mlTitan.Text = "mlTitan";
            mlTitan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpTitan
            // 
            mpTitan.Controls.Add(mbTitanDown);
            mpTitan.Controls.Add(mbTitanUp);
            mpTitan.LineColor = System.Drawing.Color.LightGray;
            mpTitan.LineWidth = 5;
            mpTitan.Location = new System.Drawing.Point(928, 93);
            mpTitan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpTitan.Name = "mpTitan";
            mpTitan.Radius = 10;
            mpTitan.Size = new System.Drawing.Size(158, 283);
            mpTitan.TabIndex = 60;
            // 
            // mbTitanDown
            // 
            mbTitanDown.Active = false;
            mbTitanDown.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbTitanDown.ActiveBackgroundImage");
            mbTitanDown.BackColor = System.Drawing.Color.Transparent;
            mbTitanDown.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbTitanDown.BackgroundImage");
            mbTitanDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbTitanDown.ButtonSize = 102;
            mbTitanDown.FlatAppearance.BorderSize = 0;
            mbTitanDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbTitanDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbTitanDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbTitanDown.ForeColor = System.Drawing.Color.Transparent;
            mbTitanDown.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbTitanDown.InactiveBackgroundImage");
            mbTitanDown.Location = new System.Drawing.Point(19, 149);
            mbTitanDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbTitanDown.Name = "mbTitanDown";
            mbTitanDown.Size = new System.Drawing.Size(102, 102);
            mbTitanDown.StateChangeActivated = true;
            mbTitanDown.TabIndex = 1;
            mbTitanDown.TabStop = false;
            mbTitanDown.UseVisualStyleBackColor = false;
            mbTitanDown.MouseDown += mbTitanDown_MouseDown;
            mbTitanDown.MouseUp += mbTitanDown_MouseUp;
            // 
            // mbTitanUp
            // 
            mbTitanUp.Active = false;
            mbTitanUp.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbTitanUp.ActiveBackgroundImage");
            mbTitanUp.BackColor = System.Drawing.Color.Transparent;
            mbTitanUp.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbTitanUp.BackgroundImage");
            mbTitanUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbTitanUp.ButtonSize = 102;
            mbTitanUp.FlatAppearance.BorderSize = 0;
            mbTitanUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbTitanUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbTitanUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbTitanUp.ForeColor = System.Drawing.Color.Transparent;
            mbTitanUp.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbTitanUp.InactiveBackgroundImage");
            mbTitanUp.Location = new System.Drawing.Point(19, 14);
            mbTitanUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbTitanUp.Name = "mbTitanUp";
            mbTitanUp.Size = new System.Drawing.Size(102, 102);
            mbTitanUp.StateChangeActivated = true;
            mbTitanUp.TabIndex = 0;
            mbTitanUp.TabStop = false;
            mbTitanUp.UseVisualStyleBackColor = false;
            mbTitanUp.MouseDown += mbTitanUp_MouseDown;
            mbTitanUp.MouseUp += mbTitanUp_MouseUp;
            // 
            // mbAutoCentering
            // 
            mbAutoCentering.Active = false;
            mbAutoCentering.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAutoCentering.ActiveBackgroundImage");
            mbAutoCentering.BackColor = System.Drawing.Color.Transparent;
            mbAutoCentering.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbAutoCentering.BackgroundImage");
            mbAutoCentering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbAutoCentering.ButtonSize = 112;
            mbAutoCentering.FlatAppearance.BorderSize = 0;
            mbAutoCentering.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbAutoCentering.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbAutoCentering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbAutoCentering.ForeColor = System.Drawing.Color.Transparent;
            mbAutoCentering.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAutoCentering.InactiveBackgroundImage");
            mbAutoCentering.Location = new System.Drawing.Point(947, 425);
            mbAutoCentering.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAutoCentering.Name = "mbAutoCentering";
            mbAutoCentering.Size = new System.Drawing.Size(112, 112);
            mbAutoCentering.StateChangeActivated = true;
            mbAutoCentering.TabIndex = 58;
            mbAutoCentering.TabStop = false;
            mbAutoCentering.UseVisualStyleBackColor = false;
            mbAutoCentering.Click += mbAutoAlignment_Click;
            // 
            // mlAutoAlignment
            // 
            mlAutoAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlAutoAlignment.Location = new System.Drawing.Point(862, 385);
            mlAutoAlignment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlAutoAlignment.Name = "mlAutoAlignment";
            mlAutoAlignment.Size = new System.Drawing.Size(298, 26);
            mlAutoAlignment.TabIndex = 57;
            mlAutoAlignment.Text = "mlAutoAlignment";
            mlAutoAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelBorderTop
            // 
            panelBorderTop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panelBorderTop.Location = new System.Drawing.Point(0, 570);
            panelBorderTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBorderTop.Name = "panelBorderTop";
            panelBorderTop.Size = new System.Drawing.Size(1324, 5);
            panelBorderTop.TabIndex = 55;
            // 
            // mlAlignment
            // 
            mlAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlAlignment.Location = new System.Drawing.Point(667, 55);
            mlAlignment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlAlignment.Name = "mlAlignment";
            mlAlignment.Size = new System.Drawing.Size(239, 35);
            mlAlignment.TabIndex = 54;
            mlAlignment.Text = "mlAlignment";
            mlAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpAlignment
            // 
            mpAlignment.Controls.Add(mbAlignmentMotorSide);
            mpAlignment.Controls.Add(mbAlignmentOperatorSide);
            mpAlignment.LineColor = System.Drawing.Color.LightGray;
            mpAlignment.LineWidth = 5;
            mpAlignment.Location = new System.Drawing.Point(705, 93);
            mpAlignment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpAlignment.Name = "mpAlignment";
            mpAlignment.Radius = 10;
            mpAlignment.Size = new System.Drawing.Size(158, 283);
            mpAlignment.TabIndex = 53;
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
            mbAlignmentMotorSide.Location = new System.Drawing.Point(18, 14);
            mbAlignmentMotorSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAlignmentMotorSide.Name = "mbAlignmentMotorSide";
            mbAlignmentMotorSide.Size = new System.Drawing.Size(102, 102);
            mbAlignmentMotorSide.StateChangeActivated = true;
            mbAlignmentMotorSide.TabIndex = 0;
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
            mbAlignmentOperatorSide.Location = new System.Drawing.Point(18, 149);
            mbAlignmentOperatorSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAlignmentOperatorSide.Name = "mbAlignmentOperatorSide";
            mbAlignmentOperatorSide.Size = new System.Drawing.Size(102, 102);
            mbAlignmentOperatorSide.StateChangeActivated = true;
            mbAlignmentOperatorSide.TabIndex = 1;
            mbAlignmentOperatorSide.TabStop = false;
            mbAlignmentOperatorSide.UseVisualStyleBackColor = false;
            mbAlignmentOperatorSide.MouseDown += mbAlignmentOperatorSide_MouseDown;
            mbAlignmentOperatorSide.MouseUp += mbAlignmentOperatorSide_MouseUp;
            // 
            // mlOverturning
            // 
            mlOverturning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlOverturning.Location = new System.Drawing.Point(439, 55);
            mlOverturning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlOverturning.Name = "mlOverturning";
            mlOverturning.Size = new System.Drawing.Size(239, 34);
            mlOverturning.TabIndex = 51;
            mlOverturning.Text = "mlOverturning";
            mlOverturning.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpOverturning
            // 
            mpOverturning.Controls.Add(mbOverturningUp);
            mpOverturning.Controls.Add(mbOverturningDown);
            mpOverturning.LineColor = System.Drawing.Color.LightGray;
            mpOverturning.LineWidth = 5;
            mpOverturning.Location = new System.Drawing.Point(480, 93);
            mpOverturning.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpOverturning.Name = "mpOverturning";
            mpOverturning.Radius = 10;
            mpOverturning.Size = new System.Drawing.Size(158, 283);
            mpOverturning.TabIndex = 50;
            // 
            // mbOverturningUp
            // 
            mbOverturningUp.Active = false;
            mbOverturningUp.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbOverturningUp.ActiveBackgroundImage");
            mbOverturningUp.BackColor = System.Drawing.Color.Transparent;
            mbOverturningUp.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbOverturningUp.BackgroundImage");
            mbOverturningUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbOverturningUp.ButtonSize = 102;
            mbOverturningUp.FlatAppearance.BorderSize = 0;
            mbOverturningUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbOverturningUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbOverturningUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbOverturningUp.ForeColor = System.Drawing.Color.Transparent;
            mbOverturningUp.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbOverturningUp.InactiveBackgroundImage");
            mbOverturningUp.Location = new System.Drawing.Point(19, 14);
            mbOverturningUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbOverturningUp.Name = "mbOverturningUp";
            mbOverturningUp.Size = new System.Drawing.Size(102, 102);
            mbOverturningUp.StateChangeActivated = true;
            mbOverturningUp.TabIndex = 0;
            mbOverturningUp.TabStop = false;
            mbOverturningUp.UseVisualStyleBackColor = false;
            mbOverturningUp.MouseDown += mbOverturningUp_MouseDown;
            mbOverturningUp.MouseUp += mbOverturningUp_MouseUp;
            // 
            // mbOverturningDown
            // 
            mbOverturningDown.Active = false;
            mbOverturningDown.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbOverturningDown.ActiveBackgroundImage");
            mbOverturningDown.BackColor = System.Drawing.Color.Transparent;
            mbOverturningDown.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbOverturningDown.BackgroundImage");
            mbOverturningDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbOverturningDown.ButtonSize = 102;
            mbOverturningDown.FlatAppearance.BorderSize = 0;
            mbOverturningDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbOverturningDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbOverturningDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbOverturningDown.ForeColor = System.Drawing.Color.Transparent;
            mbOverturningDown.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbOverturningDown.InactiveBackgroundImage");
            mbOverturningDown.Location = new System.Drawing.Point(19, 149);
            mbOverturningDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbOverturningDown.Name = "mbOverturningDown";
            mbOverturningDown.Size = new System.Drawing.Size(102, 102);
            mbOverturningDown.StateChangeActivated = true;
            mbOverturningDown.TabIndex = 1;
            mbOverturningDown.TabStop = false;
            mbOverturningDown.UseVisualStyleBackColor = false;
            mbOverturningDown.MouseDown += mbOverturningDown_MouseDown;
            mbOverturningDown.MouseUp += mbOverturningDown_MouseUp;
            // 
            // mlSpoon
            // 
            mlSpoon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlSpoon.Location = new System.Drawing.Point(219, 55);
            mlSpoon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlSpoon.Name = "mlSpoon";
            mlSpoon.Size = new System.Drawing.Size(239, 34);
            mlSpoon.TabIndex = 49;
            mlSpoon.Text = "mlSpoon";
            mlSpoon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpSpoon
            // 
            mpSpoon.Controls.Add(mbSpoonUp);
            mpSpoon.Controls.Add(mbSpoonDown);
            mpSpoon.LineColor = System.Drawing.Color.LightGray;
            mpSpoon.LineWidth = 5;
            mpSpoon.Location = new System.Drawing.Point(256, 93);
            mpSpoon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpSpoon.Name = "mpSpoon";
            mpSpoon.Radius = 10;
            mpSpoon.Size = new System.Drawing.Size(158, 283);
            mpSpoon.TabIndex = 48;
            // 
            // mbSpoonUp
            // 
            mbSpoonUp.Active = false;
            mbSpoonUp.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSpoonUp.ActiveBackgroundImage");
            mbSpoonUp.BackColor = System.Drawing.Color.Transparent;
            mbSpoonUp.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbSpoonUp.BackgroundImage");
            mbSpoonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbSpoonUp.ButtonSize = 102;
            mbSpoonUp.FlatAppearance.BorderSize = 0;
            mbSpoonUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbSpoonUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbSpoonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbSpoonUp.ForeColor = System.Drawing.Color.Transparent;
            mbSpoonUp.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSpoonUp.InactiveBackgroundImage");
            mbSpoonUp.Location = new System.Drawing.Point(15, 14);
            mbSpoonUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSpoonUp.Name = "mbSpoonUp";
            mbSpoonUp.Size = new System.Drawing.Size(102, 102);
            mbSpoonUp.StateChangeActivated = true;
            mbSpoonUp.TabIndex = 0;
            mbSpoonUp.TabStop = false;
            mbSpoonUp.UseVisualStyleBackColor = false;
            mbSpoonUp.MouseDown += mbSpoonUp_MouseDown;
            // 
            // mbSpoonDown
            // 
            mbSpoonDown.Active = false;
            mbSpoonDown.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSpoonDown.ActiveBackgroundImage");
            mbSpoonDown.BackColor = System.Drawing.Color.Transparent;
            mbSpoonDown.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbSpoonDown.BackgroundImage");
            mbSpoonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbSpoonDown.ButtonSize = 102;
            mbSpoonDown.FlatAppearance.BorderSize = 0;
            mbSpoonDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbSpoonDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbSpoonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbSpoonDown.ForeColor = System.Drawing.Color.Transparent;
            mbSpoonDown.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbSpoonDown.InactiveBackgroundImage");
            mbSpoonDown.Location = new System.Drawing.Point(15, 149);
            mbSpoonDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSpoonDown.Name = "mbSpoonDown";
            mbSpoonDown.Size = new System.Drawing.Size(102, 102);
            mbSpoonDown.StateChangeActivated = true;
            mbSpoonDown.TabIndex = 1;
            mbSpoonDown.TabStop = false;
            mbSpoonDown.UseVisualStyleBackColor = false;
            mbSpoonDown.MouseDown += mbSpoonDown_MouseDown;
            // 
            // mlTitle
            // 
            mlTitle.AutoSize = true;
            mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlTitle.Location = new System.Drawing.Point(15, 15);
            mlTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlTitle.Name = "mlTitle";
            mlTitle.Size = new System.Drawing.Size(282, 33);
            mlTitle.TabIndex = 0;
            mlTitle.Text = "Manual Operations";
            // 
            // mlCutter
            // 
            mlCutter.BackColor = System.Drawing.Color.Transparent;
            mlCutter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutter.Location = new System.Drawing.Point(36, 385);
            mlCutter.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            mlCutter.Name = "mlCutter";
            mlCutter.Size = new System.Drawing.Size(298, 26);
            mlCutter.TabIndex = 65;
            mlCutter.Text = "mlCutter";
            mlCutter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelCuttOff
            // 
            panelCuttOff.Controls.Add(mbSharpening);
            panelCuttOff.Controls.Add(cbCutOff);
            panelCuttOff.LineColor = System.Drawing.Color.LightGray;
            panelCuttOff.LineWidth = 5;
            panelCuttOff.Location = new System.Drawing.Point(36, 420);
            panelCuttOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelCuttOff.Name = "panelCuttOff";
            panelCuttOff.Radius = 10;
            panelCuttOff.Size = new System.Drawing.Size(296, 144);
            panelCuttOff.TabIndex = 64;
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
            mbSharpening.Location = new System.Drawing.Point(155, 15);
            mbSharpening.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbSharpening.Name = "mbSharpening";
            mbSharpening.Size = new System.Drawing.Size(102, 102);
            mbSharpening.StateChangeActivated = true;
            mbSharpening.TabIndex = 48;
            mbSharpening.TabStop = false;
            mbSharpening.UseVisualStyleBackColor = false;
            mbSharpening.Click += MbSharpening_Click;
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
            cbCutOff.Location = new System.Drawing.Point(18, 15);
            cbCutOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCutOff.Name = "cbCutOff";
            cbCutOff.Size = new System.Drawing.Size(102, 102);
            cbCutOff.StateChangeActivated = true;
            cbCutOff.TabIndex = 33;
            cbCutOff.TabStop = false;
            cbCutOff.UseVisualStyleBackColor = false;
            cbCutOff.MouseDown += CbCutOff_MouseDown;
            cbCutOff.MouseUp += CbCutOff_MouseUp;
            // 
            // mlCutterVelocity
            // 
            mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlCutterVelocity.Location = new System.Drawing.Point(410, 385);
            mlCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlCutterVelocity.Name = "mlCutterVelocity";
            mlCutterVelocity.Size = new System.Drawing.Size(298, 26);
            mlCutterVelocity.TabIndex = 66;
            mlCutterVelocity.Text = "mlCutterVelocity";
            mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpbsCutterVelocity
            // 
            mpbsCutterVelocity.BackColor = System.Drawing.Color.Transparent;
            mpbsCutterVelocity.BackgroundImage = (System.Drawing.Image)resources.GetObject("mpbsCutterVelocity.BackgroundImage");
            mpbsCutterVelocity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mpbsCutterVelocity.FlatAppearance.BorderSize = 0;
            mpbsCutterVelocity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mpbsCutterVelocity.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mpbsCutterVelocity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mpbsCutterVelocity.ForeColor = System.Drawing.Color.Transparent;
            mpbsCutterVelocity.Location = new System.Drawing.Point(499, 424);
            mpbsCutterVelocity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mpbsCutterVelocity.MaxValue = 100F;
            mpbsCutterVelocity.MinValue = 0F;
            mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            mpbsCutterVelocity.PropertyName = "";
            mpbsCutterVelocity.Size = new System.Drawing.Size(125, 125);
            mpbsCutterVelocity.TabIndex = 67;
            mpbsCutterVelocity.UseVisualStyleBackColor = false;
            mpbsCutterVelocity.Value = 0F;
            mpbsCutterVelocity.ValueChangedEventEnabled = false;
            // 
            // FormManualOperations
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(mpbsCutterVelocity);
            Controls.Add(mlCutterVelocity);
            Controls.Add(mlCutter);
            Controls.Add(panelCuttOff);
            Controls.Add(mlCradleJog);
            Controls.Add(mpCradleJog);
            Controls.Add(panelBorderBottom);
            Controls.Add(cbStop);
            Controls.Add(mlTitan);
            Controls.Add(mpTitan);
            Controls.Add(mbAutoCentering);
            Controls.Add(mlAutoAlignment);
            Controls.Add(panelBorderTop);
            Controls.Add(mlAlignment);
            Controls.Add(mpAlignment);
            Controls.Add(mlOverturning);
            Controls.Add(mpOverturning);
            Controls.Add(mlSpoon);
            Controls.Add(mpSpoon);
            Controls.Add(mlTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormManualOperations";
            Text = "ManualOperations";
            Load += FormManualOperations_Load;
            mpCradleJog.ResumeLayout(false);
            mpTitan.ResumeLayout(false);
            mpAlignment.ResumeLayout(false);
            mpOverturning.ResumeLayout(false);
            mpSpoon.ResumeLayout(false);
            panelCuttOff.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label mlTitle;
        private MachineLabel mlSpoon;
        private MachinePanelEdgeRounded mpSpoon;
        private MachineButton mbSpoonDown;
        private MachineButton mbSpoonUp;
        private MachineLabel mlOverturning;
        private MachinePanelEdgeRounded mpOverturning;
        private MachineButton mbOverturningDown;
        private MachineButton mbOverturningUp;
        private MachineLabel mlAlignment;
        private MachinePanelEdgeRounded mpAlignment;
        private MachineButton mbAlignmentOperatorSide;
        private MachineButton mbAlignmentMotorSide;
        private System.Windows.Forms.Panel panelBorderTop;
        private MachineButton mbAutoCentering;
        private MachineLabel mlAutoAlignment;
        private MachineButton cbStop;
        private MachineLabel mlTitan;
        private MachinePanelEdgeRounded mpTitan;
        private MachineButton mbTitanDown;
        private MachineButton mbTitanUp;
        private System.Windows.Forms.Panel panelBorderBottom;
        private MachineLabel mlCradleJog;
        private MachinePanelEdgeRounded mpCradleJog;
        private MachineButton cbCradleJogCW;
        private MachineButton cbCradleJogACW;
        private MachineLabel mlCutter;
        private MachinePanelEdgeRounded panelCuttOff;
        private MachineButton mbSharpening;
        private MachineButton cbCutOff;
        private MachineLabel mlCutterVelocity;
        private MachineButtonSlider mpbsCutterVelocity;
    }
}