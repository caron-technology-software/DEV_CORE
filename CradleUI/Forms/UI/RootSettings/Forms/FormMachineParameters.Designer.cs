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
            clMachineParameters = new MachineEditableItemsListbox();
            clEncoderDancer = new MachineEditableItemsListbox();
            clDancer = new MachineEditableItemsListbox();
            panelForm = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            cbReturn = new MachineButton();
            mbMachineParameters = new MachineButton();
            mlMachineParameters = new System.Windows.Forms.Label();
            mlEncoder = new System.Windows.Forms.Label();
            mbEncoder = new MachineButton();
            mbEncoderDancer = new MachineButton();
            mlEncoderDancebar = new System.Windows.Forms.Label();
            mlDancer = new System.Windows.Forms.Label();
            mbDancer = new MachineButton();
            clEncoder = new MachineEditableItemsListbox();
            mbUI = new MachineButton();
            mlUI = new System.Windows.Forms.Label();
            clUI = new MachineEditableItemsListbox();
            mlAxis = new System.Windows.Forms.Label();
            mbAxis = new MachineButton();
            clAxis = new MachineEditableItemsListbox();
            panelForm.SuspendLayout();
            SuspendLayout();
            // 
            // clMachineParameters
            // 
            clMachineParameters.BackColor = System.Drawing.SystemColors.Control;
            clMachineParameters.Location = new System.Drawing.Point(434, 467);
            clMachineParameters.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clMachineParameters.Name = "clMachineParameters";
            clMachineParameters.Size = new System.Drawing.Size(701, 58);
            clMachineParameters.TabIndex = 16;
            // 
            // clEncoderDancer
            // 
            clEncoderDancer.BackColor = System.Drawing.SystemColors.Control;
            clEncoderDancer.Location = new System.Drawing.Point(434, 396);
            clEncoderDancer.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clEncoderDancer.Name = "clEncoderDancer";
            clEncoderDancer.Size = new System.Drawing.Size(701, 58);
            clEncoderDancer.TabIndex = 15;
            // 
            // clDancer
            // 
            clDancer.BackColor = System.Drawing.SystemColors.Control;
            clDancer.Location = new System.Drawing.Point(434, 181);
            clDancer.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clDancer.Name = "clDancer";
            clDancer.Size = new System.Drawing.Size(701, 58);
            clDancer.TabIndex = 14;
            // 
            // panelForm
            // 
            panelForm.BackColor = System.Drawing.Color.FromArgb(26, 37, 43);
            panelForm.Controls.Add(labelTitle);
            panelForm.Controls.Add(cbReturn);
            panelForm.Location = new System.Drawing.Point(0, 0);
            panelForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelForm.Name = "panelForm";
            panelForm.Size = new System.Drawing.Size(1178, 138);
            panelForm.TabIndex = 9;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelTitle.ForeColor = System.Drawing.Color.White;
            labelTitle.Location = new System.Drawing.Point(161, 52);
            labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(281, 33);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "Low Level Settings";
            // 
            // cbReturn
            // 
            cbReturn.Active = false;
            cbReturn.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbReturn.ActiveBackgroundImage");
            cbReturn.BackColor = System.Drawing.Color.Transparent;
            cbReturn.BackgroundImage = (System.Drawing.Image)resources.GetObject("cbReturn.BackgroundImage");
            cbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            cbReturn.ButtonSize = 102;
            cbReturn.FlatAppearance.BorderSize = 0;
            cbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            cbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            cbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbReturn.ForeColor = System.Drawing.Color.Transparent;
            cbReturn.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("cbReturn.InactiveBackgroundImage");
            cbReturn.Location = new System.Drawing.Point(14, 10);
            cbReturn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbReturn.Name = "cbReturn";
            cbReturn.Size = new System.Drawing.Size(102, 102);
            cbReturn.StateChangeActivated = true;
            cbReturn.TabIndex = 7;
            cbReturn.TabStop = false;
            cbReturn.UseVisualStyleBackColor = false;
            cbReturn.Click += cbReturn_Click;
            // 
            // mbMachineParameters
            // 
            mbMachineParameters.Active = false;
            mbMachineParameters.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbMachineParameters.ActiveBackgroundImage");
            mbMachineParameters.BackColor = System.Drawing.Color.Transparent;
            mbMachineParameters.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbMachineParameters.BackgroundImage");
            mbMachineParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbMachineParameters.ButtonSize = 64;
            mbMachineParameters.FlatAppearance.BorderSize = 0;
            mbMachineParameters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbMachineParameters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbMachineParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbMachineParameters.ForeColor = System.Drawing.Color.Transparent;
            mbMachineParameters.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbMachineParameters.InactiveBackgroundImage");
            mbMachineParameters.Location = new System.Drawing.Point(27, 181);
            mbMachineParameters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbMachineParameters.Name = "mbMachineParameters";
            mbMachineParameters.Size = new System.Drawing.Size(64, 64);
            mbMachineParameters.StateChangeActivated = false;
            mbMachineParameters.TabIndex = 35;
            mbMachineParameters.TabStop = false;
            mbMachineParameters.UseVisualStyleBackColor = false;
            mbMachineParameters.Click += mbMachineParameters_Click;
            // 
            // mlMachineParameters
            // 
            mlMachineParameters.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlMachineParameters.Location = new System.Drawing.Point(110, 183);
            mlMachineParameters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlMachineParameters.Name = "mlMachineParameters";
            mlMachineParameters.Size = new System.Drawing.Size(220, 74);
            mlMachineParameters.TabIndex = 36;
            mlMachineParameters.Text = "mlParameters";
            mlMachineParameters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlEncoder
            // 
            mlEncoder.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlEncoder.Location = new System.Drawing.Point(110, 419);
            mlEncoder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlEncoder.Name = "mlEncoder";
            mlEncoder.Size = new System.Drawing.Size(220, 74);
            mlEncoder.TabIndex = 34;
            mlEncoder.Text = "mlEncoder";
            mlEncoder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbEncoder
            // 
            mbEncoder.Active = false;
            mbEncoder.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbEncoder.ActiveBackgroundImage");
            mbEncoder.BackColor = System.Drawing.Color.Transparent;
            mbEncoder.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbEncoder.BackgroundImage");
            mbEncoder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbEncoder.ButtonSize = 64;
            mbEncoder.FlatAppearance.BorderSize = 0;
            mbEncoder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbEncoder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbEncoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbEncoder.ForeColor = System.Drawing.Color.Transparent;
            mbEncoder.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbEncoder.InactiveBackgroundImage");
            mbEncoder.Location = new System.Drawing.Point(27, 420);
            mbEncoder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbEncoder.Name = "mbEncoder";
            mbEncoder.Size = new System.Drawing.Size(64, 64);
            mbEncoder.StateChangeActivated = false;
            mbEncoder.TabIndex = 33;
            mbEncoder.TabStop = false;
            mbEncoder.UseVisualStyleBackColor = false;
            mbEncoder.Click += mbEncoder_Click;
            // 
            // mbEncoderDancer
            // 
            mbEncoderDancer.Active = false;
            mbEncoderDancer.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbEncoderDancer.ActiveBackgroundImage");
            mbEncoderDancer.BackColor = System.Drawing.Color.Transparent;
            mbEncoderDancer.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbEncoderDancer.BackgroundImage");
            mbEncoderDancer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbEncoderDancer.ButtonSize = 64;
            mbEncoderDancer.FlatAppearance.BorderSize = 0;
            mbEncoderDancer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbEncoderDancer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbEncoderDancer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbEncoderDancer.ForeColor = System.Drawing.Color.Transparent;
            mbEncoderDancer.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbEncoderDancer.InactiveBackgroundImage");
            mbEncoderDancer.Location = new System.Drawing.Point(27, 579);
            mbEncoderDancer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbEncoderDancer.Name = "mbEncoderDancer";
            mbEncoderDancer.Size = new System.Drawing.Size(64, 64);
            mbEncoderDancer.StateChangeActivated = false;
            mbEncoderDancer.TabIndex = 31;
            mbEncoderDancer.TabStop = false;
            mbEncoderDancer.UseVisualStyleBackColor = false;
            mbEncoderDancer.Click += mbEncoderWithDancer_Click;
            // 
            // mlEncoderDancebar
            // 
            mlEncoderDancebar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlEncoderDancebar.Location = new System.Drawing.Point(110, 573);
            mlEncoderDancebar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlEncoderDancebar.Name = "mlEncoderDancebar";
            mlEncoderDancebar.Size = new System.Drawing.Size(220, 74);
            mlEncoderDancebar.TabIndex = 32;
            mlEncoderDancebar.Text = "mlEncoderWithDanceBar";
            mlEncoderDancebar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mlDancer
            // 
            mlDancer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlDancer.Location = new System.Drawing.Point(110, 497);
            mlDancer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlDancer.Name = "mlDancer";
            mlDancer.Size = new System.Drawing.Size(220, 72);
            mlDancer.TabIndex = 30;
            mlDancer.Text = "mlDancer";
            mlDancer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbDancer
            // 
            mbDancer.Active = false;
            mbDancer.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDancer.ActiveBackgroundImage");
            mbDancer.BackColor = System.Drawing.Color.Transparent;
            mbDancer.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbDancer.BackgroundImage");
            mbDancer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbDancer.ButtonSize = 64;
            mbDancer.FlatAppearance.BorderSize = 0;
            mbDancer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbDancer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbDancer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbDancer.ForeColor = System.Drawing.Color.Transparent;
            mbDancer.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbDancer.InactiveBackgroundImage");
            mbDancer.Location = new System.Drawing.Point(27, 500);
            mbDancer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbDancer.Name = "mbDancer";
            mbDancer.Size = new System.Drawing.Size(64, 64);
            mbDancer.StateChangeActivated = false;
            mbDancer.TabIndex = 29;
            mbDancer.TabStop = false;
            mbDancer.UseVisualStyleBackColor = false;
            mbDancer.Click += mbDancer_Click;
            // 
            // clEncoder
            // 
            clEncoder.BackColor = System.Drawing.SystemColors.Control;
            clEncoder.Location = new System.Drawing.Point(434, 324);
            clEncoder.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clEncoder.Name = "clEncoder";
            clEncoder.Size = new System.Drawing.Size(701, 58);
            clEncoder.TabIndex = 26;
            // 
            // mbUI
            // 
            mbUI.Active = false;
            mbUI.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbUI.ActiveBackgroundImage");
            mbUI.BackColor = System.Drawing.Color.Transparent;
            mbUI.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbUI.BackgroundImage");
            mbUI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbUI.ButtonSize = 64;
            mbUI.FlatAppearance.BorderSize = 0;
            mbUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbUI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbUI.ForeColor = System.Drawing.Color.Transparent;
            mbUI.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbUI.InactiveBackgroundImage");
            mbUI.Location = new System.Drawing.Point(27, 261);
            mbUI.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbUI.Name = "mbUI";
            mbUI.Size = new System.Drawing.Size(64, 64);
            mbUI.StateChangeActivated = false;
            mbUI.TabIndex = 37;
            mbUI.TabStop = false;
            mbUI.UseVisualStyleBackColor = false;
            mbUI.Click += mbUI_Click;
            // 
            // mlUI
            // 
            mlUI.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlUI.Location = new System.Drawing.Point(110, 262);
            mlUI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlUI.Name = "mlUI";
            mlUI.Size = new System.Drawing.Size(220, 74);
            mlUI.TabIndex = 38;
            mlUI.Text = "mlUI";
            mlUI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clUI
            // 
            clUI.BackColor = System.Drawing.SystemColors.Control;
            clUI.Location = new System.Drawing.Point(434, 253);
            clUI.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clUI.Name = "clUI";
            clUI.Size = new System.Drawing.Size(701, 58);
            clUI.TabIndex = 39;
            // 
            // mlAxis
            // 
            mlAxis.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mlAxis.Location = new System.Drawing.Point(110, 340);
            mlAxis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mlAxis.Name = "mlAxis";
            mlAxis.Size = new System.Drawing.Size(220, 74);
            mlAxis.TabIndex = 41;
            mlAxis.Text = "mlAxis";
            mlAxis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbAxis
            // 
            mbAxis.Active = false;
            mbAxis.ActiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAxis.ActiveBackgroundImage");
            mbAxis.BackColor = System.Drawing.Color.Transparent;
            mbAxis.BackgroundImage = (System.Drawing.Image)resources.GetObject("mbAxis.BackgroundImage");
            mbAxis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            mbAxis.ButtonSize = 64;
            mbAxis.FlatAppearance.BorderSize = 0;
            mbAxis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            mbAxis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            mbAxis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            mbAxis.ForeColor = System.Drawing.Color.Transparent;
            mbAxis.InactiveBackgroundImage = (System.Drawing.Bitmap)resources.GetObject("mbAxis.InactiveBackgroundImage");
            mbAxis.Location = new System.Drawing.Point(28, 340);
            mbAxis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mbAxis.Name = "mbAxis";
            mbAxis.Size = new System.Drawing.Size(64, 64);
            mbAxis.StateChangeActivated = false;
            mbAxis.TabIndex = 40;
            mbAxis.TabStop = false;
            mbAxis.UseVisualStyleBackColor = false;
            mbAxis.Click += mbAxis_Click;
            // 
            // clAxis
            // 
            clAxis.BackColor = System.Drawing.SystemColors.Control;
            clAxis.Location = new System.Drawing.Point(434, 539);
            clAxis.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            clAxis.Name = "clAxis";
            clAxis.Size = new System.Drawing.Size(701, 58);
            clAxis.TabIndex = 42;
            // 
            // FormMachineParameters
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1178, 831);
            Controls.Add(clAxis);
            Controls.Add(mlAxis);
            Controls.Add(mbAxis);
            Controls.Add(clUI);
            Controls.Add(mbUI);
            Controls.Add(mlUI);
            Controls.Add(mbMachineParameters);
            Controls.Add(mlMachineParameters);
            Controls.Add(mlEncoder);
            Controls.Add(clEncoder);
            Controls.Add(mbEncoder);
            Controls.Add(mbEncoderDancer);
            Controls.Add(clMachineParameters);
            Controls.Add(mlEncoderDancebar);
            Controls.Add(clEncoderDancer);
            Controls.Add(mlDancer);
            Controls.Add(clDancer);
            Controls.Add(mbDancer);
            Controls.Add(panelForm);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMachineParameters";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormMachineParameters";
            Load += FormMachineParameters_Load;
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ResumeLayout(false);
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