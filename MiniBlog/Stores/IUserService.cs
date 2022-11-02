using MiniBlog.Model;

namespace MiniBlog.Stores;

public interface IUserService
{
    User Register(User user);
    List<User> GetAll();
    User Update(User user);
    User Delete(string name);
}