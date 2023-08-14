using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.CustomExceptions
{
    public class UserPasswordResetException : Exception
    {
        public UserPasswordResetException(string msg) : base(msg)
        {

        }
    }
}
