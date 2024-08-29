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
            Repo = new ProvinceRepository(new APIProvider<Province>(nameof(Province)));
            prefix = Constants.prefixProvince;
            numberPrefix = 0;
        }

        public int GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return GetId(index);
        }

        public new int GetId(int index)
        {
            int result = -1;
            result = (index + 1);

            return result;
        }

        public Province FindById(int idValue)
        {
            return Repo.Gets().FirstOrDefault(item => item.Id == idValue);
        }

        public Province FindByName(string nameValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Name, nameValue, false) == 0);
        }

        public Province FindById(ObservableCollection<Province> items, int idValue)
        {
            return items.FirstOrDefault(item => item.Id == idValue);
        }

        public bool IsCheckEmptyItem(ProvinceDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public Province CreateByDto(ProvinceDto dto)
        {
            Province province = new Province();
            Copy(province, dto);
            return province;
        }

        public void Copy(Province dest, ProvinceDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(ProvinceDto dest, ProvinceDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
