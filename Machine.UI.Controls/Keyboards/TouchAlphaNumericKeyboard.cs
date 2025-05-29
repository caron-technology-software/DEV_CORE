#undef DISABLE_BUTTONS_IF_NOT_USED

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;
using System.ComponentModel;

namespace Machine.UI.Controls
{
    public partial class TouchAlphaNumericKeyboard : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyName { get; private set; } = String.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TouchKeyboardType TouchKeyboardType { get; private set; }

        private const int MillisecondsTimeout = 25; //[ms]

        private string stringValue = String.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                string stringValueFix="";
                stringValue = value;
                Console.WriteLine($"[TouchKeyboard]");
                int found = 0;

                //GPIx42
                //toglie caratteri dal carriage return in poi se c'è:
                found = stringValue.IndexOf($"\r");
                if (found > 0)
                {
                    //Console.WriteLine($"1- {stringValueFix} ");
                    stringValue = stringValue.Substring(0, found).Trim();
                    Console.WriteLine($"1- {stringValue} ");
                }
                Console.WriteLine();

                //GPIx177 DOPO 11/12/2023
                //¾ -> .   //\u0010¾ -> :			
                //» -> +   //\u0010» -> *
                //\u0010A -> A  //A -> a
                //½ -> -   //\u0010½ -> _

                var replacement = stringValue;
                replacement = replacement.Replace("\u0010¾", ":");
                replacement = replacement.Replace("¾", ".");
                replacement = replacement.Replace("\u0010»", "*");
                replacement = replacement.Replace("»", "+");
                replacement = replacement.Replace("\u0010½", "_");
                replacement = replacement.Replace("½", "-");

                replacement = replacement.Replace("\u0011À", "\u0011@");

                //replacement = replacement.Replace("?", "@");  //? qual'è il carattere visualizzato da @ ???

                #region minuscolo -> maiuscolo (scielto sempre maiuscolo):
                //replacement = replacement.Replace("Q", "q");
                //replacement = replacement.Replace("\u0010q", "Q");
                //replacement = replacement.Replace("W", "w");
                //replacement = replacement.Replace("\u0010w", "W");
                //replacement = replacement.Replace("E", "e");
                //replacement = replacement.Replace("\u0010e", "E");
                //replacement = replacement.Replace("R", "r");
                //replacement = replacement.Replace("\u0010r", "R");
                //replacement = replacement.Replace("T", "t");
                //replacement = replacement.Replace("\u0010t", "T");
                //replacement = replacement.Replace("Y", "y");
                //replacement = replacement.Replace("\u0010y", "Y");
                //replacement = replacement.Replace("U", "u");
                //replacement = replacement.Replace("\u0010u", "U");
                //replacement = replacement.Replace("I", "i");
                //replacement = replacement.Replace("\u0010i", "I");
                //replacement = replacement.Replace("O", "o");
                //replacement = replacement.Replace("\u0010o", "O");
                //replacement = replacement.Replace("P", "p");
                //replacement = replacement.Replace("\u0010p", "P");

                //replacement = replacement.Replace("A", "a");
                //replacement = replacement.Replace("\u0010a", "A");
                //replacement = replacement.Replace("S", "s");
                //replacement = replacement.Replace("\u0010s", "S");
                //replacement = replacement.Replace("D", "d");
                //replacement = replacement.Replace("\u0010d", "D");
                //replacement = replacement.Replace("F", "f");
                //replacement = replacement.Replace("\u0010f", "F");
                //replacement = replacement.Replace("G", "g");
                //replacement = replacement.Replace("\u0010g", "G");
                //replacement = replacement.Replace("H", "h");
                //replacement = replacement.Replace("\u0010h", "H");
                //replacement = replacement.Replace("J", "j");
                //replacement = replacement.Replace("\u0010j", "J");
                //replacement = replacement.Replace("K", "k");
                //replacement = replacement.Replace("\u0010k", "K");
                //replacement = replacement.Replace("L", "l");
                //replacement = replacement.Replace("\u0010l", "L");

                //replacement = replacement.Replace("Z", "z");
                //replacement = replacement.Replace("\u0010z", "Z");
                //replacement = replacement.Replace("X", "x");
                //replacement = replacement.Replace("\u0010x", "X");
                //replacement = replacement.Replace("C", "c");
                //replacement = replacement.Replace("\u0010c", "C");
                //replacement = replacement.Replace("V", "v");
                //replacement = replacement.Replace("\u0010v", "V");
                //replacement = replacement.Replace("B", "b");
                //replacement = replacement.Replace("\u0010b", "B");
                //replacement = replacement.Replace("N", "n");
                //replacement = replacement.Replace("\u0010n", "N");
                //replacement = replacement.Replace("M", "m");
                //replacement = replacement.Replace("\u0010m", "M");
                #endregion

