using MiniBlog.Model;
using MiniBlog.Stores;
using Microsoft.AspNetCore.Mvc;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IArticleStore articleStore;
        private IUserStore userStore;

        public UserController(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        [HttpPost]
        public ActionResult<User> Register(User user)
        {
            if (!this.userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                this.userStore.Save(user);
            }

            return new CreatedResult($"/user/{user.Name}", user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return this.userStore.GetAll();
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = this.userStore.GetAll().FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = this.userStore.GetAll().FirstOrDefault(_ => _.Name == name);
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
            return this.userStore.GetAll().FirstOrDefault(_ =>
                string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? throw new
                InvalidOperationException();
        }
    }
}