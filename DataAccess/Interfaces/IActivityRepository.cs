using Entities;

namespace DataAccess.Interfaces
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAllBy(int familyId);
    }
}