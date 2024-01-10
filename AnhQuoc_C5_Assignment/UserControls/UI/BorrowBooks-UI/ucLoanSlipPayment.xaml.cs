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
    /// Interaction logic for ucLoanSlipPayment.xaml
    /// </summary>
    public partial class ucLoanSlipPayment : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<LoanSlipDto> getItem { get; set; }
        public Func<ucAddLoan> getUcAddLoan { get; set; }
        #endregion

        #region Properties
        private LoanSlipDto _Item;
        public LoanSlipDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private BookTitleViewModel bookTitleVM;
        private BookISBNViewModel bookISBNVM;
        private BookViewModel bookVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanSlipMap loanSlipMap;
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

        public ucLoanSlipPayment()
        {
            InitializeComponent();

            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;


            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            loanSlipMap = UnitOfMap.Instance.LoanSlipMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            btnConfirm.Click += BtnConfirm_Click;
            btnGoBack.Click += BtnGoBack_Click;
            this.DataContext = this;
            this.Loaded += UcLoanSlipPayment_Loaded;
        }

        private void UcLoanSlipPayment_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = true;
            if (Item.Deposit == 0)
            {
                isCheckEmptyItem = false;
            }
            if (Item.LoanPaid == 0)
            {
                isCheckEmptyItem = false;
            }

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            getUcAddLoan().CloseAndSave();
        }


        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            getUcAddLoan().GoBackPage();
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtLoanPaid))
                return true;
            if (Validation.GetHasError(txtDeposit))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtLoanPaid.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtDeposit.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}
