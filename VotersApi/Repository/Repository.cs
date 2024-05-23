

namespace VotersApi.Repository;

public class Repository<TEntity>
        where TEntity : AuditEntityBase
{
    internal VoteAppDbContext DbContext;
    internal DbSet<TEntity> dbSet;

    public Repository(VoteAppDbContext dbContext)
    {
        DbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await dbSet.SingleOrDefaultAsync(t => t.Id.ToString() == id.ToString());
    }

    public virtual void Add(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        DbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void Delete(Guid id)
    {
        TEntity? entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);

    }
    public virtual void Delete(TEntity? entityToDelete)
    {
        if (entityToDelete != null)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
    }


}
