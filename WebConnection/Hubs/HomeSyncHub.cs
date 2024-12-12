using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Diagnostics;

namespace WebConnection.Hubs
{
    public class HomeSyncHub(IActivityRepository activityRepository,IFamilyRepository familyRepository, IUserRepository userRepository) : Hub, IHomeSyncHub
    {
        public async Task<Family> GetFamily(int FamilyId)
        {
            return await familyRepository.GetFamilyMembersBy(FamilyId);
        }
        public async Task<string> GetAllActivities (int FamilyId)
        {
            var test = await activityRepository.GetAllBy(FamilyId); //this should return the activities for that familyid to the caller
            string Dto = JsonConvert.SerializeObject(test, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Dto;
        }
        public async Task SaveActivity(string familyId, Entities.Activity activity) //This method should in theory add the newly made activity to the database and then send to all 
        {
            await activityRepository.SaveActivity(activity);
            
            string Dto = JsonConvert.SerializeObject(activity, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await Clients.Group(familyId).SendAsync("ActivityAdded", Dto); 
        }
        public async Task UpdateActivity(string familyId, Entities.Activity activity)
        {
            Console.WriteLine("Sending Dto now to FamilyId: " + familyId);
            await activityRepository.UpdateActivities(activity);
            string Dto = JsonConvert.SerializeObject(activity, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await Clients.Group(familyId).SendAsync("ActivityUpdated", Dto);

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

