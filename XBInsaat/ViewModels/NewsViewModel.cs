using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class NewsViewModel
    {
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }


    }

}
