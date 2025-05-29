using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Caron.Cradle.Control;

namespace Caron.Cradle.UI
{
    public partial class FormMain : FormCradleBase
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //------------------------------
            // SetPanels
            //------------------------------
            Supervisor.SetPanels(
                panelTopBar,
                panelMenu,
                panelFull,
                panelCentered);

            Supervisor.SetUIState(StateUI.Dashboard);

            //------------------------------
            // Machine configuration
            //------------------------------
            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                this.panelMenu.Location = new Point(1145, 80);
                this.panelCentered.Location = new Point(0, 80);
            }
            else
            {
                this.panelMenu.Location = new Point(0, 80);
                this.panelCentered.Location = new Point(135, 80);
            }

            //------------------------------
            // TaskUI
            //------------------------------
            Task.Run(() =>
            {
                TaskFormMainUI();
            });
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Console.WriteLine("[FormMain] OnShown()");
        }

        protected override void UpdateUIForm()
        {
            Console.WriteLine("Refreshing MainForm..");
            Refresh();
        }
    }
}
