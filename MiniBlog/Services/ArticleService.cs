using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleService: IArticleService
    {
        private IArticleStore articleStore;
        private IUserStore userStore;
        public ArticleService(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        public Article Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    userStore.Save(new User(article.UserName));
                }

                articleStore.Save(article);
            }

            return article;
        }

        public List<Article> List()
        {
            return articleStore.GetAll();
        }

        public Article GetById(Guid id)
        {
            var foundArticle =
                articleStore.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}
