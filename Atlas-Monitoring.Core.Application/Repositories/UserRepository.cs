using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Properties
        private readonly IUserDataLayer _userDataLayer;
        #endregion

        #region Constructor
        public UserRepository(IUserDataLayer userDataLayer)
        {
            _userDataLayer = userDataLayer;
        }
        #endregion

        #region Publics Methods
        #region Create

        #endregion

        #region Read
        public async Task<UserReadViewModel> AuthUser(AuthUserViewModel authUserViewModel)
        {
            return await _userDataLayer.AuthUser(authUserViewModel);
        }
        #endregion

        #region Update
        public async Task UpdatePassword(AuthUserViewModel authUserViewModel)
        {
            await _userDataLayer.UpdatePassword(authUserViewModel);
        }
        #endregion

        #region Delete

        #endregion
        #endregion
    }
}
