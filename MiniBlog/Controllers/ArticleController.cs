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
        private ArticalService articalService;

        public ArticleController(ArticalService articalService)
        {
            this.articalService = articalService;
        }

        [HttpGet]
        public List<Article> List()
        {
            List<Article> all = this.articalService.List();
            return all;
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {
            article = this.articalService.Create(article);

            return new CreatedResult("/Article", article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            Article foundArticle = this.articalService.GetById(id);

            return foundArticle;
        }
    }
}