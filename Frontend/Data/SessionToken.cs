using Entities;

namespace Frontend.Data
{
    public class SessionToken
    {
        public SessionToken()
        {
            CurrentAccount = new();
        }
        public User CurrentAccount { get; set; }

    }
}
