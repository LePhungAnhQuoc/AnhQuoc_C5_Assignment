using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipViewModel: BaseViewModel<LoanSlip>
    {
        public LoanSlipViewModel()
        {
            Item = new LoanSlip();
            Repo = new LoanSlipRepository();
            prefix = Constants.prefixLoan;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public LoanSlip FindById(string id)
        {
            foreach (LoanSlip item in Repo)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public LoanSlipDto FindById(ObservableCollection<LoanSlipDto> source, string id)
        {
            foreach (LoanSlipDto item in source)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }


        public ObservableCollection<LoanSlip> FillByIdReader(string idReader, bool ignoreCase = false)
        {
            return FillByIdReader(Repo.Gets(), idReader, ignoreCase);
        }

        public ObservableCollection<LoanSlip> FillByIdReader(ObservableCollection<LoanSlip> source, string idReader, bool ignoreCase = false)
        {
            return source.Where(item => string.Compare(item.IdReader , idReader, ignoreCase) == 0).ToObservableCollection();
        }

        public ObservableCollection<LoanSlip> GetByListReader(ObservableCollection<Reader> readers)
        {
            var source = Repo.Gets();

            ObservableCollection<LoanSlip> result = new ObservableCollection<LoanSlip>();
            foreach (Reader reader in readers)
            {
                result.AddRange(source.Where(loan => loan.IdReader == reader.Id));
            }

            return result;
        }
    }
}
