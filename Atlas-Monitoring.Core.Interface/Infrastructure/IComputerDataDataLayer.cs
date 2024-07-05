using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IComputerDataDataLayer
    {
        #region Create
        /// <summary>
        /// Add new computer data
        /// </summary>
        /// <param name="computerDataView">Object ComputerDataViewModel</param>
        /// <returns>Object ComputerData</returns>
        public Task<ComputerData> AddComputerData(ComputerDataViewModel computerDataView);
        #endregion

        #region Read 
        /// <summary>
        /// Get all ComputerData of a computer
        /// </summary>
        /// <param name="computerId">Id of computer</param>
        /// <param name="minimumDataDate">Minimum data date</param>
        /// <returns>List of ComputerDataViewModel</returns>
        public Task<List<ComputerDataViewModel>> GetAllComputerDataOfAComputer(Guid computerId, DateTime minimumDataDate);
        #endregion

        #region Update

        #endregion

        #region Delete 
        /// <summary>
        /// Delete All ComputerData of a Computer
        /// </summary>
        /// <param name="computerId">Computer ID</param>
        public Task DeleteAllComputerDataOfAComputer(Guid computerId);
        #endregion
    }
}
