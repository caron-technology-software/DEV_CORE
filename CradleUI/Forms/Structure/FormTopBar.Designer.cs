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
            panelRootSettings = new System.Windows.Forms.Panel();
            panelLogo = new System.Windows.Forms.Panel();
            panelShutdown = new System.Windows.Forms.Panel();
            labelSubLine = new System.Windows.Forms.Label();
            labelTime = new System.Windows.Forms.Label();
            panelDebug = new System.Windows.Forms.Panel();
            labelLine = new System.Windows.Forms.Label();
            panelBroswerInterface = new System.Windows.Forms.Panel();
            panelPlots = new System.Windows.Forms.Panel();
            panelWorkings = new System.Windows.Forms.Panel();
            panelDebug.SuspendLayout();
            SuspendLayout();
            // 
            // panelRootSettings
            // 
            panelRootSettings.Location = new System.Drawing.Point(990, 12);
            panelRootSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelRootSettings.Name = "panelRootSettings";
            panelRootSettings.Size = new System.Drawing.Size(70, 69);
            panelRootSettings.TabIndex = 5;
            panelRootSettings.Click += panelRootSettings_Click;
            // 
            // panelLogo
            // 
            panelLogo.Location = new System.Drawing.Point(36, 12);
            panelLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new System.Drawing.Size(212, 69);
            panelLogo.TabIndex = 4;
            // 
            // panelShutdown
            // 
            panelShutdown.Location = new System.Drawing.Point(1068, 12);
            panelShutdown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelShutdown.Name = "panelShutdown";
            panelShutdown.Size = new System.Drawing.Size(70, 69);
            panelShutdown.TabIndex = 2;
            panelShutdown.Click += panelShutdown_Click;
            // 
            // labelSubLine
            // 
            labelSubLine.AutoSize = true;
            labelSubLine.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelSubLine.ForeColor = System.Drawing.SystemColors.ControlLight;
            labelSubLine.Location = new System.Drawing.Point(5, 25);
            labelSubLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSubLine.Name = "labelSubLine";
            labelSubLine.Size = new System.Drawing.Size(77, 12);
            labelSubLine.TabIndex = 7;
            labelSubLine.Text = "labelSubLine";
            // 
            // labelTime
            // 
            labelTime.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelTime.ForeColor = System.Drawing.SystemColors.ControlLight;
            labelTime.Location = new System.Drawing.Point(1146, 6);
            labelTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTime.Name = "labelTime";
            labelTime.Size = new System.Drawing.Size(133, 75);
            labelTime.TabIndex = 8;
            labelTime.Text = "23:59:00";
            labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelTime.Click += LabelTime_Click;
            // 
            // panelDebug
            // 
            panelDebug.Controls.Add(labelLine);
            panelDebug.Controls.Add(labelSubLine);
            panelDebug.ForeColor = System.Drawing.Color.IndianRed;
            panelDebug.Location = new System.Drawing.Point(258, 9);
            panelDebug.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelDebug.Name = "panelDebug";
            panelDebug.Size = new System.Drawing.Size(475, 75);
            panelDebug.TabIndex = 10;
            // 
            // labelLine
            // 
            labelLine.AutoSize = true;
            labelLine.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelLine.ForeColor = System.Drawing.Color.IndianRed;
            labelLine.Location = new System.Drawing.Point(5, 5);
            labelLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLine.Name = "labelLine";
            labelLine.Size = new System.Drawing.Size(71, 14);
            labelLine.TabIndex = 10;
            labelLine.Text = "Debug Info";
            // 
            // panelBroswerInterface
            // 
            panelBroswerInterface.Location = new System.Drawing.Point(912, 12);
            panelBroswerInterface.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBroswerInterface.Name = "panelBroswerInterface";
            panelBroswerInterface.Size = new System.Drawing.Size(70, 69);
            panelBroswerInterface.TabIndex = 11;
            panelBroswerInterface.Click += panelBroswerInterface_Click;
            // 
            // panelPlots
            // 
            panelPlots.Location = new System.Drawing.Point(756, 12);
            panelPlots.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelPlots.Name = "panelPlots";
            panelPlots.Size = new System.Drawing.Size(70, 69);
            panelPlots.TabIndex = 12;
            panelPlots.Click += panelPlots_Click;
            // 
            // panelWorkings
            // 
            panelWorkings.Location = new System.Drawing.Point(834, 12);
            panelWorkings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelWorkings.Name = "panelWorkings";
            panelWorkings.Size = new System.Drawing.Size(70, 69);
            panelWorkings.TabIndex = 13;
            panelWorkings.Click += panelWorkings_Click;
            // 
            // FormTopBar
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(26, 37, 43);
            ClientSize = new System.Drawing.Size(1493, 92);
            Controls.Add(panelWorkings);
            Controls.Add(panelPlots);
            Controls.Add(panelBroswerInterface);
            Controls.Add(panelDebug);
            Controls.Add(labelTime);
            Controls.Add(panelRootSettings);
            Controls.Add(panelLogo);
            Controls.Add(panelShutdown);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(2);
            Name = "FormTopBar";
            Text = "FormTopBar";
            Load += FormTopBar_Load;
            panelDebug.ResumeLayout(false);
            panelDebug.PerformLayout();
            ResumeLayout(false);
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