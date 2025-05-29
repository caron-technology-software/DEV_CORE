namespace Caron.Cradle.UI
{
    partial class FormLicenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicenses));
            this.panel = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.labelCode = new Machine.UI.Controls.MachineLabel();
            this.labelTitle = new Machine.UI.Controls.MachineLabel();
            this.labelMessage = new Machine.UI.Controls.MachineLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.labelCode);
            this.panel.Controls.Add(this.labelTitle);
            this.panel.Controls.Add(this.labelMessage);
            this.panel.LineColor = System.Drawing.Color.LightGray;
            this.panel.LineWidth = 5;
            this.panel.Location = new System.Drawing.Point(117, 157);
            this.panel.Name = "panel";
            this.panel.Radius = 10;
            this.panel.Size = new System.Drawing.Size(895, 388);
            this.panel.TabIndex = 0;
            // 
            // labelCode
            // 
            this.labelCode.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.ForeColor = System.Drawing.Color.DimGray;
            this.labelCode.Location = new System.Drawing.Point(77, 298);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(735, 67);
            this.labelCode.TabIndex = 1;
            this.labelCode.Text = "Code";
            this.labelCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCode.DoubleClick += new System.EventHandler(this.labelCode_DoubleClick);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Red;
            this.labelTitle.Location = new System.Drawing.Point(12, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(863, 75);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMessage
            // 
            this.labelMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.ForeColor = System.Drawing.Color.Black;
            this.labelMessage.Location = new System.Drawing.Point(12, 111);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(863, 168);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "Message";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 720);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormError";
            this.ShowIcon = false;
            this.Text = "FormLicenses";
            this.Load += new System.EventHandler(this.FormError_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachinePanelEdgeRounded panel;
        private Machine.UI.Controls.MachineLabel labelMessage;
        private Machine.UI.Controls.MachineLabel labelTitle;
        private Machine.UI.Controls.MachineLabel labelCode;
    }
}