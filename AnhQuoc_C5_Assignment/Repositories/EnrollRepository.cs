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
    public class EnrollRepository : Repository<Enroll>
    {
        public EnrollRepository() : base()
        {
        }
        public EnrollRepository(ObservableCollection<Enroll> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.Enrolls.ToObservableCollection();
        }

        public override void WriteAdd(Enroll item)
        {
            dbSource.Enrolls.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(Enroll item)
        {
            dbSource.Enrolls.Remove(item);
            dbSource.SaveChanges();
        }

        public override void WriteUpdate(Enroll updated)
        {
            var enrollViewModel = UnitOfViewModel.Instance.EnrollViewModel;

            Enroll itemSource = dbSource.Enrolls.FirstOrDefault(sourceItem => sourceItem.IdReader == updated.IdReader);
            enrollViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
