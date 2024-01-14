using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucAddLoanHistory.xaml
    /// </summary>
    public partial class ucAddLoanHistory : UserControl, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ucLoanHistoryManagement> getParentUc { get; set; }

        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanDetailHistoryRepository> getLoanDetailHistoryRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<PenaltyReasonRepository> getReasonRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private bool handle = true;
        private ucInputReaderLoanHistory ucInputReaderLoanHistory;

        private Stack<object> _storeContent;
        public Stack<object> storeContent
        {
            get
            {
                if (_storeContent == null)
                    _storeContent = new Stack<object>();
                return _storeContent;
            }
        }

        #endregion

        #region Properties
        public bool IsCancel { get; set; } = true;
        public bool? StatusValue { get; set; } = true;

        #region Datas
        private ObservableCollection<AdultDto> _AllAdults;
        public ObservableCollection<AdultDto> AllAdults
        {
            get { return _AllAdults; }
            set
            {
                _AllAdults = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AdultDto> _AllGuardians;
        public ObservableCollection<AdultDto> AllGuardians
        {
            get { return _AllGuardians; }
            set
            {
                _AllGuardians = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChildDto> _AllChilds;
        public ObservableCollection<ChildDto> AllChilds
        {
            get { return _AllChilds; }
            set
            {
                _AllChilds = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookTitleDto> _AllBookNames;
        public ObservableCollection<BookTitleDto> AllBookNames
        {
            get { return _AllBookNames; }
            set
            {
                _AllBookNames = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _AllLanguages;
        public ObservableCollection<string> AllLanguages
        {
            get { return _AllLanguages; }
            set
            {
                _AllLanguages = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReaderType> _AllReaderTypes;
        public ObservableCollection<ReaderType> AllReaderTypes
        {
            get { return _AllReaderTypes; }
            set
            {
                _AllReaderTypes = value;
                OnPropertyChanged();
            }
        }

        private ReaderType? _SelectedReaderType;
        public ReaderType? SelectedReaderType
        {
            get { return _SelectedReaderType; }
            set
            {
                _SelectedReaderType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanSlip> _AllLoanOfReader;
        public ObservableCollection<LoanSlip> AllLoanOfReader
        {
            get { return _AllLoanOfReader; }
            set
            {
                _AllLoanOfReader = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PenaltyReasonDto> _AllReason;
        public ObservableCollection<PenaltyReasonDto> AllReason
        {
            get { return _AllReason; }
            set
            {
                _AllReason = value;
                OnPropertyChanged();
            }
        }



        #endregion

        public ObservableCollection<BookDto> UnPaidBooksOfReader { get; set; }
        public ObservableCollection<BookDto> PaidBooksOfReader { get; set; }
        public ObservableCollection<LoanDetail> UnPaidLoanDetailsOfReader { get; set; }
        public ObservableCollection<LoanDetail> PaidLoanDetailsOfReader { get; set; }

        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public ObservableCollection<ucBookCard> AllUnPaidBookCard { get; set; }
        public ObservableCollection<ucBookCard> AllPaidBookCard { get; set; }


        private ReaderDto _SelectedReader;
        public ReaderDto SelectedReader
        {
            get { return _SelectedReader; }
            set
            {
                _SelectedReader = value;
                OnPropertyChanged();
            }
        }

        private AdultDto _SelectedAdult;
        public AdultDto SelectedAdult
        {
            get { return _SelectedAdult; }
            set
            {
                _SelectedAdult = value;
                OnPropertyChanged();
            }
        }

        private ChildDto _SelectedChild;
        public ChildDto SelectedChild
        {
            get { return _SelectedChild; }
            set
            {
                _SelectedChild = value;
                OnPropertyChanged();
            }
        }

        private AdultDto _SelectedGuardian;
        public AdultDto SelectedGuardian
        {
            get { return _SelectedGuardian; }
            set
            {
                _SelectedGuardian = value;
                OnPropertyChanged();
            }
        }

        private LoanSlipDto _SelectedLoanSlip;
        public LoanSlipDto SelectedLoanSlip
        {
            get { return _SelectedLoanSlip; }
            set
            {
                _SelectedLoanSlip = value;
                OnPropertyChanged();
            }
        }


        private string _InputReason;
        public string InputReason
        {
            get { return _InputReason; }
            set
            {
                _InputReason = value;
                OnPropertyChanged();
            }
        }

        private int _BorrowedDays;
        public int BorrowedDays
        {
            get { return _BorrowedDays; }
            set
            {
                _BorrowedDays = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalFineAmount;
        public decimal TotalFineAmount
        {
            get { return _TotalFineAmount; }
            set
            {
                _TotalFineAmount = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        private LoanHistoryViewModel loanHistoryVM;
        private LoanDetailHistoryViewModel loanDetailHistoryVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private BookTitleViewModel bookTitleVM;
        private BookISBNViewModel bookISBNVM;
        private BookViewModel bookVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanDetailMap loanDetailMap;
        #endregion

        #region ResultProperty
        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }


        private LoanDetailHistoryDto _Detail;
        public LoanDetailHistoryDto Detail
        {
            get { return _Detail; }
            set
            {
                _Detail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LoanDetailHistory> LoanDetailHistorys { get; set; }
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

        public ucAddLoanHistory()
        {
            InitializeComponent();

            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();
            AllLoanOfReader = new ObservableCollection<LoanSlip>();
            AllUnPaidBookCard = new ObservableCollection<ucBookCard>();
            AllPaidBookCard = new ObservableCollection<ucBookCard>();
            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            #endregion

            #region SetTextBoxMaxLength

            #endregion
            
            #region LostFocus
            #endregion

            this.DataContext = this;
            this.Loaded += ucAddLoanHistory_Loaded;
        }

        private void Closing()
        {
            if (IsCancel)
                Item = null;

            getParentUc().BackToMainPage();
        }

        private void ucAddLoanHistory_Loaded(object sender, RoutedEventArgs e)
        {
            AllReason = getReasonRepo().Gets();

            ucInputReaderLoanHistory = MainWindow.UnitOfForm.UcInputReaderLoanHistory(true);
            ucInputReaderLoanHistory.getItem = () => Item;
            ucInputReaderLoanHistory.getParentUc = () => this;
            ucInputReaderLoanHistory.getStoreContent = () => storeContent;

            LoanDetailHistorys = new ObservableCollection<LoanDetailHistory>();
            PaidBooksOfReader = new ObservableCollection<BookDto>();

            this.storeContent.Push(this.Content);
            this.Content = ucInputReaderLoanHistory;
        }

        private void NewDetail()
        {
            int indexId = getLoanDetailHistoryRepo().Count + LoanDetailHistorys.Count;
            Detail = new LoanDetailHistoryDto(loanDetailHistoryVM.GetId(indexId));
            Detail.IdLoanHistory = Item.Id;
            Detail.ExpDate = Item.ExpDate;
        }
        
        public void CloseAndSave()
        {
            if (LoanDetailHistorys.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't return any book yet");
                return;
            }

            LoanSlip loanSlip = loanSlipVM.FindById(SelectedLoanSlip.Id);
            ObservableCollection<LoanDetail> loanDetails = loanDetailVM.FillListByIdLoan(loanSlip.Id);

            if (LoanDetailHistorys.Count < loanDetails.Count)
            {
                Utilities.ShowMessageBox1("Please return all the books of this loan slip");
                return;
            }
            
            #region CheckIsExistInformation

            #endregion

            // Truyền dữ liệu cho item
            var loanHistory = new LoanHistory();
            PassValueToItem(loanHistory);

            #region Remove-LoanSlip

            getLoanDetailRepo().RemoveRange(loanDetails);
            getLoanDetailRepo().WriteDeleteRange(loanDetails);

            getLoanSlipRepo().Remove(loanSlip);
            getLoanSlipRepo().WriteDelete(loanSlip);
            #endregion

            #region Add-LoanHistory
            getLoanHistoryRepo().Add(loanHistory);
            getLoanHistoryRepo().WriteAdd(loanHistory);

            getLoanDetailHistoryRepo().AddRange(LoanDetailHistorys);
            getLoanDetailHistoryRepo().WriteAddRange(LoanDetailHistorys);
            #endregion

            ChangeBookStatus(true);
            LoanDetailHistorys.Clear();

            IsCancel = false;
            Closing();
        }
        
        public void CloseNotSave()
        {
            Item = null;
            Closing();
        }

        private void PassValueToItem(LoanHistory item)
        {
            Utilities.Copy(item, Item);
        }

        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in LoanDetailHistorys)
            {
                Book book = bookVM.FindById(loanDetail.IdBook, null);

                if (book.Status != updateStatus)
                {
                    book.Status = updateStatus;

                    // Save change to database
                    getBookRepo().WriteUpdateStatus(book, updateStatus);
                }
            }
            foreach (BookISBN isbn in getBookISBNRepo())
            {
                var books = bookVM.FillByBookISBN(isbn.ISBN, true);
                if (books.Count == 0)
                {
                    isbn.Status = false;
                }
                else
                {
                    isbn.Status = true;
                }
                getBookISBNRepo().WriteUpdateStatus(isbn, isbn.Status);
            }
        }

    }
}
