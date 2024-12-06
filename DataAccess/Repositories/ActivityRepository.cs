using Azure.Core;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ActivityRepository(DataContext context) : GenericRepository<Activity>(context), IActivityRepository
    {
        public async Task<List<Activity>> GetAllBy(int FamilyId)
        {
            return await context.Activities.Where(x => x.FamilyId == FamilyId).AsNoTracking().ToListAsync();
        }
    }
}
