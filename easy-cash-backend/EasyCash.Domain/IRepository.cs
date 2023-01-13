using System;
using Ardalis.Specification;
using System.Threading.Tasks;
using EasyCash.Shared.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;


namespace EasyCash.Domain;

public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> specification);
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification);
}
