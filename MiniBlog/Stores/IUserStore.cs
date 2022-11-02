using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public interface IUserStore
    {
        bool Delete(User user);
        List<User> GetAll();
        User Save(User user);
    }
}