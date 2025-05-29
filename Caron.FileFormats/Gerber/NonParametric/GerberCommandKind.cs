//MMIx16
namespace Caron.FileFormats.Gerber.NonParametric
{
    public static class GerberCommandKind
    {
        /// <summary>Knife up (same as M15).</summary>
        public const string A = nameof(A);


        /// <summary>Knife down (same as M14).</summary>
        public const string B = nameof(B);


        /// <summary>Pen down.</summary>
        public const string D1 = nameof(D1);


        /// <summary>Pen up.</summary>
        public const string D2 = nameof(D2);


        /// <summary>Light source (same as Q).</summary>
        public const string D4 = nameof(D4);


        /// <summary>Flick notch (same as M68).</summary>
        public const string E = nameof(E);


        /// <summary>Set feed rate.</summary>
        public const string F = nameof(F);


        /// <summary>Set origin point.</summary>
        public const string G04 = nameof(G04);


        /// <summary>Select 3.3 English data.</summary>
        public const string G70 = nameof(G70);


        /// <summary>Select 5.1 Metric data format.</summary>
        public const string G71 = nameof(G71);


        /// <summary>Identifies GERBER cutter data (4.2 format).</summary>
        public const string G91 = nameof(G91);


        /// <summary>File identifier.</summary>
        public const string H = nameof(H);


        /// <summary>Begin slowdown (same as M25).</summary>
        public const string L = nameof(L);


        /// <summary>EOF (end of file).</summary>
        public const string M0 = nameof(M0);


        /// <summary>Program stop.</summary>
        public const string M00 = nameof(M00);


        /// <summary>Optional stop.</summary>
        public const string M01 = nameof(M01);


        /// <summary>Knife down (same as B).</summary>
        public const string M14 = nameof(M14);


        /// <summary>Knife up (same as A).</summary>
        public const string M15 = nameof(M15);


        /// <summary>Maximum advance.</summary>
        public const string M17 = nameof(M17);


        /// <summary>Inhibit next overcut.</summary>
        public const string M18 = nameof(M18);


        /// <summary>Ignore overcut and advance.</summary>
        public const string M19 = nameof(M19);


        /// <summary>Message stop (displayed on OCT).</summary>
        public const string M20 = nameof(M20);


        /// <summary>Run part at reduced velocity (same as L).</summary>
        public const string M25 = nameof(M25);


        /// <summary>Restore normal velocity (same as O).</summary>
        public const string M26 = nameof(M26);


        /// <summary>Rewind data file (return).</summary>
        public const string M30 = nameof(M30);


        /// <summary>Labeler data fellows.</summary>
        public const string M31 = nameof(M31);


        /// <summary>Enable automatic sharpen.</summary>
        public const string M40 = nameof(M40);


        /// <summary>Disable automatic sharpen.</summary>
        public const string M41 = nameof(M41);


        /// <summary>Sharpen.</summary>
        public const string M42 = nameof(M42);


        /// <summary>Drill (same as R).</summary>
        public const string M43 = nameof(M43);


        /// <summary>auxiliary drill.</summary>
        public const string M44 = nameof(M44);


        /// <summary>Lift and plunge corner.</summary>
        public const string M46 = nameof(M46);


        /// <summary>Turn off knife intelligence.</summary>
        public const string M47 = nameof(M47);


        /// <summary>Turn on knife intelligence.</summary>
        public const string M48 = nameof(M48);


        /// <summary>Null knife intelligence.</summary>
        public const string M51 = nameof(M51);


        /// <summary>Run part at 95 % velocity.</summary>
        public const string M60 = nameof(M60);


        /// <summary>Run part at 90 % velocity.</summary>
        public const string M61 = nameof(M61);


        /// <summary>Run part at 85 % velocity.</summary>
        public const string M62 = nameof(M62);


        /// <summary>Run part at 80 % velocity.</summary>
        public const string M63 = nameof(M63);


        /// <summary>Run part at 75 % velocity.</summary>
        public const string M64 = nameof(M64);


        /// <summary>Run part at 70 % velocity.</summary>
        public const string M65 = nameof(M65);


        /// <summary>Run part at 65 % velocity.</summary>
        public const string M66 = nameof(M66);


        /// <summary>Run part at 60 % velocity.</summary>
        public const string M67 = nameof(M67);


        /// <summary>Special notch (same as E).</summary>
        public const string M68 = nameof(M68);


        /// <summary>Conveyor bite.</summary>
        public const string M69 = nameof(M69);


        /// <summary>Origin.</summary>
        public const string M70 = nameof(M70);


        /// <summary>Sequence number of piece.</summary>
        public const string N = nameof(N);


        /// <summary>Resume normal speed (same as M26).</summary>
        public const string O = nameof(O);


        /// <summary>Establish light as tool (same as D4).</summary>
        public const string Q = nameof(Q);


        /// <summary>Drill (same as M43).</summary>
        public const string R = nameof(R);


        /// <summary>Precedes X coordinate area.</summary>
        public const string X = nameof(X);


        /// <summary>Precedes Y coordinate area.</summary>
        public const string Y = nameof(Y);


        /// <summary>Bite size identifier.</summary>
        public const string Z = nameof(Z);


        /// <summary>Block delete.</summary>
        public const string Slash = "/";


        /// <summary>EOB (end of block).</summary>
        public const string Star = "*";
    }
}
