using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FamilyRepository(DataContext context) : GenericRepository<Family>(context), IFamilyRepository
    {
        public async Task<Family> GetFamilyMembersBy(int FamilyId)
        {
            try
            {
                Family family = await context.Families.AsNoTracking().Include(e => e.Members).ThenInclude(m => m.UserRoles).ThenInclude(ur => ur.Role).SingleOrDefaultAsync(j => j.FamilyId == FamilyId) ?? throw new NullReferenceException();
                return family;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
