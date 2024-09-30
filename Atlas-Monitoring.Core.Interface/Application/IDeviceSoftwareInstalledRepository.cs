using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IDeviceSoftwareInstalledRepository
    {
        #region Create
        /// <summary>
        /// Add software to a device
        /// </summary>
        /// <param name="listOfNewSoftware">List of software</param>
        public Task SyncSoftwareOfDevice(List<DeviceSoftwareInstalledWriteViewModel> listOfNewSoftware);
        #endregion

        #region Read
        /// <summary>
        /// Get the list of installed software on a device
        /// </summary>
        /// <param name="deviceId">Device Id</param>
        /// <returns>List of software</returns>
        public Task<List<DeviceSoftwareInstalledReadViewModel>> ListOfSoftwareOnDevice(Guid deviceId);
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}
