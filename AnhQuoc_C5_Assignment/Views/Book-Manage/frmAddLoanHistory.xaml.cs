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
    /// Interaction logic for frmAddLoanHistory.xaml
    /// </summary>
    public partial class frmAddLoanHistory : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanDetailHistoryRepository> getLoanDetailHistoryRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private bool handle = true;
        #endregion

        #region Properties
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
        #endregion

        public ObservableCollection<BookDto> UnPaidBooksOfReader { get; set; }
        public ObservableCollection<BookDto> PaidBooksOfReader { get; set; }
        public ObservableCollection<LoanDetail> UnPaidLoanDetailsOfReader { get; set; }
        public ObservableCollection<LoanDetail> PaidLoanDetailsOfReader { get; set; }


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

        public frmAddLoanHistory()
        {
            InitializeComponent();
            

            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();

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


            #region SetTextBoxMaxLength
       
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnReturnBook.Click += BtnReturnBook_Click;

            #region LostFocus
            #endregion

            this.DataContext = this;
            this.Loaded += frmAddLoanHistory_Loaded;
        }

        private void frmAddLoanHistory_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFormSelectLoanSlip();

            LoanDetailHistorys = new ObservableCollection<LoanDetailHistory>();
            PaidBooksOfReader = new ObservableCollection<BookDto>();
            GetBooksFromReader();
            GetDetailsFromLoanSlip();

            ucUnPaidBooksTable.Books = UnPaidBooksOfReader;
            ucPaidBooksTable.Books = PaidBooksOfReader;

            List<PropertyInfo> allProperties = typeof(BookDto).GetProperties().ToList();
            List<PropertyInfo> exceptDtgProperties = Utilities.FillPropertiesByName(allProperties, Constants.exceptDtgCreateLoanHistoryBook);
            ucUnPaidBooksTable.getExceptProperties = () => exceptDtgProperties;
            ucPaidBooksTable.getExceptProperties = () => exceptDtgProperties;

            BorrowedDays = (DateTime.Now - SelectedLoanSlip.LoanDate).Days;

            NewItem();
            NewDetail();
        }

        private void NewItem()
        {
            string newId = loanHistoryVM.GetId();
            Item = new LoanHistoryDto(newId);
            Utilities.Copy(Item, SelectedLoanSlip);
            Item.Id = newId;
            Item.IdReader = SelectedLoanSlip.Reader.Id;
            Item.CreateAt = DateTime.Now;
        }

        private void NewDetail()
        {
            string newId = loanDetailHistoryVM.GetId();
            Detail = new LoanDetailHistoryDto(newId);
            Detail.IdLoanHistory = Item.Id;
            Detail.ExpDate = Item.ExpDate;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (LoanDetailHistorys.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't return any book yet");
                return;
            }

            bool isCheckEmptyItem = loanHistoryVM.IsCheckEmptyItem(Item);

            // FormatValues
            FormatValues();

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            #region CheckIsExistInformation

            #endregion

            // Truyền dữ liệu cho item
            var loanHistory = new LoanHistory();
            PassValueToItem(loanHistory);

            getLoanHistoryRepo().Add(loanHistory);
            getLoanHistoryRepo().WriteAdd(loanHistory);

            
            getLoanDetailHistoryRepo().WriteAddRange(LoanDetailHistorys);

            ChangeBookStatus(true);
            LoanDetailHistorys.Clear();
            this.Close();
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }

        private void BtnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (ucUnPaidBooksTable.SelectedDto == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                return;
            }

            BookDto getBook = ucUnPaidBooksTable.SelectedDto;
            Detail.IdBook = getBook.Id;

            bool isCheckEmptyItem = loanDetailHistoryVM.IsCheckEmptyItem(Detail, true, nameof(Detail.Note), nameof(Detail.PaidMoney));

            // FormatValues
            FormatValues();

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            #region CheckIsExistInformation
            #endregion

            // Pass value to Paid table
            
            UnPaidBooksOfReader.Remove(getBook);
            PaidBooksOfReader.Add(getBook);

            // Add to list details
            var getDetail = loanDetailHistoryVM.CreateByDto(Detail);
            LoanDetailHistorys.Add(getDetail);

            Item.FineMoney += Detail.PaidMoney;
            Item.Total = Item.LoanPaid + Item.FineMoney;

            getLoanDetailHistoryRepo().Add(getDetail);
            NewDetail();
        }

        private void PassValueToItem(LoanHistory item)
        {
            Utilities.Copy(item, Item);
        }

        public bool IsValidationGetHasError()
        {
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;
        }

        private void FormatValues()
        {
        }
        
        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }

        // Others methods

        #region Filter
        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                Reader reader = readerVM.FindById(getfillList.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);

                GetBooksFromReader();
                GetDetailsFromLoanSlip();
                return;
            }
            else
            {
                SelectedReader = null;
            }

            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
        }

        private void TxtInputGuardianIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                gdInputChildName.IsEnabled = true;
                return;
            }
            else
            {
                gdInputChildName.IsEnabled = false;
            }

            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
        }

        private void TxtInputChildReaderName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<ChildDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Child> getfillList = childVM.FillContainsFullName(source, txtInput.Text, ignoreCase, StatusValue);
            ObservableCollection<ChildDto> getfillListDto = childMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().FullName == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);

                GetBooksFromReader();
                GetDetailsFromLoanSlip();
                return;
            }
            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        private void TxtInputBookName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<BookTitleDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<BookTitle> getfillList = bookTitleVM.FillContainsName(source, txtInput.Text, ignoreCase);
            ObservableCollection<BookTitleDto> getfillListDto = bookTitleMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().Name == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                return;
            }
            else
            {
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        private bool HandleAdult(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((AdultDto)cmb.SelectedItem).Identify;
            return true;
        }
        #endregion

        #region AdultInput
        private void CbAdultTxtIdentify_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleAdult(txtAdultInputIdentify, cmb);
            handle = true;
        }

        private void CbAdultTxtIdentify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(txtAdultInputIdentify, cmb);
        }

        private void TxtInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputIdentify_Filter_TextChanged(txtAdultInputIdentify, gdInputAdultIndentify, AllAdults);
        }
        #endregion

        #region GuardianInput
        private void CbGuardianTxtIdentify_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleAdult(txtGuardianInputIdentify, cmb);
            handle = true;
        }

        private void CbGuardianTxtIdentify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(txtGuardianInputIdentify, cbGuardianTxtIdentify);

            if (cbGuardianTxtIdentify.SelectedItem == null)
                return;
            AdultDto adultDto = cbGuardianTxtIdentify.SelectedItem as AdultDto;
            AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            txtChildInputReaderName.Text = string.Empty;
            cbChildTxtReaderName.ItemsSource = AllChilds;
        }

        private void TxtGuardianInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(txtGuardianInputIdentify, gdGuardianInput, AllGuardians);
        }
        #endregion

        private void GetBooksFromReader()
        {
            ReaderDto reader = SelectedReader;

            var readerBooks = bookVM.GetBooksInLoanSlip(loanSlipVM.FindById(SelectedLoanSlip.Id));
            UnPaidBooksOfReader = bookMap.ConvertToDto(readerBooks);
        }

        private void GetDetailsFromLoanSlip()
        {
            UnPaidLoanDetailsOfReader = loanDetailVM.FillListByIdLoan(SelectedLoanSlip.Id);
        }

        private void CreateFormSelectLoanSlip()
        {
            Window frmSelectLoanSlip = Utilities.CreateDefaultForm();

            var ucLoanSlipsTable = new ucLoanSlipsTable();
            ucLoanSlipsTable.AllowPagination = false;
            ucLoanSlipsTable.getLoanSlips = () => new ObservableCollection<LoanSlip>(getLoanSlipRepo().Gets());
            ucLoanSlipsTable.ModifiedPagination();
            
            ucLoanSlipsTable.Margin = new Thickness(20);
            frmSelectLoanSlip.Content = ucLoanSlipsTable;

            ucLoanSlipsTable.dgDatas.MouseDoubleClick += (_sender, _e) =>
            {
                SelectedLoanSlip = ucLoanSlipsTable.SelectedDto;
                SelectedReader = readerMap.ConvertToDto(SelectedLoanSlip.Reader);

                if (SelectedReader.ReaderType == ReaderType.Adult)
                {
                    SelectedAdult = adultMap.ConvertToDto(adultVM.FindByIdReader(SelectedReader.Id, null));

                    stkAdultInformation.Visibility = Visibility.Visible;
                    stkChildInformation.Visibility = Visibility.Collapsed;
                }


                else if (SelectedReader.ReaderType == ReaderType.Child)
                {
                    SelectedChild = childMap.ConvertToDto(childVM.FindByIdReader(SelectedReader.Id, null));
                    SelectedGuardian = adultMap.ConvertToDto(SelectedChild.Adult);

                    stkAdultInformation.Visibility = Visibility.Collapsed;
                    stkChildInformation.Visibility = Visibility.Visible;
                }
                frmSelectLoanSlip.Close();
            };
            frmSelectLoanSlip.ShowDialog();
        }
    }
}
