using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;

namespace Website.Hubs
{
    public class HomeSyncHub(IGenericRepository<Activity> activityRepository,IGenericRepository<Family> familyRepository, IUserRepository userRepository) : Hub, IHomeSyncHub
    {
        //after login in this method should be the first thing someone calls when they connect to the hub for the first time!
        public async Task JoinFamilyGroup(string familyId)
        {
            // Add the user to the group based on familyId
            await Groups.AddToGroupAsync(Context.ConnectionId, familyId);
        }

        public async Task CreateTask(string familyId, Activity activity) //This method should in theory add the newly made activity to the database and then send to all 
        {
            await activityRepository.Add(activity);
            await Clients.Group(familyId).SendAsync("ActivityUpdated", activity); 
        }
        public async Task<User> Login(User user)
        {
            return await userRepository.Login(user);
        }
        public async Task<bool> Register(User user)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, user.FamilyId.ToString() ?? throw new NullReferenceException());
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
            
        }

    }
}

