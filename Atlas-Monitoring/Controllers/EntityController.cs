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
    public class EntityController : ControllerBase
    {
        #region Properties
        private readonly IEntityRepository _entityRepository;
        #endregion

        #region Constructor
        public EntityController(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        #endregion

        #region Publics Methods
        #region Create 
        [HttpPost]
        public async Task<ActionResult<EntityReadViewModel>> CreateNewEntity(EntityWriteViewModel newEntity)
        {
            try
            {
                EntityReadViewModel entityDB = await _entityRepository.CreateNewEntity(newEntity);

                return CreatedAtAction(nameof(CreateNewEntity), new { id = entityDB.EntityId }, entityDB);
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
        [HttpGet]
        public async Task<ActionResult<List<EntityReadViewModel>>> GetListOfEntity()
        {
            try
            {
                return Ok(await _entityRepository.GetListOfEntity());
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
        [HttpDelete("{entityId}")]
        public async Task<ActionResult> DeleteOneEntity(Guid entityId)
        {
            try
            {
                await _entityRepository.DeleteOneEntity(entityId);

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
        #endregion
    }
}
