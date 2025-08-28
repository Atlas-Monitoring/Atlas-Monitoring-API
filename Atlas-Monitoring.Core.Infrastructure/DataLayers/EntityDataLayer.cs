using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class EntityDataLayer : IEntityDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public EntityDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Publics Methods
        #region Create
        public async Task<EntityReadViewModel> CreateNewEntity(EntityWriteViewModel entityWriteViewModel)
        {
            Entity newEntity = new()
            {
                EntityId = Guid.NewGuid(),
                Name = entityWriteViewModel.Name,
            };

            await _context.Entity.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return new()
            {
                EntityId = newEntity.EntityId,
                Name = newEntity.Name
            };
        }
        #endregion

        #region Read
        public async Task<List<EntityReadViewModel>> GetListOfEntity()
        {
            List<Entity> listOfEntities = await _context.Entity.ToListAsync();
            List<EntityReadViewModel> listOfEntitiesViewModel = new();
            foreach (Entity entity in listOfEntities)
            {
                listOfEntitiesViewModel.Add(new() { EntityId = entity.EntityId, Name = entity.Name });
            }

            return listOfEntitiesViewModel;
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteOneEntity(Guid entityId)
        {
            if (await _context.Entity.Where(item => item.EntityId == entityId).AnyAsync())
            {
                if (await _context.Device.Where(item => item.Entity.EntityId == entityId).AnyAsync())
                {
                    throw new CustomDataBaseException("Can't delete this entity because one or more device are assign on it");
                }

                Entity entityToDelete = await _context.Entity.Where(item => item.EntityId == entityId).SingleAsync();

                _context.Entity.Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        #endregion
    }
}
