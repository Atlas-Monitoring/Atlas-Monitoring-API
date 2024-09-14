using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class ComputerPartsDataLayer : IComputerPartsDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public ComputerPartsDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<DevicePartsReadViewModel> AddComputerPart(DevicePartsWriteViewModel computerPart)
        {
            if (!await _context.Device.Where(item => item.Id == computerPart.DeviceId && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                throw new CustomDataBaseException($"Computer Id '{computerPart.DeviceId}' don't exist");
            }

            DeviceParts newDevicePart = new()
            {
                Id = Guid.NewGuid(),
                Device = await _context.Device.Where(item => item.Id == computerPart.DeviceId).SingleAsync(),
                Name = computerPart.Name,
                Labels = computerPart.Labels
            };

            await _context.DeviceParts.AddAsync(newDevicePart);
            await _context.SaveChangesAsync();

            return TransformDevicePartToDevicePartViewModel(newDevicePart);
        }
        #endregion

        #region Read
        public async Task<List<DevicePartsReadViewModel>> GetAllComputerPartByComputerId(Guid computerId)
        {
            List<DeviceParts> listeDeviceParts = await _context.DeviceParts.Where(item => item.Device.Id == computerId).Include(item => item.Device).ToListAsync();
            List<DevicePartsReadViewModel> listeDevicePartsReadModel = new();

            foreach (DeviceParts deviceParts in listeDeviceParts) 
            { 
                listeDevicePartsReadModel.Add(TransformDevicePartToDevicePartViewModel(deviceParts));
            }

            return listeDevicePartsReadModel;
        }

        public async Task<bool> CheckIfComputerPartOfComputerExist(DevicePartsWriteViewModel computerPart)
        {
            return await _context.DeviceParts.Where(item => item.Device.Id == computerPart.DeviceId && item.Name == computerPart.Name && item.Device.DeviceType.Id == DeviceType.Computer.Id).AnyAsync();
        }
        #endregion

        #region Update
        public async Task<DevicePartsReadViewModel> UpdateComputerPart(DevicePartsWriteViewModel computerPart)
        {
            DeviceParts devicePartsBDD = await _context.DeviceParts.Where(item => item.Device.Id == computerPart.DeviceId && item.Name == computerPart.Name).SingleAsync();
            devicePartsBDD.Labels = computerPart.Labels;

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
