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
    /// Interaction logic for ucBookISBNManagement.xaml
    /// </summary>
    public partial class ucBookISBNManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<BookTitle> _BookTitles;
        public ObservableCollection<BookTitle> BookTitles
        {
            get
            {
                return _BookTitles;
            }
            set
            {
                _BookTitles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Author> _Authors;

        public ObservableCollection<Author> Authors
        {
            get { return _Authors; }
            set 
            {
                _Authors = value;
                OnPropertyChanged();
            }
        }


        private BookTitle _SelectedBookTitle;
        public BookTitle SelectedBookTitle
        {
            get { return _SelectedBookTitle; }
            set
            {
                _SelectedBookTitle = value;
                OnPropertyChanged();
            }
        }

        public bool? StatusValue { get; set; } = null;

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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));

        public bool IsAllowUpdateStatus
        {
            get { return (bool)GetValue(IsAllowUpdateStatusProperty); }
            set { SetValue(IsAllowUpdateStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdateStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateStatusProperty =
            DependencyProperty.Register("IsAllowUpdateStatus", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));

        public bool IsAllowSearchByName
        {
            get { return (bool)GetValue(IsAllowSearchByNameProperty); }
            set { SetValue(IsAllowSearchByNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByNameProperty =
            DependencyProperty.Register("IsAllowSearchByName", typeof(bool), typeof(ucBookISBNManagement), new PropertyMetadata(true));


        

        #endregion

        #endregion

        #region Fields
        private ObservableCollection<BookISBN> listFillBookISBNs;
        #endregion

        #region ViewModels
        private BookISBNViewModel bookISBNVM;
        private BookTitleViewModel bookTitleVM;
        private AuthorViewModel authorVM;
        #endregion

        #region Mapping
        private BookISBNMap bookISBNMap;
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

        public ucBookISBNManagement()
        {
            InitializeComponent();
            
            #region Allocations
            listFillBookISBNs = new ObservableCollection<BookISBN>();

            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;

            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            #endregion

            BookTitles = bookTitleVM.Repo.Gets();
            Authors = authorVM.Repo.Gets();

            btnAdd.Click += BtnAdd_Click;
            ucBookISBNsTable.btnInfoClick += UcBookISBNsTable_btnInfoClick;
            cbBookTitles.SelectionChanged += CbBookTitles_SelectionChanged;
            cbAuthors.SelectionChanged += CbAuthors_SelectionChanged;
            
            btnClearCbBookTitles.Click += btnClearCbBookTitles_Click;
            btnClearCbAuthors.Click += BtnClearCbAuthors_Click;
            this.Loaded += UcBookISBNManagement_Loaded;
            this.DataContext = this;
        }

        private void UcBookISBNManagement_Loaded(object sender, RoutedEventArgs e)
        {
            AddToListFill(getBookISBNRepo().Gets());
            AddItemsToDataGrid(listFillBookISBNs);


            #region IsAllow-Feature
            ucBookISBNsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Filter-Methods
        private void CbBookTitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
            {
                btnClearCbBookTitles.Visibility = Visibility.Hidden;
                ucBookISBNsTable.dtgTitle.Visibility = Visibility.Visible;
            }
            else
            {
                btnClearCbBookTitles.Visibility = Visibility.Visible;
                ucBookISBNsTable.dtgTitle.Visibility = Visibility.Collapsed;
            }

            Fillter();
        }

        private void CbAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
            {
                btnClearCbAuthors.Visibility = Visibility.Hidden;
            }
            else
            {
                btnClearCbAuthors.Visibility = Visibility.Visible;
            }

            Fillter();
        }


        private void Fillter()
        {
            var source = getBookISBNRepo().Gets();

            // Filter 1
            var listFill = FillByBookTitle(source);
            listFill = FillByAuthor(listFill);

            AddToListFill(listFill);
            AddItemsToDataGrid(listFillBookISBNs);
        }

        private ObservableCollection<BookISBN> FillByBookTitle(ObservableCollection<BookISBN> source)
        {
            if (cbBookTitles.SelectedIndex != -1)
            {
                var selectedItem = (BookTitle)cbBookTitles.SelectedItem;
                return bookISBNVM.FillByIdBookTitle(source, selectedItem.Id, StatusValue);
            }
            return source;
        }

        private ObservableCollection<BookISBN> FillByAuthor(ObservableCollection<BookISBN> source)
        {
            if (cbAuthors.SelectedIndex != -1)
            {
                var selectedItem = (Author)cbAuthors.SelectedItem;
                return bookISBNVM.FillByIdAuthor(source, selectedItem.Id, StatusValue);
            }
            return source;
        }

        #endregion


        private void UcBookISBNsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            BookISBNDto selectedItem = ucBookISBNsTable.SelectedDto;

            frmBookISBNInformation frmBookISBNInformation = MainWindow.UnitOfForm.FrmBookISBNInformation(true);
            frmBookISBNInformation.getItem = () => selectedItem;
            frmBookISBNInformation.Show();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddBookISBN frmAddBookISBN = MainWindow.UnitOfForm.FrmAddBookISBN(true);
            frmAddBookISBN.getIdBookISBN = () => bookISBNVM.GetId();
            frmAddBookISBN.ShowDialog();

            if (frmAddBookISBN.Context.Item == null) // Cancel the operation
            {
                return;
            }

            BookISBNDto newBookISBNDto = frmAddBookISBN.Context.Item;

            #region AddNewItem
            BookISBN newBookISBN = bookISBNVM.CreateByDto(newBookISBNDto);
            getBookISBNRepo().Add(newBookISBN);
            getBookISBNRepo().WriteAdd(newBookISBN);
            #endregion

            #region AddTo-listFill
            listFillBookISBNs.Add(newBookISBN);
            AddItemsToDataGrid(listFillBookISBNs);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("book ISBN"));
        }


        private void AddToListFill(ObservableCollection<BookISBN> items)
        {
            listFillBookISBNs.Clear();
            listFillBookISBNs.AddRange(items);
        }

        private void AddItemsToDataGrid(ObservableCollection<BookISBN> items)
        {
            var itemDtos = bookISBNMap.ConvertToDto(items);

            ucBookISBNsTable.dgDatas.ItemsSource = itemDtos;
        }

        private void btnClearCbBookTitles_Click(object sender, RoutedEventArgs e)
        {
            cbBookTitles.SelectedItem = null;
        }

        private void BtnClearCbAuthors_Click(object sender, RoutedEventArgs e)
        {
            cbAuthors.SelectedItem = null;
        }
    }
}
