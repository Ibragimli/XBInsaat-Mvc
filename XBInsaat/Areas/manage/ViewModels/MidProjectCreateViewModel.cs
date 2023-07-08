using System.Collections;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class MidProjectCreateViewModel
    {
        public MidProjectCreateDto MidProjectCreateDto { get; set; }
        public IEnumerable<HighProject> HighProjects { get; set; }
    }
}
