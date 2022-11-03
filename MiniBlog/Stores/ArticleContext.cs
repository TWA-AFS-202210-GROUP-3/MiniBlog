using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class ArticleContext : IArticleStore
    {
        List<Article> _articles = new List<Article>();
        public bool Delete(Article articles)
        {
            return _articles.Remove(articles);
        }

        public List<Article> GetAll()
        {
            return _articles;
        }

        public Article Save(Article article)
        {
            _articles.Add(article);
            return article;
        }
    }
}
