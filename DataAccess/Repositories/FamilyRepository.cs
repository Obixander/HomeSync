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
            return await context.Families.AsNoTracking().Include(e => e.Members).SingleOrDefaultAsync(j => j.FamilyId == FamilyId) ?? throw new NullReferenceException();
        }
    }
}
