using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomList
    {
        private int customListId;
        private string name;
        private List<CustomListItem>? items;
        private DateTime startDate;
        private DateTime endDate;
        public CustomList()
        {
            
        }
        public CustomList(string name, DateTime Start, DateTime End)
        {
            Name = name;
            StartDate = Start;
            EndDate = End;
        }

        public int CustomListId { get => customListId; set => customListId = value; }
        public string Name { get => name; set => name = value; }
        public List<CustomListItem>? Items { get => items; set => items = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
    }
}
