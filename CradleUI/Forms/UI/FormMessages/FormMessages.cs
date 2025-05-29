using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caron.Cradle.UI
{
    public partial class FormMessages : FormCradleBase
    {
        public FormMessages()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                if (Supervisor.Control.HighLevel.Errors.EmergencyStatus)
                {
                    //labelTitle.Text = Localization.EmergencyStatus;
                    //labelMessage.Text = Localization.PleaseShutdownAndRestart;
                    //GPIx164
                    if (Supervisor.Control.LowLevel.Info.EmergencyState > 0)
                    {
                        switch (Supervisor.Control.LowLevel.Info.EmergencyState)
                        {
                            case 1:
                                labelTitle.Text = Localization.EmergencyStatus;
                                labelMessage.Text = Localization.DriverFault;
                                break;
                            case 2:
                                labelTitle.Text = Localization.EmergencyStatus;
                                labelMessage.Text = Localization.EtherCATFault;
                                break;
                            case 4:
                                labelTitle.Text = Localization.EmergencyStatus;
                                labelMessage.Text = Localization.ErrorHighLevelCommunication;
                                break;
                            default:
                                labelTitle.Text = Localization.EmergencyStatus;
                                if (Supervisor.Control.LowLevel.Info.HeartBeatState)
                                {
                                    labelMessage.Text = Localization.ErrorHighLevelCommunication;
                                }
                                else
                                { 
                                    labelMessage.Text = Localization.ErrorLowLevelCommunication;   //Localization.PleaseShutdownAndRestart;
                                }
                                break;
                        }
                    }
                    else
                    {
                        labelTitle.Text = Localization.EmergencyStatus;
                        if (Supervisor.Control.LowLevel.Info.HeartBeatState)
                        {
                            labelMessage.Text = Localization.ErrorHighLevelCommunication;
                        }
                        else
                        {
                            labelMessage.Text = Localization.ErrorLowLevelCommunication;   //Localization.PleaseShutdownAndRestart;
                        }
                    }
                    //GPFx164
                }
            }
        }
    }
}
