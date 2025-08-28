using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDevicePartsDataLayer
    {
        #region Create
        /// <summary>
        /// Add a new device part to the DataBase
        /// </summary>
        /// <param name="devicePart">Device part</param>
        /// <returns>New object device</returns>
        public Task<DevicePartsReadViewModel> AddDevicePart(DevicePartsWriteViewModel devicePart);
        #endregion

        #region Read
        /// <summary>
        /// Get all device part of a device
        /// </summary>
        /// <param name="deviceId">Device Id</param>
        /// <returns>List of all device part of a device</returns>
        public Task<List<DevicePartsReadViewModel>> GetAllDevicePartByDeviceId(Guid deviceId);

        /// <summary>
        /// Check if device part exist
        /// </summary>
        /// <param name="devicePart">Device part</param>
        /// <returns>Booléan</returns>
        public Task<bool> CheckIfDevicePartODeviceExist(DevicePartsWriteViewModel devicePart);
        #endregion

        #region Update
        /// <summary>
        /// Update device part
        /// </summary>
        /// <param name="devicePart">Object device part</param>
        /// <returns>Device part object updated</returns>
        public Task<DevicePartsReadViewModel> UpdateDevicePart(DevicePartsWriteViewModel devicePart);
        #endregion
    }
}
