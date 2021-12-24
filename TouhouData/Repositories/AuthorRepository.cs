using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;
using TouhouData.Context;

namespace TouhouData.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(TouhouContext touhouContext) : base (touhouContext)
        {

        }

        public bool AuthorExists(string articleId)
        {
            throw new NotImplementedException();
        }

        public void CreateAuthor(Author article)
        {
            throw new NotImplementedException();
        }
    }
}
