using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class DevicePartsDataLayer : IDevicePartsDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public DevicePartsDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<DevicePartsReadViewModel> AddDevicePart(DevicePartsWriteViewModel devicePart)
        {
            if (!await _context.Device.Where(item => item.Id == devicePart.DeviceId).AnyAsync())
            {
                throw new CustomDataBaseException($"Device Id '{devicePart.DeviceId}' don't exist");
            }

            DeviceParts newDevicePart = new()
            {
                Id = Guid.NewGuid(),
                Device = await _context.Device.Where(item => item.Id == devicePart.DeviceId).SingleAsync(),
                Name = devicePart.Name,
                Labels = devicePart.Labels
            };

            await _context.DeviceParts.AddAsync(newDevicePart);
            await _context.SaveChangesAsync();

            return TransformDevicePartToDevicePartViewModel(newDevicePart);
        }
        #endregion

        #region Read
        public async Task<List<DevicePartsReadViewModel>> GetAllDevicePartByDeviceId(Guid deviceId)
        {
            List<DeviceParts> listeDeviceParts = await _context.DeviceParts.Where(item => item.Device.Id == deviceId).Include(item => item.Device).ToListAsync();
            List<DevicePartsReadViewModel> listeDevicePartsReadModel = new();

            foreach (DeviceParts deviceParts in listeDeviceParts)
            {
                listeDevicePartsReadModel.Add(TransformDevicePartToDevicePartViewModel(deviceParts));
            }

            return listeDevicePartsReadModel;
        }

        public async Task<bool> CheckIfDevicePartODeviceExist(DevicePartsWriteViewModel devicePart)
        {
            return await _context.DeviceParts.Where(item => item.Device.Id == devicePart.DeviceId && item.Name == devicePart.Name).AnyAsync();
        }
        #endregion

        #region Update
        public async Task<DevicePartsReadViewModel> UpdateDevicePart(DevicePartsWriteViewModel devicePart)
        {
            DeviceParts devicePartsBDD = await _context.DeviceParts.Where(item => item.Device.Id == devicePart.DeviceId && item.Name == devicePart.Name).Include(item => item.Device).SingleAsync();
            devicePartsBDD.Labels = devicePart.Labels;

            _context.Entry(devicePartsBDD).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return TransformDevicePartToDevicePartViewModel(devicePartsBDD);
        }
        #endregion
        #endregion

        #region Private Methods
        /// <summary>
        /// Transform DevicePart Object to DevicePartViewModel Object
        /// </summary>
        /// <param name="devicePart">Object DevicePart</param>
        /// <returns>DevicePartViewModel Object</returns>
        private DevicePartsReadViewModel TransformDevicePartToDevicePartViewModel(DeviceParts devicePart)
        {
            return new()
            {
                Id = devicePart.Id,
                DeviceId = devicePart.Device.Id,
                Name = devicePart.Name,
                Labels = devicePart.Labels
            };
        }
        #endregion
    }
}
