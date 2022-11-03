using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Service
{
    public class UserService
    {
        private IArticleStore _articleStore;
        private IUserStore _userStore;

        public UserService(IArticleStore _articleStore, IUserStore _userStore)
        {
            this._articleStore = _articleStore;
            this._userStore = _userStore;
        }

        public User RegisterUser(User user)
        {
            if (!_userStore.GetAll().Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                _userStore.Save(user);
            }

            return user;
        }

        public List<User> GetAllUsers()
        {
            return _userStore.GetAll();
        }

        public User UpdateUserInfo(User user)
        {
            var foundUser = _userStore.GetAll().FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        public User DeleteUser(string name)
        {
            var foundUser = _userStore.GetAll().FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                _userStore.Delete(foundUser);
                var articles = _articleStore.GetAll()
                    .Where(article => article.UserName == foundUser.Name)
                    .ToList();
                articles.ForEach(article => _articleStore.Delete(article));
            }

            return foundUser;
        }

        public User GetByName(string name)
        {
            return _userStore.GetAll().FirstOrDefault(_ =>
                string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? throw new
                InvalidOperationException();
        }
    }
}
