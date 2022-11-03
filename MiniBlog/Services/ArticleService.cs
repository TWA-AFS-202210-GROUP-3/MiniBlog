namespace MiniBlog.Services
{
    using MiniBlog.Model;
    using MiniBlog.Stores;

    public class ArticleService : IArticleService
    {
        private IArticleStore articleStore ;
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
                if (!this.userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    this.userStore.Save(new User(article.UserName));
                }

                this.articleStore.Save(article);
            }

            return article;
        }

        public List<Article> GetAll()
        {
            return this.articleStore.GetAll();
        }

        public Article GetById(Guid id)
        {
            var foundArticle =
                this.articleStore.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}
