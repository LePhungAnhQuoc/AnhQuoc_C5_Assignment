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
using System.Windows.Threading;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucRetureBookInfo.xaml
    /// </summary>
    public partial class ucRetureBookInfo : UserControl, INotifyPropertyChanged
    {
        #region getDatas
        public Func<Stack<object>> getStoreContent { get; set; }
        public Func<ucAddLoanHistory> getParentUc { get; set; }
        public Func<LoanHistoryDto> getItem { get; set; }


        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanDetailHistoryRepository> getLoanDetailHistoryRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Constants
        private const double CardWidth = 200;
        private const double CardMargin = 10;

        #endregion

        #region Fields
        private bool flag_RegisterEvent = false;
        private bool handle = true;

        private ucAddLoanHistory ucAddLoanHistory;

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
        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }

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

        private ucBookCard _SelectedUnPaidBookCard;
        public ucBookCard SelectedUnPaidBookCard
        {
            get { return _SelectedUnPaidBookCard; }
            set
            {
                _SelectedUnPaidBookCard = value;
                OnPropertyChanged();
            }
        }


        private ucBookCard _SelectedPaidBookCard;
        public ucBookCard SelectedPaidBookCard
        {
            get { return _SelectedPaidBookCard; }
            set
            {
                _SelectedPaidBookCard = value;
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
        private PenaltyReasonViewModel reasonVM;
        private ChildViewModel childVM;
        private BookTitleViewModel bookTitleVM;
        private BookISBNViewModel bookISBNVM;
        private BookViewModel bookVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private PenaltyReasonMap reasonMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanDetailMap loanDetailMap;
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

        public ucRetureBookInfo()
        {
            InitializeComponent();

            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;
            reasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;

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
            reasonMap = UnitOfMap.Instance.PenaltyReasonMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            #endregion

            #region SetTextBoxMaxLength

            #endregion

            #region Events
           
            btnReturnBook.Click += BtnReturnBook_Click;
            btnFinish.Click += BtnFinish_Click;
            #endregion

            #region LostFocus
            #endregion

            this.DataContext = this;
            this.Loaded += ucRetureBookInfo_Loaded;
        }

        private void ucRetureBookInfo_Loaded(object sender, RoutedEventArgs e)
        {
            ucAddLoanHistory = getParentUc();

            SelectedReader = ucAddLoanHistory.SelectedReader;
            SelectedLoanSlip = ucAddLoanHistory.SelectedLoanSlip;

           
            ucAddLoanHistory.BorrowedDays = (DateTime.Now - SelectedLoanSlip.LoanDate).Days;
            BorrowedDays = ucAddLoanHistory.BorrowedDays;

            NewItem();

            GetDetailsFromLoanSlip();
            GetBooksFromLoanSlip();

            ConvertToUnPaidBookCard(ucAddLoanHistory.UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();
            ConvertToPaidBookCard(ucAddLoanHistory.PaidBooksOfReader);
            AddPaidBookCardToWrap();

            if (ucAddLoanHistory.AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(ucAddLoanHistory.AllUnPaidBookCard.FirstOrDefault(), null);
            }
            if (ucAddLoanHistory.AllPaidBookCard.Count > 0)
            {
                UcPaidBookCard_MouseLeftButtonDown(ucAddLoanHistory.AllPaidBookCard.FirstOrDefault(), null);
            }
        }

        private void NewItem()
        {
            Item = getItem();
        }

        private void NewDetail()
        {
            int indexId = getLoanDetailHistoryRepo().Count + ucAddLoanHistory.LoanDetailHistorys.Count;
            ucAddLoanHistory.Detail = new LoanDetailHistoryDto(loanDetailHistoryVM.GetId(indexId));
            ucAddLoanHistory.Detail.IdLoanHistory = Item.Id;
            ucAddLoanHistory.Detail.ExpDate = Item.ExpDate;

            Detail = ucAddLoanHistory.Detail;
        }


        private void UnPaidBookCards_SelectionChanged()
        {
            NewDetail();
            txtReason.Text = string.Empty;
        }
        
        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in ucAddLoanHistory.LoanDetailHistorys)
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

        private void CalculatePayMent()
        {
            foreach (var detail in ucAddLoanHistory.LoanDetailHistorys)
            {
                Item.FineMoney += detail.PaidMoney;
            }
            Item.Total = Item.FineMoney + Item.LoanPaid;
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            //CalculatePayMent();

            frmDefault frmConfirmInformation = new frmDefault();
            
            ucLoanHistoryConfirm ucLoanHistoryConfirm = new ucLoanHistoryConfirm();
            ucLoanHistoryConfirm.getParentUc = getParentUc;
            ucLoanHistoryConfirm.getItem = () => Item;

            ucLoanHistoryConfirm.Margin = new Thickness(10);

            if (!flag_RegisterEvent)
            {
                flag_RegisterEvent = true;

                Button btnConfirm = ucLoanHistoryConfirm.btnConfirm;
                Button btnCancel = ucLoanHistoryConfirm.btnCancel;

                btnConfirm.Click += (_sender, _e) =>
                {
                    getParentUc().CloseAndSave();
                    frmConfirmInformation.Close();
                };

                btnCancel.Click += (_sender, _e) =>
                {
                    frmConfirmInformation.Close();
                };
            }
            Utilities.AddItemToFormDefault(frmConfirmInformation, ucLoanHistoryConfirm);

            frmConfirmInformation.Width = 800;
            frmConfirmInformation.SizeToContent = SizeToContent.Height;
            
            frmConfirmInformation.ShowDialog();
        }

        private void BtnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUnPaidBookCard == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                return;
            }

            BookDto getBook = bookVM.FindById(ucAddLoanHistory.UnPaidBooksOfReader, SelectedUnPaidBookCard.Item.Id, null);
            ucAddLoanHistory.Detail.IdBook = getBook.Id;

            bool isCheckEmptyItem = loanDetailHistoryVM.IsCheckEmptyItem(ucAddLoanHistory.Detail, true, nameof(ucAddLoanHistory.Detail.Note), nameof(ucAddLoanHistory.Detail.PaidMoney));

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            ucAddLoanHistory.UnPaidBooksOfReader.Remove(getBook);
            ucAddLoanHistory.PaidBooksOfReader.Add(getBook);

            ConvertToUnPaidBookCard(ucAddLoanHistory.UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();
            ConvertToPaidBookCard(ucAddLoanHistory.PaidBooksOfReader);
            AddPaidBookCardToWrap();

            // Add to list details
            var getDetail = loanDetailHistoryVM.CreateByDto(ucAddLoanHistory.Detail);
            ucAddLoanHistory.LoanDetailHistorys.Add(getDetail);

            var detail = Detail.PaidMoney;
            Item.FineMoney += ucAddLoanHistory.Detail.PaidMoney;
            Item.Total = Item.LoanPaid + Item.FineMoney;

            // Change button status Visibility
            if (ucAddLoanHistory.UnPaidBooksOfReader.Count == 0)
            {
                btnFinish.Visibility = Visibility.Visible;
                btnReturnBook.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnFinish.Visibility = Visibility.Collapsed;
                btnReturnBook.Visibility = Visibility.Visible;
            }

            if (ucAddLoanHistory.AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(ucAddLoanHistory.AllUnPaidBookCard.FirstOrDefault(), null);
            }
            if (ucAddLoanHistory.AllPaidBookCard.Count > 0)
            {
                UcPaidBookCard_MouseLeftButtonDown(ucAddLoanHistory.AllPaidBookCard.FirstOrDefault(), null);
            }
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

        private void GetDetailsFromLoanSlip()
        {
            ucAddLoanHistory.UnPaidLoanDetailsOfReader = loanDetailVM.FillListByIdLoan(ucAddLoanHistory.SelectedLoanSlip.Id);
        }

        private void GetBooksFromLoanSlip()
        {
            ucAddLoanHistory.UnPaidBooksOfReader = bookMap.ConvertToDto(bookVM.GetBooksInLoanDetails(ucAddLoanHistory.UnPaidLoanDetailsOfReader));
        }

        #region InputReason

        private bool HandleReason(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((PenaltyReasonDto)cmb.SelectedItem).Name;
            return true;
        }


        private void CbAdultTxtIdentify_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleReason(txtReason, cmb);
            handle = true;
        }

        private void CbAdultTxtIdentify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleReason(txtReason, cmb);
        }

        private void TxtInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputIdentify_Filter_TextChanged(txtReason, gdInputReason, ucAddLoanHistory.AllReason);
        }

        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<PenaltyReasonDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<PenaltyReason> getfillList = reasonVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, ucAddLoanHistory.StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Name == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                // Xử lý nghiệp vụ
                return;
            }
            else
            {
            }
            comBoBox.ItemsSource = reasonMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion


        private void ConvertToUnPaidBookCard(ObservableCollection<BookDto> books)
        {
            ucAddLoanHistory.AllUnPaidBookCard.Clear();
            foreach (var book in books)
            { 
                ucBookCard ucUnPaidBookCard = new ucBookCard();
                ucUnPaidBookCard.Width = CardWidth;
                ucUnPaidBookCard.Margin = new Thickness(CardMargin);
                ucUnPaidBookCard.Focusable = true;
                ucUnPaidBookCard.MouseLeftButtonDown += UcUnPaidBookCard_MouseLeftButtonDown;
                ucUnPaidBookCard.getItem = () => book;
                ucAddLoanHistory.AllUnPaidBookCard.Add(ucUnPaidBookCard);
            }
        }

        private void UcUnPaidBookCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookCard card = sender as ucBookCard;
            if (SelectedUnPaidBookCard != null && SelectedUnPaidBookCard == card) return;
            
            SelectedUnPaidBookCard = card;
            UnPaidBookCards_SelectionChanged();
        }

        private void AddUnPaidBookCardToWrap()
        {
            wrapUnPaidBooksTable.Children.Clear();
            foreach (var ucCard in ucAddLoanHistory.AllUnPaidBookCard)
            {
                wrapUnPaidBooksTable.Children.Add(ucCard);
            }
        }

        private void ConvertToPaidBookCard(ObservableCollection<BookDto> books)
        {
            ucAddLoanHistory.AllPaidBookCard.Clear();
            foreach (var book in books)
            {
                ucBookCard ucPaidBookCard = new ucBookCard();
                ucPaidBookCard.Width = CardWidth;
                ucPaidBookCard.Margin = new Thickness(CardMargin);
                ucPaidBookCard.Focusable = true;
                ucPaidBookCard.MouseLeftButtonDown += UcPaidBookCard_MouseLeftButtonDown;
                ucPaidBookCard.getItem = () => book;
                ucAddLoanHistory.AllPaidBookCard.Add(ucPaidBookCard);
            }
        }

        private void UcPaidBookCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookCard card = sender as ucBookCard;
            if (SelectedPaidBookCard != null && SelectedPaidBookCard == card) return;

            SelectedPaidBookCard = card;
        }

        private void AddPaidBookCardToWrap()
        {
            wrapPaidBooksTable.Children.Clear();
            foreach (var ucCard in ucAddLoanHistory.AllPaidBookCard)
            {
                wrapPaidBooksTable.Children.Add(ucCard);
            }
        }

    }
}
