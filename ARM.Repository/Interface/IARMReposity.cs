namespace ARM.Repository
{
    /// <summary>
    /// Defines methods for interacting with the app backend.
    /// </summary>
    public interface IARMReposity
    {
        /// <summary>
        /// Returns the login repository.
        /// </summary>
        ILoginRepository Login { get; }

        IProductRepository Product { get; }

        ISaleRepository Sale { get; }
    }
}