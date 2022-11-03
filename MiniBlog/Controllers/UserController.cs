using MiniBlog.Model;
using MiniBlog.Stores;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Service;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            return new CreatedResult("/user", userService.RegisterUser(user));
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userService.GetAllUsers();
        }

        [HttpPut]
        public User Update(User user)
        {
            return userService.UpdateUserInfo(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            return userService.DeleteUser(name);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.GetByName(name);
        }
    }
}