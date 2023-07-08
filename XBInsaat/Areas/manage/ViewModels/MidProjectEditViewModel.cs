using System.Collections;
using XBInsaat.Core.Entites;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class MidProjectEditViewModel
    {
        public MidProject MidProject { get; set; }
        public IEnumerable<MidProjectImage> MidProjectImages { get; set; }
    }
}
