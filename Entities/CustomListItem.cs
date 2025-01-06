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
    }
}
