namespace Entities
{
    public class User
    {
        private int userId;
        private string userName;
        private string password;
        private string profilePhoto;
        public User()
        {
            
        }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
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
    }
}
