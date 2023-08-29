using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class LoginViewModel
    {
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }
        public LoginPostDto LoginPostDto { get; set; }

    }
}
