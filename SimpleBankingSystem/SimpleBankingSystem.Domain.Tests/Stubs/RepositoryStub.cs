using SimpleBankingSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests.Stubs
{
    /// <summary>
    /// Stub implementation repository for entities
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class RepositoryStub<T> : IRepository<T> where T : class
    {
        protected List<T> Entities = new List<T>();

        public T GetById(Int64 id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>Collection of the entities</returns>
        public IQueryable<T> Query()
        {
            return Entities.AsQueryable<T>();
        }

        /// <summary>
        /// Adds entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var index = Entities.IndexOf(entity);
            Entities[index] = entity;
        }

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
        }

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="entities">Collection of the entities</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in Entities)
                Entities.Remove(entity);
        }
    }
}
