using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ValueFormatExpception : Exception
    {
        public ValueFormatExpception(string msg) : base(msg)
        {

        }
    }
}
