using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
   
    public class ItemNullException : Exception
    {
        public ItemNullException(string msg) : base(msg)
        {

        }
    }
}
