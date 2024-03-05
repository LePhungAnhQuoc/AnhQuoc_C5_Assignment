using System;
using System.Collections.Generic;
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
    /// Interaction logic for ucLoanHistoryInformation.xaml
    /// </summary>
    public partial class ucLoanHistoryInformation : UserControl, INotifyPropertyChanged
    {
        #region Prop-Dp




        public bool IsDisplayListDetail
        {
            get { return (bool)GetValue(IsDisplayListDetailProperty); }
            set { SetValue(IsDisplayListDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDisplayListDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDisplayListDetailProperty =
            DependencyProperty.Register("IsDisplayListDetail", typeof(bool), typeof(ucLoanHistoryInformation), new PropertyMetadata(true));



        #endregion

        #region GetDatas
        public Func<LoanHistoryDto> getItem { get; set; }
        #endregion

        #region Prop
        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region ViewModels
        private LoanHistoryViewModel loanHistoryVM;
        private LoanDetailHistoryViewModel loanDetailHistoryVM;
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


        public ucLoanHistoryInformation()
        {
            InitializeComponent();

            #region Allocate
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;

            #endregion

            this.DataContext = this;
            this.Loaded += ucLoanHistoryInformation_Loaded;
            Utilities.SetToolTipForTextBlock(mainContent);
        }
        
        private void ucLoanHistoryInformation_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsDisplayListDetail)
                ucLoanDetailHistorysTable.Visibility = Visibility.Collapsed;

            Item = getItem();
            ucLoanDetailHistorysTable.getLoanDetailHistorys = () => loanDetailHistoryVM.FillListByIdLoanHistory(Item.Id);
            ucLoanDetailHistorysTable.ModifiedPagination();

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Utilities.ExpandMore((sender as TextBlock).Text);
        }
    }
}
