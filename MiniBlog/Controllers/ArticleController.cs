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
        // dependency injection
        private readonly IArticleStore _articleStore;
        private readonly IArticleServices _artickeServices;

        public ArticleController(IArticleStore articleStore, IArticleServices artickeServices)
        {
            this._articleStore = articleStore;
            this._artickeServices = artickeServices;
        }

        [HttpGet]
        public List<Article> List()
        {
            return _artickeServices.List();
        }

        [HttpPost]
        public ActionResult<Article> Create(Article article)
        {

            return new CreatedResult("/companies", _artickeServices.Create(article));
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return _artickeServices.GetById(id);
        }
    }
}