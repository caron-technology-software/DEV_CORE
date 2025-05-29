using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.cbManualOperations = new Machine.UI.Controls.MachineButton();
            this.cbUserSettings = new Machine.UI.Controls.MachineButton();
            this.mbDashboard = new Machine.UI.Controls.MachineButton();
            this.cbLoadUnload = new Machine.UI.Controls.MachineButton();
            this.cbWorkingsSettings = new Machine.UI.Controls.MachineButton();
            this.SuspendLayout();
            // 
            // cbManualOperations
            // 
            this.cbManualOperations.Active = false;
            this.cbManualOperations.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbManualOperations.ActiveBackgroundImage")));
            this.cbManualOperations.BackColor = System.Drawing.Color.Transparent;
            this.cbManualOperations.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbManualOperations.BackgroundImage")));
            this.cbManualOperations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbManualOperations.ButtonSize = 102;
            this.cbManualOperations.FlatAppearance.BorderSize = 0;
            this.cbManualOperations.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbManualOperations.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbManualOperations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbManualOperations.ForeColor = System.Drawing.Color.Transparent;
            this.cbManualOperations.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbManualOperations.InactiveBackgroundImage")));
            this.cbManualOperations.Location = new System.Drawing.Point(17, 178);
            this.cbManualOperations.Name = "cbManualOperations";
            this.cbManualOperations.Size = new System.Drawing.Size(102, 102);
            this.cbManualOperations.StateChangeActivated = true;
            this.cbManualOperations.TabIndex = 9;
            this.cbManualOperations.TabStop = false;
            this.cbManualOperations.UseVisualStyleBackColor = false;
            this.cbManualOperations.Click += new System.EventHandler(this.cbManualOperations_Click);
            // 
            // cbUserSettings
            // 
            this.cbUserSettings.Active = false;
            this.cbUserSettings.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbUserSettings.ActiveBackgroundImage")));
            this.cbUserSettings.BackColor = System.Drawing.Color.Transparent;
            this.cbUserSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbUserSettings.BackgroundImage")));
            this.cbUserSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbUserSettings.ButtonSize = 102;
            this.cbUserSettings.FlatAppearance.BorderSize = 0;
            this.cbUserSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbUserSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbUserSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUserSettings.ForeColor = System.Drawing.Color.Transparent;
            this.cbUserSettings.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbUserSettings.InactiveBackgroundImage")));
            this.cbUserSettings.Location = new System.Drawing.Point(17, 306);
            this.cbUserSettings.Name = "cbUserSettings";
            this.cbUserSettings.Size = new System.Drawing.Size(102, 102);
            this.cbUserSettings.StateChangeActivated = true;
            this.cbUserSettings.TabIndex = 6;
            this.cbUserSettings.TabStop = false;
            this.cbUserSettings.UseVisualStyleBackColor = false;
            this.cbUserSettings.Click += new System.EventHandler(this.cbUserSettings_Click);
            // 
            // mbDashboard
            // 
            this.mbDashboard.Active = true;
            this.mbDashboard.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDashboard.ActiveBackgroundImage")));
            this.mbDashboard.BackColor = System.Drawing.Color.Transparent;
            this.mbDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDashboard.BackgroundImage")));
            this.mbDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDashboard.ButtonSize = 102;
            this.mbDashboard.FlatAppearance.BorderSize = 0;
            this.mbDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDashboard.ForeColor = System.Drawing.Color.Transparent;
            this.mbDashboard.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDashboard.InactiveBackgroundImage")));
            this.mbDashboard.Location = new System.Drawing.Point(17, 50);
            this.mbDashboard.Name = "mbDashboard";
            this.mbDashboard.Size = new System.Drawing.Size(102, 102);
            this.mbDashboard.StateChangeActivated = true;
            this.mbDashboard.TabIndex = 5;
            this.mbDashboard.TabStop = false;
            this.mbDashboard.UseVisualStyleBackColor = false;
            this.mbDashboard.Click += new System.EventHandler(this.cbDashboard_Click);
            // 
            // cbLoadUnload
            // 
            this.cbLoadUnload.Active = false;
            this.cbLoadUnload.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbLoadUnload.ActiveBackgroundImage")));
            this.cbLoadUnload.BackColor = System.Drawing.Color.Transparent;
            this.cbLoadUnload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbLoadUnload.BackgroundImage")));
            this.cbLoadUnload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbLoadUnload.ButtonSize = 102;
            this.cbLoadUnload.FlatAppearance.BorderSize = 0;
            this.cbLoadUnload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbLoadUnload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbLoadUnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLoadUnload.ForeColor = System.Drawing.Color.Transparent;
            this.cbLoadUnload.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbLoadUnload.InactiveBackgroundImage")));
            this.cbLoadUnload.Location = new System.Drawing.Point(17, 562);
            this.cbLoadUnload.Name = "cbLoadUnload";
            this.cbLoadUnload.Size = new System.Drawing.Size(102, 102);
            this.cbLoadUnload.StateChangeActivated = true;
            this.cbLoadUnload.TabIndex = 10;
            this.cbLoadUnload.TabStop = false;
            this.cbLoadUnload.UseVisualStyleBackColor = false;
            this.cbLoadUnload.Click += new System.EventHandler(this.cbLoadUnload_Click);
            // 
            // cbWorkingsSettings
            // 
            this.cbWorkingsSettings.Active = false;
            this.cbWorkingsSettings.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbWorkingsSettings.ActiveBackgroundImage")));
            this.cbWorkingsSettings.BackColor = System.Drawing.Color.Transparent;
            this.cbWorkingsSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbWorkingsSettings.BackgroundImage")));
            this.cbWorkingsSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbWorkingsSettings.ButtonSize = 102;
            this.cbWorkingsSettings.FlatAppearance.BorderSize = 0;
            this.cbWorkingsSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbWorkingsSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbWorkingsSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbWorkingsSettings.ForeColor = System.Drawing.Color.Transparent;
            this.cbWorkingsSettings.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbWorkingsSettings.InactiveBackgroundImage")));
            this.cbWorkingsSettings.Location = new System.Drawing.Point(17, 434);
            this.cbWorkingsSettings.Name = "cbWorkingsSettings";
            this.cbWorkingsSettings.Size = new System.Drawing.Size(102, 102);
            this.cbWorkingsSettings.StateChangeActivated = false;
            this.cbWorkingsSettings.TabIndex = 40;
            this.cbWorkingsSettings.TabStop = false;
            this.cbWorkingsSettings.UseVisualStyleBackColor = false;
            this.cbWorkingsSettings.Click += new System.EventHandler(this.cbWorkingsSettings_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(135, 720);
            this.ControlBox = false;
            this.Controls.Add(this.cbWorkingsSettings);
            this.Controls.Add(this.cbLoadUnload);
            this.Controls.Add(this.cbManualOperations);
            this.Controls.Add(this.cbUserSettings);
            this.Controls.Add(this.mbDashboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMenu";
            this.ShowIcon = false;
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButton mbDashboard;
        private Machine.UI.Controls.MachineButton cbUserSettings;
        private Machine.UI.Controls.MachineButton cbManualOperations;
        private MachineButton cbLoadUnload;
        private MachineButton cbWorkingsSettings;
    }
}