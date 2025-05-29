using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.Denninson
{
    public static partial class DenninsonConverter
    {
        public static class Constants
        {
            public static class Caron
            {
                public static class Headers
                {
                    public const string Section201 =
@"000 --------------------------------------------------------------
000 Spread Nr.
000 v   Section Nr.
000 v   v   Section begin 
000 v   v   v     Section end 
000 v   v   v     v     Marker ID 
000 v   v   v     v     v               Marker Filename 
000 V   V   V     V     V               V
";

                    public const string Section002 =
@"000 --------------------------------------------------------------
000 Order ID
000 v        Spread Nr.
000 v        v   Step Nr.
000 v        v   v   Section from
000 v        v   v   v   Section to
000 v        v   v   v   v   Nr. of ply
000 v        v   v   v   v   v   Color ID
000 v        v   v   v   v   v   v                         Roll ID
000 v        v   v   v   v   v   v                         v                    Roll Destination
000 v        v   v   v   v   v   v                         v                    v   Parameter-Set
000 v        v   v   v   v   v   v                         v                    v   v Cloth Allowance
000 v        v   v   v   v   v   v                         v                    v   v v    Spreading method
000 v        v   v   v   v   v   v                         v                    v   v v    v  Roll Length
000 v        v   v   v   v   v   v                         v                    v   v v    v  v      All.Beg
000 v        v   v   v   v   v   v                         v                    v   v v    v  v      v    Al.E
000 V        V   V   V   V   V   V                         V                    V   V V    V  V      V    V
";

                    public const string Section007 =
@"000 --------------------------------------------------------------
000 Order ID
000 v        General Allowance Begin of spread
000 v        v    General Allowance end of spread
000 V        V    V
";

                    public const string Section014 =
@"000 --------------------------------------------------------------
000 Number of overlapzone
000 v   Cut point(mm)
000 v   v     Spread point(mm)
000 V   V     V
";

                    public const string Section118 =
@"000 --------------------------------------------------------------
000 Order ID
000 v        Splice Allowance Begin of spread
000 v        v    Splice Allowance end of spread
000 V        V    V
";
                }
            }

            public static class Gerber
            {
                public static class Headers
                {
                    public const string Section201 =
@"000--------------------------------------------------------------
000 Spread Nr.Sections 001                                         
000 ³   Section Nr.                                                
000 ³   ³   Section length                                            
000 ³   ³   ³     Marker ID                                        
000 ³   ³   ³     ³               Marker Filename                      
000 V   V   V     V               V                                
";
                    public const string Section002 =
@"000--------------------------------------------------------------
000 Order ID                                              Step 002
000 ³        Spread Nr.
000 ³        ³   Step Nr.
000 ³        ³   ³   Spread Length
000 ³        ³   ³   ³     Section from 
000 ³        ³   ³   ³     ³   Section to 
000 ³        ³   ³   ³     ³   ³   Nr.of ply
000 ³        ³   ³   ³     ³   ³   ³   Color ID 
000 ³        ³   ³   ³     ³   ³   ³   ³                         Roll ID
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    Roll Destination
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   Parameter-Set
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ Cloth Allowance
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ ³    Spreading method
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ ³    ³  Roll Length
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ ³    ³  ³      All.Beg
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ ³    ³  ³      ³    Al.E
000 ³        ³   ³   ³     ³   ³   ³   ³                         ³                    ³   ³ ³    ³  ³      ³    |    Spread plies
000 V        V   V   V     V   V   V   V                         V                    V   V V    V  V      V    V    V
";

                    public const string Section007 =
@"000 --------------------------------------------------------------
000 Order ID               Allowans separate for begin and end 007
000 ³        General Allowance Begin of spread
000 ³        ³    General Allowance end of spread
000 V        V    V
";

                    public const string Section118 =
@"000 --------------------------------------------------------------
000 Order ID        Splice allowans separate for begin and end 118
000 |        Splice Allowance Begin of spread
000 |        |    Splice Allowance end of spread
000 V        V    V
";

                    public const string Section008 =
@"000 --------------------------------------------------------------
000 Order ID                                   Processing time 008
000 ³        Date begin of order 
000 ³        ³        Time begin order
000 ³        ³        ³      Date end of order 
000 ³        ³        ³      ³        Time end order
000 V        V        V      V        V
";
                    public const string Section009 =
@"000 --------------------------------------------------------------
000 Order ID                           Order transmition time  009
000 ³        Date begin transmition 
000 ³        ³        Time begin transmition
000 ³        ³        ³      Date end transmition 
000 ³        ³        ³      ³        Time end transmition
000 V        V        V      V        V
";
                }
            }
        }
    }
}
