using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.Internal;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        #region Properties
        private readonly IComputerRepository _computerRepository;
        #endregion

        #region Constructor
        public ComputersController(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
        }
        #endregion

        #region Publics Methods
        #region Create
        [HttpPost]
        public async Task<ActionResult<ComputerReadViewModel>> AddNewComputer(ComputerWriteViewModel newComputer)
        {
            try
            {
                ComputerReadViewModel computerDatabase = await _computerRepository.AddComputer(newComputer);

                return CreatedAtAction(nameof(AddNewComputer), new { id = computerDatabase.Id }, computerDatabase);
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
            catch(Exception ex)
            {
                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }
        #endregion

        #region Read
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ComputerReadViewModel>>> GetAllComputers()
        {
            try
            {
                return Ok(await _computerRepository.GetAllComputer());
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
        public async Task<ActionResult<ComputerReadViewModel>> GetComputerById(Guid id)
        {
            try
            {
                return Ok(await _computerRepository.GetOneComputerById(id));
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

        [HttpGet("{computerName}/{computerSerialNumber}")]
        public async Task<ActionResult<ComputerReadViewModel>> GetIdOfComputer(string computerName, string computerSerialNumber = "")
        {
            try
            {
                return Ok(await _computerRepository.GetIdOfComputer(computerName, computerSerialNumber));
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
        public async Task<ActionResult<ComputerReadViewModel>> UpdateComputer(Guid id, ComputerWriteViewModel computer)
        {
            try
            {
                if (id != computer.Id) { throw new CustomModelException("Id don't match !"); }

                return Ok(await _computerRepository.UpdateComputer(computer));
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

        [HttpPut("{id}/{newDeviceStatus}")]
        public async Task<ActionResult<ComputerReadViewModel>> UpdateComputerStatus(Guid id, DeviceStatus newDeviceStatus)
        {
            try
            {
                await _computerRepository.UpdateComputerStatus(id, newDeviceStatus);

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

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComputer(Guid id)
        {
            try
            {
                await _computerRepository.DeleteComputer(id);

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
