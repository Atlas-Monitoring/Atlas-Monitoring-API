using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DevicePerformanceDataRepository : IDevicePerformanceDataRepository
    {
        #region Properties
        private readonly IDevicePerformanceDataDataLayer _devicePerformanceDataDataLayer;
        #endregion

        #region Constructor
        public DevicePerformanceDataRepository(IDevicePerformanceDataDataLayer computerDataDataLayer)
        {
            _devicePerformanceDataDataLayer = computerDataDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<DevicePerformanceData> AddDevicePerformance(DevicePerformanceDataViewModel devicePerformanceDataViewModel)
        {
            devicePerformanceDataViewModel = CheckDevicePerformanceDataViewModel(devicePerformanceDataViewModel);

            return await _devicePerformanceDataDataLayer.AddDevicePerformance(devicePerformanceDataViewModel);
        }
        #endregion

        #region Read
        public async Task<List<DevicePerformanceDataViewModel>> GetAllDevicePerformanceDataOfADevice(Guid deviceId, DateTime minimumDataDate)
        {
            return await _devicePerformanceDataDataLayer.GetAllDevicePerformanceDataOfADevice(deviceId, minimumDataDate);
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllDevicePerformanceOfADevice(Guid deviceId)
        {
            await _devicePerformanceDataDataLayer.DeleteAllDevicePerformanceOfADevice(deviceId);
        }
        #endregion
        #endregion

        #region Private Methods
        private static DevicePerformanceDataViewModel CheckDevicePerformanceDataViewModel(DevicePerformanceDataViewModel devicePerformanceDataViewModel)
        {
            if (devicePerformanceDataViewModel.ProcessorUtilityPourcent < 0) { throw new CustomModelException($"The property 'ProcessorUtilityPourcent' can't be lower than 0"); }
            if (devicePerformanceDataViewModel.MemoryUsed < 0) { throw new CustomModelException($"The property 'MemoryUsed' can't be lower than 0"); }
            if (devicePerformanceDataViewModel.UptimeSinceInSecond < 0) { throw new CustomModelException($"The property 'UptimeSinceInSecond' can't be lower than 0"); }

            return devicePerformanceDataViewModel;
        }
        #endregion
    }
}
