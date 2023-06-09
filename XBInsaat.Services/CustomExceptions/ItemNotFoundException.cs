using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string msg) : base(msg)
        {

        }
    }
}
