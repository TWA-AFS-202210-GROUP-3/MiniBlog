using MiniBlog.Model;

namespace MiniBlog.Stores;

public interface IArticalService
{
    Article Create(Article article);

    Article GetById(Guid id);

    List<Article> List();
}