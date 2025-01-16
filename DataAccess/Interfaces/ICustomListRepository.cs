using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ICustomListRepository : IGenericRepository<CustomList>
    {
        Task RemoveItem(CustomListItem item);
        Task UpdateList(CustomList list);
        Task SaveList(CustomList list);
        Task<List<CustomList>> GetAllBy(int familyId);
        Task DeleteList(CustomList list);
    }
}
