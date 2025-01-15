using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebConnection.Hubs
{
    public class HomeSyncHub(ICustomListRepository customListRepository, IActivityRepository activityRepository, IFamilyRepository familyRepository, IUserRepository userRepository) : Hub, IHomeSyncHub
    {




        public async Task UpdateUserRole(User Member, Role newRole)
        {
            var temp = await userRepository.UpdateRole(Member, newRole);
            string Dto = JsonConvert.SerializeObject(temp, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await Clients.Group(Member.FamilyId.ToString()).SendAsync("UserUpdated", Dto);
        }

        /// <summary>
        /// this method is used to add a family to the database
        /// </summary>
        /// <param name="family">the family that gets added to the database</param>
        /// <returns>A string with either Succes or the error message</returns>
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

        /// <summary>
        /// This method is used to add an account to the database
        /// </summary>
        /// <param name="user">This the account that gets added to the database</param>
        /// <param name="family">this is used to check if the family the account is trying to join exists</param>
        /// <returns>Returns a string with a message of succes or error</returns>
        public async Task<string> CreateAccount(User user, Family family)
        {
            try
            {
                await userRepository.CreateAccount(user, family);
                return "Succes";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// This method is used to delete an list of the database
        /// </summary>
        /// <param name="FamilyId">is Used to find the right group to send the update to</param>
        /// <param name="list">the list to be deleted from the database</param>
        /// <returns>void</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FamilyId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
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
        public async Task<string> GetFamily(int FamilyId)
        {
            return JsonConvert.SerializeObject(await familyRepository.GetFamilyMembersBy(FamilyId), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        public async Task<string> GetAllActivities(int FamilyId)
        {
            var test = await activityRepository.GetAllBy(FamilyId); //this should return the activities for that familyid to the caller
            string Dto = JsonConvert.SerializeObject(test, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Dto;
        }
        public async Task SaveActivity(string familyId, Entities.Activity activity)
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
        public async Task<string> Login(User user)
        {
            return JsonConvert.SerializeObject(await userRepository.Login(user), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
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
        public async Task<bool> AccountUpdated(User user)
        {
            try
            {
                await userRepository.Update(user);
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

