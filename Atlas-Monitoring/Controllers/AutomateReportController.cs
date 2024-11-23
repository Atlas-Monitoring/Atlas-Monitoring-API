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
    public class AutomateReportController : ControllerBase
    {
        #region Properties
        private readonly IAutomateReportRepository _automateReportRepository;
        #endregion

        #region Constructor
        public AutomateReportController(IAutomateReportRepository automateReportRepository)
        {
            _automateReportRepository = automateReportRepository;
        }
        #endregion

        #region Public Methods
        #region Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddNewReport(AutomateReportWriteViewModel newReport)
        {
            try
            {
                await _automateReportRepository.CreateNewReport(newReport);

                return CreatedAtAction(nameof(AddNewReport), new { id = newReport.Id }, newReport);
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
        [HttpGet("GetBetweenDate/{startDate}/{endDate}")]
        public async Task<ActionResult<List<AutomateReportReadViewModel>>> GetReportBetweenDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await _automateReportRepository.GetReportBetweenDate(startDate, endDate));
            }
            catch (CustomModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CustomNoContentException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutomateReportReadViewModel>> GetOneReportById(Guid id)
        {
            try
            {
                return Ok(await _automateReportRepository.GetOneReportById(id));
            }
            catch (CustomNoContentException ex)
            {
                return NoContent();
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOneReportById(Guid id)
        {
            try
            {
                await _automateReportRepository.DeleteOneReportById(id);

                return Ok();
            }
            catch (CustomNoContentException ex)
            {
                return NoContent();
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
