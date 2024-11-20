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
        private readonly IDeviceHistoryDataLayer _deviceHistoryDataLayer;
        #endregion

        #region Constructor
        public ComputerDataLayer(DefaultDbContext context, IDeviceHistoryDataLayer deviceHistoryDataLayer)
        {
            _context = context;
            _deviceHistoryDataLayer = deviceHistoryDataLayer;
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

            await _deviceHistoryDataLayer.AddHistoryToDevice(new()
            {
                LogLevel = LogLevel.Information,
                DeviceId = newComputer.Id,
                Message = "Computer added"
            });

            return TransformDeviceToComputerReadViewModel(newComputer);
        }
        #endregion

        #region Read
        public async Task<List<ComputerReadViewModel>> GetAllComputer()
        {
            List<Device> computers = await _context.Device.Where(item => item.DeviceType.Id == DeviceType.Computer.Id).Include(item => item.Entity).ToListAsync();

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
                List<DeviceHistoryWriteViewModel> listDeviceHistory = GetDifferenceBetweenComputerVersion(computer, computerDatabase);

                computerDatabase.Name = computer.Name;
                computerDatabase.Ip = computer.Ip == string.Empty ? computerDatabase.Ip : computer.Ip;
                computerDatabase.Domain = computer.Domain;
                computerDatabase.MaxRam = computer.MaxRam;
                computerDatabase.NumberOfLogicalProcessors = computer.NumberOfLogicalProcessors;
                computerDatabase.OS = computer.OS;
                computerDatabase.OSVersion = computer.OSVersion;
                computerDatabase.UserName = computer.UserName == string.Empty ? computerDatabase.UserName : computer.UserName;
                computerDatabase.SerialNumber = computer.SerialNumber;
                computerDatabase.Model = computer.Model;
                computerDatabase.Manufacturer = computer.Manufacturer;
                computerDatabase.DateUpdated = DateTime.Now;

                _context.Entry(computerDatabase).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                foreach (DeviceHistoryWriteViewModel deviceHistory in listDeviceHistory)
                {
                    await _deviceHistoryDataLayer.AddHistoryToDevice(deviceHistory);
                }

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

                await _deviceHistoryDataLayer.AddHistoryToDevice(new()
                {
                    LogLevel = LogLevel.Information,
                    DeviceId = id,
                    Message = $"New status for computer {deviceStatus}"
                });
            }
        }

        public async Task UpdateEntityOfComputer(Guid computerId, Guid entityId)
        {
            if (!await _context.Device.Where(item => item.Id == computerId && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                throw new CustomNoContentException($"Computer with id {computerId} don't exist");
            }
            else if (entityId != Guid.Empty && !await _context.Entity.Where(item => item.EntityId == entityId).AnyAsync())
            {
                throw new CustomNoContentException($"Entity with id {entityId} don't exist");
            }
            else
            {
                Device device = await _context.Device.Where(item => item.Id == computerId && item.DeviceType.Id == DeviceType.Computer.Id).Include(item => item.Entity).SingleAsync();

                if (entityId == Guid.Empty)
                {
                    device.Entity = null;
                }
                else
                {
                    device.Entity = await _context.Entity.Where(item => item.EntityId == entityId).SingleAsync();
                }
                
                _context.Entry(device).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await _deviceHistoryDataLayer.AddHistoryToDevice(new()
                {
                    LogLevel = LogLevel.Information,
                    DeviceId = computerId,
                    Message = $"Computer entity updated '{(device.Entity is null ? string.Empty : device.Entity.Name)}'"
                });
            }
        }
        #endregion

        #region Delete
        public async Task DeleteComputer(Guid id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (await _context.Device.Where(item => item.Id == id && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
                {
                    //Delete computer Data
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM ComputerData WHERE DeviceId = {id.ToString()}");
                    await _context.SaveChangesAsync();

                    //Delete computer hard drive
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM ComputerHardDrive WHERE DeviceId = {id.ToString()}");
                    await _context.SaveChangesAsync();

                    //Delete computer parts
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceParts WHERE DeviceId = {id.ToString()}");
                    await _context.SaveChangesAsync();

                    //Delete computer Software installed
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceSoftwareInstalled WHERE DeviceId = {id.ToString()}");
                    await _context.SaveChangesAsync();

                    //Delete computer History
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceHistory WHERE DeviceId = {id.ToString()}");
                    await _context.SaveChangesAsync();

                    //Delete computer
                    int numberOfDeletions =  await _context.Database.ExecuteSqlAsync($"DELETE FROM Device WHERE Id = {id.ToString()} AND DeviceTypeId = {DeviceType.Computer.Id}");
                    await _context.SaveChangesAsync();

                    if (numberOfDeletions > 1) { throw new CustomDataBaseException($"Delete abort due to a number of device deleted > 1 ({numberOfDeletions})"); }

                    transaction.Commit();
                }
                else
                {
                    throw new CustomNoContentException($"Computer with id {id} don't exist");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is not CustomNoContentException && ex is not CustomDataBaseException)
            {
                transaction.Rollback();
                throw new CustomDataBaseException($"Exception with database : {ex.Message}");
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
                Model = computerWriteViewModel.Model,
                Manufacturer = computerWriteViewModel.Manufacturer,
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
                Model = device.Model,
                Manufacturer = device.Manufacturer,
                DateAdd = device.DateAdd,
                DateUpdated = device.DateUpdated,
                EntityId = device.Entity is null ? null : device.Entity.EntityId,
                EntityName = device.Entity is null ? string.Empty : device.Entity.Name
            };
        }

        private List<DeviceHistoryWriteViewModel> GetDifferenceBetweenComputerVersion(ComputerWriteViewModel computerNewData, Device deviceBDD)
        {
            List<DeviceHistoryWriteViewModel> listDeviceHistory = new();

            if (computerNewData.Ip != deviceBDD.Ip && computerNewData.Ip != string.Empty)
            {
                listDeviceHistory.Add(new()
                {
                    LogLevel = LogLevel.Information,
                    DeviceId = computerNewData.Id,
                    Message = $"Computer Ip updated from '{computerNewData.Ip}' to '{deviceBDD.Ip}'"
                });
            }

            if (computerNewData.OSVersion != deviceBDD.OSVersion)
            {
                listDeviceHistory.Add(new()
                {
                    LogLevel = LogLevel.Information,
                    DeviceId = computerNewData.Id,
                    Message = $"Computer OSVersion updated from '{computerNewData.OSVersion}' to '{deviceBDD.OSVersion}'"
                });
            }

            if (computerNewData.UserName != deviceBDD.UserName && computerNewData.UserName != string.Empty)
            {
                listDeviceHistory.Add(new()
                {
                    LogLevel = LogLevel.Information,
                    DeviceId = computerNewData.Id,
                    Message = $"Computer UserName updated from '{computerNewData.UserName}' to '{deviceBDD.UserName}'"
                });
            }

            return listDeviceHistory;
        }
        #endregion
    }
}
