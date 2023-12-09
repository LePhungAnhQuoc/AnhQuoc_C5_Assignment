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
    /// Interaction logic for ucLoanSlipsTable.xaml
    /// </summary>
    public partial class ucLoanSlipsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<bool?> getItem_Status { get; set; }

        private Func<ObservableCollection<LoanSlip>> _getLoanSlips;
        public Func<ObservableCollection<LoanSlip>> getLoanSlips
        {
            get
            {
                return _getLoanSlips;
            }
            set
            {
                _getLoanSlips = value;
            }
        }
        public Func<List<PropertyInfo>> getExceptProperties { get; set; }
        #endregion

        #region prop-dp
        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucLoanSlipsTable), new PropertyMetadata(true));



        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucLoanSlipsTable), new PropertyMetadata(10));


        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Mappping
        private LoanSlipMap loanSlipMap;
        #endregion

        #region Properties
        private LoanSlipDto _SelectedDto;
        public LoanSlipDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<LoanSlipDto> _LoanSlips;
        public ObservableCollection<LoanSlipDto> LoanSlips
        {
            get
            {
                return _LoanSlips;
            }
            set
            {
                _LoanSlips = value;
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


        public ucLoanSlipsTable()
        {
            InitializeComponent();

            loanSlipMap = UnitOfMap.Instance.LoanSlipMap;
            Loaded += ucLoanSlipsTable_Loaded;
            this.DataContext = this;
        }

        private void ucLoanSlipsTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
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
            var allDatas = getLoanSlips();

            var listFilledStatus = allDatas;
            LoanSlips = loanSlipMap.ConvertToDto(listFilledStatus);

            dgDatas.ItemsSource = LoanSlips;

            if (AllowPagination == true)
            {
                ucPagination.getUcLoanSlipsTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => LoanSlips.ToObservableCollectionObj();
            }
        }
    }
}
