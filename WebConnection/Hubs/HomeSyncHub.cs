using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.Interfaces;

namespace WebConnection.Hubs
{
    public class HomeSyncHub(IActivityRepository activityRepository,IFamilyRepository familyRepository, IUserRepository userRepository) : Hub, IHomeSyncHub
    {
        public async Task<Family> GetFamily(int FamilyId)
        {
            return await familyRepository.GetFamilyMembersBy(FamilyId);
        }
        public async Task<List<Activity>> GetAllActivities (int FamilyId)
        {
            return await activityRepository.GetAllBy(FamilyId); //this should return the activities for that familyid to the caller
        }
        public async Task CreateTask(string familyId, Activity activity) //This method should in theory add the newly made activity to the database and then send to all 
        {
            await activityRepository.Add(activity);
            await Clients.Group(familyId).SendAsync("ActivityAdded", activity); 
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

