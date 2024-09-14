using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class ComputerPartsRepository : IComputerPartsRepository
    {
        #region Properties
        private readonly IComputerPartsDataLayer _computerPartsDataLayer;
        #endregion

        #region Constructor
        public ComputerPartsRepository(IComputerPartsDataLayer computerPartsDataLayer)
        {
            _computerPartsDataLayer = computerPartsDataLayer;
        }
        #endregion

        #region Public Methods
        #region Read
        public async Task<List<DevicePartsReadViewModel>> GetAllComputerPartByComputerId(Guid computerId)
        {
            return await _computerPartsDataLayer.GetAllComputerPartByComputerId(computerId);
        }
        #endregion

        #region Update
        public async Task<DevicePartsReadViewModel> SyncComputerPart(DevicePartsWriteViewModel computerPart)
        {
            if (await _computerPartsDataLayer.CheckIfComputerPartOfComputerExist(computerPart))
            {
                return await _computerPartsDataLayer.UpdateComputerPart(computerPart);
            }
            else
            {
                return await _computerPartsDataLayer.AddComputerPart(computerPart);
            }
        }
        #endregion
        #endregion
    }
}
