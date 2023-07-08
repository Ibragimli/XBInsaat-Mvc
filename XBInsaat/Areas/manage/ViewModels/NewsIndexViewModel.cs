using XBInsaat.Core.Entites;
using XBInsaat.Service.Helper;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class NewsIndexViewModel
    {
        public PagenetedList<News> News { get; set; }
    }
}
