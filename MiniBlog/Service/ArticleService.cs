using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Service
{
    public class ArticleService
    {
        private IArticleStore _articleStore;
        private IUserStore _userStore;

        public ArticleService(IArticleStore _articleStore, IUserStore userStore)
        {
            this._articleStore = _articleStore;
            _userStore = userStore;
        }

        public Article CreateArticle(Article article)
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

        public List<Article> GetAllArticles()
        {
            return _articleStore.GetAll();
        }
    }
}
