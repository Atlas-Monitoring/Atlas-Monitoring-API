using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        #region Properties
        private readonly IDeviceRepository _deviceRepository;
        #endregion

        #region Constructor
        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<ActionResult<DeviceReadViewModel>> AddNewDevice(DeviceWriteViewModel newDevice)
        {
            try
            {
                DeviceReadViewModel deviceBDD = await _deviceRepository.CreateNewDevice(newDevice);

                return CreatedAtAction(nameof(AddNewDevice), new { id = deviceBDD.Id }, deviceBDD);
            }
            catch (CustomDataAlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CustomModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CustomDataBaseException ex)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceReadViewModel>> UpdateDevice(Guid id, DeviceWriteViewModel device)
        {
            try
            {
                if (id != device.Id) { throw new CustomModelException("Id don't match !"); }

                return Ok(await _deviceRepository.UpdateDevice(device));
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

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            try
            {
                await _deviceRepository.DeleteDevice(id);

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
