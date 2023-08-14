using XBInsaat.Core.Entites;
using XBInsaat.Service.Helper;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class LoggerIndexViewModel
    {
        public PagenetedList<Logger> Loggers { get; set; }
    }
}
