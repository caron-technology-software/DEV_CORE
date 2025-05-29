namespace Caron.Cradle.UI
{
    partial class FormAnalogInputsCalibration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnalogInputsCalibration));
            this.panelForm = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.mbReturn = new Machine.UI.Controls.MachineButton();
            this.mlDancer = new System.Windows.Forms.Label();
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.panelDancer = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlInputValue = new Machine.UI.Controls.MachineLabel();
            this.mlApply = new Machine.UI.Controls.MachineLabel();
            this.mlReset = new Machine.UI.Controls.MachineLabel();
            this.mlMaxValue = new Machine.UI.Controls.MachineLabel();
            this.mlMinValue = new Machine.UI.Controls.MachineLabel();
            this.mbDancer = new Machine.UI.Controls.MachineButton();
            this.mlPrecInputValue = new Machine.UI.Controls.MachineLabel();
            this.panelForm.SuspendLayout();
            this.panelDancer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.labelTitle);
            this.panelForm.Controls.Add(this.mbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1010, 120);
            this.panelForm.TabIndex = 10;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(138, 45);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(374, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Analog Inputs Calibration";
            // 
            // mbReturn
            // 
            this.mbReturn.Active = false;
            this.mbReturn.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbReturn.ActiveBackgroundImage")));
            this.mbReturn.BackColor = System.Drawing.Color.Transparent;
            this.mbReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbReturn.BackgroundImage")));
            this.mbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbReturn.ButtonSize = 102;
            this.mbReturn.FlatAppearance.BorderSize = 0;
            this.mbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbReturn.ForeColor = System.Drawing.Color.Transparent;
            this.mbReturn.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbReturn.InactiveBackgroundImage")));
            this.mbReturn.Location = new System.Drawing.Point(12, 9);
            this.mbReturn.Name = "mbReturn";
            this.mbReturn.Size = new System.Drawing.Size(102, 102);
            this.mbReturn.StateChangeActivated = true;
            this.mbReturn.TabIndex = 7;
            this.mbReturn.TabStop = false;
            this.mbReturn.UseVisualStyleBackColor = false;
            this.mbReturn.Click += new System.EventHandler(this.mbReturn_Click);
            // 
            // mlDancer
            // 
            this.mlDancer.AutoSize = true;
            this.mlDancer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDancer.Location = new System.Drawing.Point(126, 185);
            this.mlDancer.Name = "mlDancer";
            this.mlDancer.Size = new System.Drawing.Size(98, 22);
            this.mlDancer.TabIndex = 26;
            this.mlDancer.Text = "mlDancer";
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.BackColor = System.Drawing.SystemColors.Control;
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 3;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(37, 150);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 5;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(295, 91);
            this.machinePanelEdgeRounded.TabIndex = 30;
            // 
            // panelDancer
            // 
            this.panelDancer.Controls.Add(this.mlPrecInputValue);
            this.panelDancer.Controls.Add(this.mlInputValue);
            this.panelDancer.Controls.Add(this.mlApply);
            this.panelDancer.Controls.Add(this.mlReset);
            this.panelDancer.Controls.Add(this.mlMaxValue);
            this.panelDancer.Controls.Add(this.mlMinValue);
            this.panelDancer.LineColor = System.Drawing.Color.LightGray;
            this.panelDancer.LineWidth = 4;
            this.panelDancer.Location = new System.Drawing.Point(446, 150);
            this.panelDancer.Name = "panelDancer";
            this.panelDancer.Radius = 4;
            this.panelDancer.Size = new System.Drawing.Size(469, 264);
            this.panelDancer.TabIndex = 18;
            // 
            // mlInputValue
            // 
            this.mlInputValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlInputValue.ForeColor = System.Drawing.Color.Red;
            this.mlInputValue.Location = new System.Drawing.Point(19, 10);
            this.mlInputValue.Name = "mlInputValue";
            this.mlInputValue.Size = new System.Drawing.Size(432, 41);
            this.mlInputValue.TabIndex = 6;
            this.mlInputValue.Text = "mlInputValue";
            this.mlInputValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlApply
            // 
            this.mlApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mlApply.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlApply.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlApply.Location = new System.Drawing.Point(92, 186);
            this.mlApply.Name = "mlApply";
            this.mlApply.Size = new System.Drawing.Size(130, 38);
            this.mlApply.TabIndex = 5;
            this.mlApply.Text = "mlApply";
            this.mlApply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlApply.Click += new System.EventHandler(this.mlApply_Click);
            // 
            // mlReset
            // 
            this.mlReset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mlReset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mlReset.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlReset.Location = new System.Drawing.Point(228, 186);
            this.mlReset.Name = "mlReset";
            this.mlReset.Size = new System.Drawing.Size(130, 38);
            this.mlReset.TabIndex = 3;
            this.mlReset.Text = "mlReset";
            this.mlReset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlReset.Click += new System.EventHandler(this.mlReset_Click);
            // 
            // mlMaxValue
            // 
            this.mlMaxValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlMaxValue.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.mlMaxValue.Location = new System.Drawing.Point(20, 136);
            this.mlMaxValue.Name = "mlMaxValue";
            this.mlMaxValue.Size = new System.Drawing.Size(431, 26);
            this.mlMaxValue.TabIndex = 1;
            this.mlMaxValue.Text = "mlMaxValue";
            // 
            // mlMinValue
            // 
            this.mlMinValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlMinValue.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.mlMinValue.Location = new System.Drawing.Point(20, 110);
            this.mlMinValue.Name = "mlMinValue";
            this.mlMinValue.Size = new System.Drawing.Size(431, 26);
            this.mlMinValue.TabIndex = 0;
            this.mlMinValue.Text = "mlMinValue";
            // 
            // mbDancer
            // 
            this.mbDancer.Active = false;
            this.mbDancer.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDancer.ActiveBackgroundImage")));
            this.mbDancer.BackColor = System.Drawing.Color.Transparent;
            this.mbDancer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDancer.BackgroundImage")));
            this.mbDancer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDancer.ButtonSize = 64;
            this.mbDancer.FlatAppearance.BorderSize = 0;
            this.mbDancer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDancer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDancer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDancer.ForeColor = System.Drawing.Color.Transparent;
            this.mbDancer.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDancer.InactiveBackgroundImage")));
            this.mbDancer.Location = new System.Drawing.Point(51, 164);
            this.mbDancer.Name = "mbDancer";
            this.mbDancer.Size = new System.Drawing.Size(64, 64);
            this.mbDancer.StateChangeActivated = true;
            this.mbDancer.TabIndex = 22;
            this.mbDancer.TabStop = false;
            this.mbDancer.UseVisualStyleBackColor = false;
            // 
            // mlPrecInputValue
            // 
            this.mlPrecInputValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlPrecInputValue.ForeColor = System.Drawing.Color.DimGray;
            this.mlPrecInputValue.Location = new System.Drawing.Point(19, 51);
            this.mlPrecInputValue.Name = "mlPrecInputValue";
            this.mlPrecInputValue.Size = new System.Drawing.Size(432, 41);
            this.mlPrecInputValue.TabIndex = 7;
            this.mlPrecInputValue.Text = "mlPrecInputValue";
            this.mlPrecInputValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAnalogInputsCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 720);
            this.Controls.Add(this.mbDancer);
            this.Controls.Add(this.mlDancer);
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.Controls.Add(this.panelDancer);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAnalogInputsCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMachineCalibration";
            this.Load += new System.EventHandler(this.FormMachineCalibration_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.panelDancer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private Machine.UI.Controls.MachineButton mbReturn;
        private Machine.UI.Controls.MachinePanelEdgeRounded panelDancer;
        private Machine.UI.Controls.MachineLabel mlApply;
        private Machine.UI.Controls.MachineLabel mlReset;
        private Machine.UI.Controls.MachineLabel mlMinValue;
        private Machine.UI.Controls.MachineLabel mlInputValue;
        private Machine.UI.Controls.MachineButton mbDancer;
        private System.Windows.Forms.Label mlDancer;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
        private Machine.UI.Controls.MachineLabel mlMaxValue;
        private Machine.UI.Controls.MachineLabel mlPrecInputValue;
    }
}