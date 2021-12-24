using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;
using TouhouData.Context;

namespace TouhouData.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(TouhouContext touhouContext) : base(touhouContext)
        {

        }

        public bool ArticleExists(string articleId)
        {
            throw new NotImplementedException();
        }

        public void CreateArticle(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
