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
            this.mlCradleJog = new Machine.UI.Controls.MachineLabel();
            this.mpCradleJog = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.cbCradleJogCW = new Machine.UI.Controls.MachineButton();
            this.cbCradleJogACW = new Machine.UI.Controls.MachineButton();
            this.panelBorderBottom = new System.Windows.Forms.Panel();
            this.cbStop = new Machine.UI.Controls.MachineButton();
            this.mlTitan = new Machine.UI.Controls.MachineLabel();
            this.mpTitan = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbTitanDown = new Machine.UI.Controls.MachineButton();
            this.mbTitanUp = new Machine.UI.Controls.MachineButton();
            this.mbAutoCentering = new Machine.UI.Controls.MachineButton();
            this.mlAutoAlignment = new Machine.UI.Controls.MachineLabel();
            this.panelBorderTop = new System.Windows.Forms.Panel();
            this.mlAlignment = new Machine.UI.Controls.MachineLabel();
            this.mpAlignment = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbAlignmentMotorSide = new Machine.UI.Controls.MachineButton();
            this.mbAlignmentOperatorSide = new Machine.UI.Controls.MachineButton();
            this.mlOverturning = new Machine.UI.Controls.MachineLabel();
            this.mpOverturning = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbOverturningUp = new Machine.UI.Controls.MachineButton();
            this.mbOverturningDown = new Machine.UI.Controls.MachineButton();
            this.mlSpoon = new Machine.UI.Controls.MachineLabel();
            this.mpSpoon = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbSpoonUp = new Machine.UI.Controls.MachineButton();
            this.mbSpoonDown = new Machine.UI.Controls.MachineButton();
            this.mlTitle = new System.Windows.Forms.Label();
            this.mlCutter = new Machine.UI.Controls.MachineLabel();
            this.panelCuttOff = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mbSharpening = new Machine.UI.Controls.MachineButton();
            this.cbCutOff = new Machine.UI.Controls.MachineButton();
            this.mlCutterVelocity = new Machine.UI.Controls.MachineLabel();
            this.mpbsCutterVelocity = new Machine.UI.Controls.MachineButtonSlider();
            this.mpCradleJog.SuspendLayout();
            this.mpTitan.SuspendLayout();
            this.mpAlignment.SuspendLayout();
            this.mpOverturning.SuspendLayout();
            this.mpSpoon.SuspendLayout();
            this.panelCuttOff.SuspendLayout();
            this.SuspendLayout();
            // 
            // mlCradleJog
            // 
            this.mlCradleJog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCradleJog.Location = new System.Drawing.Point(29, 57);
            this.mlCradleJog.Name = "mlCradleJog";
            this.mlCradleJog.Size = new System.Drawing.Size(205, 75);
            this.mlCradleJog.TabIndex = 63;
            this.mlCradleJog.Text = "mlCradleJog";
            this.mlCradleJog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpCradleJog
            // 
            this.mpCradleJog.Controls.Add(this.cbCradleJogCW);
            this.mpCradleJog.Controls.Add(this.cbCradleJogACW);
            this.mpCradleJog.LineColor = System.Drawing.Color.LightGray;
            this.mpCradleJog.LineWidth = 5;
            this.mpCradleJog.Location = new System.Drawing.Point(60, 136);
            this.mpCradleJog.Name = "mpCradleJog";
            this.mpCradleJog.Radius = 10;
            this.mpCradleJog.Size = new System.Drawing.Size(135, 245);
            this.mpCradleJog.TabIndex = 62;
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
            this.cbCradleJogCW.Location = new System.Drawing.Point(16, 12);
            this.cbCradleJogCW.Name = "cbCradleJogCW";
            this.cbCradleJogCW.Size = new System.Drawing.Size(102, 102);
            this.cbCradleJogCW.StateChangeActivated = true;
            this.cbCradleJogCW.TabIndex = 1;
            this.cbCradleJogCW.TabStop = false;
            this.cbCradleJogCW.UseVisualStyleBackColor = false;
            this.cbCradleJogCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogCW_MouseDown);
            this.cbCradleJogCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogCW_MouseUp);
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
            this.cbCradleJogACW.Location = new System.Drawing.Point(16, 129);
            this.cbCradleJogACW.Name = "cbCradleJogACW";
            this.cbCradleJogACW.Size = new System.Drawing.Size(102, 102);
            this.cbCradleJogACW.StateChangeActivated = true;
            this.cbCradleJogACW.TabIndex = 0;
            this.cbCradleJogACW.TabStop = false;
            this.cbCradleJogACW.UseVisualStyleBackColor = false;
            this.cbCradleJogACW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogACW_MouseDown);
            this.cbCradleJogACW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCradleJogACW_MouseUp);
            // 
            // panelBorderBottom
            // 
            this.panelBorderBottom.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelBorderBottom.Location = new System.Drawing.Point(5, 710);
            this.panelBorderBottom.Name = "panelBorderBottom";
            this.panelBorderBottom.Size = new System.Drawing.Size(1135, 4);
            this.panelBorderBottom.TabIndex = 56;
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
            this.cbStop.Location = new System.Drawing.Point(503, 580);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(125, 125);
            this.cbStop.StateChangeActivated = true;
            this.cbStop.TabIndex = 54;
            this.cbStop.TabStop = false;
            this.cbStop.UseVisualStyleBackColor = false;
            this.cbStop.Click += new System.EventHandler(this.CbStop_Click);
            // 
            // mlTitan
            // 
            this.mlTitan.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTitan.Location = new System.Drawing.Point(905, 57);
            this.mlTitan.Name = "mlTitan";
            this.mlTitan.Size = new System.Drawing.Size(205, 75);
            this.mlTitan.TabIndex = 61;
            this.mlTitan.Text = "mlTitan";
            this.mlTitan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpTitan
            // 
            this.mpTitan.Controls.Add(this.mbTitanDown);
            this.mpTitan.Controls.Add(this.mbTitanUp);
            this.mpTitan.LineColor = System.Drawing.Color.LightGray;
            this.mpTitan.LineWidth = 5;
            this.mpTitan.Location = new System.Drawing.Point(936, 136);
            this.mpTitan.Name = "mpTitan";
            this.mpTitan.Radius = 10;
            this.mpTitan.Size = new System.Drawing.Size(135, 245);
            this.mpTitan.TabIndex = 60;
            // 
            // mbTitanDown
            // 
            this.mbTitanDown.Active = false;
            this.mbTitanDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbTitanDown.ActiveBackgroundImage")));
            this.mbTitanDown.BackColor = System.Drawing.Color.Transparent;
            this.mbTitanDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbTitanDown.BackgroundImage")));
            this.mbTitanDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbTitanDown.ButtonSize = 102;
            this.mbTitanDown.FlatAppearance.BorderSize = 0;
            this.mbTitanDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbTitanDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbTitanDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbTitanDown.ForeColor = System.Drawing.Color.Transparent;
            this.mbTitanDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbTitanDown.InactiveBackgroundImage")));
            this.mbTitanDown.Location = new System.Drawing.Point(16, 129);
            this.mbTitanDown.Name = "mbTitanDown";
            this.mbTitanDown.Size = new System.Drawing.Size(102, 102);
            this.mbTitanDown.StateChangeActivated = true;
            this.mbTitanDown.TabIndex = 1;
            this.mbTitanDown.TabStop = false;
            this.mbTitanDown.UseVisualStyleBackColor = false;
            this.mbTitanDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbTitanDown_MouseDown);
            this.mbTitanDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbTitanDown_MouseUp);
            // 
            // mbTitanUp
            // 
            this.mbTitanUp.Active = false;
            this.mbTitanUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbTitanUp.ActiveBackgroundImage")));
            this.mbTitanUp.BackColor = System.Drawing.Color.Transparent;
            this.mbTitanUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbTitanUp.BackgroundImage")));
            this.mbTitanUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbTitanUp.ButtonSize = 102;
            this.mbTitanUp.FlatAppearance.BorderSize = 0;
            this.mbTitanUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbTitanUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbTitanUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbTitanUp.ForeColor = System.Drawing.Color.Transparent;
            this.mbTitanUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbTitanUp.InactiveBackgroundImage")));
            this.mbTitanUp.Location = new System.Drawing.Point(16, 12);
            this.mbTitanUp.Name = "mbTitanUp";
            this.mbTitanUp.Size = new System.Drawing.Size(102, 102);
            this.mbTitanUp.StateChangeActivated = true;
            this.mbTitanUp.TabIndex = 0;
            this.mbTitanUp.TabStop = false;
            this.mbTitanUp.UseVisualStyleBackColor = false;
            this.mbTitanUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbTitanUp_MouseDown);
            this.mbTitanUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbTitanUp_MouseUp);
            // 
            // mbAutoCentering
            // 
            this.mbAutoCentering.Active = false;
            this.mbAutoCentering.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAutoCentering.ActiveBackgroundImage")));
            this.mbAutoCentering.BackColor = System.Drawing.Color.Transparent;
            this.mbAutoCentering.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbAutoCentering.BackgroundImage")));
            this.mbAutoCentering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbAutoCentering.ButtonSize = 112;
            this.mbAutoCentering.FlatAppearance.BorderSize = 0;
            this.mbAutoCentering.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbAutoCentering.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbAutoCentering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbAutoCentering.ForeColor = System.Drawing.Color.Transparent;
            this.mbAutoCentering.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAutoCentering.InactiveBackgroundImage")));
            this.mbAutoCentering.Location = new System.Drawing.Point(839, 445);
            this.mbAutoCentering.Name = "mbAutoCentering";
            this.mbAutoCentering.Size = new System.Drawing.Size(112, 112);
            this.mbAutoCentering.StateChangeActivated = true;
            this.mbAutoCentering.TabIndex = 58;
            this.mbAutoCentering.TabStop = false;
            this.mbAutoCentering.UseVisualStyleBackColor = false;
            this.mbAutoCentering.Click += new System.EventHandler(this.mbAutoAlignment_Click);
            // 
            // mlAutoAlignment
            // 
            this.mlAutoAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlAutoAlignment.Location = new System.Drawing.Point(765, 396);
            this.mlAutoAlignment.Name = "mlAutoAlignment";
            this.mlAutoAlignment.Size = new System.Drawing.Size(255, 31);
            this.mlAutoAlignment.TabIndex = 57;
            this.mlAutoAlignment.Text = "mlAutoAlignment";
            this.mlAutoAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelBorderTop
            // 
            this.panelBorderTop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelBorderTop.Location = new System.Drawing.Point(5, 568);
            this.panelBorderTop.Name = "panelBorderTop";
            this.panelBorderTop.Size = new System.Drawing.Size(1135, 4);
            this.panelBorderTop.TabIndex = 55;
            // 
            // mlAlignment
            // 
            this.mlAlignment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlAlignment.Location = new System.Drawing.Point(686, 57);
            this.mlAlignment.Name = "mlAlignment";
            this.mlAlignment.Size = new System.Drawing.Size(205, 75);
            this.mlAlignment.TabIndex = 54;
            this.mlAlignment.Text = "mlAlignment";
            this.mlAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpAlignment
            // 
            this.mpAlignment.Controls.Add(this.mbAlignmentMotorSide);
            this.mpAlignment.Controls.Add(this.mbAlignmentOperatorSide);
            this.mpAlignment.LineColor = System.Drawing.Color.LightGray;
            this.mpAlignment.LineWidth = 5;
            this.mpAlignment.Location = new System.Drawing.Point(717, 136);
            this.mpAlignment.Name = "mpAlignment";
            this.mpAlignment.Radius = 10;
            this.mpAlignment.Size = new System.Drawing.Size(135, 245);
            this.mpAlignment.TabIndex = 53;
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
            this.mbAlignmentMotorSide.Location = new System.Drawing.Point(15, 12);
            this.mbAlignmentMotorSide.Name = "mbAlignmentMotorSide";
            this.mbAlignmentMotorSide.Size = new System.Drawing.Size(102, 102);
            this.mbAlignmentMotorSide.StateChangeActivated = true;
            this.mbAlignmentMotorSide.TabIndex = 0;
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
            this.mbAlignmentOperatorSide.Location = new System.Drawing.Point(15, 129);
            this.mbAlignmentOperatorSide.Name = "mbAlignmentOperatorSide";
            this.mbAlignmentOperatorSide.Size = new System.Drawing.Size(102, 102);
            this.mbAlignmentOperatorSide.StateChangeActivated = true;
            this.mbAlignmentOperatorSide.TabIndex = 1;
            this.mbAlignmentOperatorSide.TabStop = false;
            this.mbAlignmentOperatorSide.UseVisualStyleBackColor = false;
            this.mbAlignmentOperatorSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentOperatorSide_MouseDown);
            this.mbAlignmentOperatorSide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbAlignmentOperatorSide_MouseUp);
            // 
            // mlOverturning
            // 
            this.mlOverturning.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlOverturning.Location = new System.Drawing.Point(467, 57);
            this.mlOverturning.Name = "mlOverturning";
            this.mlOverturning.Size = new System.Drawing.Size(205, 75);
            this.mlOverturning.TabIndex = 51;
            this.mlOverturning.Text = "mlOverturning";
            this.mlOverturning.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpOverturning
            // 
            this.mpOverturning.Controls.Add(this.mbOverturningUp);
            this.mpOverturning.Controls.Add(this.mbOverturningDown);
            this.mpOverturning.LineColor = System.Drawing.Color.LightGray;
            this.mpOverturning.LineWidth = 5;
            this.mpOverturning.Location = new System.Drawing.Point(496, 136);
            this.mpOverturning.Name = "mpOverturning";
            this.mpOverturning.Radius = 10;
            this.mpOverturning.Size = new System.Drawing.Size(135, 245);
            this.mpOverturning.TabIndex = 50;
            // 
            // mbOverturningUp
            // 
            this.mbOverturningUp.Active = false;
            this.mbOverturningUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbOverturningUp.ActiveBackgroundImage")));
            this.mbOverturningUp.BackColor = System.Drawing.Color.Transparent;
            this.mbOverturningUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbOverturningUp.BackgroundImage")));
            this.mbOverturningUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbOverturningUp.ButtonSize = 102;
            this.mbOverturningUp.FlatAppearance.BorderSize = 0;
            this.mbOverturningUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbOverturningUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbOverturningUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbOverturningUp.ForeColor = System.Drawing.Color.Transparent;
            this.mbOverturningUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbOverturningUp.InactiveBackgroundImage")));
            this.mbOverturningUp.Location = new System.Drawing.Point(16, 12);
            this.mbOverturningUp.Name = "mbOverturningUp";
            this.mbOverturningUp.Size = new System.Drawing.Size(102, 102);
            this.mbOverturningUp.StateChangeActivated = true;
            this.mbOverturningUp.TabIndex = 0;
            this.mbOverturningUp.TabStop = false;
            this.mbOverturningUp.UseVisualStyleBackColor = false;
            this.mbOverturningUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbOverturningUp_MouseDown);
            this.mbOverturningUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbOverturningUp_MouseUp);
            // 
            // mbOverturningDown
            // 
            this.mbOverturningDown.Active = false;
            this.mbOverturningDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbOverturningDown.ActiveBackgroundImage")));
            this.mbOverturningDown.BackColor = System.Drawing.Color.Transparent;
            this.mbOverturningDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbOverturningDown.BackgroundImage")));
            this.mbOverturningDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbOverturningDown.ButtonSize = 102;
            this.mbOverturningDown.FlatAppearance.BorderSize = 0;
            this.mbOverturningDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbOverturningDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbOverturningDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbOverturningDown.ForeColor = System.Drawing.Color.Transparent;
            this.mbOverturningDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbOverturningDown.InactiveBackgroundImage")));
            this.mbOverturningDown.Location = new System.Drawing.Point(16, 129);
            this.mbOverturningDown.Name = "mbOverturningDown";
            this.mbOverturningDown.Size = new System.Drawing.Size(102, 102);
            this.mbOverturningDown.StateChangeActivated = true;
            this.mbOverturningDown.TabIndex = 1;
            this.mbOverturningDown.TabStop = false;
            this.mbOverturningDown.UseVisualStyleBackColor = false;
            this.mbOverturningDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbOverturningDown_MouseDown);
            this.mbOverturningDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbOverturningDown_MouseUp);
            // 
            // mlSpoon
            // 
            this.mlSpoon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSpoon.Location = new System.Drawing.Point(248, 57);
            this.mlSpoon.Name = "mlSpoon";
            this.mlSpoon.Size = new System.Drawing.Size(205, 75);
            this.mlSpoon.TabIndex = 49;
            this.mlSpoon.Text = "mlSpoon";
            this.mlSpoon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpSpoon
            // 
            this.mpSpoon.Controls.Add(this.mbSpoonUp);
            this.mpSpoon.Controls.Add(this.mbSpoonDown);
            this.mpSpoon.LineColor = System.Drawing.Color.LightGray;
            this.mpSpoon.LineWidth = 5;
            this.mpSpoon.Location = new System.Drawing.Point(279, 136);
            this.mpSpoon.Name = "mpSpoon";
            this.mpSpoon.Radius = 10;
            this.mpSpoon.Size = new System.Drawing.Size(135, 245);
            this.mpSpoon.TabIndex = 48;
            // 
            // mbSpoonUp
            // 
            this.mbSpoonUp.Active = false;
            this.mbSpoonUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSpoonUp.ActiveBackgroundImage")));
            this.mbSpoonUp.BackColor = System.Drawing.Color.Transparent;
            this.mbSpoonUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSpoonUp.BackgroundImage")));
            this.mbSpoonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSpoonUp.ButtonSize = 102;
            this.mbSpoonUp.FlatAppearance.BorderSize = 0;
            this.mbSpoonUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSpoonUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSpoonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSpoonUp.ForeColor = System.Drawing.Color.Transparent;
            this.mbSpoonUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSpoonUp.InactiveBackgroundImage")));
            this.mbSpoonUp.Location = new System.Drawing.Point(13, 12);
            this.mbSpoonUp.Name = "mbSpoonUp";
            this.mbSpoonUp.Size = new System.Drawing.Size(102, 102);
            this.mbSpoonUp.StateChangeActivated = true;
            this.mbSpoonUp.TabIndex = 0;
            this.mbSpoonUp.TabStop = false;
            this.mbSpoonUp.UseVisualStyleBackColor = false;
            this.mbSpoonUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbSpoonUp_MouseDown);
            // 
            // mbSpoonDown
            // 
            this.mbSpoonDown.Active = false;
            this.mbSpoonDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSpoonDown.ActiveBackgroundImage")));
            this.mbSpoonDown.BackColor = System.Drawing.Color.Transparent;
            this.mbSpoonDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSpoonDown.BackgroundImage")));
            this.mbSpoonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSpoonDown.ButtonSize = 102;
            this.mbSpoonDown.FlatAppearance.BorderSize = 0;
            this.mbSpoonDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSpoonDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSpoonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSpoonDown.ForeColor = System.Drawing.Color.Transparent;
            this.mbSpoonDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbSpoonDown.InactiveBackgroundImage")));
            this.mbSpoonDown.Location = new System.Drawing.Point(13, 129);
            this.mbSpoonDown.Name = "mbSpoonDown";
            this.mbSpoonDown.Size = new System.Drawing.Size(102, 102);
            this.mbSpoonDown.StateChangeActivated = true;
            this.mbSpoonDown.TabIndex = 1;
            this.mbSpoonDown.TabStop = false;
            this.mbSpoonDown.UseVisualStyleBackColor = false;
            this.mbSpoonDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbSpoonDown_MouseDown);
            // 
            // mlTitle
            // 
            this.mlTitle.AutoSize = true;
            this.mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTitle.Location = new System.Drawing.Point(13, 13);
            this.mlTitle.Name = "mlTitle";
            this.mlTitle.Size = new System.Drawing.Size(282, 33);
            this.mlTitle.TabIndex = 0;
            this.mlTitle.Text = "Manual Operations";
            // 
            // mlCutter
            // 
            this.mlCutter.BackColor = System.Drawing.Color.Transparent;
            this.mlCutter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutter.Location = new System.Drawing.Point(91, 396);
            this.mlCutter.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlCutter.Name = "mlCutter";
            this.mlCutter.Size = new System.Drawing.Size(255, 31);
            this.mlCutter.TabIndex = 65;
            this.mlCutter.Text = "mlCutter";
            this.mlCutter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelCuttOff
            // 
            this.panelCuttOff.Controls.Add(this.mbSharpening);
            this.panelCuttOff.Controls.Add(this.cbCutOff);
            this.panelCuttOff.LineColor = System.Drawing.Color.LightGray;
            this.panelCuttOff.LineWidth = 5;
            this.panelCuttOff.Location = new System.Drawing.Point(92, 432);
            this.panelCuttOff.Name = "panelCuttOff";
            this.panelCuttOff.Radius = 10;
            this.panelCuttOff.Size = new System.Drawing.Size(254, 125);
            this.panelCuttOff.TabIndex = 64;
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
            this.mbSharpening.Click += new System.EventHandler(this.MbSharpening_Click);
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
            this.cbCutOff.Location = new System.Drawing.Point(15, 13);
            this.cbCutOff.Name = "cbCutOff";
            this.cbCutOff.Size = new System.Drawing.Size(102, 102);
            this.cbCutOff.StateChangeActivated = true;
            this.cbCutOff.TabIndex = 33;
            this.cbCutOff.TabStop = false;
            this.cbCutOff.UseVisualStyleBackColor = false;
            this.cbCutOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbCutOff_MouseDown);
            this.cbCutOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CbCutOff_MouseUp);
            // 
            // mlCutterVelocity
            // 
            this.mlCutterVelocity.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutterVelocity.Location = new System.Drawing.Point(441, 396);
            this.mlCutterVelocity.Name = "mlCutterVelocity";
            this.mlCutterVelocity.Size = new System.Drawing.Size(255, 31);
            this.mlCutterVelocity.TabIndex = 66;
            this.mlCutterVelocity.Text = "mlCutterVelocity";
            this.mlCutterVelocity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // mpbsCutterVelocity
            // 
            this.mpbsCutterVelocity.BackColor = System.Drawing.Color.Transparent;
            this.mpbsCutterVelocity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mpbsCutterVelocity.BackgroundImage")));
            this.mpbsCutterVelocity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mpbsCutterVelocity.FlatAppearance.BorderSize = 0;
            this.mpbsCutterVelocity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mpbsCutterVelocity.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mpbsCutterVelocity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mpbsCutterVelocity.ForeColor = System.Drawing.Color.Transparent;
            this.mpbsCutterVelocity.Location = new System.Drawing.Point(501, 432);
            this.mpbsCutterVelocity.MaxValue = 100F;
            this.mpbsCutterVelocity.MinValue = 0F;
            this.mpbsCutterVelocity.Name = "mpbsCutterVelocity";
            this.mpbsCutterVelocity.PropertyName = "";
            this.mpbsCutterVelocity.Size = new System.Drawing.Size(125, 125);
            this.mpbsCutterVelocity.TabIndex = 67;
            this.mpbsCutterVelocity.UseVisualStyleBackColor = false;
            this.mpbsCutterVelocity.Value = 0F;
            this.mpbsCutterVelocity.ValueChangedEventEnabled = false;
            // 
            // FormManualOperations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 720);
            this.Controls.Add(this.mpbsCutterVelocity);
            this.Controls.Add(this.mlCutterVelocity);
            this.Controls.Add(this.mlCutter);
            this.Controls.Add(this.panelCuttOff);
            this.Controls.Add(this.mlCradleJog);
            this.Controls.Add(this.mpCradleJog);
            this.Controls.Add(this.panelBorderBottom);
            this.Controls.Add(this.cbStop);
            this.Controls.Add(this.mlTitan);
            this.Controls.Add(this.mpTitan);
            this.Controls.Add(this.mbAutoCentering);
            this.Controls.Add(this.mlAutoAlignment);
            this.Controls.Add(this.panelBorderTop);
            this.Controls.Add(this.mlAlignment);
            this.Controls.Add(this.mpAlignment);
            this.Controls.Add(this.mlOverturning);
            this.Controls.Add(this.mpOverturning);
            this.Controls.Add(this.mlSpoon);
            this.Controls.Add(this.mpSpoon);
            this.Controls.Add(this.mlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormManualOperations";
            this.Text = "ManualOperations";
            this.Load += new System.EventHandler(this.FormManualOperations_Load);
            this.mpCradleJog.ResumeLayout(false);
            this.mpTitan.ResumeLayout(false);
            this.mpAlignment.ResumeLayout(false);
            this.mpOverturning.ResumeLayout(false);
            this.mpSpoon.ResumeLayout(false);
            this.panelCuttOff.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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