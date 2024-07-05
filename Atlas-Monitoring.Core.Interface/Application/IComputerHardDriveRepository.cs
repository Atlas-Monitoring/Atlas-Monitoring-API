using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IComputerHardDriveRepository
    {
        #region Create
        /// <summary>
        /// Add new Computer Hard Drive
        /// </summary>
        /// <param name="computerHardDriveView">Object ComputerHardDriveViewModel</param>
        /// <returns>Object ComputerHardDrive</returns>
        public Task<ComputerHardDrive> AddComputerHardDrive(ComputerHardDriveViewModel computerHardDriveView);
        #endregion

        #region Read 
        /// <summary>
        /// Get all Computer Hard Drive of a computer
        /// </summary>
        /// <param name="computerId">Id of computer</param>
        /// <returns>List of ComputerHardDriveViewModel</returns>
        public Task<List<ComputerHardDriveViewModel>> GetAllComputerHardDriveOfAComputer(Guid computerId);
        #endregion

        #region Update
        /// <summary>
        /// Update one Computer Hard Drive
        /// </summary>
        /// <param name="computerHardDriveViewModel">Object ComputerHardDriveViewModel</param>
        /// <returns>Object ComputerHardDriveViewModel</returns>
        public Task<ComputerHardDriveViewModel> UpdateOneHardDrive(ComputerHardDriveViewModel computerHardDriveViewModel);
        #endregion

        #region Delete 
        /// <summary>
        /// Delete All HardDrive of a Computer
        /// </summary>
        /// <param name="computerId">Computer ID</param>
        public Task DeleteAllComputerHardDriveOfAComputer(Guid computerId);
        #endregion
    }
}
