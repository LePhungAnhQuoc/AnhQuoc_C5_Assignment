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
    /// Interaction logic for ucBooksTable.xaml
    /// </summary>
    public partial class ucBooksTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<BookDto>> getBooks { get; set; }
        #endregion

        #region prop-dp


        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucBooksTable), new PropertyMetadata(true));



        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucBooksTable), new PropertyMetadata(10));


        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Properties
        private BookDto _SelectedDto;
        public BookDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<BookDto> _Books;
        public ObservableCollection<BookDto> Books
        {
            get
            {
                return _Books;
            }
            set
            {
                _Books = value;
                OnPropertyChanged();

                Modified_Pagination();
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


        public ucBooksTable()
        {
            InitializeComponent();
            Loaded += UcBooksTable_Loaded;
            this.DataContext = this;
        }

        private void UcBooksTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (getBooks != null)
            {
                Books = getBooks();
            }

            if (!AllowPagination)
            {
                ucPagination.Visibility = Visibility.Collapsed;
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            btnInfoClick?.Invoke(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }

        private void Modified_Pagination()
        {
            dgBooks.ItemsSource = Books;

            if (AllowPagination == true)
            {
                ucPagination.getUcBooksTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => Books.ToObservableCollectionObj();
            }
        }
    }
}
