using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleStore articleStoreContext;
        private readonly IUserStore userStoreContext;

        public ArticleService(IArticleStore articleStoreContext, IUserStore userStoreContext)
        {
            this.articleStoreContext = articleStoreContext;
            this.userStoreContext = userStoreContext;
        }

        public List<Article> List()
        {
            return this.articleStoreContext.GetAll();
        }

        public Article Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!this.userStoreContext.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    this.userStoreContext.Save(new User(article.UserName));
                }

                this.articleStoreContext.Save(article);
            }

            return article;
        }

        public Article GetById(Guid id)
        {
            var foundArticle =
                this.articleStoreContext.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }

        public bool Delete(Article article)
        {
            return this.articleStoreContext.Delete(article);
        }
    }
}
