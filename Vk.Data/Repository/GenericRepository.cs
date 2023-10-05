using Microsoft.EntityFrameworkCore;
using Vk.Base.Model;
using Vk.Data.Context;

namespace Vk.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    //Database temsilcisi
    private readonly VkDbContext dbcontext;
    
    // List All
    public List<TEntity> GetAll()
    {
        return dbcontext.Set<TEntity>().AsNoTracking().ToList();
    }
    
    // List By Id
    public TEntity GetById(int id)
    {
        return dbcontext.Set<TEntity>().Find(id);
    }
    
    // Soft Delete
    public void Delete(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbcontext.Set<TEntity>().Update(entity);    
    }
    
    // Soft Delete By Id
    public void Delete(int id)
    {
        TEntity entity = dbcontext.Set<TEntity>().Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbcontext.Set<TEntity>().Update(entity);
    }
    
    // Hard Delete
    public void Remove(TEntity entity)
    {
        dbcontext.Set<TEntity>().Remove(entity);
    }

    // Hard Delete By Id
    public void Remove(int id)
    {
        TEntity entity = dbcontext.Set<TEntity>().Find(id);
        dbcontext.Set<TEntity>().Remove(entity);
    }
    
    // Update
    public void Update(TEntity entity)
    {
        dbcontext.Set<TEntity>().Update(entity);
    }

    // Insert
    public void Insert(TEntity entity)
    {
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId = 1;
        dbcontext.Set<TEntity>().Add(entity);
    }

    // Insert List
    public void InsertRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertUserId = 1;
            x.InsertDate = DateTime.UtcNow; 
        });
        dbcontext.Set<TEntity>().AddRange(entities);    }

    
    public IQueryable<TEntity> GetAsQueryable()
    {
        /*
        IEnumerable<Order> orders = orderRepo.GetAll();
        IQueryable<Order> ordersQuery = orders.AsQueryable();

        IEnumerable<Order> filteredOrders = orders.Where(o => o.CustomerId == 3);
        IQueryable<Order> filteredOrdersQuery = ordersQuery.Where(o => o.CustomerId == 3);
        
        */
        return dbcontext.Set<TEntity>().AsQueryable();
    }
}