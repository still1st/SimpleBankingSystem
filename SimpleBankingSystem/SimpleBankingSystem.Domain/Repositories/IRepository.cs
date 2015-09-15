using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Gets all entities
        /// </summary>
        /// <returns>Collection of the entities</returns>
        IEnumerable<T> GetAll();

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
    }
}
