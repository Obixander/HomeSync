using Entities;

namespace DataAccess.Interfaces
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAllBy(int familyId);
        Task SaveActivity(Activity Entity);
        Task UpdateActivities(Activity Entity);
        Task DeleteActivity(Activity Entity);
    }
}