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
        private readonly IArticleStore articleStore;
        private readonly IUserStore userStore;

        public ArticleController(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleStore.GetAll();
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!this.userStore.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    this.userStore.Save(new User(article.UserName));
                }

                articleStore.Save(article);
            }

            return new CreatedResult("/Arctile", article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle =
                this.articleStore.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}