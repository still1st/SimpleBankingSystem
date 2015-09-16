using System;

namespace SimpleBankingSystem.Domain.DataAccess.Base
{
    /// <summary>
    /// Implements default .NET disposable pattern
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable"/> class.
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(Boolean disposing)
        {
            if (!IsDisposed && disposing)
            {
                DisposeCore();
            }

            IsDisposed = true;
        }

        /// <summary>
        /// Performs actual disposing
        /// </summary>
        protected abstract void DisposeCore();

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        protected Boolean IsDisposed { get; private set; }
    }
}

