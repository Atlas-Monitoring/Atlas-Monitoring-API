using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        #region Properties
        private readonly IDeviceDataLayer _deviceDataLayer;
        private readonly IDeviceHistoryDataLayer _deviceHistoryDataLayer;
        #endregion

        #region Constructor
        public DeviceRepository(IDeviceDataLayer deviceDataLayer, IDeviceHistoryDataLayer deviceHistoryDataLayer)
        {
            _deviceDataLayer = deviceDataLayer;
            _deviceHistoryDataLayer = deviceHistoryDataLayer;
        }
        #endregion

        #region Publics Methods
        #region Create
        #endregion

        #region Read
        public async Task<List<DeviceReadViewModel>> ListOfDevices()
        {
            return await _deviceDataLayer.ListOfDevices();
        }

        public async Task<List<DeviceReadViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId)
        {
            return await _deviceDataLayer.ListOfDevicesFilteredOnType(deviceTypeId);
        }

        public async Task<DeviceReadViewModel> GetOneDevice(Guid deviceId)
        {
            return await _deviceDataLayer.GetOneDevice(deviceId);
        }
        #endregion

        #region Update
        public async Task UpdateDeviceStatus(Guid id, DeviceStatus deviceStatus)
        {
            await _deviceDataLayer.UpdateDeviceStatus(id, deviceStatus);

            await _deviceHistoryDataLayer.AddHistoryToDevice(new()
            {
                LogLevel = LogLevel.Information,
                DeviceId = id,
                Message = $"New status for device {deviceStatus}"
            });
        }

        public async Task UpdateEntityOfDevice(Guid deviceId, Guid entityId)
        {
            await _deviceDataLayer.UpdateEntityOfDevice(deviceId, entityId);

            await _deviceHistoryDataLayer.AddHistoryToDevice(new()
            {
                LogLevel = LogLevel.Information,
                DeviceId = deviceId,
                Message = $"Device entity updated with entityId {entityId.ToString()}"
            });
        }
        #endregion

        #region Delete
        public async Task DeleteDevice(Guid deviceId)
        {
            await _deviceDataLayer.DeleteDevice(deviceId);
        }
        #endregion
        #endregion
    }
}
