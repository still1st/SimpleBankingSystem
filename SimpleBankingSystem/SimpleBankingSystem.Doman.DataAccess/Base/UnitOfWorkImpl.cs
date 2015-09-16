using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Kingstar.Data.Base
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkImpl"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public UnitOfWorkImpl(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        protected DbContext DataContext
        {
            get
            {
                return _dataContext ?? (_dataContext = _contextFactory.GetContext());
            }
        }

        /// <summary>
        /// Commits current changes in the unit of work
        /// </summary>
        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
        }

        private readonly IContextFactory _contextFactory;
        private DbContext _dataContext;
    }
}
