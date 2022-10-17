using Core.Entities;

namespace Core.Interface
{
    public interface IGenericPersistence<TEntity> where TEntity : BaseClass
    {
        Task<TEntity> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(TEntity entity);
        Task<List<TEntity>> Get();
        Task<TEntity> Get(int id);
    }
}
