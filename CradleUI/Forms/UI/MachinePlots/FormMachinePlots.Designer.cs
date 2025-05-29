namespace Caron.Cradle.UI
{
    partial class FormMachinePlots : FormCradleBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMachinePlots));
            this.panelForm = new System.Windows.Forms.Panel();
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.panelRefresh = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.machineTransparentPanel = new Machine.UI.Controls.Panels.MachinePanelTransparent();
            this.plot = new ScottPlot.FormsPlot();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.comboBoxItems);
            this.panelForm.Controls.Add(this.panelRefresh);
            this.panelForm.Controls.Add(this.labelTitle);
            this.panelForm.Controls.Add(this.cbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1280, 80);
            this.panelForm.TabIndex = 10;
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(772, 24);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(296, 30);
            this.comboBoxItems.TabIndex = 14;
            // 
            // panelRefresh
            // 
            this.panelRefresh.Location = new System.Drawing.Point(1152, 11);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Size = new System.Drawing.Size(60, 60);
            this.panelRefresh.TabIndex = 13;
            this.panelRefresh.Click += new System.EventHandler(this.panelRefresh_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(95, 18);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(110, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Charts";
            // 
            // cbReturn
            // 
            this.cbReturn.Active = false;
            this.cbReturn.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.ActiveBackgroundImage")));
            this.cbReturn.BackColor = System.Drawing.Color.Transparent;
            this.cbReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbReturn.BackgroundImage")));
            this.cbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbReturn.ButtonSize = 60;
            this.cbReturn.FlatAppearance.BorderSize = 0;
            this.cbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbReturn.ForeColor = System.Drawing.Color.Transparent;
            this.cbReturn.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.InactiveBackgroundImage")));
            this.cbReturn.Location = new System.Drawing.Point(9, 9);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(60, 60);
            this.cbReturn.StateChangeActivated = false;
            this.cbReturn.TabIndex = 7;
            this.cbReturn.TabStop = false;
            this.cbReturn.UseVisualStyleBackColor = false;
            this.cbReturn.Click += new System.EventHandler(this.cbReturn_Click);
            // 
            // machineTransparentPanel
            // 
            this.machineTransparentPanel.Location = new System.Drawing.Point(12, 86);
            this.machineTransparentPanel.Name = "machineTransparentPanel";
            this.machineTransparentPanel.Size = new System.Drawing.Size(1256, 702);
            this.machineTransparentPanel.TabIndex = 13;
            // 
            // plot
            // 
            this.plot.Location = new System.Drawing.Point(34, 96);
            this.plot.Name = "plot";
            this.plot.Size = new System.Drawing.Size(1234, 683);
            this.plot.TabIndex = 11;
            // 
            // FormMachineCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.plot);
            this.Controls.Add(this.machineTransparentPanel);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMachineCharts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMachineCharts";
            this.Load += new System.EventHandler(this.FormMachineCharts_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButton cbReturn;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelRefresh;
        private ScottPlot.FormsPlot plot;
        private Machine.UI.Controls.Panels.MachinePanelTransparent machineTransparentPanel;
        private System.Windows.Forms.ComboBox comboBoxItems;
    }
}