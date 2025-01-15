using Entities;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UpdateRole(User Member, Role newRole);
        Task<User> Login(User user);
        Task<string> CreateAccount(User user, Family family);
    }
}