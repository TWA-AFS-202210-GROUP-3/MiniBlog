using MiniBlog.Model;

namespace MiniBlog.Services
{
    public interface IArticleService
    {
        Article Create(Article article);
        List<Article> GetAll();
        Article GetById(Guid id);
    }
}