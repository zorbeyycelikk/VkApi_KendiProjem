using Microsoft.EntityFrameworkCore;
using Vk.Base.Model;
using Vk.Data.Context;

namespace Vk.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    //Database temsilcisi
    private readonly VkDbContext dbContext;
    
    public GenericRepository(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    // List All
    public List<TEntity> GetAll()
    {
        return dbContext.Set<TEntity>().AsNoTracking().ToList();
    }
    
    // List By Id
    public TEntity GetById(int id)
    {
        return dbContext.Set<TEntity>().Find(id);
    }
    
    // Soft Delete
    public void Delete(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbContext.Set<TEntity>().Update(entity);    
    }
    
    // Soft Delete By Id
    public void Delete(int id)
    {
        TEntity entity = dbContext.Set<TEntity>().Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbContext.Set<TEntity>().Update(entity);
    }
    
    // Hard Delete
    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    // Hard Delete By Id
    public void Remove(int id)
    {
        TEntity entity = dbContext.Set<TEntity>().Find(id);
        dbContext.Set<TEntity>().Remove(entity);
    }
    
    // Update
    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    // Insert
    public void Insert(TEntity entity)
    {
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId = 1;
        dbContext.Set<TEntity>().Add(entity);
    }

    // Insert List
    public void InsertRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertUserId = 1;
            x.InsertDate = DateTime.UtcNow; 
        });
        dbContext.Set<TEntity>().AddRange(entities);    }

    
    public IQueryable<TEntity> GetAsQueryable()
    {
        /*
        IEnumerable<Order> orders = orderRepo.GetAll();
        IQueryable<Order> ordersQuery = orders.AsQueryable();

        IEnumerable<Order> filteredOrders = orders.Where(o => o.CustomerId == 3);
        IQueryable<Order> filteredOrdersQuery = ordersQuery.Where(o => o.CustomerId == 3);
        
        */
        return dbContext.Set<TEntity>().AsQueryable();
    }
}