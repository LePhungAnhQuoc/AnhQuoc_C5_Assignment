using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for ucBookManagement.xaml
    /// </summary>
    public partial class ucBookManagement : UserControl
    {
        #region GetDatas
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private ObservableCollection<Book> listFillBooks;
        #endregion

        #region Properties
        public bool? StatusValue { get; set; } = null;
        #endregion

        #region ViewModels
        private BookViewModel bookVM;
        private BookISBNViewModel bookISBNVM;
        #endregion

        #region Mapping
        private BookMap bookMap;
        #endregion

        #region UserControls
        private ucAddBook ucAddBook;
        #endregion

        #region prop-Dp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));

        public bool IsAllowUpdateStatus
        {
            get { return (bool)GetValue(IsAllowUpdateStatusProperty); }
            set { SetValue(IsAllowUpdateStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdateStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateStatusProperty =
            DependencyProperty.Register("IsAllowUpdateStatus", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));


        public bool IsAllowSearchByBookISBN
        {
            get { return (bool)GetValue(IsAllowSearchByBookISBNProperty); }
            set { SetValue(IsAllowSearchByBookISBNProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByBookISBN.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByBookISBNProperty =
            DependencyProperty.Register("IsAllowSearchByBookISBN", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));


        public bool IsAllowSearchByName
        {
            get { return (bool)GetValue(IsAllowSearchByNameProperty); }
            set { SetValue(IsAllowSearchByNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByNameProperty =
            DependencyProperty.Register("IsAllowSearchByName", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));



        public bool IsAllowBorrowBook
        {
            get { return (bool)GetValue(IsAllowBorrowBookProperty); }
            set { SetValue(IsAllowBorrowBookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowBorrowBook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowBorrowBookProperty =
            DependencyProperty.Register("IsAllowBorrowBook", typeof(bool), typeof(ucBookManagement), new PropertyMetadata(true));



        #endregion

        #endregion

        public ucBookManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillBooks = new ObservableCollection<Book>();

            bookVM = UnitOfViewModel.Instance.BookViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;

            bookMap = UnitOfMap.Instance.BookMap;
            #endregion

            ucBooksTable.btnDeleteClick += UcBooksTable_btnDeleteClick;
            cbBookISBNs.SelectionChanged += CbBookISBNs_SelectionChanged;
            txtSearchByName.TextChanged += TxtSearch_TextChanged;
            btnAdd.Click += BtnAdd_Click;
            btnClearComboBox.Click += BtnClearComboBox_Click;
            
            #region Add-UserControl
            ucAddBook = MainWindow.UnitOfForm.UcAddBook(true);
            ucAddBook.Visibility = Visibility.Collapsed;
            ucAddBook.getUcBookManagement = () => this;

            stkFeatures.Children.Add(ucAddBook);
            #endregion

            this.Loaded += UcBookManagement_Loaded;
        }

        private void UcBookManagement_Loaded(object sender, RoutedEventArgs e)
        {
            var allBooks = getBookRepo().Gets();

            AddToListFill(allBooks);
            AddItemsToDataGrid(listFillBooks);

            cbBookISBNs.ItemsSource = getBookISBNRepo().Gets();
            
            btnClearComboBox.Visibility = Visibility.Hidden;

            #region IsAllow-Feature
            ucBooksTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            gdCbBookISBNs.Visibility = (IsAllowSearchByBookISBN) ? Visibility.Visible : Visibility.Collapsed;
            gdTxtSearchByName.Visibility = (IsAllowSearchByName) ? Visibility.Visible : Visibility.Collapsed;
            bdBorrowBook.Visibility = (IsAllowBorrowBook) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Fillter-Methods
        private void CbBookISBNs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fillter();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            ObservableCollection<Book> source = getBookRepo().Gets();
            ObservableCollection<Book> results = null;

            results = FillByComboBoxBookISBN(source);
            results = FillByTxtSearchByName(results);

            AddToListFill(results);
            AddItemsToDataGrid(listFillBooks);
        }

        private ObservableCollection<Book> FillByComboBoxBookISBN(ObservableCollection<Book> source)
        {
            BookISBN selectedISBN = null;
            try
            {
                if (cbBookISBNs.SelectedIndex != -1)
                {
                    selectedISBN = (BookISBN)cbBookISBNs.SelectedItem;
                    btnClearComboBox.Visibility = Visibility.Visible;
                }
                else
                {
                    selectedISBN = null;
                }
            }
            catch
            {
            }
            if (selectedISBN == null)
            {
                return source;
            }
            else
            {
                BookViewModel viewModel = new BookViewModel();
                viewModel.Repo = new BookRepository(source);
                return viewModel.FillByBookISBN(selectedISBN.ISBN, StatusValue);
            }
        }

        private ObservableCollection<Book> FillByTxtSearchByName(ObservableCollection<Book> source)
        {
            string textSearch = txtSearchByName.Text.ToLower();

            ObservableCollection<Book> results = new ObservableCollection<Book>();
            foreach (Book item in source)
            {
                BookDto itemDto = bookMap.ConvertToDto(item);
                bool isCheck = itemDto.Title.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void UcBooksTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            bool? bookStatusBorrow = true;
            bool? bookISBNStatusValue = null;

            bool updateStatus = false;
            if (ucBooksTable.dgBooks.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                return;
            }
            var message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;

            #region UpdateBookStatus
            BookDto bookDtoSelect = ucBooksTable.SelectedDto;
            Book bookSelect = bookVM.FindById(bookDtoSelect.Id, !updateStatus);

            bookSelect.Status = updateStatus;
            getBookRepo().WriteUpdateStatus(bookSelect, updateStatus);
            #endregion

            #region UpdateBookISBNStatus
            BookISBN bookISBNFinded = bookISBNVM.FindByISBN(bookSelect.ISBN, bookISBNStatusValue);
            var listFillISBN = bookVM.FillByBookISBN(bookISBNFinded.ISBN, bookStatusBorrow);
            listFillISBN = bookVM.FillByStatus(listFillISBN, true);
            if (listFillISBN.Count == 0)
            {
                bookISBNFinded.Status = false;
                getBookISBNRepo().WriteUpdateStatus(bookISBNFinded, false);
            }
            #endregion

            AddItemsToDataGrid(listFillBooks);
        }

        #region Add-New-Books-Methods
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SetAddStatus(0);
        }

        public void AddNewBooks(ObservableCollection<Book> getNewBooks)
        {
            #region SaveBook
            getBookRepo().AddRange(getNewBooks);
            getBookRepo().WriteAddRange(getNewBooks);
            #endregion

            #region AddToListFill
            listFillBooks.AddRange(getNewBooks);
            AddItemsToDataGrid(listFillBooks);
            #endregion

            #region SaveBookISBNStatus
            BookISBN selectedBookISBN = ucAddBook.SelectedBookISBN;
            if (selectedBookISBN.Status == false)
            {
                selectedBookISBN.Status = true;
                getBookISBNRepo().WriteUpdateStatus(selectedBookISBN, true);
            }
            #endregion

            SetAddStatus(1);
        }

        public void CancelAddNewBooks()
        {
            SetAddStatus(1);
        }

        private void SetAddStatus(int status)
        {
            switch (status)
            {
                case 0:
                    borderBtnAdd.Visibility = Visibility.Collapsed;
                    ucAddBook.Visibility = Visibility.Visible;
                    break;
                case 1:
                    borderBtnAdd.Visibility = Visibility.Visible;
                    ucAddBook.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        #endregion

        #region Others
        private void AddToListFill(ObservableCollection<Book> items)
        {
            listFillBooks.Clear();
            listFillBooks.AddRange(items);
        }

        private void AddItemsToDataGrid(ObservableCollection<Book> items)
        {
            var itemDtos = bookMap.ConvertToDto(items);

            ucBooksTable.Books = itemDtos;
        }

        private void BtnClearComboBox_Click(object sender, RoutedEventArgs e)
        {
            cbBookISBNs.SelectedItem = null;
            btnClearComboBox.Visibility = Visibility.Hidden;
        }
        #endregion

        private void btnBorrowBook_Click(object sender, RoutedEventArgs e)
        {
            frmBorrowBook frmBorrowBook = MainWindow.UnitOfForm.FrmBorrowBook(true);
            frmBorrowBook.ShowDialog();
        }
    }
}
