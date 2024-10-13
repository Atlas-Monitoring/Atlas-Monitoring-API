using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Interface.Infrastructure
{
    public interface IUserDataLayer
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
        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="authUserViewModel">Object AuthUserViewModel</param>
        Task UpdatePassword(AuthUserViewModel authUserViewModel);
        #endregion

        #region Delete

        #endregion
    }
}
