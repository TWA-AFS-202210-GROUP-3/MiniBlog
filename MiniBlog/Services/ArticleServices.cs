using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleServices: IArticleServices
    {
        private readonly IArticleStore _articleStore;
        private readonly IUserStore _userStore;

        public ArticleServices(IArticleStore articleStore, IUserStore userStore)
        {
            _articleStore = articleStore;
            _userStore = userStore;
        }

        public Article Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!_userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    _userStore.Save(new User(article.UserName));
                }

                _articleStore.Save(article);
            }

            return article;
        }

        public Article GetById(Guid id)
        {
            var foundArticle =
                _articleStore.GetAll().FirstOrDefault(article => article.Id == id);

            return foundArticle;
        }

        public List<Article> List()
        {
            return _articleStore.GetAll();
        }
    }
}
