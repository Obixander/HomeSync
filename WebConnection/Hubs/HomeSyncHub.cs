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
    public class HomeSyncHub(ICustomListRepository customListRepository, IActivityRepository activityRepository,IFamilyRepository familyRepository, IUserRepository userRepository) : Hub, IHomeSyncHub
    {
        public async Task<string> CreateFamily(Family family)
        {
            try
            {
                await familyRepository.Add(family);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> CreateAccount(User user, Family family)
        { //the user that's received for some reason has the family to be null????  
            return await userRepository.CreateAccount(user, family);
        }
        public async Task DeleteList(string FamilyId, CustomList list)
        {
            try
            {
                await customListRepository.DeleteList(list);
                string Dto = JsonConvert.SerializeObject(list, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                await Clients.Group(FamilyId).SendAsync("ListDeleted", Dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateList(int FamilyId, CustomList list)
        {
            await customListRepository.Update(list);
            string Dto = JsonConvert.SerializeObject(list, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await Clients.Group(FamilyId.ToString()).SendAsync("ListUpdated", Dto);
        }
        public async Task<string> GetAllLists(int familyId)
        {
            var test = await customListRepository.GetAllBy(familyId); //this should return the activities for that familyid to the caller
            string Dto = JsonConvert.SerializeObject(test, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Dto;
        }

        public async Task SaveList(string familyId, CustomList list)
        {
            try
            {
                await customListRepository.SaveList(list);

                string Dto = JsonConvert.SerializeObject(list, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                await Clients.Group(familyId).SendAsync("ListAdded", Dto);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
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
        public async Task DeleteActivity(string familyId, Entities.Activity activity)
        {
            try
            {
                await activityRepository.DeleteActivity(activity);
                string Dto = JsonConvert.SerializeObject(activity, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                await Clients.Group(familyId).SendAsync("ActivityDeleted", Dto);
            }
            catch (Exception ex)
            {
                throw;
            }
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

