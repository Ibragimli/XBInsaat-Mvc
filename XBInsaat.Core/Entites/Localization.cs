﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class Localization : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
