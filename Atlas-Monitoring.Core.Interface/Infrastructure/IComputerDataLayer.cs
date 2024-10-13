using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IComputerDataLayer
    {
        #region Create
        /// <summary>
        /// Add a new computer to the DataBase
        /// </summary>
        /// <param name="computer">Computer</param>
        /// <returns>New object computer</returns>
        public Task<ComputerReadViewModel> AddComputer(ComputerWriteViewModel computer);
        #endregion

        #region Read
        /// <summary>
        /// Get all computers of the database
        /// </summary>
        /// <returns>List of all computer</returns>
        public Task<List<ComputerReadViewModel>> GetAllComputer();

        /// <summary>
        /// Get one computer by id
        /// </summary>
        /// <param name="id">Id of the computer</param>
        /// <returns>One computer link to the Id</returns>
        public Task<ComputerReadViewModel> GetOneComputerById(Guid id);

        /// <summary>
        /// Return Guid of computer if exist in database
        /// </summary>
        /// <param name="computerName">Computer name</param>
        /// <param name="computerSerialNumber">Serial number</param>
        /// <returns>Guid if exist</returns>
        public Task<Guid> GetIdOfComputer(string computerName, string computerSerialNumber);

        /// <summary>
        /// Check if computer already exist in database
        /// </summary>
        /// <param name="computerName">Computer name</param>
        /// <param name="computerSerialNumber">Serial number</param>
        /// <returns>Boolean</returns>
        public Task<bool> CheckIfComputerExist(string computerName, string computerSerialNumber);
        #endregion

        #region Update
        /// <summary>
        /// Update computer data
        /// </summary>
        /// <param name="computer">Object computer</param>
        /// <returns>Computer data updated</returns>
        public Task<ComputerReadViewModel> UpdateComputer(ComputerWriteViewModel computer);

        /// <summary>
        /// Update status of computer
        /// </summary>
        /// <param name="id">Id of computer</param>
        /// <param name="deviceStatus">Status of the device</param>
        public Task UpdateComputerStatus(Guid id, DeviceStatus deviceStatus);

        /// <summary>
        /// Update Entity of computer
        /// </summary>
        /// <param name="computerId">Id of computer</param>
        /// <param name="entityId">Id of entity</param>
        public Task UpdateEntityOfComputer(Guid computerId, Guid entityId);
        #endregion

        #region Delete
        /// <summary>
        /// Delete computer data
        /// </summary>
        /// <param name="id">Id of the computer</param>
        public Task DeleteComputer(Guid id);
        #endregion
    }
}
