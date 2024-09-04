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

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucBookManagement.xaml
    /// </summary>
    public partial class ucBookManagement : UserControl, INotifyPropertyChanged
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

        private ObservableCollection<BookISBN> _AllBookISBNs;
        public ObservableCollection<BookISBN> AllBookISBNs
        {
            get { return _AllBookISBNs; }
            set
            { 
                _AllBookISBNs = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> _AllCategorys;
        public ObservableCollection<Category> AllCategorys
        {
            get { return _AllCategorys; }
            set 
            {
                _AllCategorys = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private BookViewModel bookVM;
        private BookISBNViewModel bookISBNVM;
        private CategoryViewModel catVM;
        #endregion

        #region Mapping
        private BookMap bookMap;
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

        #region UserControls
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
        
        #endregion

        #endregion

        public ucBookManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillBooks = new ObservableCollection<Book>();

            bookVM = UnitOfViewModel.Instance.BookViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            catVM = UnitOfViewModel.Instance.CategoryViewModel;

            bookMap = UnitOfMap.Instance.BookMap;
            #endregion

            AllBookISBNs = bookISBNVM.Repo.Gets();
            AllCategorys = catVM.Repo.Gets();

            cbBookISBNs.SelectionChanged += CbBookISBNs_SelectionChanged;
            cbCategorys.SelectionChanged += CbCategorys_SelectionChanged;
            txtSearchByBookTitle.TextChanged += TxtSearch_TextChanged;
            btnAdd.Click += BtnAdd_Click;
            
            btnClearCbBookISBN.Click += btnClearCbBookISBN_Click;
            btnClearCbCategory.Click += BtnClearCbCategory_Click;

            this.DataContext = this;
            this.Loaded += UcBookManagement_Loaded;
        }

        private void UcBookManagement_Loaded(object sender, RoutedEventArgs e)
        {
            var allBooks = getBookRepo().Gets();

            AddToListFill(allBooks);
            AddItemsToDataGrid(listFillBooks);
            
            #region IsAllow-Feature
            ucBooksTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            gdCbBookISBNs.Visibility = (IsAllowSearchByBookISBN) ? Visibility.Visible : Visibility.Collapsed;
            gdTxtSearchByBookTitle.Visibility = (IsAllowSearchByName) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Fillter-Methods
        private void CbBookISBNs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
            {
                btnClearCbBookISBN.Visibility = Visibility.Hidden;
                ucBooksTable.dtgISBN.Visibility = Visibility.Visible;
            }
            else
            {
                btnClearCbBookISBN.Visibility = Visibility.Visible;
                ucBooksTable.dtgISBN.Visibility = Visibility.Collapsed;
            }
            Fillter();
        }

        private void CbCategorys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
            {
                btnClearCbCategory.Visibility = Visibility.Hidden;
                ucBooksTable.dtgCategory.Visibility = Visibility.Visible;
            }
            else
            {
                btnClearCbCategory.Visibility = Visibility.Visible;
                ucBooksTable.dtgCategory.Visibility = Visibility.Collapsed;
            }

            Fillter();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            ObservableCollection<Book> source = getBookRepo().Gets();
            ObservableCollection<Book> results = source;

            results = FillByBookISBN(results);
            results = bookVM.FillContainsBookTitleName(results, txtSearchByBookTitle.Text, true);
            results = FillByCategory(results);

            AddToListFill(results);
            AddItemsToDataGrid(listFillBooks);
        }

        private ObservableCollection<Book> FillByBookISBN(ObservableCollection<Book> source)
        {
            if (cbBookISBNs.SelectedIndex != -1)
            {
                var selectedItem = (BookISBN)cbBookISBNs.SelectedItem;
                return bookVM.FillByBookISBN(source, selectedItem.ISBN, StatusValue);
            }
            return source;
        }

        private ObservableCollection<Book> FillByCategory(ObservableCollection<Book> source)
        {
            if (cbCategorys.SelectedIndex != -1)
            {
                var selectedItem = (Category)cbCategorys.SelectedItem;
                return bookVM.FillByCategory(source, selectedItem.Id, StatusValue);
            }
            return source;
        }

        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddBook frmAddBook = MainWindow.UnitOfForm.FrmAddBook(true);
            frmAddBook.getIdBook = () => bookVM.GetId();
            frmAddBook.ShowDialog();
         
            if (frmAddBook.Context.Item == null) // Cancel the operation
            {
                return;
            }
            var listBooks = frmAddBook.Context.BooksResult;

            #region AddNewItem
            getBookRepo().AddRange(listBooks);
            getBookRepo().WriteAddRange(listBooks);
            
            #endregion

            #region AddTo-listFill
            listFillBooks.AddRange(listBooks);
            AddItemsToDataGrid(listFillBooks);
            #endregion

            Utilitys.ShowMessageBox1(Utilitys.NotifyAddSuccessfully("book"));

            #region SaveBookISBNStatus
            BookISBN selectedBookISBN = bookISBNVM.FindByISBN(listBooks.First().ISBN, null);
            if (selectedBookISBN.Status == false)
            {
                selectedBookISBN.Status = true;
                getBookISBNRepo().WriteUpdate(selectedBookISBN);
            }
            #endregion
        }
        
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

        private void btnClearCbBookISBN_Click(object sender, RoutedEventArgs e)
        {
            cbBookISBNs.SelectedItem = null;
        }

        private void BtnClearCbCategory_Click(object sender, RoutedEventArgs e)
        {
            cbCategorys.SelectedItem = null;
        }
        #endregion
    }
}
