using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class LoanHistoryViewModel : BaseViewModel<LoanHistory>
    {
        public LoanHistoryViewModel()
        {
            Item = new LoanHistory();
            Repo = new LoanHistoryRepository();
            prefix = Constants.prefixLoanHistory;
            numberPrefix = 2;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public LoanHistory FindByIdReader(string idReader, bool? statusValue = null)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);
            return source.FirstOrDefault(item => item.IdReader == idReader);
        }

        public LoanHistory FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public LoanHistory FindById(ObservableCollection<LoanHistory> items, string idValue)
        {
            return items.FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public bool IsCheckEmptyItem(LoanHistoryDto item)
        {
            if (Utilities.IsCheckEmptyString(item.Id))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.IdReader))
            {
                return false;
            }

            if (item.Quantity == 0)
            {
                return false;
            }

            if (item.LoanDate == Constants.dateEmptyValue)
            {
                return false;
            }
            if (item.ExpDate == Constants.dateEmptyValue)
            {
                return false;
            }

            if (item.LoanPaid == 0)
            {
                return false;
            }

            if (item.Deposit == 0)
            {
                return false;
            }
            
            if (item.Total == 0)
            {
                return false;
            }

            if (item.CreateAt == Constants.dateEmptyValue)
            {
                return false;
            }
            return true;
        }


        public LoanHistory CreateByDto(LoanHistoryDto dto)
        {
            LoanHistory loanHistory = new LoanHistory();
            loanHistory.Id = dto.Id;
            Copy(loanHistory, dto);
            return loanHistory;
        }

        public void Copy(LoanHistory dest, LoanHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(LoanHistoryDto dest, LoanHistoryDto source)
        {
            Utilities.Copy(dest, source);
        }


        public ObservableCollection<LoanHistory> FillByIdReader(ObservableCollection<LoanHistory> source, string idReader, bool ignoreCase = false)
        {
            return source.Where(item => string.Compare(item.IdReader, idReader, ignoreCase) == 0).ToObservableCollection();
        }

        public ObservableCollection<LoanHistory> FillByIdUser(ObservableCollection<LoanHistory> source, string idUser, bool ignoreCase = false)
        {
            return source.Where(item => string.Compare(item.IdUser, idUser, ignoreCase) == 0).ToObservableCollection();
        }

        public ObservableCollection<LoanHistory> FillByReaderFullName(ObservableCollection<LoanHistory> source, string readerFullName, bool igNoreCase)
        {
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            ReaderMap readerMap = UnitOfMap.Instance.ReaderMap;
            LoanHistoryMap map = UnitOfMap.Instance.LoanHistoryMap;
            ObservableCollection<LoanHistory> results = new ObservableCollection<LoanHistory>();
            foreach (LoanHistory item in source)
            {
                var itemDto = map.ConvertToDto(item);

                Reader readerFinded = readerVM.FindById(item.IdReader);
                ReaderDto readerDtoFinded = readerMap.ConvertToDto(readerFinded);

                bool isCheck = readerDtoFinded.FullName.ContainsCorrectly(readerFullName, igNoreCase);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }

    }
}
