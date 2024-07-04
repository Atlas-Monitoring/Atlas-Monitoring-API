using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        #region Properties
        private readonly IComputerDataLayer _computerDataLayer;
        #endregion

        #region Constructor
        public ComputerRepository(IComputerDataLayer computerDataLayer)
        {
            _computerDataLayer = computerDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerReadViewModel> AddComputer(ComputerWriteViewModel computer)
        {
            computer = CheckComputerWriteViewModel(computer);

            //Todo : Add HardDrive

            //Todo : Add ComputerData

            return await _computerDataLayer.AddComputer(computer);
        }
        #endregion

        #region Read
        public async Task<List<ComputerReadViewModel>> GetAllComputer()
        {
            return await _computerDataLayer.GetAllComputer();
        }

        public async Task<ComputerReadViewModel> GetOneComputerById(Guid id)
        {
            return await _computerDataLayer.GetOneComputerById(id);
        }

        public async Task<Guid> CheckIfComputerAlreadyExist(string computerName, string computerSerialNumber)
        {
            return await _computerDataLayer.CheckIfComputerAlreadyExist(computerName, computerSerialNumber);
        }
        #endregion

        #region Update
        public async Task<ComputerReadViewModel> UpdateComputer(ComputerWriteViewModel computer)
        {
            computer = CheckComputerWriteViewModel(computer);

            return await _computerDataLayer.UpdateComputer(computer);
        }
        #endregion

        #region Delete
        public async Task DeleteComputer(Guid id)
        {
            await _computerDataLayer.DeleteComputer(id);
        }
        #endregion
        #endregion

        #region Private Methods
        private ComputerWriteViewModel CheckComputerWriteViewModel(ComputerWriteViewModel computer)
        {
            //Throw custom exception if something is wrong
            if (computer.Name == null || computer.Name == string.Empty) { throw new CustomModelException("Computer name is empty"); }

            //Correct data if needed
            if (computer.Ip == null) { computer.Ip = string.Empty; }
            if (computer.Domain == null) { computer.Domain = string.Empty; }
            if (computer.MaxRam < 0) { computer.MaxRam = 0; }
            if (computer.NumberOfLogicalProcessors < 0) { computer.NumberOfLogicalProcessors = 0; }
            if (computer.OS == null) { computer.OS = string.Empty; }
            if (computer.OSVersion == null) { computer.OSVersion = string.Empty; }
            if (computer.UserName == null) { computer.UserName = string.Empty; }
            if (computer.SerialNumber == null) { computer.SerialNumber = string.Empty; }

            //Check if data would be truncated
            if (computer.Name.Length > 15) { throw new CustomModelException("Property 'Name' would be truncate (15 characters max)"); }
            if (computer.Ip.Length > 15) { throw new CustomModelException("Property 'Ip' would be truncate (15 characters max)"); }
            if (computer.Domain.Length > 15) { throw new CustomModelException("Property 'Domain' would be truncate (15 characters max)"); }
            if (computer.OS.Length > 35) { throw new CustomModelException("Property 'OS' would be truncate (35 characters max)"); }
            if (computer.OSVersion.Length > 35) { throw new CustomModelException("Property 'OSVersion' would be truncate (35 characters max)"); }
            if (computer.UserName.Length > 48) { throw new CustomModelException("Property 'UserName' would be truncate (48 characters max)"); }
            if (computer.SerialNumber.Length > 120) { throw new CustomModelException("Property 'SerialNumber' would be truncate (120 characters max)"); }

            //Define property DateUpdate to now
            computer.DateUpdated = DateTime.Now;

            return computer;
        }
        #endregion
    }
}
