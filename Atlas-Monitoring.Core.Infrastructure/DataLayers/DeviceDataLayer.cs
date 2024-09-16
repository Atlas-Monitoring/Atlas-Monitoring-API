using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
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
        public async Task<DeviceViewModel> CreateNewDevice(DeviceViewModel newDevice)
        {
            Device newDeviceBDD = new()
            {
                Id = Guid.NewGuid(),
                DeviceStatus = newDevice.DeviceStatus,
                DeviceType = newDevice.DeviceType,
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

            await _context.Device.AddAsync(newDeviceBDD);
            await _context.SaveChangesAsync();

            return TransformModelToViewModel(newDeviceBDD);
        }
        #endregion

        #region Read
        public async Task<List<DeviceViewModel>> ListOfDevices()
        {
            List<Device> listOfDevices = await _context.Device.Where(item => item.DeviceType.Id != DeviceType.Computer.Id).Include(x => x.DeviceType).ToListAsync();
            List<DeviceViewModel> listOfDevicesViewModel = new();

            foreach (Device device in listOfDevices)
            {
                listOfDevicesViewModel.Add(TransformModelToViewModel(device));
            }

            return listOfDevicesViewModel;
        }

        public async Task<List<DeviceViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId)
        {
            List<Device> listOfDevices = await _context.Device.Where(item => item.DeviceType.Id == deviceTypeId).Include(x => x.DeviceType).ToListAsync();
            List<DeviceViewModel> listOfDevicesViewModel = new();

            foreach (Device device in listOfDevices)
            {
                listOfDevicesViewModel.Add(TransformModelToViewModel(device));
            }

            return listOfDevicesViewModel;
        }

        public async Task<DeviceViewModel> GetOneDevice(Guid deviceId)
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
        public async Task<DeviceViewModel> UpdateDevice(DeviceViewModel updatedDevice)
        {
            if (await _context.Device.Where(item => item.Id == updatedDevice.Id && item.DeviceType.Id != DeviceType.Computer.Id).AnyAsync())
            {
                Device deviceBDD = await _context.Device.Where(item => item.Id == updatedDevice.Id).Include(x => x.DeviceType).SingleAsync();

                deviceBDD.DeviceStatus = updatedDevice.DeviceStatus;
                deviceBDD.DeviceType = updatedDevice.DeviceType;
                deviceBDD.Name = updatedDevice.Name;
                deviceBDD.Ip = updatedDevice.Ip;
                deviceBDD.Domain = updatedDevice.Domain;
                deviceBDD.UserName = updatedDevice.UserName;
                deviceBDD.SerialNumber = updatedDevice.SerialNumber;
                deviceBDD.Model = updatedDevice.Model;
                deviceBDD.Manufacturer = updatedDevice.Manufacturer;
                deviceBDD.DateUpdated = updatedDevice.DateUpdated;

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
        public async Task<DeviceViewModel> DeleteDevice(Guid deviceId)
        {
            if (await _context.Device.Where(item => item.Id == deviceId && item.DeviceType.Id != DeviceType.Computer.Id).AnyAsync())
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
        private DeviceViewModel TransformModelToViewModel(Device device)
        {
            return new DeviceViewModel()
            {
                Id = device.Id,
                DeviceStatus = device.DeviceStatus,
                DeviceType = device.DeviceType,
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
