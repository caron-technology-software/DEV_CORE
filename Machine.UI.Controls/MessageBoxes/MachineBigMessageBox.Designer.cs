namespace Machine.UI.Controls
{
    partial class MachineBigMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineBigMessageBox));
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.mlTitle = new Machine.UI.Controls.MachineLabel();
            this.sbCancel = new Machine.UI.Controls.MachineButton();
            this.sbOk = new Machine.UI.Controls.MachineButton();
            this.SuspendLayout();
            // 
            // rtbMessage
            // 
            this.rtbMessage.BackColor = System.Drawing.Color.Black;
            this.rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.rtbMessage.Location = new System.Drawing.Point(41, 106);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMessage.Size = new System.Drawing.Size(694, 250);
            this.rtbMessage.TabIndex = 4;
            this.rtbMessage.Text = "";
            // 
            // mlTitle
            // 
            this.mlTitle.BackColor = System.Drawing.Color.Transparent;
            this.mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTitle.ForeColor = System.Drawing.Color.White;
            this.mlTitle.Location = new System.Drawing.Point(110, 24);
            this.mlTitle.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.mlTitle.Name = "mlTitle";
            this.mlTitle.Size = new System.Drawing.Size(552, 73);
            this.mlTitle.TabIndex = 3;
            this.mlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sbCancel
            // 
            this.sbCancel.Active = false;
            this.sbCancel.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbCancel.ActiveBackgroundImage")));
            this.sbCancel.BackColor = System.Drawing.Color.Transparent;
            this.sbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbCancel.BackgroundImage")));
            this.sbCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbCancel.ButtonSize = 72;
            this.sbCancel.FlatAppearance.BorderSize = 0;
            this.sbCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbCancel.ForeColor = System.Drawing.Color.Transparent;
            this.sbCancel.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbCancel.InactiveBackgroundImage")));
            this.sbCancel.Location = new System.Drawing.Point(451, 362);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(72, 72);
            this.sbCancel.StateChangeActivated = true;
            this.sbCancel.TabIndex = 1;
            this.sbCancel.TabStop = false;
            this.sbCancel.UseVisualStyleBackColor = false;
            this.sbCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sbCancel_MouseClick);
            // 
            // sbOk
            // 
            this.sbOk.Active = false;
            this.sbOk.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbOk.ActiveBackgroundImage")));
            this.sbOk.BackColor = System.Drawing.Color.Transparent;
            this.sbOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbOk.BackgroundImage")));
            this.sbOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbOk.ButtonSize = 72;
            this.sbOk.FlatAppearance.BorderSize = 0;
            this.sbOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbOk.ForeColor = System.Drawing.Color.Transparent;
            this.sbOk.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbOk.InactiveBackgroundImage")));
            this.sbOk.Location = new System.Drawing.Point(227, 362);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(72, 72);
            this.sbOk.StateChangeActivated = true;
            this.sbOk.TabIndex = 0;
            this.sbOk.TabStop = false;
            this.sbOk.UseVisualStyleBackColor = false;
            this.sbOk.Click += new System.EventHandler(this.sbOk_Click);
            // 
            // MachineBigMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Machine.UI.Controls.Properties.Resources.rectangle_big_filled;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(779, 464);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.mlTitle);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.sbOk);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MachineBigMessageBox";
            this.Opacity = 0.97D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MachineMessageBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButton sbOk;
        private Machine.UI.Controls.MachineButton sbCancel;
        private Machine.UI.Controls.MachineLabel mlTitle;
        private System.Windows.Forms.RichTextBox rtbMessage;
    }
}