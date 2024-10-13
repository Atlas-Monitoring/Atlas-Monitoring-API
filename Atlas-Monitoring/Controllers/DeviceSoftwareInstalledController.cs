using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceSoftwareInstalledController : ControllerBase
    {
        #region Properties
        private readonly IDeviceSoftwareInstalledRepository _deviceSoftwareInstalledRepository;
        #endregion

        #region Constructor
        public DeviceSoftwareInstalledController(IDeviceSoftwareInstalledRepository deviceSoftwareInstalledRepository)
        {
            _deviceSoftwareInstalledRepository = deviceSoftwareInstalledRepository;
        }
        #endregion

        #region Public Methods
        #region Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SyncSoftwareInstalled(List<DeviceSoftwareInstalledWriteViewModel> listOfNewSoftware)
        {
            try
            {
                await _deviceSoftwareInstalledRepository.SyncSoftwareOfDevice(listOfNewSoftware);

                return Ok();
            }
            catch (CustomNoContentException ex)
            {
                return NoContent();
            }
            catch (CustomModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }
        #endregion

        #region Read
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<List<DeviceSoftwareInstalledReadViewModel>>> ListOfSoftwareOnDevice(Guid deviceId)
        {
            try
            {
                return Ok(await _deviceSoftwareInstalledRepository.ListOfSoftwareOnDevice(deviceId));
            }
            catch (CustomNoContentException ex)
            {
                return NoContent();
            }
            catch (CustomModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
        #endregion
    }
}
