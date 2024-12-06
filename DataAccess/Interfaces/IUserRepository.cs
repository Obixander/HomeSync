using Entities;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Login(User user);
        Task<string> CreateUser(User user);
    }
}