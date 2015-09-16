using System;
using System.Data.Entity;

namespace Kingstar.Data.Base
{
    /// <summary>
    /// Provides basic interface for data context factory
    /// </summary>
    public interface IContextFactory : IDisposable
    {
        /// <summary>
        /// Gets the current context from the factory.
        /// </summary>
        /// <returns></returns>
        DbContext GetContext();
    }
}
