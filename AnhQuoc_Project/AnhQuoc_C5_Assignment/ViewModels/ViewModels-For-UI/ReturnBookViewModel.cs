using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AnhQuoc_C5_Assignment
{
    public class ReturnBookViewModel : BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        private bool handle = true;

        private ucAddLoanHistory ucAddLoanHistory;
        private ucRetureBookInfo ucRetureBookInfo;
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

        #region Constants
        private const double CardWidth = 250;
        private const double CardMargin = 10;
        #endregion

        #region Properties
        public bool IsCancel { get; set; } = true;
        public bool? StatusValue { get; set; } = true;

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

        private PenaltyReasonPaid _SelectedReason;
        public PenaltyReasonPaid SelectedReason
        {
            get { return _SelectedReason; }
            set
            {
                _SelectedReason = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<LoanDetailHistoryDto> LoanDetailHistoryDtos { get; set; }
        public ObservableCollection<PenaltyReasonPaid> PenaltyReasonPaids { get; set; }
        public ObservableCollection<BookDto> BookPaids { get; set; }  


        #region Datas
        private ObservableCollection<ReaderDto> _AllReader;
        public ObservableCollection<ReaderDto> AllReader
        {
            get { return _AllReader; }
            set
            {
                _AllReader = value;
                OnPropertyChanged();
            }
        }
        

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

        private ObservableCollection<LoanSlip> _AllAdultLoan;
        public ObservableCollection<LoanSlip> AllAdultLoan
        {
            get { return _AllAdultLoan; }
            set
            {
                _AllAdultLoan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanSlip> _AllReaderLoan;
        public ObservableCollection<LoanSlip> AllReaderLoan
        {
            get { return _AllReaderLoan; }
            set
            {
                _AllReaderLoan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanDetail> _AllAdultLoanDetail;
        public ObservableCollection<LoanDetail> AllAdultLoanDetail
        {
            get { return _AllAdultLoanDetail; }
            set
            {
                _AllAdultLoanDetail = value;
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
        public ObservableCollection<LoanDetail> UnPaidLoanDetailsOfReader { get; set; }
        public ObservableCollection<LoanDetail> PaidLoanDetailsOfReader { get; set; }

        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public ObservableCollection<ucBookCard> AllUnPaidBookCard { get; set; }

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
        private PenaltyReasonViewModel reasonVM;
        #endregion

        #region Mapping
        private PenaltyReasonMap reasonMap;
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanDetailMap loanDetailMap;
        private LoanDetailHistoryMap loanDetailHistoryMap;
        #endregion
        
        #region RelayCommands
        public RelayCommand ucAddLoanHistoryLoadedCmd { get; private set; }
        public RelayCommand ReaderLoadedCmd { get; private set; }
        public RelayCommand BookLoadedCmd { get; private set; }
        public RelayCommand LoanConfirmLoadedCmd { get; private set; }


        public RelayCommand ReaderBtnConfirmClickCmd { get; private set; }
        public RelayCommand ReaderBtnCancelClickCmd { get; private set; }


        public RelayCommand ReadercbTxtIdReaderDropDownClosedCmd { get; private set; }
        public RelayCommand ReadercbTxtIdReaderSelectionChangedCmd { get; private set; }
        public RelayCommand ReadertxtIdReaderTextChangedCmd { get; private set; }
        
        
        public RelayCommand BookBtnConfirmClickCmd { get; private set; }
        public RelayCommand BookBtnCancelClickCmd { get; private set; }


        public RelayCommand BookcbTxtReasonDropDownClosedCmd { get; set; }
        public RelayCommand BookcbTxtReasonSelectionChangedCmd { get; set; }
        public RelayCommand BooktxtReasonTextChangedCmd { get; set; }

        #endregion

        public ReturnBookViewModel()
        {
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();
            AllLoanOfReader = new ObservableCollection<LoanSlip>();
            AllUnPaidBookCard = new ObservableCollection<ucBookCard>();


            #region Allocates
            reasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            reasonMap = UnitOfMap.Instance.PenaltyReasonMap;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            loanDetailHistoryMap = UnitOfMap.Instance.LoanDetailHistoryMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            #endregion
                        
            #region Init-RelayCmds
            ucAddLoanHistoryLoadedCmd = new RelayCommand(ucAddLoanHistoryLoaded);
            ReaderLoadedCmd = new RelayCommand(ReaderLoaded);
            BookLoadedCmd = new RelayCommand(BookLoaded);
            LoanConfirmLoadedCmd = new RelayCommand(LoanConfirmLoaded);



            ReaderBtnConfirmClickCmd = new RelayCommand(ReaderBtnConfirmClick);
            ReaderBtnCancelClickCmd = new RelayCommand(ReaderBtnCancelClick);

            ReadercbTxtIdReaderDropDownClosedCmd = new RelayCommand(null, ReadercbTxtIdReaderDropDownClosed);
            ReadercbTxtIdReaderSelectionChangedCmd = new RelayCommand(null, ReadercbTxtIdReaderSelectionChanged);
            ReadertxtIdReaderTextChangedCmd = new RelayCommand(null, ReadertxtIdReaderTextChanged);

            BookBtnConfirmClickCmd = new RelayCommand(BookBtnConfirmClick);
            BookBtnCancelClickCmd = new RelayCommand(BookBtnCancelClick);

            BookcbTxtReasonDropDownClosedCmd = new RelayCommand(null, BookcbTxtReasonDropDownClosed);
            BookcbTxtReasonSelectionChangedCmd = new RelayCommand(null, BookcbTxtReasonSelectionChanged);
            BooktxtReasonTextChangedCmd = new RelayCommand(null, BooktxtReasonTextChanged);
            #endregion


        }

        #region AddLoanHistory-Commands
        private void ucAddLoanHistoryLoaded(object para)
        {
            ucAddLoanHistory = para as ucAddLoanHistory;

            AllReader = readerMap.ConvertToDto(ucAddLoanHistory.getReaderRepo().Gets());
            AllReason = reasonMap.ConvertToDto(ucAddLoanHistory.getReasonRepo().Gets());

            ucInputReaderLoanHistory = MainWindow.UnitOfForm.UcInputReaderLoanHistory(true);
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectionChanged += DgDatas_SelectionChanged;
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.LoadingRow += DgDatas_LoadingRow;

            LoanDetailHistoryDtos = new ObservableCollection<LoanDetailHistoryDto>();
            PenaltyReasonPaids = new ObservableCollection<PenaltyReasonPaid>();
            BookPaids = new ObservableCollection<BookDto>();

            ucAddLoanHistory.Content = ucInputReaderLoanHistory;
        }
        #endregion

        #region Reader-Implement-Commands
        private void ReaderLoaded(object para)
        {
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllLoanOfReader;
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();

            ucInputReaderLoanHistory.btnConfirm.IsEnabled = false;
        }

        private void ReaderBtnConfirmClick(object para)
        {
            if (SelectedReader == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }
            SelectedReader = SelectedReader;

            if (ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("loan slip"));
                return;
            }

            NewItem();

            ucRetureBookInfo = MainWindow.UnitOfForm.UcRetureBookInfo(true);

            storeContent.Push(ucAddLoanHistory.Content);
            ucAddLoanHistory.Content = ucRetureBookInfo;
        }

        private void ReaderBtnCancelClick(object para)
        {
            CloseNotSave();
        }
        

        #region ReaderInput
        private bool HandleReader(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((ReaderDto)cmb.SelectedItem).Id;
            return true;
        }

        private void ReadercbTxtIdReaderDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleReader(ucInputReaderLoanHistory.txtIdReader, cmb);
            handle = true;
        }

        private void ReadercbTxtIdReaderSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleReader(ucInputReaderLoanHistory.txtIdReader, cmb);
        }

        private void ReadertxtIdReaderTextChanged(object para)
        {
            TxtIdReader_Filter_TextChanged(ucInputReaderLoanHistory.txtIdReader, ucInputReaderLoanHistory.gdInputIdReader, readerVM.CreateByDto(AllReader));
        }

        private void TxtIdReader_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<Reader> sourceDto)
        {
            SelectedLoanSlip = null;

            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
            {
                SelectedReader = null;
                return;
            }

            ObservableCollection<Reader> getfillList = readerVM.FillContainIds(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Id == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = getfillList.First();
                SelectedReader = readerMap.ConvertToDto(reader);

                GetReaderLoanAndLoanDetails();

                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllReaderLoan;
                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();

                return;
            }
            else
            {
                SelectedReader = null;

                AllReaderLoan = new ObservableCollection<LoanSlip>();

                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllReaderLoan;
                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();
            }
            comBoBox.ItemsSource = readerMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        private void GetReaderLoanAndLoanDetails()
        {
            if (SelectedReader.ReaderType == ReaderType.Adult)
            {
                AllAdultLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllAdultLoanDetail = loanDetailVM.FillListByIdLoans(AllAdultLoan);
                AllReaderLoan = AllAdultLoan;
            }
            else if (SelectedReader.ReaderType == ReaderType.Child)
            {
                AllChildLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
                AllReaderLoan = AllChildLoan;
            }
        }


        #endregion

        
        private void DgDatas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null)
            {
                ucInputReaderLoanHistory.btnConfirm.IsEnabled = false;
                return;
            }
            SelectedLoanSlip = ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem as LoanSlipDto;

            ucInputReaderLoanHistory.btnConfirm.IsEnabled = true;
        }

        private void NewItem()
        {
            string newId = loanHistoryVM.GetId();
            Item = new LoanHistoryDto(newId);
            IsCheckProperties isCheckProperties = new IsCheckProperties(CheckPropertyType.Except);
            Utilities.Copy(Item, SelectedLoanSlip, isCheckProperties);
            Item.Id = newId;
            Item.CreateAt = DateTime.Now;
        }

        private void GetLoanSlipFromReader()
        {
            ObservableCollection<LoanSlip> loans = loanSlipVM.FillByIdReader(SelectedReader.Id);
            AllLoanOfReader.AddRange(loans);

            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();
        }
        #endregion

        #region ReturnBookInfo-Commands
        private void BookLoaded(object para)
        {
            ucRetureBookInfo.cbTxtReason.ItemsSource = AllReason;

            BorrowedDays = (DateTime.Now - SelectedLoanSlip.LoanDate).Days;

            GetDetailsFromLoanSlip();
            GetBooksFromLoanSlip();

            ConvertToUnPaidBookCard(UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();


            // Allocate all detail
            for (int i = 0; i < AllUnPaidBookCard.Count; i++)
            {
                NewDetail();
                Detail.IdBook = UnPaidBooksOfReader[i].Id;
                LoanDetailHistoryDtos.Add(Detail);


                PenaltyReasonPaid reasonPaid = new PenaltyReasonPaid();
                PenaltyReasonPaids.Add(reasonPaid);
            }

            if (AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(AllUnPaidBookCard.FirstOrDefault(), null);
            }          
        }

        private void BookBtnConfirmClick(object para)
        {
            CalculatePayMent();

            frmDefault frmConfirmInformation = new frmDefault();
            ucLoanHistoryConfirm ucLoanHistoryConfirm = new ucLoanHistoryConfirm();

            Button btnConfirm = ucLoanHistoryConfirm.btnConfirm;
            Button btnCancel = ucLoanHistoryConfirm.btnCancel;

            btnConfirm.Click += (_sender, _e) =>
            {
                CloseAndSave();
                frmConfirmInformation.Close();
            };

            btnCancel.Click += (_sender, _e) =>
            {
                frmConfirmInformation.Close();
            };
            
            Utilities.AddItemToFormDefault(frmConfirmInformation, ucLoanHistoryConfirm);

            frmConfirmInformation.Width = 800;
            frmConfirmInformation.SizeToContent = SizeToContent.Height;

            frmConfirmInformation.ShowDialog();
        }

        private void BookBtnCancelClick(object para)
        {
            ucAddLoanHistory.Content = storeContent.Pop();
        }

        #region InputReason

        private bool HandleReason(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
            {
                ucRetureBookInfo.txtFineAmount.IsEnabled = false;
                return false;
            }
            txt.Text = ((PenaltyReasonDto)cmb.SelectedItem).Name;
            return true;
        }


        private void BookcbTxtReasonDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleReason(ucRetureBookInfo.txtReason, cmb);
            handle = true;
        }

        private void BookcbTxtReasonSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleReason(ucRetureBookInfo.txtReason, cmb);
        }

        private void BooktxtReasonTextChanged(object para)
        {
            TxtInputReason_Filter_TextChanged(ucRetureBookInfo.txtReason, ucRetureBookInfo.gdInputReason, AllReason);
        }

        private void TxtInputReason_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<PenaltyReasonDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            ObservableCollection<PenaltyReason> getfillList = null;

            getfillList = reasonVM.FillContainsName(sourceDto, txtInput.Text, ignoreCase, StatusValue);
            if (getfillList.Count == 1 && getfillList.First().Name == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                PenaltyReason reason = getfillList.First();

                // Debt code
                if (SelectedReason.Reason != null && SelectedReason.Reason.Id != reason.Id)
                {
                    SelectedReason.IsPaided = false;
                }

                if (SelectedReason.IsPaided == false)
                {
                    SelectedReason.Reason = reason;
                    decimal fineAmount = 0;
                    if (reason.Id == Constants.reason1)
                    {
                        fineAmount = 0;
                        SelectedUnPaidBookCard.getItem().IdBookStatus = Constants.bookStatusNormal;
                        SelectedUnPaidBookCard.Background = Brushes.White;
                        SelectedUnPaidBookCard.Foreground = Utilities.GetColorFromCode("#000000");
                    }
                    else if (reason.Id == Constants.reason2)
                    {
                        fineAmount = SelectedUnPaidBookCard.Item.PriceCurrent;
                        SelectedUnPaidBookCard.getItem().IdBookStatus = Constants.bookStatusLost;
                        SelectedUnPaidBookCard.Background = Utilities.GetColorFromCode("#da3445");
                        SelectedUnPaidBookCard.Foreground = Utilities.GetColorFromCode("#ffffff");
                    }
                    else if (reason.Id == Constants.reason3)
                    {
                        fineAmount = 0;
                        SelectedUnPaidBookCard.getItem().IdBookStatus = Constants.bookStatusSpoil;
                        SelectedUnPaidBookCard.Background = Utilities.GetColorFromCode("#f7c300");
                        SelectedUnPaidBookCard.Foreground = Utilities.GetColorFromCode("#000000");
                    }

                    if (BookPaids.FirstOrDefault(book => book.Id == SelectedUnPaidBookCard.getItem().Id) == null)
                    {
                        BookPaids.Add(SelectedUnPaidBookCard.getItem());
                    }
                    

                    Detail.PaidMoney = fineAmount;
                    SelectedReason.IsPaided = true;
                }

                if (SelectedReason.Reason != null)
                {
                    if (SelectedReason.Reason.Id == Constants.reason2)
                        ucRetureBookInfo.txtFineAmount.IsEnabled = false;
                    else
                        ucRetureBookInfo.txtFineAmount.IsEnabled = true;
                }
                return;
            }
            else
            {
                ucRetureBookInfo.txtFineAmount.IsEnabled = false;
            }
            comBoBox.ItemsSource = reasonMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion
        

        private void NewDetail()
        {
            int indexId = LoanDetailHistoryDtos.Count + loanDetailHistoryVM.getMaxIndexId(nameof(Detail.Id));
            Detail = new LoanDetailHistoryDto(loanDetailHistoryVM.GetId(indexId));
            Detail.IdLoanHistory = Item.Id;
            Detail.ExpDate = Item.ExpDate;
        }

        private void UnPaidBookCards_SelectionChanged()
        {
            ucRetureBookInfo.txtFineAmount.IsEnabled = false;

            int index = AllUnPaidBookCard.IndexOf(SelectedUnPaidBookCard);
            Detail = LoanDetailHistoryDtos[index];

            SelectedReason = PenaltyReasonPaids[index];

            if (SelectedReason.Reason == null)
                ucRetureBookInfo.txtReason.Text = string.Empty;
            else
            {
                ucRetureBookInfo.txtReason.Text = SelectedReason.Reason.Name;
            }
        }


        private void ChangeBookStatus(bool updateStatus)
        {
            var bookList = bookVM.GetBooksInLoanDetailHistorys(loanDetailHistoryVM.CreateByDto(LoanDetailHistoryDtos));
            foreach (var book in bookList)
            {
                if (book.IdBookStatus != Constants.bookStatusLost)
                {
                    book.Status = updateStatus;
                
                    // Save change to database
                    ucAddLoanHistory.getBookRepo().WriteUpdate(book);
                }
            }
            var isbnList = bookISBNVM.GetISBNsFromBooks(bookList);

            foreach (BookISBN isbn in isbnList)
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
                ucAddLoanHistory.getBookISBNRepo().WriteUpdate(isbn);
            }
        }

        private void CalculatePayMent()
        {
            foreach (var detail in LoanDetailHistoryDtos)
            {
                Item.FineMoney += detail.PaidMoney;
            }
            Item.Total = Item.FineMoney + Item.LoanPaid;
        }

        private void PassValueToItem(LoanHistory item)
        {
            IsCheckProperties isCheckProperties = new IsCheckProperties(CheckPropertyType.Except);
            Utilities.Copy(item, Item, isCheckProperties);
        }

        private void GetDetailsFromLoanSlip()
        {
            UnPaidLoanDetailsOfReader = loanDetailVM.FillListByIdLoan(SelectedLoanSlip.Id);
        }

        private void GetBooksFromLoanSlip()
        {
            UnPaidBooksOfReader = bookMap.ConvertToDto(bookVM.GetBooksInLoanDetails(UnPaidLoanDetailsOfReader));
        }


        private void ConvertToUnPaidBookCard(ObservableCollection<BookDto> books)
        {
            AllUnPaidBookCard.Clear();
            foreach (var book in books)
            {
                ucBookCard ucUnPaidBookCard = new ucBookCard();
                ucUnPaidBookCard.Background = Brushes.White;
                ucUnPaidBookCard.Width = CardWidth;
                ucUnPaidBookCard.Margin = new Thickness(CardMargin);
                ucUnPaidBookCard.Focusable = true;
                ucUnPaidBookCard.MouseLeftButtonDown += UcUnPaidBookCard_MouseLeftButtonDown;
                ucUnPaidBookCard.getItem = () => book;
                AllUnPaidBookCard.Add(ucUnPaidBookCard);
            }
        }

        private void UcUnPaidBookCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookCard card = sender as ucBookCard;
            if (SelectedUnPaidBookCard != null && SelectedUnPaidBookCard == card) return;


            // old card
            if (SelectedUnPaidBookCard != null)
            {
                SelectedUnPaidBookCard.borderEffectContainer.Visibility = Visibility.Hidden;
            }

            //  new card
            SelectedUnPaidBookCard = card;
            SelectedUnPaidBookCard.borderEffectContainer.Visibility = Visibility.Visible;

            UnPaidBookCards_SelectionChanged();


        }

        private void AddUnPaidBookCardToWrap()
        {
            ucRetureBookInfo.wrapUnPaidBooksTable.Children.Clear();
            foreach (var ucCard in AllUnPaidBookCard)
            {
                ucRetureBookInfo.wrapUnPaidBooksTable.Children.Add(ucCard);
            }
        }


        #endregion

        #region LoanConfirm-Commands
        private void LoanConfirmLoaded(object para)
        {
        }
        #endregion


        private void DgDatas_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            LoanSlipDto loan = e.Row.Item as LoanSlipDto;

            if (loanSlipVM.IsOutOfExpireDate(loanSlipVM.CreateByDto(loan)))
            {
                e.Row.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }

        private void Closing()
        {
            if (IsCancel)
                Item = null;

            ucAddLoanHistory.getParentUc().BackToMainPage();
        }

        public void CloseAndSave()
        {
            if (LoanDetailHistoryDtos.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't return any book yet");
                return;
            }

            LoanSlip loanSlip = loanSlipVM.FindById(SelectedLoanSlip.Id);
            ObservableCollection<LoanDetail> loanDetails = loanDetailVM.FillListByIdLoan(loanSlip.Id);

            if (LoanDetailHistoryDtos.Count < loanDetails.Count)
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

            ucAddLoanHistory.getLoanDetailRepo().RemoveRange(loanDetails);
            ucAddLoanHistory.getLoanDetailRepo().WriteDeleteRange(loanDetails);

            ucAddLoanHistory.getLoanSlipRepo().Remove(loanSlip);
            ucAddLoanHistory.getLoanSlipRepo().WriteDelete(loanSlip);
            #endregion

            #region Add-LoanHistory
            ucAddLoanHistory.getLoanHistoryRepo().Add(loanHistory);
            ucAddLoanHistory.getLoanHistoryRepo().WriteAdd(loanHistory);

            var loanDetailHistorys = loanDetailHistoryVM.CreateByDto(LoanDetailHistoryDtos);
            ucAddLoanHistory.getLoanDetailHistoryRepo().AddRange(loanDetailHistorys);
            ucAddLoanHistory.getLoanDetailHistoryRepo().WriteAddRange(loanDetailHistorys);
            #endregion

            // Cập nhật tình trạng sách (book status)
            BookPaids.ForEach(bookDto =>
            {
                Book book = bookVM.FindById(bookDto.Id);

                book.IdBookStatus = bookDto.IdBookStatus;
                bookVM.Repo.WriteUpdate(book);
            });

            // Cập nhật tình trạng sách (status)
            ChangeBookStatus(true);

            LoanDetailHistoryDtos.Clear();
            IsCancel = false;
            Closing();
        }

        public void CloseNotSave()
        {
            Item = null;
            Closing();
        }
    }
}
