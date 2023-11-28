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
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository() : base()
        {

        }
        public CategoryRepository(ObservableCollection<Category> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Categories.ToObservableCollection();
        }

        public override void WriteAdd(Category item)
        {
            dbSource.Categories.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(Category item)
        {
            dbSource.Categories.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(Category updated)
        {
            var CategoryViewModel = UnitOfViewModel.Instance.CategoryViewModel;

            Category itemSource = dbSource.Categories.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            CategoryViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Category item, bool status)
        {
            Category itemSource = dbSource.Categories.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
