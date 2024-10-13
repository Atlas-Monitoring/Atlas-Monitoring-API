using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class UserDataLayer : IUserDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public UserDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create

        #endregion

        #region Read
        public async Task<UserReadViewModel> AuthUser(AuthUserViewModel authUserViewModel)
        {
            if (await _context.User.Where(item => item.UserName == authUserViewModel.UserName).AnyAsync())
            {
                User user = await _context.User.Where(item => item.UserName == authUserViewModel.UserName).SingleAsync();

                //Check BCrypt Password
                if (BCrypt.Net.BCrypt.EnhancedVerify(authUserViewModel.Password, user.Password))
                {
                    return TransformModelToViewModel(user);
                }
                else
                {
                    throw new CustomAuthentificationFailedException();
                }
            }
            else
            {
                throw new CustomAuthentificationFailedException();
            }
        }
        #endregion

        #region Update
        public async Task UpdatePassword(AuthUserViewModel authUserViewModel)
        {
            if (await _context.User.Where(item => item.UserName == authUserViewModel.UserName).AnyAsync())
            {
                User user = await _context.User.Where(item => item.UserName == authUserViewModel.UserName).SingleAsync();

                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(authUserViewModel.Password);

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomDataBaseException("User not found");
            }
        }
        #endregion

        #region Delete

        #endregion
        #endregion

        #region Private Methods
        private UserReadViewModel TransformModelToViewModel(User user)
        {
            return new()
            {
                IdUser = user.IdUser,
                UserName = user.UserName
            };
        }
        #endregion
    }
}
