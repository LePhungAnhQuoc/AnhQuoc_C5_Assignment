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
    /// Interaction logic for ucLoanDetailsTable.xaml
    /// </summary>
    public partial class ucLoanDetailsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        private Func<ObservableCollection<LoanDetail>> _getLoanDetails;
        public Func<ObservableCollection<LoanDetail>> getLoanDetails
        {
            get
            {
                return _getLoanDetails;
            }
            set
            {
                _getLoanDetails = value;
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
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucLoanDetailsTable), new PropertyMetadata(true));



        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucLoanDetailsTable), new PropertyMetadata(10));


        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Properties
        private LoanDetailDto _SelectedDto;
        public LoanDetailDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<LoanDetailDto> _LoanDetails;
        public ObservableCollection<LoanDetailDto> LoanDetails
        {
            get
            {
                return _LoanDetails;
            }
            set
            {
                _LoanDetails = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Mapping
        private LoanDetailMap loanDetailMap;
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


        public ucLoanDetailsTable()
        {
            InitializeComponent();
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            Loaded += ucLoanDetailsTable_Loaded;
            this.DataContext = this;
        }

        private void ucLoanDetailsTable_Loaded(object sender, RoutedEventArgs e)
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
                Utilitys.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
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
            var allDatas = getLoanDetails();

            var listFilledStatus = allDatas;
            LoanDetails = loanDetailMap.ConvertToDto(listFilledStatus);

            dgDatas.ItemsSource = LoanDetails;

            if (AllowPagination == true)
            {
                ucPagination.getUcLoanDetailsTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => LoanDetails.ToObservableCollectionObj();
            }
        }
    }
}
