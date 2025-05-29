using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public class ControlStatus
    {
        public static Guid GuidSession { get; } = Guid.NewGuid();

        public WorkingContext WorkingContext { get; set; } = new WorkingContext();
        public MachineConfiguration Configuration { get; set; } = new MachineConfiguration();
        public MachineStatus Status { get; set; } = new MachineStatus();
        public MachineSettings Settings { get; set; } = new MachineSettings();
        public WorkingsSettings WorkingsSettings { get; set; } = new WorkingsSettings();
        public MachineEndurance MachineEndurance { get; set; } = new MachineEndurance();
        public Working Working { get; set; } = new Working();
        public WorkingStatus WorkingStatus { get; set; } = new WorkingStatus();
        public TasksStatus TasksStatus { get; set; } = new TasksStatus();
        public Signals Signals { get; set; } = new Signals();
        public Errors Errors { get; set; } = new Errors();

        //GPIx164
        private volatile bool heartbeatEnabled = false;
        public bool HeartbeatEnabled
        {
            get
            {
                return heartbeatEnabled;
            }
            set
            {
                heartbeatEnabled = value;
            }
        }
        //GPFx164

        public ControlStatus()
        {
            //--
        }
    }
}
