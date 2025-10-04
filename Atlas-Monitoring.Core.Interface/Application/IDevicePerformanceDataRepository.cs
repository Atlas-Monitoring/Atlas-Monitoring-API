using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IDevicePerformanceDataRepository
    {
        #region Create
        /// <summary>
        /// Add new computer data
        /// </summary>
        /// <param name="computerDataView">Object ComputerDataViewModel</param>
        /// <returns>Object ComputerData</returns>
        public Task<DevicePerformanceData> AddDevicePerformance(DevicePerformanceDataViewModel computerDataView);
        #endregion

        #region Read 
        /// <summary>
        /// Get all DevicePerformanceData of a device
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
        /// Delete All DevicePerformanceData of a Device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public Task DeleteAllDevicePerformanceOfADevice(Guid deviceId);
        #endregion
    }
}
