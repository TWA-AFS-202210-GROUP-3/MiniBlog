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
        // dependency injection
        private readonly IArticleStore _articleStore;
        private IUserServices _userServices;
        private readonly IUserStore _userStore;
    

        public UserController(IArticleStore articleStore, IUserStore userStore,IUserServices userServices)
        {
            _articleStore = articleStore;
            _userStore = userStore;
            _userServices = userServices;
        }

        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            return new CreatedResult("/user", _userServices.Register(user));
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return _userServices.GetAll();
        }

        [HttpPut]
        public User Update(User user)
        {

            return _userServices.Update(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {

            return _userServices.Delete(name);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return _userServices.GetByName(name);
        }
    }
}