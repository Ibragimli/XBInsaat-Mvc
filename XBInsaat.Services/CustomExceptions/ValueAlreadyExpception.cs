using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ValueAlreadyExpception : Exception
    {
        public ValueAlreadyExpception(string msg) : base(msg)
        {

        }
    }
}
