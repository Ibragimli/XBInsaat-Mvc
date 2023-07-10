using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IRevolutionSliderEditServices
    {
        Task RevolutionSliderEdit(RevolutionSlider RevolutionSlider);
        Task<RevolutionSlider> GetSearch(int Id);
    }

}
