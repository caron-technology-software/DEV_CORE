namespace Caron.Cradle.UI
{
    partial class FormMachineEnduranceLimits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMachineEnduranceLimits));
            this.panelForm = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.mbReturn = new Machine.UI.Controls.MachineButton();
            this.mbStatistics = new Machine.UI.Controls.MachineButton();
            this.mlStatistics = new System.Windows.Forms.Label();
            this.mbCutter = new Machine.UI.Controls.MachineButton();
            this.mlCutter = new System.Windows.Forms.Label();
            this.mbWorkingHours = new Machine.UI.Controls.MachineButton();
            this.mbDigitalOutputs = new Machine.UI.Controls.MachineButton();
            this.mlDigitalOutputs = new System.Windows.Forms.Label();
            this.mlWorkingHours = new System.Windows.Forms.Label();
            this.mlDigitalInputs = new System.Windows.Forms.Label();
            this.mbDigitalInputs = new Machine.UI.Controls.MachineButton();
            this.listboxDigitalInputs = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.listboxDigitalOutputs = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.listboxCutter = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.listboxStatistics = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.listboxWorkingHours = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.panelForm.SuspendLayout();
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
            this.labelTitle.Size = new System.Drawing.Size(506, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Machine Endurance Setting Values";
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
            this.mbReturn.Click += new System.EventHandler(this.cbReturn_Click);
            // 
            // mbStatistics
            // 
            this.mbStatistics.Active = false;
            this.mbStatistics.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbStatistics.ActiveBackgroundImage")));
            this.mbStatistics.BackColor = System.Drawing.Color.Transparent;
            this.mbStatistics.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbStatistics.BackgroundImage")));
            this.mbStatistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbStatistics.ButtonSize = 64;
            this.mbStatistics.FlatAppearance.BorderSize = 0;
            this.mbStatistics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbStatistics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbStatistics.ForeColor = System.Drawing.Color.Transparent;
            this.mbStatistics.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbStatistics.InactiveBackgroundImage")));
            this.mbStatistics.Location = new System.Drawing.Point(41, 456);
            this.mbStatistics.Name = "mbStatistics";
            this.mbStatistics.Size = new System.Drawing.Size(64, 64);
            this.mbStatistics.StateChangeActivated = false;
            this.mbStatistics.TabIndex = 35;
            this.mbStatistics.TabStop = false;
            this.mbStatistics.UseVisualStyleBackColor = false;
            this.mbStatistics.Click += new System.EventHandler(this.mbStatistics_Click);
            // 
            // mlStatistics
            // 
            this.mlStatistics.AutoSize = true;
            this.mlStatistics.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlStatistics.Location = new System.Drawing.Point(114, 482);
            this.mlStatistics.Name = "mlStatistics";
            this.mlStatistics.Size = new System.Drawing.Size(118, 22);
            this.mlStatistics.TabIndex = 36;
            this.mlStatistics.Text = "mlStatistics";
            // 
            // mbCutter
            // 
            this.mbCutter.Active = false;
            this.mbCutter.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbCutter.ActiveBackgroundImage")));
            this.mbCutter.BackColor = System.Drawing.Color.Transparent;
            this.mbCutter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbCutter.BackgroundImage")));
            this.mbCutter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbCutter.ButtonSize = 64;
            this.mbCutter.FlatAppearance.BorderSize = 0;
            this.mbCutter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbCutter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbCutter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbCutter.ForeColor = System.Drawing.Color.Transparent;
            this.mbCutter.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbCutter.InactiveBackgroundImage")));
            this.mbCutter.Location = new System.Drawing.Point(41, 384);
            this.mbCutter.Name = "mbCutter";
            this.mbCutter.Size = new System.Drawing.Size(64, 64);
            this.mbCutter.StateChangeActivated = false;
            this.mbCutter.TabIndex = 33;
            this.mbCutter.TabStop = false;
            this.mbCutter.UseVisualStyleBackColor = false;
            this.mbCutter.Click += new System.EventHandler(this.mbCutter_Click);
            // 
            // mlCutter
            // 
            this.mlCutter.AutoSize = true;
            this.mlCutter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlCutter.Location = new System.Drawing.Point(114, 410);
            this.mlCutter.Name = "mlCutter";
            this.mlCutter.Size = new System.Drawing.Size(90, 22);
            this.mlCutter.TabIndex = 34;
            this.mlCutter.Text = "mlCutter";
            // 
            // mbWorkingHours
            // 
            this.mbWorkingHours.Active = false;
            this.mbWorkingHours.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbWorkingHours.ActiveBackgroundImage")));
            this.mbWorkingHours.BackColor = System.Drawing.Color.Transparent;
            this.mbWorkingHours.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbWorkingHours.BackgroundImage")));
            this.mbWorkingHours.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbWorkingHours.ButtonSize = 64;
            this.mbWorkingHours.FlatAppearance.BorderSize = 0;
            this.mbWorkingHours.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbWorkingHours.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbWorkingHours.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbWorkingHours.ForeColor = System.Drawing.Color.Transparent;
            this.mbWorkingHours.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbWorkingHours.InactiveBackgroundImage")));
            this.mbWorkingHours.Location = new System.Drawing.Point(41, 312);
            this.mbWorkingHours.Name = "mbWorkingHours";
            this.mbWorkingHours.Size = new System.Drawing.Size(64, 64);
            this.mbWorkingHours.StateChangeActivated = false;
            this.mbWorkingHours.TabIndex = 31;
            this.mbWorkingHours.TabStop = false;
            this.mbWorkingHours.UseVisualStyleBackColor = false;
            this.mbWorkingHours.Click += new System.EventHandler(this.mbWorkingHours_Click);
            // 
            // mbDigitalOutputs
            // 
            this.mbDigitalOutputs.Active = false;
            this.mbDigitalOutputs.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalOutputs.ActiveBackgroundImage")));
            this.mbDigitalOutputs.BackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDigitalOutputs.BackgroundImage")));
            this.mbDigitalOutputs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDigitalOutputs.ButtonSize = 64;
            this.mbDigitalOutputs.FlatAppearance.BorderSize = 0;
            this.mbDigitalOutputs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDigitalOutputs.ForeColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalOutputs.InactiveBackgroundImage")));
            this.mbDigitalOutputs.Location = new System.Drawing.Point(41, 168);
            this.mbDigitalOutputs.Name = "mbDigitalOutputs";
            this.mbDigitalOutputs.Size = new System.Drawing.Size(64, 64);
            this.mbDigitalOutputs.StateChangeActivated = false;
            this.mbDigitalOutputs.TabIndex = 27;
            this.mbDigitalOutputs.TabStop = false;
            this.mbDigitalOutputs.UseVisualStyleBackColor = false;
            this.mbDigitalOutputs.Click += new System.EventHandler(this.mbDigitalOutputs_Click);
            // 
            // mlDigitalOutputs
            // 
            this.mlDigitalOutputs.AutoSize = true;
            this.mlDigitalOutputs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDigitalOutputs.Location = new System.Drawing.Point(114, 194);
            this.mlDigitalOutputs.Name = "mlDigitalOutputs";
            this.mlDigitalOutputs.Size = new System.Drawing.Size(164, 22);
            this.mlDigitalOutputs.TabIndex = 28;
            this.mlDigitalOutputs.Text = "mlDigitalOutputs";
            // 
            // mlWorkingHours
            // 
            this.mlWorkingHours.AutoSize = true;
            this.mlWorkingHours.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingHours.Location = new System.Drawing.Point(114, 338);
            this.mlWorkingHours.Name = "mlWorkingHours";
            this.mlWorkingHours.Size = new System.Drawing.Size(162, 22);
            this.mlWorkingHours.TabIndex = 32;
            this.mlWorkingHours.Text = "mlWorkingHours";
            // 
            // mlDigitalInputs
            // 
            this.mlDigitalInputs.AutoSize = true;
            this.mlDigitalInputs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDigitalInputs.Location = new System.Drawing.Point(114, 266);
            this.mlDigitalInputs.Name = "mlDigitalInputs";
            this.mlDigitalInputs.Size = new System.Drawing.Size(148, 22);
            this.mlDigitalInputs.TabIndex = 30;
            this.mlDigitalInputs.Text = "mlDigitalInputs";
            // 
            // mbDigitalInputs
            // 
            this.mbDigitalInputs.Active = false;
            this.mbDigitalInputs.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalInputs.ActiveBackgroundImage")));
            this.mbDigitalInputs.BackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDigitalInputs.BackgroundImage")));
            this.mbDigitalInputs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDigitalInputs.ButtonSize = 64;
            this.mbDigitalInputs.FlatAppearance.BorderSize = 0;
            this.mbDigitalInputs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDigitalInputs.ForeColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalInputs.InactiveBackgroundImage")));
            this.mbDigitalInputs.Location = new System.Drawing.Point(41, 240);
            this.mbDigitalInputs.Name = "mbDigitalInputs";
            this.mbDigitalInputs.Size = new System.Drawing.Size(64, 64);
            this.mbDigitalInputs.StateChangeActivated = false;
            this.mbDigitalInputs.TabIndex = 29;
            this.mbDigitalInputs.TabStop = false;
            this.mbDigitalInputs.UseVisualStyleBackColor = false;
            this.mbDigitalInputs.Click += new System.EventHandler(this.mbDigitalInputs_Click);
            // 
            // listboxDigitalInputs
            // 
            this.listboxDigitalInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxDigitalInputs.Location = new System.Drawing.Point(360, 239);
            this.listboxDigitalInputs.Name = "listboxDigitalInputs";
            this.listboxDigitalInputs.Size = new System.Drawing.Size(543, 62);
            this.listboxDigitalInputs.TabIndex = 22;
            // 
            // listboxDigitalOutputs
            // 
            this.listboxDigitalOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxDigitalOutputs.Location = new System.Drawing.Point(360, 168);
            this.listboxDigitalOutputs.Name = "listboxDigitalOutputs";
            this.listboxDigitalOutputs.Size = new System.Drawing.Size(543, 62);
            this.listboxDigitalOutputs.TabIndex = 20;
            // 
            // listboxCutter
            // 
            this.listboxCutter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxCutter.Location = new System.Drawing.Point(360, 383);
            this.listboxCutter.Name = "listboxCutter";
            this.listboxCutter.Size = new System.Drawing.Size(543, 62);
            this.listboxCutter.TabIndex = 24;
            // 
            // listboxStatistics
            // 
            this.listboxStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxStatistics.Location = new System.Drawing.Point(360, 454);
            this.listboxStatistics.Name = "listboxStatistics";
            this.listboxStatistics.Size = new System.Drawing.Size(543, 62);
            this.listboxStatistics.TabIndex = 25;
            // 
            // listboxWorkingHours
            // 
            this.listboxWorkingHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxWorkingHours.Location = new System.Drawing.Point(360, 310);
            this.listboxWorkingHours.Name = "listboxWorkingHours";
            this.listboxWorkingHours.Size = new System.Drawing.Size(543, 64);
            this.listboxWorkingHours.TabIndex = 26;
            // 
            // FormMachineEnduranceLimits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 720);
            this.Controls.Add(this.mbStatistics);
            this.Controls.Add(this.listboxWorkingHours);
            this.Controls.Add(this.mlStatistics);
            this.Controls.Add(this.listboxStatistics);
            this.Controls.Add(this.mbCutter);
            this.Controls.Add(this.listboxCutter);
            this.Controls.Add(this.mlCutter);
            this.Controls.Add(this.mbWorkingHours);
            this.Controls.Add(this.listboxDigitalInputs);
            this.Controls.Add(this.mbDigitalOutputs);
            this.Controls.Add(this.mlDigitalOutputs);
            this.Controls.Add(this.listboxDigitalOutputs);
            this.Controls.Add(this.mlWorkingHours);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.mlDigitalInputs);
            this.Controls.Add(this.mbDigitalInputs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMachineEnduranceLimits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMachineEnduranceSettingValues";
            this.Load += new System.EventHandler(this.FormMachineEnduranceSettings_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private Machine.UI.Controls.MachineButton mbReturn;
        private Machine.UI.Controls.MachineButton mbWorkingHours;
        private Machine.UI.Controls.MachineButton mbDigitalOutputs;
        private System.Windows.Forms.Label mlDigitalOutputs;
        private System.Windows.Forms.Label mlWorkingHours;
        private System.Windows.Forms.Label mlDigitalInputs;
        private Machine.UI.Controls.MachineButton mbDigitalInputs;

        private Machine.UI.Controls.MachineEditableItemsListbox listboxDigitalInputs;
        private Machine.UI.Controls.MachineEditableItemsListbox listboxDigitalOutputs;
        private Machine.UI.Controls.MachineButton mbCutter;
        private System.Windows.Forms.Label mlCutter;
        private Machine.UI.Controls.MachineButton mbStatistics;
        private System.Windows.Forms.Label mlStatistics;
        private Machine.UI.Controls.MachineEditableItemsListbox listboxCutter;
        private Machine.UI.Controls.MachineEditableItemsListbox listboxStatistics;
        private Machine.UI.Controls.MachineEditableItemsListbox listboxWorkingHours;
    }
}