using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDeviceDataLayer
    {
        #region Create
        /// <summary>
        /// Create a new device
        /// </summary>
        /// <param name="newDevice">Object DeviceViewModel</param>
        /// <returns>Object DeviceViewModel</returns>
        public Task<DeviceViewModel> CreateNewDevice(DeviceViewModel newDevice);
        #endregion

        #region Read
        /// <summary>
        /// Get the list of the devices
        /// </summary>
        /// <returns>List of Object DeviceViewModel</returns>
        public Task<List<DeviceViewModel>> ListOfDevices();

        /// <summary>
        /// Get the list of the devices filtered on type
        /// </summary>
        /// <param name="deviceTypeId">Id of device type</param>
        /// <returns>List of Object DeviceViewModel</returns>
        public Task<List<DeviceViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId);

        /// <summary>
        /// Get one device by GUID
        /// </summary>
        /// <returns>List of Object DeviceViewModel</returns>
        public Task<DeviceViewModel> GetOneDevice(Guid deviceId);
        #endregion

        #region Update
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="updatedDevice">Object DeviceViewModel</param>
        /// <returns>Object DeviceViewModel</returns>
        public Task<DeviceViewModel> UpdateDevice(DeviceViewModel updatedDevice);
        #endregion

        #region Delete
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="deviceId">GUID of the device</param>
        /// <returns>List of Object DeviceViewModel</returns>
        public Task<DeviceViewModel> DeleteDevice(Guid deviceId);
        #endregion
    }
}
