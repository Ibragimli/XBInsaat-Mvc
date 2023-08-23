using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Services.Interfaces.Area.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Implementations.Area.Dashboard
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCareerCount()
        {
            return await _unitOfWork.CareerRepository.GetTotalCountAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);

        }

        public async Task<int> GetContactCount()
        {
            return await _unitOfWork.ContactUsRepository.GetTotalCountAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);
        }


        public async Task<List<int>> GetMonthCareerCount()
        {
            var careers = await _unitOfWork.CareerRepository.GetAllAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);
            List<int> monthCounts = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                var count = careers.Where(x => x.CreatedDate.Month == i).Count();
                monthCounts.Add(count);
            }
            return monthCounts;
        }

        public async Task<List<int>> GetMonthContactCount()
        {
            var careers = await _unitOfWork.ContactUsRepository.GetAllAsync(x => x.CreatedDate.Year == DateTime.UtcNow.Year);
            List<int> monthCounts = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                var count = careers.Where(x => x.CreatedDate.Month == i).Count();
                monthCounts.Add(count);
            }
            return monthCounts;
        }
    }
}
