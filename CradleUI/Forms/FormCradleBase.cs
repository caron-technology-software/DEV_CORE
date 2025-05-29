using System;

using Machine.UI.Common;

namespace Caron.Cradle.UI
{
    public class FormCradleBase : FormMachineBase
    {
        protected static Supervisor Supervisor { get; set; }

        protected bool SyncStatusOnLoad { get; set; } = false;

        public FormCradleBase()
        {
            //--
        }

        public static void SetSupervisor(Supervisor supervisor)
        {
            Supervisor = supervisor;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(100, 100);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCradleBase";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }
    }
}
