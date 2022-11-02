using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;

namespace MiniBlog.Services
{
    public interface IUserService
    {
        ActionResult<User> Register(User user);
        List<User> GetAll();
        User Update(User user);
        User Delete(string name);
        User GetByName(string name);
    }
}