using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T>(DataContext context) : IGenericRepository<T> where T : class
    {
        //i want to do this all async but i dont know if it makes much sense

        public async Task Add(T entity)
        {
            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAt(int id)
        {
            try
            {
                context.Remove(id);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await context.Set<T>().AsNoTracking().ToListAsync(); //no tracking as the is returns to the client and is not needed to be tracked
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> GetBy(int id)
        {
            try
            {
                return await context.Set<T>().FindAsync(id) ?? throw new Exception();
            }
            catch
            {
                throw;
            }

        }

        public async Task Update(T entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
