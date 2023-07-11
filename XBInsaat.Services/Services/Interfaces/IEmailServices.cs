using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces
{
    public interface IEmailServices
    {
        public void Send(string to, string subject, string html);
    }
}
