using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucReaderLoanInformation.xaml
    /// </summary>
    public partial class ucReaderLoanInformation : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ReaderDto> getItem { get; set; }
        #endregion

        #region Property
        private ObservableCollection<Child> _AllChildOfAdult;
        public ObservableCollection<Child> AllChildOfAdult
        {
            get { return _AllChildOfAdult; }
            set
            {
                _AllChildOfAdult = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanSlip> _AllChildLoan;
        public ObservableCollection<LoanSlip> AllChildLoan
        {
            get { return _AllChildLoan; }
            set
            {
                _AllChildLoan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanDetail> _AllChildLoanDetail;
        public ObservableCollection<LoanDetail> AllChildLoanDetail
        {
            get { return _AllChildLoanDetail; }
            set
            {
                _AllChildLoanDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _AllChildBook;
        public ObservableCollection<Book> AllChildBook
        {
            get { return _AllChildBook; }
            set
            {
                _AllChildBook = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModel
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private ReaderViewModel readerVM;
        private LoanSlipViewModel loanVM;
        private BookTitleViewModel bookTitleVM;
        private BookISBNViewModel bookISBNVM;
        private BookViewModel bookVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;


        #endregion

        #region Mapping
        private LoanSlipMap loanMap;
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucReaderLoanInformation()
        {
            InitializeComponent();

            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            loanVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            loanMap = UnitOfMap.Instance.LoanSlipMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            #endregion

            this.DataContext = this;
            this.Loaded += FrmAdultInformation_Loaded;
        }
        
        private void FrmAdultInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Reader reader = readerVM.FindById(getItem().Id);
            Adult adult = adultVM.FindByIdReader(reader.Id, null);

            ucAdultInformation.getItem = () => adultMap.ConvertToDto(adult);
            GetBooksFromReader(reader);
            GetChildrenFromAdult(adult);
            GetChildBooks();
        }

        private void GetBooksFromReader(Reader reader)
        {
            var readerLoans = loanVM.FillByIdReader(reader.Id, false);
            var readerBooks = bookVM.GetBooksInLoanSlips(readerLoans);
            ucAdultBooksTable.Books = bookMap.ConvertToDto(readerBooks);
        }

        private void GetChildrenFromAdult(Adult adult)
        {
            AllChildOfAdult = childVM.FillByIdAdult(adult.IdReader, null);
            ucChildsTable.getItem_Status = () => null;
            ucChildsTable.getChilds = () => AllChildOfAdult;
        }

        private void GetChildBooks()
        {
            AllChildLoan = loanVM.GetByListReader(AllChildOfAdult.Select(child => readerVM.FindById(child.IdReader)).ToObservableCollection());
            AllChildLoanDetail = loanDetailVM.GetByListLoan(AllChildLoan);
            AllChildBook = bookVM.GetBooksInLoanDetails(AllChildLoanDetail);
            ucChildBooksTable.Books = bookMap.ConvertToDto(AllChildBook);
        }
    }
}