                replacement = replacement.Replace("\u0010", "");
                replacement = replacement.Replace("\u0011", "");

                // Replace invalid characters with "" strings (da abilitare per filtrare caratteri non compresi in @"[^A-Za-z0-9:\.*+_-]").
                try
                {
                    //////////replacement = Regex.Replace(replacement, @"[^A-Za-z0-9:\.*+_-@]", "",     //[^A-Za-z0-9:\.*+_-]   //[^\w\.@-]
                    //////////                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
                }
                // If we timeout when replacing invalid characters,
                // we should return Empty.
                catch (RegexMatchTimeoutException)
                {
                    replacement = "ERROR!";
                }

                //replacement = replacement.Replace("½", "-");
                if (!replacement.Equals("ERROR!"))
                {
                    stringValueFix = replacement;
                }
                //GPFx42

                //toglie caratteri dal carriage return in poi se c'è:
                found = stringValueFix.IndexOf($"\r");
                if (found > 0)
                {
                    //Console.WriteLine($"1- {stringValueFix} ");
                    stringValueFix = stringValueFix.Substring(0, found).Trim();
                    Console.WriteLine($"2- {stringValueFix} ");
                }

                var bytes = UTF8Encoding.UTF8.GetBytes(stringValue);
                foreach (var item in bytes)
                {
                    Console.WriteLine($"{item} ");
                }
                Console.WriteLine($"3- {stringValueFix} ");
                //GPFx177

                if (!(stringValueFix.Trim().Equals("")))
                {
                    stringValue = stringValueFix;
                }

                //toglie caratteri dal carriage return in poi se c'è:
                found = stringValue.IndexOf($"\r");
                if (found > 0)
                {
                    //Console.WriteLine($"1- {stringValueFix} ");
                    stringValue = stringValue.Substring(0, found).Trim();
                    Console.WriteLine($"4- {stringValue} ");
                }
                Console.WriteLine();

