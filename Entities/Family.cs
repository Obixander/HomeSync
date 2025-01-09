using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Family
    {
        private int familyId;
        private List<User> members;
        private List<Activity> activities;
        private List<CustomList> customLists;
        private string familyName;
        private string familyPassword;

        //public Family()
        //{
        //    Members = new();
        //    Activities = new();
        //    CustomLists = new();
        //}
        public Family()
        {
            Members = new();
            Activities = new();
            CustomLists = new();
        }

        public int FamilyId { get => familyId; set => familyId = value; }
        public List<User> Members { get => members; set => members = value; }
        public List<Activity> Activities { get => activities; set => activities = value; }
        public List<CustomList> CustomLists { get => customLists; set => customLists = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string FamilyPassword { get => familyPassword; set => familyPassword = value; }
    }
}
