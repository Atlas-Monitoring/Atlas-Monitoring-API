using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class ComputerDataRepository : IDevicePerformanceDataRepository
    {
        #region Properties
        private readonly IDevicePerformanceDataDataLayer _computerDataDataLayer;
        #endregion

        #region Constructor
        public ComputerDataRepository(IDevicePerformanceDataDataLayer computerDataDataLayer)
        {
            _computerDataDataLayer = computerDataDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<DevicePerformanceData> AddDevicePerformance(DevicePerformanceDataViewModel computerDataView)
        {
            computerDataView = CheckDevicePerformanceDataViewModel(computerDataView);

            return await _computerDataDataLayer.AddDevicePerformance(computerDataView);
        }
        #endregion

        #region Read
        public async Task<List<DevicePerformanceDataViewModel>> GetAllDevicePerformanceDataOfADevice(Guid deviceId, DateTime minimumDataDate)
        {
            return await _computerDataDataLayer.GetAllDevicePerformanceDataOfADevice(deviceId, minimumDataDate);
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllDevicePerformanceOfADevice(Guid deviceId)
        {
            await _computerDataDataLayer.DeleteAllDevicePerformanceOfADevice(deviceId);
        }
        #endregion
        #endregion

        #region Private Methods
        private DevicePerformanceDataViewModel CheckDevicePerformanceDataViewModel(DevicePerformanceDataViewModel devicePerformanceDataViewModel)
        {
            if (devicePerformanceDataViewModel.ProcessorUtilityPourcent < 0) { throw new CustomModelException($"The property 'ProcessorUtilityPourcent' can't be lower than 0"); }
            if (devicePerformanceDataViewModel.MemoryUsed < 0) { throw new CustomModelException($"The property 'MemoryUsed' can't be lower than 0"); }
            if (devicePerformanceDataViewModel.UptimeSinceInSecond < 0) { throw new CustomModelException($"The property 'UptimeSinceInSecond' can't be lower than 0"); }

            return devicePerformanceDataViewModel;
        }
        #endregion
    }
}
