using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DevicePartsRepository : IDevicePartsRepository
    {
        #region Properties
        private readonly IDevicePartsDataLayer _devicePartsDataLayer;
        #endregion

        #region Constructor
        public DevicePartsRepository(IDevicePartsDataLayer devicePartsDataLayer)
        {
            _devicePartsDataLayer = devicePartsDataLayer;
        }
        #endregion

        #region Public Methods
        #region Read
        public async Task<List<DevicePartsReadViewModel>> GetAllDevicePartByDeviceId(Guid deviceId)
        {
            return await _devicePartsDataLayer.GetAllDevicePartByDeviceId(deviceId);
        }
        #endregion

        #region Update
        public async Task<DevicePartsReadViewModel> SyncDevicePart(DevicePartsWriteViewModel devicePart)
        {
            if (await _devicePartsDataLayer.CheckIfDevicePartODeviceExist(devicePart))
            {
                return await _devicePartsDataLayer.UpdateDevicePart(devicePart);
            }
            else
            {
                return await _devicePartsDataLayer.AddDevicePart(devicePart);
            }
        }
        #endregion
        #endregion
    }
}
