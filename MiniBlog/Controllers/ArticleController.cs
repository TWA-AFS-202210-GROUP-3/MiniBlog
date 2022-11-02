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

        public ArticleController(IArticleStore articleStore)
        {
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
                if (!UserStoreWillReplaceInFuture.Instance.GetAll().Exists(_ => article.UserName == _.Name))
                {
                    UserStoreWillReplaceInFuture.Instance.Save(new User(article.UserName));
                }

                this.articleStore.Save(article);
            }

            return new CreatedResult("/Article", article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle =
                ArticleStoreWillReplaceInFuture.Instance.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}