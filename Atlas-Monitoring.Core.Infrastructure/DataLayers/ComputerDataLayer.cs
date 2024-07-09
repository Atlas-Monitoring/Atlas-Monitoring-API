using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            if (await _context.Device.Where(item => item.Name == computer.Name && item.SerialNumber == computer.SerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                throw new CustomDataBaseException($"Computer with name '{computer.Name}' and serial number '{computer.SerialNumber}' already exist");
            }

            Device newComputer = TransformViewModelToDeviceObject(computer, true);

            EntityEntry deviteTypeEntityEntry = _context.Entry(newComputer.DeviceType);
            deviteTypeEntityEntry.State = EntityState.Unchanged;

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
                throw new CustomNoContentException($"Computer with id {id} don't exist");
            }
        }

        public async Task<Guid> GetIdOfComputer(string computerName, string computerSerialNumber)
        {
            if (await _context.Device.Where(item => item.Name == computerName && item.SerialNumber == computerSerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device computer = await _context.Device.Where(item => item.Name == computerName && item.SerialNumber == computerSerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();

                return computer.Id;
            }
            else
            {
                throw new CustomNoContentException($"Computer with name '{computerName}' and serial number '{computerSerialNumber}' don't exist");
            }
        }

        public async Task<bool> CheckIfComputerExist(string computerName, string computerSerialNumber)
        {
            return await _context.Device.Where(item => item.Name == computerName && item.SerialNumber == computerSerialNumber && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync();
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
                computerDatabase.DateUpdated = DateTime.Now;

                _context.Entry(computerDatabase).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return TransformDeviceToComputerReadViewModel(computerDatabase);
            }
            else
            {
                throw new CustomNoContentException($"Computer with id {computer.Id} don't exist");
            }
        }

        public async Task UpdateComputerStatus(Guid id, DeviceStatus deviceStatus)
        {
            if (await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                Device device = await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).SingleAsync();

                device.DeviceStatus = deviceStatus;

                _context.Entry(device).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
            }
            else
            {
                throw new CustomNoContentException($"Computer with id {id} don't exist");
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Private Methods
        private Device TransformViewModelToDeviceObject(ComputerWriteViewModel computerWriteViewModel, bool isNewComputer)
        {
            return new()
            {
                Id = isNewComputer ? Guid.NewGuid() : computerWriteViewModel.Id,
                DeviceStatus = isNewComputer ? DeviceStatus.New : _context.Device.Where(item => item.Id == computerWriteViewModel.Id).Single().DeviceStatus,
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
                DeviceType = DeviceType.Computer,
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
