using MiniBlog.Model;

namespace MiniBlog.Services
{
    public interface IArticleServices
    {
        Article Create(Article article);
        List<Article> List();
        Article GetById(Guid id);

    }
}
