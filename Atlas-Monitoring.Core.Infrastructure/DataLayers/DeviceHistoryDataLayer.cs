using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DeviceHistoryDataLayer : IDeviceHistoryDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DeviceHistoryDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task AddHistoryToDevice(DeviceHistoryWriteViewModel deviceHistory)
        {
            DeviceHistory newDeviceHistory = new()
            {
                Id = Guid.NewGuid(),
                DateAdd = DateTime.Now,
                Device = _context.Device.Where(item => item.Id == deviceHistory.DeviceId).Single(),
                LogLevel = deviceHistory.LogLevel,
                Message = deviceHistory.Message,
            };

            await _context.DeviceHistory.AddAsync(newDeviceHistory);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public async Task<List<DeviceHistoryReadViewModel>> GetHistoryOfADevice(Guid deviceId)
        {
            List<DeviceHistory> listOfDeviceHistory = await _context.DeviceHistory.Where(item => item.Device.Id == deviceId).Include(item => item.Device).ToListAsync();
            List<DeviceHistoryReadViewModel> listOfDeviceHistoryViewModel = new();

            foreach (DeviceHistory deviceHistory in listOfDeviceHistory)
            {
                listOfDeviceHistoryViewModel.Add(new()
                {
                    Id = deviceHistory.Id,
                    DeviceId = deviceHistory.Device.Id,
                    DateAdd = deviceHistory.DateAdd,
                    LogLevel = deviceHistory.LogLevel,
                    Message = deviceHistory.Message
                });
            }

            return listOfDeviceHistoryViewModel;
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllHistoryOfADevice(Guid deviceId)
        {
            List<DeviceHistory> listOfDeviceHistory = await _context.DeviceHistory.Where(item => item.Device.Id == deviceId).ToListAsync();

            foreach (DeviceHistory deviceHistory in listOfDeviceHistory)
            {
                _context.DeviceHistory.Remove(deviceHistory);
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion
    }
}
