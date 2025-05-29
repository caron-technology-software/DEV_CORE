using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public enum TouchKeyboardType
    {
        AllKeys = 0,
        NoSymbols
    }

    public class ButtonUpperLowerKey
    {
        public Button Button { get; set; }
        public string LowerChar { get; set; }
        public string UpperChar { get; set; }

        public ButtonUpperLowerKey(Button button, char lowerChar, char upperChar)
        {
            Button = button;
            LowerChar = lowerChar.ToString();
            UpperChar = upperChar.ToString();
        }

        public ButtonUpperLowerKey(Button button, char lowerChar)
        {
            Button = button;
            LowerChar = lowerChar.ToString();
            UpperChar = LowerChar;
        }

        public ButtonUpperLowerKey(Button button, string lowerChar, string upperChar)
        {
            Button = button;
            LowerChar = lowerChar;
            UpperChar = upperChar;
        }
    }

    public class TouchKeyboard
    {
        public const int IconSize = 50;
    }
}
