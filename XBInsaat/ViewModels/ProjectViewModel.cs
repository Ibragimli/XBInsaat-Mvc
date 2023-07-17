using XBInsaat.Core.Entites;

namespace XBInsaat.Mvc.ViewModels
{
    public class ProjectViewModel
    {
        public HighProject HighProject { get; set; }
        public IEnumerable<MidProject> MidProjects { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
