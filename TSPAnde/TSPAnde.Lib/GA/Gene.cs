﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public class Gene
    {
        public int Id { get; set; }

        public bool IsDepo { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
