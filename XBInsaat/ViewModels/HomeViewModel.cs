using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<HighProject> HighProjects { get; set; }
        public IEnumerable<MidProject> MidProjects { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<XBService> XBServices { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public ContactUsCreateDto ContactUsCreateDto { get; set; }
        public HomeIndexProjectsViewModel HomeIndexProjectsViewModel { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
    }
    public class HomeIndexProjectsViewModel
    {
        public IEnumerable<HighProject> HighProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }

    }
    public class HomeIndexContactUsViewModel
    {
        public ContactUsCreateDto ContactUsCreateDto { get; set; }
        public IEnumerable<Setting> Settings { get; set; }

    }
}
