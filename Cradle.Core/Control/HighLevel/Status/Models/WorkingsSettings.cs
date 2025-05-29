using Machine.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    [Synchronization()]
    public class WorkingsSettings
    {
        public string SoftwareVersion { get; set; }
        public List<WorkingSetting> Items { get; set; } = new List<WorkingSetting>();

        //GPIx243 
        public int GeneratedEthercatErrorAtStart;
        //GPFx243 

        public WorkingsSettings()
        {
            //--
        }
    }

    [Synchronization()]
    public class WorkingSetting
    {
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public WorkingParameters Parameters { get; set; }

        public WorkingSetting()
        {
            Guid = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Parameters = new WorkingParameters();
        }

        public WorkingSetting(WorkingParameters parameters)
        {
            Guid = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Parameters = parameters;
        }
    }

    [Synchronization()]
    public class WorkingParameters
    {
        public uint PreFeedMaterial { get; set; }
        public double CutterVelocity { get; set; }
        public double CradleScalingFactor { get; set; }
        public bool StraightRoller { get; set; }
        public bool PhotocellAlignmentEnabled { get; set; }
        public bool PhotocellMaterialPresenceEnabled { get; set; }
        //GPIx101 4)
        public bool EnablePhotocellRollPresence { get; set; }
        //GPFx101
        public WorkingMode WorkingMode { get; set; }

        public WorkingParameters()
        {
            PreFeedMaterial = 0;
            CutterVelocity = 1.0;
            CradleScalingFactor = 1.0;
            StraightRoller = true;
            PhotocellAlignmentEnabled = true;
            PhotocellMaterialPresenceEnabled = true;
            EnablePhotocellRollPresence = false;//MMIx05
            WorkingMode = WorkingMode.Encoder;
        }
    }
}
