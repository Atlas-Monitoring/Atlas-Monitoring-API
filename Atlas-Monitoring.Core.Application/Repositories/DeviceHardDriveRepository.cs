using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DeviceHardDriveRepository : IDeviceHardDriveRepository
    {
        #region Properties
        private readonly IDeviceHardDriveDataLayer _deviceHardDriveDataLayer;
        #endregion

        #region Constructor
        public DeviceHardDriveRepository(IDeviceHardDriveDataLayer deviceHardDriveDataLayer)
        {
            _deviceHardDriveDataLayer = deviceHardDriveDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        #endregion

        #region Read
        public async Task<List<DeviceHardDriveViewModel>> GetAllDeviceHardDriveOfADevice(Guid deviceId)
        {
            return await _deviceHardDriveDataLayer.GetAllDeviceHardDriveOfADevice(deviceId);
        }
        #endregion

        #region Update
        public async Task<DeviceHardDriveViewModel> SyncOneHardDrive(DeviceHardDriveViewModel deviceHardDriveView)
        {
            deviceHardDriveView = CheckDeviceHardDriveViewModel(deviceHardDriveView);

            deviceHardDriveView.Id = await _deviceHardDriveDataLayer.GetGuidOfDeviceHardDriveIfExist(deviceHardDriveView.DeviceId, deviceHardDriveView.Letter);

            if (deviceHardDriveView.Id != Guid.Empty)
            {
                return await _deviceHardDriveDataLayer.UpdateOneHardDrive(deviceHardDriveView);
            }
            else
            {
                return await _deviceHardDriveDataLayer.AddDeviceHardDrive(deviceHardDriveView);
            }
        }
        #endregion

        #region Delete
        public async Task DeleteAllDeviceHardDriveOfADevice(Guid deviceId)
        {
            await _deviceHardDriveDataLayer.DeleteAllHardDriveOfADevice(deviceId);
        }

        public async Task DeleteOneHardDriveOfADevice(Guid deviceId, Guid deviceHardDriveId)
        {
            await _deviceHardDriveDataLayer.DeleteOneHardDriveOfADevice(deviceId, deviceHardDriveId);
        }
        #endregion
        #endregion

        #region Private Methods
        private DeviceHardDriveViewModel CheckDeviceHardDriveViewModel(DeviceHardDriveViewModel deviceHardDriveViewModel)
        {
            if (deviceHardDriveViewModel.Letter.Length > 2) { throw new CustomModelException($"Property 'Name' would be truncate (2 characters max)"); }
            if (deviceHardDriveViewModel.TotalSpace < 0) { throw new CustomModelException($"The property 'TotalSpace' can't be lower than 0"); }
            if (deviceHardDriveViewModel.SpaceUse < 0) { throw new CustomModelException($"The property 'SpaceUse' can't be lower than 0"); }

            return deviceHardDriveViewModel;
        }
        #endregion
    }
}
