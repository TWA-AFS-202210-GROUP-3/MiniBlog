using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService: IUserService
    {
        private IArticleStore articleStore;
        private IUserStore userStore;
        public UserService(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        public ActionResult<User> Register(User user)
        {
            if (!userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Save(user);
            }

            return new CreatedResult($"/user", user);
        }

        public List<User> GetAll()
        {
            return userStore.GetAll();
        }

        public User Update(User user)
        {
            var foundUser = userStore.GetAll().FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }
        public User Delete(string name)
        {
            var foundUser = userStore.GetAll().FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Delete(foundUser);
                var articles = articleStore.GetAll()
                    .Where(article => article.UserName == foundUser.Name)
                    .ToList();
                articles.ForEach(article => articleStore.Delete(article));
            }

            return foundUser;
        }

        public User GetByName(string name)
        {
            return userStore.GetAll().FirstOrDefault(_ =>
                string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? throw new
                InvalidOperationException();
        }
    }
}
