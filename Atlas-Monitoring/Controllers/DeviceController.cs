using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace Atlas_Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        #region Properties
        private readonly DefaultDbContext _context;
        private readonly IDeviceRepository _deviceRepository;
        #endregion

        #region Constructor
        public DeviceController(DefaultDbContext context, IDeviceRepository deviceRepository)
        {
            _context = context;
            _deviceRepository = deviceRepository;
        }
        #endregion

        #region Publics Methods
        #region Create
        #endregion

        #region Read
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DeviceReadViewModel>>> GetAllDevices()
        {
            try
            {
                return Ok(await _deviceRepository.ListOfDevices());
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

        [HttpGet("GetAllFiltered/{id}")]
        public async Task<ActionResult<List<DeviceReadViewModel>>> GetAllDevices(int deviceTypeId)
        {
            try
            {
                return Ok(await _deviceRepository.ListOfDevicesFilteredOnType(deviceTypeId));
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

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceReadViewModel>> GetDeviceById(Guid id)
        {
            try
            {
                return Ok(await _deviceRepository.GetOneDevice(id));
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
        [HttpPut("{id}/{newDeviceStatus}")]
        public async Task<ActionResult> UpdateDeviceStatus(Guid id, DeviceStatus newDeviceStatus)
        {
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _deviceRepository.UpdateDeviceStatus(id, newDeviceStatus);

                await transaction.CommitAsync();

                return Ok();
            }
            catch (CustomNoContentException ex)
            {
                await transaction.RollbackAsync();

                return NoContent();
            }
            catch (CustomModelException ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }

        [HttpPut("AssignEntity/{deviceId}/{entityId}")]
        public async Task<ActionResult> UpdateEntityOfDevice(Guid deviceId, Guid entityId)
        {
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _deviceRepository.UpdateEntityOfDevice(deviceId, entityId);

                await transaction.CommitAsync();

                return Ok();
            }
            catch (CustomNoContentException ex)
            {
                await transaction.RollbackAsync();

                return NoContent();
            }
            catch (CustomModelException ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{deviceId}")]
        public async Task<ActionResult> DeleteDevice(Guid deviceId)
        {
            try
            {
                await _deviceRepository.DeleteDevice(deviceId);

                return NoContent();
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
