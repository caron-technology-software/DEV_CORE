using System;
using System.Collections.Generic;
using System.Linq;

namespace Caron.FileFormats.Denninson
{
    internal static class Constants
    {
        public const int DefaultAllowance = 0;
        public const char ParametersSeparator = 'V';

        public const int SpreadingMethodStartIndex = 91;
        public const int ParameterSetStartIndex = 84;

        public const int SectionEndStartIndex = 18;
        public const int SectionEndLength = 5;

        public const int FrontAllowanceStartIndex = 13;
        public const int RearAllowanceStartIndex = 18;

        public static class Headers
        {
            public const string Section = "201";
            public const string Step = "002";
            public const string OverlapZone = "014";
            public const string GeneralAllowance = "007";
            public const string SpliceAllowance = "118";
        }

    }
}
