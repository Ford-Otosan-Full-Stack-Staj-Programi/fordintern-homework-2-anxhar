using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using OdevApi.Base;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace OdevApi.Data;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext context;
    public DbSet<TEntity> entities;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
        this.entities = this.context.Set<TEntity>();
    }
    public List<TEntity> GetAll()
    {
        return entities.AsNoTracking().Take(1000).ToList();
    }

    public TEntity GetById(int entityId)
    {
        return entities.Find(entityId);
    }


    public void Insert(TEntity entity)
    {
        entity.GetType().GetProperty("CreatedBy").SetValue(entity, "patika@dev.com");
        entity.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);
        entities.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        var column = entity.GetType().GetProperty("IsDeleted");
        if (column is not null)
        {
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }
        else
        {
            entities.Remove(entity);
        }
    }
    public void Remove(int id)
    {
        var entity = GetById(id);
        var column = entity.GetType().GetProperty("IsDeleted");
        if (column is not null)
        {
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }
        else
        {
            entities.Remove(entity);
        }
    }

    public void Update(TEntity entity)
    {
        context.ChangeTracker.Clear();
        context.Update(entity);
    }

    public List<TEntity> Where(Expression<Func<TEntity, bool>> where)
    {
        return entities.Where(where).ToList();
    }
}
