using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        #region Properties
        private readonly IDeviceDataLayer _deviceDataLayer;
        #endregion

        #region Constructor
        public DeviceRepository(IDeviceDataLayer deviceDataLayer)
        {
            _deviceDataLayer = deviceDataLayer;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<DeviceViewModel> CreateNewDevice(DeviceViewModel newDevice)
        {
            return await _deviceDataLayer.CreateNewDevice(newDevice);
        }
        #endregion

        #region Read
        public async Task<List<DeviceViewModel>> ListOfDevices()
        {
            return await _deviceDataLayer.ListOfDevices();
        }

        public async Task<List<DeviceViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId)
        {
            return await _deviceDataLayer.ListOfDevicesFilteredOnType(deviceTypeId);
        }

        public async Task<DeviceViewModel> GetOneDevice(Guid deviceId)
        {
            return await _deviceDataLayer.GetOneDevice(deviceId);
        }
        #endregion

        #region Update
        public async Task<DeviceViewModel> UpdateDevice(DeviceViewModel updatedDevice)
        {
            return await _deviceDataLayer.UpdateDevice(updatedDevice);
        }
        #endregion

        #region Delete
        public async Task<DeviceViewModel> DeleteDevice(Guid deviceId)
        {
            return await _deviceDataLayer.DeleteDevice(deviceId);
        }
        #endregion
        #endregion
    }
}
