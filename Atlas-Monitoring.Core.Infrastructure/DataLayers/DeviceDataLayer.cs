using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public async Task<DeviceReadViewModel> CreateNewDevice(DeviceWriteViewModel newDevice)
        {
            Device newDeviceBDD = new()
            {
                Id = Guid.NewGuid(),
                DeviceStatus = newDevice.DeviceStatus,
                DeviceType = _context.DeviceType.Where(item => item.Id == newDevice.DeviceTypeId).Single(),
                Name = newDevice.Name,
                Ip = newDevice.Ip,
                Domain = newDevice.Domain,
                UserName = newDevice.UserName,
                SerialNumber = newDevice.SerialNumber,
                Model = newDevice.Model,
                Manufacturer = newDevice.Manufacturer,
                DateAdd = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            EntityEntry deviceTypeEntry = _context.Entry(newDeviceBDD.DeviceType);
            deviceTypeEntry.State = EntityState.Unchanged;

            await _context.Device.AddAsync(newDeviceBDD);
            await _context.SaveChangesAsync();

            return TransformModelToViewModel(newDeviceBDD);
        }
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
        public async Task<DeviceReadViewModel> UpdateDevice(DeviceWriteViewModel updatedDevice)
        {
            if (await _context.Device.Where(item => item.Id == updatedDevice.Id && item.DeviceType.Id != DeviceType.Computer.Id && item.DeviceType.Id != DeviceType.Server.Id).AnyAsync())
            {
                Device deviceBDD = await _context.Device.Where(item => item.Id == updatedDevice.Id).Include(x => x.DeviceType).SingleAsync();

                deviceBDD.DeviceStatus = updatedDevice.DeviceStatus;
                deviceBDD.DeviceType = _context.DeviceType.Where(item => item.Id == updatedDevice.DeviceTypeId).Single();
                deviceBDD.Name = updatedDevice.Name;
                deviceBDD.Ip = updatedDevice.Ip;
                deviceBDD.Domain = updatedDevice.Domain;
                deviceBDD.UserName = updatedDevice.UserName;
                deviceBDD.SerialNumber = updatedDevice.SerialNumber;
                deviceBDD.Model = updatedDevice.Model;
                deviceBDD.Manufacturer = updatedDevice.Manufacturer;
                deviceBDD.DateUpdated = DateTime.Now;

                _context.Entry(deviceBDD).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return TransformModelToViewModel(deviceBDD);
            }
            else
            {
                throw new CustomNoContentException($"No device with id {updatedDevice.Id.ToString()} found !");
            }
        }
        #endregion

        #region Delete
        public async Task<DeviceReadViewModel> DeleteDevice(Guid deviceId)
        {
            if (await _context.Device.Where(item => item.Id == deviceId && item.DeviceType.Id != DeviceType.Computer.Id && item.DeviceType.Id != DeviceType.Server.Id).AnyAsync())
            {
                Device deviceBDD = await _context.Device.Where(item => item.Id == deviceId).SingleAsync();

                _context.Device.Remove(deviceBDD);

                await _context.SaveChangesAsync();

                return TransformModelToViewModel(deviceBDD);
            }
            else
            {
                throw new CustomNoContentException($"No device with id {deviceId.ToString()} found !");
            }
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
                Ip = device.Ip,
                Domain = device.Domain,
                UserName = device.UserName,
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
