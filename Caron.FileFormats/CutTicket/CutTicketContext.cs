using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.CutTicketX
{
    public sealed class CutTicketContext
    {
        private static CutTicketContext instance;

        private static readonly Object locker = new Object();

        private static volatile bool isSpreadInprogress;
        public static bool IsSpreadInprogress { get => isSpreadInprogress; set => isSpreadInprogress = value; }


        private static volatile CutTicket cutTicket;
        public static CutTicket CutTicket { get => cutTicket; set => cutTicket = value; }

        private static volatile CutTicketReport cutTicketReport = new CutTicketReport();
        public static CutTicketReport CutTicketReport { get => cutTicketReport; set => cutTicketReport = value; }

        private static volatile string cutFilePath;
        public static string CutFilePath { get => cutFilePath; set => cutFilePath = value; }


        private static volatile int[] pliesDone;
        public static int[] PliesDone { get => pliesDone; set => pliesDone = value; }


        private static volatile int[] precedentPliesDone;
        public static int[] PrecedentPliesDone { get => precedentPliesDone; set => precedentPliesDone = value; }


        private static volatile int numberOfColors;
        public static int NumberOfColors { get => numberOfColors; set => numberOfColors = value; }


        private static volatile int indexColor;
        public static int IndexColor { get => indexColor; set => indexColor = value; }


        private static volatile int numberOfPlies;
        public static int NumberOfPlies { get => numberOfPlies; set => numberOfPlies = value; }


        private static volatile int parameterSet;
        public static int ParameterSet { get => parameterSet; set => parameterSet = value; }

        private static volatile int spreadMaterialAtStart;
        public static int SpreadMaterialAtStart { get => spreadMaterialAtStart; set => spreadMaterialAtStart = value; }

        private static volatile int wasteMaterialAtStart;
        public static int WasteMaterialAtStart { get => wasteMaterialAtStart; set => wasteMaterialAtStart = value; }

        static CutTicketContext()
        {
            var context = new CutTicketContext();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"[CutTicketContext]");
            sb.AppendLine($"\tCutFilePath: {CutFilePath}");

            return sb.ToString();
        }

        public static CutTicketContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new CutTicketContext();
                        }
                    }
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }
    }
}
