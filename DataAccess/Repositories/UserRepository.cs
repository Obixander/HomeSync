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
    public class UserRepository(DataContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<bool> CreateUser(User user)
        {
            if (await context.FindAsync<User>(user) != null)
            {
                return false;
            }
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            if(await context.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password) != null)
            {
                return true;
            }
            return false;
        }



    }
}
