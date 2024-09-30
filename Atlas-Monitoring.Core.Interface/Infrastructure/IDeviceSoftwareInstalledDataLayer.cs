using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IDeviceSoftwareInstalledDataLayer
    {
        #region Create
        /// <summary>
        /// Add software to a device
        /// </summary>
        /// <param name="newSoftware">New software</param>
        public Task AddNewSoftware(DeviceSoftwareInstalledWriteViewModel newSoftware);
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
        /// <summary>
        /// Delete all software installed on device
        /// </summary>
        /// <param name="deviceId"></param>
        public Task DeleteAllSoftwareInstalledOnDevice(Guid deviceId);
        #endregion
    }
}
