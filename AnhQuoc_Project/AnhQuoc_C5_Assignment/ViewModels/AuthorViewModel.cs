using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AuthorViewModel: BaseViewModel<Author>
    {
        public AuthorViewModel()
        {
            Item = new Author();
            Repo = new AuthorRepository();
            prefix = Constants.prefixAuthor;
            numberPrefix = 2;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public Author FindById(string idValue)
        {
            return FindById(Repo.Gets(), idValue);
        }

        public Author FindById(ObservableCollection<Author> source, string idValue)
        {
            return source.FirstOrDefault(item => item.Id == idValue);

        }


        public bool IsCheckEmptyItem(AuthorDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public Author CreateByDto(AuthorDto dto)
        {
            Author author = new Author();
            Copy(author, dto);
            return author;
        }

        public void Copy(Author dest, AuthorDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(AuthorDto dest, AuthorDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
