using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class ArticalService : IArticalService
    {
        private IArticleStore articleStore;
        private IUserStore userStore;

        public ArticalService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public Article Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!this.userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    userStore.Save(new User(article.UserName));
                }

                this.articleStore.Save(article);
            }

            return article;
        }

        public Article GetById(Guid id)
        {
            var foundArticle = articleStore.GetAll().FirstOrDefault(article => article.Id == id);

            return foundArticle;
        }

        public List<Article> List()
        {
            return this.articleStore.GetAll();
        }
    }
}
