using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class ComputerDataRepository : IComputerDataRepository
    {
        #region Properties
        private readonly IComputerDataDataLayer _computerDataDataLayer;
        #endregion

        #region Constructor
        public ComputerDataRepository(IComputerDataDataLayer computerDataDataLayer)
        {
            _computerDataDataLayer = computerDataDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerData> AddComputerData(DevicePerformanceDataViewModel computerDataView)
        {
            computerDataView = CheckComputerDataViewModel(computerDataView);

            return await _computerDataDataLayer.AddComputerData(computerDataView);
        }
        #endregion

        #region Read
        public async Task<List<DevicePerformanceDataViewModel>> GetAllComputerDataOfAComputer(Guid computerId, DateTime minimumDataDate)
        {
            return await _computerDataDataLayer.GetAllComputerDataOfAComputer(computerId, minimumDataDate);
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllComputerDataOfAComputer(Guid computerId)
        {
            await _computerDataDataLayer.DeleteAllComputerDataOfAComputer(computerId);
        }
        #endregion
        #endregion

        #region Private Methods
        private DevicePerformanceDataViewModel CheckComputerDataViewModel(DevicePerformanceDataViewModel computerDataViewModel)
        {
            if (computerDataViewModel.ProcessorUtilityPourcent < 0) { throw new CustomModelException($"The property 'ProcessorUtilityPourcent' can't be lower than 0");  }
            if (computerDataViewModel.MemoryUsed < 0) { throw new CustomModelException($"The property 'MemoryUsed' can't be lower than 0");  }
            if (computerDataViewModel.UptimeSinceInSecond < 0) { throw new CustomModelException($"The property 'UptimeSinceInSecond' can't be lower than 0");  }

            return computerDataViewModel;
        }
        #endregion
    }
}
