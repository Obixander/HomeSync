using Entities;

namespace Website.Client.Data
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
