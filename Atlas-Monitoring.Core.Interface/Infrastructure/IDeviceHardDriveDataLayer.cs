using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDeviceHardDriveDataLayer
    {
        #region Create
        /// <summary>
        /// Add new Device Hard Drive
        /// </summary>
        /// <param name="deviceHardDriveView">Object DeviceHardDriveViewModel</param>
        /// <returns>Object DeviceHardDriveViewModel</returns>
        public Task<DeviceHardDriveViewModel> AddDeviceHardDrive(DeviceHardDriveViewModel deviceHardDriveView);
        #endregion

        #region Read 
        /// <summary>
        /// Get all Device Hard Drive of a Device
        /// </summary>
        /// <param name="deviceId">Id of device</param>
        /// <returns>List of DeviceHardDriveViewModel</returns>
        public Task<List<DeviceHardDriveViewModel>> GetAllDeviceHardDriveOfADevice(Guid deviceId);

        /// <summary>
        /// Get Device HardDrive from device Id and Letter of HardDrive
        /// </summary>
        /// <param name="deviceId">Device Id</param>
        /// <param name="letter">Letter of HardDrive</param>
        /// <returns>Guid of Device Hard drive or empty Guid</returns>
        public Task<Guid> GetGuidOfDeviceHardDriveIfExist(Guid deviceId, string letter);
        #endregion

        #region Update
        /// <summary>
        /// Update one device Hard Drive
        /// </summary>
        /// <param name="deviceHardDriveViewModel">Object DeviceHardDriveViewModel</param>
        /// <returns>Object DeviceHardDriveViewModel</returns>
        public Task<DeviceHardDriveViewModel> UpdateOneHardDrive(DeviceHardDriveViewModel deviceHardDriveViewModel);
        #endregion

        #region Delete
        /// <summary>
        /// Delete one HardDrive of a device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <param name="deviceHardDriveId">Device Hard drive ID</param>
        public Task DeleteOneHardDriveOfADevice(Guid deviceId, Guid deviceHardDriveId);

        /// <summary>
        /// Delete All HardDrive of a device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public Task DeleteAllHardDriveOfADevice(Guid deviceId);
        #endregion
    }
}
