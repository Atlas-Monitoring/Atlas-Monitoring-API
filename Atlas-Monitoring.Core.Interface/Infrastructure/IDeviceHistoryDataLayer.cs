using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDeviceHistoryDataLayer
    {
        #region Create
        Task AddHistoryToDevice(DeviceHistoryWriteViewModel deviceHistory);
        #endregion

        #region Read
        Task<List<DeviceHistoryReadViewModel>> GetHistoryOfADevice(Guid deviceId);
        #endregion

        #region Update

        #endregion

        #region Delete
        Task DeleteAllHistoryOfADevice(Guid deviceId);
        #endregion
    }
}
