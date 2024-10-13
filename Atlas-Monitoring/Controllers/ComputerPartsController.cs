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
    public class ComputerPartsController : ControllerBase
    {
        #region Properties
        private readonly IComputerPartsRepository _computerPartsRepository;
        #endregion

        #region Constructor
        public ComputerPartsController(IComputerPartsRepository computerPartsRepository)
        {
            _computerPartsRepository = computerPartsRepository;
        }
        #endregion

        #region Publics Methods
        #region Read
        [HttpGet("{computerId}")]
        public async Task<ActionResult<List<DevicePartsReadViewModel>>> GetAllComputerPartByComputerId(Guid computerId)
        {
            try
            {
                return Ok(await _computerPartsRepository.GetAllComputerPartByComputerId(computerId));
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
        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<DevicePartsReadViewModel>> UpdateComputerPart(DevicePartsWriteViewModel devicePartsWriteViewModel)
        {
            try
            {
                return Ok(await _computerPartsRepository.SyncComputerPart(devicePartsWriteViewModel));
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
        #endregion
    }
}
