using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        #region Properties
        private readonly IEntityDataLayer _entityDataLayer;
        #endregion

        #region Constructor
        public EntityRepository(IEntityDataLayer entityDataLayer)
        {
            _entityDataLayer = entityDataLayer;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<EntityReadViewModel> CreateNewEntity(EntityWriteViewModel entityWriteViewModel)
        {
            return await _entityDataLayer.CreateNewEntity(entityWriteViewModel);
        }
        #endregion

        #region Read
        public async Task<List<EntityReadViewModel>> GetListOfEntity()
        {
            return await _entityDataLayer.GetListOfEntity();
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteOneEntity(Guid entityId)
        {
            await _entityDataLayer.DeleteOneEntity(entityId);
        }
        #endregion
        #endregion
    }
}
