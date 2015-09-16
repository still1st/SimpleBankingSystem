using SimpleBankingSystem.Domain.DataAccess;
using System;
using System.Data.Entity;

namespace SimpleBankingSystem.Domain.DataAccess.Base
{
    class DefaultContextFactoryImpl : Disposable, IContextFactory
    {
        /// <summary>
        /// Performs actual disposing
        /// </summary>
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }

        /// <summary>
        /// Gets the current context from the factory.
        /// </summary>
        /// <returns></returns>
        public DbContext GetContext()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("ContextFactory");

            return _dataContext ?? (_dataContext = new SimpleBankingSystemContext());
        }

        /// <summary>
        /// The cached instacne of the data context
        /// </summary>
        private SimpleBankingSystemContext _dataContext;
    }
}
