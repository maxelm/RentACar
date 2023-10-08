namespace RentACar.Application.Contracts.Persistence
{
	public interface IAsyncRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(Guid id);
		Task<IReadOnlyCollection<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
	}
}
