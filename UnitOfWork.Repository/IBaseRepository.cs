using SqlSugar;

namespace UnitOfWork.Repository
{
    public interface IBaseRepository<TEntity> : ISugarRepository, ISimpleClient<TEntity> where TEntity : class, new()
    {

    }
}