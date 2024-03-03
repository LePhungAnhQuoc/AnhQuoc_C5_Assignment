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
    /// Interaction logic for ucBookTitleManagement.xaml
    /// </summary>
    public partial class ucBookTitleManagement : UserControl
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
        private ObservableCollection<BookTitle> listFillBookTitles;
        #endregion

        #region ViewModels
        private BookTitleViewModel bookTitleVM;
        private BookViewModel bookVM;
        private BookISBNViewModel bookISBNVM;
        #endregion

        #region Mapping
        private BookTitleMap bookTitleMap;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));

        public bool IsAllowUpdateStatus
        {
            get { return (bool)GetValue(IsAllowUpdateStatusProperty); }
            set { SetValue(IsAllowUpdateStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdateStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateStatusProperty =
            DependencyProperty.Register("IsAllowUpdateStatus", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));
        


        public bool IsAllowSearchByName
        {
            get { return (bool)GetValue(IsAllowSearchByNameProperty); }
            set { SetValue(IsAllowSearchByNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByNameProperty =
            DependencyProperty.Register("IsAllowSearchByName", typeof(bool), typeof(ucBookTitleManagement), new PropertyMetadata(true));


        

        #endregion

        #endregion

        public ucBookTitleManagement()
        {
            InitializeComponent();
            
            #region Allocations
            listFillBookTitles = new ObservableCollection<BookTitle>();

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;

            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            #endregion

            ucBookTitlesTable.btnInfoClick += UcBookTitlesTable_btnInfoClick;
            btnAdd.Click += BtnAdd_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += UcBookTitleManagement_Loaded;
        }

        private void UcBookTitleManagement_Loaded(object sender, RoutedEventArgs e)
        {
            var allBookTitles = getBookTitleRepo().Gets();

            AddToListFill(allBookTitles);
            AddItemsToDataGrid(listFillBookTitles);

            #region IsAllow-Feature
            ucBookTitlesTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var source = getBookTitleRepo().Gets();

            string textSearch = txtSearch.Text.ToLower();
            var results = bookTitleVM.FillContainsName(source, textSearch, true);
            
            AddToListFill(results);
            AddItemsToDataGrid(listFillBookTitles);
        }
        #endregion

        private void UcBookTitlesTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucBookTitlesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("bookTitle"));
                return;
            }

            BookTitleDto bookTitleDtoSelect = ucBookTitlesTable.SelectedBookTitleDto;
            BookTitle bookTitleSelect = bookTitleVM.FindById(bookTitleDtoSelect.Id);

            ucBookTitleInformation ucBookTitleInformation = MainWindow.UnitOfForm.UcBookTitleInformation(true);
            ucBookTitleInformation.getItem = () => bookTitleDtoSelect;

            Window frmBookTitleInformation = Utilities.CreateDefaultForm();
            frmBookTitleInformation.Content = ucBookTitleInformation;
            frmBookTitleInformation.Show();

            ucBookTitleInformation.btnBack.Click += (_sender, _e) =>
            {
                frmBookTitleInformation.Close();
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddBookTitle frmAddBookTitle = MainWindow.UnitOfForm.FrmAddBookTitle(true);
            frmAddBookTitle.getIdBookTitle = () => bookTitleVM.GetId();
            frmAddBookTitle.ShowDialog();

            if (frmAddBookTitle.Context.Item == null) // Cancel the operation
            {
                return;
            }

            BookTitleDto newBookTitleDto = frmAddBookTitle.Context.Item;

            #region AddNewItem
            BookTitle newBookTitle = bookTitleVM.CreateByDto(newBookTitleDto);
            getBookTitleRepo().Add(newBookTitle);
            getBookTitleRepo().WriteAdd(newBookTitle);
            #endregion

            #region AddTo-listFill
            listFillBookTitles.Add(newBookTitle);
            AddItemsToDataGrid(listFillBookTitles);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("book title"));
        }

        private void AddItemsToDataGrid(ObservableCollection<BookTitle> items)
        {
            var itemDtos = bookTitleMap.ConvertToDto(items);
            ucBookTitlesTable.dgDatas.ItemsSource = itemDtos;
        }

        private void AddToListFill(ObservableCollection<BookTitle> items)
        {
            listFillBookTitles.Clear();
            listFillBookTitles.AddRange(items);
        }
    }
}
