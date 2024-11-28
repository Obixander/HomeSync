using Entities;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> Login(string username,string password);
        Task<bool> CreateUser(User user);
    }
}