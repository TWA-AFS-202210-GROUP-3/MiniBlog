using MiniBlog.Model;
using MiniBlog.Stores;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Services;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            return new CreatedResult($"/user/{user.Name}", this.userService.Register(user));
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userService.GetAll();
        }

        [HttpPut]
        public User Update(User user)
        {
            return userService.Update(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            return userService.Delete(name);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.GetByName(name);
        }
    }
}