using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;

namespace MiniBlog.Services;

public interface IArticleService
{
    List<Article> List();

    ActionResult<Article> Create(Article article);

    bool Delete(Article article);
}