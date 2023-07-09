using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminXBServiceEditServices : IAdminXBServiceEditServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminXBServiceEditServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task EditXBService(XBService XBService)
        {
            bool checkBool = false;

            var oldXBService = await GetXBService(XBService.Id);
            if (oldXBService == null)
                throw new ItemNullException("Servis tapılmadı!");
            Check(XBService);

            if (oldXBService.NameAz != XBService.NameAz)
            {
                oldXBService.NameAz = XBService.NameAz;
                checkBool = true;
            }
            if (oldXBService.NameEn != XBService.NameEn)
            {
                oldXBService.NameEn = XBService.NameEn;
                checkBool = true;
            }
            if (oldXBService.NameRu != XBService.NameRu)
            {
                oldXBService.NameRu = XBService.NameRu;
                checkBool = true;
            }
            if (oldXBService.DescribeAz != XBService.DescribeAz)
            {

                oldXBService.DescribeAz = XBService.DescribeAz;
                checkBool = true;

            }
            if (oldXBService.DescribeEn != XBService.DescribeEn)
            {

                oldXBService.DescribeEn = XBService.DescribeEn;
                checkBool = true;

            }
            if (oldXBService.DescribeRu != XBService.DescribeRu)
            {
                oldXBService.DescribeRu = XBService.DescribeRu;
                checkBool = true;
            }
            oldXBService.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<XBService> GetXBService(int id)
        {
            var XBService = await _unitOfWork.XBServiceRepository.GetAsync(x => x.Id == id);
            return XBService;
        }

        private void Check(XBService XBService)
        {
            if (XBService.NameAz.Length < 3 || XBService.NameEn.Length < 3 || XBService.NameRu.Length < 3)
            {
                throw new ValueFormatExpception("Servis adının uzunluğu minimum 3 ola bilər");
            }
            if (XBService.NameAz.Length > 100 || XBService.NameEn.Length > 100 || XBService.NameRu.Length > 100)
            {
                throw new ValueFormatExpception("Servis adının uzunluğu maksimum 100 ola bilər");
            }
            if (XBService.DescribeAz.Length > 5000 || XBService.DescribeRu.Length > 5000 || XBService.DescribeEn.Length > 5000)
            {
                throw new ValueFormatExpception("Servis təsvirinin uzunluğu maksimum 5000 ola bilər");

            }
            if (XBService.DescribeAz.Length < 3 || XBService.DescribeRu.Length < 3 || XBService.DescribeEn.Length < 3)
            {
                throw new ValueFormatExpception("Servis təsvirinin uzunluğu minimum 3 ola bilər");
            }
        }

    }
}
