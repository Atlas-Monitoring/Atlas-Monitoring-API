using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IDeviceHardDriveRepository
    {
        #region Create
        #endregion

        #region Read 
        /// <summary>
        /// Get all device Hard Drive of a device
        /// </summary>
        /// <param name="deviceId">Id of device</param>
        /// <returns>List of DeviceHardDriveViewModel</returns>
        public Task<List<DeviceHardDriveViewModel>> GetAllDeviceHardDriveOfADevice(Guid deviceId);
        #endregion

        #region Update
        /// <summary>
        /// Update one device Hard Drive
        /// </summary>
        /// <param name="deviceHardDriveViewModel">Object DeviceHardDriveViewModel</param>
        /// <returns>Object DeviceHardDriveViewModel</returns>
        public Task<DeviceHardDriveViewModel> SyncOneHardDrive(DeviceHardDriveViewModel deviceHardDriveViewModel);
        #endregion

        #region Delete
        /// <summary>
        /// Delete one HardDrive of a Device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <param name="deviceHardDriveId">Device Hard drive ID</param>
        public Task DeleteOneHardDriveOfADevice(Guid deviceId, Guid deviceHardDriveId);

        /// <summary>
        /// Delete All HardDrive of a Device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public Task DeleteAllDeviceHardDriveOfADevice(Guid deviceId);
        #endregion
    }
}
