namespace MiniBlog.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using MiniBlog.Model;
    using MiniBlog.Service;
    using MiniBlog.Stores;

    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private IArticleStore _articleStore;
        private IUserStore _userStore;
        private ArticleService articleService;

        public ArticleController(IArticleStore _articleStore, IUserStore _userStore, ArticleService articleService)
        {
            this._articleStore = _articleStore;
            this._userStore = _userStore;
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleService.GetAllArticles();
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {
            return new CreatedResult("/article", articleService.CreateArticle(article));
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle =
                _articleStore.GetAll().FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}