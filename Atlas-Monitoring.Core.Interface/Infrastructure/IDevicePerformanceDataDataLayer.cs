using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDevicePerformanceDataDataLayer
    {
        #region Create
        /// <summary>
        /// Add new device performance data
        /// </summary>
        /// <param name="deviceDataView">Object DevicePerformanceDataViewModel</param>
        /// <returns>Object DevicePerformanceDataViewModel</returns>
        public Task<DevicePerformanceData> AddDevicePerformance(DevicePerformanceDataViewModel deviceDataView);
        #endregion

        #region Read 
        /// <summary>
        /// Get all performance data of a device
        /// </summary>
        /// <param name="deviceId">Id of device</param>
        /// <param name="minimumDataDate">Minimum data date</param>
        /// <returns>List of DevicePerformanceDataViewModel</returns>
        public Task<List<DevicePerformanceDataViewModel>> GetAllDevicePerformanceDataOfADevice(Guid deviceId, DateTime minimumDataDate);
        #endregion

        #region Update

        #endregion

        #region Delete 
        /// <summary>
        /// Delete All device performance data of a device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public Task DeleteAllDevicePerformanceOfADevice(Guid deviceId);
        #endregion
    }
}
