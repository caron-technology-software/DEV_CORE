using System;
using System.Linq;
using System.Text;
using System.Threading;

using Machine.Common;

using Caron.Cradle.Control;

namespace Caron.Cradle.UI
{
    public class UI
    {
        public UserType CurrentUserTypeLogged { get; set; } = UserType.Null;
        public bool UserLogged => CurrentUserTypeLogged != UserType.Null;
        public bool EtherCatError { get; internal set; } = false;
        public bool MachineEnduranceWarningAlreadyShowed { get; internal set; } = false;

        public bool FormsInitilized { get; internal set; } = false;
        public bool PanelsInitilized { get; internal set; } = false;
        public bool Initialized { get => FormsInitilized && PanelsInitilized; }

        private volatile StateUI state;
        public StateUI State { get => state; internal set => state = value; }

        private volatile StateUI precedentState;
        public StateUI PrecedentState { get => precedentState; internal set => precedentState = value; }

        public UiFormsCollection Forms { get; internal set; } = new UiFormsCollection();
        public UiPanelsCollection Panels { get; internal set; } = new UiPanelsCollection();

    }
}