                try
                {
                    txtValue.Text = stringValue;
                }
                catch
                {
                    txtValue?.Invoke((MethodInvoker)delegate ()
                    {
                        txtValue.Text = stringValue;
                    });
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CapsLock { get; private set; } = false;
        private readonly List<Button> charKeys = new List<Button>();
        private readonly List<ButtonUpperLowerKey> symbolKeys = new List<ButtonUpperLowerKey>();

        private volatile bool taskRunning = false;
        private StreamHID StreamHID { get; set; }

        public TouchAlphaNumericKeyboard(string propertyName, string propertyValue, TouchKeyboardType touchKeyboardType = TouchKeyboardType.NoSymbols, StreamHID streamHID = null)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            TouchKeyboardType = touchKeyboardType;

            PropertyName = propertyName;
            labelPropertyName.Text = PropertyName;
            StringValue = propertyValue;

            StreamHID = streamHID;

            txtValue.BorderStyle = BorderStyle.FixedSingle;

            charKeys.AddRange(new List<Button>(){
                    btnQ,btnW,btnE,btnR,btnT,btnY,btnU,btnI,btnO,btnP,
                    btnA,btnS,btnD,btnF,btnG,btnH,btnJ,btnK,btnL,
                    btnZ,btnX,btnC,btnV,btnB,btnN,btnM
            });

            if (TouchKeyboardType == TouchKeyboardType.AllKeys)
            {
                symbolKeys.AddRange(new List<ButtonUpperLowerKey>()
                {
                    new  ButtonUpperLowerKey(btn1, '1', '!'),
                    new  ButtonUpperLowerKey(btn2, '2', '"'),
                    new  ButtonUpperLowerKey(btn3, '3', '£'),
                    new  ButtonUpperLowerKey(btn4, '4', '$'),
                    new  ButtonUpperLowerKey(btn5, '5', '%'),
                    new  ButtonUpperLowerKey(btn6, "6", "&&"),
                    new  ButtonUpperLowerKey(btn7, '7', '/'),
                    new  ButtonUpperLowerKey(btn8, '8', '('),
                    new  ButtonUpperLowerKey(btn9, '9', ')'),
                    new  ButtonUpperLowerKey(btn0, '0', '='),

                    new  ButtonUpperLowerKey(btnDotDoubleDot, '.', ':'),
                    new  ButtonUpperLowerKey(btnCommaSemiColumn, ',', ';'),
                    new  ButtonUpperLowerKey(btnMinusUnderscore, '-', '_'),
                    new  ButtonUpperLowerKey(btnPlusAsterisk, '+', '*'),
                    new  ButtonUpperLowerKey(btnLeGe, '<', '>'),

                    new  ButtonUpperLowerKey(btnSquareBraketLeft, '[', '{'),
                    new  ButtonUpperLowerKey(btnSquareBraketRight, ']', '}'),
                    new  ButtonUpperLowerKey(btnApostrophe, '\'', '?'),
                    new  ButtonUpperLowerKey(btnAt, '@', '\\'),
               });
            }
            else
            {
#if DISABLE_BUTTONS_IF_NOT_USED
                btnDotDoubleDot.Enabled = false;
                btnCommaSemiColumn.Enabled = false;
                btnMinusUnderscore.Enabled = false;
                btnPlusAsterisk.Enabled = false;
                btnLeGe.Enabled = false;
                btnSquareBraketLeft.Enabled = false;
                btnSquareBraketRight.Enabled = false;
                btnApostrophe.Enabled = false;
                btnAt.Enabled = false;
#endif
                symbolKeys.AddRange(new List<ButtonUpperLowerKey>()
                {
                    new ButtonUpperLowerKey(btn1, '1'),
                    new ButtonUpperLowerKey(btn2, '2'),
                    new ButtonUpperLowerKey(btn3, '3'),
                    new ButtonUpperLowerKey(btn4, '4'),
                    new ButtonUpperLowerKey(btn5, '5'),
                    new ButtonUpperLowerKey(btn6, '6'),
                    new ButtonUpperLowerKey(btn7, '7'),
                    new ButtonUpperLowerKey(btn8, '8'),
                    new ButtonUpperLowerKey(btn9, '9'),
                    new ButtonUpperLowerKey(btn0, '0'),
                });
            }

            btnShift.Text = String.Empty;
            btnShift.Image = IconChar.ArrowUp.ToBitmap(Color.DarkGray, TouchKeyboard.IconSize);
            btnShift2.Text = String.Empty;
            btnShift2.Image = IconChar.ArrowUp.ToBitmap(Color.DarkGray, TouchKeyboard.IconSize);
            btnCancelDigit.Text = String.Empty;
            btnCancelDigit.Image = IconChar.ArrowLeft.ToBitmap(Color.DarkGray, TouchKeyboard.IconSize);
            btnPaste.Text = String.Empty;
            btnPaste.Image = IconChar.Paste.ToBitmap(Color.DarkGray, TouchKeyboard.IconSize);

            foreach (var key in charKeys)
            {
                key.MouseClick += KeyCallback;
            }

            foreach (var key in symbolKeys)
            {
                key.Button.MouseClick += KeyCallback;
            }

            panelKeyboard.Refresh();

            RefreshKeys();
            Refresh();

            TopLevel = true;
            TopMost = true;

            if (streamHID != null)
            {
                taskRunning = true;

                Task.Run(() =>
                {
                    StreamHID.Reset();

                    while (taskRunning)
                    {
                        if (string.IsNullOrEmpty(StreamHID.Text) == false)
                        {
                            StringValue = StreamHID.Text;
                            StreamHID.Reset();
                        }

                        Thread.Sleep(MillisecondsTimeout);
                    }
                });
            }
        }

        private void KeyCallback(object sender, EventArgs e)
        {
            StringValue += ((Button)sender).Text;
        }

        private void RefreshKeys()
        {
            foreach (var key in charKeys)
            {
                if (CapsLock)
                {
                    key.Text = key.Text.ToUpper();
                }
                else
                {
                    key.Text = key.Text.ToLower();
                }
            }

            foreach (var key in symbolKeys)
            {
                if (CapsLock)
                {
                    key.Button.Text = $"{key.UpperChar}";
                }
                else
                {
                    key.Button.Text = $"{key.LowerChar}";
                }
            }
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            CapsLock = !CapsLock;
            RefreshKeys();
        }

        private void btnShift2_Click(object sender, EventArgs e)
        {
            CapsLock = !CapsLock;
            RefreshKeys();
        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            StringValue += " ";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            StringValue = String.Empty;
        }

        private void btnCancelDigit_Click(object sender, EventArgs e)
        {
            if (StringValue.Length > 0)
            {
                StringValue = StringValue.Substring(0, StringValue.Length - 1);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            taskRunning = false;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            taskRunning = false;

            this.Close();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            StringValue = Clipboard.GetText(TextDataFormat.Text);
        }
    }
}
