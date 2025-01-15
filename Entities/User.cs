using System.Text.Json.Serialization;

namespace Entities
{
    public class User
    {
        private int userId;

        private string userName;
        private string password;
        private string profilePhoto;
        private ICollection<Activity>? activities;
        private int? familyId;  // Foreign key to Family (nullable)
        private Family? family; // Navigation property
        private List<Role> roles; //maybe does something idk anymore
        public User()
        {
            UserRoles = new List<UserRole>();
        }
        public User(int userId)
        {
            UserId = userId;
            UserRoles = new List<UserRole>();
        }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            UserRoles = new List<UserRole>();
        }

        public int UserId
        { 
            get => userId;
            set
            {
                if (value != userId)
                userId = value;
            }
        }
        public string UserName
        {
            get => userName;
            set => userName = value;
        }
        public string Password
        { 
            get => password;
            set 
            {
                if (value != password)
                password = value;
            }
        }

        public string ProfilePhoto { get => profilePhoto; set => profilePhoto = value; }
        public ICollection<Activity>? Activities { get => activities; set => activities = value; }
        public int? FamilyId { get => familyId; set => familyId = value; }
        [JsonIgnore]
        public Family? Family { get => family; set => family = value; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is User other)
            {
                return this.UserId == other.UserId && this.UserName == other.UserName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, UserName);
        }
    }
}
