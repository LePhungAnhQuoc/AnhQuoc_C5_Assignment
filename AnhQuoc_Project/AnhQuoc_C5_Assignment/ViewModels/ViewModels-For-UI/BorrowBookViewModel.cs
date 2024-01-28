﻿using System;
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
        

        #region RelayCommands
        public RelayCommand ucAddLoanLoadedCmd { get; private set; }
        public RelayCommand ReaderLoadedCmd { get; private set; }
        public RelayCommand BookInfoLoadedCmd { get; private set; }

        public RelayCommand ReaderBtnConfirmClickCmd { get; private set; }
        public RelayCommand ReaderBtnCancelClickCmd { get; private set; }
        public RelayCommand BookInfoBtnConfirmClickCmd { get; private set; }
        public RelayCommand BookInfoBtnCancelClickCmd { get; private set; }


        public RelayCommand ReadercbTxtIdReaderDropDownClosedCmd { get; private set; }
        public RelayCommand ReadercbTxtIdReaderSelectionChangedCmd { get; private set; }
        public RelayCommand ReadertxtIdReaderTextChangedCmd { get; private set; }
        

        // Input book Info
        public RelayCommand ReadercbTxtBookNameDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbTxtBookNameSelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtInputBookNameTextChangedCmd { get; set; }
        #endregion
        
        public BorrowBookViewModel()
        {
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


            ReadercbTxtIdReaderDropDownClosedCmd = new RelayCommand(null, ReadercbTxtIdReaderDropDownClosed);
            ReadercbTxtIdReaderSelectionChangedCmd = new RelayCommand(null, ReadercbTxtIdReaderSelectionChanged);
            ReadertxtIdReaderTextChangedCmd = new RelayCommand(null, ReadertxtIdReaderTextChanged);



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

            Parameter paramExpDate = paraVM.FindById(Constants.paraQD9);
            int days = Convert.ToInt32(paramExpDate.Value);
            LoanSlipDto.ExpDate = LoanSlipDto.LoanDate.AddDays(days);
        }

        

        #region AddLoan-Inplement-Commands
        public void ucAddLoanLoaded(object para)
        {
            // Load Form in ucAddLoan
            ucAddLoan = para as ucAddLoan;
            AllReader = readerMap.ConvertToDto(ucAddLoan.getReaderRepo().Gets());
            
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
            ucAddLoan.getParentUc().BackToMainPage();
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
            storeContent.Push(ucSelectReaderInfo.Content);
            ucSelectReaderInfo.Content = ucInputBookInfo;
        }

        private void ReaderBtnCancelClick(object para)
        {
            LoanSlipDto = null;
            ucAddLoan.getParentUc().BackToMainPage();
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
            if (handle) HandleReader(ucSelectReaderInfo.txtIdReader, cmb);
            handle = true;
        }

        private void ReadercbTxtIdReaderSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleReader(ucSelectReaderInfo.txtIdReader, cmb);
        }

        private void ReadertxtIdReaderTextChanged(object para)
        {
            TxtIdReader_Filter_TextChanged(ucSelectReaderInfo.txtIdReader, ucSelectReaderInfo.gdInputIdReader, readerVM.CreateByDto(AllReader));
        }

        private void TxtIdReader_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<Reader> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Reader> getfillList = readerVM.FillContainIds(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Id == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = getfillList.First();
                SelectedReader = readerMap.ConvertToDto(reader);

                GetBooksFromReader();

                GetReaderLoanAndLoanDetails();

                if (SelectedReader.ReaderType == ReaderType.Adult)
                {
                    ucSelectReaderInfo.ucLoanDetailsBorrowedTable.getLoanDetails = () => AllAdultLoanDetail;
                }
                else if (SelectedReader.ReaderType == ReaderType.Child)
                {
                    ucSelectReaderInfo.ucLoanDetailsBorrowedTable.getLoanDetails = () => AllChildLoanDetail;
                }
                ucSelectReaderInfo.ucLoanDetailsBorrowedTable.ModifiedPagination();

                return;
            }

            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = readerMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion


        private void GetReaderLoanAndLoanDetails()
        {
            if (SelectedReader.ReaderType == ReaderType.Adult)
            {
                AllAdultLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllAdultLoanDetail = loanDetailVM.FillListByIdLoans(AllAdultLoan);
            }
            else if (SelectedReader.ReaderType == ReaderType.Child)
            {
                AllChildLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
            }
        }

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
            SelectedReader.ReaderType = ReaderType.Adult;
            AllBookNames = bookTitleMap.ConvertToDto(ucAddLoan.getBookTitleRepo().Gets());
            ucInputBookInfo.cbTxtBookName.ItemsSource = AllBookNames;

            // Load All Book ISBN In WrapPanel
            AllBookISBN = bookISBNVM.FillByStatus(ucAddLoan.getBookISBNRepo().Gets(), BookISBNStatusValue);
            AllBookISBNCard.Clear();
            ConvertToBookISBNCard(AllBookISBN);
            AddBookISBNCardToWrap();
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
            ucInputBookInfo.Content = storeContent.Pop();
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
            ucInputBookInfo.wrapBookISBN.Children.Clear();
            foreach (var ucCard in AllBookISBNCard)
            {
                ucInputBookInfo.wrapBookISBN.Children.Add(ucCard);
            }
        }

        private void AddLoanDetailCardToWrap()
        {
            ucInputBookInfo.wrapLoanDetail.Children.Clear();
            foreach (var ucCard in AllLoanDetailCard)
            {
                ucInputBookInfo.wrapLoanDetail.Children.Add(ucCard);
            }
        }

        #region Input-BookName
        private void HandleBookName(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((BookTitleDto)cmb.SelectedItem).Name;
        }

        private void ReadercbTxtBookNameDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleBookName(ucInputBookInfo.txtInputBookName, cmb);
            handle = true;
        }

        private void ReadercbTxtBookNameSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleBookName(ucInputBookInfo.txtInputBookName, cmb);
        }

        private void ReadertxtInputBookNameTextChanged(object para)
        {
            TxtInputBookName_Filter_TextChanged(ucInputBookInfo.txtInputBookName, ucInputBookInfo.gdInputBookName, AllBookNames);
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

                if (ucInputBookInfo.cbTxtBookName.SelectedItem == null)
                    return;

                BookTitleDto bookTitleDto = ucInputBookInfo.cbTxtBookName.SelectedItem as BookTitleDto;

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
            ucSelectBooksTable.getExceptProperties = () => Constants.exceptDtgBookCreateLoanSlip;
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
