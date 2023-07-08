using System.Collections;
using XBInsaat.Core.Entites;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class HighProjectEditViewModel
    {
        public HighProject HighProject { get; set; }
        public IEnumerable<HighProjectImage> HighProjectImages { get; set; }
    }
}
