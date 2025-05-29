using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormMachineParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMachineParameters));
            this.clMachineParameters = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.clEncoderDancer = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.clDancer = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.mbMachineParameters = new Machine.UI.Controls.MachineButton();
            this.mlMachineParameters = new System.Windows.Forms.Label();
            this.mlEncoder = new System.Windows.Forms.Label();
            this.mbEncoder = new Machine.UI.Controls.MachineButton();
            this.mbEncoderDancer = new Machine.UI.Controls.MachineButton();
            this.mlEncoderDancebar = new System.Windows.Forms.Label();
            this.mlDancer = new System.Windows.Forms.Label();
            this.mbDancer = new Machine.UI.Controls.MachineButton();
            this.clEncoder = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.mbUI = new Machine.UI.Controls.MachineButton();
            this.mlUI = new System.Windows.Forms.Label();
            this.clUI = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.mlAxis = new System.Windows.Forms.Label();
            this.mbAxis = new Machine.UI.Controls.MachineButton();
            this.clAxis = new Machine.UI.Controls.MachineEditableItemsListbox();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // clMachineParameters
            // 
            this.clMachineParameters.BackColor = System.Drawing.SystemColors.Control;
            this.clMachineParameters.Location = new System.Drawing.Point(372, 405);
            this.clMachineParameters.Name = "clMachineParameters";
            this.clMachineParameters.Size = new System.Drawing.Size(601, 50);
            this.clMachineParameters.TabIndex = 16;
            // 
            // clEncoderDancer
            // 
            this.clEncoderDancer.BackColor = System.Drawing.SystemColors.Control;
            this.clEncoderDancer.Location = new System.Drawing.Point(372, 343);
            this.clEncoderDancer.Name = "clEncoderDancer";
            this.clEncoderDancer.Size = new System.Drawing.Size(601, 50);
            this.clEncoderDancer.TabIndex = 15;
            // 
            // clDancer
            // 
            this.clDancer.BackColor = System.Drawing.SystemColors.Control;
            this.clDancer.Location = new System.Drawing.Point(372, 157);
            this.clDancer.Name = "clDancer";
            this.clDancer.Size = new System.Drawing.Size(601, 50);
            this.clDancer.TabIndex = 14;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.labelTitle);
            this.panelForm.Controls.Add(this.cbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1010, 120);
            this.panelForm.TabIndex = 9;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(138, 45);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(281, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Low Level Settings";
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
            this.cbReturn.Location = new System.Drawing.Point(12, 9);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(102, 102);
            this.cbReturn.StateChangeActivated = true;
            this.cbReturn.TabIndex = 7;
            this.cbReturn.TabStop = false;
            this.cbReturn.UseVisualStyleBackColor = false;
            this.cbReturn.Click += new System.EventHandler(this.cbReturn_Click);
            // 
            // mbMachineParameters
            // 
            this.mbMachineParameters.Active = false;
            this.mbMachineParameters.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbMachineParameters.ActiveBackgroundImage")));
            this.mbMachineParameters.BackColor = System.Drawing.Color.Transparent;
            this.mbMachineParameters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbMachineParameters.BackgroundImage")));
            this.mbMachineParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbMachineParameters.ButtonSize = 64;
            this.mbMachineParameters.FlatAppearance.BorderSize = 0;
            this.mbMachineParameters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbMachineParameters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbMachineParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbMachineParameters.ForeColor = System.Drawing.Color.Transparent;
            this.mbMachineParameters.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbMachineParameters.InactiveBackgroundImage")));
            this.mbMachineParameters.Location = new System.Drawing.Point(23, 157);
            this.mbMachineParameters.Name = "mbMachineParameters";
            this.mbMachineParameters.Size = new System.Drawing.Size(64, 64);
            this.mbMachineParameters.StateChangeActivated = false;
            this.mbMachineParameters.TabIndex = 35;
            this.mbMachineParameters.TabStop = false;
            this.mbMachineParameters.UseVisualStyleBackColor = false;
            this.mbMachineParameters.Click += new System.EventHandler(this.mbMachineParameters_Click);
            // 
            // mlMachineParameters
            // 
            this.mlMachineParameters.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlMachineParameters.Location = new System.Drawing.Point(94, 159);
            this.mlMachineParameters.Name = "mlMachineParameters";
            this.mlMachineParameters.Size = new System.Drawing.Size(236, 64);
            this.mlMachineParameters.TabIndex = 36;
            this.mlMachineParameters.Text = "mlParameters";
            this.mlMachineParameters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlEncoder
            // 
            this.mlEncoder.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlEncoder.Location = new System.Drawing.Point(94, 363);
            this.mlEncoder.Name = "mlEncoder";
            this.mlEncoder.Size = new System.Drawing.Size(236, 64);
            this.mlEncoder.TabIndex = 34;
            this.mlEncoder.Text = "mlEncoder";
            this.mlEncoder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbEncoder
            // 
            this.mbEncoder.Active = false;
            this.mbEncoder.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoder.ActiveBackgroundImage")));
            this.mbEncoder.BackColor = System.Drawing.Color.Transparent;
            this.mbEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbEncoder.BackgroundImage")));
            this.mbEncoder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbEncoder.ButtonSize = 64;
            this.mbEncoder.FlatAppearance.BorderSize = 0;
            this.mbEncoder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbEncoder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbEncoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbEncoder.ForeColor = System.Drawing.Color.Transparent;
            this.mbEncoder.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoder.InactiveBackgroundImage")));
            this.mbEncoder.Location = new System.Drawing.Point(23, 364);
            this.mbEncoder.Name = "mbEncoder";
            this.mbEncoder.Size = new System.Drawing.Size(64, 64);
            this.mbEncoder.StateChangeActivated = false;
            this.mbEncoder.TabIndex = 33;
            this.mbEncoder.TabStop = false;
            this.mbEncoder.UseVisualStyleBackColor = false;
            this.mbEncoder.Click += new System.EventHandler(this.mbEncoder_Click);
            // 
            // mbEncoderDancer
            // 
            this.mbEncoderDancer.Active = false;
            this.mbEncoderDancer.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoderDancer.ActiveBackgroundImage")));
            this.mbEncoderDancer.BackColor = System.Drawing.Color.Transparent;
            this.mbEncoderDancer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbEncoderDancer.BackgroundImage")));
            this.mbEncoderDancer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbEncoderDancer.ButtonSize = 64;
            this.mbEncoderDancer.FlatAppearance.BorderSize = 0;
            this.mbEncoderDancer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbEncoderDancer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbEncoderDancer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbEncoderDancer.ForeColor = System.Drawing.Color.Transparent;
            this.mbEncoderDancer.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoderDancer.InactiveBackgroundImage")));
            this.mbEncoderDancer.Location = new System.Drawing.Point(23, 502);
            this.mbEncoderDancer.Name = "mbEncoderDancer";
            this.mbEncoderDancer.Size = new System.Drawing.Size(64, 64);
            this.mbEncoderDancer.StateChangeActivated = false;
            this.mbEncoderDancer.TabIndex = 31;
            this.mbEncoderDancer.TabStop = false;
            this.mbEncoderDancer.UseVisualStyleBackColor = false;
            this.mbEncoderDancer.Click += new System.EventHandler(this.mbEncoderWithDancer_Click);
            // 
            // mlEncoderDancebar
            // 
            this.mlEncoderDancebar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlEncoderDancebar.Location = new System.Drawing.Point(94, 497);
            this.mlEncoderDancebar.Name = "mlEncoderDancebar";
            this.mlEncoderDancebar.Size = new System.Drawing.Size(236, 64);
            this.mlEncoderDancebar.TabIndex = 32;
            this.mlEncoderDancebar.Text = "mlEncoderWithDanceBar";
            this.mlEncoderDancebar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlDancer
            // 
            this.mlDancer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDancer.Location = new System.Drawing.Point(94, 431);
            this.mlDancer.Name = "mlDancer";
            this.mlDancer.Size = new System.Drawing.Size(236, 62);
            this.mlDancer.TabIndex = 30;
            this.mlDancer.Text = "mlDancer";
            this.mlDancer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.mbDancer.Location = new System.Drawing.Point(23, 433);
            this.mbDancer.Name = "mbDancer";
            this.mbDancer.Size = new System.Drawing.Size(64, 64);
            this.mbDancer.StateChangeActivated = false;
            this.mbDancer.TabIndex = 29;
            this.mbDancer.TabStop = false;
            this.mbDancer.UseVisualStyleBackColor = false;
            this.mbDancer.Click += new System.EventHandler(this.mbDancer_Click);
            // 
            // clEncoder
            // 
            this.clEncoder.BackColor = System.Drawing.SystemColors.Control;
            this.clEncoder.Location = new System.Drawing.Point(372, 281);
            this.clEncoder.Name = "clEncoder";
            this.clEncoder.Size = new System.Drawing.Size(601, 50);
            this.clEncoder.TabIndex = 26;
            // 
            // mbUI
            // 
            this.mbUI.Active = false;
            this.mbUI.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbUI.ActiveBackgroundImage")));
            this.mbUI.BackColor = System.Drawing.Color.Transparent;
            this.mbUI.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbUI.BackgroundImage")));
            this.mbUI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbUI.ButtonSize = 64;
            this.mbUI.FlatAppearance.BorderSize = 0;
            this.mbUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbUI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbUI.ForeColor = System.Drawing.Color.Transparent;
            this.mbUI.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbUI.InactiveBackgroundImage")));
            this.mbUI.Location = new System.Drawing.Point(23, 226);
            this.mbUI.Name = "mbUI";
            this.mbUI.Size = new System.Drawing.Size(64, 64);
            this.mbUI.StateChangeActivated = false;
            this.mbUI.TabIndex = 37;
            this.mbUI.TabStop = false;
            this.mbUI.UseVisualStyleBackColor = false;
            this.mbUI.Click += new System.EventHandler(this.mbUI_Click);
            // 
            // mlUI
            // 
            this.mlUI.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlUI.Location = new System.Drawing.Point(94, 227);
            this.mlUI.Name = "mlUI";
            this.mlUI.Size = new System.Drawing.Size(236, 64);
            this.mlUI.TabIndex = 38;
            this.mlUI.Text = "mlUI";
            this.mlUI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clUI
            // 
            this.clUI.BackColor = System.Drawing.SystemColors.Control;
            this.clUI.Location = new System.Drawing.Point(372, 219);
            this.clUI.Name = "clUI";
            this.clUI.Size = new System.Drawing.Size(601, 50);
            this.clUI.TabIndex = 39;
            // 
            // mlAxis
            // 
            this.mlAxis.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlAxis.Location = new System.Drawing.Point(94, 295);
            this.mlAxis.Name = "mlAxis";
            this.mlAxis.Size = new System.Drawing.Size(236, 64);
            this.mlAxis.TabIndex = 41;
            this.mlAxis.Text = "mlAxis";
            this.mlAxis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbAxis
            // 
            this.mbAxis.Active = false;
            this.mbAxis.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAxis.ActiveBackgroundImage")));
            this.mbAxis.BackColor = System.Drawing.Color.Transparent;
            this.mbAxis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbAxis.BackgroundImage")));
            this.mbAxis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbAxis.ButtonSize = 64;
            this.mbAxis.FlatAppearance.BorderSize = 0;
            this.mbAxis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbAxis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbAxis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbAxis.ForeColor = System.Drawing.Color.Transparent;
            this.mbAxis.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAxis.InactiveBackgroundImage")));
            this.mbAxis.Location = new System.Drawing.Point(24, 295);
            this.mbAxis.Name = "mbAxis";
            this.mbAxis.Size = new System.Drawing.Size(64, 64);
            this.mbAxis.StateChangeActivated = false;
            this.mbAxis.TabIndex = 40;
            this.mbAxis.TabStop = false;
            this.mbAxis.UseVisualStyleBackColor = false;
            this.mbAxis.Click += new System.EventHandler(this.mbAxis_Click);
            // 
            // clAxis
            // 
            this.clAxis.BackColor = System.Drawing.SystemColors.Control;
            this.clAxis.Location = new System.Drawing.Point(372, 467);
            this.clAxis.Name = "clAxis";
            this.clAxis.Size = new System.Drawing.Size(601, 50);
            this.clAxis.TabIndex = 42;
            // 
            // FormMachineParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 720);
            this.Controls.Add(this.clAxis);
            this.Controls.Add(this.mlAxis);
            this.Controls.Add(this.mbAxis);
            this.Controls.Add(this.clUI);
            this.Controls.Add(this.mbUI);
            this.Controls.Add(this.mlUI);
            this.Controls.Add(this.mbMachineParameters);
            this.Controls.Add(this.mlMachineParameters);
            this.Controls.Add(this.mlEncoder);
            this.Controls.Add(this.clEncoder);
            this.Controls.Add(this.mbEncoder);
            this.Controls.Add(this.mbEncoderDancer);
            this.Controls.Add(this.clMachineParameters);
            this.Controls.Add(this.mlEncoderDancebar);
            this.Controls.Add(this.clEncoderDancer);
            this.Controls.Add(this.mlDancer);
            this.Controls.Add(this.clDancer);
            this.Controls.Add(this.mbDancer);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMachineParameters";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMachineParameters";
            this.Load += new System.EventHandler(this.FormMachineParameters_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Machine.UI.Controls.MachineButton cbReturn;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private MachineEditableItemsListbox clDancer;
        private MachineEditableItemsListbox clEncoderDancer;
        private MachineEditableItemsListbox clMachineParameters;
        private MachineButton mbEncoderDancer;
        private System.Windows.Forms.Label mlEncoderDancebar;
        private System.Windows.Forms.Label mlDancer;
        private MachineButton mbDancer;
        private System.Windows.Forms.Label mlEncoder;
        private MachineButton mbEncoder;
        private MachineEditableItemsListbox clEncoder;
        private MachineButton mbMachineParameters;
        private System.Windows.Forms.Label mlMachineParameters;
        private MachineButton mbUI;
        private System.Windows.Forms.Label mlUI;
        private MachineEditableItemsListbox clUI;
        private System.Windows.Forms.Label mlAxis;
        private MachineButton mbAxis;
        private MachineEditableItemsListbox clAxis;
    }
}