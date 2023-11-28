using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AuthorViewModel: ViewModelBase<Author>
    {
        public AuthorViewModel()
        {
            Item = new Author();
            Repo = new AuthorRepository();
            prefix = Constants.prefixAuthor;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public Author FindById(string idValue)
        {
            foreach (Author author in Repo)
            {
                if (author.Id == idValue)
                {
                    return author;
                }
            }
            return null;
        }
    }
}
