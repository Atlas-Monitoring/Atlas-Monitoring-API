using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IDeviceHistoryRepository
    {
        #region Create

        #endregion

        #region Read
        Task<List<DeviceHistoryReadViewModel>> GetHistoryOfADevice(Guid deviceId);
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}
