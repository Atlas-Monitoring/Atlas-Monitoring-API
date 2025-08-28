using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class ComputerHardDriveRepository : IComputerHardDriveRepository
    {
        #region Properties
        private readonly IComputerHardDriveDataLayer _computerHardDriveDataLayer;
        #endregion

        #region Constructor
        public ComputerHardDriveRepository(IComputerHardDriveDataLayer computerHardDriveDataLayer)
        {
            _computerHardDriveDataLayer = computerHardDriveDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerHardDrive> AddComputerHardDrive(DeviceHardDriveViewModel computerHardDriveView)
        {
            computerHardDriveView = CheckComputerHardDriveViewModel(computerHardDriveView);

            return await _computerHardDriveDataLayer.AddComputerHardDrive(computerHardDriveView);
        }
        #endregion

        #region Read
        public async Task<List<DeviceHardDriveViewModel>> GetAllComputerHardDriveOfAComputer(Guid computerId)
        {
            return await _computerHardDriveDataLayer.GetAllComputerHardDriveOfAComputer(computerId);
        }

        public async Task<Guid> GetGuidOfComputerHardDriveIfExist(Guid computerId, string letter)
        {
            return await _computerHardDriveDataLayer.GetGuidOfComputerHardDriveIfExist(computerId, letter);
        }
        #endregion

        #region Update
        public async Task<DeviceHardDriveViewModel> UpdateOneHardDrive(DeviceHardDriveViewModel computerHardDriveViewModel)
        {
            return await _computerHardDriveDataLayer.UpdateOneHardDrive(computerHardDriveViewModel);
        }
        #endregion

        #region Delete
        public async Task DeleteAllComputerHardDriveOfAComputer(Guid computerId)
        {
            await _computerHardDriveDataLayer.DeleteAllComputerHardDriveOfAComputer(computerId);
        }

        public async Task DeleteOneHardDriveOfAComputer(Guid computerId, Guid computerHardDriveId)
        {
            await _computerHardDriveDataLayer.DeleteOneHardDriveOfAComputer(computerId, computerHardDriveId);
        }
        #endregion
        #endregion

        #region Private Methods
        private DeviceHardDriveViewModel CheckComputerHardDriveViewModel(DeviceHardDriveViewModel computerHardDriveViewModel)
        {
            if (computerHardDriveViewModel.Letter.Length > 2) { throw new CustomModelException($"Property 'Name' would be truncate (2 characters max)"); }
            if (computerHardDriveViewModel.TotalSpace < 0) { throw new CustomModelException($"The property 'TotalSpace' can't be lower than 0"); }
            if (computerHardDriveViewModel.SpaceUse < 0) { throw new CustomModelException($"The property 'SpaceUse' can't be lower than 0"); }

            return computerHardDriveViewModel;
        }
        #endregion
    }
}
