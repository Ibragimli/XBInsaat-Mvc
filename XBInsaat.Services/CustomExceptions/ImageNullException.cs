using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ImageNullException : Exception
    {
        public ImageNullException(string msg) : base(msg)
        {

        }
    }
}
