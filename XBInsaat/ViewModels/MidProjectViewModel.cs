using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class MidProjectViewModel
    {
        public MidProject MidProject { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
    }
    
}
