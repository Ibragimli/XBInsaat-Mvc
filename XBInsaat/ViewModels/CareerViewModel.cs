using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class CareerViewModel
    {
        public IEnumerable<Setting> Settings { get; set; }
        public CareerPostDto CareerPostDto { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }

    }

}
