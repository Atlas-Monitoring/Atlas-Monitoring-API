using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDeviceDataLayer
    {
        #region Create
        /// <summary>
        /// Create a new device
        /// </summary>
        /// <param name="newDevice">Object DeviceWriteViewModel</param>
        /// <returns>Object DeviceReadViewModel</returns>
        public Task<DeviceReadViewModel> CreateNewDevice(DeviceWriteViewModel newDevice);
        #endregion

        #region Read
        /// <summary>
        /// Get the list of the devices
        /// </summary>
        /// <returns>List of Object DeviceReadViewModel</returns>
        public Task<List<DeviceReadViewModel>> ListOfDevices();

        /// <summary>
        /// Get the list of the devices filtered on type
        /// </summary>
        /// <param name="deviceTypeId">Id of device type</param>
        /// <returns>List of Object DeviceReadViewModel</returns>
        public Task<List<DeviceReadViewModel>> ListOfDevicesFilteredOnType(int deviceTypeId);

        /// <summary>
        /// Get one device by GUID
        /// </summary>
        /// <returns>List of Object DeviceReadViewModel</returns>
        public Task<DeviceReadViewModel> GetOneDevice(Guid deviceId);
        #endregion

        #region Update
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="updatedDevice">Object DeviceWriteViewModel</param>
        /// <returns>Object DeviceReadViewModel</returns>
        public Task<DeviceReadViewModel> UpdateDevice(DeviceWriteViewModel updatedDevice);
        #endregion

        #region Delete
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="deviceId">GUID of the device</param>
        /// <returns>List of Object DeviceViewModel</returns>
        public Task<DeviceReadViewModel> DeleteDevice(Guid deviceId);
        #endregion
    }
}
