using Machine.UI.Common;

namespace Caron.Cradle.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelMenu = new Machine.UI.Common.MachinePanel();
            this.panelTopBar = new Machine.UI.Common.MachinePanel();
            this.panelCentered = new Machine.UI.Common.MachinePanel();
            this.panelFull = new Machine.UI.Common.MachinePanel();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.CurrentFormShowing = "";
            this.panelMenu.Location = new System.Drawing.Point(0, 80);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(135, 720);
            this.panelMenu.TabIndex = 0;
            // 
            // panelTopBar
            // 
            this.panelTopBar.CurrentFormShowing = "";
            this.panelTopBar.Location = new System.Drawing.Point(0, 0);
            this.panelTopBar.Margin = new System.Windows.Forms.Padding(2);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(1280, 80);
            this.panelTopBar.TabIndex = 1;
            // 
            // panelCentered
            // 
            this.panelCentered.CurrentFormShowing = "";
            this.panelCentered.Location = new System.Drawing.Point(135, 80);
            this.panelCentered.Name = "panelCentered";
            this.panelCentered.Size = new System.Drawing.Size(1145, 720);
            this.panelCentered.TabIndex = 2;
            // 
            // panelFull
            // 
            this.panelFull.CurrentFormShowing = "";
            this.panelFull.Location = new System.Drawing.Point(0, 80);
            this.panelFull.Name = "panelFull";
            this.panelFull.Size = new System.Drawing.Size(1280, 720);
            this.panelFull.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelCentered);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelFull);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Common.MachinePanel panelMenu;
        private Machine.UI.Common.MachinePanel panelTopBar;
        private Machine.UI.Common.MachinePanel panelCentered;
        private Machine.UI.Common.MachinePanel panelFull;
    }
}

