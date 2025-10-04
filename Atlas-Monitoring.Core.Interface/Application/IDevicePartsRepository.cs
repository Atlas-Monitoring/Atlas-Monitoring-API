using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IDevicePartsRepository
    {
        #region Read
        /// <summary>
        /// Get all device part of a Device
        /// </summary>
        /// <param name="computerId">Device Id</param>
        /// <returns>List of all device part of a Device</returns>
        public Task<List<DevicePartsReadViewModel>> GetAllDevicePartByDeviceId(Guid computerId);
        #endregion

        #region Update
        /// <summary>
        /// Sync device part
        /// </summary>
        /// <param name="devicePart">Object device part</param>
        /// <returns>Device part object updated</returns>
        public Task<DevicePartsReadViewModel> SyncDevicePart(DevicePartsWriteViewModel devicePart);
        #endregion
    }
}
