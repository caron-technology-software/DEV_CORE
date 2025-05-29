using ProRob;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Machine.UI.Common
{
    public class FormMachineBase : Form
    {
        public object UIStateParameter { get; set; } = null;

        public static void SetPanelFromBaseMachineForm(MachinePanel panel, FormMachineBase form, bool forceReload = false)
        {
            try
            {
                if (forceReload || panel.CurrentFormShowing != form.Name)
                {
                    panel.SuspendLayout();

                    if (panel.Controls.Count > 0)
                    {
                        panel.Controls[0].Visible = false;
                    }
                    panel.Controls.Clear();

                    panel.CurrentFormShowing = form.Name;

                    Console.WriteLine($"[UI] CurrentFormShowing: {panel.CurrentFormShowing}");

                    form.SuspendLayout();

                    form.TopLevel = false;
                    form.AutoScroll = true;

                    panel.Controls.Add(form);

                    form.Opacity = 0;
                    form.Show();

                    form.ResumeLayout();

                    panel.Show();

                    panel.ResumeLayout();

                    ProConsole.WriteTitle($"LOADED {form.Name}", ConsoleColor.Yellow);
                }
            }
            catch
            {
                //--
            }
        }

        public static void DisablePanel(MachinePanel panel)
        {
            panel?.Invoke((MethodInvoker)delegate ()
            {
                panel.CurrentFormShowing = String.Empty;

                panel.SendToBack();
                panel.Visible = false;
            }); 
        }

        public FormMachineBase()
        {
            ShowInTaskbar = false;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected virtual void UpdateUIForm()
        {
            ProRob.ProConsole.WriteLine($"[INFO] {Name}: UpdateUIForm not implemented", ConsoleColor.DarkMagenta);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible == true)
            {
                UpdateUIForm();
            }
        }

        protected override void OnShown(EventArgs e)
        {
#if DEBUG
            Console.WriteLine("[FormMachineBase] OnShown()");
#endif
            base.OnShown(e);
            Application.DoEvents();
        }

        private void InitializeComponent()
        {

            this.SuspendLayout();
            // 
            // FormMachineBase
            // 
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMachineBase";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }

        protected static void MachineKitPreloadControls()
        {
            ThreadPool.QueueUserWorkItem((t) =>
            {
                Thread.Sleep(100);

                try
                {
                    foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                    {
                        foreach (var method in type.GetMethods(BindingFlags.DeclaredOnly |
                                            BindingFlags.NonPublic |
                                            BindingFlags.Public | BindingFlags.Instance |
                                            BindingFlags.Static))
                        {
                            System.Runtime.CompilerServices.RuntimeHelpers.PrepareMethod(method.MethodHandle);
                            //Console.WriteLine(method.Name);
                        }
                    }
                }
                catch (Exception) { }
            });
        }
    }
}
