using SimpleBankingSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SimpleBankingSystem.Domain.DataAccess.Base
{
    public abstract class RepositoryBase<T> : IRepository<T> where T:class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        protected RepositoryBase(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Found entity or null</returns>
        public virtual T GetById(Int64 id)
        {
            return ContextFactory.GetContext().Set<T>().Find(id);
        }

        /// <summary>
        /// Queries this repository - provides ability to query using LINQ through entities inside repository.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Query()
        {
            return ContextFactory.GetContext().Set<T>();
        }

        /// <summary>
        /// Adds the specified entity into the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Current repository instance for chained operations
        /// </returns>
        public virtual void Add(T entity)
        {
            ContextFactory.GetContext().Set<T>().Add(entity);
        }

        /// <summary>
        /// Adds the range entities into the repository
        /// </summary>
        /// <param name="entities">Collection of the entities</param>
        /// <returns>
        /// Current repository instance for chained operations
        /// </returns>
        public virtual void AddRange(IEnumerable<T> entities)
        {
            ContextFactory.GetContext().Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Current repository instance for chained operations
        /// </returns>
        public virtual void Update(T entity)
        {
            ContextFactory.GetContext().Set<T>().Attach(entity);
            ContextFactory.GetContext().Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Current repository instance for chained operations
        /// </returns>
        public virtual void Delete(T entity)
        {
            ContextFactory.GetContext().Set<T>().Remove(entity);
        }

        /// <summary>
        /// Deletes the entities range.
        /// </summary>
        /// <param name="entities">Collection of the entities</param>
        /// <returns>
        /// Current repository instance for chained operations
        /// </returns>
        public void DeleteRange(IEnumerable<T> entities)
        {
            ContextFactory.GetContext().Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Gets the context factory.
        /// </summary>
        protected IContextFactory ContextFactory
        {
            get { return _contextFactory; }
        }

        /// <summary>
        /// Current instance of the context factory
        /// </summary>
        private readonly IContextFactory _contextFactory;
    }
}
