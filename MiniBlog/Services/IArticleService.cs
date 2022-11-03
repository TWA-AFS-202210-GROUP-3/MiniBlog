using MiniBlog.Model;

namespace MiniBlog.Services
{
    public interface IArticleService
    {
        Article Create(Article article);
        Article GetById(Guid id);
        List<Article> List();
    }
}