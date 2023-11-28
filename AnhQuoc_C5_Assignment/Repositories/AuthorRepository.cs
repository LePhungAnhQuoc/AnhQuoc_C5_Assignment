using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository() : base()
        {
        }
        public AuthorRepository(ObservableCollection<Author> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Authors.ToObservableCollection();
        }

        public override void WriteAdd(Author item)
        {
            dbSource.Authors.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(Author item)
        {
            dbSource.Authors.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(Author updated)
        {
            var authorViewModel = UnitOfViewModel.Instance.AuthorViewModel;

            Author itemSource = dbSource.Authors.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            authorViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Author item, bool status)
        {
            Author itemSource = dbSource.Authors.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
