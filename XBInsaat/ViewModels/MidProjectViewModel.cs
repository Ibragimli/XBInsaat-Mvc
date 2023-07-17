using XBInsaat.Core.Entites;

namespace XBInsaat.Mvc.ViewModels
{
    public class MidProjectViewModel
    {
        public MidProject MidProject { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
