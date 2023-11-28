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
    /// Interaction logic for ucBookISBNsTable.xaml
    /// </summary>
    public partial class ucBookISBNsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<BookISBN>> getBookISBNs { get; set; }
        #endregion

        #region Fields
        private BookISBNMap bookISBNMap;
        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Properties
        private ObservableCollection<BookISBNDto> _BookISBNDtos;
        public ObservableCollection<BookISBNDto> BookISBNDtos
        {
            get
            {
                return _BookISBNDtos;
            }
            set
            {
                _BookISBNDtos = value;
                OnPropertyChanged();
            }
        }

        private BookISBNDto _SelectedDto;
        public BookISBNDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
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


        public ucBookISBNsTable()
        {
            InitializeComponent();
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;

            Loaded += ucBookISBNsTable_Loaded;
            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;

            this.DataContext = this;
        }

        private void DgDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(null, null);
        }

        private void ucBookISBNsTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (getBookISBNs != null)
            {
                var allBookISBNs = getBookISBNs();
                var allBookISBNDtos = bookISBNMap.ConvertToDto(allBookISBNs);
                BookISBNDtos = allBookISBNDtos;
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
    }
}
