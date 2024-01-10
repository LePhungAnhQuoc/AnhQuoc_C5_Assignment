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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucSelectReaderInfo.xaml
    /// </summary>
    public partial class ucSelectReaderInfo : UserControl, INotifyPropertyChanged
    {
        #region getDatas
        public Func<LoanSlipDto> getItem { get; set; }
        public Func<Stack<object>> getStoreContent { get; set; }
        public Func<ucAddLoan> getParentUc { get; set; }

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
        #endregion

        #region Constants
        private const int CardWidth = 300;
        #endregion

        #region Fields
        private bool handle;
        private ucAddLoan ucAddLoan;
        #endregion

        #region Forms
        private ucInputBookInfo ucInputBookInfo;
        #endregion

        #region Properties
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

        public ucSelectReaderInfo()
        {
            InitializeComponent();
        
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

            #region Register-Event
            btnConfirm.Click += BtnConfirm_Click;
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

          
            gdInputChildName.IsEnabledChanged += GdInputChildName_IsEnabledChanged;

            #endregion

            this.Loaded += ucSelectReaderInfo_Loaded;
            this.DataContext = this;
        }

        private void ucSelectReaderInfo_Loaded(object sender, RoutedEventArgs e)
        {
            ucAddLoan = getParentUc();

            ucAddLoan.AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();
            AllReaderTypes = ucAddLoan.AllReaderTypes;

            NewItem();

            ucAddLoan.SelectedReaderType = ReaderType.Adult;
            SelectedReaderType = ucAddLoan.SelectedReaderType;

            stkAdultInformation.Visibility = Visibility.Visible;
            stkChildInformation.Visibility = Visibility.Collapsed;
        }


        private void NewItem()
        {
            LoanSlipDto = getItem();
        }
        

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ucInputBookInfo = MainWindow.UnitOfForm.UcInputBookInfo(true);
            ucInputBookInfo.getItem = getItem;
            ucInputBookInfo.getStoreContent = getStoreContent;
            ucInputBookInfo.getParentUc = getParentUc;

            getStoreContent().Push(this.Content);
            this.Content = ucInputBookInfo;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = getStoreContent().Pop();
        }
        
        private bool IsAllSelecting()
        {
            if (ucAddLoan.SelectedReader == null)
            {
                Utilities.ShowMessageBox1("Please select reader");
                return false;
            }
            return true;
        }
        

      

        private bool HandleAdult(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((AdultDto)cmb.SelectedItem).Identify;
            return true;
        }

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
            TxtInputIdentify_Filter_TextChanged(txtAdultInputIdentify, gdInputAdultIndentify, ucAddLoan.AllAdults);
        }

        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, ucAddLoan.StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = readerVM.FindById(getfillList.First().IdReader);
                ucAddLoan.SelectedReader = readerMap.ConvertToDto(reader);
                SelectedReader = ucAddLoan.SelectedReader;

                GetBooksFromReader();
                GetReaderLoanAndLoanDetails();
                ucLoanDetailsBorrowedTable.getLoanDetails = () => ucAddLoan.AllAdultLoanDetail;
                ucLoanDetailsBorrowedTable.ModifiedPagination();

                return;
            }
            else
            {
                ucAddLoan.SelectedReader = null;
                SelectedReader = ucAddLoan.SelectedReader;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
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

        private void GetAllChildOfAdult(AdultDto adultDto)
        {
            ucAddLoan.AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            txtChildInputReaderName.Text = string.Empty;
            cbChildTxtReaderName.ItemsSource = ucAddLoan.AllChilds;
        }

        private void TxtGuardianInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(txtGuardianInputIdentify, gdGuardianInput, ucAddLoan.AllGuardians);
        }

        private void TxtInputGuardianIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, ucAddLoan.StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Adult guardian = adultVM.FindByIdentify(getfillList.First().Identify, null);
                AdultDto guardianDto = adultMap.ConvertToDto(guardian);

                GetAllChildOfAdult(guardianDto);

                gdInputChildName.IsEnabled = true;
                return;
            }
            else
            {
                gdInputChildName.IsEnabled = false;
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
            TxtInputChildReaderName_Filter_TextChanged(txtChildInputReaderName, gdInputChildName, ucAddLoan.AllChilds);
        }

        private void TxtInputChildReaderName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<ChildDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Child> getfillList = childVM.FillContainsFullName(source, txtInput.Text, ignoreCase, ucAddLoan.StatusValue);
            ObservableCollection<ChildDto> getfillListDto = childMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().FullName == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);
                ucAddLoan.SelectedReader = readerMap.ConvertToDto(reader);
                SelectedReader = ucAddLoan.SelectedReader;
                GetBooksFromReader();

                GetReaderLoanAndLoanDetails();
                ucLoanDetailsBorrowedTable.getLoanDetails = () => ucAddLoan.AllChildLoanDetail;
                ucLoanDetailsBorrowedTable.ModifiedPagination();
                return;
            }
            else
            {
                ucAddLoan.SelectedReader = null;
                SelectedReader = ucAddLoan.SelectedReader;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion

        // Others methods

        #region GetsDatas
        private void CbSelectReaderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectReaderType.SelectedItem == null)
                return;
            var readerType = (ReaderType)cbSelectReaderType.SelectedItem;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                ucAddLoan.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoan.StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoan.FillReaderByType);
                var adults = adultVM.GetListFromReaders(ucAddLoan.FillReaderByType, ucAddLoan.StatusValue);

                ucAddLoan.AllAdults = adultMap.ConvertToDto(adults);
                cbAdultTxtIdentify.ItemsSource = ucAddLoan.AllAdults;

                stkAdultInformation.Visibility = Visibility.Visible;
                stkChildInformation.Visibility = Visibility.Collapsed;
            }
            else
            {
                handle = true;
                ucAddLoan.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoan.StatusValue);


                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoan.FillReaderByType);
                var childs = childVM.GetListFromReaders(ucAddLoan.FillReaderByType, ucAddLoan.StatusValue);
                var childDTOs = childMap.ConvertToDto(childs);


                var adults = adultVM.GetListFromChilds(childs, ucAddLoan.StatusValue);

                ucAddLoan.AllGuardians = adultMap.ConvertToDto(adults);
                cbGuardianTxtIdentify.ItemsSource = ucAddLoan.AllGuardians;

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
        }
        #endregion

        
        private Book FindTheLastestBook(ObservableCollection<Book> books)
        {
            bool? bookStatus = null;
            string isbn = ucAddLoan.SelectedISBN.ISBN;
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
        
        private void GetBooksFromReader()
        {
            ReaderDto reader = ucAddLoan.SelectedReader;

            var readerLoans = loanSlipVM.FillByIdReader(reader.Id, false);
            var readerBooks = bookVM.GetBooksInLoanSlips(readerLoans);
            ucAddLoan.BooksOfReader = readerBooks;
        }

        private void GetChildrenFromAdult(Adult adult)
        {
            ucAddLoan.AllChildOfAdult = childVM.FillByIdAdult(adult.IdReader, null);
        }

        private void GetChildBooks()
        {
            ucAddLoan.AllChildLoan = loanSlipVM.GetByListReader(ucAddLoan.AllChildOfAdult.Select(child => readerVM.FindById(child.IdReader)).ToObservableCollection());
            ucAddLoan.AllChildLoanDetail = loanDetailVM.FillListByIdLoans(ucAddLoan.AllChildLoan);
            ucAddLoan.AllChildBook = bookVM.GetBooksInLoanDetails(ucAddLoan.AllChildLoanDetail);
        }

        private void GetReaderLoanAndLoanDetails()
        {
            if (ucAddLoan.SelectedReaderType == ReaderType.Adult)
            {
                ucAddLoan.AllAdultLoan = loanSlipVM.FillByIdReader(ucAddLoan.SelectedReader.Id);
                ucAddLoan.AllAdultLoanDetail = loanDetailVM.FillListByIdLoans(ucAddLoan.AllAdultLoan);
            }
            else if (ucAddLoan.SelectedReaderType == ReaderType.Child)
            {
                ucAddLoan.AllChildLoan = loanSlipVM.FillByIdReader(ucAddLoan.SelectedReader.Id);
                ucAddLoan.AllChildLoanDetail = loanDetailVM.FillListByIdLoans(ucAddLoan.AllChildLoan);
            }
        }

        private void ConvertToBookISBNCard(ObservableCollection<BookISBN> bookISBNs)
        {
            ucAddLoan.AllBookISBNCard.Clear();
            foreach (var isbn in bookISBNs)
            {
                ucBookISBNCard ucBookISBNCard = new ucBookISBNCard();
                ucBookISBNCard.Width = CardWidth;
                ucBookISBNCard.Margin = new Thickness(10);
                ucBookISBNCard.MouseLeftButtonDown += UcBookISBNCard_MouseLeftButtonDown;
                ucBookISBNCard.getItem = () => bookISBNMap.ConvertToDto(isbn);
                ucAddLoan.AllBookISBNCard.Add(ucBookISBNCard);
            }
        }

        private void UcBookISBNCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookISBNCard ucBookISBNCard = sender as ucBookISBNCard;
            ucAddLoan.SelectedISBN = ucBookISBNCard.getItem();
            BookISBN isbn = bookISBNVM.FindByISBN(ucAddLoan.SelectedISBN.ISBN, null);

            var books = bookVM.FillByBookISBN(isbn.ISBN, ucAddLoan.BookStatusValue);
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

            if (ucAddLoan.SelectedBook == null) // User click cancel button
            {
                return;
            }
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
                ucAddLoan.SelectedBook = ucSelectBooksTable.SelectedDto;
                if (ucAddLoan.SelectedBook == null)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                    return;
                }
                frmSelectBooksTable.Close();
            };
            btnCancelSelectBook.Click += (_sender, _e) =>
            {
                frmSelectBooksTable.Close();
                ucAddLoan.SelectedBook = null;
            };

            Utilities.AddItemToFormDefault(frmSelectBooksTable, ucSelectBooksTable, btnConfirmSelectBook, btnCancelSelectBook);
            frmSelectBooksTable.ShowDialog();
        }
    }
}
