using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DeviceHardDriveDataLayer : IDeviceHardDriveDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DeviceHardDriveDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<DeviceHardDriveViewModel> AddDeviceHardDrive(DeviceHardDriveViewModel deviceHardDriveView)
        {
            if (!await _context.Device.Where(item => item.Id == deviceHardDriveView.DeviceId && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                throw new CustomDataBaseException($"Device Id '{deviceHardDriveView.DeviceId}' don't exist");
            }

            DeviceHardDrive deviceHardDrive = new()
            {
                Id = Guid.NewGuid(),
                Device = await _context.Device.Where(item => item.Id == deviceHardDriveView.DeviceId).SingleAsync(),
                Letter = deviceHardDriveView.Letter,
                TotalSpace = deviceHardDriveView.TotalSpace,
                SpaceUse = deviceHardDriveView.SpaceUse,
                DateAdd = DateTime.Now,
                DateUpdate = DateTime.Now
            };

            await _context.DeviceHardDrive.AddAsync(deviceHardDrive);
            await _context.SaveChangesAsync();

            return deviceHardDriveView;
        }
        #endregion

        #region Read
        public async Task<List<DeviceHardDriveViewModel>> GetAllDeviceHardDriveOfADevice(Guid deviceId)
        {
            List<DeviceHardDrive> listDeviceHardDrive = await _context.DeviceHardDrive.Where(item => item.Device.Id == deviceId).ToListAsync();
            List<DeviceHardDriveViewModel> listDeviceHardDriveViewModel = new();

            foreach (DeviceHardDrive deviceHardDrive in listDeviceHardDrive)
            {
                deviceHardDrive.Device = new() { Id = deviceId };
                listDeviceHardDriveViewModel.Add(TransformDeviceHardDriveToDeviceHardDriveViewModel(deviceHardDrive));
            }

            return listDeviceHardDriveViewModel;
        }

        public async Task<Guid> GetGuidOfDeviceHardDriveIfExist(Guid deviceId, string letter)
        {
            if (await _context.DeviceHardDrive.Where(item => item.Device.Id == deviceId && item.Letter == letter).AnyAsync())
            {
                DeviceHardDrive DeviceHardDrive = await _context.DeviceHardDrive.Where(item => item.Device.Id == deviceId && item.Letter == letter).SingleAsync();
                return DeviceHardDrive.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region Update
        public async Task<DeviceHardDriveViewModel> UpdateOneHardDrive(DeviceHardDriveViewModel deviceHardDriveViewModel)
        {
            DeviceHardDrive deviceHardDrive = await _context.DeviceHardDrive.Where(item => item.Id == deviceHardDriveViewModel.Id).Include(item => item.Device).SingleAsync();
            deviceHardDrive.Letter = deviceHardDriveViewModel.Letter;
            deviceHardDrive.TotalSpace = deviceHardDriveViewModel.TotalSpace;
            deviceHardDrive.SpaceUse = deviceHardDriveViewModel.SpaceUse;
            deviceHardDrive.DateUpdate = DateTime.Now;

            _context.Entry(deviceHardDrive).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return TransformDeviceHardDriveToDeviceHardDriveViewModel(deviceHardDrive);
        }
        #endregion

        #region Delete
        public async Task DeleteOneHardDriveOfADevice(Guid deviceId, Guid deviceHardDriveId)
        {
            if (await _context.DeviceHardDrive.Where(item => item.Id == deviceHardDriveId && item.Device.Id == deviceId).AnyAsync())
            {
                DeviceHardDrive deviceHardDrive = await _context.DeviceHardDrive.Where(item => item.Id == deviceHardDriveId && item.Device.Id == deviceId).SingleAsync();

                _context.DeviceHardDrive.Remove(deviceHardDrive);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomNoContentException($"Any HardDrive with id '{deviceHardDriveId}' and device id '{deviceId}' was found");
            }

        }
        public async Task DeleteAllHardDriveOfADevice(Guid deviceId)
        {
            List<DeviceHardDrive> listDeviceHardDrive = await _context.DeviceHardDrive.Where(item => item.Device.Id == deviceId).ToListAsync();

            foreach (DeviceHardDrive deviceHardDrive in listDeviceHardDrive)
            {
                _context.DeviceHardDrive.Remove(deviceHardDrive);
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Private Methods
        private DeviceHardDriveViewModel TransformDeviceHardDriveToDeviceHardDriveViewModel(DeviceHardDrive deviceHardDrive)
        {
            return new()
            {
                Id = deviceHardDrive.Id,
                DeviceId = deviceHardDrive.Device.Id,
                Letter = deviceHardDrive.Letter,
                TotalSpace = deviceHardDrive.TotalSpace,
                SpaceUse = deviceHardDrive.SpaceUse
            };
        }
        #endregion
    }
}
