namespace MiniBlog.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using MiniBlog.Model;
    using MiniBlog.Stores;

    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private IArticleStore articleStore;
        private IUserStore userStore;

        public ArticleController(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        [HttpGet]
        public List<Article> List()
        {
            return this.articleStore.GetAll();
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!this.userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    userStore.Save(new User(article.UserName));
                }

                this.articleStore.Save(article);
            }

            return new CreatedResult("/Article", article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle =
                articleStore.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}