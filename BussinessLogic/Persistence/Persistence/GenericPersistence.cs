using BussinessLogic.Persistence.Context;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Persistence.Persistence
{
    public class GenericPersistence<TEntity> : IGenericPersistence<TEntity> where TEntity : BaseClass
    {
        private readonly StoreDbContext _context;

        public GenericPersistence(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> Delete(TEntity entity)
        {
            var entityToDelete = await Get(entity.Id);

            entityToDelete.Deleted = true;

            return await Update(entityToDelete);
        }

        public async Task<List<TEntity>> Get()
        {
            var values = await _context.Set<TEntity>()
                .Where(x => x.Deleted == false)
                .ToListAsync();

            if (values == null) throw new ArgumentNullException(nameof(values));

            return values;
        }

        public async Task<TEntity> Get(int id)
        {
            var value =  await _context.Set<TEntity>()
                .Where(x => x.Deleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (value == null) throw new ArgumentNullException(nameof(value));
            
            return value;
        }

        public async Task<int> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            return await _context.SaveChangesAsync();
        }
    }
}
