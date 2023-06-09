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
        IProjectRepository ProjectRepository { get; }
        ISettingRepository SettingRepository { get; }
        IImageSettingRepository ImageSettingRepository { get; }
        IXBServiceRepository XBServiceRepository { get; }
        IProjectImageRepository ProjectImageRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        ICameraRepository CameraRepository { get; }
        Task<int> CommitAsync();

    }
}
