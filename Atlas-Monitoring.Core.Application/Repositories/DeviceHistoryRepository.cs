using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DeviceHistoryRepository : IDeviceHistoryRepository
    {
        #region Properties
        private readonly IDeviceHistoryDataLayer _deviceHistoryDataLayer;
        #endregion

        #region Constructor
        public DeviceHistoryRepository(IDeviceHistoryDataLayer deviceHistoryDataLayer)
        {
            _deviceHistoryDataLayer = deviceHistoryDataLayer;
        }
        #endregion

        #region Publics Methods
        #region Create

        #endregion

        #region Read
        public async Task<List<DeviceHistoryReadViewModel>> GetHistoryOfADevice(Guid deviceId)
        {
            return await _deviceHistoryDataLayer.GetHistoryOfADevice(deviceId);
        }
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
        #endregion
    }
}
