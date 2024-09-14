using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IComputerPartsDataLayer
    {
        #region Create
        /// <summary>
        /// Add a new device part to the DataBase
        /// </summary>
        /// <param name="computerPart">Device part</param>
        /// <returns>New object computer</returns>
        public Task<DevicePartsReadViewModel> AddComputerPart(DevicePartsWriteViewModel computerPart);
        #endregion

        #region Read
        /// <summary>
        /// Get all device part of a computer
        /// </summary>
        /// <param name="computerId">Computer Id</param>
        /// <returns>List of all device part of a computer</returns>
        public Task<List<DevicePartsReadViewModel>> GetAllComputerPartByComputerId(Guid computerId);

        /// <summary>
        /// Check if computer part exist
        /// </summary>
        /// <param name="computerPart">Device part</param>
        /// <returns>Booléan</returns>
        public Task<bool> CheckIfComputerPartOfComputerExist(DevicePartsWriteViewModel computerPart);
        #endregion

        #region Update
        /// <summary>
        /// Update device part
        /// </summary>
        /// <param name="computerPart">Object device part</param>
        /// <returns>Device part object updated</returns>
        public Task<DevicePartsReadViewModel> UpdateComputerPart(DevicePartsWriteViewModel computerPart);
        #endregion
    }
}
