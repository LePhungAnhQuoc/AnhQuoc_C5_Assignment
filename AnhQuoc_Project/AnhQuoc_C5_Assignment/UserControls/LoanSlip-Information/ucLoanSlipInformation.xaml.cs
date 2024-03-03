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
    /// Interaction logic for ucLoanSlipInformation.xaml
    /// </summary>
    public partial class ucLoanSlipInformation : UserControl, INotifyPropertyChanged
    {
        #region Prop-Dp




        public bool IsDisplayListDetail
        {
            get { return (bool)GetValue(IsDisplayListDetailProperty); }
            set { SetValue(IsDisplayListDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDisplayListDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDisplayListDetailProperty =
            DependencyProperty.Register("IsDisplayListDetail", typeof(bool), typeof(ucLoanSlipInformation), new PropertyMetadata(true));



        #endregion

        #region GetDatas
        public Func<LoanSlipDto> getItem { get; set; }
        #endregion

        #region Prop
        private LoanSlipDto _Item;
        public LoanSlipDto Item
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
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;
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


        public ucLoanSlipInformation()
        {
            InitializeComponent();

            #region Allocate
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            #endregion

            this.DataContext = this;
            this.Loaded += ucLoanSlipInformation_Loaded;
            Utilities.SetToolTipForTextBlock(mainContent);
        }
        
        private void ucLoanSlipInformation_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsDisplayListDetail)
                ucLoanDetailsTable.Visibility = Visibility.Collapsed;

            Item = getItem();
            ucLoanDetailsTable.getLoanDetails = () => loanDetailVM.FillListByIdLoan(Item.Id);
            ucLoanDetailsTable.ModifiedPagination();

        }
    }
}
