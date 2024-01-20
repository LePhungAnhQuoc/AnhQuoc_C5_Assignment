using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnhQuoc_C5_Assignment
{
    public class BorrowBookViewModel : BaseViewModel<object>, IPageViewModel
    {
        #region getDatas

        #region getParent-UI-Controls
        public Func<ucLoanSlipManagement> getParentUc { get; set; }
        public Func<ucSelectReaderInfo> getucSelectReaderInfo { get; set; }
        public Func<ucInputBookInfo> getucInputBookInfo { get; set; }
        #endregion

        #region Pass-UI-Controls
        public Func<ComboBox> getcbAdultTxtIdentify { get; set; }
        public Func<StackPanel> getstkAdultInformation { get; set; }
        public Func<StackPanel> getstkChildInformation { get; set; }
        public Func<ComboBox> getcbGuardianTxtIdentify { get; set; }
        public Func<ComboBox> getcbSelectReaderType { get; set; }
        public Func<TextBox> gettxtAdultInputIdentify { get; set; }
        public Func<Grid> getgdInputAdultIndentify { get; set; }
        public Func<ucLoanDetailsTable> getucLoanDetailsBorrowedTable { get; set; }
        public Func<TextBox> gettxtGuardianInputIdentify { get; set; }
        public Func<TextBox> gettxtChildInputReaderName { get; set; }
        public Func<ComboBox> getcbChildTxtReaderName { get; set; }
        public Func<WrapPanel> getwrapLoanDetail { get; set; }
        public Func<WrapPanel> getwrapBookISBN { get; set; }
        public Func<TextBox> gettxtInputBookName { get; set; }
        public Func<Grid> getgdInputBookName { get; set; }
        public Func<ComboBox> getcbTxtBookName { get; set; }
        public Func<Grid> getgdInputChildName { get; set; }
        public Func<Grid> getgdGuardianInput { get; set; }
        
        #endregion

        #endregion

        #region Forms
        private ucSelectReaderInfo ucSelectReaderInfo;
        private ucInputBookInfo ucInputBookInfo;
        #endregion

        #region Constants
        private const double CardWidth = 220;
        private const double CardMargin = 10;

        #endregion

        #region Fields
        private ucAddLoan ucAddLoan;
        private bool handle;

        private Stack<object> _storeContent;
        public Stack<object> storeContent
        {
            get
            {
                if (_storeContent == null)
                    _storeContent = new Stack<object>();
                return _storeContent;
            }
            set { _storeContent = value; }
        }


        #endregion

        #region Properties
        public bool IsCancel { get; set; } = true;

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

        private ObservableCollection<Book> _BooksOfReader;
        public ObservableCollection<Book> BooksOfReader
        {
            get { return _BooksOfReader; }
            set
            {
                _BooksOfReader = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _TotalBooksOfReader;
        public ObservableCollection<Book> TotalBooksOfReader
        {
            get
            {
                if (BooksOfReader != null && AllChildBook != null)
                {
                    return BooksOfReader.Concat(AllChildBook).ToObservableCollection();
                }
                return null;
            }
        }


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

        public ObservableCollection<BookISBN> AllBookISBN { get; set; }
        public ObservableCollection<ucBookISBNCard> AllBookISBNCard { get; set; }
        public ObservableCollection<ucLoanDetailCard> AllLoanDetailCard { get; set; }


        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public bool? BookISBNStatusValue { get; set; } = null;
        public bool? BookStatusValue { get; set; } = null;
        public bool? StatusValue { get; set; } = true;
        public int TotalQuantity { get; set; }


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

        private BookDto _SelectedBook;
        public BookDto SelectedBook
        {
            get { return _SelectedBook; }
            set
            {
                _SelectedBook = value;
                OnPropertyChanged();
            }
        }

        private BookISBNDto _SelectedISBN;
        public BookISBNDto SelectedISBN
        {
            get { return _SelectedISBN; }
            set
            {
                _SelectedISBN = value;
                OnPropertyChanged();
            }
        }

        private ucLoanDetailCard _SelectedDetailCard;
        public ucLoanDetailCard SelectedDetailCard
        {
            get { return _SelectedDetailCard; }
            set
            {
                _SelectedDetailCard = value;
                OnPropertyChanged();
            }
        }


        #endregion


        #region ViewModels
        private ReaderViewModel readerVM;
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
        private LoanSlipDto _LoanSlipDto;
        public LoanSlipDto LoanSlipDto
        {
            get { return _LoanSlipDto; }
            set
            {
                _LoanSlipDto = value;
                OnPropertyChanged();
            }
        }

        private LoanSlip _LoanSlip;
        public LoanSlip LoanSlip
        {
            get { return _LoanSlip; }
            set
            {
                _LoanSlip = value;
                OnPropertyChanged();
            }
        }


        private LoanDetail _LoanDetail;
        public LoanDetail LoanDetail
        {
            get { return _LoanDetail; }
            set
            {
                _LoanDetail = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<LoanDetail> LoanDetails { get; set; }
        #endregion


        public string Name { get; }



        #region RelayCommands
        public RelayCommand ucAddLoanLoadedCmd { get; private set; }
        public RelayCommand ReaderLoadedCmd { get; private set; }
        public RelayCommand BookInfoLoadedCmd { get; private set; }

        public RelayCommand ReaderBtnConfirmClickCmd { get; private set; }
        public RelayCommand ReaderBtnCancelClickCmd { get; private set; }
        public RelayCommand BookInfoBtnConfirmClickCmd { get; private set; }
        public RelayCommand BookInfoBtnCancelClickCmd { get; private set; }

        public RelayCommand ReadercbSelectReaderTypeSelectionChangedCmd { get; private set; }

        public RelayCommand ReadercbAdultTxtIdentifyDropDownClosedCmd { get; private set; }
        public RelayCommand ReadercbAdultTxtIdentifySelectionChangedCmd { get; private set; }
        public RelayCommand ReadertxtAdultInputIdentifyTextChangedCmd { get; private set; }

        public RelayCommand ReadercbGuardianTxtIdentifyDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbGuardianTxtIdentifySelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtGuardianInputIdentifyTextChangedCmd { get; set; }

        public RelayCommand ReadercbChildTxtReaderNameDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbChildTxtReaderNameSelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtChildInputReaderNameTextChangedCmd { get; set; }



        // Input book Info
        public RelayCommand ReadercbTxtBookNameDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbTxtBookNameSelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtInputBookNameTextChangedCmd { get; set; }
        #endregion
        
        public BorrowBookViewModel()
        {
            Name = "Search Page";

            AllBookISBNCard = new ObservableCollection<ucBookISBNCard>();
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();

            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            #endregion

            #region SetTextBoxMaxLength
            #endregion
            
            #region Init-commands
            ucAddLoanLoadedCmd = new RelayCommand(null, ucAddLoanLoaded);
            ReaderLoadedCmd = new RelayCommand(null, ReaderLoaded);
            BookInfoLoadedCmd = new RelayCommand(null, BookInfoLoaded);

            ReaderBtnConfirmClickCmd = new RelayCommand(null, ReaderBtnConfirmClick);
            ReaderBtnCancelClickCmd = new RelayCommand(null, ReaderBtnCancelClick);
            ReadercbSelectReaderTypeSelectionChangedCmd = new RelayCommand(null, ReadercbSelectReaderTypeSelectionChanged);



            ReadercbAdultTxtIdentifyDropDownClosedCmd = new RelayCommand(null, ReadercbAdultTxtIdentifyDropDownClosed);
            ReadercbAdultTxtIdentifySelectionChangedCmd = new RelayCommand(null, ReadercbAdultTxtIdentifySelectionChanged);
            ReadertxtAdultInputIdentifyTextChangedCmd = new RelayCommand(null, ReadertxtAdultInputIdentifyTextChanged);


            ReadercbGuardianTxtIdentifyDropDownClosedCmd = new RelayCommand(null, ReadercbGuardianTxtIdentifyDropDownClosed);
            ReadercbGuardianTxtIdentifySelectionChangedCmd = new RelayCommand(null, ReadercbGuardianTxtIdentifySelectionChanged);
            ReadertxtGuardianInputIdentifyTextChangedCmd = new RelayCommand(null, ReadertxtGuardianInputIdentifyTextChanged);


            ReadercbChildTxtReaderNameDropDownClosedCmd = new RelayCommand(null, ReadercbChildTxtReaderNameDropDownClosed);
            ReadercbChildTxtReaderNameSelectionChangedCmd = new RelayCommand(null, ReadercbChildTxtReaderNameSelectionChanged);
            ReadertxtChildInputReaderNameTextChangedCmd = new RelayCommand(null, ReadertxtChildInputReaderNameTextChanged);


            BookInfoBtnConfirmClickCmd = new RelayCommand(null, BookInfoBtnConfirmClick);
            BookInfoBtnCancelClickCmd = new RelayCommand(null, BookInfoBtnCancelClick);

            ReadercbTxtBookNameDropDownClosedCmd = new RelayCommand(null, ReadercbTxtBookNameDropDownClosed);
            ReadercbTxtBookNameSelectionChangedCmd = new RelayCommand(null, ReadercbTxtBookNameSelectionChanged);
            ReadertxtInputBookNameTextChangedCmd = new RelayCommand(null, ReadertxtInputBookNameTextChanged);
            #endregion

        }
        
        private void NewItem()
        {
            LoanSlipDto = new LoanSlipDto(loanSlipVM.GetId());
            LoanSlipDto.IdUser = MainWindow.loginUser.Id;
            LoanSlipDto.LoanDate = DateTime.Now;
            LoanSlipDto.ExpDate = LoanSlipDto.LoanDate.AddDays(Constants.LoanSlipExpDate);
        }


        #region AddLoan-Inplement-Commands
        public void ucAddLoanLoaded(object para)
        {
            // Load Form in ucAddLoan
            ucAddLoan = para as ucAddLoan;
            
            this.getParentUc = ucAddLoan.getParentUc;
            
            ucSelectReaderInfo = MainWindow.UnitOfForm.UcSelectReaderInfo(true);
            storeContent.Push(ucAddLoan.Content);
            ucAddLoan.Content = ucSelectReaderInfo;

            NewItem();
        }


        private Book FindTheLastestBook(ObservableCollection<Book> books)
        {
            bool? bookStatus = null;
            string isbn = SelectedISBN.ISBN;
            if (ucAddLoan.getLoanSlipRepo().Gets().Count == 0)
                return null;


            var temp = ucAddLoan.getLoanSlipRepo().Gets().Where(item =>
            {
                var details = loanDetailVM.FillListByIdLoan(item.Id);

                details = loanDetailVM.FillListByISBN(details, isbn, bookStatus);
                if (details.Count > 0)
                {
                    return true;
                }
                return false;
            });
            temp = temp.OrderBy(item => item.ExpDate);
            LoanSlip loanLastest = temp.First();

            var loanDetails = loanDetailVM.FillListByIdLoan(loanLastest.Id);
            loanDetails = loanDetailVM.FillListByISBN(loanDetails, isbn, bookStatus);

            return bookVM.FindById(loanDetails.First().IdBook, bookStatus);
        }

        public void CloseAndSave()
        {
            CalculatePayment();

            // Truyền dữ liệu cho item
            LoanSlip = new LoanSlip();

            PassValueToItem(LoanSlip);

            ucAddLoan.getLoanSlipRepo().Add(LoanSlip);
            ucAddLoan.getLoanSlipRepo().WriteAdd(LoanSlip);

            ucAddLoan.getLoanDetailRepo().AddRange(LoanDetails);
            ucAddLoan.getLoanDetailRepo().WriteAddRange(LoanDetails);

            ChangeBookStatus(false);
            LoanDetails.Clear();

            IsCancel = false;
            getParentUc().BackToMainPage();
        }

        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in LoanDetails)
            {
                Book book = bookVM.FindById(loanDetail.IdBook, null);

                if (book.Status != updateStatus)
                {
                    book.Status = updateStatus;

                    // Save change to database
                    ucAddLoan.getBookRepo().WriteUpdateStatus(book, updateStatus);
                }
            }
            foreach (BookISBN isbn in ucAddLoan.getBookISBNRepo())
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
                ucAddLoan.getBookISBNRepo().WriteUpdateStatus(isbn, isbn.Status);
            }
        }


        #endregion

        #region Reader-Implement-Commands
        private void ReaderLoaded(object para)
        {
            // Load form in ucSelectReaderInfo
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();

            SelectedReaderType = ReaderType.Adult;
            ReadercbSelectReaderTypeSelectionChanged(getcbSelectReaderType());

            getstkAdultInformation().Visibility = Visibility.Visible;
            getstkChildInformation().Visibility = Visibility.Collapsed;
        }

        private bool IsAllSelecting()
        {
            if (SelectedReader == null)
            {
                Utilities.ShowMessageBox1("Please select reader");
                return false;
            }
            return true;
        }

        private void ReaderBtnConfirmClick(object para)
        {
            ucInputBookInfo = MainWindow.UnitOfForm.UcInputBookInfo(true);
            storeContent.Push(getucSelectReaderInfo().Content);
            getucSelectReaderInfo().Content = ucInputBookInfo;
        }

        private void ReaderBtnCancelClick(object para)
        {
            LoanSlipDto = null;
            getParentUc().BackToMainPage();
        }

        private void ReadercbSelectReaderTypeSelectionChanged(object para)
        {
            ComboBox cbSelectReaderType = para as ComboBox;
            if (SelectedReaderType == null) return;

            var readerType = (ReaderType)SelectedReaderType;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var adults = adultVM.GetListFromReaders(FillReaderByType, StatusValue);

                AllAdults = adultMap.ConvertToDto(adults);
                getcbAdultTxtIdentify().ItemsSource = AllAdults;

                getstkAdultInformation().Visibility = Visibility.Visible;
                getstkChildInformation().Visibility = Visibility.Collapsed;
            }
            else
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);


                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var childs = childVM.GetListFromReaders(FillReaderByType, StatusValue);
                var childDTOs = childMap.ConvertToDto(childs);


                var adults = adultVM.GetListFromChilds(childs, StatusValue);

                AllGuardians = adultMap.ConvertToDto(adults);
                getcbGuardianTxtIdentify().ItemsSource = AllGuardians;

                getstkAdultInformation().Visibility = Visibility.Collapsed;
                getstkChildInformation().Visibility = Visibility.Visible;
            }
        }



        private bool HandleAdult(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((AdultDto)cmb.SelectedItem).Identify;
            return true;
        }

        #region AdultInput
        private void ReadercbAdultTxtIdentifyDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleAdult(gettxtAdultInputIdentify(), cmb);
            handle = true;
        }

        private void ReadercbAdultTxtIdentifySelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(gettxtAdultInputIdentify(), cmb);
        }

        private void ReadertxtAdultInputIdentifyTextChanged(object para)
        {
            TxtInputIdentify_Filter_TextChanged(gettxtAdultInputIdentify(), getgdInputAdultIndentify(), AllAdults);
        }

        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = readerVM.FindById(getfillList.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);

                GetBooksFromReader();
                GetReaderLoanAndLoanDetails();

                getucLoanDetailsBorrowedTable().getLoanDetails = () => AllAdultLoanDetail;
                getucLoanDetailsBorrowedTable().ModifiedPagination();

                return;
            }
            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        private void GetReaderLoanAndLoanDetails()
        {
            if (SelectedReaderType == ReaderType.Adult)
            {
                AllAdultLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllAdultLoanDetail = loanDetailVM.FillListByIdLoans(AllAdultLoan);
            }
            else if (SelectedReaderType == ReaderType.Child)
            {
                AllChildLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
            }
        }


        #endregion

        #region GuardianInput
        private void ReadercbGuardianTxtIdentifyDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleAdult(gettxtGuardianInputIdentify(), cmb);
            handle = true;
        }

        private void ReadercbGuardianTxtIdentifySelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(gettxtGuardianInputIdentify(), cmb);
        }

        private void GetAllChildOfAdult(AdultDto adultDto)
        {
            AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            gettxtChildInputReaderName().Text = string.Empty;
            getcbChildTxtReaderName().ItemsSource = AllChilds;
        }

        private void ReadertxtGuardianInputIdentifyTextChanged(object para)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(gettxtGuardianInputIdentify(), getgdGuardianInput(), AllGuardians);
        }

        private void TxtInputGuardianIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Adult guardian = adultVM.FindByIdentify(getfillList.First().Identify, null);
                AdultDto guardianDto = adultMap.ConvertToDto(guardian);

                GetAllChildOfAdult(guardianDto);

                getgdInputChildName().IsEnabled = true;
                return;
            }
            else
            {
                getgdInputChildName().IsEnabled = false;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion

        #region ChildInput
        private void HandleChild(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((ChildDto)cmb.SelectedItem).FullName;
        }

        private void ReadercbChildTxtReaderNameDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleChild(gettxtChildInputReaderName(), cmb);
            handle = true;
        }

        private void ReadercbChildTxtReaderNameSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleChild(gettxtChildInputReaderName(), cmb);
        }

        private void ReadertxtChildInputReaderNameTextChanged(object para)
        {
            TxtInputChildReaderName_Filter_TextChanged(gettxtChildInputReaderName(), getgdInputChildName(), AllChilds);
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

                GetReaderLoanAndLoanDetails();
                getucLoanDetailsBorrowedTable().getLoanDetails = () => AllChildLoanDetail;
                getucLoanDetailsBorrowedTable().ModifiedPagination();
                return;
            }
            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion


        private void GetBooksFromReader()
        {
            ReaderDto reader = SelectedReader;

            var readerLoans = loanSlipVM.FillByIdReader(reader.Id, false);
            var readerBooks = bookVM.GetBooksInLoanSlips(readerLoans);
            BooksOfReader = readerBooks;
        }

        private void GetChildrenFromAdult(Adult adult)
        {
            AllChildOfAdult = childVM.FillByIdAdult(adult.IdReader, null);
        }

        private void GetChildBooks()
        {
            AllChildLoan = loanSlipVM.GetByListReader(AllChildOfAdult.Select(child => readerVM.FindById(child.IdReader)).ToObservableCollection());
            AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
            AllChildBook = bookVM.GetBooksInLoanDetails(AllChildLoanDetail);
        }
        #endregion

        #region BookInfo-Implement-Commands
        private void BookInfoLoaded(object para)
        {
            // Load form in ucInputBookInfo
            AllBookISBNCard = new ObservableCollection<ucBookISBNCard>();
            AllLoanDetailCard = new ObservableCollection<ucLoanDetailCard>();


            LoanDetails = new ObservableCollection<LoanDetail>();
            SelectedReaderType = ReaderType.Adult;
            AllBookNames = bookTitleMap.ConvertToDto(ucAddLoan.getBookTitleRepo().Gets());
            getcbTxtBookName().ItemsSource = AllBookNames;
        }
        

        private void NewDetail()
        {
            int indexId = LoanDetails.Count + ucAddLoan.getLoanDetailRepo().Count;

            LoanDetail = new LoanDetail();
            LoanDetail.Id = loanDetailVM.GetId(indexId);

            LoanDetail.IdLoan = LoanSlipDto.Id;
            LoanDetail.ExpDate = LoanSlipDto.ExpDate;
        }

        private void BookInfoBtnConfirmClick(object para)
        {
            if (LoanDetails.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't chosen any book yet");
                return;
            }
            CloseAndSave();
        }


        private void BookInfoBtnCancelClick(object para)
        {
            getucInputBookInfo().Content = storeContent.Pop();
        }

        private void BtnBookDetailConfirm(object sender, RoutedEventArgs e)
        {
            BookDto bookDto = SelectedBook;
            var tempCheck = bookVM.FindById(BooksOfReader, bookDto.Id, null);
            if (tempCheck == null)
                tempCheck = bookVM.FindById(bookVM.GetBooksInLoanDetails(LoanDetails), bookDto.Id, null);
            if (tempCheck != null)
            {
                Utilities.ShowMessageBox1("This reader already borrow this book!", "Can not borrow this book");
                return;
            }

            if (!bookDto.Status)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookStatus());
                return;
            }
            else
            {
                NewDetail();
                LoanDetail.IdBook = bookDto.Id;

                LoanDetails.Add(LoanDetail);

                ConvertToLoanDetailCard(LoanDetails);
                AddLoanDetailCardToWrap();

                AllBookISBN.Remove(AllBookISBN.FirstOrDefault(item => item.ISBN == SelectedISBN.ISBN));
                AllBookISBNCard.Remove(AllBookISBNCard.FirstOrDefault(item => item.getItem().ISBN == SelectedISBN.ISBN));

                AddBookISBNCardToWrap();
            }
        }

        private void UcLoanDetailCard_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            BookDto bookDto = bookMap.ConvertToDto(bookVM.FindById(SelectedDetailCard.Item.IdBook, null));

            LoanDetail getLoanDetail = LoanDetails.FirstOrDefault(item => item.Id == SelectedDetailCard.Item.Id);

            LoanDetails.Remove(getLoanDetail);

            ConvertToLoanDetailCard(LoanDetails);
            AddLoanDetailCardToWrap();


            BookISBN getISBN = bookISBNVM.FindByISBN(bookDto.ISBN, null);

            AllBookISBN.Add(getISBN);

            ConvertToBookISBNCard(AllBookISBN);
            AddBookISBNCardToWrap();
        }

        private void UcBookISBNCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookISBNCard ucBookISBNCard = sender as ucBookISBNCard;
            SelectedISBN = ucBookISBNCard.getItem();
            BookISBN isbn = bookISBNVM.FindByISBN(SelectedISBN.ISBN, null);

            var books = bookVM.FillByBookISBN(isbn.ISBN, BookStatusValue);
            if (!isbn.Status)
            {
                Utilities.ShowMessageBox1("All Books of this ISBN has been borrowed already");
                return;
            }
            else if (books.Count == 0)
            {
                Utilities.ShowMessageBox1("This ISBN doesn't have book already");
                return;
            }

            OpenSelectBookForm(books);

            if (SelectedBook == null) // User click cancel button
            {
                return;
            }
            BtnBookDetailConfirm(null, null);
        }


        private void ConvertToBookISBNCard(ObservableCollection<BookISBN> bookISBNs)
        {
            AllBookISBNCard.Clear();
            foreach (var isbn in bookISBNs)
            {
                ucBookISBNCard ucBookISBNCard = new ucBookISBNCard();
                ucBookISBNCard.Width = CardWidth;
                ucBookISBNCard.Margin = new Thickness(CardMargin);
                ucBookISBNCard.MouseLeftButtonDown += UcBookISBNCard_MouseLeftButtonDown;
                ucBookISBNCard.getItem = () => bookISBNMap.ConvertToDto(isbn);
                AllBookISBNCard.Add(ucBookISBNCard);
            }
        }

        private void ConvertToLoanDetailCard(ObservableCollection<LoanDetail> loanDetails)
        {
            AllLoanDetailCard.Clear();
            foreach (var isbn in loanDetails)
            {
                ucLoanDetailCard ucLoanDetailCard = new ucLoanDetailCard();
                ucLoanDetailCard.Width = CardWidth;
                ucLoanDetailCard.Margin = new Thickness(CardMargin);
                ucLoanDetailCard.getItem = () => loanDetailMap.ConvertToDto(isbn);

                ucLoanDetailCard.btnDeleteClick += (_sender, _e) =>
                {
                    ucLoanDetailCard card = ucLoanDetailCard;
                    if (SelectedDetailCard != null && SelectedDetailCard == card) return;

                    SelectedDetailCard = card;
                };

                ucLoanDetailCard.btnDeleteClick += UcLoanDetailCard_btnDeleteClick;
                AllLoanDetailCard.Add(ucLoanDetailCard);
            }
        }

        private void AddBookISBNCardToWrap()
        {
            getwrapBookISBN().Children.Clear();
            foreach (var ucCard in AllBookISBNCard)
            {
                getwrapBookISBN().Children.Add(ucCard);
            }
        }

        private void AddLoanDetailCardToWrap()
        {
            getwrapLoanDetail().Children.Clear();
            foreach (var ucCard in AllLoanDetailCard)
            {
                getwrapLoanDetail().Children.Add(ucCard);
            }
        }

        #region BookNameInput
        private void HandleBookName(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((BookTitleDto)cmb.SelectedItem).Name;
        }

        private void ReadercbTxtBookNameDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleBookName(gettxtInputBookName(), cmb);
            handle = true;
        }

        private void ReadercbTxtBookNameSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleBookName(gettxtInputBookName(), cmb);
        }

        private void ReadertxtInputBookNameTextChanged(object para)
        {
            TxtInputBookName_Filter_TextChanged(gettxtInputBookName(), getgdInputBookName(), AllBookNames);
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

                if (getcbTxtBookName().SelectedItem == null)
                    return;
                BookTitleDto bookTitleDto = getcbTxtBookName().SelectedItem as BookTitleDto;

                AllBookISBN = bookISBNVM.FillByIdBookTitle(bookTitleDto.Id, BookISBNStatusValue);
                AllBookISBNCard.Clear();
                ConvertToBookISBNCard(AllBookISBN);
                AddBookISBNCardToWrap();

                return;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion


        #endregion
        

        public void CalculatePayment()
        {
            Parameter paraQD10 = paraVM.FindById(Constants.paraQD10);
            Parameter paraQD11 = paraVM.FindById(Constants.paraQD11);
            decimal paraQD11_Value = Convert.ToDecimal(paraQD11.Value.Replace("%", "")) / 100;

            LoanSlipDto.LoanPaid = Convert.ToDecimal(paraQD10.Value.ToString());

            LoanSlipDto.Deposit = CalculateDeposit(paraQD11_Value);
        }

        private decimal CalculateDeposit(decimal percentValue)
        {
            decimal price = 0.0M;
            var listBook = bookVM.GetBooksInLoanDetails(LoanDetails);

            foreach (var book in listBook)
            {
                price += book.Price * percentValue;
            }
            return price;
        }
        

        private void PassValueToItem(LoanSlip item)
        {
            item.Id = LoanSlipDto.Id;
            item.IdUser = LoanSlipDto.IdUser;
            item.IdReader = SelectedReader.Id;
            item.Quantity = LoanDetails.Count;
            item.LoanDate = LoanSlipDto.LoanDate;
            item.ExpDate = LoanSlipDto.ExpDate;
            item.LoanPaid = LoanSlipDto.LoanPaid;
            item.Deposit = LoanSlipDto.Deposit;
        }

        private void OpenSelectBookForm(ObservableCollection<Book> books)
        {
            ucBooksTable ucSelectBooksTable = MainWindow.UnitOfForm.UcBooksTable(true);

            #region Set-ExceptProperty
            List<PropertyInfo> allProperties = Utilities.getPropsFromType(typeof(BookDto));
            List<PropertyInfo> exceptDtgProperties = Utilities.FillPropertiesByName(allProperties, Constants.exceptDtgCreateLoanSlipBook);
            ucSelectBooksTable.getExceptProperties = () => exceptDtgProperties;
            #endregion

            ucSelectBooksTable.Books = bookMap.ConvertToDto(books);

            Button btnConfirmSelectBook = new Button();
            Button btnCancelSelectBook = new Button();

            btnConfirmSelectBook.Style = Application.Current.FindResource(Constants.styleBtnConfirm) as Style;
            btnCancelSelectBook.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;

            frmDefault frmSelectBooksTable = new frmDefault();
            frmSelectBooksTable.Width = 900;
            frmSelectBooksTable.SizeToContent = SizeToContent.Height;
            frmSelectBooksTable.frmTitle = "Select book form";
            frmSelectBooksTable.lblHeader = "Please select book in this ISBN";

            btnConfirmSelectBook.Click += (_sender, _e) =>
            {
                SelectedBook = ucSelectBooksTable.SelectedDto;
                if (SelectedBook == null)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                    return;
                }
                frmSelectBooksTable.Close();
            };
            btnCancelSelectBook.Click += (_sender, _e) =>
            {
                frmSelectBooksTable.Close();
                SelectedBook = null;
            };

            Utilities.AddItemToFormDefault(frmSelectBooksTable, ucSelectBooksTable, btnConfirmSelectBook, btnCancelSelectBook);
            frmSelectBooksTable.ShowDialog();
        }
        

    }
}
