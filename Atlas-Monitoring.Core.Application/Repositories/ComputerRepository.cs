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
        private readonly IDeviceHardDriveRepository _deviceHardDriveRepository;
        private readonly IDevicePerformanceDataRepository _devicePerformanceDataRepository;
        private readonly IDevicePartsRepository _devicePartsRepository;
        private readonly IDeviceSoftwareInstalledRepository _deviceSoftwareInstalledRepository;
        private readonly IDeviceHistoryRepository _deviceHistoryRepository;
        #endregion

        #region Constructor
        public ComputerRepository(IComputerDataLayer computerDataLayer, IDeviceHardDriveRepository deviceHardDriveRepository, IDevicePerformanceDataRepository deviceDataRepository, IDevicePartsRepository devicePartsRepository, IDeviceSoftwareInstalledRepository deviceSoftwareInstalledRepository, IDeviceHistoryRepository deviceHistoryRepository)
        {
            _computerDataLayer = computerDataLayer;
            _deviceHardDriveRepository = deviceHardDriveRepository;
            _devicePerformanceDataRepository = deviceDataRepository;
            _devicePartsRepository = devicePartsRepository;
            _deviceSoftwareInstalledRepository = deviceSoftwareInstalledRepository;
            _deviceHistoryRepository = deviceHistoryRepository;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerReadViewModel> AddComputer(ComputerWriteViewModel computer)
        {
            computer = CheckComputerWriteViewModel(computer);
            computer.DateAdd = DateTime.Now;

            if (await _computerDataLayer.CheckIfComputerExist(computer.Name, computer.SerialNumber))
            {
                throw new CustomDataAlreadyExistException($"A computer with name '{computer.Name}' and serial number '{computer.SerialNumber}' already exist");
            }
            else
            {
                //Add Computer
                ComputerReadViewModel computerBdd = await _computerDataLayer.AddComputer(computer);

                //Add Computer Hard Drive
                foreach (DeviceHardDriveViewModel deviceHardDriveViewModel in computer.ComputerHardDrives)
                {
                    deviceHardDriveViewModel.DeviceId = computerBdd.Id;

                    await _deviceHardDriveRepository.SyncOneHardDrive(deviceHardDriveViewModel);
                }

                //Add Computer Performance Data
                computer.ComputerLastData.DeviceId = computerBdd.Id;
                await _devicePerformanceDataRepository.AddDevicePerformance(computer.ComputerLastData);

                //Add Computer Software Installed
                computer.SoftwareInstalled.ForEach(item => item.DeviceId = computerBdd.Id);
                await _deviceSoftwareInstalledRepository.SyncSoftwareOfDevice(computer.SoftwareInstalled);

                //Add Computer Part
                foreach (DevicePartsWriteViewModel devicePartsWriteViewModel in computer.ComputerParts)
                {
                    devicePartsWriteViewModel.DeviceId = computerBdd.Id;

                    await _devicePartsRepository.SyncDevicePart(devicePartsWriteViewModel);
                }

                return computerBdd;
            }
        }
        #endregion

        #region Read
        public async Task<List<ComputerReadViewModel>> GetAllComputer()
        {
            return await _computerDataLayer.GetAllComputer();
        }

        public async Task<ComputerReadViewModel> GetOneComputerById(Guid id)
        {
            ComputerReadViewModel computer = await _computerDataLayer.GetOneComputerById(id);
            computer.ComputerHardDrives = await _deviceHardDriveRepository.GetAllDeviceHardDriveOfADevice(id);
            computer.ComputerLastData = await _devicePerformanceDataRepository.GetAllDevicePerformanceDataOfADevice(id, DateTime.Now.AddDays(-1)); //Get last 24 hours of data
            computer.ComputerHistory = await _deviceHistoryRepository.GetHistoryOfADevice(id);
            computer.ComputerParts = await _devicePartsRepository.GetAllDevicePartByDeviceId(id);
            computer.SoftwareInstalled = await _deviceSoftwareInstalledRepository.ListOfSoftwareOnDevice(id);

            return computer;
        }

        public async Task<Guid> GetIdOfComputer(string computerName, string computerSerialNumber)
        {
            return await _computerDataLayer.GetIdOfComputer(computerName, computerSerialNumber);
        }
        #endregion

        #region Update
        public async Task<ComputerReadViewModel> UpdateComputer(ComputerWriteViewModel computer)
        {
            computer = CheckComputerWriteViewModel(computer);

            //Sync Computer Hard Drive
            foreach (DeviceHardDriveViewModel deviceHardDriveViewModel in computer.ComputerHardDrives)
            {
                deviceHardDriveViewModel.DeviceId = computer.Id;

                await _deviceHardDriveRepository.SyncOneHardDrive(deviceHardDriveViewModel);
            }

            //Add Computer Performance Data
            computer.ComputerLastData.DeviceId = computer.Id;
            await _devicePerformanceDataRepository.AddDevicePerformance(computer.ComputerLastData);

            //Sync Computer Software Installed
            computer.SoftwareInstalled.ForEach(item => item.DeviceId = computer.Id);
            await _deviceSoftwareInstalledRepository.SyncSoftwareOfDevice(computer.SoftwareInstalled);

            //Sync Computer Part
            foreach (DevicePartsWriteViewModel devicePartsWriteViewModel in computer.ComputerParts)
            {
                devicePartsWriteViewModel.DeviceId = computer.Id;

                await _devicePartsRepository.SyncDevicePart(devicePartsWriteViewModel);
            }

            return await _computerDataLayer.UpdateComputer(computer);
        }
        #endregion

        #region Delete
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
            if (computer.Domain.Length > 25) { throw new CustomModelException("Property 'Domain' would be truncate (25 characters max)"); }
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
