using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DeviceSoftwareInstalledDataLayer : IDeviceSoftwareInstalledDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DeviceSoftwareInstalledDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task AddNewSoftware(DeviceSoftwareInstalledWriteViewModel newSoftware)
        {
            if (_context.Device.Where(item => item.Id == newSoftware.DeviceId).Any())
            {
                await _context.Database.ExecuteSqlAsync($"INSERT INTO DeviceSoftwareInstalled (DeviceId, AppName, Version, Publisher) VALUES ({newSoftware.DeviceId},{newSoftware.AppName},{newSoftware.Version},{newSoftware.Publisher});");
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomNoContentException($"Device with id '{newSoftware.DeviceId}' don't exist !");
            }

        }
        #endregion

        #region Read
        public async Task<List<DeviceSoftwareInstalledReadViewModel>> ListOfSoftwareOnDevice(Guid deviceId)
        {
            if (_context.Device.Where(item => item.Id == deviceId).Any())
            {
                List<DeviceSoftwareInstalledReadViewModel> listSoftwareViewModel = new();
                List<DeviceSoftwareInstalled> listSoftware = await _context.DeviceSoftwareInstalled.Where(item => item.Device.Id == deviceId).ToListAsync();

                foreach (DeviceSoftwareInstalled software in listSoftware)
                {
                    listSoftwareViewModel.Add(new() { AppName = software.AppName, Publisher = software.Publisher, Version = software.Version });
                }

                return listSoftwareViewModel;
            }
            else
            {
                throw new CustomNoContentException($"Device with id '{deviceId}' don't exist !");
            }
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteAllSoftwareInstalledOnDevice(Guid deviceId)
        {
            if (_context.Device.Where(item => item.Id == deviceId).Any())
            {
                await _context.Database.ExecuteSqlAsync($"DELETE FROM DeviceSoftwareInstalled WHERE DeviceId = {deviceId};");
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomNoContentException($"Device with id '{deviceId}' don't exist !");
            }
        }
        #endregion
        #endregion
    }
}
