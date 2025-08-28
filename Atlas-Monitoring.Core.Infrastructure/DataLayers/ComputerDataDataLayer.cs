using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class ComputerDataDataLayer : IComputerDataDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public ComputerDataDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerData> AddComputerData(DevicePerformanceDataViewModel computerDataView)
        {
            if (await _context.Device.Where(item => item.Id == computerDataView.DeviceId).AnyAsync())
            {
                ComputerData computerData = new()
                {
                    Id = Guid.NewGuid(),
                    Device = await _context.Device.Where(item => item.Id == computerDataView.DeviceId).SingleAsync(),
                    DateAdd = DateTime.Now,
                    ProcessorUtilityPourcent = computerDataView.ProcessorUtilityPourcent,
                    MemoryUsed = computerDataView.MemoryUsed,
                    UptimeSinceInSecond = computerDataView.UptimeSinceInSecond
                };

                await _context.ComputerData.AddAsync(computerData);
                await _context.SaveChangesAsync();

                return computerData;
            }
            else
            {
                throw new CustomNoContentException($"Computer with id '{computerDataView.DeviceId}' don't exist !");
            }
        }
        #endregion

        #region Read
        public async Task<List<DevicePerformanceDataViewModel>> GetAllComputerDataOfAComputer(Guid computerId, DateTime minimumDataDate)
        {
            List<ComputerData> listComputerData = await _context.ComputerData.Where(item => item.Device.Id == computerId && item.DateAdd >= minimumDataDate).ToListAsync();

            List<DevicePerformanceDataViewModel> listComputerDataViewModel = new();

            foreach (ComputerData computerData in listComputerData)
            {
                listComputerDataViewModel.Add(TransformComputerDataToComputerDataViewModel(computerData));
            }

            return listComputerDataViewModel;
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllComputerDataOfAComputer(Guid computerId)
        {
            List<ComputerData> listComputerData = await _context.ComputerData.Where(item => item.Device.Id == computerId).ToListAsync();

            foreach (ComputerData computerData in listComputerData)
            {
                _context.ComputerData.Remove(computerData);
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Private Methods
        /// <summary>
        /// Transform ComputerData Object to ComputerDataViewModel Object
        /// </summary>
        /// <param name="computerData">Object ComputerData</param>
        /// <returns>ComputerDataViewModel Object</returns>
        private DevicePerformanceDataViewModel TransformComputerDataToComputerDataViewModel(ComputerData computerData)
        {
            return new()
            {
                Id = computerData.Id,
                DeviceId = computerData.Id,
                DateAdd = computerData.DateAdd,
                ProcessorUtilityPourcent = computerData.ProcessorUtilityPourcent,
                MemoryUsed = computerData.MemoryUsed,
                UptimeSinceInSecond = computerData.UptimeSinceInSecond
            };
        }
        #endregion
    }
}
