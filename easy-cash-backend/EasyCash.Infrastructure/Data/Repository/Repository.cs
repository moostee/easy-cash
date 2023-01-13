namespace EasyCash.Infrastructure.Data.Repository;
using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EasyCash.Domain;
using EasyCash.Shared.Entity;

internal class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    private readonly EasyCashContext _context;
    public Repository(EasyCashContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Ardalis.Specification.ISpecification<TEntity> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<TEntity> FindOneAsync(ISpecification<TEntity> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator.Default.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
    }

    public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }
}
