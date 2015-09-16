using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Repositories
{
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity</returns>
        T GetById(Int64 id);

        /// <summary>
        /// Queries this repository - provides ability to query using LINQ through entities inside repository.
        /// </summary>
        /// <returns>Collection of the entities</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Adds entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(T entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="entities">Collection of the entities</param>
        void DeleteRange(IEnumerable<T> entities);
    }
}
