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
        private ArticleService articleService;

        public ArticleController(ArticleService articleService)
        {
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
            return articleService.GetByID(id);
        }
    }
}