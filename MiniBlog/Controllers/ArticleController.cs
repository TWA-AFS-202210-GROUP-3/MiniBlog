using MiniBlog.Services;

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
        private readonly IArticleService articleService;

        public ArticleController(IArticleStore articleStore, IUserStore userStore, IArticleService articleService)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return this.articleService.List();
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {
            

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