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
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            #endregion
            
            btnAdd.Click += BtnAdd_Click;
            ucBookISBNsTable.btnInfoClick += UcBookISBNsTable_btnInfoClick;
            cbBookTitles.SelectionChanged += CbBookTitles_SelectionChanged;
            btnClearComboBox.Click += BtnClearComboBox_Click;

            this.Loaded += UcBookISBNManagement_Loaded;
            this.DataContext = this;
        }

        private void UcBookISBNManagement_Loaded(object sender, RoutedEventArgs e)
        {
            AddToListFill(getBookISBNRepo().Gets());
            AddItemsToDataGrid(listFillBookISBNs);

            BookTitles = getBookTitleRepo().Gets();

            btnClearComboBox.Visibility = Visibility.Hidden;

            #region IsAllow-Feature
            ucBookISBNsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Filter-Methods
        private void CbBookTitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var source = getBookISBNRepo().Gets();

            // Filter 1
            AddToListFill(FillByBookTitle(source));

            AddItemsToDataGrid(listFillBookISBNs);
        }

        private ObservableCollection<BookISBN> FillByBookTitle(ObservableCollection<BookISBN> source)
        {
            BookTitle selectedTitle = null;
            try
            {
                if (cbBookTitles.SelectedIndex != -1)
                {
                    selectedTitle = (BookTitle)cbBookTitles.SelectedItem;
                    btnClearComboBox.Visibility = Visibility.Visible;
                }
                else
                {
                    selectedTitle = null;
                }
            }
            catch
            {
            }
            if (selectedTitle == null)
            {
                return source;
            }
            else
            {
                BookISBNViewModel viewModel = new BookISBNViewModel();
                viewModel.Repo = new BookISBNRepository(source);

                var listFillDto_Temp = viewModel.FillByIdBookTitle(selectedTitle.Id, StatusValue);
                return listFillDto_Temp;
            }
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
            ucAddBookISBN ucAddBookISBN = MainWindow.UnitOfForm.UcAddBookISBN(true);
            Window frmAddBookISBN = Utilities.CreateDefaultForm();
            frmAddBookISBN.Content = ucAddBookISBN;

            ucAddBookISBN.btnConfirm.Click += (_sender, _e) =>
            {
                if (ucAddBookISBN.IsCheckValidForm)
                    frmAddBookISBN.Close();
            };
            ucAddBookISBN.btnCancel.Click += (_sender, _e) =>
            {
                frmAddBookISBN.Close();
            };

            frmAddBookISBN.ShowDialog();

            if (ucAddBookISBN.Item == null)
                return;

            BookISBN newItem = bookISBNVM.CreateByDto(ucAddBookISBN.Item);
            // Add to listFilled
            listFillBookISBNs.Add(newItem);
            AddItemsToDataGrid(listFillBookISBNs);
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

        private void BtnClearComboBox_Click(object sender, RoutedEventArgs e)
        {
            cbBookTitles.SelectedItem = null;
            btnClearComboBox.Visibility = Visibility.Hidden;
        }
    }
}
