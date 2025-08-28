using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DevicePerformanceDataDataLayer : IDevicePerformanceDataDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DevicePerformanceDataDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<DevicePerformanceData> AddDevicePerformance(DevicePerformanceDataViewModel devicePerformanceDataView)
        {
            if (await _context.Device.Where(item => item.Id == devicePerformanceDataView.DeviceId).AnyAsync())
            {
                DevicePerformanceData devicePerformanceData = new()
                {
                    Id = Guid.NewGuid(),
                    Device = await _context.Device.Where(item => item.Id == devicePerformanceDataView.DeviceId).SingleAsync(),
                    DateAdd = DateTime.Now,
                    ProcessorUtilityPourcent = devicePerformanceDataView.ProcessorUtilityPourcent,
                    MemoryUsed = devicePerformanceDataView.MemoryUsed,
                    UptimeSinceInSecond = devicePerformanceDataView.UptimeSinceInSecond
                };

                await _context.DevicePerformanceData.AddAsync(devicePerformanceData);
                await _context.SaveChangesAsync();

                return devicePerformanceData;
            }
            else
            {
                throw new CustomNoContentException($"Device with id '{devicePerformanceDataView.DeviceId}' don't exist !");
            }
        }
        #endregion

        #region Read
        public async Task<List<DevicePerformanceDataViewModel>> GetAllDevicePerformanceDataOfADevice(Guid deviceId, DateTime minimumDataDate)
        {
            List<DevicePerformanceData> listDevicePerformanceData = await _context.DevicePerformanceData.Where(item => item.Device.Id == deviceId && item.DateAdd >= minimumDataDate).ToListAsync();

            List<DevicePerformanceDataViewModel> listDevicePerformanceDataViewModel = new();

            foreach (DevicePerformanceData deviceData in listDevicePerformanceData)
            {
                listDevicePerformanceDataViewModel.Add(TransformDevicePerformanceDataToDevicePerformanceDataViewModel(deviceData));
            }

            return listDevicePerformanceDataViewModel;
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllDevicePerformanceOfADevice(Guid deviceId)
        {
            List<DevicePerformanceData> listDevicePerformanceData = await _context.DevicePerformanceData.Where(item => item.Device.Id == deviceId).ToListAsync();

            foreach (DevicePerformanceData devicePerformanceData in listDevicePerformanceData)
            {
                _context.DevicePerformanceData.Remove(devicePerformanceData);
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Private Methods
        /// <summary>
        /// Transform DevicePerformanceData Object to DevicePerformanceDataViewModel Object
        /// </summary>
        /// <param name="devicePerformanceData">Object DevicePerformanceData</param>
        /// <returns>DevicePerformanceDataViewModel Object</returns>
        private DevicePerformanceDataViewModel TransformDevicePerformanceDataToDevicePerformanceDataViewModel(DevicePerformanceData devicePerformanceData)
        {
            return new()
            {
                Id = devicePerformanceData.Id,
                DeviceId = devicePerformanceData.Id,
                DateAdd = devicePerformanceData.DateAdd,
                ProcessorUtilityPourcent = devicePerformanceData.ProcessorUtilityPourcent,
                MemoryUsed = devicePerformanceData.MemoryUsed,
                UptimeSinceInSecond = devicePerformanceData.UptimeSinceInSecond
            };
        }
        #endregion
    }
}
