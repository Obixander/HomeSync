using Azure.Core;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ActivityRepository(DataContext context) : GenericRepository<Activity>(context), IActivityRepository
    {

        public async Task UpdateActivities(Activity entity)
        {
            try
            {
                //NO WORKING
                context.Update(entity);   
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle any exception and log if necessary
                throw new ApplicationException("An error occurred while updating activities.", ex);
            }
        } 
        public async Task<List<Activity>> GetAllBy(int FamilyId)
        {
            DateTime referenceDate = DateTime.Now; //gets today might used for being able to change what week to get
            DateTime startOfWeek = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1); // End of Sunday
            
            return await context.Activities.Where(x => x.FamilyId == FamilyId && x.StartDate >= startOfWeek.Date && x.EndDate <= endOfWeek.Date).Include(u => u.AssignedMembers).ToListAsync();
        }

        public async Task SaveActivity(Activity Entity)
        {
            try
            {
                List<int> userIds = new();
                foreach (var user in Entity.AssignedMembers ?? throw new NullReferenceException())
                {
                    userIds.Add(user.UserId);
                }
                var assignedUsers = await context.Users
                    .Where(u => userIds.Contains(u.UserId))
                    .ToListAsync();
                Entity.AssignedMembers = assignedUsers;
                await context.AddAsync(Entity);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
