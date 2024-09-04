using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AuthorViewModel: BaseViewModel<Author>
    {
        public AuthorViewModel()
        {
            Item = new Author();
            Repo = new AuthorRepository(new APIProvider<Author>(nameof(Author)));
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


        public ObservableCollection<Author> FillByTextSearch(ObservableCollection<Author> source, string textSearch, bool igNoreCase)
        {
            AuthorMap authorMap = UnitOfMap.Instance.AuthorMap;
            ObservableCollection<Author> results = new ObservableCollection<Author>();
            foreach (Author item in source)
            {
                var itemDto = authorMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, igNoreCase);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }


        public bool IsCheckEmptyItem(AuthorDto item)
        {
            return Utilitys.IsCheckEmptyItem(item, false);
        }

        public Author CreateByDto(AuthorDto dto)
        {
            Author author = new Author();
            Copy(author, dto);
            return author;
        }

        public void Copy(Author dest, AuthorDto source)
        {
            Utilitys.Copy(dest, source);
        }

        public void Copy(AuthorDto dest, AuthorDto source)
        {
            Utilitys.Copy(dest, source);
        }
    }
}
