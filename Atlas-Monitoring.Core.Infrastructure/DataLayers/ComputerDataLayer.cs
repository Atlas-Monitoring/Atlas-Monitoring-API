using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class ComputerDataLayer : IComputerDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public ComputerDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerReadViewModel> AddComputer(ComputerWriteViewModel computer)
        {
            Device newComputer = TransformViewModelToDeviceObject(computer, true);

            await _context.Device.AddAsync(newComputer);
            await _context.SaveChangesAsync();

            return TransformDeviceToComputerReadViewModel(newComputer);
        }
        #endregion

        #region Read
        public async Task<List<ComputerReadViewModel>> GetAllComputer()
        {
            List<Device> computers = await _context.Device.Where(item => item.DeviceType.Id == DeviceType.Computer.Id).ToListAsync();

            List<ComputerReadViewModel> listComputerReadViewModels = new();

            foreach (Device computer in computers)
            {
                listComputerReadViewModels.Add(TransformDeviceToComputerReadViewModel(computer));
            }

            return listComputerReadViewModels;
        }

        public async Task<ComputerReadViewModel> GetOneComputerById(Guid id)
        {
            if (await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device computer = await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();

                return TransformDeviceToComputerReadViewModel(computer);
            }
            else
            {
                throw new CustomDataBaseException($"Computer with id {id} don't exist");
            }
        }

        public async Task<Guid> CheckIfComputerAlreadyExist(string computerName, string computerSerialNumber)
        {
            if (await _context.Device.Where(item => item.Name == computerName && item.SerialNumber == computerSerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device computer = await _context.Device.Where(item => item.Name == computerName && item.SerialNumber == computerSerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();

                return computer.Id;
            }
            else
            {
                throw new CustomDataBaseException($"Computer with name '{computerName}' and serial number '{computerSerialNumber}' don't exist");
            }
        }
        #endregion

        #region Update
        public async Task<ComputerReadViewModel> UpdateComputer(ComputerWriteViewModel computer)
        {
            if (await _context.Device.Where(item => item.Id == computer.Id && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device computerDatabase = await _context.Device.Where(item => item.Id == computer.Id && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();
                computerDatabase.Name = computer.Name;
                computerDatabase.Ip = computer.Ip;
                computerDatabase.Domain = computer.Domain;
                computerDatabase.MaxRam = computer.MaxRam;
                computerDatabase.NumberOfLogicalProcessors = computer.NumberOfLogicalProcessors;
                computerDatabase.OS = computer.OS;
                computerDatabase.OSVersion = computer.OSVersion;
                computerDatabase.UserName = computer.UserName;
                computerDatabase.SerialNumber = computer.SerialNumber;

                _context.Entry(computerDatabase).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return TransformDeviceToComputerReadViewModel(computerDatabase);
            }
            else
            {
                throw new CustomDataBaseException($"Computer with id {computer.Id} don't exist");
            }
        }
        #endregion

        #region Delete
        public async Task DeleteComputer(Guid id)
        {
            if (await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device computer = await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();

                _context.Device.Remove(computer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomDataBaseException($"Computer with id {id} don't exist");
            }
        }
        #endregion
        #endregion

        #region Private Methods
        private Device TransformViewModelToDeviceObject(ComputerWriteViewModel computerWriteViewModel, bool isNewComputer)
        {
            return new()
            {
                Id = isNewComputer ? Guid.NewGuid() : computerWriteViewModel.Id,
                DeviceStatus = isNewComputer ? DeviceStatus.New : DeviceStatus.Undefined, //Todo : Récupérer le statut actuel
                DeviceType = DeviceType.Computer,
                Name = computerWriteViewModel.Name,
                Ip = computerWriteViewModel.Ip,
                Domain = computerWriteViewModel.Domain,
                MaxRam = computerWriteViewModel.MaxRam,
                NumberOfLogicalProcessors = computerWriteViewModel.NumberOfLogicalProcessors,
                OS = computerWriteViewModel.OS,
                OSVersion = computerWriteViewModel.OSVersion,
                UserName = computerWriteViewModel.UserName,
                SerialNumber = computerWriteViewModel.SerialNumber,
                DateAdd = computerWriteViewModel.DateAdd,
                DateUpdated = computerWriteViewModel.DateUpdated
            };
        }

        private ComputerReadViewModel TransformDeviceToComputerReadViewModel(Device device)
        {
            return new()
            {
                Id = device.Id,
                DeviceStatus = device.DeviceStatus,
                DeviceType = device.DeviceType,
                Name = device.Name,
                Ip = device.Ip,
                Domain = device.Domain,
                MaxRam = device.MaxRam,
                NumberOfLogicalProcessors = device.NumberOfLogicalProcessors,
                OS = device.OS,
                OSVersion = device.OSVersion,
                UserName = device.UserName,
                SerialNumber = device.SerialNumber,
                DateAdd = device.DateAdd,
                DateUpdated = device.DateUpdated
            };
        }
        #endregion
    }
}
