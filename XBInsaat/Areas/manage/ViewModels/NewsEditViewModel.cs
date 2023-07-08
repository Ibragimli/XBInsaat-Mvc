using System.Collections;
using XBInsaat.Core.Entites;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class NewsEditViewModel
    {
        public News News { get; set; }
        public IEnumerable<NewsImage> NewsImages { get; set; }
    }
}
