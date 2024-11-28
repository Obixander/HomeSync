namespace DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteAt(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetBy(int id);
    }
}