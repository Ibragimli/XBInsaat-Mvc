using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class HomeIndexContactUsViewModel
    {
        public ContactUsCreateDto ContactUsCreateDto { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }

    }
}
