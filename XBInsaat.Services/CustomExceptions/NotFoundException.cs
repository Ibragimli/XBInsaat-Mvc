using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {

        }
    }
}
