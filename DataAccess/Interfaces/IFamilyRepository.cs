using Entities;

namespace DataAccess.Interfaces
{
    public interface IFamilyRepository : IGenericRepository<Family>
    {
        Task<Family> GetFamilyMembersBy(int FamilyId);
    }
}