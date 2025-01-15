using Azure.Core;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository(DataContext context) : GenericRepository<User>(context), IUserRepository
    {

        public async Task<User> UpdateRole(User Member, Role newRole)
        {
            if (context.Entry(Member).State == EntityState.Detached)
            {
                context.Users.Attach(Member);
            }
            int temp = context.Roles.Where(u => u.RoleName == newRole.RoleName).Select(r => r.RoleId).First();
            Member.UserRoles.First().RoleId = temp;
            context.Entry(Member).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Member;
        }
        public async Task<string> CreateAccount(User user, Family family)
        {
            try
            {
                if (context.Families.Any(u => u.FamilyName == family.FamilyName) && context.Families.Any(u => u.FamilyPassword == family.FamilyPassword))
                {

                    if (context.Users.Any(g => g.UserName == user.UserName))
                    {
                        return "An Error has occured this username is already in use";
                    }
                    user.FamilyId = await context.Families.Where(u => u.FamilyName == family.FamilyName && u.FamilyPassword == family.FamilyPassword).Select(u => u.FamilyId).FirstOrDefaultAsync();
                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                    user.UserId = await context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).Select(i => i.UserId).FirstOrDefaultAsync();
                    UserRole role = new(user.UserId, 1);
                    context.UserRoles.Add(role);
                    await context.SaveChangesAsync();
                    return "Succes the user has been created";
                }
                return "Family does not exist or password is incorrect";
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> Login(User user)
        {
            try
            {
                if (await context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password) != null)
                {
                    return await context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync();
                    //return await context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password) ?? throw new NullReferenceException("if this happends something has gone very very wrong :C");
                }
                throw new NullReferenceException();
            }
            catch
            {
                throw;
            }
        }

    }
}
