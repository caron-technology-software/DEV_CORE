using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    partial class FormMachineConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMachineConfiguration));
            this.panelForm = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.cbReverseEncoder = new Machine.UI.Controls.MachineButtonLabel();
            this.cbCutterPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.cbTitanPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.cbEncoderPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.cbUserCanEnableEncoder = new Machine.UI.Controls.MachineButtonLabel();
            this.mcbMachineType = new Machine.UI.Controls.MachineComboBoxItem();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.machineSerial = new Machine.UI.Controls.MachineStringEditableItemWithName();
            this.cbEnableFunctionPhotocellRollPresence = new Machine.UI.Controls.MachineButtonLabel();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
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
            this.labelTitle.Size = new System.Drawing.Size(336, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Machine Configuration";
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
            // cbReverseEncoder
            // 
            this.cbReverseEncoder.Active = false;
            this.cbReverseEncoder.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReverseEncoder.ActiveBackgroundImage")));
            this.cbReverseEncoder.ButtonSize = 102;
            this.cbReverseEncoder.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReverseEncoder.InactiveBackgroundImage")));
            this.cbReverseEncoder.Location = new System.Drawing.Point(52, 557);
            this.cbReverseEncoder.Name = "cbReverseEncoder";
            this.cbReverseEncoder.PropertyName = "cbReverseEncoder";
            this.cbReverseEncoder.Size = new System.Drawing.Size(450, 110);
            this.cbReverseEncoder.TabIndex = 11;
            // 
            // cbCutterPresence
            // 
            this.cbCutterPresence.Active = false;
            this.cbCutterPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCutterPresence.ActiveBackgroundImage")));
            this.cbCutterPresence.ButtonSize = 102;
            this.cbCutterPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbCutterPresence.InactiveBackgroundImage")));
            this.cbCutterPresence.Location = new System.Drawing.Point(52, 325);
            this.cbCutterPresence.Name = "cbCutterPresence";
            this.cbCutterPresence.PropertyName = "cbCutterPresence";
            this.cbCutterPresence.Size = new System.Drawing.Size(450, 110);
            this.cbCutterPresence.TabIndex = 10;
            // 
            // cbTitanPresence
            // 
            this.cbTitanPresence.Active = false;
            this.cbTitanPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbTitanPresence.ActiveBackgroundImage")));
            this.cbTitanPresence.ButtonSize = 102;
            this.cbTitanPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbTitanPresence.InactiveBackgroundImage")));
            this.cbTitanPresence.Location = new System.Drawing.Point(508, 325);
            this.cbTitanPresence.Name = "cbTitanPresence";
            this.cbTitanPresence.PropertyName = "cbTitanPresence";
            this.cbTitanPresence.Size = new System.Drawing.Size(450, 110);
            this.cbTitanPresence.TabIndex = 13;
            // 
            // cbEncoderPresence
            // 
            this.cbEncoderPresence.Active = false;
            this.cbEncoderPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEncoderPresence.ActiveBackgroundImage")));
            this.cbEncoderPresence.ButtonSize = 102;
            this.cbEncoderPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEncoderPresence.InactiveBackgroundImage")));
            this.cbEncoderPresence.Location = new System.Drawing.Point(52, 441);
            this.cbEncoderPresence.Name = "cbEncoderPresence";
            this.cbEncoderPresence.PropertyName = "cbEncoderPresence";
            this.cbEncoderPresence.Size = new System.Drawing.Size(450, 110);
            this.cbEncoderPresence.TabIndex = 12;
            // 
            // cbUserCanEnableEncoder
            // 
            this.cbUserCanEnableEncoder.Active = false;
            this.cbUserCanEnableEncoder.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbUserCanEnableEncoder.ActiveBackgroundImage")));
            this.cbUserCanEnableEncoder.ButtonSize = 102;
            this.cbUserCanEnableEncoder.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbUserCanEnableEncoder.InactiveBackgroundImage")));
            this.cbUserCanEnableEncoder.Location = new System.Drawing.Point(508, 441);
            this.cbUserCanEnableEncoder.Name = "cbUserCanEnableEncoder";
            this.cbUserCanEnableEncoder.PropertyName = "cbUserCanEnableEncoder";
            this.cbUserCanEnableEncoder.Size = new System.Drawing.Size(450, 110);
            this.cbUserCanEnableEncoder.TabIndex = 14;
            // 
            // mcbMachineType
            // 
            this.mcbMachineType.BackColor = System.Drawing.Color.Transparent;
            this.mcbMachineType.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.mcbMachineType.ColorBackgroundSelectedItem = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.mcbMachineType.Location = new System.Drawing.Point(52, 225);
            this.mcbMachineType.Name = "mcbMachineType";
            this.mcbMachineType.PropertyName = null;
            this.mcbMachineType.PropertyValue = -1;
            this.mcbMachineType.SelectedIndex = -1;
            this.mcbMachineType.Size = new System.Drawing.Size(602, 44);
            this.mcbMachineType.StringID = null;
            this.mcbMachineType.TabIndex = 15;
            // 
            // panelBorder
            // 
            this.panelBorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelBorder.Location = new System.Drawing.Point(4, 282);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Size = new System.Drawing.Size(1000, 4);
            this.panelBorder.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(4, 694);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 4);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(4, 146);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 4);
            this.panel2.TabIndex = 17;
            // 
            // machineSerial
            // 
            this.machineSerial.BackColor = System.Drawing.Color.Transparent;
            this.machineSerial.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.machineSerial.ColorBackgroundSelectedItem = System.Drawing.Color.DarkGray;
            this.machineSerial.IsSelected = false;
            this.machineSerial.Location = new System.Drawing.Point(52, 168);
            this.machineSerial.Name = "machineSerial";
            this.machineSerial.PropertyName = null;
            this.machineSerial.PropertyValue = null;
            this.machineSerial.Size = new System.Drawing.Size(602, 42);
            this.machineSerial.StringID = null;
            this.machineSerial.TabIndex = 18;
            // 
            // cbEnableFunctionPhotocellRollPresence
            // 
            this.cbEnableFunctionPhotocellRollPresence.Active = false;
            this.cbEnableFunctionPhotocellRollPresence.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEnableFunctionPhotocellRollPresence.ActiveBackgroundImage")));
            this.cbEnableFunctionPhotocellRollPresence.ButtonSize = 102;
            this.cbEnableFunctionPhotocellRollPresence.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbEnableFunctionPhotocellRollPresence.InactiveBackgroundImage")));
            this.cbEnableFunctionPhotocellRollPresence.Location = new System.Drawing.Point(508, 557);
            this.cbEnableFunctionPhotocellRollPresence.Name = "cbEnableFunctionPhotocellRollPresence";
            this.cbEnableFunctionPhotocellRollPresence.PropertyName = "cbEnableFunctionPhotocellRollPresence";
            this.cbEnableFunctionPhotocellRollPresence.Size = new System.Drawing.Size(450, 110);
            this.cbEnableFunctionPhotocellRollPresence.TabIndex = 19;
            // 
            // FormMachineConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 720);
            this.Controls.Add(this.cbEnableFunctionPhotocellRollPresence);
            this.Controls.Add(this.machineSerial);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBorder);
            this.Controls.Add(this.mcbMachineType);
            this.Controls.Add(this.cbUserCanEnableEncoder);
            this.Controls.Add(this.cbTitanPresence);
            this.Controls.Add(this.cbEncoderPresence);
            this.Controls.Add(this.cbReverseEncoder);
            this.Controls.Add(this.cbCutterPresence);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMachineConfiguration";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RootEnableDisableFunctions";
            this.Load += new System.EventHandler(this.FormRootSettingsMachineConfiguration_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Machine.UI.Controls.MachineButton cbReturn;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private Machine.UI.Controls.MachineButtonLabel cbReverseEncoder;
        private Machine.UI.Controls.MachineButtonLabel cbCutterPresence;
        private Machine.UI.Controls.MachineButtonLabel cbTitanPresence;
        private Machine.UI.Controls.MachineButtonLabel cbEncoderPresence;
        private Machine.UI.Controls.MachineButtonLabel cbUserCanEnableEncoder;
        private MachineComboBoxItem mcbMachineType;
        private System.Windows.Forms.Panel panelBorder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MachineStringEditableItemWithName machineSerial;
        private MachineButtonLabel cbEnableFunctionPhotocellRollPresence;
    }
}