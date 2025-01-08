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
    public class CustomListRepository(DataContext context) : GenericRepository<CustomList>(context), ICustomListRepository
    {
        public async Task SaveList(CustomList list)
        {
            try
            {
                context.CustomLists.Add(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CustomList>> GetAllBy(int FamilyId)
        {
            DateTime referenceDate = DateTime.Now; //gets today might used for being able to change what week to get
            DateTime startOfWeek = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1); // End of Sunday

            return await context.CustomLists.Where(x => x.FamilyId == FamilyId && x.StartDate >= startOfWeek.Date && x.EndDate <= endOfWeek.Date).Include(c => c.Items).ToListAsync();
        }

        public async Task DeleteList(CustomList list)
        {
            if (list.Items == null)
            {
                list.Items = new();
            }
            foreach (var item in list.Items.ToList())
            {
                context.Remove(item); // Remove each item
            }

            context.Remove(list); // Then remove the list itself
            await context.SaveChangesAsync(); // Save changes
        }

    }
}
