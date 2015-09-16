namespace Kingstar.Data.Base
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits current changes in the unit of work
        /// </summary>
        void Commit();
    }
}
