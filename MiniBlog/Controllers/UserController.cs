using MiniBlog.Model;
using MiniBlog.Stores;
using Microsoft.AspNetCore.Mvc;

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
            user = this.userService.Register(user);

            return new CreatedResult($"/Article/{user.Name}", user);
            //if (!userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            //{
            //    userStore.Save(user);
            //}

            //return new CreatedResult($"/users/{user.Name}", user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return this.userStore.GetAll();
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = userStore.GetAll().FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = userStore.GetAll().FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                this.userStore.Delete(foundUser);
                var articles = this.articleStore.GetAll()
                    .Where(article => article.UserName == foundUser.Name)
                    .ToList();
                articles.ForEach(article => this.articleStore.Delete(article));
            }

            return foundUser;
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userStore.GetAll().FirstOrDefault(_ =>
                string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? throw new
                InvalidOperationException();
        }
    }
}