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
        private IHighProjectRepository _highProjectRepository;
        private IImageSettingRepository _imageSettingRepository;
        private IHighProjectImageRepository _highProjectImageRepository;
        private ISettingRepository _settingRepository;
        private IEmailSettingRepository _emailSettingRepository;
        private IAppUserRepository _userRepository;
        private IXBServiceRepository _xBServiceRepository;
        private ICameraRepository _cameraRepository;
        private ICareerRepository _careerRepository;
        private IMidProjectRepository _midProjectRepository;
        private ILowProjectRepository _lowProjectRepository;
        private INewsRepository _newsRepository;
        private INewsImageRepository _newsImageRepository;
        private IMidProjectImageRepository _midProjectImageRepository;
        private IRevolutionSliderRepository _revolutionSliderRepository;
        private IContactUsRepository _contactUsRepository;
        private ILoggerRepository _loggerRepository;
        private ILocalizationRepository _localizationRepository;
        private IRolePageRepository _rolePageRepository;
        private IRolePageIdentityRoleIdRepository _rolePageIdentityRoleIdRepository;
        private IIdentityRoleRepository _identityRoleRepository;



        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IImageSettingRepository ImageSettingRepository => _imageSettingRepository = _imageSettingRepository ?? new ImageSettingRepository(_context);
        public IHighProjectImageRepository HighProjectImageRepository => _highProjectImageRepository = _highProjectImageRepository ?? new HighProjectImageRepository(_context);
        public ISettingRepository SettingRepository => _settingRepository = _settingRepository ?? new SettingRepository(_context);
        public IXBServiceRepository XBServiceRepository => _xBServiceRepository = _xBServiceRepository ?? new XBServiceRepository(_context);
        public IAppUserRepository AppUserRepository => _userRepository = _userRepository ?? new AppUserRepository(_context);
        public ICameraRepository CameraRepository => _cameraRepository = _cameraRepository ?? new CameraRepository(_context);

        public IHighProjectRepository HighProjectRepository => _highProjectRepository = _highProjectRepository ?? new HighProjectRepository(_context);

        public IMidProjectRepository MidProjectRepository => _midProjectRepository = _midProjectRepository ?? new MidProjectRepository(_context);

        public ILowProjectRepository LowProjectRepository => _lowProjectRepository = _lowProjectRepository ?? new LowProjectRepository(_context);

        public INewsRepository NewsRepository => _newsRepository = _newsRepository ?? new NewsRepository(_context);

        public IMidProjectImageRepository MidProjectImageRepository => _midProjectImageRepository = _midProjectImageRepository ?? new MidProjectImageRepository(_context);

        public INewsImageRepository NewsImageRepository => _newsImageRepository = _newsImageRepository ?? new NewsImageRepository(_context);

        public IRevolutionSliderRepository RevolutionSliderRepository => _revolutionSliderRepository = _revolutionSliderRepository ?? new RevolutionSliderRepository(_context);

        public IContactUsRepository ContactUsRepository => _contactUsRepository = _contactUsRepository ?? new ContactUsRepository(_context);

        public IEmailSettingRepository EmailSettingRepository => _emailSettingRepository = _emailSettingRepository ?? new EmailSettingRepository(_context);
        public ILoggerRepository LoggerRepository => _loggerRepository = _loggerRepository ?? new LoggerRepository(_context);

        public ICareerRepository CareerRepository => _careerRepository = _careerRepository ?? new CareerRepository(_context);
        public ILocalizationRepository LocalizationRepository => _localizationRepository = _localizationRepository ?? new LocalizationRepository(_context);
        public IRolePageRepository RolePageRepository => _rolePageRepository = _rolePageRepository ?? new RolePageRepository(_context);

        public IRolePageIdentityRoleIdRepository RolePageIdentityRoleIdRepository => _rolePageIdentityRoleIdRepository = _rolePageIdentityRoleIdRepository ?? new RolePageIdentityRoleIdRepository(_context);
        public IIdentityRoleRepository IdentityRoleRepository => _identityRoleRepository = _identityRoleRepository ?? new IdentityRoleRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
