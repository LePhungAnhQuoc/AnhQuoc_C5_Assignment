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
    /// Interaction logic for ucLoanDetailHistorysTable.xaml
    /// </summary>
    public partial class ucLoanDetailHistorysTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        private Func<ObservableCollection<LoanDetailHistory>> _getLoanDetailHistorys;
        public Func<ObservableCollection<LoanDetailHistory>> getLoanDetailHistorys
        {
            get
            {
                return _getLoanDetailHistorys;
            }
            set
            {
                _getLoanDetailHistorys = value;
            }
        }

        public Func<string[]> getExceptProperties { get; set; }
        #endregion

        #region prop-dp


        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucLoanDetailHistorysTable), new PropertyMetadata(true));



        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucLoanDetailHistorysTable), new PropertyMetadata(10));


        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Properties
        private LoanDetailHistoryDto _SelectedDto;
        public LoanDetailHistoryDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<LoanDetailHistoryDto> _LoanDetailHistorys;
        public ObservableCollection<LoanDetailHistoryDto> LoanDetailHistorys
        {
            get
            {
                return _LoanDetailHistorys;
            }
            set
            {
                _LoanDetailHistorys = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Mapping
        private LoanDetailHistoryMap LoanDetailHistoryMap;
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


        public ucLoanDetailHistorysTable()
        {
            InitializeComponent();
            LoanDetailHistoryMap = UnitOfMap.Instance.LoanDetailHistoryMap;
            Loaded += ucLoanDetailHistorysTable_Loaded;
            this.DataContext = this;
        }

        private void ucLoanDetailHistorysTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }

            if (btnInfoClick == null)
            {
                dtgBtnInfo.Visibility = Visibility.Collapsed;
            }

            if (!AllowPagination)
            {
                ucPagination.Visibility = Visibility.Collapsed;
            }

            if (getExceptProperties != null)
            {
                Utilities.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
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

        public void ModifiedPagination()
        {
            var allDatas = getLoanDetailHistorys();

            var listFilledStatus = allDatas;
            LoanDetailHistorys = LoanDetailHistoryMap.ConvertToDto(listFilledStatus);

            dgDatas.ItemsSource = LoanDetailHistorys;

            if (AllowPagination == true)
            {
                ucPagination.getUcLoanDetailHistorysTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => LoanDetailHistorys.ToObservableCollectionObj();
            }
        }
    }
}
