using XBInsaat.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.HelperService.Interfaces
{
    public interface ILoggerServices
    {
        Task LoggerCreate(string controller, string action, string name, string role, string? product = "salam");

    }
}
