using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomListItem
    {
        private int customlistItemId;
        private string content;
        private bool completed;
        private int customListId;  // Foreign key to CustomList
        public CustomListItem()
        {
            
        }
        public CustomListItem(string content)
        {
            Content = content;
        }
        public CustomListItem(string content, bool Completed)
        {
            Content = content;
            this.Completed = Completed;
        }
        public int CustomlistItemId { get => customlistItemId; set => customlistItemId = value; }
        public string Content { get => content; set => content = value; }
        public bool Completed { get => completed; set => completed = value; }

        public int CustomListId { get => customListId; set => customListId = value; }

        // Navigation property (not required but useful for EF Core to track the relationship)
        public CustomList CustomList { get; set; }
    }
}
