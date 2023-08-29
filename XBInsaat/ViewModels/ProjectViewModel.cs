using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Mvc.ViewModels
{
    public class ProjectViewModel
    {
        public HighProject HighProject { get; set; }
        public IEnumerable<MidProject> MidProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Localization> Localizations { get; set; }
        public HomeIndexContactUsViewModel HomeIndexContactUsViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }

    }

}
