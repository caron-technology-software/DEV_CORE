namespace Machine.UI.Controls
{
    partial class MachineMessageInfoWithChecker
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
            this.slTitle = new Machine.UI.Controls.MachineLabel();
            this.slMessage = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slTitle
            // 
            this.slTitle.BackColor = System.Drawing.Color.Transparent;
            this.slTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slTitle.ForeColor = System.Drawing.Color.White;
            this.slTitle.Location = new System.Drawing.Point(110, 15);
            this.slTitle.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.slTitle.Name = "slTitle";
            this.slTitle.Size = new System.Drawing.Size(552, 60);
            this.slTitle.TabIndex = 3;
            this.slTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slTitle.Click += new System.EventHandler(this.SlTitle_Click);
            // 
            // slMessage
            // 
            this.slMessage.BackColor = System.Drawing.Color.Transparent;
            this.slMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slMessage.ForeColor = System.Drawing.Color.White;
            this.slMessage.Location = new System.Drawing.Point(106, 87);
            this.slMessage.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.slMessage.Name = "slMessage";
            this.slMessage.Size = new System.Drawing.Size(567, 138);
            this.slMessage.TabIndex = 2;
            this.slMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slMessage.Click += new System.EventHandler(this.SlMessage_Click);
            // 
            // MachineMessageInfoWithChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Machine.UI.Controls.Properties.Resources.rectangle_filled;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(779, 250);
            this.Controls.Add(this.slTitle);
            this.Controls.Add(this.slMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MachineMessageInfoWithChecker";
            this.Opacity = 0.97D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormMachineMessageDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Machine.UI.Controls.MachineLabel slMessage;
        private Machine.UI.Controls.MachineLabel slTitle;
    }
}