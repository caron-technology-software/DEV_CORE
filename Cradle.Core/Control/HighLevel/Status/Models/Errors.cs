﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public class Errors
    {
        public bool EtherCat { get; set; } = false;
        public bool EmergencyStatus { get; set; } = false;
        public int CommunicationError { get; set; } = 0;
        public Errors()
        {
            //--
        }
    }
}
