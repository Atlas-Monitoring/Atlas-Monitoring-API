using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Application
{
    public interface IUserRepository
    {
        #region Create

        #endregion

        #region Read
        /// <summary>
        /// Auth user
        /// </summary>
        /// <param name="authUserViewModel">Object AuthUserViewModel</param>
        /// <returns>Object UserReadViewModel</returns>
        Task<UserReadViewModel> AuthUser(AuthUserViewModel authUserViewModel);
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}
