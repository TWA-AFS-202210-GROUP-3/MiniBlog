using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;

namespace MiniBlog.Services;

public interface IArticleService
{
    List<Article> List();

    Article Create(Article article);

    Article GetById(Guid id);

    public bool Delete(Article article);
}