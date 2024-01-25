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
            var results = FillByTextSearch(getBookTitleRepo().Gets());
            AddToListFill(results);
            AddItemsToDataGrid(listFillBookTitles);
        }

        private ObservableCollection<BookTitle> FillByTextSearch(ObservableCollection<BookTitle> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<BookTitle> results = new ObservableCollection<BookTitle>();
            foreach (BookTitle itemDto in source)
            {
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(itemDto);
                }
            }
            return results;
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ucAddBookTitle ucAddBookTitle = MainWindow.UnitOfForm.UcAddBookTitle(true);
            ucAddBookTitle.getId = () => bookTitleVM.GetId();

            Window frmAddBookTitle = Utilities.CreateDefaultForm();
           
            ucAddBookTitle.btnConfirm.Click += (_sender, _e) =>
            {
                if (ucAddBookTitle.IsCheckValidForm)
                    frmAddBookTitle.Close();
            };
            ucAddBookTitle.btnCancel.Click += (_sender, _e) =>
            {
                frmAddBookTitle.Close();
            };

            StackPanel stkPanel = new StackPanel();
            stkPanel.Children.Add(ucAddBookTitle);
            frmAddBookTitle.Content = stkPanel;

            frmAddBookTitle.ShowDialog();

            if (ucAddBookTitle.Item == null)
                return;
            
            #region AddToListFill
            listFillBookTitles.Add(ucAddBookTitle.Item);
            AddItemsToDataGrid(listFillBookTitles);
            #endregion
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
