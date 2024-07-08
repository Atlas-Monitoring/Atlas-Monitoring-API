using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersDataController : ControllerBase
    {
        #region Properties
        private readonly IComputerDataRepository _computerDataRepository;
        #endregion

        #region Constructor
        public ComputersDataController(IComputerDataRepository computerDataRepository)
        {
            _computerDataRepository = computerDataRepository;
        }
        #endregion

        #region Public Methods
        #region Create
        [HttpPost]
        public async Task<ActionResult<ComputerDataViewModel>> AddNewComputerData(ComputerDataViewModel newComputerData)
        {
            try
            {
                ComputerData computerDataDatabase = await _computerDataRepository.AddComputerData(newComputerData);

                return CreatedAtAction(nameof(AddNewComputerData), new { id = computerDataDatabase.Id }, computerDataDatabase);
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
        [HttpGet("GetByComputerIdAndDate")]
        public async Task<ActionResult<List<ComputerDataViewModel>>> GetComputersDataOfAComputer(Guid idComputer, DateTime minimumDataDate)
        {
            try
            {
                return Ok(await _computerDataRepository.GetAllComputerDataOfAComputer(idComputer, minimumDataDate));
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
