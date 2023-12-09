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
    public class ProvinceRepository : Repository<Province>
    {
        public ProvinceRepository(): base()
        {
        }

        public ProvinceRepository(ObservableCollection<Province> items): base(items)
        {
        }
        
        public override void WriteUpdate(Province updated)
        {
            var ProvinceViewModel = UnitOfViewModel.Instance.ProvinceViewModel;

            Province itemSource = dbSource.Provinces.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            ProvinceViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }

        public override void WriteUpdateStatus(Province item, bool status)
        {
            Province itemSource = dbSource.Provinces.FirstOrDefault(sourceItem => sourceItem.Id == item.Id);
            itemSource.Status = status;
            dbSource.SaveChanges();
        }
    }
}
