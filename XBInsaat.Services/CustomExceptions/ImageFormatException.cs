using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{
    public class ImageFormatException : Exception
    {
        public ImageFormatException(string msg) : base(msg)
        {

        }
    }
}
