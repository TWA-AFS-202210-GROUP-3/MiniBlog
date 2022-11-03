using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService : IUserService
    {
        private IArticleStore articleStore;
        private IUserStore userStore;

        public UserService(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        public User Register(User user)
        {
            if (!this.userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                this.userStore.Save(user);
            }

            return user;
        }

        public List<User> GetAll()
        {
            return this.userStore.GetAll();
        }

        public User Update(User user)
        {
            var foundUser = this.userStore.GetAll().FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }
            return foundUser;
        }

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
    }
}
