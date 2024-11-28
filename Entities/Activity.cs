using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum StatusOfActivity
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }
    public class Activity
    {
        private int activityId;
        private string name;
        private DateTime startDate;
        private DateTime endDate;
        private string description;
        private List<User>? assignedMembers;
        private StatusOfActivity status;

        public Activity()
        {
            
        }
        public Activity(string name, DateTime start, DateTime end, StatusOfActivity status = 0)
        {
            Name = name;
            StartDate = start;
            EndDate = end;
            Status = status;
        }
        public int ActivityId { get => activityId; set => activityId = value; }
        public string Name { get => name; set => name = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Description { get => description; set => description = value; }
        public List<User>? AssignedMembers { get => assignedMembers; set => assignedMembers = value; }
        public StatusOfActivity Status { get => status; set => status = value; }
    }
}
