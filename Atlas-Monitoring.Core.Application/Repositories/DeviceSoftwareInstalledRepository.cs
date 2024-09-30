using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;
using Serilog;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class DeviceSoftwareInstalledRepository : IDeviceSoftwareInstalledRepository
    {
        #region Properties
        private readonly IDeviceSoftwareInstalledDataLayer _deviceSoftwareInstalledDataLayer;

        public DeviceSoftwareInstalledRepository(IDeviceSoftwareInstalledDataLayer deviceSoftwareInstalledDataLayer)
        {
            _deviceSoftwareInstalledDataLayer = deviceSoftwareInstalledDataLayer;
        }
        #endregion

        #region Constructor

        #endregion

        #region Public Methods
        #region Create
        public async Task SyncSoftwareOfDevice(List<DeviceSoftwareInstalledWriteViewModel> listOfNewSoftware)
        {
            if (listOfNewSoftware.Any())
            {
                //First delete all
                await _deviceSoftwareInstalledDataLayer.DeleteAllSoftwareInstalledOnDevice(listOfNewSoftware[0].DeviceId);

                //Then add all
                foreach (DeviceSoftwareInstalledWriteViewModel software in listOfNewSoftware)
                {
                    try
                    {
                        await _deviceSoftwareInstalledDataLayer.AddNewSoftware(software);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "An exception occured");
                    }
                }
            }
        }
        #endregion

        #region Read
        public async Task<List<DeviceSoftwareInstalledReadViewModel>> ListOfSoftwareOnDevice(Guid deviceId)
        {
            return await _deviceSoftwareInstalledDataLayer.ListOfSoftwareOnDevice(deviceId);
        }
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
        #endregion
    }
}
