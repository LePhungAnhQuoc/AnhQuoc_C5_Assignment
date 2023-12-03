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
    /// Interaction logic for ucReadersTable.xaml
    /// </summary>
    public partial class ucReadersTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<bool?> getItem_Status { get; set; }
        public Func<ObservableCollection<Reader>> getReaders { get; set; }
        public Func<List<PropertyInfo>> getExceptProperties { get; set; }
        #endregion

        #region Propdps
        
        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucReadersTable), new PropertyMetadata(true));
        
        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucReadersTable), new PropertyMetadata(10));



        #endregion

        #region Fields
        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler btnUpdateClick;
        public event RoutedEventHandler btnRestoreClick;
        public event RoutedEventHandler btnTransferGuardian;
        #endregion

        #region Properties
        private ObservableCollection<ReaderDto> _ReaderDtos;
        public ObservableCollection<ReaderDto> ReaderDtos
        {
            get
            {
                return _ReaderDtos;
            }
            set
            {
                _ReaderDtos = value;
                OnPropertyChanged();
            }
        }

        private ReaderDto _SelectedReaderDto;
        public ReaderDto SelectedReaderDto
        {
            get
            {
                return _SelectedReaderDto;
            }
            set
            {
                _SelectedReaderDto = value;
                OnPropertyChanged();
            }
        }
      
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        #endregion

        #region ViewModels
        public ReaderViewModel readerVM { get; set; }
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

        public ucReadersTable()
        {
            InitializeComponent();

            #region Allocations
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            readerMap = UnitOfMap.Instance.ReaderMap;
            #endregion

            dgReaders.MouseDoubleClick += DgReaders_MouseDoubleClick;

            this.Loaded += UcReadersTable_Loaded;
            this.DataContext = this;    
        }
        
        private void UcReadersTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnInfoClick == null)
            {
                dtgbtnInfo.Visibility = Visibility.Collapsed;
            }
            if (btnUpdateClick == null)
            {
                dtgbtnUpdate.Visibility = Visibility.Collapsed;
            }
            if (btnRestoreClick == null)
            {
                dtgbtnRestore.Visibility = Visibility.Collapsed;
            }
            if (btnTransferGuardian == null)
            {
                dtgbtnTransferGuardian.Visibility = Visibility.Collapsed;
            }

            if (getExceptProperties != null)
            {
                Utilities.SetExceptPropertiesForDataGrid(dgReaders, getExceptProperties());
            }

            if (getReaders != null)
            {
                ModifiedPagination();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateClick?.Invoke(sender, e);
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            btnRestoreClick?.Invoke(sender, e);
        }

        private void btnTransferGuardian_Click(object sender, RoutedEventArgs e)
        {
            btnTransferGuardian?.Invoke(sender, e);
        }

        private void DgReaders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(dgReaders, null);
        }

        public void ModifiedPagination()
        {
            var allReaders = getReaders();
            var listFilledReader = readerVM.FillByStatus(allReaders, getItem_Status());

            ReaderDtos = readerMap.ConvertToDto(listFilledReader);
            dgReaders.ItemsSource = ReaderDtos;

            if (AllowPagination == true)
            {
                ucPagination.getUcReadersTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => ReaderDtos.ToObservableCollectionObj();
            }
        }
    }
}
