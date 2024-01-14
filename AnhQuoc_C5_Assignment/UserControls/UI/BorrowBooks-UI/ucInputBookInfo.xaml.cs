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
    /// Interaction logic for ucInputBookInfo.xaml
    /// </summary>
    public partial class ucInputBookInfo : UserControl, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ucAddLoan> getParentUc { get; set; }
        public Func<Stack<object>> getStoreContent { get; set; }
        public Func<LoanSlipDto> getItem { get; set; }

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
        private const double CardWidth = 220;
        private const double CardMargin = 10;
        #endregion

        #region Fields
        private bool handle;
        private ucAddLoan ucAddLoan;
        #endregion

        #region Forms
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

        public ucInputBookInfo()
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
          
            cbTxtBookName.DropDownClosed += CbTxtBookName_DropDownClosed;
            cbTxtBookName.SelectionChanged += CbTxtBookName_SelectionChanged;
            txtInputBookName.TextChanged += TxtInputBookName_TextChanged;

            gdInputBookName.IsEnabledChanged += GdInputBookName_IsEnabledChanged;

            bdInputBookInformation.IsEnabledChanged += BdInputBookInformation_IsEnabledChanged;
            #endregion

            this.Loaded += ucInputBookInfo_Loaded;
            this.DataContext = this;
        }

        private void ucInputBookInfo_Loaded(object sender, RoutedEventArgs e)
        {
            ucAddLoan = getParentUc();
            ucAddLoan.AllBookISBNCard = new ObservableCollection<ucBookISBNCard>();
            ucAddLoan.AllLoanDetailCard = new ObservableCollection<ucLoanDetailCard>();


            NewItem();

            ucAddLoan.LoanDetails = new ObservableCollection<LoanDetail>();
            ucAddLoan.SelectedReaderType = ReaderType.Adult;
            ucAddLoan.AllBookNames = bookTitleMap.ConvertToDto(getBookTitleRepo().Gets());
            cbTxtBookName.ItemsSource = ucAddLoan.AllBookNames;
        }


        private void NewItem()
        {
            LoanSlipDto = getItem();
        }

        private void NewDetail()
        {
            int indexId = ucAddLoan.LoanDetails.Count + getLoanDetailRepo().Count;

            ucAddLoan.LoanDetail = new LoanDetail();
            ucAddLoan.LoanDetail.Id = loanDetailVM.GetId(indexId);

            ucAddLoan.LoanDetail.IdLoan = LoanSlipDto.Id;
            ucAddLoan.LoanDetail.ExpDate = LoanSlipDto.ExpDate;
        }
        
        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ucAddLoan.LoanDetails.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't chosen any book yet");
                return;
            }
            getParentUc().CloseAndSave();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = getStoreContent().Pop();
        }

        private void BtnBookDetailConfirm_Click(object sender, RoutedEventArgs e)
        {
            BookDto bookDto = ucAddLoan.SelectedBook;
            var tempCheck = bookVM.FindById(getParentUc().BooksOfReader, bookDto.Id, null);
            if (tempCheck == null)
                tempCheck = bookVM.FindById(bookVM.GetBooksInLoanDetails(ucAddLoan.LoanDetails), bookDto.Id, null);
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
                ucAddLoan.LoanDetail.IdBook = bookDto.Id;
                
                ucAddLoan.LoanDetails.Add(ucAddLoan.LoanDetail);

                ConvertToLoanDetailCard(ucAddLoan.LoanDetails);
                AddLoanDetailCardToWrap();

                ucAddLoan.AllBookISBNCard.Remove(ucAddLoan.AllBookISBNCard.FirstOrDefault(item => item.getItem().ISBN == ucAddLoan.SelectedISBN.ISBN));
                ucAddLoan.AllBookISBN.Remove(ucAddLoan.AllBookISBN.FirstOrDefault(item => item.ISBN == ucAddLoan.
                SelectedISBN.ISBN));

                AddBookISBNCardToWrap();
            }
        }


        private void PassValueToItem(LoanSlip item)
        {
            item.Id = LoanSlipDto.Id;
            item.IdUser = LoanSlipDto.IdUser;
            item.IdReader = ucAddLoan.SelectedReader.Id;
            item.Quantity = ucAddLoan.LoanDetails.Count;
            item.LoanDate = LoanSlipDto.LoanDate;
            item.ExpDate = LoanSlipDto.ExpDate;
            item.LoanPaid = LoanSlipDto.LoanPaid;
            item.Deposit = LoanSlipDto.Deposit;
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
            TxtInputBookName_Filter_TextChanged(txtInputBookName, gdInputBookName, ucAddLoan.AllBookNames);
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

                if (cbTxtBookName.SelectedItem == null)
                    return;
                BookTitleDto bookTitleDto = cbTxtBookName.SelectedItem as BookTitleDto;

                ucAddLoan.AllBookISBN = bookISBNVM.FillByIdBookTitle(bookTitleDto.Id, ucAddLoan.BookISBNStatusValue);
                ucAddLoan.AllBookISBNCard.Clear();
                ConvertToBookISBNCard(ucAddLoan.AllBookISBN);
                AddBookISBNCardToWrap();

                return;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion

        // Others methods

        #region GetsDatas
        private void GdInputBookName_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (sender as Grid);
            if (!grid.IsEnabled)
                return;
            handle = true;

       
        }
        #endregion


        private void BdInputBookInformation_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border bd = (sender as Border);
            if (bd.IsEnabled)
            {
                gdInputBookName.IsEnabled = true;
            }
        }

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

        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in ucAddLoan.LoanDetails)
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
        

        private void ConvertToBookISBNCard(ObservableCollection<BookISBN> bookISBNs)
        {
            ucAddLoan.AllBookISBNCard.Clear();
            foreach (var isbn in bookISBNs)
            {
                ucBookISBNCard ucBookISBNCard = new ucBookISBNCard();
                ucBookISBNCard.Width = CardWidth;
                ucBookISBNCard.Margin = new Thickness(CardMargin);
                ucBookISBNCard.MouseLeftButtonDown += UcBookISBNCard_MouseLeftButtonDown;
                ucBookISBNCard.getItem = () => bookISBNMap.ConvertToDto(isbn);
                ucAddLoan.AllBookISBNCard.Add(ucBookISBNCard);
            }
        }

        private void ConvertToLoanDetailCard(ObservableCollection<LoanDetail> loanDetails)
        {
            ucAddLoan.AllLoanDetailCard.Clear();
            foreach (var isbn in loanDetails)
            {
                ucLoanDetailCard ucLoanDetailCard = new ucLoanDetailCard();
                ucLoanDetailCard.Width = CardWidth;
                ucLoanDetailCard.Margin = new Thickness(CardMargin);
                ucLoanDetailCard.getItem = () => loanDetailMap.ConvertToDto(isbn);
                ucAddLoan.AllLoanDetailCard.Add(ucLoanDetailCard);
            }
        }


        private void AddBookISBNCardToWrap()
        {
            wrapBookISBN.Children.Clear();
            foreach (var ucCard in ucAddLoan.AllBookISBNCard)
            {
                wrapBookISBN.Children.Add(ucCard);
            }
        }

        private void AddLoanDetailCardToWrap()
        {
            wrapLoanDetail.Children.Clear();
            foreach (var ucCard in ucAddLoan.AllLoanDetailCard)
            {
                wrapLoanDetail.Children.Add(ucCard);
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
            BtnBookDetailConfirm_Click(null, null);
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
