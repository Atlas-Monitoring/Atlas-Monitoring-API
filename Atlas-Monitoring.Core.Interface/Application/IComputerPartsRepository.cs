using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IComputerPartsRepository
    {
        #region Read
        /// <summary>
        /// Get all device part of a computer
        /// </summary>
        /// <param name="computerId">Computer Id</param>
        /// <returns>List of all device part of a computer</returns>
        public Task<List<DevicePartsReadViewModel>> GetAllComputerPartByComputerId(Guid computerId);
        #endregion

        #region Update
        /// <summary>
        /// Sync device part
        /// </summary>
        /// <param name="computerPart">Object device part</param>
        /// <returns>Device part object updated</returns>
        public Task<DevicePartsReadViewModel> SyncComputerPart(DevicePartsWriteViewModel computerPart);
        #endregion
    }
}
