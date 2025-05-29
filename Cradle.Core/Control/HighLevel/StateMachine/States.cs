using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public enum ControlState
    {
        Null,
        Emergency,

        WaitMarch,

        Normal,

        ManualOperations,
        LoadUnload,

        //GPIx21
        IOConfig,
        //GPFx21

        CutOff,
        Sharpening,

        CradleJog,
        CradleJogManualOperations,
        CradleJogLoadUnload,

        CradleRewind
    }
}