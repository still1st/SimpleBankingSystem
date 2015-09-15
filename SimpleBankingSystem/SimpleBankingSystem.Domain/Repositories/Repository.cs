using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Default implementation repository for entities
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class Repository<T> : IRepository<T> where T: class
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
        public IEnumerable<T> GetAll()
        {
            return Entities;
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
    }
}
