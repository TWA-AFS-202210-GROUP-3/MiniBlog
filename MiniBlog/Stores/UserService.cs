using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class UserService : IArticalService
    {
        private IArticleStore articleStore;
        private IUserStore userStore;

        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public User Register(User user)
        {
            if (!userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Save(user);
            }

            return user;
        }

        public List<User> GetAll()
        {
            return this.userStore.GetAll();
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
    }
}
