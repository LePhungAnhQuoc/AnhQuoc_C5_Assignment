using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ProvinceViewModel : BaseViewModel<Province>
    {
        public ProvinceViewModel()
        {
            Item = new Province();
            Item.Id = 0;
            Repo = new ProvinceRepository();
            prefix = Constants.prefixProvince;
            numberPrefix = 0;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Province FindById(int idValue)
        {
            return Repo.Gets().FirstOrDefault(item => item.Id == idValue);
        }

        public Province FindById(ObservableCollection<Province> items, int idValue)
        {
            return items.FirstOrDefault(item => item.Id == idValue);
        }
    }
}
