using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class StatisticalViewModel : BaseViewModel<Statistical>
    {
        public StatisticalViewModel()
        {
            Item = new Statistical();
            Repo = new StatisticalRepository();
            prefix = string.Empty;
            numberPrefix = 1;
        }

        public Statistical FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.DateTime.ToString(), idValue, false) == 0);
        }

        public Statistical FindById(ObservableCollection<Statistical> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.DateTime.ToString(), idValue, false) == 0);
        }

        public Statistical CreateByDto(StatisticalDto dto)
        {
            Statistical statistical = new Statistical();
            Copy(statistical, dto);
            return statistical;
        }

        public void Copy(Statistical dest, StatisticalDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(StatisticalDto dest, StatisticalDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
