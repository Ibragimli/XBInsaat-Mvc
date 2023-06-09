using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.CustomExceptions
{

    public class ImageCountException : Exception
    {
        public ImageCountException(string msg) : base(msg)
        {

        }
    }
}
