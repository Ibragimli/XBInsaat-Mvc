using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ItemAlreadyException : Exception
    {
        public ItemAlreadyException(string msg) : base(msg)
        {

        }
    }
}
