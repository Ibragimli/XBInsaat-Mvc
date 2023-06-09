using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Datacontext;
using XBInsaat.Data.Repositories;

namespace XBInsaat.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IProjectRepository _projectRepository;
        private IImageSettingRepository _imageSettingRepository;
        private IProjectImageRepository _projectImageRepository;
        private ISettingRepository _settingRepository;
        private IAppUserRepository _userRepository;
        private IXBServiceRepository _xBServiceRepository;
        private ICameraRepository _cameraRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IProjectRepository ProjectRepository => _projectRepository = _projectRepository ?? new ProjectRepository(_context);
        public IImageSettingRepository ImageSettingRepository => _imageSettingRepository = _imageSettingRepository ?? new ImageSettingRepository(_context);
        public IProjectImageRepository ProjectImageRepository => _projectImageRepository = _projectImageRepository ?? new ProjectImageRepository(_context);
        public ISettingRepository SettingRepository => _settingRepository = _settingRepository ?? new SettingRepository(_context);
        public IXBServiceRepository XBServiceRepository => _xBServiceRepository = _xBServiceRepository ?? new XBServiceRepository(_context);
        public IAppUserRepository AppUserRepository => _userRepository = _userRepository ?? new AppUserRepository(_context);
        public ICameraRepository CameraRepository => _cameraRepository = _cameraRepository ?? new CameraRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
