﻿using System;
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
    /// Interaction logic for frmBorrowBook.xaml
    /// </summary>
    public partial class frmBorrowBook : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<EnrollRepository> getEnrollRepo { get; set; }

        #endregion

        #region Properties

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

        public ObservableCollection<Book> AllBooksOfReader { get; set; }
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
        #endregion

        #region Fields
        private bool handle = true;
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
        private EnrollViewModel enrollVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        #endregion

        #region ResultProperty
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

        private Enroll _Enroll;
        public Enroll Enroll
        {
            get { return _Enroll; }
            set
            {
                _Enroll = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<LoanDetail> LoanDetails { get; set; }
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

        public frmBorrowBook()
        {
            InitializeComponent();
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();
            
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            enrollVM = UnitOfViewModel.Instance.EnrollViewModel;


            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;


            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            //txtLName.MaxLength = maxLength;
            //txtFName.MaxLength = maxLength;
            #endregion

            btnConfirm.Click += BtnConfirm_Click;
            btnBookDetailConfirm.Click += BtnBookDetailConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            cbSelectReaderType.SelectionChanged += CbSelectReaderType_SelectionChanged;

            cbAdultTxtIdentify.DropDownClosed += CbAdultTxtIdentify_DropDownClosed;
            cbAdultTxtIdentify.SelectionChanged += CbAdultTxtIdentify_SelectionChanged;
            txtAdultInputIdentify.TextChanged += TxtInputIdentify_TextChanged;

            cbGuardianTxtIdentify.DropDownClosed += CbGuardianTxtIdentify_DropDownClosed;
            cbGuardianTxtIdentify.SelectionChanged += CbGuardianTxtIdentify_SelectionChanged;
            txtGuardianInputIdentify.TextChanged += TxtGuardianInputIdentify_TextChanged;

            cbChildTxtReaderName.DropDownClosed += CbChildTxtReaderName_DropDownClosed;
            cbChildTxtReaderName.SelectionChanged += CbChildTxtReaderName_SelectionChanged;
            txtChildInputReaderName.TextChanged += TxtChildInputReaderName_TextChanged;

            cbTxtBookName.DropDownClosed += CbTxtBookName_DropDownClosed;
            cbTxtBookName.SelectionChanged += CbTxtBookName_SelectionChanged;
            txtInputBookName.TextChanged += TxtInputBookName_TextChanged;

            gdInputChildName.IsEnabledChanged += GdInputChildName_IsEnabledChanged;
            gdInputBookName.IsEnabledChanged += GdInputBookName_IsEnabledChanged;

            bdInputBookInformation.IsEnabledChanged += BdInputBookInformation_IsEnabledChanged;
            cbSelectLanguage.IsEnabledChanged += CbSelectLanguage_IsEnabledChanged;
            cbSelectLanguage.SelectionChanged += CbSelectLanguage_SelectionChanged;


            this.Loaded += frmBorrowBook_Loaded;
            this.DataContext = this;
        }

        private void frmBorrowBook_Loaded(object sender, RoutedEventArgs e)
        {
            NewItem();
            ucLoanDetailsTable.LoanDetails = new ObservableCollection<LoanDetail>();
            
            List<PropertyInfo> allProperties = Utilities.getPropsFromType(typeof(BookDto));
            List<PropertyInfo> exceptDtgProperties = Utilities.FillPropertiesByName(allProperties, Constants.exceptDtgBorrowBook);
            ucTemporaryBooksTable.getExceptProperties = () => exceptDtgProperties;

            LoanDetails = new ObservableCollection<LoanDetail>();
            SelectedReaderType = ReaderType.Adult;
        }

        private void NewItem()
        {
            const int expDay = 7;

            LoanSlip = new LoanSlip();
            LoanSlip.Id = loanSlipVM.GetId();
            LoanSlip.LoanDate = DateTime.Now;
            LoanSlip.ExpDate = LoanSlip.LoanDate.AddDays(expDay);
        }
        
        private void NewDetail()
        {
            LoanDetail = new LoanDetail();
            LoanDetail.Id = loanDetailVM.GetId();
            LoanDetail.IdLoan = LoanSlip.Id;
            LoanDetail.ExpDate = LoanSlip.ExpDate;
        }

        private void NewEnroll()
        {
            Enroll = new Enroll();
            Enroll.Id = enrollVM.GetId();
            Enroll.EnrolDate = DateTime.Now;
            Enroll.ExpDate = null;
            Enroll.Note = string.Empty;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.IsCheckEmptyString(txtDeposit.Text))
            {
                Utilities.ShowMessageBox1("Please enter deposit amount");
                return;
            }
            if (LoanDetails.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't chosen any book yet");
                return;
            }
            // Truyền dữ liệu cho item
            PassValueToItem(LoanSlip);

            getLoanSlipRepo().Add(LoanSlip);
            getLoanSlipRepo().WriteAdd(LoanSlip);

            getLoanDetailRepo().AddRange(LoanDetails);
            getLoanDetailRepo().WriteAddRange(LoanDetails);

            ChangeBookStatus(false);
            LoanDetails.Clear();
            this.Close();
        }

        private void BtnBookDetailConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ucTemporaryBooksTable.dgBooks.SelectedItem == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                return;
            }
            BookDto bookDto = ucTemporaryBooksTable.dgBooks.SelectedItem as BookDto;
            var tempCheck = bookVM.FindById(AllBooksOfReader, bookDto.Id, null);
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
                ucLoanDetailsTable.LoanDetails.Add(LoanDetail);

                LoanDetails.Add(LoanDetail);
                ucTemporaryBooksTable.Books.Clear();

                AllLanguages.Remove(bookDto.Language);
                txtDisplayBookISBN.Text = txtDisplayBookISBNStatus.Text = string.Empty;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoanSlip = null;

            this.Close();
        }

        private void PassValueToItem(LoanSlip item)
        {
            LoanSlip.IdReader = SelectedReader.Id;
            LoanSlip.Quantity = LoanDetails.Count;
            LoanSlip.LoanPaid = Convert.ToDecimal("0.000");
            LoanSlip.Deposit = Convert.ToDecimal(txtDeposit.Text);
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
                bdInputBookInformation.IsEnabled = true;
                return;
            }
            else
            {
                bdInputBookInformation.IsEnabled = false;
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
                bdInputBookInformation.IsEnabled = true;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);
                GetBooksFromReader();
                return;
            }
            else
            {
                bdInputBookInformation.IsEnabled = false;
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
                cbSelectLanguage.IsEnabled = true;
                return;
            }
            else
            {
                cbSelectLanguage.IsEnabled = false;
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
        }

        private void TxtGuardianInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(txtGuardianInputIdentify, gdGuardianInput, AllGuardians);
        }
        #endregion

        #region ChildInput
        private void HandleChild(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((ChildDto)cmb.SelectedItem).FullName;
        }

        private void CbChildTxtReaderName_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleChild(txtChildInputReaderName, cmb);
            handle = true;
        }

        private void CbChildTxtReaderName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleChild(txtChildInputReaderName, cmb);
        }

        private void TxtChildInputReaderName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputChildReaderName_Filter_TextChanged(txtChildInputReaderName, gdInputChildName, AllChilds);
        }

        #endregion

        #region BookNameInput
        private void HandleBookName(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((BookTitleDto)cmb.SelectedItem).Name;
        }

        private void CbTxtBookName_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleBookName(txtInputBookName, cmb);
            handle = true;
        }

        private void CbTxtBookName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleBookName(txtInputBookName, cmb);
        }

        private void TxtInputBookName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputBookName_Filter_TextChanged(txtInputBookName, gdInputBookName, AllBookNames);
        }

        #endregion

        // Others methods
       
        #region GetsDatas
        private void CbSelectReaderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var readerType = (ReaderType)cbSelectReaderType.SelectedItem;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var adults = adultVM.GetListFromReaders(FillReaderByType, StatusValue);

                AllAdults = adultMap.ConvertToDto(adults);
                cbAdultTxtIdentify.ItemsSource = AllAdults;

                stkAdultInformation.Visibility = Visibility.Visible;
                stkChildInformation.Visibility = Visibility.Collapsed;
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
                cbGuardianTxtIdentify.ItemsSource = AllGuardians;

                stkAdultInformation.Visibility = Visibility.Collapsed;
                stkChildInformation.Visibility = Visibility.Visible;
            }
        }

        private void GdInputChildName_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (sender as Grid);
            if (!grid.IsEnabled)
                return;
            handle = true;
            if (cbGuardianTxtIdentify.SelectedItem == null)
                return;
            AdultDto adultDto = cbGuardianTxtIdentify.SelectedItem as AdultDto;
            AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            cbChildTxtReaderName.ItemsSource = AllChilds;
        }

        private void GdInputBookName_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (sender as Grid);
            if (!grid.IsEnabled)
                return;
            handle = true;
            
            AllBookNames = bookTitleMap.ConvertToDto(getBookTitleRepo().Gets());
            cbTxtBookName.ItemsSource = AllBookNames;
        }
        #endregion


        private void BdInputBookInformation_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border bd = (sender as Border);
            if (bd.IsEnabled)
            {
                gdInputBookName.IsEnabled = true;
                cbSelectLanguage.IsEnabled = false;
            }
        }

        private void CbSelectLanguage_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ComboBox cmb = (sender as ComboBox);
            if (!cmb.IsEnabled)
                return;
            if (cbTxtBookName.SelectedItem == null)
                return;
            BookTitleDto bookTitleDto = cbTxtBookName.SelectedItem as BookTitleDto;

            var bookISBNs = bookISBNVM.FillByIdBookTitle(bookTitleDto.Id, BookISBNStatusValue);

            AllLanguages = bookISBNVM.FillLanguages(bookISBNs, BookISBNStatusValue);
            cmb.ItemsSource = AllLanguages;
        }

        private void CbSelectLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.SelectedItem == null)
                return;
            string language = cmb.SelectedItem as string;

            BookTitleDto bookTitleDto = cbTxtBookName.SelectedItem as BookTitleDto;
            BookISBN isbn = bookISBNVM.Find(bookTitleDto.Id, language, false, BookISBNStatusValue);
           
            txtDisplayBookISBN.Text = isbn.ISBN;

            var books = bookVM.FillByBookISBN(isbn.ISBN, BookStatusValue);
            if (books.Count == 0)
            {
                txtDisplayBookISBNStatus.Text = "Out of Books";
                Utilities.ShowMessageBox1("This ISBN doesn't have book already. So your loanSlip will be pass to enroll data");
                PassToEnrollData(null);

                this.Close();
                return;
            }
            else if (!isbn.Status)
            {
                txtDisplayBookISBNStatus.Text = "Out of Books";
                Utilities.ShowMessageBox1("All Books of this ISBN has been borrowed already. So your loanSlip will be pass to enroll data");
                
                PassToEnrollData(FindTheLastestBook(books).Id);

                this.Close();
                return;
            }
            else
            {
                txtDisplayBookISBNStatus.Text = "Available";
            }
            ucTemporaryBooksTable.Books = bookMap.ConvertToDto(books);
        }

        private Book FindTheLastestBook(ObservableCollection<Book> books)
        {
            bool? bookStatus = null;
            string isbn = txtDisplayBookISBN.Text;
            if (getLoanSlipRepo().Gets().Count == 0)
                return null;


            var temp = getLoanSlipRepo().Gets().Where(item =>
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

        private void PassToEnrollData(int? idBook)
        {
            NewEnroll();
            Enroll.ISBN = txtDisplayBookISBN.Text;
            Enroll.IdReader = SelectedReader.Id;
            Enroll.IdBook = idBook;

            getEnrollRepo().Add(Enroll);
            getEnrollRepo().WriteAdd(Enroll);
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

        private void GetBooksFromReader()
        {
            ReaderDto reader = SelectedReader;

            var readerLoans = loanSlipVM.FillByIdReader(reader.Id, false);
            var readerBooks = bookVM.GetBooksInLoanSlips(readerLoans);
            AllBooksOfReader = readerBooks;
        }
    }
}
