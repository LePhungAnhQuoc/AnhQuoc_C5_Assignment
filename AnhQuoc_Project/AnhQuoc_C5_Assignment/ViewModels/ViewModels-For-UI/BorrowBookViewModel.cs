using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace AnhQuoc_C5_Assignment
{
    public class BorrowBookViewModel : BaseViewModel<Book>, IPageViewModel
    {
        #region Forms
        private ucSelectReaderInfo ucSelectReaderInfo;
        private ucInputBookInfo ucInputBookInfo;
        #endregion

        #region Constants
        private const double CardWidth = 200;
        private const double CardMargin = 5;

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

        BindingExpression bind = null;
        ExpireDateRule expAdultRule = null;
        ExpireDateRule expGuardianRule = null;
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

        private ObservableCollection<LoanDetail> _AllReaderLoanDetail;
        public ObservableCollection<LoanDetail> AllReaderLoanDetail
        {
            get { return _AllReaderLoanDetail; }
            set
            {
                _AllReaderLoanDetail = value;
                OnPropertyChanged();
            }
        }
        

        public ObservableCollection<BookISBN> AllBookISBN { get; set; }
        public ObservableCollection<ucBookISBNCard> AllBookISBNCard { get; set; }
        public ObservableCollection<ucLoanDetailCard> AllLoanDetailCard { get; set; }


        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public bool? BookISBNStatusValue { get; set; } = null;
        public bool? BookStatusValue { get; set; } = null;
        public bool? ReaderStatusValue { get; set; } = null;
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

        private AdultDto _Guardian;
        public AdultDto Guardian
        {
            get { return _Guardian; }
            set 
            {
                _Guardian = value;
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
            AllReaderTypes = Utilitys.GetListFromEnum<ReaderType>().ToObservableCollection();
            AllReaderLoanDetail = new ObservableCollection<LoanDetail>();

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
            LoanSlipDto.User = MainWindow.loginUser;

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
            SelectedReader = null;

            ucSelectReaderInfo = MainWindow.UnitOfForm.UcSelectReaderInfo(true);
            ucSelectReaderInfo.ucLoanDetailsBorrowedTable.dtgReader.Visibility = Visibility.Visible;
            ucSelectReaderInfo.ucLoanDetailsBorrowedTable.dtgReaderType.Visibility = Visibility.Visible;

            ucSelectReaderInfo.ucLoanDetailsBorrowedTable.dgDatas.LoadingRow += DgDatas_LoadingRow;

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
            // Truyền dữ liệu cho item
            LoanSlip LoanSlip = new LoanSlip();
            Utilitys.Copy(LoanSlip, LoanSlipDto);
            LoanSlip.Reader = null;
            LoanSlip.User = null;


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
                    ucAddLoan.getBookRepo().WriteUpdate(book);
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
                ucAddLoan.getBookISBNRepo().WriteUpdate(isbn);
            }
        }


        #endregion

        #region Reader-Implement-Commands
        private void ReaderLoaded(object para)
        {
            // Load form in ucSelectReaderInfo
            AllReaderTypes = Utilitys.GetListFromEnum<ReaderType>().ToObservableCollection();


            bind = ucSelectReaderInfo.txtExpireDate.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
            expAdultRule = bind.ParentBinding.ValidationRules[0] as ExpireDateRule;
            bind = ucSelectReaderInfo.txtGuardianExpireDate.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
            expGuardianRule = bind.ParentBinding.ValidationRules[0] as ExpireDateRule;
        }

        private void DgDatas_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            LoanDetailDto detail = e.Row.Item as LoanDetailDto;

            if (loanDetailVM.IsOutOfExpireDate(loanDetailVM.CreateByDto(detail)))
            {
                e.Row.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
        
        private bool IsAllSelecting()
        {
            if (SelectedReader == null)
            {
                Utilitys.ShowMessageBox1("Please select reader");
                return false;
            }
            return true;
        }

        private void ReaderBtnConfirmClick(object para)
        {
            ucInputBookInfo = MainWindow.UnitOfForm.UcInputBookInfo(true);
            storeContent.Push(ucAddLoan.Content);
            ucAddLoan.Content = ucInputBookInfo;
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
            ComboBox comBoBox = Utilitys.FindVisualChild<ComboBox>(parent);

            if (Utilitys.IsCheckEmptyString(txtInput.Text))
            {
                SelectedReader = null;
                return;
            }

            ObservableCollection<Reader> getfillList = readerVM.FillContainIds(sourceDto, txtInput.Text, ignoreCase, ReaderStatusValue);

            if (getfillList.Count == 1 && getfillList.First().Id == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = getfillList.First();

                SelectedReader = readerMap.ConvertToDto(reader);

                if (SelectedReader.ReaderType == ReaderType.Child)
                {
                    Child child = childVM.FindByIdReader(SelectedReader.Id, null);
                    Guardian = adultMap.ConvertToDto(adultVM.FindByIdReader(child.IdAdult, null));
                    
                    ucSelectReaderInfo.stkGuardian.IsEnabled = true;
                }
                else if (SelectedReader.ReaderType == ReaderType.Adult)
                {
                    Guardian = null;
                    ucSelectReaderInfo.stkGuardian.IsEnabled = false;
                }

                LoanSlipDto.IdReader = SelectedReader.Id;
                LoanSlipDto.Reader = reader;

                GetBooksFromReader();
                GetReaderLoanAndLoanDetails();
                
                ucSelectReaderInfo.ucLoanDetailsBorrowedTable.getLoanDetails = () => AllReaderLoanDetail;
                ucSelectReaderInfo.ucLoanDetailsBorrowedTable.ModifiedPagination();


                #region Modify-btnConfirm-IsEnabled
                bool isOutOfExpireLoanDetail = loanDetailVM.IsOutOfExpireDate(AllReaderLoanDetail);

                #region Parameter
                ParameterViewModel paraVM = UnitOfViewModel.Instance.ParameterViewModel;
                Parameter para = paraVM.FindById(Constants.paraQD2);
                int value = -1;
                int.TryParse(para.Value, out value);
                #endregion

                Reader adultReaderFind = null;
                if (SelectedReader.ReaderType == ReaderType.Adult)
                    adultReaderFind = readerVM.FindById(reader.Id);
                else if (SelectedReader.ReaderType == ReaderType.Child)
                {
                    Child child = childVM.FindByIdReader(reader.Id);
                    Adult adult = adultVM.FindByIdReader(child.IdAdult);
                    adultReaderFind = readerVM.FindById(adult.IdReader);
                }

                if (adultReaderFind.Status == false || isOutOfExpireLoanDetail || BooksOfReader.Count >= value)
                    ucSelectReaderInfo.btnConfirm.IsEnabled = false;
                else
                    ucSelectReaderInfo.btnConfirm.IsEnabled = true;

                if (SelectedReader.ReaderType == ReaderType.Adult)
                {
                    expAdultRule.ValidatesOnTargetUpdated = true;
                    expGuardianRule.ValidatesOnTargetUpdated = false;

                    // Toggle OnPropertyChanged (Debt code!)
                    SelectedReader = SelectedReader;
                    Guardian = Guardian;
                }
                else
                {
                    expAdultRule.ValidatesOnTargetUpdated = false;
                    expGuardianRule.ValidatesOnTargetUpdated = true;

                    // Toggle OnPropertyChanged (Debt code!)
                    SelectedReader = SelectedReader;
                    Guardian = Guardian;
                }
                
                #endregion

                return;
            }
            else
            {
                SelectedReader = null;

                LoanSlipDto.IdReader = null;
                LoanSlipDto.Reader = null;
            }
            comBoBox.ItemsSource = readerMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion
        
        private void GetReaderLoanAndLoanDetails()
        {
            Action<string> adultAction = (IdAdultReader) =>
            {
                GetChildrenFromAdult(adultVM.FindByIdReader(IdAdultReader, null));


                var allChildLoanDetails = new ObservableCollection<LoanDetail>();
                foreach (Child child in AllChildOfAdult)
                {
                    var childLoans = loanSlipVM.FillByIdReader(child.IdReader);
                    var childLoanDetails = loanDetailVM.FillListByIdLoans(childLoans);

                    allChildLoanDetails.AddRange(childLoanDetails);
                }

                var adultLoans = loanSlipVM.FillByIdReader(IdAdultReader);
                var adultLoanDetails = loanDetailVM.FillListByIdLoans(adultLoans);

                AllReaderLoanDetail.Clear();

                AllReaderLoanDetail.AddRange(adultLoanDetails);
                AllReaderLoanDetail.AddRange(allChildLoanDetails);
            };

            if (SelectedReader.ReaderType == ReaderType.Adult)
            {
                adultAction(SelectedReader.Id);
            }
            else if (SelectedReader.ReaderType == ReaderType.Child)
            {
                Child child = childVM.FindByIdReader(SelectedReader.Id, null);
                Reader adult = readerVM.FindById(child.IdAdult);
                adultAction(adult.Id);
            }
        }

        private void GetBooksFromReader()
        {
            ReaderDto reader = SelectedReader;

            Action<ReaderDto> adultAction = (adultReader) =>
            {
                var adultReaderLoans = loanSlipVM.FillByIdReader(adultReader.Id, false);
                var adultReaderBooks = bookVM.GetBooksInLoanSlips(adultReaderLoans);
                BooksOfReader = adultReaderBooks;


                // get all child
                ObservableCollection<Child> allChilds = childVM.FillByIdAdult(adultReader.Id, true);

                allChilds.ForEach((childItem) =>
                {
                    var childLoans = loanSlipVM.FillByIdReader(childItem.IdReader, false);
                    var childBooks = bookVM.GetBooksInLoanSlips(childLoans);
                    BooksOfReader.AddRange(childBooks);
                });
            };

            if (SelectedReader.ReaderType == ReaderType.Adult)
            {
                adultAction(SelectedReader);
            }

            else if (SelectedReader.ReaderType == ReaderType.Child)
            {
                Child child = childVM.FindByIdReader(SelectedReader.Id);
                Reader adultReader = readerVM.FindById(child.IdAdult);

                adultAction(readerMap.ConvertToDto(adultReader));
            }
        }

        private void GetChildrenFromAdult(Adult adult)
        {
            AllChildOfAdult = childVM.FillByIdAdult(adult.IdReader, null);
        }
        
        #endregion

        #region BookInfo-Implement-Commands
        private void BookInfoLoaded(object para)
        {
            // Load form in ucInputBookInfo
            AllBookISBNCard = new ObservableCollection<ucBookISBNCard>();
            AllLoanDetailCard = new ObservableCollection<ucLoanDetailCard>();


            LoanDetails = new ObservableCollection<LoanDetail>();

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
            int indexId = LoanDetails.Count + loanDetailVM.getMaxIndexId(nameof(LoanDetail.Id)); 

            LoanDetail = new LoanDetail();
            LoanDetail.Id = loanDetailVM.GetId(indexId);

            LoanDetail.IdLoan = LoanSlipDto.Id;
            LoanDetail.ExpDate = LoanSlipDto.ExpDate;
        }

        private void BookInfoBtnConfirmClick(object para)
        {
            CalculatePayment();
            LoanSlipDto.Quantity = LoanDetails.Count;

            if (LoanDetails.Count == 0)
            {
                Utilitys.ShowMessageBox1("You haven't chosen any book yet");
                return;
            }

            frmDefault frmConfirmInformation = new frmDefault();
            ucLoanSlipConfirm ucLoanSlipConfirm = new ucLoanSlipConfirm();

            ucLoanSlipConfirm.Margin = new Thickness(10);

            Button btnConfirm = ucLoanSlipConfirm.btnConfirm;
            Button btnCancel = ucLoanSlipConfirm.btnCancel;

            btnConfirm.Click += (_sender, _e) =>
            {
                CloseAndSave();
                frmConfirmInformation.Close();
            };

            btnCancel.Click += (_sender, _e) =>
            {
                frmConfirmInformation.Close();
            };

            Utilitys.AddItemToFormDefault(frmConfirmInformation, ucLoanSlipConfirm);

            frmConfirmInformation.Width = 800;
            frmConfirmInformation.SizeToContent = SizeToContent.Height;

            frmConfirmInformation.ShowDialog();
        }


        private void BookInfoBtnCancelClick(object para)
        {
            ucAddLoan.Content = storeContent.Pop();
        }

        private void BtnBookDetailConfirm(object sender, RoutedEventArgs e)
        {
            #region Parameter
            Parameter para = paraVM.FindById(Constants.paraQD2);
            int value = -1;
            int.TryParse(para.Value, out value);
            #endregion

            if (BooksOfReader.Count + LoanDetails.Count >= value)
            {
                Utilitys.ShowMessageBox1($"1 reader can only borrow a maximum of {value} books");
                return;
            }

            NewDetail();
            LoanDetail.IdBook = SelectedBook.Id;

            LoanDetails.Add(LoanDetail);

            ConvertToLoanDetailCard(LoanDetails);
            AddLoanDetailCardToWrap();


            AllBookISBN.Remove(AllBookISBN.FirstOrDefault(item => item.ISBN == SelectedISBN.ISBN));
            AllBookISBNCard.Remove(AllBookISBNCard.FirstOrDefault(item => item.getItem().ISBN == SelectedISBN.ISBN));
            AddBookISBNCardToWrap();
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
                Utilitys.ShowMessageBox1("All Books of this ISBN has been borrowed already");
                return;
            }
            else if (books.Count == 0)
            {
                Utilitys.ShowMessageBox1("This ISBN doesn't have book already");
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
            ComboBox comBoBox = Utilitys.FindVisualChild<ComboBox>(parent);

            Action<ObservableCollection<BookISBN>> handleBookISBN = (sourceISBN) =>
            {
                AllBookISBNCard.Clear();
                ConvertToBookISBNCard(AllBookISBN);
                AddBookISBNCardToWrap();
            };

            if (Utilitys.IsCheckEmptyString(txtInput.Text))
            {
                AllBookISBN = ucAddLoan.getBookISBNRepo().Gets();
                handleBookISBN(AllBookISBN);

                return;
            }
               
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
                handleBookISBN(AllBookISBN);

                return;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion


        #endregion
        

        public void CalculatePayment()
        {
            var listBook = bookVM.GetBooksInLoanDetails(LoanDetails);

            Parameter paraQD10 = paraVM.FindById(Constants.paraQD10);
            Parameter paraQD11 = paraVM.FindById(Constants.paraQD11);
            decimal paraQD11_Value = Convert.ToDecimal(paraQD11.Value.Replace("%", "")) / 100;

            LoanSlipDto.LoanPaid = Convert.ToDecimal(paraQD10.Value.ToString().Replace(".000", "")) * listBook.Count;
            LoanSlipDto.Deposit = CalculateDeposit(listBook, paraQD11_Value);
        }

        private decimal CalculateDeposit(ObservableCollection<Book> listBook, decimal percentValue)
        {
            decimal price = 0.0M;

            foreach (var book in listBook)
            {
                price += book.Price * percentValue;
            }
            return price;
        }
        
        
        private void OpenSelectBookForm(ObservableCollection<Book> books)
        {
            frmDefault frmSelectBooksTable = new frmDefault();

            ucBooksTable ucSelectBooksTable = new ucBooksTable();
            ucSelectBooksTable.AllowPagination = false;

            ucSelectBooksTable.getExceptProperties = () => Constants.exceptDtgBookCreateLoanSlip;
            ucSelectBooksTable.Books = bookMap.ConvertToDto(books);

            Button btnConfirmSelectBook = new Button();
            Button btnCancelSelectBook = new Button();

            btnConfirmSelectBook.Style = Application.Current.FindResource(Constants.styleBtnConfirm) as Style;
            btnCancelSelectBook.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;

            frmSelectBooksTable.frmTitle = "Select book form";
            frmSelectBooksTable.lblHeader = "Please select book in this ISBN";

            frmSelectBooksTable.Width = 900;
            frmSelectBooksTable.Height = 500;
            frmSelectBooksTable.SizeToContent = SizeToContent.Manual;

            bool isConfirm = false;
            Action<object> confirmHandle = (sender) =>
            {
                SelectedBook = ucSelectBooksTable.SelectedDto;
                if (SelectedBook == null)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("book"));
                    return;
                }

                // Kiểm tra tình trạng cuốn sách
                if (!SelectedBook.Status)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyBookStatus());
                    return;
                }

                if (SelectedBook.IdBookStatus == Constants.bookStatusSpoil)
                {
                    Utilitys.ShowMessageBox1("This book cannot be borrowed because the book is spoiled");
                    return;
                }

                isConfirm = true;
                frmSelectBooksTable.Close();
            };


            btnConfirmSelectBook.Click += (_sender, _e) => confirmHandle(_sender);
            btnCancelSelectBook.Click += (_sender, _e) =>
            {
                frmSelectBooksTable.Close();
                SelectedBook = null;
            };

            frmSelectBooksTable.Closing += (_sender, _e) =>
            {
                if (isConfirm == false)
                    SelectedBook = null;
            };

            ucSelectBooksTable.dgBooks.MouseDoubleClick += (_sender, _e) => confirmHandle(_sender);

            Utilitys.AddItemToFormDefault(frmSelectBooksTable, ucSelectBooksTable, btnConfirmSelectBook, btnCancelSelectBook);

            // Show
            frmSelectBooksTable.ShowDialog();
        }
    }
}
