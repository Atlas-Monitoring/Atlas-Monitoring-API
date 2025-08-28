using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DeviceDataLayer : IDeviceDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DeviceDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create
        #endregion

        #region Read
        public async Task<List<DeviceReadViewModel>> ListOfDevices()
        {
            List<Device> listOfDevices = await _context.Device.Where(item => item.DeviceType.Id != DeviceType.Computer.Id && item.DeviceType.Id != DeviceType.Server.Id).Include(x => x.DeviceType).ToListAsync();
            List<DeviceReadViewModel> listOfDevicesViewModel = new();

            foreach (Device device in listOfDevices)
            {
                listOfDevicesViewModel.Add(TransformModelToViewModel(device));
            }

            return listOfDevicesViewModel;
        }

        public async Task<List<DeviceReadViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId)
        {
            List<Device> listOfDevices = await _context.Device.Where(item => item.DeviceType.Id == deviceTypeId).Include(x => x.DeviceType).ToListAsync();
            List<DeviceReadViewModel> listOfDevicesViewModel = new();

            foreach (Device device in listOfDevices)
            {
                listOfDevicesViewModel.Add(TransformModelToViewModel(device));
            }

            return listOfDevicesViewModel;
        }

        public async Task<DeviceReadViewModel> GetOneDevice(Guid deviceId)
        {
            if (await _context.Device.Where(item => item.Id == deviceId).AnyAsync())
            {
                Device deviceBDD = await _context.Device.Where(item => item.Id == deviceId).Include(x => x.DeviceType).SingleAsync();

                return TransformModelToViewModel(deviceBDD);
            }
            else
            {
                throw new CustomNoContentException($"No device with id {deviceId.ToString()} found !");
            }
        }
        #endregion

        #region Update
        public async Task UpdateDeviceStatus(Guid id, DeviceStatus deviceStatus)
        {
            if (await _context.Device.Where(item => item.Id == id).AnyAsync())
            {
                Device device = await _context.Device.Where(item => item.Id == id).SingleAsync();

                device.DeviceStatus = deviceStatus;

                _context.Entry(device).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateEntityOfDevice(Guid deviceId, Guid entityId)
        {
            if (!await _context.Device.Where(item => item.Id == deviceId).AnyAsync())
            {
                throw new CustomNoContentException($"Device with id {deviceId} don't exist");
            }
            else if (entityId != Guid.Empty && !await _context.Entity.Where(item => item.EntityId == entityId).AnyAsync())
            {
                throw new CustomNoContentException($"Entity with id {entityId} don't exist");
            }
            else
            {
                Device device = await _context.Device.Where(item => item.Id == deviceId).Include(item => item.Entity).SingleAsync();

                if (entityId == Guid.Empty)
                {
                    device.Entity = null;
                }
                else
                {
                    device.Entity = await _context.Entity.Where(item => item.EntityId == entityId).SingleAsync();
                }

                _context.Entry(device).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        public async Task DeleteDevice(Guid deviceId)
        {
            //Delete computer Data
            await _context.Database.ExecuteSqlAsync($"DELETE FROM ComputerData WHERE DeviceId = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            //Delete computer hard drive
            await _context.Database.ExecuteSqlAsync($"DELETE FROM ComputerHardDrive WHERE DeviceId = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            //Delete computer parts
            await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceParts WHERE DeviceId = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            //Delete computer Software installed
            await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceSoftwareInstalled WHERE DeviceId = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            //Delete computer History
            await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceHistory WHERE DeviceId = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            //Delete computer
            int numberOfDeletions = await _context.Database.ExecuteSqlAsync($"DELETE FROM Device WHERE Id = {deviceId.ToString()}");
            await _context.SaveChangesAsync();

            if (numberOfDeletions > 1) { throw new CustomDataBaseException($"Delete abort due to a number of device deleted > 1 ({numberOfDeletions})"); }
        }
        #endregion
        #endregion

        #region Private Methods
        private DeviceReadViewModel TransformModelToViewModel(Device device)
        {
            return new DeviceReadViewModel()
            {
                Id = device.Id,
                DeviceStatus = device.DeviceStatus,
                DeviceTypeId = device.DeviceType.Id,
                DeviceTypeName = device.DeviceType.Name,
                Name = device.Name,
                Domain = device.Domain,
                SerialNumber = device.SerialNumber,
                Model = device.Model,
                Manufacturer = device.Manufacturer,
                DateAdd = device.DateAdd,
                DateUpdated = device.DateUpdated
            };
        }
        #endregion
    }
}
