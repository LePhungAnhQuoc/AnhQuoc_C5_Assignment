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

        public ObservableCollection<LoanSlip> FillByIdUser(ObservableCollection<LoanSlip> source, string idUser, bool ignoreCase = false)
        {
            return source.Where(item => string.Compare(item.IdUser, idUser, ignoreCase) == 0).ToObservableCollection();
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




        public bool IsOutOfExpireDate(ObservableCollection<LoanSlip> loanSlips) 
        {
            return loanSlips.FirstOrDefault((item) => IsOutOfExpireDate(item)) != null;
        }


        public ObservableCollection<LoanSlip> FillByLastYear()
        {
            return Repo.Gets().Where((item) => item.LoanDate.Year == (DateTime.Now.Year - 1)).ToObservableCollection();
        }

        public ObservableCollection<LoanSlip> FillByReaderFullName(ObservableCollection<LoanSlip> source, string readerFullName, bool igNoreCase)
        {
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            ReaderMap readerMap = UnitOfMap.Instance.ReaderMap;
            LoanSlipMap map = UnitOfMap.Instance.LoanSlipMap;
            ObservableCollection<LoanSlip> results = new ObservableCollection<LoanSlip>();
            foreach (LoanSlip item in source)
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


        public LoanSlip FindByMostQuantity()
        {
            return Repo.Gets().OrderByDescending(item => item.Quantity).FirstOrDefault();
        }

        public bool IsOutOfExpireDate(LoanSlip loan)
        {
            return DateTime.Now > loan.ExpDate;
        }

        public LoanSlip CreateByDto(LoanSlipDto dto)
        {
            LoanSlip loanSlip = new LoanSlip();
            Copy(loanSlip, dto);
            return loanSlip;
        }

        public void Copy(LoanSlip dest, LoanSlipDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(LoanSlipDto dest, LoanSlipDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
