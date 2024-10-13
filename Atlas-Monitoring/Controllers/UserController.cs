using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Atlas_Monitoring.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Publics Methods
        #region Create
        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<ActionResult<UserReadViewModel>> AuthUser(AuthUserViewModel authUserViewModel)
        {
            try
            {
                UserReadViewModel userReadViewModel = await _userRepository.AuthUser(authUserViewModel);

                AuthHelpers authHelpers = new AuthHelpers();

                return Ok(authHelpers.GenerateJWTToken(userReadViewModel.UserName));
            }
            catch (CustomAuthentificationFailedException ex)
            {
                return BadRequest("Authentification failed");
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Exception", statusCode: 500);
            }
        }
        #endregion

        #region Read

        #endregion

        #region Update
        [HttpPut("UpdatePassword")]
        public async Task<ActionResult<UserReadViewModel>> UpdatePassword(AuthUserViewModel authUserViewModel)
        {
            try
            {
                await _userRepository.UpdatePassword(authUserViewModel);

                return Ok();
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

        #region Delete

        #endregion
        #endregion
    }
}
