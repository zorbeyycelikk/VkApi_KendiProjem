using System.Linq.Expressions;
using Vk.Base.Model;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{ 
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken ,params string [] includes);
    TEntity GetById(int id);
    List<TEntity> GetAll();
    void Delete(int id);
    void Delete(TEntity entity);
    void Remove(int id);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    void Insert(TEntity entity);
    void InsertRange(List<TEntity> entities);
    IQueryable<TEntity> GetAsQueryable(params string [] includes);

    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression, params string[] includes);
}