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
        public IEnumerable<RevolutionSlider> RevolutionSliders { get; set; }
        public ContactUsCreateDto ContactUsCreateDto { get; set; }
        public HomeIndexProjectsViewModel HomeIndexProjectsViewModel { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
        public HomeIndexProjectViewModel HomeIndexProjectViewModel { get; set; }
        public HomeIndexNewsViewModel HomeIndexNewsViewModel { get; set; }
        public HomeIndexNewViewModel HomeIndexNewViewModel { get; set; }
        public HomeIndexMidProjectViewModel HomeIndexMidProjectViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public SettingViewModel SettingViewModel { get; set; }
    }
    public class HomeIndexProjectsViewModel
    {
        public IEnumerable<HighProject> HighProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }

    }
    public class SettingViewModel
    {
        public IEnumerable<Localization> Localizations { get; set; }
        public IEnumerable<Setting> Settings { get; set; }

    }

    public class HomeIndexNewsViewModel
    {
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }

    }
    public class HomeIndexNewViewModel
    {
        public IEnumerable<News> News { get; set; }
        public News New { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<NewsImage> NewsImages { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }


    }
    public class HomeIndexMidProjectViewModel
    {
        public IEnumerable<MidProject> MidProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<MidProjectImage> MidProjectImages { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }

    }
    public class HomeIndexProjectViewModel
    {
        public IEnumerable<HighProject> HighProjects { get; set; }
        public IEnumerable<MidProject> MidProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<HighProjectImage> HighProjectImages { get; set; }
        public IEnumerable<MidProjectImage> MidProjectImages { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }

    }

}
