namespace Caron.Cradle.UI
{
    partial class FormTopBar
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
            this.panelRootSettings = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelShutdown = new System.Windows.Forms.Panel();
            this.labelSubLine = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.panelDebug = new System.Windows.Forms.Panel();
            this.labelLine = new System.Windows.Forms.Label();
            this.panelBroswerInterface = new System.Windows.Forms.Panel();
            this.panelPlots = new System.Windows.Forms.Panel();
            this.panelWorkings = new System.Windows.Forms.Panel();
            this.panelDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRootSettings
            // 
            this.panelRootSettings.Location = new System.Drawing.Point(1000, 13);
            this.panelRootSettings.Name = "panelRootSettings";
            this.panelRootSettings.Size = new System.Drawing.Size(60, 60);
            this.panelRootSettings.TabIndex = 5;
            this.panelRootSettings.Click += new System.EventHandler(this.panelRootSettings_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Location = new System.Drawing.Point(31, 10);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(182, 60);
            this.panelLogo.TabIndex = 4;
            // 
            // panelShutdown
            // 
            this.panelShutdown.Location = new System.Drawing.Point(1073, 13);
            this.panelShutdown.Name = "panelShutdown";
            this.panelShutdown.Size = new System.Drawing.Size(60, 60);
            this.panelShutdown.TabIndex = 2;
            this.panelShutdown.Click += new System.EventHandler(this.panelShutdown_Click);
            // 
            // labelSubLine
            // 
            this.labelSubLine.AutoSize = true;
            this.labelSubLine.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubLine.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelSubLine.Location = new System.Drawing.Point(4, 22);
            this.labelSubLine.Name = "labelSubLine";
            this.labelSubLine.Size = new System.Drawing.Size(77, 12);
            this.labelSubLine.TabIndex = 7;
            this.labelSubLine.Text = "labelSubLine";
            // 
            // labelTime
            // 
            this.labelTime.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelTime.Location = new System.Drawing.Point(1157, 8);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(114, 65);
            this.labelTime.TabIndex = 8;
            this.labelTime.Text = "23:59:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTime.Click += new System.EventHandler(this.LabelTime_Click);
            // 
            // panelDebug
            // 
            this.panelDebug.Controls.Add(this.labelLine);
            this.panelDebug.Controls.Add(this.labelSubLine);
            this.panelDebug.ForeColor = System.Drawing.Color.IndianRed;
            this.panelDebug.Location = new System.Drawing.Point(221, 8);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.Size = new System.Drawing.Size(515, 65);
            this.panelDebug.TabIndex = 10;
            // 
            // labelLine
            // 
            this.labelLine.AutoSize = true;
            this.labelLine.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLine.ForeColor = System.Drawing.Color.IndianRed;
            this.labelLine.Location = new System.Drawing.Point(4, 4);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(71, 14);
            this.labelLine.TabIndex = 10;
            this.labelLine.Text = "Debug Info";
            // 
            // panelBroswerInterface
            // 
            this.panelBroswerInterface.Location = new System.Drawing.Point(927, 13);
            this.panelBroswerInterface.Name = "panelBroswerInterface";
            this.panelBroswerInterface.Size = new System.Drawing.Size(60, 60);
            this.panelBroswerInterface.TabIndex = 11;
            this.panelBroswerInterface.Click += new System.EventHandler(this.panelBroswerInterface_Click);
            // 
            // panelPlots
            // 
            this.panelPlots.Location = new System.Drawing.Point(781, 13);
            this.panelPlots.Name = "panelPlots";
            this.panelPlots.Size = new System.Drawing.Size(60, 60);
            this.panelPlots.TabIndex = 12;
            this.panelPlots.Click += new System.EventHandler(this.panelPlots_Click);
            // 
            // panelWorkings
            // 
            this.panelWorkings.Location = new System.Drawing.Point(854, 13);
            this.panelWorkings.Name = "panelWorkings";
            this.panelWorkings.Size = new System.Drawing.Size(60, 60);
            this.panelWorkings.TabIndex = 13;
            this.panelWorkings.Click += new System.EventHandler(this.panelWorkings_Click);
            // 
            // FormTopBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1280, 80);
            this.Controls.Add(this.panelWorkings);
            this.Controls.Add(this.panelPlots);
            this.Controls.Add(this.panelBroswerInterface);
            this.Controls.Add(this.panelDebug);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.panelRootSettings);
            this.Controls.Add(this.panelLogo);
            this.Controls.Add(this.panelShutdown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormTopBar";
            this.Text = "FormTopBar";
            this.Load += new System.EventHandler(this.FormTopBar_Load);
            this.panelDebug.ResumeLayout(false);
            this.panelDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelShutdown;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelRootSettings;
        private System.Windows.Forms.Label labelSubLine;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Panel panelDebug;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.Panel panelBroswerInterface;
        private System.Windows.Forms.Panel panelPlots;
        private System.Windows.Forms.Panel panelWorkings;
    }
}