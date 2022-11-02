using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class UserStoreContext : IUserStore
    {
        List<User> _users = new List<User>();

        public bool Delete(User user)
        {
            _users.Remove(user);
            return true;
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User Save(User user)
        {
            _users.Add(user);
            return user;
        }
    }
}
