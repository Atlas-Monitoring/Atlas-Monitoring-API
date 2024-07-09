using Atlas_Monitoring.Core.Application.Repositories;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersHardDriveController : ControllerBase
    {
        #region Properties
        private readonly IComputerHardDriveRepository _computerHardDriveRepository;
        #endregion

        #region Constructor
        public ComputersHardDriveController(IComputerHardDriveRepository computerHardDriveRepository)
        {
            _computerHardDriveRepository = computerHardDriveRepository;
        }
        #endregion

        #region Public Methods
        #region Read
        [HttpGet("{computerId}")]
        public async Task<ActionResult<List<ComputerHardDriveViewModel>>> GetHardDrivesByComputerId(Guid computerId)
        {
            try
            {
                return Ok(await _computerHardDriveRepository.GetAllComputerHardDriveOfAComputer(computerId));
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
        [HttpPut("{computerId}")]
        public async Task<ActionResult<List<ComputerReadViewModel>>> UpdateComputerHardDrive(Guid computerId, List<ComputerHardDriveViewModel> listComputerHardDrive)
        {
            try
            {
                //Check if all computer Id match with computerId in parameter
                if (listComputerHardDrive.Where(item => item.ComputerId != computerId).Any()) { throw new CustomModelException("ComputerId don't match !"); }
                List<ComputerHardDriveViewModel> listComputerHardDriveUpdated = new();

                //Update data for each Hard Drive
                foreach (ComputerHardDriveViewModel computerHardDriveViewModel in listComputerHardDrive) 
                {
                    //Check if disk already exist in database
                    Guid computerHardDriveGuid = await _computerHardDriveRepository.GetGuidOfComputerHardDriveIfExist(computerId, computerHardDriveViewModel.Letter);

                    //If exist update data
                    if (computerHardDriveGuid != Guid.Empty)
                    {
                        computerHardDriveViewModel.Id = computerHardDriveGuid;
                        listComputerHardDriveUpdated.Add(await _computerHardDriveRepository.UpdateOneHardDrive(computerHardDriveViewModel));
                    }
                    //Else add HardDrive
                    else
                    {
                        listComputerHardDriveUpdated.Add(TransformObjectToViewModel(await _computerHardDriveRepository.AddComputerHardDrive(computerHardDriveViewModel)));
                    }
                }

                return Ok(listComputerHardDriveUpdated);
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
        [HttpDelete("{computerId}/{computerHardDriveId}")]
        public async Task<ActionResult> DeleteOneHardDriveOfAComputer(Guid computerId, Guid computerHardDriveId)
        {
            try
            {
                await _computerHardDriveRepository.DeleteOneHardDriveOfAComputer(computerId, computerHardDriveId);

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

        #region Private Methods
        private ComputerHardDriveViewModel TransformObjectToViewModel(ComputerHardDrive computerHardDrive)
        {
            return new()
            {
                Id = computerHardDrive.Id,
                ComputerId = computerHardDrive.Device.Id,
                Letter = computerHardDrive.Letter,
                SpaceUse = computerHardDrive.SpaceUse,
                TotalSpace = computerHardDrive.TotalSpace
            };
        }
        #endregion
    }
}
