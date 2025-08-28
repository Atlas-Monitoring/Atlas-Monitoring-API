using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Application;
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
    public class ComputersController : ControllerBase
    {
        #region Properties
        private readonly DefaultDbContext _context;
        private readonly IComputerRepository _computerRepository;
        #endregion

        #region Constructor
        public ComputersController(DefaultDbContext context, IComputerRepository computerRepository)
        {
            _context = context;
            _computerRepository = computerRepository;
        }
        #endregion

        #region Publics Methods
        #region Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ComputerReadViewModel>> AddNewComputer(ComputerWriteViewModel newComputer)
        {
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                ComputerReadViewModel computerDatabase = await _computerRepository.AddComputer(newComputer);

                await transaction.CommitAsync();

                return CreatedAtAction(nameof(AddNewComputer), new { id = computerDatabase.Id }, computerDatabase);
            }
            catch (CustomDataAlreadyExistException ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(ex.Message);
            }
            catch (CustomModelException ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(ex.Message);
            }
            catch (CustomDataBaseException ex)
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<ComputerReadViewModel>> UpdateComputer(Guid id, ComputerWriteViewModel computer)
        {
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (id != computer.Id) { throw new CustomModelException("Id don't match !"); }

                await transaction.CommitAsync();

                return Ok(await _computerRepository.UpdateComputer(computer));
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
        #endregion
        #endregion
    }
}
