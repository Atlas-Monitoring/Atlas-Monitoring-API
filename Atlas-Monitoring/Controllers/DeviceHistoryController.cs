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
    public class DeviceHistoryController : ControllerBase
    {
        #region Properties
        private readonly IDeviceHistoryRepository _deviceHistoryRepository;
        #endregion

        #region Constructor
        public DeviceHistoryController(IDeviceHistoryRepository deviceHistoryRepository)
        {
            _deviceHistoryRepository = deviceHistoryRepository;
        }
        #endregion

        #region Publics Methods
        #region Create

        #endregion

        #region Read
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<List<DeviceHistoryReadViewModel>>> GetHistoryOfADevice(Guid deviceId)
        {
            try
            {
                return Ok(await _deviceHistoryRepository.GetHistoryOfADevice(deviceId));
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
