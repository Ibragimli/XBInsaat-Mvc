using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Repositories;

namespace XBInsaat.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IHighProjectRepository HighProjectRepository { get; }
        IMidProjectRepository MidProjectRepository { get; }
        ILowProjectRepository LowProjectRepository { get; }
        ISettingRepository SettingRepository { get; }
        IEmailSettingRepository EmailSettingRepository { get; }
        IImageSettingRepository ImageSettingRepository { get; }
        IXBServiceRepository XBServiceRepository { get; }
        IHighProjectImageRepository HighProjectImageRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        ICameraRepository CameraRepository { get; }
        INewsRepository NewsRepository { get; }
        IMidProjectImageRepository MidProjectImageRepository { get; }
        INewsImageRepository NewsImageRepository { get; }
        IRevolutionSliderRepository RevolutionSliderRepository { get; }
        IContactUsRepository ContactUsRepository { get; }
        Task<int> CommitAsync();

    }
}
